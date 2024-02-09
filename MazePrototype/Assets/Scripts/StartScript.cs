using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    // The name of the scene you want to load
    public string sceneToLoad;

    // This function is called when the button is clicked
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
