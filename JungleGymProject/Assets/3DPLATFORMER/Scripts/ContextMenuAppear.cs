using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextMenuAppear : MonoBehaviour
{
    public TextMeshProUGUI self;
    public float triggerRadius = 2f;

    private void Start()
    {
        
    }
    void Update()
    {
        //Set mesh render to false
        self.GetComponent<TextMeshProUGUI>().enabled = false;
        //Check for player within radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, triggerRadius);

        foreach (Collider collider in colliders)
        {
            //Check for collision with target
            if (collider.CompareTag("Player"))
            {
                self.GetComponent<TextMeshProUGUI>().enabled = true;
            }
    
        }


    }
}
