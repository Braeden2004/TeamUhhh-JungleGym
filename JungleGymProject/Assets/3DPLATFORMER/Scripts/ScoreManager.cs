using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int coinTotal = 0;

    private void Awake()
    {
        instance = this;
    }
    public void AddCoin()
    {
        coinTotal += 1;
    }

    public void Update()
    {
        if (coinTotal == 5) 
        {
            TelemetryLogger.Log(this, "Ticket Time", Time.deltaTime);
        }
    }
}
