using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the prefab of its respective collectable

public class ticketScript : MonoBehaviour
{
    [Header("Audio")]
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
            audioManager.PlaySFX(audioManager.ticketGet);

            ScoreManager.instance.AddTicket();

            Destroy(gameObject);
        }
    }
}
