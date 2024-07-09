using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    [Header("Audio")]
    AudioManager audioManager;

    public ParticleSystem destroyParticle;

    public float timerMax;
    public float timerCurrent;




    public void Start()
    {
        timerCurrent = timerMax;
    }

    private void Update()
    {
        //if player has all clipboards
        if (ScoreManager.instance.clipboardTotal == ScoreManager.instance.totalClipboardInScene)
        {
            //start timer
            timerCurrent -= Time.deltaTime;

            if (timerCurrent < 0)
            {

                //spawn particle effect
                Instantiate(destroyParticle, transform.position, Quaternion.identity);


                //destroy the object
                Destroy(gameObject);

                Debug.Log("Destroyed");
            }
        }
    }
}
