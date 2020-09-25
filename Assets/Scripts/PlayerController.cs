using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActionControls playerActionControls;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    private Collider2D col2d;

    private bool crouch = false;

    //Vector2 inputDir;
    //PlayerControls playerControls;

    //Rigidbody2D rb2d;
    //Vector2 walkInput;
    //Vector2 crouchInput;
    //Vector2 jumpInput;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerControls = new PlayerControls();
        //playerControls.Player.Movement.performed += movePlayer;
        //playerControls.Enable();

        //rb2d = GetComponent<Rigidbody2D>();

        playerActionControls.Player.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private void Crouch()
    {
        print("Crouching");
    }

    private bool isGrounded()
    {
        Vector2 feetPos = transform.position;
        feetPos.y -= col2d.bounds.extents.y;
        return Physics2D.OverlapCircle(feetPos, .1f, ground);
    }

    // Update is called once per frame
    void Update()
    {   
        if (playerActionControls.Player.Crouch.triggered)
        {
            print("Is crouching");
        }

        // Read the movement value
        float movementInput = playerActionControls.Player.Movement.ReadValue<float>();
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;
    }

    //private void movePlayer(InputAction.CallbackContext obj)
    //{
    //    inputDir = obj.ReadValue<Vector2>();
    //}
}
