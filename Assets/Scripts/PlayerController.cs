using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance;
    #endregion

    public delegate void ButtonPressed();
    public static event ButtonPressed OnPlayerInteract;
    public static event ButtonPressed OnPlayerJump;
    public static event ButtonPressed OnGamePaused;

    #region Initialization
    private PlayerActionControls playerActionControls;
    [SerializeField] private float speed;
    public float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Used for variable jump height")]
    public float lowJumpGravityMultiplier = 2;
    [Tooltip("Used for anti-floaty jumps")]
    public float fallGravityMultiplier = 2;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public float checkRadius;
    public bool isGrounded { get; private set; }


    public Animator animator;

    /// <summary>
    /// For determining which way the player is currently facing.
    /// True if facing to the right
    /// </summary>
    private bool FacingRight
    {
        get => transform.localScale.x > 0;
        set
        {
            //If the player is not facing the correct direction,
            if (value != FacingRight)
            {
                // Multiply the player's x local scale by -1.
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }
    #endregion

    #region Input Variables

    //Movement
    private Vector2 movementInput = Vector2.zero;
    //Jump
    private bool jumpKeyDown = false;
    private bool jumpFirstFrame = false;
    //Pause
    private bool isPaused = false;

    #endregion

    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioSource moveSound;

    //-----------------

    private void Awake()
    {
        Instance = this;
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        GetComponents<PlayerAbility>().ToList()
            .ForEach(pa => pa.registerDelegates());
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
        playerActionControls.Player.Movement.performed += ctx =>
        {
            movementInput = ctx.ReadValue<Vector2>();
            animator.SetFloat("Speed", Mathf.Abs(movementInput.x));
            if (movementInput.x != 0)
            {
                FacingRight = movementInput.x > 0;
            }
            //Move sound effect
            if (movementInput.x != 0)
            {
                if (!moveSound.isPlaying)
                {
                    moveSound.Play();
                }
            }
            else
            {
                if (moveSound.isPlaying)
                {
                    moveSound.Pause();
                }
            }
        };
        playerActionControls.Player.Jump.performed += ctx =>
        {
            jumpKeyDown = true;
            jumpFirstFrame = true;
            animator.SetBool("isJumping", true);
            OnPlayerJump?.Invoke();
        };
        playerActionControls.Player.Jump.canceled += ctx => jumpKeyDown = false;
        playerActionControls.Player.Interact.performed += _ => OnPlayerInteract?.Invoke();
        playerActionControls.Player.Pause.performed += ctx => onPause();
    }

    private void onPause()
    {
        isPaused = !isPaused;
        OnGamePaused?.Invoke();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    private void FixedUpdate()
    {
        //Do grounded check
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (!isGrounded && groundedCheck != null)
        {
            isGrounded = groundedCheck.GetInvocationList().Any(
                del => ((GroundedCheck)del).Invoke()
                );
        }
        if (!wasGrounded && isGrounded)
        {
            AudioSource.PlayClipAtPoint(landSound, transform.position);
            animator.SetBool("isJumping", false);
            onGrounded?.Invoke();
        }
    }
    public delegate bool GroundedCheck();
    public event GroundedCheck groundedCheck;
    public delegate void OnGrounded();
    public event OnGrounded onGrounded;

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (jumpFirstFrame)
            {
                Jump(jumpSound);
            }
        }
        else
        {
            //Variable jump height
            if (rb.velocity.y > 0 && !jumpKeyDown)
            {
                rb.velocity += Vector2.up
                    * Physics2D.gravity.y
                    * (lowJumpGravityMultiplier - 1)
                    * Time.deltaTime;
            }
            //Anti-floaty jump
            else if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up
                    * Physics2D.gravity.y
                    * (fallGravityMultiplier - 1)
                    * Time.deltaTime;
            }
        }

        // Move the player
        rb.velocity = new Vector2(movementInput.x * speed, rb.velocity.y);

    }

    public void Jump(AudioClip jumpSound)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        jumpFirstFrame = false;
    }

}
