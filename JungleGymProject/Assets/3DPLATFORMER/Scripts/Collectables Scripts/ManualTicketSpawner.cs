using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTicketSpawner : MonoBehaviour
{
    public GameObject ticketPrefab;

    void Update()
    {
        //start timer
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(ticketPrefab, -transform.forward, Quaternion.identity);
        }
    }

}
