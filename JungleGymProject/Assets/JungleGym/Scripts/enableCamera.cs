using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class enableCamera : MonoBehaviour
{

    public GameObject otherCamera;


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
