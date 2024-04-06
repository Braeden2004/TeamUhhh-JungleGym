using System.Reflection;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source ----------------")]
    [SerializeField]  AudioSource Music_Source;
    [SerializeField]  AudioSource SFX_Source;
    [SerializeField] AudioSource SFX2_Source;
    [SerializeField] AudioSource SFX3_Source;
    [SerializeField] AudioSource RSFX_Source;

    [Header("-----------Audio Clip ----------------")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip ropeGrab;
    public AudioClip ropeSwing;
    public AudioClip roll;
    public AudioClip run;

    public AudioClip baloonRise;
    public AudioClip balloonPop;
    public AudioClip wallBreak;
    public AudioClip monkeyGrunt;
    public AudioClip ticketGet;
    public AudioClip clipboardGet;

    public AudioClip ambience;

    public AudioClip menuHover;
    public AudioClip menuPress;

    public AudioClip Pause;
    //Quiet down music on pause

    public AudioClip title;
    public AudioClip hubMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip endScreen;


    private void Start()
    {
        Music_Source.Stop();
        Music_Source.volume = 0.1f;
        Music_Source.clip = title;
        Music_Source.loop = true;
        Music_Source.Play();
    }

    public void PlaySFX(int slot, AudioClip clip)
    {
        if (slot == 1)
        {
            SFX_Source.PlayOneShot(clip);
        }
        if (slot == 2)
        {
            SFX2_Source.PlayOneShot(clip);
        }
        if (slot == 3)
        {
            SFX3_Source.PlayOneShot(clip);
        }
    }

    public void PlayRSFX(AudioClip clip)
    {
        RSFX_Source.clip = clip;
        RSFX_Source.Play();
    }

    public void StopRSFX(AudioClip clip)
    {
        RSFX_Source.Stop();
    }

    public void PitchAdjustSFX(int slot, float min, float max)
    {
        if (slot == 1)
        {
            SFX_Source.pitch = Random.Range(min, max);
        }
        if (slot == 2)
        {
            SFX2_Source.pitch = Random.Range(min, max);
        }
        if (slot == 3)
        {
            SFX3_Source.pitch = Random.Range(min, max);
        }
    }

    public void PitchAdjustRSFX(float min, float max)
    {
        RSFX_Source.pitch = Random.Range(min, max);
    }

    public void defaultPitchSFX(int slot)
    {
        if (slot == 1)
        {
            SFX_Source.pitch = 1f;
        }
        if (slot == 2)
        {
            SFX2_Source.pitch = 1f;
        }
        if (slot == 3)
        {
            SFX3_Source.pitch = 1f;
        }
        
    }

    public void defaultPitchRSFX()
    {
        RSFX_Source.pitch = 1f;
    }
}
