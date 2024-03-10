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
    }
}
