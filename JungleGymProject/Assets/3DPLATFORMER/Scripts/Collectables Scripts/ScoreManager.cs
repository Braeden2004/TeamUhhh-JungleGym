using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    [Header("Total Clipboards in the world")]
    public int totalClipboardInScene; 
    public int totalhubClipboards;
    public int totalSavanahClipboards;
    public int totalTundraClipboards; 
    public int totalGauntletClipboards; 

    [Header("Clipboards Collected")]
    public clipboardScript clipboardScript;

    public int clipboardTotal = 0; // grandtotal number of collected clipboards
    public int hubClipboardTotal = 0; //total number of collected world clipboards
    public int savanahClipboardTotal = 0; //total number of collected savanah clipboards
    public int tundraClipboardTotal = 0; //total number of collected tundra clipboards
    public int gauntletClipboardTotal = 0; //total number of collected gauntlet clipboards

    [Header("Total Tickets in the world")]
    public int totalTicketsInScene;
    public int totalhubTickets;
    public int totalSavanahTickets;
    public int totalTundraTickets;
    public int totalGauntletTickets;

    [Header("Tickets Collected")]
    public int ticketTotal = 0; //total number of collected tickets

    public int hubTicketTotal = 0; 
    public int savanahTicketTotal = 0;
    public int tundraTicketTotal = 0; 
    public int gauntletTicketTotal = 0;

    public Timer timer;


    //This script handels the methods to keep track of collectables like tickets and clipboards

    private void Start()
    {
        //Find all clipboards in the scene from start of game
        totalhubClipboards = GameObject.FindGameObjectsWithTag("ClipHub").Length;
        totalSavanahClipboards = GameObject.FindGameObjectsWithTag("ClipSavanah").Length;
        totalTundraClipboards = GameObject.FindGameObjectsWithTag("ClipTundra").Length;
        totalGauntletClipboards = GameObject.FindGameObjectsWithTag("ClipGauntlet").Length;

        totalClipboardInScene = totalhubClipboards + totalSavanahClipboards + totalTundraClipboards + totalGauntletClipboards;

        //Find all clipboards in the scene from start of game
        totalhubTickets = GameObject.FindGameObjectsWithTag("TicketHub").Length;
        totalSavanahTickets = GameObject.FindGameObjectsWithTag("TicketSavanah").Length;
        totalTundraTickets = GameObject.FindGameObjectsWithTag("TicketTundra").Length;
        totalGauntletTickets = GameObject.FindGameObjectsWithTag("TicketGauntlet").Length;

        totalTicketsInScene = totalhubTickets + totalSavanahTickets + totalTundraTickets + totalGauntletTickets;
    }

    private void Update()
    {
        //Debug.Log("Total Clipboards in Scene: " + totalClipboardInScene);
}

    private void Awake()
    {
        instance = this;
    }

    public void AddTicket()
    {

        //update ticket total
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
        for (int i = 0; i == totalTicketsInScene; i++)
        {
            TelemetryLogger.Log(this, "Clipboard Collected Time", timer);
        }

        if (clipboardTotal == totalTicketsInScene)
        {
            TelemetryLogger.Log(this, "Clipboard Collected Total Time", timer);
        }
    }
}