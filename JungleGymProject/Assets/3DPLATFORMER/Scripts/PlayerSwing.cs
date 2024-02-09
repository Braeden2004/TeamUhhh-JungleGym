using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    [Header ("Checks")]
    public Transform ropeStartPoint;
    SpringJoint joint;
    public bool isSwinging = false;
    public bool canSwing;
    public Rope connectedRope;
    public Rope connectableRope;

    [Header("Spring Joint Parameters")]
    public float springRate = 4.5f;
    public float damperRate = 7f;
    public float massScale = 4.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isSwinging && canSwing)
            {
                StartSwinging();
            }
            else
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

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;
    }

    void StartSwinging()
    {
        isSwinging = true;
        ConfigureSpringJoint();
        connectedRope = connectableRope;
    }

    void ReleaseSwing()
    {
        isSwinging = false;
        Destroy(joint);
        connectedRope = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rope")
        {
            ropeStartPoint = other.gameObject.transform.parent;
            canSwing = true;
            connectableRope = other.GetComponent<Rope>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rope")
        {
            ropeStartPoint = null;
            canSwing = false;
            connectableRope = null;
        }
    }
}

