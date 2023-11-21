using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;


public class SwitchLanes : MonoBehaviour
{
    public float horizontalpos;
    public float newhorizontalpos;
    public float verticalpos = -6f;
    public float newverticalpos;
    public float speed = 10;
    public float wiggleDuration = 0.5f;
    public float wiggleSpeed = 30f;
    private bool hasMoved;
    private float hasMovedtimer;
    public GameObject player2;
    private Vector3 pastposition;
    // Start is called before the first frame update
    void Start()
    {
        verticalpos = -6f;
        horizontalpos = 0f;
        newverticalpos = -6f;
    }

    // Update is called once per frame
    void Update()
    {
       




        if (Input.GetKeyDown("a") && hasMoved == false && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 2.4f))
        {
            
            StartCoroutine(WiggleCoroutine());
            newhorizontalpos = Mathf.Clamp(horizontalpos - 2.4f, -2.4f, 2.4f);
            hasMoved = true;
            hasMovedtimer = 0;
            pastposition = transform.position;
        }
        if (Input.GetKeyDown("d") && hasMoved == false && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 2.4f))
        {
            StartCoroutine(WiggleCoroutine());
            newhorizontalpos = Mathf.Clamp(horizontalpos + 2.4f, -2.4f, 2.4f);
            hasMoved = true;
            hasMovedtimer = 0;
            pastposition = transform.position;
        }
        if (Input.GetKeyDown("s") && hasMoved == false && !Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), 2.4f))
        {
            StartCoroutine(WiggleCoroutine());
            newverticalpos = Mathf.Clamp(verticalpos - 2f, -6f, -4f);
            hasMoved = true;
            hasMovedtimer = 0;
            pastposition = transform.position;
        }
        if (Input.GetKeyDown("w") && hasMoved == false && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 2.4f))
        {
            StartCoroutine(WiggleCoroutine());
            newverticalpos = Mathf.Clamp(verticalpos + 2f, -6f, -4f);
            hasMoved = true;
            hasMovedtimer = 0;
            pastposition = transform.position;
        }
        Move();
        if (transform.position == player2.transform.position)
        {
            Debug.Log("ouch e");
            newhorizontalpos = pastposition.x;
            newverticalpos = pastposition.z;
        }
        hasMovedtimer += Time.deltaTime;
        if(hasMovedtimer >= 0.1f)
        {
            hasMoved = false;
        }
        
        
        

    }
    void Move()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(newhorizontalpos, 0.5f, newverticalpos), speed * Time.deltaTime);
        horizontalpos = newhorizontalpos;
        verticalpos = newverticalpos;
    }
    IEnumerator WiggleCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < wiggleDuration)
        {
            // Calculate the new scale based on a sine wave for a wiggling effect
            float yOffset = Mathf.Sin(Time.time * wiggleSpeed) * ((wiggleDuration - elapsedTime) / wiggleDuration) * 0.2f; // Adjust the amplitude as needed
                                                                                                                           
            transform.localScale = new Vector3(yOffset*5 + 1, yOffset + 1, yOffset + 1);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the scale after the wiggle duration
        transform.localScale = new Vector3(1, 1, 1);
        
    }
}



