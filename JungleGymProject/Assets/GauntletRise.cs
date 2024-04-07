using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauntletRise : MonoBehaviour
{
    public float riseSpeed = 10.0f;
    public float riseHeight = 100.0f;

    public bool forceRise = false; // dev option to force rise
    public bool currentlyRising = false;
    public GameObject otherCamera;


    // Start is called before the first frame update
    void Start()
    {
        otherCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((ScoreManager.instance.clipboardTotal == ScoreManager.instance.totalClipboardInScene-1) || forceRise == true)
        {
            Rise();
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







 


