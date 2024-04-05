using System.Reflection;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source ----------------")]
    [SerializeField]  AudioSource Music_Source;
    [SerializeField]  AudioSource SFX_Source;

    [Header("-----------Audio Clip ----------------")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip ropeGrab;
    public AudioClip ropeSwing;
    public AudioClip roll;
    public AudioClip run1;
    public AudioClip run2;
    public AudioClip run3;
    public AudioClip run4;

    public AudioClip baloonRise;
    public AudioClip balloonPop;
    public AudioClip wallBreak;
    public AudioClip monkeyGrunt1;
    public AudioClip monkeyGrunt2;
    public AudioClip monkeyGrunt3;
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

    public void PlaySFX(AudioClip clip)
    {
        SFX_Source.PlayOneShot(clip);
    }

}
