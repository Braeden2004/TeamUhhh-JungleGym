using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextMenuAppear : MonoBehaviour
{
    public TextMeshProUGUI self;
    public float triggerRadius = 2f;

    private GameObject player;
    private PlayerSwing swingScript;

    private void Start()
    {

        player = GameObject.FindWithTag("Player");
        swingScript = player.GetComponent<PlayerSwing>();

    }
    void Update()
    {
        //Set mesh render to false
        self.GetComponent<TextMeshProUGUI>().enabled = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, triggerRadius);
        //Check for player within radius
        foreach (Collider collider in colliders)
        {
            //Check for collision with target
            if (collider.CompareTag("Player"))
            {
                if (swingScript.isSwinging == false)
                {
                    self.GetComponent<TextMeshProUGUI>().enabled = true;
                }
                else
                {
                    self.GetComponent<TextMeshProUGUI>().enabled = false;
                }

            }
        }
    }
}
