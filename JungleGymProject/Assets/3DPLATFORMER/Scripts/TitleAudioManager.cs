using System.Reflection;
using UnityEngine;

public class TitleAudioManager : MonoBehaviour
{
    [Header("-----------Audio Source ----------------")]
    [SerializeField] AudioSource Music_Source;
    [SerializeField] AudioSource SFX_Source;

    [Header("-----------Audio Clip ----------------")]
    

    public AudioClip title;
    public AudioClip settings;
    public AudioClip ingame;

    private void Start()
    {
        Music_Source.clip = title;
        Music_Source.loop = true;
        Music_Source.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX_Source.PlayOneShot(clip);
    }

}
