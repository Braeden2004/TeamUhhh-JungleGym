using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//source https://discussions.unity.com/t/edit-values-for-multiple-objects-concurrently/32263/2
public class monkeychainScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //check if its the player
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("monkeyChain"))
            {
                //get rigidbody
                Rigidbody rb = obj.GetComponent<Rigidbody>();

                //chech if rigidbody is not null
                if (rb != null)
                {
                    //enable physics
                    rb.isKinematic = false;
                }
            }
        }
    }
}
