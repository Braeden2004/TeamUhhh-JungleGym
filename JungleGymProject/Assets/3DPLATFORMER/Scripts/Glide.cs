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
    float timer;
    public bool tired;

    [SerializeField][Range(0,1)] float gravityMultiplier;
    [SerializeField][Range(1,3)] float airControlMultiplier;
    [SerializeField] float timeLimit;

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
        if(playerController.jumpHold && playerController.isFalling && !tired)
        {
            playerController.gravity = originalGrav * gravityMultiplier;
            playerController.maxGravity = originalMaxGrav * gravityMultiplier;
            playerController.airControl = originalAirControl * airControlMultiplier;
            timer += Time.deltaTime;
            if(timer > timeLimit)
            {
                tired = true;
                timer = 0; 
            }
        }
        else
        {
            playerController.gravity = originalGrav;
            playerController.maxGravity = originalMaxGrav;
            playerController.airControl = originalAirControl;
        }

        if(playerController.isGrounded())
        {
            tired = false;
            timer = 0;
        }
    }
}
