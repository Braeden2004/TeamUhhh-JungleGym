using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClipboardTypeScript : MonoBehaviour
{
    enum ClipboardType { WorldClipboard, SavanahClipboard, TundraClipboard, GauntletClipboard}
    [SerializeField] ClipboardType clipboardType;

    //color of the clipboard
    [SerializeField] Color savanahColor;
    [SerializeField] Color tundraColor;
    [SerializeField] Color gauntletColor;
    [SerializeField] Color worldColor;

    public Material clipMaterial;

    //change color based on clipboard type
    void Start()
    {
        //Check if world clipboard 
        if (clipboardType == ClipboardType.WorldClipboard)
        {
            //change color
            clipMaterial.color = worldColor;
        }

        //Check if Savanah clipboard 
        if (clipboardType == ClipboardType.SavanahClipboard)
        {
            //change color
            clipMaterial.color = savanahColor;
        }

        //Check if Tundra clipboard 
        if (clipboardType == ClipboardType.TundraClipboard)
        {
            //change color
            clipMaterial.color = tundraColor;
        }

        //Check if Gauntlet clipboard 
        if (clipboardType == ClipboardType.GauntletClipboard)
        {
            //change color
            clipMaterial.color = gauntletColor;
        }
    }

}
