using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform lineBottom;
    PlayerSwing player;
    public LineRenderer rope;
    GameObject playerObj;

    private void Start()
    {
        rope = GetComponent<LineRenderer>();
        rope.SetPosition(0, transform.parent.position);
        rope.SetPosition(1, lineBottom.position);
    }

    private void Update()
    {
        if (player != null)
        {
            if (player.isSwinging)
            {
                rope.SetPosition(1, playerObj.transform.position);
            }
            else
            {
                DrawRope();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObj = other.gameObject;
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
            //player = null;
        }
    }

    void DrawRope()
    {
        rope.SetPosition(0, transform.parent.position);
        rope.SetPosition(1, lineBottom.position);
    }
}
