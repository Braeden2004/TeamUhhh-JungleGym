using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour
{
    public static bool GameIsStarted = false;


    private void Awake()
    {
        //Enable Mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        //go to next scene in queue (build settings)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        GameIsStarted = true;

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit(); // this doesnt work in editor just build
    }

    public void Menu()
    {
        Debug.Log("MainMenu");

        //go to next scene in queue (build settings)
        SceneManager.LoadScene("Menu");
    }
}
