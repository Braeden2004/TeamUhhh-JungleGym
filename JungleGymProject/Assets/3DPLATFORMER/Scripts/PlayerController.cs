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
    bool puffed;
    public bool jumpHold;
    public bool isFalling;
    public bool canMove;
    bool slopeUp;

    [Header("Parameters")]
    public float accelSpeed;
    public float maxSpeed;
    public float deceleration;
    [Range(0, 1)] public float airControl;
    //[Range(0, 4)] public float friction;
    [SerializeField] LayerMask groundLayer;

    [Header("Gravity + Jumping")]
    public float gravity;
    public float maxGravity;
    public bool useGravity = true; //for wallrunning
    [SerializeField] float apexHeight = 4f;
    [SerializeField] float apexTime = 0.5f;
    public float jumpVel;

    [Header("Slope anim smoothing")]
    //For lerping slope rotation
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float animTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        //Initialize gravity & jump velocity
        gravity = -2 * apexHeight / Mathf.Pow(apexTime, 2);
        jumpVel = 2 * apexHeight / apexTime;
        useGravity = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        HandleFriction();
        HandleGravity();
        AnimChecks();

        Jump();
    }

    private void FixedUpdate()
    {

        HandleForward();

        if (canMove)
        {
            Move();
        }
    }

    public bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer))
        {
            return true;
        }
        return false;
    }

    public Vector3 AdjustVelocityToSlope(Vector3 velocity) //Smooth walking down slopes
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1.1f, groundLayer))
        {
            Quaternion slopeRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 adjustedVel = slopeRot * velocity;
            return adjustedVel;
        }
        return velocity;
    }

    void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");


        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpHold = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            jumpHold = false;
        }*/
    }

    void HandleFriction()
    {
        /*if(isGrounded())
        {
            rb.drag = friction;
        }
        else
        {
            rb.drag = 0f;
        }*/

        /*if(isGrounded())
        {
            rb.AddForce(friction * -rb.velocity);
        }*/

        Vector3 xzVel = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized;

        if (isGrounded() && xzVel != Vector3.zero)
        {
            rb.AddForce(deceleration * -xzVel);

            if(Mathf.Abs(rb.velocity.magnitude) < 0.25f)
            {
                rb.velocity = Vector3.zero;
            }
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
                rb.velocity += new Vector3(0, gravity, 0) * Time.deltaTime;
            }
        }

        /*if(rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }*/
    }

    void CheckSlopeDirection()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit slopeHit;

        if (Physics.Raycast(ray, out slopeHit, 1.1f, groundLayer))
        {
            float slopeAngle = Vector3.Angle(slopeHit.normal, transform.forward);
            var dot = Vector3.Dot(slopeHit.normal, transform.forward);
            if(dot < 0f)
            {
                slopeUp = true;
            }
            else if(dot > 0f)
            {
                slopeUp = false;
            }
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
        Quaternion moveRot = Quaternion.Euler(0, yRot, 0);

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, groundLayer))
        {
            //Quaternion rotationRef = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * moveRot, animCurve.Evaluate(animTime)); //Get rotation of slope
            //transform.rotation = Quaternion.Euler(rotationRef.eulerAngles.x, yRot, rotationRef.eulerAngles.z); //Rotate to slope
            //if(!swinging.isSwinging) //FOR SWINGING OBJECT
            transform.rotation = Quaternion.Euler(transform.rotation.x, yRot, transform.rotation.z);
        }
    }


    void ClampGroundVel()
    {
        Vector3 groundVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (groundVel.magnitude > maxSpeed)
        {
            Vector3 clampedVel = groundVel.normalized * maxSpeed;
            rb.velocity = new Vector3(clampedVel.x, rb.velocity.y, clampedVel.z);
        }
    }

    void Move()
    {
        //Original settings
        if (moveDir != Vector3.zero)
        {
            Vector3 newVel = Vector3.zero;
            if (isGrounded())
            {
                newVel = moveDir * accelSpeed * Time.deltaTime;
                rb.AddForce(newVel, ForceMode.VelocityChange);
            }
            else
            {
                newVel = moveDir * accelSpeed * airControl * Time.deltaTime;
            }

            CheckSlopeDirection();
            if (!slopeUp)
            {
                newVel = AdjustVelocityToSlope(newVel);
            }

            rb.AddForce(newVel, ForceMode.VelocityChange);

        }

        /*if (moveDir != Vector3.zero)
        {
            Vector3 newVel = Vector3.zero;
            if (isGrounded())
            {
                newVel = moveDir * accelSpeed * Time.deltaTime; //Remove deltatime for extremely snappy movement
            }
            else
            {
                newVel = moveDir * accelSpeed * Time.deltaTime * airControl;
            }

            CheckSlopeDirection();
            if(!slopeUp)
            {
                newVel = AdjustVelocityToSlope(newVel);
            }
            rb.velocity += newVel;
        }*/

        ClampGroundVel();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, jumpVel, 0), ForceMode.Impulse); //Change rb.velocity.x/z values to 0 for less boosty jump
        }
    }

    void AnimChecks() //Turn off for roll test
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