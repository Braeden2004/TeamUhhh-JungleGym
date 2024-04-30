using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCard : MonoBehaviour
{
    public GameObject levelCardHub;
    public GameObject levelCardSavanah;
    public GameObject levelCardRainForest;
    public GameObject levelCardGauntlet;

    //timer
    public bool timerActive = false;

    public float timerCurrent;
    public float timerMax;
    public bool timesUp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            timerActive = true;
        }

        if (timerActive == true)
        {
            timer();
        }   
            
    }

    public void timer()
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
