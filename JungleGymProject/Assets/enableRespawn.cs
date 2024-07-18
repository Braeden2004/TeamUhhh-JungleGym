using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableRespawn : MonoBehaviour
{

    public GameObject respawnButton;
    public GameObject respawnButtonDummy;
    public bool canRespawn = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canRespawn = true;

            respawnButton.SetActive(true);
            respawnButtonDummy.SetActive(false);
        }
    }
}
