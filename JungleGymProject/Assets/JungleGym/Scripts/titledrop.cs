using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
//using static UnityEditor.Rendering.ShadowCascadeGUI;

public class titledrop : MonoBehaviour
{
    public GameObject otherCamera;

    //fading
    public float fadeSpeed = 0.1f;
    public float delay = 2f;

    public Image image;

    public bool canFadeIn = false;
    public bool canFadeOut = false;

    [Header("Audio")]
    AudioManager audioManager;

    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //immediatly set alpha to 0
        image.CrossFadeAlpha(0, 0, false);
    }

    // Update is called once per frame
    void Update()
    {
        //check if object is active
        if (otherCamera.activeSelf == true)
        {
            //start to fade after delay in seconds
            StartCoroutine(FadeAfterDelay(delay));
            //Insert AudioClip Here JUAN

        }

        //fade in
        if (canFadeIn == true)
        {
            fadeIn();
        }
        
        //fade out
        if (canFadeOut == true)
        {
            fadeOut();
        }
    }


    IEnumerator FadeAfterDelay(float delay)
    {
        canFadeIn = true;
        canFadeOut = false;

        //can fade after delay
        yield return new WaitForSeconds(delay);

        canFadeIn = false;
        canFadeOut = true;
    }

    void fadeOut()
    {
        image.CrossFadeAlpha(0, fadeSpeed, false);
    }

    void fadeIn()
    {         
        image.CrossFadeAlpha(1, fadeSpeed, false);
    }
}
