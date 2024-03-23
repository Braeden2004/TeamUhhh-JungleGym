using UnityEngine;

public class HingeRopeSwing : MonoBehaviour
{

    Transform bottomOfRope;
    public bool isSwinging;
    bool canSwing;
    Rigidbody ropeBody;
    ConfigurableJoint joint;
    PlayerController player;
    public Vector3 ropeVelWhenGrabbed;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Swing();
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
            player.maxSpeed *= 2;
        }

        else if(Input.GetKeyDown(KeyCode.E) && isSwinging)
        {
            isSwinging = false;
            Destroy(joint);
            ropeBody = null;
            player.maxSpeed /= 2;
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

