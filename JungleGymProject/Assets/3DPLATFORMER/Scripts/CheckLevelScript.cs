using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class CheckLevelScript : MonoBehaviour
{
    [Header("Audio")]
    AudioManager audioManager;

    [Header("LevelTracking")]
    public bool hubEntered;
    public bool savanahEntered;
    public bool rainForestEntered;
    public bool gauntletEntered;

    [Header("TitleCardTimer")]
    public bool timerActive = false;

    public float timerCurrent;
    public float timerMax;
    public bool timesUp;

    [Header("LevelCards")]
    private GameObject levelCardHub;
    private GameObject levelCardSavanah;
    private GameObject levelCardRainForest;
    private GameObject levelCardGauntlet;


    private void Start()
    {
        //find the level cards
        levelCardHub = GameObject.Find("HubCard");
        levelCardSavanah = GameObject.Find("SavanahCard");
        levelCardRainForest = GameObject.Find("RainForestCard");
        levelCardGauntlet = GameObject.Find("GauntletCard");


        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        hubEntered = false;

        Debug.Log(other.tag);

        if (other.CompareTag ("Level1"))
        {
            audioManager.NewMusicTrack(audioManager.level1Music);
            Debug.Log("Now entering Level1");

            //entering level 1
            savanahEntered = true;

            //sets it false for a frame before true
            timerActive = true;
        }

        if (other.CompareTag ("Level2"))
        {
            audioManager.NewMusicTrack(audioManager.level2Music);
            Debug.Log("Now entering Level2");

            rainForestEntered = true;
            timerActive = true;
        }

        if (other.CompareTag("Level3"))
        {
            Debug.Log("Now entering Level3");

            //juan music

            gauntletEntered = true;
            timerActive = true;
        }

        if (other.CompareTag ("Challenge"))
        {
            Debug.Log("Now entering Challenge");
        }
    }

    public void Update()
    {
        if (timerActive == true)
        {
            CardTimer();

            //set cards actvive
            if (hubEntered == true)
            {
                levelCardHub.SetActive(true);
            }
            if (savanahEntered == true)
            {
                levelCardSavanah.SetActive(true);
            }
            if (rainForestEntered == true)
            {
                levelCardRainForest.SetActive(true);
            }

            if (gauntletEntered == true)
            {
                levelCardGauntlet.SetActive(true);
            }

        }

        if (timerActive == false)
        {
            //set all inactive
            levelCardHub.SetActive(false);
            levelCardSavanah.SetActive(false);
            levelCardRainForest.SetActive(false);
            levelCardGauntlet.SetActive(false);
        }

    }

    public void OnTriggerStay(Collider other)
    {
        
    }
    

    public void OnTriggerExit(Collider other)
    {
        hubEntered = true;

        //set all false
        levelCardHub.SetActive(false);
        levelCardSavanah.SetActive(false);
        levelCardRainForest.SetActive(false);
        levelCardGauntlet.SetActive(false);

        //activate timer
        timerActive = true;

        Debug.Log(other.tag);
        if (other.CompareTag("Level1"))
        {
            Debug.Log("Now exiting Level1");
            audioManager.NewMusicTrack(audioManager.hubMusic);

            savanahEntered = false;
        }

        if (other.CompareTag("Level2"))
        {
            Debug.Log("Now exiting Level2");
            audioManager.NewMusicTrack(audioManager.hubMusic);

            rainForestEntered = false;
        }

        if (other.CompareTag("Level3"))
        {
            Debug.Log("Now exiting Level3");
            audioManager.NewMusicTrack(audioManager.hubMusic);

            gauntletEntered = false;
        }

        if (other.CompareTag("Challenge"))
        {
            Debug.Log("Now exiting Challenge");
            audioManager.NewMusicTrack(audioManager.hubMusic);

            gauntletEntered = false;
        }
    }


    public void CardTimer()
    {
        if (timerCurrent < timerMax)
        {
            timerCurrent += Time.deltaTime;
        }
        else
        {
            timesUp = true;
            timerActive = false;
            Debug.Log("Times Up");

            //reset timer
            timerCurrent = 0;

        }
    }
}
