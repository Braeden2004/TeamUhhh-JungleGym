using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    private Rigidbody rb;

    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Perform the ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);
        Jump();
    }

    void FixedUpdate()
    {
        // Input for player movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        // Apply movement
        Vector3 moveVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

}
