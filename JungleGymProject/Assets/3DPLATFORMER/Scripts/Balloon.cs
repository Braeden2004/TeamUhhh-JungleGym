using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Vector3 originalTransform;
    Rigidbody rb;
    Rigidbody playerBody;
    public GameObject playerObj;
    public Rope attachedRope;
    float timer;
    bool startPopTimer;
    public bool attached;
    bool addForceToPlayer;
    [SerializeField] float popForce;
    [SerializeField] float popTimer;
    [SerializeField] float floatSpeed;
    [SerializeField] float maxDist;

    public TextMeshProUGUI timerText;

    [Header("Audio")]
    AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        originalTransform = transform.position;

        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //timer 
        timerText.text = Convert.ToString((int)(popTimer-timer));

        if (attachedRope != null)
        {
            if (attachedRope.playerObj != null)
            {
                playerBody = attachedRope.playerObj.GetComponent<Rigidbody>();
                playerObj = attachedRope.playerObj;
            }
        }

        Vector3 balloonForce = (Vector3.up * floatSpeed * Time.fixedDeltaTime) + (transform.forward * floatSpeed * Time.fixedDeltaTime);

        if (attached)
        {
            if (playerObj != null)
            {
                if (playerObj.GetComponent<PlayerSwing>().isSwinging && playerObj.GetComponent<SpringJoint>() != null)
                {
                    SpringJoint attachedPlayerJoint = playerObj.GetComponent<SpringJoint>();

                    attachedPlayerJoint.minDistance = maxDist;
                    attachedPlayerJoint.maxDistance = maxDist;

                    attachedPlayerJoint.connectedAnchor = transform.position;

                    /*if (playerObj.GetComponent<PlayerController>().moveDir == Vector3.zero)
                    {
                        Vector3 playerForce = (Vector3.up * floatSpeed * 10f * Time.fixedDeltaTime) + (transform.forward * floatSpeed * Time.fixedDeltaTime); //legit just trying shit idk anymore
                        playerBody.AddForce(playerForce);

                    }
                    else
                    {
                        playerBody.AddForce(balloonForce);
                    }*/


                    startPopTimer = true;

                    


                }
            }
        }

        if(startPopTimer)
        {
            if(timer == 0)
            {
                //Audio for playing balloon rise
                audioManager.AdjustVolume(3, 1f);
                audioManager.defaultPitchSFX(3);
                audioManager.PlaySFX(3, audioManager.baloonRise);
            }

            //display timer
            timerText.gameObject.SetActive(true);

            rb.velocity = balloonForce;
            //rb.AddForce(balloonForce);

            timer += Time.fixedDeltaTime;

            if (timer > popTimer)
            {
                if (attached)
                {
                    addForceToPlayer = true;
                }
                Pop();
                Respawn();
            }
        }
        else
        {
            //hide timer
            timerText.gameObject.SetActive(false);
        }
    }

    void Pop()
    {
        if (addForceToPlayer)
        {
            playerObj.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * popForce, ForceMode.Impulse);
            addForceToPlayer = false;
        }

        if (attached)
        {
            playerObj.GetComponent<PlayerSwing>().ReleaseSwing();
            attached = false;
        }
        timer = 0;
        startPopTimer = false;

        //Audio for playing balloon rise

        audioManager.AdjustVolume(3, 1f);
        audioManager.defaultPitchSFX(3);
        audioManager.PlaySFX(3, audioManager.balloonPop);
    }

    void Respawn()
    {
        Instantiate(this.gameObject, originalTransform, transform.rotation);
        Destroy(this.gameObject);
    }
}
