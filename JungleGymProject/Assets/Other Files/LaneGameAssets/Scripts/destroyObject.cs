using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Update()
    {
        //if player has all clipboards
        if (ScoreManager.instance.clipboardTotal == ScoreManager.instance.totalClipboardInScene)
        {
            //destroy the object
            Destroy(gameObject);
        }
    }
}
