using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    Rigidbody rb;
    public float xInput;
    public float zInput;
    Vector3 moveDir;

    [SerializeField] float accelSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Move();
        Jump();
        AnimChecks();
    }

    bool isGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, 2f, groundLayer))
        {
            return true;
        }
        return false;
    }

    void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(xInput, 0, zInput);
    }

    void Move()
    {
        rb.AddForce(moveDir.normalized * accelSpeed, ForceMode.Acceleration);

        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        //rb.velocity = new Vector3(xInput * moveSpeed, rb.velocity.y, zInput * moveSpeed); 
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) 
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }
    void AnimChecks()
    {
        if (rb.velocity == Vector3.zero)
        {
            animator.SetBool("IsIdle", true);
        }
        else
        {
            animator.SetBool("IsIdle", false);
        }

    }
}
