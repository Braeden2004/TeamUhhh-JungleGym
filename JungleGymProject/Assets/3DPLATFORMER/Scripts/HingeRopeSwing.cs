using UnityEngine;

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
    [SerializeField][Range(0, 1)] float accelMultiplier;
    [SerializeField][Range(0, 2)] float maxSpeedMultiplier;
    [SerializeField][Range(0, 1)] float jumpMultiplier;
    [SerializeField] Vector3 offset;
    [SerializeField] float slideDownSpeed;
    float jumpBoost;
    float originalAccel;
    float originalMaxSpeed;

    [Header("Audio")]
    AudioManager audioManager;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        roll = GetComponent<Roll>();
        rb = GetComponent<Rigidbody>();
        originalAccel = player.accelSpeed;
        originalMaxSpeed = player.maxSpeed;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        if(other.gameObject.CompareTag("Rope"))
        {
            canSwing = true;
            bottomOfRope = other.gameObject.transform.GetChild(0); 
            ropeBody = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rope"))
        {
            canSwing = false;
        }
    }

    void Swing()
    {
        //ropeVelWhenGrabbed = rb.velocity;

        if(!isSwinging && canSwing) //Input.GetKeyDown(KeyCode.E) && 
        {
            //Snap to bottom of rope if underneath
            if (transform.position.y < bottomOfRope.position.y) 
            {
                rb.position = bottomOfRope.position + offset; //Still need to find a way to convert to 'forward'
                transform.position = bottomOfRope.position + offset;
            } 

            isSwinging = true;
            ConfigureJoint();
            jumpBoost = player.jumpVel * jumpMultiplier;
            player.maxSpeed = originalMaxSpeed * maxSpeedMultiplier;
            player.accelSpeed = originalAccel * accelMultiplier;

            if(roll.isRolling)
            {
                roll.isRolling = false;
            }
            audioManager.PlaySFX(1, audioManager.ropeGrab);
            audioManager.PlayRSFX(audioManager.ropeSwing);

        }

        else if(isSwinging)
        {
            MoveOnRope();
            if (Input.GetButtonDown("Roll")) //Input.GetKeyDown(KeyCode.E) ||
            {
                isSwinging = false;
                Destroy(joint);
                ropeBody = null;
                hasSwung = true;
                canSwing = false;
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                isSwinging = false;
                rb.AddForce(new Vector3(0, jumpBoost, 0), ForceMode.Impulse);
                Destroy(joint);
                ropeBody = null;
                hasSwung = true;
                canSwing = false;
                audioManager.StopRSFX();
            }
        }
    }

    void MoveOnRope()
    {
        if (transform.position.y >= bottomOfRope.position.y)
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

