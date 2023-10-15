using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Code Assistance from https://www.youtube.com/watch?v=CieCJ2mNTXE

    [Header("Movement")]
    public float acceleration;
    public float maxSpeed; // Maximum speed the player can reach

    public float groundDrag;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        //assign rigidbody and freeze its rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        // Clamp the player's velocity to the maximum speed
        Vector3 velocityLimiter = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        rb.velocity = velocityLimiter;

        //curent speed of player
        float currentSpeed = rb.velocity.magnitude;
        Debug.Log("Current Speed: " + currentSpeed);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculate movement direction based on direction player is facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Add force to make the thing move
        rb.AddForce(moveDirection.normalized * acceleration * 10f, ForceMode.Force);
    }

}
