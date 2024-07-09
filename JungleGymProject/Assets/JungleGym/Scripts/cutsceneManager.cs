using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cutsceneManager : MonoBehaviour
{
    public bool skipReady = false;
    public GameObject skiptext;


    // Start is called before the first frame update
    void Start()
    {
        skiptext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {

            //make sure players are not holding down space
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                skipReady = true;
            }
        }
        else //make sure players are not holding down space
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                skipReady = true;
            }
        }

        if (skipReady == true)
        {
            //set text active
            skiptext.SetActive(true);


            //allow player to skip cutscene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //skip cutscene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}
