using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public ParticleSystem puffLand;
    Rigidbody rb;
    [Header("Values")]
    public float xInput;
    public float zInput;
    Vector3 moveDir;
    private bool puffed;
    [Header("Parameters")]
    [SerializeField] float accelSpeed;
    [SerializeField] public float maxSpeed;
    [SerializeField] float groundDrag;
    [SerializeField] public float airControl;
    [SerializeField] public float jumpPower;
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
        HandleFriction();

        Move();
        Jump();
        AnimChecks();

        Debug.DrawRay(transform.position, Vector3.down * 1.2f);
    }

    bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f, groundLayer))
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

    void HandleFriction()
    {
        if(isGrounded())
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }    
    }

    void Move()
    {
        if (isGrounded())
        {
            //rb.AddForce(moveDir.normalized * accelSpeed, ForceMode.Acceleration);
            rb.AddForce(moveDir.normalized * accelSpeed / 10f, ForceMode.VelocityChange);
        }
        else
        {
            //rb.AddForce(moveDir.normalized * accelSpeed * airControl, ForceMode.Acceleration);
            rb.AddForce(moveDir.normalized * accelSpeed / 10f * airControl, ForceMode.VelocityChange);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        /*Vector3 groundVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (groundVel.magnitude > maxSpeed)
        {
            Vector3 clampedVel = groundVel.normalized * maxSpeed;
            rb.velocity = new Vector3(clampedVel.x, rb.velocity.y, clampedVel.z);
        }*/

        //rb.velocity = new Vector3(xInput * moveSpeed, rb.velocity.y, zInput * moveSpeed); //Use GetAxis for acceleration, but breaks rb.addforce
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
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
        
        if(isGrounded() == true && puffed == false)
        {
            puffLand.Play();
            puffed = true;
        }
        if(isGrounded() == false)
        {
            puffed = false;

        }

    }
}