using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckLevelScript : MonoBehaviour
{
    [Header("Audio")]
    AudioManager audioManager;

    private void Start()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag == "Level1")
        {
            audioManager.NewMusicTrack(audioManager.level1Music);
            Debug.Log("Now entering Level1");
        }

        if (other.tag == "Level2")
        {
            audioManager.NewMusicTrack(audioManager.level2Music);
            Debug.Log("Now entering Level2");
        }

        if (other.tag == "Challenge")
        {
            Debug.Log("Now entering Challenge");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //play music here???
    }
    

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Level1")
        {
            Debug.Log("Now exiting Level1");
            audioManager.NewMusicTrack(audioManager.hubMusic);
        }

        if (other.tag == "Level2")
        {
            Debug.Log("Now exiting Level2");
            audioManager.NewMusicTrack(audioManager.hubMusic);
        }

        if (other.tag == "Challenge")
        {
            Debug.Log("Now exiting Challenge");
            audioManager.NewMusicTrack(audioManager.hubMusic);
        }
    }
}
