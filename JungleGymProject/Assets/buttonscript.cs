using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonScript : MonoBehaviour
{
    public float teleportDistance;
    public bool isPressed = false;

    //ticket stuff
    public float ticketCost;
    public TextMeshProUGUI ticketCostText;

    public GameObject canvas; // the canvas that holds the ticket cost text

    [Header("Audio")]
    AudioManager audioManager;

    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        ticketCostText.text = ticketCost.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isPressed == false)
            {
                if (ScoreManager.instance.ticketTotal >= ticketCost)
                {
                    //teleport down to show press
                    transform.position = new Vector3(transform.position.x, transform.position.y - teleportDistance, transform.position.z);

                    //SFX
                    audioManager.defaultPitchSFX(3);
                    audioManager.AdjustVolume(3, 10f);

                    audioManager.PlaySFX(3, audioManager.wallBreak);
                    audioManager.PlaySFX(3, audioManager.bigButtonPress);

                    //hide hovering text
                    canvas.SetActive(false); 

                    isPressed = true;
                }
            }
            
        }
    }


}
