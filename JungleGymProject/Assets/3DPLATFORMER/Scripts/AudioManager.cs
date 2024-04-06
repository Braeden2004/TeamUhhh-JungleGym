using System.Collections;
using System.Reflection;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source ----------------")]
    [SerializeField] AudioSource Music_Source;
    [SerializeField] AudioSource SFX_Source;
    [SerializeField] AudioSource SFX2_Source;
    [SerializeField] AudioSource SFX3_Source;
    [SerializeField] AudioSource RSFX_Source;
    [SerializeField] AudioSource RSFX2_Source;

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
    public AudioClip bouncePad;

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
        
        Music_Source.volume = 0.1f;
        Music_Source.clip = title;
        Music_Source.Play();
        RSFX2_Source.clip = ambience;
        RSFX2_Source.Play();
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

    public void StopRSFX()
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

    public bool PlayingRSFX()
    {
        return RSFX_Source.isPlaying;
    }

    public void NewMusicTrack(AudioClip clip)
    {
        Debug.Log("MS: " + Music_Source.clip.name);
        Debug.Log("Clip: " + clip.name);

        if (Music_Source.clip.name != clip.name)
        {
            StopAllCoroutines();

            StartCoroutine(FadeTrackOut());

            Music_Source.Stop();
            Music_Source.clip = clip;
            Music_Source.Play();

            StartCoroutine(FadeTrackIn());
       }
    }

    public void AdjustVolume(int slot,  float volume)
    {
        if (slot == 1)
        {
            SFX_Source.volume = volume;
        }
        if (slot == 2)
        {
            SFX2_Source.volume = volume;
        }
        if (slot == 3)
        {
            SFX3_Source.volume = volume;
        }
        if (slot == 4)
        {
            RSFX_Source.volume = volume;
        }
        if (slot == 5)
        {
            RSFX2_Source.volume = volume;
        }
        if (slot == 6)
        {
            Music_Source.volume = volume;
        }
    }

    private IEnumerator FadeTrackOut()
    {
        Debug.Log("Fading out");
        float timeToFade = 1.25f;
        float timeElapsed = 0;

        while(timeElapsed < timeToFade)
        {
            Music_Source.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            yield return null;
        }

    }

    private IEnumerator FadeTrackIn()
    {
        Debug.Log("Fading in");

        float timeToFade = 1.25f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            Music_Source.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            yield return null;
        }

    }
}
