using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgresscollectablesScript : MonoBehaviour
{
    [Header("Total")]
    public TextMeshProUGUI textMeshProClipboard0;
    public TextMeshProUGUI textMeshProTicket0;


    [Header("Level 1")]
    public TextMeshProUGUI textMeshProClipboard1;
    public TextMeshProUGUI textMeshProTicket1;


    [Header("Level 2")]
    public TextMeshProUGUI textMeshProClipboard2;
    public TextMeshProUGUI textMeshProTicket2;


    [Header("Level 3")]
    public TextMeshProUGUI textMeshProClipboard3;
    public TextMeshProUGUI textMeshProTicket3;


    [Header("Level 4")]
    public TextMeshProUGUI textMeshProClipboard4;
    public TextMeshProUGUI textMeshProTicket4;


    //This script attaches to the ticket icon in the hud, it speaks to the ticketCollectScript to find out how many tickets are counted on screen

    // Update is called once per frame
    void Update()
    {
        //draw tickets
        textMeshProTicket0.text = ((ScoreManager.instance.ticketTotal.ToString()) + "/" + (ScoreManager.instance.totalTicketsInScene.ToString()));
        textMeshProTicket1.text = ScoreManager.instance.hubTicketTotal.ToString() + "/" + ScoreManager.instance.totalhubTickets.ToString();
        textMeshProTicket2.text = ScoreManager.instance.savanahTicketTotal.ToString() + "/" + ScoreManager.instance.totalSavanahTickets.ToString();
        textMeshProTicket3.text = ScoreManager.instance.tundraTicketTotal.ToString() + "/" + ScoreManager.instance.totalTundraTickets.ToString();
        textMeshProTicket4.text = ScoreManager.instance.gauntletTicketTotal.ToString() + "/" + ScoreManager.instance.totalGauntletTickets.ToString();


        //draw Clipboards
        textMeshProClipboard0.text = ((ScoreManager.instance.clipboardTotal.ToString()) + "/" + (ScoreManager.instance.totalClipboardInScene.ToString()));
        textMeshProClipboard1.text = ScoreManager.instance.hubClipboardTotal.ToString() + "/" + ScoreManager.instance.totalhubClipboards.ToString();
        textMeshProClipboard2.text = ScoreManager.instance.savanahClipboardTotal.ToString() + "/" + ScoreManager.instance.totalSavanahClipboards.ToString();
        textMeshProClipboard3.text = ScoreManager.instance.tundraClipboardTotal.ToString() + "/" + ScoreManager.instance.totalTundraClipboards.ToString();
        textMeshProClipboard4.text = ScoreManager.instance.gauntletClipboardTotal.ToString() + "/" + ScoreManager.instance.totalGauntletClipboards.ToString();


    }
}
