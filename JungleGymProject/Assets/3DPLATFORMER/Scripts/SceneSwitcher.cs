using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.instance.clipboardTotal == 5)
        {
            //go to next scene in queue (build settings)
            SceneManager.LoadScene("DemoComplete");
        }
    }
}
