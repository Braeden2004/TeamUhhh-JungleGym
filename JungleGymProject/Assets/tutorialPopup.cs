using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPopup : MonoBehaviour
{

    public GameObject target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            target.SetActive(false);
        }
    }
}
