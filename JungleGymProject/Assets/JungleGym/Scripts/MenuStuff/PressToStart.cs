using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PressToStart : MonoBehaviour
{
    

    public GameObject MainMenu;
    public static bool gameStarted = false;

    // Update is called once per frame
    void Update()
    {
        //check if any button is pressed
        if (Input.anyKey)
        {
            //if any button is pressed, set the main menu to active
            MainMenu.SetActive(true);

            //set this object to inactive
            gameObject.SetActive(false);


            gameStarted = true;
        }


    }
}
