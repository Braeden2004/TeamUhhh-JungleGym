using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer;
    [SerializeField] public TMP_Text timerText;
    [SerializeField] TMP_Text clickToBeginText;
    bool gameStart;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        gameStart = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !gameStart)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameStart = true;
            Time.timeScale = 1;
        }

        if(gameStart)
        {
            clickToBeginText.enabled = false;
            RunTimer();
        }
    }

     void RunTimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
}
