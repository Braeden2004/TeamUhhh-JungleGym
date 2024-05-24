using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; //Add empty gameobject as spawnPoint
    [SerializeField] GameObject player; //Add your player
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject progressMenuUI;
    [SerializeField] GameObject audioMenuUI;

    [Header("Audio")]
    AudioManager audioManager;

    [Header("Clipboard Animation")]
    public bool clipboardActive = false;


    [Header("RespawnTimer")]
    public bool respawnReady = true;
    public float respawnTime; //seconds to respawn
    public float currentRespawnTime;


    private void Awake()
    {
        //set which UI elements are active on startup
        progressMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);


        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //set respawn timer
        currentRespawnTime = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //escape key for pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !audioMenuUI.activeSelf)
        {
            Pause();

            TelemetryLogger.Log(this, "Player Pause Position", player.transform.position);
            TelemetryLogger.Log(this, "Tickets Collected", ScoreManager.instance.ticketTotal);
            TelemetryLogger.Log(this, "Clipboards Collected", ScoreManager.instance.clipboardTotal);

        }

        //hold tab for progress menu
        if (Input.GetKey(KeyCode.Tab))
        {
            clipboardActive = true;
            //progressMenuUI.SetActive(true);
        }
        else
        {
            clipboardActive = false;
            //progressMenuUI.SetActive(false);
        }

        //hold R to respawn
        HoldToRespawn();
    }

    public void Resume()
    {
        audioManager.PlaySFX(2, audioManager.Pause);
        audioManager.AdjustVolume(6, 1f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    void Pause()
    {
        audioManager.StopSFX(1);
        audioManager.StopSFX(2);
        audioManager.StopSFX(3);
        audioManager.PlaySFX(1, audioManager.Pause);
        audioManager.AdjustVolume(6, 0.5f);
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void AudioMenu()
    {
        pauseMenuUI.SetActive(false);
        audioMenuUI.SetActive(true);
    }

    public void GoBack()
    {
        pauseMenuUI.SetActive(true);
        audioMenuUI.SetActive(false);
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

    void HoldToRespawn()
    {
        //hold R for x seconds to respawn
        if (Input.GetKey(KeyCode.R))
        {
            //countdown
            currentRespawnTime -= Time.deltaTime;

            if (currentRespawnTime <= 0)
            {
                if (respawnReady == true)
                {
                    Respawn();
                    respawnReady = false;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            //cancel countdown and reset timer
            currentRespawnTime = respawnTime;

            respawnReady = true; // prevents player from getting stuck in respawn loop by holding down R
        }
    }
}
