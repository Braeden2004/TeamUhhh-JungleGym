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
    public Rope connectedRope;
    public Rope connectableRope;
    PlayerController player;
    Roll roll;
    public Balloon connectedBalloon;

    [Header("Spring Joint Parameters")]
    public float springRate = 4.5f;
    public float damperRate = 7f;
    public float massScale = 4.5f;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        roll = GetComponent<Roll>();
        //originalMaxSpeed = player.maxSpeed;
    }

    void Update()
    {
        if (!isSwinging && canSwing)
        {
            audioManager.PlaySFX(1, audioManager.ropeGrab);
            audioManager.PlaySFX(1, audioManager.ropeSwing);
            StartSwinging();
            if (roll.isRolling)
            {
                roll.isRolling = false;
            }
        }
        else if(isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Roll"))
            {
                ReleaseSwing();
            }
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
        if(connectedRope.balloon != null)
        {
            connectedBalloon = connectableRope.balloon;
            connectedBalloon.attached = true;
        }
    }

    public void ReleaseSwing()
    {
        canSwing = false;
        ropeStartPoint = null;
        isSwinging = false;
        Destroy(joint);
        connectedRope = null;
        connectedBalloon = null;
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

