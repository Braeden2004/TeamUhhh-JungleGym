using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDcollectablesScript : MonoBehaviour
{
    //public ticketScript ticketScript;
    //public clipboardScript clipboardScript;

    public TextMeshProUGUI textMeshProTicket;
    public TextMeshProUGUI textMeshProClipboard;

    //This script attaches to the ticket icon in the hud, it speaks to the ticketCollectScript to find out how many tickets are counted on screen

    // Update is called once per frame
    void Update()
    {
        // Update the text to display ticket value
        if (textMeshProTicket != null)
        {
            //draw tickets
            textMeshProTicket.text = "X " + ScoreManager.instance.ticketTotal.ToString();
        }

        // Update the text to display Clipboard value
        if (textMeshProClipboard != null)
        {
            //draw Clipboards
            textMeshProClipboard.text = "X " + ScoreManager.instance.clipboardTotal.ToString();
        }

    }
}
