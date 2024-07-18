using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delaystart : MonoBehaviour
{
    //start animation after random time
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        //set animator inactive
        GetComponent<Animator>().enabled = false;

        //set delay to random value
        delayTime = Random.Range(0.5f, 5f);

        //start coroutine
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {

        //enable animator component after delay
        yield return new WaitForSeconds(delayTime);
        GetComponent<Animator>().enabled = true;
    }
}
