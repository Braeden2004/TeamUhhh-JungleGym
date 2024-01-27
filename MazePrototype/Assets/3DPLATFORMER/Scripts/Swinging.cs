using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    //HingeJoint hinge;
    FixedJoint hinge;
    //CharacterJoint hinge;

    GameObject rope;

    bool canSwing;
    bool isSwinging;

    private void Update()
    {
        if(canSwing)
        {
            if (Input.GetKeyDown(KeyCode.E) && hinge == null)
            {
                hinge = transform.AddComponent<FixedJoint>();
                hinge.connectedBody = rope.transform.parent.GetComponent<Rigidbody>();
                isSwinging = true;
                canSwing = false;
            }
        }
        else if (isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.E) && hinge != null)
            {
                Destroy(hinge);
                isSwinging = false;
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
