using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Audio")]
    AudioManager audioManager;

    //make a public float for the bounce force
    public float bounceForce;
    public Rigidbody rb;

    //add impulse force to the player 

    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.tag == "Player")
            {
                audioManager.PlaySFX(3, audioManager.bouncePad);
                rb = other.gameObject.GetComponent<Rigidbody>();

                //check if rb y velocity is greater than 0
                if (rb.velocity.y != 0)
                {
                    //setting it to 0 prevents player from losing velocity when landing on bounce pad from above
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                

                if (rb.velocity.y == 0)
                {
                    rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                }
                
            }

            //make the hidden monkey fellas bounce
            if (other.gameObject.tag == "HiddenMonkey")
            {
                rb = other.gameObject.GetComponent<Rigidbody>();

                //check if rb y velocity is greater than 0
                if (rb.velocity.y != 0)
                {
                    //setting it to 0 prevents player from losing velocity when landing on bounce pad from above
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }


                if (rb.velocity.y == 0)
                {
                    rb.AddForce(Vector3.up * bounceForce/2, ForceMode.Impulse);
                }
        }
        
    }
    

}
