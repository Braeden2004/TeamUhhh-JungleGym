using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{

    public TextMeshProUGUI textField;

    public void OnConnectionSuccess(int sessionNumber)
    {
        textField.text = $"Session #{sessionNumber}";
    }

    public void OnConnectionFail(string errorMessage)
    {
        textField.text = $"Fail: {errorMessage}" ;
    }
}
