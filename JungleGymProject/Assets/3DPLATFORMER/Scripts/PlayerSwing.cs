using UnityEngine;

public class PlayerSwing : MonoBehaviour
{

    [Header("Audio")]
    AudioManager audioManager;
    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [Header ("Checks")]
    public Transform ropeStartPoint;
    public SpringJoint joint;
    public bool isSwinging = false;
    public bool canSwing;
    bool swung;
    public Rope connectedRope;
    public Rope connectableRope;
    [SerializeField][Range(1, 2)] float maxSpeedMultiplier;
    float originalMaxSpeed;
    PlayerController player;
    Rigidbody rb;
    Roll roll;
    //public float swingJumpMaximum; threshold for extra jump WIP

    [Header("Spring Joint Parameters")]
    public float springRate = 4.5f;
    public float damperRate = 7f;
    public float massScale = 4.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
        roll = GetComponent<Roll>();
        originalMaxSpeed = player.maxSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isSwinging && canSwing)
            {
                audioManager.PlaySFX(audioManager.ropeGrab);
                audioManager.PlaySFX(audioManager.ropeSwing);
                StartSwinging();
                player.maxSpeed = originalMaxSpeed * maxSpeedMultiplier;
                if (roll.isRolling)
                {
                    roll.isRolling = false;
                }
            }
            
        }
        else if(isSwinging)
        {
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Roll"))
            ReleaseSwing();
            audioManager.PlaySFX(audioManager.jump);
            swung = true;
        }

        if(player.isGrounded() && swung)
        {
            swung = false;
            player.maxSpeed = originalMaxSpeed;
        }
    }

    void ConfigureSpringJoint()
    {
        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = ropeStartPoint.position;


        float distanceFromPoint = Vector3.Distance(transform.position, ropeStartPoint.position);
        
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = springRate;
        joint.damper = damperRate;
        joint.massScale = massScale;
    }

    void StartSwinging()
    {
        isSwinging = true;
        ConfigureSpringJoint();
        connectedRope = connectableRope;
    }

    public void ReleaseSwing()
    {
        isSwinging = false;
        Destroy(joint);
        connectedRope = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Balloon")
        {
            ropeStartPoint = other.gameObject.transform.parent;
            canSwing = true;
            connectableRope = other.GetComponent<Rope>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Balloon")
        {
            ropeStartPoint = null;
            canSwing = false;
            connectableRope = null;
        }
    }
}

