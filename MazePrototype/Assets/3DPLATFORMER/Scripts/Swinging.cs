using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    HingeJoint hinge;


    bool canSwing;
    bool isSwinging;
    PlayerController player;
    Rigidbody rb;
    [SerializeField] Rigidbody ropeBottom;
    [SerializeField] float swingForce;

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rope")
        {
            if (Input.GetKeyDown(KeyCode.E) && hinge == null)
            {
                hinge = transform.AddComponent<HingeJoint>();
                hinge.connectedBody = other.transform.parent.GetComponent<Rigidbody>();
            }
            else if(Input.GetKeyDown(KeyCode.E) && hinge != null)
            {
                Destroy(hinge);
            }
        }
    }
}
