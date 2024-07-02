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
    [SerializeField] float offset;
    [SerializeField] float slideDownSpeed;
    float jumpBoost;
    float originalAccel;
    float originalMaxSpeed;
    float originalFriction;

    [SerializeField] private Transform monkeyTransform;
    Vector3 localPos;
    [SerializeField] Animator anim;

    Vector3 dir;
    float dot;
    Vector3 newPos;

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
        localPos = monkeyTransform.localPosition;

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

        UpdateAnim();
    }

    private void FixedUpdate()
    {
        if (isSwinging)
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
            dir = (transform.position - other.transform.position).normalized;

            dir = Vector3.ProjectOnPlane(dir, other.transform.right);
            dot = Vector3.Dot(dir, other.transform.forward);

            float sign = Mathf.Sign(dot);

            newPos = sign * other.transform.forward + other.transform.position + dir;
            newPos.y = transform.position.y;

            Vector3 temp = monkeyTransform.localPosition;
            temp.z = sign * offset;
            monkeyTransform.localPosition = temp;
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
            /*Snap to bottom of rope if underneath
            if (transform.position.y < bottomOfRope.position.y)
            {
                rb.position = newPos;
                transform.position = newPos;
            }*/

            rb.position = newPos;
            transform.position = newPos;

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
                monkeyTransform.localPosition = localPos;
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
                monkeyTransform.localPosition = localPos;
                atBottom = false;
            }
        }
    }

    bool atBottom;
    void MoveOnRope()
    {
        if (transform.position.y < bottomOfRope.position.y && !atBottom)
        {
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = bottomOfRope.localPosition;
            atBottom = true;
        }

        else if (transform.position.y > bottomOfRope.position.y && !atBottom)
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

    void UpdateAnim()
    {
        if (isSwinging)
        {
            anim.SetBool("IsSwinging", isSwinging);
        }
        else
        {
            anim.SetBool("IsSwinging", false);
        }
    }
}

