using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling : MonoBehaviour
{
    public Animator animator;
    [Header("Roll Settings")]
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;

    
    private float dashTimer = 0f;
 
    public float rollforce = 1;
    public float rollMax = 15;
    public float rollJump = 9;
    public float rollAirCtrl = 0;
    public bool isDashing = false;
    private Vector3 direction;

    Rigidbody rb;
    PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("q") && !isDashing)
        {
            direction = new Vector3(playerController.xInput,0, playerController.zInput).normalized;
            StartCoroutine(Dash());
        }

        if (isDashing)
        {
            DashMovement();
        }
        animator.SetBool("IsRolling", isDashing);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashTimer = 0f;
        float maxreturn = playerController.maxSpeed;
        float jumpreturn = playerController.jumpPower;
        float airctrlreturn = playerController.airControl;
        while (dashTimer < dashDuration)
        {
            playerController.maxSpeed = rollMax;
            playerController.jumpPower = rollJump;
            playerController.airControl = rollAirCtrl;
            dashTimer += Time.deltaTime;
            yield return null;
        }
        playerController.maxSpeed = maxreturn;
        playerController.jumpPower = jumpreturn;
        playerController.airControl = airctrlreturn;
        isDashing = false;
    }

    void DashMovement()
    {
        rb.AddForce(direction * rollforce);
        /*float distanceToMove = dashDistance * Time.deltaTime / dashDuration;
        if (playerController.xInput > 0 && playerController.zInput == 0)
        {
            transform.Translate(transform.right * distanceToMove);
        }
        else if (playerController.xInput > 0 && playerController.zInput < 0)
        {
            transform.Translate((transform.right - transform.forward).normalized * distanceToMove);
        }
        else if (playerController.xInput > 0 && playerController.zInput > 0)
        {
            transform.Translate((transform.right + transform.forward).normalized * distanceToMove);
        }
        else if (playerController.xInput < 0 && playerController.zInput < 0)
        {
            transform.Translate((-transform.right - transform.forward).normalized * distanceToMove);
        }
        else if (playerController.xInput < 0 && playerController.zInput > 0)
        {
            transform.Translate((-transform.right + transform.forward).normalized * distanceToMove);
        }
       else if (playerController.zInput < 0)
        {
            transform.Translate(-transform.forward * distanceToMove);
        }
        else
        {
            transform.Translate(transform.forward * distanceToMove);
        }*/
    }
}
