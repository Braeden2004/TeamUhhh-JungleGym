using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the prefab of its respective collectable

public class clipboardScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddClipboard();

            Destroy(gameObject);
        }
    }
}
