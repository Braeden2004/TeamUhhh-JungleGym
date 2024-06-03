using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    //[Header("Audio")]
    //AudioManager audioManager;
    private void Awake()
    {
        //Sets the audio stuff up
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


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

                //AUDIO QUEUE
                //audioManager.PlaySFX(audioManager.ropeGrab);
                //audioManager.PlaySFX(audioManager.ropeSwing);
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

