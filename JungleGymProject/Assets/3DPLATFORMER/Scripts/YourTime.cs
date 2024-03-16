using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YourTime : MonoBehaviour
{
    public TextMeshProUGUI textMeshProYourTime;

    // Update is called once per frame
    void Update()
    {
        // Update the text to display ticket value
        if (textMeshProYourTime != null)
        {
            //Say the players time
            textMeshProYourTime.text = SceneSwitcher.yourTime.ToString();
        }
    }
}
