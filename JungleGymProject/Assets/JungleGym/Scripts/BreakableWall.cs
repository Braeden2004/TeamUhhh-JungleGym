using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Roll rollscript;
    private Rigidbody rb;
    public float minVelocity;

    private int freezeTimeTotal;
    private int currentfreezeTime;
    private bool freeze;

    public ParticleSystem destroyParticle;


    [Header("Audio")]
    AudioManager audioManager;

    //getrollscript
    private void Start()
    {
        rollscript = GameObject.Find("PlayerPlaceHolder").GetComponent<Roll>();
        
        //get player rigidbody
        rb = GameObject.Find("PlayerPlaceHolder").GetComponent<Rigidbody>();

        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
                    //spawn particle effect
                    Instantiate(destroyParticle, transform.position, Quaternion.identity);

                    audioManager.defaultPitchSFX(3);
                    audioManager.AdjustVolume(3, 10f);
                    audioManager.PlaySFX(3, audioManager.wallBreak);

                    //destroy the wall
                    Destroy(gameObject);
                }
            }
        }
    }
}
