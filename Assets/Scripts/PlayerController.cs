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

    private bool jumpKeyDown = false;
    private bool jumpFirstFrame = false;
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
        playerActionControls.Player.Jump.performed += ctx =>
        {
            jumpKeyDown = true;
            jumpFirstFrame = true;
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
            if (jumpKeyDown)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                animator.SetBool("isJumping", true);
                if (jumpFirstFrame)
                {
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                }
                jumpFirstFrame = false;
            }
        }
        else if (jumpFirstFrame && extraJumps > 0 && enableExtraJumps)
        {
            extraJumps--;
            rb.velocity = Vector2.up * jumpSpeed;
            animator.SetBool("isJumping", true);
            AudioSource.PlayClipAtPoint(doubleJumpSound, transform.position);
            jumpFirstFrame = false;
        }

        // Read the movement value
        float movementInput = playerActionControls.Player.Movement.ReadValue<float>();
        // Move the player
        rb.velocity = new Vector2(movementInput * speed, rb.velocity.y);

        //Movement visual effect
        if (movementInput != 0)
        {
            FacingRight = movementInput > 0;
        }
        animator.SetFloat("Speed", Mathf.Abs(movementInput));

        //Move sound effect
        if (movementInput != 0)
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
    }

    public void resetExtraJumps(int extraextras = 0)
    {
        extraJumps = extraJumpsValue + extraextras;
    }

}
