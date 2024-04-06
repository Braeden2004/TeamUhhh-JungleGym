using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckLevelScript : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.tag == "Level1")
        {
            Debug.Log("Now entering Level1");
        }

        if (other.tag == "Level2")
        {
            Debug.Log("Now entering Level2");
        }

        if (other.tag == "Challenge")
        {
            Debug.Log("Now entering Challenge");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //play music here???
    }
    

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Level1")
        {
            Debug.Log("Now exiting Level1");
        }

        if (other.tag == "Level2")
        {
            Debug.Log("Now exiting Level2");
        }

        if (other.tag == "Challenge")
        {
            Debug.Log("Now exiting Challenge");
        }
    }
}
