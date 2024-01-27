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
    [SerializeField] float rollMaxSpeed;
    float originalMaxSpeed;
    [SerializeField] [Range(0, 0.5f)] float rollFriction;
    //[SerializeField][Range(0, 2f)] float groundFriction; //TO BE TESTED
    float originalFriction;
    [SerializeField][Range(0, 1)] float accelSpeedMultiplier;
    [SerializeField][Range(0, 1)] float jumpHeightMultiplier;

    [Header("Roll speed values")]
    [SerializeField] float rollBoostForce;
    [SerializeField] float rollForce;

    [Header("Slope calculation variables")]
    RaycastHit slopeHit;
    Vector3 slopeNormal;
    float slopeAngle;
    float slopeAccel;
    Vector3 slopeDir;

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        //col = GetComponent<CapsuleCollider>();
        //originalScale = col.height;
        originalScale = transform.localScale.y;
        originalMaxSpeed = player.maxSpeed;
        originalFriction = player.friction;
    }

    void Update()
    {

        if (Input.GetButtonDown("Roll"))
        {
            OnStartRoll();
        }
        if(Input.GetButtonUp("Roll"))
        {
            OnStopRoll();
        }

        if(player.isGrounded())
        {
            slammed = false;
        }
        
        CheckForSlope();

        /*//TO BE TESTED
        if(OnSlope() && isRolling)
        {
            player.friction = rollFriction;
            player.maxSpeed = rollMaxSpeed;
        }
        else if(!OnSlope() && isRolling)
        {
            player.maxSpeed = originalMaxSpeed;
            player.friction = groundFriction;
        }*/
    }

    private bool OnSlope()
    {
        if (!player.isGrounded())
        {
            return false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
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
        player.maxSpeed = rollMaxSpeed;
        player.friction = rollFriction;
        player.accelSpeed *= accelSpeedMultiplier;
        player.jumpVel *= jumpHeightMultiplier;
        //player.canMove = false; //TO BE TESTED
        //col.height = playerScale;
        RollMovement();
    }

    void OnStopRoll()
    {
        isRolling = false;
        player.maxSpeed = originalMaxSpeed;
        player.friction = originalFriction;
        player.accelSpeed /= accelSpeedMultiplier;
        player.jumpVel /= jumpHeightMultiplier;
        //player.canMove = true;
        //col.height = originalScale;
        transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
    }

    bool slammed;
    void RollMovement()
    {
        if(player.moveDir == Vector3.zero && !slammed && !player.isGrounded()) //Slam downward if there's no input
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * rollBoostForce, ForceMode.Impulse);
            slammed = true;
        }

        else //Otherwise, add a small upwards boost
        {
            rb.AddForce(Vector3.up * rollBoostForce / 5f, ForceMode.Impulse);
        }

        rb.velocity = slopeDir * rb.velocity.magnitude; //Set velocity direction to follow slope
    }

    void CheckForSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out slopeHit, 1.5f, groundMask))
        {
            slopeNormal = slopeHit.normal;
            slopeAngle = Vector3.Angle(Vector3.up, slopeNormal);

            slopeAccel = Mathf.Sin(slopeAngle * Mathf.Deg2Rad); //Calculate slope acceleration
            slopeDir = Vector3.Cross(Vector3.Cross(slopeNormal, -Vector3.up), slopeNormal).normalized; //Get direction of slope

            //Vector3 slope = Vector3.ProjectOnPlane(rb.velocity, slopeNormal);
        }

        Debug.DrawRay(slopeHit.point, slopeDir * 100f, Color.red);

        if (OnSlope() && isRolling)
        {
            //rb.velocity = slopeDir * rb.velocity.magnitude; //Maintain velocity on sloped surfaces -> lead to a bug where you'd bounce back and forth when the direction of slope changes
            rb.AddForce(slopeAccel * rollForce * slopeDir); //Add force down the slope
        }

        if(player.moveDir != slopeDir)
        {
            rb.AddForce(rollForce * slopeDir); //Add extra force if a player is trying to roll up a hill
        }
    }

}
