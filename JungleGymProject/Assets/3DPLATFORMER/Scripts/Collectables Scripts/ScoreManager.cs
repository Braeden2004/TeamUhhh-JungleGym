using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int ticketTotal = 0; //total number of collected tickets
    public int clipboardTotal = 0; // total number of collected clipboards

    //This script handels the methods to keep track of collectables like tickets and clipboards

    private void Awake()
    {
        instance = this;
    }

    public void AddTicket()
    {
        ticketTotal += 1;
    }

    public void AddClipboard()
    {
        clipboardTotal += 1;
    
        for (int i = 0; i == 5; i++)
        {
            TelemetryLogger.Log(this, "Clipboard Collected Time", Time.deltaTime);
        }
    }

    public void Update()
    {
        if (clipboardTotal == 5) 
        {
            TelemetryLogger.Log(this, "Clipboard Collected Total Time", Time.deltaTime);
        }

        if (ticketTotal == 5)
        {
            TelemetryLogger.Log(this, "Ticket Collected Total Time", Time.deltaTime);
        }
    }

}