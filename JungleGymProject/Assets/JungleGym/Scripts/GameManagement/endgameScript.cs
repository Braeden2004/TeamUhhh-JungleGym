using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endgameScript : MonoBehaviour
{

    public GameObject target;

    public bool endGame = false;

    //timer
    public float timerMax = 3f;
    public float timercurrent;


    public void Start()
    {
        timercurrent = timerMax;
    }

    public void OnTriggerEnter(Collider other)
    {
        //set the fade out object active
        if (other.tag == "Player") target.SetActive(true);

        endGame = true;
    }

    public void Update()
    {
        
    }
}
