using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    [Header("Clipboards")]
    public clipboardScript clipboardScript;

    public int clipboardTotal = 0; // grandtotal number of collected clipboards
    public int hubClipboardTotal = 0; //total number of collected world clipboards
    public int savanahClipboardTotal = 0; //total number of collected savanah clipboards
    public int tundraClipboardTotal = 0; //total number of collected tundra clipboards
    public int gauntletClipboardTotal = 0; //total number of collected gauntlet clipboards

    [Header("Tickets")]
    public int ticketTotal = 0; //total number of collected tickets

    //This script handels the methods to keep track of collectables like tickets and clipboards

    private void Awake()
    {
        instance = this;
    }

    public void AddTicket()
    {
        ticketTotal += 1;
    }

    public void AddClipboard(clipboardScript.ClipboardType clipboardTypeTotal)
    {
        //Add to grand total
        clipboardTotal += 1;
        

        //Find out what clipboard type it is and increase total
        if (clipboardTypeTotal == clipboardScript.ClipboardType.HubClipboard)
        {
            //Add to world clipboard total
            hubClipboardTotal += 1;
        }
        else if (clipboardTypeTotal == clipboardScript.ClipboardType.SavanahClipboard)
        {
            //Add to savanah clipboard total
            savanahClipboardTotal += 1;
        }
        else if (clipboardTypeTotal == clipboardScript.ClipboardType.TundraClipboard)
        {
            //Add to tundra clipboard total
            tundraClipboardTotal += 1;
        }
        else if (clipboardTypeTotal == clipboardScript.ClipboardType.GauntletClipboard)
        {
            //Add to gauntlet clipboard total
            gauntletClipboardTotal += 1;
        }





        //Telemetry Tracking
        for (int i = 0; i == 5; i++)
        {
            TelemetryLogger.Log(this, "Clipboard Collected Time", Time.deltaTime);
        }

        if (clipboardTotal == 5)
        {
            TelemetryLogger.Log(this, "Clipboard Collected Total Time", Time.deltaTime);
        }
    }
}