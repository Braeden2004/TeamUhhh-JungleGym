using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressClipboardScript : MonoBehaviour
{
    private Vector3 startPos; // location to move from
    public Image targetObject; // object to move to
    public float speed = 1f; // speed of movement

    public MenuManager PauseMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        //get start pos
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //check if this object is active
        if (PauseMenuScript.clipboardActive == true)
        {
            //move towards target with lerp
            //move towards target with lerp
            transform.position = Vector3.Lerp(transform.position, targetObject.rectTransform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, speed * Time.deltaTime);
        }
    }
}
