using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class cutsceneManager : MonoBehaviour
{
    public bool skipReady = false;
    public GameObject skiptext;
    public VideoPlayer vPlayer;
    public Scene targetScene;

    private void Awake()
    {
        vPlayer.Play();
        vPlayer.loopPointReached += CheckOver;
    }


    // Start is called before the first frame update
    void Start()
    {
        skiptext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {

            //make sure players are not holding down space
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                skipReady = true;
            }
        }
        else //make sure players are not holding down space
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                skipReady = true;
            }
        }

        if (skipReady == true)
        {
            //set text active
            skiptext.SetActive(true);


            //allow player to skip cutscene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //skip cutscene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        
    }


    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //check if the video is over
        //reference https://discussions.unity.com/t/solved-how-to-change-scene-after-video-has-ended/760158/4
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //the scene to load after the video has ended.
    }
}
