using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Roll : MonoBehaviour
{
    [Header("Audio")]
    AudioManager audioManager;
    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    PlayerController player;
    Glide glide;
    Rigidbody rb;
    public bool isRolling;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Animator anim;

    [Header("Player values")]
    [SerializeField][Range(0.4f, 0.7f)] float playerScale;
    float originalScale;
    [SerializeField][Range(1, 3)] float groundMaxSpeedMultiplier;
    [SerializeField][Range(2, 6)] float slopeMaxSpeedMultiplier;
    //[SerializeField][Range(0, 1)] float decelMultiplier;
    [SerializeField][Range(0, 1)] float frictionMultiplier;
    [SerializeField][Range(0, 1)] float accelSpeedMultiplier;
    [SerializeField][Range(0, 2)] float jumpHeightMultiplier;
    float originalMaxSpeed;
    float originalAccel;
    float originalFriction;
    float originalJumpHeight;

    [Header("Roll speed values")]
    [SerializeField] float rollBoostForce;
    [SerializeField] float rollForce;
    bool slammed;
    bool jumped;

    [Header("Slope calculation variables")]
    RaycastHit slopeHit;
    Vector3 slopeNormal;
    float slopeAngle;
    float slopeAccel;
    public Vector3 slopeDir;

    float rollingNumber;
    public bool rolledOnSlope;
    public float maxSpeedTimeLimit;
    float maxSpeedTimer;
    float reverseRollTimer;
    public float reverseRollDelay;

    HingeRopeSwing swing;
    PlayerSwing balloon;

    public AnimationCurve animCurve;

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        glide = GetComponent<Glide>();
        swing = GetComponent<HingeRopeSwing>();
        balloon = GetComponent<PlayerSwing>();
        originalScale = transform.localScale.y;
        originalMaxSpeed = player.maxSpeed;
        originalAccel = player.accelSpeed;
        originalFriction = player.frictionRate;
        originalJumpHeight = player.jumpVel;
        rollingNumber = 0;
    }

    void Update()
    {
        CheckForSlope();

        if (Input.GetButtonDown("Roll"))
        {
            OnStartRoll();
            //AUDIO FOR START OF ROLL HERE
            audioManager.PlaySFX(audioManager.roll);

            rollingNumber++;

            //TelemetryLogger.Log(this, "Times Rolled", rollingNumber);
        }

        if (Input.GetButtonUp("Roll") && !swing.isSwinging)
        {
            OnStopRoll();
        }

        if (player.isGrounded())
        {
            slammed = false;
            jumped = false;
        }

        if(rolledOnSlope)
        {
            player.maxSpeed = originalMaxSpeed * slopeMaxSpeedMultiplier;
            maxSpeedTimer += Time.deltaTime;
            if(maxSpeedTimer > maxSpeedTimeLimit)
            {
                player.maxSpeed = originalMaxSpeed * groundMaxSpeedMultiplier;
            }
        }

        UpdateAnim();
    }

    private void FixedUpdate()
    {
        if (isRolling)
        {
            glide.tired = true;
            //AUDIO FOR CONTINUOUS ROLL HERE
            //NEEDS COROUTINE
            //RollMove();
            if (OnSlope())
            {
                Vector3 slopeForce = slopeAccel * rollForce * slopeDir;

                var slopeDot = Vector3.Dot(rb.velocity, slopeDir);
                if(slopeDot > 0)
                {
                    rb.AddForce(slopeForce, ForceMode.Acceleration); //Add force down the slope
                    reverseRollTimer = 0;
                }
                else if(slopeDot < 0)
                {
                   reverseRollTimer += Time.deltaTime;
                    if(reverseRollTimer > reverseRollDelay)
                    {
                        rb.AddForce(slopeForce, ForceMode.Acceleration);
                    }
                }    

                rolledOnSlope = true;

                var dot = Vector3.Dot(slopeHit.normal, transform.forward);
                if (dot < 0f)
                {
                    rb.AddForce(slopeForce, ForceMode.Acceleration); //Add extra force if a player is trying to roll up a hill
                }
                
                /*else if (player.moveDir == Vector3.zero)
                {
                    rb.velocity = slopeDir * rb.velocity.magnitude; //Set velocity direction to follow slope -> This causes massive acceleration randomly
                }*/
            }
        }
    }

    public bool OnSlope()
    {
        if (!player.isGrounded())
        {
            return false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.1f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    void OnStartRoll()
    {
        isRolling = true;
        player.maxSpeed = originalMaxSpeed * groundMaxSpeedMultiplier;
        //transform.localScale = new Vector3(transform.localScale.x, playerScale, transform.localScale.z);
        //player.deceleration *= decelMultiplier;
        player.frictionRate = originalFriction * frictionMultiplier;
        player.accelSpeed = originalAccel * accelSpeedMultiplier;
        player.jumpVel = originalJumpHeight * jumpHeightMultiplier;
        RollBoosts();
    }

    void OnStopRoll()
    {
        isRolling = false;
        player.maxSpeed = originalMaxSpeed;
        player.accelSpeed = originalAccel;
        //player.deceleration /= decelMultiplier;
        player.frictionRate = originalFriction;
        player.jumpVel = originalJumpHeight;
        rolledOnSlope = false;
        reverseRollTimer = 0;
        maxSpeedTimer = 0;
        //transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
    }

    void RollBoosts()
    {
        if (player.moveDir == Vector3.zero && !player.isGrounded() && !slammed) //Slam downward if there's no input
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * rollBoostForce, ForceMode.Impulse);
            slammed = true;
        }

        else if (!jumped)//Otherwise, add a small upwards boost
        {
            rb.AddForce(Vector3.up * rollBoostForce / 2f, ForceMode.Impulse);
            jumped = true;
        }
    }

    void CheckForSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out slopeHit, 1.1f, groundMask))
        {
            slopeNormal = slopeHit.normal;
            slopeAngle = Vector3.Angle(Vector3.up, slopeNormal);

            slopeAccel = Mathf.Tan(slopeAngle * Mathf.Deg2Rad); //Calculate slope acceleration
            slopeDir = Vector3.Cross(Vector3.Cross(slopeNormal, -Vector3.up), slopeNormal).normalized; //Get direction of slope
            //float slopeAngle = Vector3.Angle(slopeHit.normal, transform.forward);
        }
        Debug.DrawRay(slopeHit.point, slopeDir * 100f, Color.red);
    }

    void UpdateAnim()
    {
        if (isRolling)
        {
            anim.SetBool("IsRolling", isRolling);
        }
        else
        {
            anim.SetBool("IsRolling", false);
        }

        Vector3 xzVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float isAtMaxSpeed = xzVel.magnitude / player.maxSpeed;
        anim.SetFloat("Velocity", isAtMaxSpeed);
    }
}
