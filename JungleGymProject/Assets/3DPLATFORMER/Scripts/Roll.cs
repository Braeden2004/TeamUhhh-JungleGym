using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    PlayerController player;
    Rigidbody rb;
    CapsuleCollider col;
    public bool isRolling;
    [SerializeField] LayerMask groundMask;

    [Header("Player values")]
    [SerializeField][Range(0.4f, 0.7f)] float playerScale;
    float originalScale;
    //[SerializeField] float rollMaxSpeed;
    //float originalMaxSpeed;
    //[SerializeField] [Range(0, 0.5f)] float rollFriction;
    //float originalFriction;
    //[SerializeField] float rollDecel;
    //float originalDecel;
    [SerializeField][Range(1, 2)] float maxSpeedMultiplier;
    [SerializeField][Range(0, 1)] float decelMultiplier;
    [SerializeField][Range(0, 1)] float accelSpeedMultiplier;
    [SerializeField][Range(0, 2)] float jumpHeightMultiplier;

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

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale.y;
        //originalFriction = player.friction;
    }

    void Update()
    {
        CheckForSlope();

        if (Input.GetButtonDown("Roll"))
        {
            OnStartRoll();
        }

        if(Input.GetButtonUp("Roll"))
        {
            OnStopRoll();
        }
        
        if(isRolling)
        {
            //RollMove();
            if (OnSlope())
            {
                rb.AddForce(slopeAccel * rollForce * slopeDir); //Add force down the slope
                if (player.moveDir != slopeDir)
                {
                    rb.AddForce(rollForce * slopeDir); //Add extra force if a player is trying to roll up a hill
                }
                if(player.moveDir == Vector3.zero)
                {
                    //rb.velocity = slopeDir * rb.velocity.magnitude; //Set velocity direction to follow slope
                }
            }
        }
        else if (player.isGrounded())
        {
            slammed = false;
            jumped = false;
        }
    }

    private bool OnSlope()
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
        transform.localScale = new Vector3(transform.localScale.x, playerScale, transform.localScale.z);
        player.maxSpeed *= maxSpeedMultiplier;
        //player.friction = rollFriction;
        player.deceleration *= decelMultiplier;
        player.accelSpeed *= accelSpeedMultiplier;
        player.jumpVel *= jumpHeightMultiplier;
        //player.canMove = false;
        RollBoosts();
    }

    void OnStopRoll()
    {
        isRolling = false;
        player.maxSpeed /= maxSpeedMultiplier;
        //player.friction = originalFriction;
        player.deceleration /= decelMultiplier;
        player.accelSpeed /= accelSpeedMultiplier;
        player.jumpVel /= jumpHeightMultiplier;
        //player.canMove = true;
        transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
    }

    void RollBoosts()
    {
        if(player.moveDir == Vector3.zero && !player.isGrounded() && !slammed) //Slam downward if there's no input
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * rollBoostForce, ForceMode.Impulse);
            slammed = true;
        }

        else if(!jumped)//Otherwise, add a small upwards boost
        {
            rb.AddForce(Vector3.up * rollBoostForce / 3f, ForceMode.Impulse);
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

            slopeAccel = Mathf.Sin(slopeAngle * Mathf.Deg2Rad); //Calculate slope acceleration
            slopeDir = Vector3.Cross(Vector3.Cross(slopeNormal, -Vector3.up), slopeNormal).normalized; //Get direction of slope
            //float slopeAngle = Vector3.Angle(slopeHit.normal, transform.forward);
        }
        Debug.DrawRay(slopeHit.point, slopeDir * 100f, Color.red);

    }

    /*void RollMove()
    {
        if (player.isGrounded())
        {
            Vector3 vel = player.moveDir * player.accelSpeed * Time.deltaTime;
            vel = player.AdjustVelocityToSlope(vel);
            rb.AddForce(vel, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(player.moveDir * player.accelSpeed * Time.deltaTime * player.airControl, ForceMode.VelocityChange);
        }
    }*/
}
