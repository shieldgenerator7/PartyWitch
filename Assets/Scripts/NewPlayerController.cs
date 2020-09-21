using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private PlayerActionControls playerActionControls;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    private Collider2D col2d;

    // Runs before Start
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
        // Don't want to pass in anything into the Jump function so we use an underscore
        playerActionControls.Player.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
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
        // Read the movement value
        float movementInput = playerActionControls.Player.Movement.ReadValue<float>();
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;
    }
}
