using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    Roll roll;
    HingeRopeSwing swing;
    PlayerSwing balloon;
    float originalGrav;
    float originalMaxGrav;
    float originalAirControl;
    float timer;
    public bool tired;
    public bool isGliding;
    public Animator anim;

    [SerializeField][Range(0,1)] float gravityMultiplier;
    [SerializeField][Range(1,3)] float airControlMultiplier;
    [SerializeField] float timeLimit;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        roll = GetComponent<Roll>();
        swing = GetComponent<HingeRopeSwing>();
        balloon = GetComponent<PlayerSwing>();
        originalGrav = playerController.gravity;
        originalMaxGrav = playerController.maxGravity;
        originalAirControl = playerController.airControl;
    }

    void Update()
    {
        if(playerController.jumpHold && playerController.isFalling && !tired && !roll.isRolling && !swing.isSwinging)
        {
            playerController.gravity = originalGrav * gravityMultiplier;
            playerController.maxGravity = originalMaxGrav * gravityMultiplier;
            playerController.airControl = originalAirControl * airControlMultiplier;
            timer += Time.deltaTime;
            isGliding = true;
            if(timer > timeLimit)
            {
                tired = true;
                timer = 0; 
            }
        }
        else
        {
            isGliding = false;
            playerController.gravity = originalGrav;
            playerController.maxGravity = originalMaxGrav;
            playerController.airControl = originalAirControl;
        }

        if(playerController.isGrounded() || balloon.isSwinging || swing.isSwinging)
        {
            tired = false;
            timer = 0;
        }

        SetAnim();
    }

    void SetAnim()
    {
        if (isGliding)
        {
            anim.SetBool("IsGliding", isGliding);
        }
        else
        {
            anim.SetBool("IsGliding", false);
        }
    }
}
