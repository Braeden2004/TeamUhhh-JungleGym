using UnityEngine;

public class HingeRopeSwing : MonoBehaviour
{

    Transform bottomOfRope;
    public bool isSwinging;
    public bool canSwing;
    public bool hasSwung;
    Rigidbody ropeBody;
    Rigidbody rb;
    ConfigurableJoint joint;
    PlayerController player;
    Roll roll;
    public Vector3 ropeVelWhenGrabbed;
    [SerializeField][Range(0, 1)] float accelMultiplier;
    [SerializeField][Range(0, 2)] float maxSpeedMultiplier;
    [SerializeField][Range(0, 1)] float jumpMultiplier;
    [SerializeField] Vector3 offset;
    float jumpBoost;
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
            bottomOfRope = other.gameObject.transform.GetChild(0); 
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
            //Snap to bottom of rope, slightly off sometimes due to update/fixedupdate (?)
            ropeBody.velocity = Vector3.zero;
            ropeBody.velocity = rb.velocity;
            rb.position = bottomOfRope.position + offset; //Still need to find a way to convert to 'forward'
            transform.position = bottomOfRope.position + offset;

            isSwinging = true;
            ConfigureJoint();
            jumpBoost = player.jumpVel * jumpMultiplier;
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
                rb.AddForce(new Vector3(0, jumpBoost, 0), ForceMode.Impulse);
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
    }
}

