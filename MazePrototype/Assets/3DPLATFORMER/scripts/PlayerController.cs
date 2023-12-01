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
    [SerializeField] float maxGravity;

    //rotationtocamera
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    

        //RotateWithCamera();
        GetInput();
        HandleFriction();
        HandleGravity();

        AnimChecks();

        Debug.DrawRay(transform.position, Vector3.down * 1.2f);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
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

        #region Rotation to camera direction
        //RotateBody
        Vector3 camDir = Camera.main.transform.forward;
        camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);

        transform.forward = camDir;

        //move in direction of camera
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        moveDir = Vector3.ProjectOnPlane(moveDir, Vector3.up);
        #endregion

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
        if (!isGrounded())
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


    void RotateWithCamera()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }
    }
}