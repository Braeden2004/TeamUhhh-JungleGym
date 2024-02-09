using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform lineBottom;
    public Transform renderPoint;
    public LineRenderer rope;
    GameObject playerObj;
    PlayerSwing player;

    private void Start()
    {
        rope = GetComponent<LineRenderer>();
        renderPoint = lineBottom;
    }

    private void Update()
    {
        DrawRope(renderPoint);

        if (player != null)
        {
            if (player.connectedRope == this)
            {
                if (player.isSwinging)
                {
                    renderPoint = playerObj.transform;
                }
            }
            else
            {
                renderPoint = lineBottom;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObj = other.gameObject;
            player = other.GetComponent<PlayerSwing>();
        }
    }

    void DrawRope(Transform pointToRender)
    {
        rope.SetPosition(0, transform.parent.position);
        rope.SetPosition(1, pointToRender.position);
    }
}
