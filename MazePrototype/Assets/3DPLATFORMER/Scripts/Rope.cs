using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    PlayerSwing player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<PlayerSwing>();
            player.ropeStartPoint = transform.parent;
            player.canSwing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.ropeStartPoint = null;
            player.canSwing = false;
            player = null;
        }
    }
}
