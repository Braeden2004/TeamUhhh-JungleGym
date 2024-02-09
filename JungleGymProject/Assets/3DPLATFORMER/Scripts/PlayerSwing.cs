using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    public Transform ropeStartPoint;

    SpringJoint joint;
    public bool isSwinging = false;
    public bool canSwing;

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
    }

    void ReleaseSwing()
    {
        isSwinging = false;
        Destroy(joint);
    }
}

