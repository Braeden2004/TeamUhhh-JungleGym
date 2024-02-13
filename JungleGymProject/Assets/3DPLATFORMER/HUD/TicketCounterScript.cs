using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicketCounterScript : MonoBehaviour
{
    public coinCollect coinScript;
    public TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text to display the value of points
        if (textMeshPro != null)
        {
            //draw points
            textMeshPro.text = "X " + ScoreManager.instance.coinTotal.ToString();
        }
    }
}
