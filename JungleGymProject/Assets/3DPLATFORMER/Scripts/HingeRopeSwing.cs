using UnityEngine;

public class HingeRopeSwing : MonoBehaviour
{

    Transform bottomOfRope;
    public bool isSwinging;
    bool canSwing;
    public bool hasSwung;
    Rigidbody ropeBody;
    Rigidbody rb;
    ConfigurableJoint joint;
    PlayerController player;
    Roll roll;
    public Vector3 ropeVelWhenGrabbed;
    [SerializeField][Range(0, 1)] float accelMultiplier;
    [SerializeField][Range(0, 2)] float maxSpeedMultiplier;
    float originalAccel;
    float originalMaxSpeed;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        roll = GetComponent<Roll>();
        rb = GetComponent<Rigidbody>();
        originalAccel = player.accelSpeed;
        originalMaxSpeed = player.maxSpeed;
    }

    private void Update()
    {
        Swing();

        if(player.isGrounded() && hasSwung && !roll.isRolling)
        {
            hasSwung = false;
            player.accelSpeed = originalAccel;
            player.maxSpeed = originalMaxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rope")
        {
            canSwing = true;
            bottomOfRope = other.gameObject.transform;
            ropeBody = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rope")
        {
            canSwing = false;
        }
    }

    void Swing()
    {
        //ropeVelWhenGrabbed = rb.velocity;

        if(Input.GetKeyDown(KeyCode.E) && !isSwinging && canSwing)
        {
            isSwinging = true;
            ConfigureJoint();
            player.maxSpeed = originalMaxSpeed * maxSpeedMultiplier;
            player.accelSpeed = originalAccel * accelMultiplier;
            if(roll.isRolling)
            {
                roll.isRolling = false;
            }
        }

        else if(isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Roll"))
            {
                isSwinging = false;
                Destroy(joint);
                ropeBody = null;
                hasSwung = true;
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                isSwinging = false;
                rb.AddForce(new Vector3(0, player.jumpVel, 0), ForceMode.Impulse);
                Destroy(joint);
                ropeBody = null;
                hasSwung = true;
            }
        }
    }

    void ConfigureJoint()
    {
        joint = gameObject.AddComponent<ConfigurableJoint>();
        joint.connectedBody = ropeBody;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        transform.position = bottomOfRope.position;
    }
}

