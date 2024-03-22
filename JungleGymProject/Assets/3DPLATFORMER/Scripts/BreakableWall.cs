using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public Roll rollscript;
    public Rigidbody rb;
    public float minVelocity;

    public int freezeTimeTotal;
    private int currentfreezeTime;
    public bool freeze;


    //getrollscript
    private void Start()
    {
        rollscript = GameObject.Find("PlayerPlaceHolder").GetComponent<Roll>();
    }

    private void Update()
    {
        if (freeze ==true)
        {
            //freeze the whole game for 2 frames
            Time.timeScale = 0f;

            //set the freeze time
            currentfreezeTime = freezeTimeTotal;
            currentfreezeTime--;

            if (freezeTimeTotal <= 0)
            {
                Time.timeScale = 1f;

                //destroy the wall
                Destroy(gameObject);

                freeze = false;
            }
        }
        
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
                    freeze = true;    
                }
            }
        }
    }
}
