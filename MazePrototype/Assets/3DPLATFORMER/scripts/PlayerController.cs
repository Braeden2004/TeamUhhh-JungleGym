using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public ParticleSystem puffLand;
    Rigidbody rb;
    [Header("Input")]
    public float xInput;
    public float zInput;
    public Vector3 moveDir;
    private bool puffed;
    public bool jumpHold;
    public bool isFalling;
    [Header("Parameters")]
    [SerializeField] float accelSpeed;
    public float maxSpeed;
    [SerializeField] float groundDrag;
    public float airControl;
    public float jumpPower;
    [SerializeField] LayerMask groundLayer;
    public float gravity;
    public float maxGravity;
    public bool useGravity;

    public bool canMove;

    //rotationtocamera
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleFriction();
        HandleGravity();
        HandleForward();

        AnimChecks();

        Jump();

        Debug.DrawRay(transform.position, Vector3.down * 1.2f);
    }

    private void FixedUpdate()
    {
        if(canMove)
        Move();
    }

    bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f, groundLayer))
        {
            return true;
        }
        return false;
    }

    void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpHold = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            jumpHold = false;
        }
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

    void HandleGravity()
    {
        if (!isGrounded() && useGravity)
        {
            if(rb.velocity.y < -maxGravity)
            {
                rb.velocity = new Vector3(rb.velocity.x, -maxGravity, rb.velocity.z);
            }
            else
            {
                rb.velocity -= new Vector3(0, gravity, 0) * Time.deltaTime;
            }
        }

        if(rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    void HandleForward()
    {
        //Get camera forward and right
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camRight.y = 0;
        camForward.y = 0;

        //Multiply camera's directional vectors by inputs
        Vector3 forwardRelative = camForward * zInput;
        Vector3 rightRelative = camRight * xInput;

        //Set desired move direction to be based on camera direction
        moveDir = (forwardRelative + rightRelative).normalized;

        //Rotate player
        Quaternion lookRot = Camera.main.transform.rotation;
        float yRot = lookRot.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }    

    void Move()
    {
        if (isGrounded())
        {
            rb.AddForce(moveDir.normalized * accelSpeed * Time.deltaTime, ForceMode.VelocityChange);
            //rb.velocity += (moveDir.normalized * accelSpeed);
        }
        else
        {
            rb.AddForce(moveDir.normalized * accelSpeed * Time.deltaTime * airControl, ForceMode.VelocityChange);
        }

        Vector3 groundVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (groundVel.magnitude > maxSpeed)
        {
            Vector3 clampedVel = groundVel.normalized * maxSpeed;
            rb.velocity = new Vector3(clampedVel.x, rb.velocity.y, clampedVel.z);
        }

        //rb.velocity = new Vector3(xInput * moveSpeed, rb.velocity.y, zInput * moveSpeed); //Use GetAxis for acceleration, but breaks rb.addforce
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(rb.velocity.x, jumpPower, rb.velocity.z), ForceMode.Impulse);
        }
    }

    void AnimChecks()
    {
        if (moveDir.magnitude < 0.1f)
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