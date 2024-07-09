using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauntletRise : MonoBehaviour
{
    public float riseSpeed;
    public float riseHeight;

    public bool forceRise = false; // dev option to force rise
    public bool currentlyRising = false;
    public GameObject otherCamera;

    private Vector3 startPosition;

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

        //set othercamera
        //otherCamera = GameObject.Find("GauntletRiseCam");
        otherCamera.SetActive(false);

        startPosition = new Vector3(231.7f,-215.3f, 15.6f);

    }

    // Update is called once per frame
    void Update()
    {
        if ((ScoreManager.instance.clipboardTotal >= ScoreManager.instance.totalClipboardInScene-1) || forceRise == true)
        {
            Rise();
        }
        else
        {
            transform.position = startPosition; //this means the prefab will start raised but go underground in a single frame, this fixes the baloon glitch
        }

    }

    public void Rise()
    {
        if (transform.position.y < riseHeight)
        {
            //rise
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
            currentlyRising = true;

            Debug.Log("Rising");

            //set actuve
            otherCamera.SetActive(true);

            //CUE SOUND CUE
            audioManager.PlaySFX(3, audioManager.GauntletRise);

            Debug.Log("Camera enabled");
        }
        else
        {
            currentlyRising = false;

            //set actuve
            otherCamera.SetActive(false);

            Debug.Log("Camera disabled");
        }
    }


           
  

         

}







 


