using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingCollisionCheck : MonoBehaviour
{
    private Roll rollscript;
    private Rigidbody rb;
    public float minVelocity;

    public bool vendingMachineHit = false;

    [Header("Audio")]
    AudioManager audioManager;

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
                    //audio
                    audioManager.defaultPitchSFX(3);
                    audioManager.AdjustVolume(3, 10f);
                    audioManager.PlaySFX(3, audioManager.wallBreak);

                    //make thing happen
                    vendingMachineHit = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        vendingMachineHit = false;
    }
}
