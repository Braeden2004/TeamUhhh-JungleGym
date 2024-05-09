using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//This script is attached to the prefab of its respective collectable

public class ticketScript : MonoBehaviour
{
    public ParticleSystem collectParticle;

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
        if (other.CompareTag("Player"))
        {
            //AUDIO QUEUE
            audioManager.defaultPitchSFX(1);
            audioManager.PlaySFX(3, audioManager.Pause);
            audioManager.PlaySFX(1, audioManager.ticketGet);

            //spawn particle effect
            Instantiate(collectParticle, transform.position, Quaternion.identity);

            ScoreManager.instance.AddTicket();

            Destroy(gameObject);
        }
    }
}
