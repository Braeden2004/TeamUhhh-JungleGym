using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int coinTotal = 0;

    float timer;
    float totalTimer;

    private void Awake()
    {
        instance = this;
    }
    public void AddCoin()
    {
        coinTotal += 1;
        if (int i = 0; i = 5; int++)
            {
            TelemetryLogger.Log(this, "Clipboard");
            timer = time.deltaTime;
            }
    }

    public void Update()
    {
        if (AddCoin())
        {
            totalTimer = timer - timer;
            TelemetryLogger.Log(this, "Time between clipboard", totalTimer);
        }

        if (coinTotal == 5) 
        {
            TelemetryLogger.Log(this, "Clipboard", Time.deltaTime);
        }
    }
}
