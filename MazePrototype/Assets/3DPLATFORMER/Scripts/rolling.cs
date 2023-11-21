using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;

    public bool isDashing = false;
    private float dashTimer = 0f;

    private void Update()
    {
        if (Input.GetKeyDown("q") && !isDashing)
        {
            StartCoroutine(Dash());
        }

        if (isDashing)
        {
            DashMovement();
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
            yield return null;
        }
        
        isDashing = false;
    }

    void DashMovement()
    {
        
        float distanceToMove = dashDistance * Time.deltaTime/dashDuration;
        transform.Translate(Vector3.forward * distanceToMove);
    }
}
