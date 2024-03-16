using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public Timer timerScript;
    public static string yourTime;

    // Start is called before the first frame update
    void Start()
	{
		//find the timer script
		timerScript = GameObject.Find("PauseMenuCanvas").GetComponent<Timer>();
	}


    // Update is called once per frame
    void Update()
    {
        //if all 5 clips are collected or if I press the 5 key
        if ((ScoreManager.instance.clipboardTotal == 5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            //grab the timer value befor switching the scene
            yourTime = timerScript.timerText.text;
            Debug.Log(yourTime);

            //go to next scene in queue (build settings)
            SceneManager.LoadScene("DemoComplete");
        }
    }
}
