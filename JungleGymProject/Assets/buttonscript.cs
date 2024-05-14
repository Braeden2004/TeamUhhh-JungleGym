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


                    canvas.SetActive(false); //hide ticket cost text

                    isPressed = true;
                }
            }
            
        }
    }


}
