using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the prefab of its respective collectable

public class clipboardScript : MonoBehaviour
{
    //Unique Clipboard name
    public string clipboardName;

    AudioManager audioManager;
    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //AUDIO QUEUE
            audioManager.PlaySFX(audioManager.clipboardGet);
            audioManager.PlaySFX(audioManager.ticketGet); 
            
            //increase score
            ScoreManager.instance.AddClipboard();
            
            //Clipboard has been collected (use this for telemetry)
            Debug.Log(clipboardName + " Clipboard was collected");

            //destroy itself
            Destroy(gameObject);

        }
    }
}
