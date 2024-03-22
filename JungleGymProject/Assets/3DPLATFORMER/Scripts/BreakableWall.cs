using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public Roll rollscript;
    public Rigidbody rb;
    public float minVelocity;


    //getrollscript
    private void Start()
    {
        rollscript = GameObject.Find("PlayerPlaceHolder").GetComponent<Roll>();
    }

    //check for a trigger enter
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");


        if (other.gameObject.tag == "Player")
        {
            //is the player rolling
            if (rollscript.isRolling == true)
            {
                if (rb.velocity.magnitude > minVelocity)
                {
                    //destroy the wall
                    Destroy(gameObject);
                }
            }
        }
    }
}
