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
        if (musicVol == -20f)
        {
            audioMixer.SetFloat("musicVol", -80);
        }
    }

    public void SetSFXVolume(float sfxVol)
    {
        audioMixer.SetFloat("sfxVol", sfxVol);
        if (sfxVol == -20f)
        {
            audioMixer.SetFloat("musicVol", -80);
        }
    }

    public void SetAmbienceVolume(float ambienceVol)
    {
        audioMixer.SetFloat("ambienceVol", ambienceVol);
        if (ambienceVol == -20f)
        {
            audioMixer.SetFloat("musicVol", -80);
        }
    }
}
