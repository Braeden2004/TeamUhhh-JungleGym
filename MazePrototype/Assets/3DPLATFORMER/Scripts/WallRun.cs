using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    Rigidbody rb;

    bool wallLeft;
    bool wallRight;

    RaycastHit leftWall;
    RaycastHit rightWall;

    [SerializeField] float wallCheckDistance = 0.7f;
    [SerializeField] LayerMask wallMask;
    [SerializeField] float groundCheckDistance = 1.5f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float wallStickForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        WallCheck();

        if(aboveGround() && (wallRight || wallLeft))
        {
            RunOnWall();
        }
        else
        {
            rb.useGravity = true;
        }
    }
    
    bool aboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }

    void WallCheck()
    {
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWall, wallCheckDistance, wallMask);
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWall, wallCheckDistance, wallMask);
    }

    void RunOnWall()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWall.normal : leftWall.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        rb.AddForce(-wallNormal * wallStickForce, ForceMode.Force); 
    }
}
