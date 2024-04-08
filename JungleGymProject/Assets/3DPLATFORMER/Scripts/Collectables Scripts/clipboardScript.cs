using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the prefab of its respective collectable

public class clipboardScript : MonoBehaviour
{
    //Unique Clipboard name
    public string clipboardName;

    //clipboard type
    public enum ClipboardType { HubClipboard, SavanahClipboard, TundraClipboard, GauntletClipboard }
    [SerializeField] ClipboardType clipboardType;

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
            audioManager.defaultPitchSFX(1);
            audioManager.PitchAdjustSFX(1, 4f, 4f);
            audioManager.PlaySFX(1, audioManager.clipboardGet);
            audioManager.PlaySFX(3, audioManager.menuHover);
            audioManager.defaultPitchSFX(2);
            audioManager.PlaySFX(2,audioManager.menuPress); 

            //spawn particle effect
            Instantiate(collectParticle, transform.position, Quaternion.identity);

            //increase score
            ScoreManager.instance.AddClipboard(clipboardType);
            
            //Clipboard has been collected (use this for telemetry)
            Debug.Log(clipboardName + " Clipboard was collected");
            TelemetryLogger.Log(this, "Clipboard Collected", clipboardName);

            //destroy itself
            Destroy(gameObject);

        }
    }
}
