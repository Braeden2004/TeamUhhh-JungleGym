using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class SceneSwitcher : MonoBehaviour
{
    public Timer timerScript;
    public static string yourTime;
    public GameObject otherCamera;

    //basic timer
    public float timerStart = 5f;
    public float currentTimer;

    //endgametimer
    public float endTimerMax = 3f;
    public float endTimercurrent;
    public endgameScript endgameScript;

    // Start is called before the first frame update
    void Start()
    {
        //find the timer script
        timerScript = GameObject.Find("HUD").GetComponent<Timer>();

        //set timers
        currentTimer = timerStart;
        endTimercurrent = endTimerMax;
    }


    // Update is called once per frame
    private void Update()
    {
        //if all 5 clips are collected or if I press the 5 key
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //grab the timer value before switching the scene
            Debug.Log("Current: " + ScoreManager.instance.clipboardTotal);
            Debug.Log("total: " + ScoreManager.instance.totalClipboardInScene);

            yourTime = timerScript.timerText.text;
            Debug.Log(yourTime);

            //go to next scene in queue (build settings)
            SceneManager.LoadScene("DemoComplete");
        }

        //end game after going through exit
        if (endgameScript.endGame == true)
        {
            endTimercurrent -= Time.deltaTime;

            if (endTimercurrent <= 0)
            {
                //grab the timer value before switching the scene
                Debug.Log("Current: " + ScoreManager.instance.clipboardTotal);
                Debug.Log("total: " + ScoreManager.instance.totalClipboardInScene);

                yourTime = timerScript.timerText.text;
                Debug.Log(yourTime);

                //go to next scene in queue (build settings)
                SceneManager.LoadScene("DemoComplete");
            }
        }

        //activate camera after all clipboards
        if ((ScoreManager.instance.clipboardTotal == ScoreManager.instance.totalClipboardInScene) && (currentTimer > 0))
        {
            otherCamera.SetActive(true);

            currentTimer -= Time.deltaTime;
        }
        else
        {
            otherCamera.SetActive(false);
        }
    }
}
    
       
   