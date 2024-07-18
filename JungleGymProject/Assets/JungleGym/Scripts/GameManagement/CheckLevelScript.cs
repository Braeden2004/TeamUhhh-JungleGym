using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
    public GameObject levelCardHub;
    public GameObject levelCardSavanah;
    public GameObject levelCardRainForest;
    public GameObject levelCardGauntlet;

    public PlayerController playerscript;


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

        if (other.CompareTag("Level1"))
        {

            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.level1Music);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();

            Debug.Log("Now entering Level1");

            //entering level 1
            savanahEntered = true;

            //sets it false for a frame before true
            timerActive = true;
        }

        if (other.CompareTag("Level2"))
        {
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.level2Music);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();

            Debug.Log("Now entering Level2");

            rainForestEntered = true;
            timerActive = true;
        }

        if (other.CompareTag("Level3"))
        {
            Debug.Log("Now entering Level3");

            //juan music
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.gauntlet);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();

            gauntletEntered = true;
            timerActive = true;
        }

        if (other.CompareTag("Hub"))
        {
            Debug.Log("Now entering Hub");

            //juan music
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.hubMusic);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();

        }

        if (other.CompareTag("Challenge"))
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
                Debug.Log("Hub Entered");
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


    public void OnTriggerExit(Collider other)
    {

        Debug.Log(other.tag);
        if (other.CompareTag("Level1"))
        {
            Debug.Log("Now exiting Level1");
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.hubMusic);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();


            savanahEntered = false;
        }

        if (other.CompareTag("Level2"))
        {
            Debug.Log("Now exiting Level2");
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.hubMusic);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();

            rainForestEntered = false;
        }

        if (other.CompareTag("Level3"))
        {
            
            Debug.Log("Now exiting Level3");
            /*Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.hubMusic);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();*/

            gauntletEntered = false;
        }

        if (other.CompareTag("Challenge"))
        {
            Debug.Log("Now exiting Challenge");
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.hubMusic);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();

            gauntletEntered = false;
        }

        if (other.CompareTag("Hub"))
        {
           /*Debug.Log("Now exiting Spawn");
            Tween fade1 = audioManager.Music_Source.DOFade(0, 1);
            Tween fade2 = audioManager.Music_Source.DOFade(1, 1);
            fade1.onComplete += () => audioManager.NewMusicTrack(audioManager.hubMusic);

            Sequence swapAudio = DOTween.Sequence();
            swapAudio.Append(fade1);
            swapAudio.Append(fade2);
            swapAudio.Play();*/

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
