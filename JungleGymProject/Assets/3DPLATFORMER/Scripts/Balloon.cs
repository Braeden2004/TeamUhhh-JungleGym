using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody playerBody;
    public Rope attachedRope;
    float timer;
    bool addForceToPlayer;
    [SerializeField] float popForce;
    [SerializeField] float popTimer;
    [SerializeField] float floatSpeed;
    [SerializeField] float maxDist;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attachedRope != null)
        {
            if (attachedRope.player != null)
            {
                playerBody = attachedRope.player.gameObject.GetComponent<Rigidbody>();

                if (attachedRope.player.joint != null)
                {
                    SpringJoint attachedPlayerJoint = attachedRope.player.joint;

                    attachedPlayerJoint.minDistance = maxDist;
                    attachedPlayerJoint.maxDistance = maxDist;

                    //Move this out for auto movement
                    Vector3 balloonForce = (Vector3.up * floatSpeed * Time.deltaTime) + (transform.forward * floatSpeed * Time.deltaTime);
                    rb.AddForce(balloonForce);
                    //attachedRope.player.gameObject.GetComponent<PlayerController>().useGravity = false;
                    //playerBody.AddForce(balloonForce);

                    attachedPlayerJoint.connectedAnchor = transform.position;

                    /*if (attachedRope.player.gameObject.GetComponent<PlayerController>().moveDir == Vector3.zero)
                    {
                        Vector3 velocityDifference = rb.velocity - playerBody.velocity;
                        playerBody.velocity += velocityDifference;
                    }*/

                    addForceToPlayer = true;

                    //Move this out for auto pop
                    timer += Time.deltaTime;
                    if (timer > popTimer)
                    {
                        Pop();
                    }
                }
            }
        }

        
    }

    void Pop()
    {
        if (addForceToPlayer)
        {
            attachedRope.player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * popForce, ForceMode.Impulse);
        }
        attachedRope.player.ReleaseSwing();
        Destroy(this.gameObject);
    }
}
