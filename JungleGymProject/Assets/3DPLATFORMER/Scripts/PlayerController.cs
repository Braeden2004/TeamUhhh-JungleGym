using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [Header("Audio")]
    AudioManager audioManager;
    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //Initialize gravity, friction, max jump height
        gravity = -2 * apexHeight / Mathf.Pow(apexTime, 2);
        jumpVel = 2 * apexHeight / apexTime;
        frictionRate = maxSpeed / timeToZero;
    }

    public int jumpTotal;

    [Header("References")]
    public Animator animator;
    public ParticleSystem puffLand;
    public Roll roll;
    PlayerSwing balloon;
    HingeRopeSwing swing;
    Rigidbody rb;

    [Header("Input")]
    public float xInput;
    public float zInput;
    public Vector3 moveDir;
    bool puffed;
    public bool jumpHold;
    public bool isFalling;
    public bool canMove;
    bool slopeDown;

    [Header("Parameters")]
    public float accelSpeed;
    public float maxSpeed;
    //public float deceleration;
    //public float airDeceleration;
    public float frictionRate;
    public float airFrictionRate;
    [SerializeField] float timeToZero;
    [Range(0, 2)] public float airControl;
    [SerializeField] LayerMask groundLayer;

    [Header("Gravity + Jumping")]
    public float gravity;
    public float maxGravity;
    public bool useGravity = true; //for wallrunning
    [SerializeField] float apexHeight = 4f;
    [SerializeField] float apexTime = 0.5f;
    public float jumpVel;

    //JumpBuffer + Coyote Time
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    //variable jump height
    public float minimumJumpHeight = 1.5f;

    /*[Header("Slope anim smoothing")]
    //For lerping slope rotation
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float animTime;*/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        balloon = GetComponent<PlayerSwing>();
        swing = GetComponent<HingeRopeSwing>();

        useGravity = true;
        canMove = true;

        jumpTotal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        HandleGravity();
        AnimChecks();

        Jump();
    }

    private void FixedUpdate()
    {
        HandleForward();
        HandleFriction();

        if (canMove)
        {
            Move();
        }

    }

    public bool isGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer))
        {
            animator.SetBool("IsGrounded", true);
            return true;
        }
        animator.SetBool("IsGrounded", false);
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
        /*if(isGrounded() && !roll.isRolling)
        {
            rb.drag = friction;
        }
        else if (roll.isRolling)
        {
            rb.drag = rollFriction;
        }
        else if(!isGrounded())
        {
            rb.drag = 0f;
        }*/

        if (isGrounded() && moveDir == Vector3.zero)
        {
            rb.velocity -= frictionRate * rb.velocity * Time.fixedDeltaTime;
        }
        else if (!isGrounded() && !roll.isRolling) //&& !swing.isSwinging) //&& !balloon.isSwinging)
        {
            Vector3 xzVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity -= airFrictionRate * xzVel * Time.fixedDeltaTime;
        }

        /*if(isGrounded())
        {
            rb.AddForce(friction * -rb.velocity);
        }*/

        /*Vector3 xzVel = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized;

        if (isGrounded() && xzVel.magnitude > 0.1f)
        {
            rb.AddForce(deceleration * -xzVel);
        }
        else if (!isGrounded() && xzVel != Vector3.zero)
        {
            rb.AddForce(airDeceleration * -xzVel);
        }*/
    }

    void HandleGravity()
    {
        if (!isGrounded() && useGravity)
        {
            if (rb.velocity.y < -maxGravity)
            {
                rb.velocity = new Vector3(rb.velocity.x, -maxGravity, rb.velocity.z);
            }
            else
            {
                rb.velocity += new Vector3(0, gravity, 0) * Time.deltaTime;
            }
        }

        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }

        else
        {
            if (isFalling == true)
            {
                //AUDIO QUEUE
                audioManager.PlaySFX(audioManager.land);

                isFalling = false;
            }

        }
    }

    void CheckSlopeDirection()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit slopeHit;

        if (Physics.Raycast(ray, out slopeHit, 1.1f, groundLayer))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            float slopeDirAngle = Vector3.Angle(slopeHit.normal, moveDir);

            int x = (int)Mathf.Round(slopeAngle);
            int y = (int)slopeDirAngle;

            if (x % (90f - y) == 0)
            {
                slopeDown = true;
            }
            else
            {
                slopeDown = false;
            }

            /*var dot = Vector3.Dot(slopeHit.normal, transform.forward);
            if(dot < 0f)
            {
                slopeUp = true;
            }
            else if(dot > 0f)
            {
                slopeUp = false;
            }*/
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

        /*
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
                transform.rotation = Quaternion.Euler(transform.rotation.x, yRot, transform.rotation.z);
            }
        */
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
        if (moveDir != Vector3.zero)
        {
            Vector3 newVel = Vector3.zero;
            if (isGrounded())
            {
                newVel = moveDir * accelSpeed * Time.fixedDeltaTime;
                rb.AddForce(newVel, ForceMode.VelocityChange); //Add extra force when on the ground to reach max speed faster
            }
            else
            {
                newVel = moveDir * accelSpeed * airControl * Time.fixedDeltaTime;
            }

            CheckSlopeDirection();
            if (slopeDown)
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


        //AUDIO FOR MOVE HERE
        //NEEDS COROUTINE
        /*int randmove = Random.Range(0, 3);
        if(randmove == 0)
        {
            //audioManager.PlaySFX(audioManager.run1);
        }
        if (randmove == 1)
        {
            //audioManager.PlaySFX(audioManager.run2);
        }
        if (randmove == 2)
        {
            //audioManager.PlaySFX(audioManager.run3);
        }
        if (randmove == 3)
        {
            //audioManager.PlaySFX(audioManager.run4);
        }*/
    }

    void Jump()
    {
        //Cyote Time
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            //countdown timer
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Jump Buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //reset timer
            jumpBufferCounter = jumpBufferTime;

        }
        else if (jumpBufferCounter>0)
        {
            //count down timer
            jumpBufferCounter -= Time.deltaTime;
        }

        //Jump Logic
        if ((jumpBufferCounter > 0) && (coyoteTimeCounter > 0))
        {
            //AUDIO QUEUE
            audioManager.PlaySFX(audioManager.jump);

            jumpTotal++;
            //TelemetryLogger.Log(this, "Jump Amount", jumpTotal);

            //make player jump
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(rb.velocity.x, jumpVel, rb.velocity.z), ForceMode.Impulse); //Change rb.velocity.x/z values to 0 for less boosty jump

            //reset values
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0;
        }

        //Prevent player from double Jumping
        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0f;
        }

        //Variable Jump Height //SOURCE: https://www.youtube.com/watch?v=Mo1-sKYbks0
        if (Input.GetKeyUp(KeyCode.Space) && (rb.velocity.y > 0))
        {
            Debug.Log("Reducing jump height");

            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / minimumJumpHeight, rb.velocity.z); //reduce upward velocity
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

        if (isGrounded() == true && puffed == false)
        {
            puffLand.Play();
            puffed = true;
        }
        if (isGrounded() == false)
        {
            puffed = false;
        }
    }
}