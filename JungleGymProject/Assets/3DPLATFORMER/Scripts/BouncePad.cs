using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    //make a public float for the bounce force
    public float bounceForce;
    public Rigidbody rb;

    //add impulse force to the player 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }

        //make the hidden monkey fellas bounce
        if (collision.gameObject.tag == "HiddenMonkey")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * bounceForce/2, ForceMode.Impulse);
        }
    }

}
