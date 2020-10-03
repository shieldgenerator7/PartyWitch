using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    #region Singleton
    private static PlayerController _instance;
    public static PlayerController Instance;
    #endregion

    public delegate void Interaction();
    public static event Interaction OnPlayerInteract;
    public static event Interaction OnPlayerJump;

    #region Initialization
    private PlayerActionControls playerActionControls;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private Collider2D col2d;
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
            Color color = doubleJumpIndicator.color;
            color.a = (_extraJumps > 0)
                ? 1
                : 0.3f;
            doubleJumpIndicator.color = color;
        }
    }
    public int extraJumpsValue;


    public Animator animator;
    public SpriteRenderer doubleJumpIndicator;

    private bool FacingRight = true;  // For determining which way the player is currently facing.

    //landing events

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;


    private bool jumpKeyDown = false;
    private bool jumpFirstFrame = false;
    private bool interactKeyDown = false;
    #endregion


    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    //-----------------

    private void Awake()
    {
        _instance = this;
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
        playerActionControls.Player.Jump.performed += ctx =>
        {
            jumpKeyDown = true;
            jumpFirstFrame = true;
            OnPlayerJump?.Invoke();
        };
        playerActionControls.Player.Jump.canceled += ctx => jumpKeyDown = false;
        playerActionControls.Player.Interact.performed += _ => interactCall(true);
        playerActionControls.Player.Interact.canceled += _ => interactCall(false);
    }

    private void interactCall(bool value)
    {
        interactKeyDown = value;
        if (interactKeyDown)
        {
            OnPlayerInteract?.Invoke();
        }
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
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
                jumpFirstFrame = false;
            }
        }
        else if (jumpFirstFrame && extraJumps > 0 && enableExtraJumps)
        {
            extraJumps--;
            rb.velocity = Vector2.up * jumpSpeed;
            animator.SetBool("isJumping", true);
            jumpFirstFrame = false;
        }

        if (!isGrounded)
        {
            Debug.Log("is grounded true");
            OnLandEvent.Invoke();
        }

        // Read the movement value
        float movementInput = playerActionControls.Player.Movement.ReadValue<float>();
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        //================
        // If the input is moving the player right and the player is facing left...
        if (movementInput < 0 && !FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movementInput > 0 && FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(movementInput));
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void resetExtraJumps(int extraextras = 0)
    {
        extraJumps = extraJumpsValue + extraextras;
    }

    public void OnLanding()
    {
        Debug.Log("isJumping false");
        animator.SetBool("isJumping", false);
    }

}
