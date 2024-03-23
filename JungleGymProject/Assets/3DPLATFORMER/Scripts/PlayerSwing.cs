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
    PlayerController player;
    Rigidbody rb;
    //public float swingJumpMaximum; threshold for extra jump WIP

    [Header("Spring Joint Parameters")]
    public float springRate = 4.5f;
    public float damperRate = 7f;
    public float massScale = 4.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
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
                player.maxSpeed *= maxSpeedMultiplier;
            }
            
        }
        else if(isSwinging && Input.GetKeyDown(KeyCode.Space))
        {
            /*if (player.moveDir != Vector3.zero)
            {
                rb.AddForce(player.jumpVel / 2f * transform.up, ForceMode.Impulse); //extra jump for when you're moving slowly WIP
            }*/

            ReleaseSwing();
            audioManager.PlaySFX(audioManager.jump);
            swung = true;
        }

        if(player.isGrounded() && swung)
        {
            swung = false;
            player.maxSpeed /= maxSpeedMultiplier;
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

