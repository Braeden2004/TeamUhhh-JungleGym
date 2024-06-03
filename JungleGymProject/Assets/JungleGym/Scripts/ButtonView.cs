using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonView : MonoBehaviour
{
    public float teleportDistance;
    public bool isPressed = false;

    [Header("Audio")]
    AudioManager audioManager;

    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isPressed == false)
            {
                //teleport down to show press
                transform.position = new Vector3(transform.position.x, transform.position.y - teleportDistance, transform.position.z);

                //SFX
                audioManager.defaultPitchSFX(3);
                audioManager.AdjustVolume(3, 10f);

                audioManager.PlaySFX(3, audioManager.bigButtonPress);

                isPressed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isPressed == true)
            {
                //teleport down to show press
                transform.position = new Vector3(transform.position.x, transform.position.y + teleportDistance, transform.position.z);

                isPressed = false;
            }
        }
    }


}
