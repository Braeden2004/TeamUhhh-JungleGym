using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    Rigidbody rb;
    float xInput;
    float zInput;
    Vector3 moveDir;

    [SerializeField] float moveSpeed;
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

        //moveDir = new Vector3(xInput, 0, zInput);
    }

    void Move()
    {
        //rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Acceleration);

        rb.velocity = new Vector3(xInput * moveSpeed, rb.velocity.y, zInput * moveSpeed); 
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded()) 
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }
    void AnimChecks()
    {
        if (rb.velocity == new Vector3(0, 0, 0))
        {
            animator.SetBool("IsIdle", true);
        }
        else
        {
            animator.SetBool("IsIdle", false);
        }

    }
}
