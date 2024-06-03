using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class enableCamera : MonoBehaviour
{

    public GameObject otherCamera;
    //public float camSwitchSpeed = 1.0f;


    public void Start()
    {
        otherCamera.SetActive(false);
    }

    //enable camera when player enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //set actuve
            otherCamera.SetActive(true);

            //adjust how fast the camera switches to the otherCamera

            Debug.Log("Camera enabled");
        }
    }

    //disable camera when player exits the trigger
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //set actuve
            otherCamera.SetActive(false);

            Debug.Log("Camera disabled");

        }
    }




}
