using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private bool jumpBtnDown = false;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
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

    //private void Jump()
    //{
    //    if (onGround)
    //    {
    //        rb.velocity = Vector2.up * jumpSpeed;
    //        canDoubleJump = true;
    //    }
    //}

    //private bool isGrounded()
    //{
    //    Vector2 feetPos = transform.position;
    //    feetPos.y -= col2d.bounds.extents.y;
    //    return Physics2D.OverlapCircle(feetPos, .1f, ground);
    //}

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
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
            extraJumps--;
        }
        else if (jumpBtnDown && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }

        // Read the movement value
        float movementInput = playerActionControls.Player.Movement.ReadValue<float>();
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;
    }

}
