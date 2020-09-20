using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scripts : MonoBehaviour
{
    Vector2 inputDir;
    PlayerControls playerControls;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Movement.performed += movePlayer;
        playerControls.Enable();

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //this where you decide which direction to move based on input dir
        float speedYouWant = 5.1f;
        rb2d.AddForce(Vector2.right * rb2d.mass * speedYouWant);
        rb2d.velocity = new Vector2(speedYouWant, rb2d.velocity.y);
    }

    private void movePlayer(InputAction.CallbackContext obj)
    {
        inputDir = obj.ReadValue<Vector2>();
    }
}
