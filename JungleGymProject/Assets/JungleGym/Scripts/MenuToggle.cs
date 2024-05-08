using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToggle : MonoBehaviour
{
    //note to self, attach this script to the object you want to toggle it will need the specific image references 

    public Image On;
    public Image Off;
    public Image img;
    int index;


    // Update is called once per frame
    void Update()
    {
        if (index == 1)
        {
            img.gameObject.SetActive(false);
        }
        
        if (index == 0)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void ON()
    {
        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
    }

    public void OFF()
    {
        index = 0;
        Off.gameObject.SetActive(false);
        On.gameObject.SetActive(true);
    }
}
