using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    float originalGrav;
    float originalAirControl;

    [SerializeField] float newGrav;
    [SerializeField] float airControl;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        originalGrav = playerController.gravity;
        originalAirControl = playerController.airControl;
    }

    void Update()
    {
        if(playerController.jumpHold && playerController.isFalling)
        {
            playerController.gravity = newGrav;
            playerController.airControl = airControl;
        }
        else
        {
            playerController.gravity = originalGrav;
            playerController.airControl = originalAirControl;
        }
    }
}
