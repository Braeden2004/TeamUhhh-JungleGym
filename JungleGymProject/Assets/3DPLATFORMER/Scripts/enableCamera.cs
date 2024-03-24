using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class enableCamera : MonoBehaviour
{

    public GameObject otherCamera;

    //enable camera when player enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //set actuve
            otherCamera.SetActive(true);
        }
    }

    //disable camera when player exits the trigger
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //set actuve
            otherCamera.SetActive(false);

        }
    }



}
