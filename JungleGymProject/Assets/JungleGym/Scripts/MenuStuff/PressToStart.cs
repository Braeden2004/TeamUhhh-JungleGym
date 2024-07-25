using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PressToStart : MonoBehaviour
{
    

    public GameObject MainMenu;
    public static bool gameStarted = false;


    [Header("Audio")]
    AudioManager audioManager;

    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if any button is pressed
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //if any button is pressed, set the main menu to active
            MainMenu.SetActive(true);

            //set this object to inactive
            gameObject.SetActive(false);


            gameStarted = true;


            //playSound
            //AUDIO QUEUE
            audioManager.defaultPitchSFX(1);
            audioManager.PitchAdjustSFX(1, 4f, 4f);
            audioManager.PlaySFX(1, audioManager.clipboardGet);
            audioManager.defaultPitchSFX(2);
        }
    }
}
