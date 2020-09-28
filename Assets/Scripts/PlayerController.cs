using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    private PlayerActionControls playerActionControls;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private Collider2D col2d;
    private bool enableExtraJumps = false;

    public Transform groundCheck;
    public float checkRadius;
    private bool isGrounded;
    private int extraJumps;
    public int extraJumpsValue;

	public Animator animator;
    private bool jumpBtnDown = false;
	
	private bool FacingRight = true;  // For determining which way the player is currently facing.
	
	//landing events

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	

	
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	

    //-----------------
	
	private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
		
		if (OnLandEvent == null)
		OnLandEvent = new UnityEvent();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
        playerActionControls.Player.Jump.performed += ctx => jumpBtnDown = true;
        playerActionControls.Player.Jump.canceled += ctx => jumpBtnDown = false;
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

    public void TestFunction()
    {
        print("This is a test");
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (jumpBtnDown && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpSpeed;
			animator.SetBool("isJumping", true);
            extraJumps--;
        }
        else if (jumpBtnDown && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
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
	
	public void OnLanding ()
	{
		Debug.Log("isJumping false");
		animator.SetBool("isJumping", false);
	}

}
