using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the prefab of its respective collectable

public class clipboardScript : MonoBehaviour
{
    //Unique Clipboard name
    public string clipboardName;
    public ParticleSystem collectParticle;

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

            //spawn particle effect
            Instantiate(collectParticle, transform.position, Quaternion.identity);

            //increase score
            ScoreManager.instance.AddClipboard();
            
            //Clipboard has been collected (use this for telemetry)
            Debug.Log(clipboardName + " Clipboard was collected");
            TelemetryLogger.Log(this, "Clipboard Collected", clipboardName);

            //destroy itself
            Destroy(gameObject);

        }
    }
}
