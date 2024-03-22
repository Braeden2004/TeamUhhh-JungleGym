using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rope : MonoBehaviour
{
    public Transform lineBottom;
    public Transform renderPoint;
    public LineRenderer rope;
    GameObject playerObj;
    public PlayerSwing player;


    //Braeden Variables for Context Menu
    public TextMeshProUGUI grabText;

    [SerializeField] GameObject telemteryTrigger;
    private void Start()
    {
        //rope = gameObject.AddComponent<LineRenderer>();
        rope = gameObject.GetComponent<LineRenderer>();
        rope.startWidth = 0.15f;
        rope.endWidth = 0.15f;
        renderPoint = lineBottom;

        //Start context menu false
        grabText.GetComponent<TextMeshProUGUI>().enabled = false;
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

                    //Make context menu false
                    grabText.GetComponent<TextMeshProUGUI>().enabled = false;
                    if (telemteryTrigger != null)
                    {
                        telemteryTrigger.SetActive(true);
                    }
                }
            }
            else
            {
                renderPoint = lineBottom;
                if (telemteryTrigger != null)
                {
                    telemteryTrigger.SetActive(false);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObj = other.gameObject;
            player = other.GetComponent<PlayerSwing>();

            //Make context menu visible
            grabText.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObj = other.gameObject;
            player = other.GetComponent<PlayerSwing>();

            //Make context menu false
            grabText.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void DrawRope(Transform pointToRender)
    {
        rope.SetPosition(0, transform.parent.position);
        rope.SetPosition(1, pointToRender.position);
    }
}
