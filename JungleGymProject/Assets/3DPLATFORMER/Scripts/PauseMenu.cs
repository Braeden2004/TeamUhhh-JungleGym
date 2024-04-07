using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; //Add empty gameobject as spawnPoint
    [SerializeField] GameObject player; //Add your player
    [SerializeField] GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();

            TelemetryLogger.Log(this, "Player Pause Position", player.transform.position);
            TelemetryLogger.Log(this, "Tickets Collected", ScoreManager.instance.ticketTotal);
            TelemetryLogger.Log(this, "Clipboards Collected", ScoreManager.instance.clipboardTotal);

        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Respawn()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Resume();
    }
}
