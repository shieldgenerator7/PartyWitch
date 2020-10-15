using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


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
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Used for variable jump height")]
    public float lowJumpGravityMultiplier = 2;
    private Rigidbody2D rb;
    public bool enableExtraJumps = false;

    public Transform groundCheck;
    public float checkRadius;
    private bool isGrounded;
    private int _extraJumps;
    private int extraJumps
    {
        get => _extraJumps;
        set
        {
            _extraJumps = value;
            doubleJumpIndicator.color = (_extraJumps > 0)
                ? Color.white
                : noExtraJumpColor;
        }
    }
    public int extraJumpsValue;


    public Animator animator;
    public SpriteRenderer doubleJumpIndicator;
    public Color noExtraJumpColor = new Color(1, 1, 1, 0.3f);

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
    public AudioClip doubleJumpSound;
    public AudioClip landSound;
    public AudioSource moveSound;

    //-----------------

    private void Awake()
    {
        Instance = this;
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        extraJumps = extraJumpsValue;
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
        if (!wasGrounded && isGrounded)
        {
            AudioSource.PlayClipAtPoint(landSound, transform.position);
            animator.SetBool("isJumping", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            resetExtraJumps();
            if (jumpFirstFrame)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                jumpFirstFrame = false;
            }
        }
        else
        {
            if (jumpFirstFrame && extraJumps > 0 && enableExtraJumps)
            {
                extraJumps--;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                AudioSource.PlayClipAtPoint(doubleJumpSound, transform.position);
                jumpFirstFrame = false;
            }
            if (rb.velocity.y > 0 && !jumpKeyDown)
            {
                rb.velocity += Vector2.up
                    * Physics2D.gravity.y
                    * (lowJumpGravityMultiplier - 1)
                    * Time.deltaTime;
            }
        }

        // Move the player
        rb.velocity = new Vector2(movementInput.x * speed, rb.velocity.y);

    }

    public void resetExtraJumps(int extraextras = 0)
    {
        extraJumps = extraJumpsValue + extraextras;
    }

}
