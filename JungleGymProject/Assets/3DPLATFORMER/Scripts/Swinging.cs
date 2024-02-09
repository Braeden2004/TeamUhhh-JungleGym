using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [SerializeField] HingeJoint hinge;
    Rigidbody rb;

    [SerializeField] GameObject rope;

    [SerializeField] bool canSwing;
    public bool isSwinging;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(canSwing && !isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.E) && hinge == null)
            {
                hinge = rope.GetComponent<HingeJoint>();
                hinge.connectedBody = rb;
                isSwinging = true;
                canSwing = false;
            }
        }
        else if (isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.E) && hinge != null)
            {
                isSwinging = false;
                hinge.connectedBody = null;
                hinge = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rope")
        {
            rope = other.gameObject;
            canSwing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canSwing = false;
        rope = null;
    }
}
