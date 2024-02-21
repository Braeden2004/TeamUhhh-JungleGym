using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rope : MonoBehaviour
{
    public Transform lineBottom;
    public Transform renderPoint;
    public LineRenderer rope;
    public TextMeshPro textPrompt;
    GameObject playerObj;
    public PlayerSwing player;

    private void Start()
    {
        rope = gameObject.AddComponent<LineRenderer>();
        rope.startWidth = 0.15f;
        rope.endWidth = 0.15f;
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
