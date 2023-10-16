using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component on this object
    }

    // Detect player touching the spawned object
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player touched the object"); // Debug message (optional)

        // Play the audio clip
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Destroy the object after the sound has finished playing
        Destroy(gameObject, audioSource.clip.length);
    }
}
