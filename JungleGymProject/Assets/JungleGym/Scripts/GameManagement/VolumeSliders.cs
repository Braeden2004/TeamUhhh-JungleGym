using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSliders : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float musicVol)
    {
        audioMixer.SetFloat("musicVol", musicVol);
    }

    public void SetSFXVolume(float sfxVol)
    {
        audioMixer.SetFloat("sfxVol", sfxVol);
    }

    public void SetAmbienceVolume(float ambienceVol)
    {
        audioMixer.SetFloat("ambienceVol", ambienceVol);
    }
}
