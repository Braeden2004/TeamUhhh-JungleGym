using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    float originalGrav;
    float originalMaxGrav;
    float originalAirControl;

    [SerializeField] float newGrav;
    [SerializeField] float airControl;
    [SerializeField] float newMaxGrav;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        originalGrav = playerController.gravity;
        originalMaxGrav = playerController.maxGravity;
        originalAirControl = playerController.airControl;
    }

    void Update()
    {
        if(playerController.jumpHold && playerController.isFalling)
        {
            playerController.gravity = newGrav;
            playerController.maxGravity = newMaxGrav;
            playerController.airControl = airControl;
        }
        else
        {
            playerController.gravity = originalGrav;
            playerController.maxGravity = originalMaxGrav;
            playerController.airControl = originalAirControl;
        }
    }
}
