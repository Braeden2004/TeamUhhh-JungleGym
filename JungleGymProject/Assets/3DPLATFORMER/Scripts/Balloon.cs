using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Rigidbody rb;
    public Rope attachedRope;
    float timer;
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
    void Update()
    {
        if (attachedRope != null)
        {
            if (attachedRope.player != null)
            {

                attachedRope.player.joint.minDistance = maxDist;
                attachedRope.player.joint.maxDistance = maxDist;
                rb.AddForce(Vector3.up * floatSpeed * Time.deltaTime);
                rb.AddForce(transform.forward * floatSpeed * Time.deltaTime);
                attachedRope.player.joint.connectedAnchor = transform.position;
            }
        }

        timer += Time.deltaTime;
        if(timer > popTimer) 
        {
            Pop();
        }
    }

    void Pop()
    {
        //attachedRope.player.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        attachedRope.player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * popForce, ForceMode.Impulse);
        attachedRope.player.ReleaseSwing();
        Destroy(this.gameObject);
    }
}
