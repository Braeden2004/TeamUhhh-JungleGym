using UnityEngine;
using System.Collections;

public class HingeRopeSwing : MonoBehaviour
{

    Transform bottomOfRope;
    public bool isSwinging;
    public bool canSwing;
    public bool hasSwung;
    Rigidbody ropeBody;
    Rigidbody rb;
    public ConfigurableJoint joint;
    PlayerController player;
    Roll roll;
    public Vector3 ropeVelWhenGrabbed;
    [SerializeField][Range(0, 2)] float accelMultiplier;
    [SerializeField][Range(0, 2)] float maxSpeedMultiplier;
    [SerializeField][Range(0, 2)] float jumpMultiplier;
    [SerializeField] Vector3 offset;
    [SerializeField] float slideDownSpeed;
    float jumpBoost;
    float originalAccel;
    float originalMaxSpeed;
    float originalFriction;

    [Header("Audio")]
    AudioManager audioManager;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        roll = GetComponent<Roll>();
        rb = GetComponent<Rigidbody>();
        originalAccel = player.accelSpeed;
        originalMaxSpeed = player.maxSpeed;
        originalFriction = player.frictionRate;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        Swing();

        if (player.isGrounded() && hasSwung && !roll.isRolling)
        {
            hasSwung = false;
            player.accelSpeed = originalAccel;
            player.maxSpeed = originalMaxSpeed;
        }
    }

    private void FixedUpdate()
    {
        if(isSwinging)
        {
            MoveOnRope();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            canSwing = true;
            bottomOfRope = other.gameObject.transform.GetChild(1);
            ropeBody = other.GetComponent<Rigidbody>();
            other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rope"))
        {
            canSwing = false;
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    void Swing()
    {
        //ropeVelWhenGrabbed = rb.velocity;

        if (!isSwinging && canSwing) //Input.GetKeyDown(KeyCode.E) && 
        {
            //Snap to bottom of rope if underneath
            if (transform.position.y < bottomOfRope.position.y)
            {
                rb.position = bottomOfRope.position + offset;
                transform.position = bottomOfRope.position + offset;
            }

            if (roll.isRolling)
            {
                roll.OnStopRoll();
            }

            isSwinging = true;
            ConfigureJoint();
            jumpBoost = player.jumpVel * jumpMultiplier;
            player.frictionRate = player.airFrictionRate;
            player.maxSpeed = originalMaxSpeed * maxSpeedMultiplier;
            player.accelSpeed = originalAccel * accelMultiplier;

            audioManager.PlaySFX(1, audioManager.ropeGrab);
            audioManager.PlayRSFX(audioManager.ropeSwing);

        }

        else if (isSwinging)
        {
            //MoveOnRope();
            if (Input.GetButtonDown("Roll")) //Input.GetKeyDown(KeyCode.E) ||
            {
                isSwinging = false;
                Destroy(joint);
                ropeBody = null;
                hasSwung = true;
                canSwing = false;
                atBottom = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                isSwinging = false;
                rb.AddForce(new Vector3(0, jumpBoost, 0), ForceMode.Impulse);
                Destroy(joint);
                ropeBody = null;
                hasSwung = true;
                canSwing = false;
                player.frictionRate = originalFriction;
                player.maxSpeed = originalMaxSpeed;
                player.accelSpeed = originalAccel;
                audioManager.StopRSFX();
                atBottom = false;
            }
        }
    }

    bool atBottom;
    void MoveOnRope()
    {
        if (transform.position.y >= bottomOfRope.position.y && !atBottom)
        {
            if (joint != null)
            {
                float newY = joint.connectedAnchor.y;
                joint.autoConfigureConnectedAnchor = false;

                newY -= Time.deltaTime * slideDownSpeed;
                Vector3 newPos = new Vector3(joint.connectedAnchor.x, newY, joint.connectedAnchor.z);
                joint.connectedAnchor = newPos;
            }
        }
        else
        {
            atBottom = true;
        }
    }

    void ConfigureJoint()
    {
        joint = gameObject.AddComponent<ConfigurableJoint>();
        joint.connectedBody = ropeBody;
        joint.autoConfigureConnectedAnchor = true;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
    }
}

