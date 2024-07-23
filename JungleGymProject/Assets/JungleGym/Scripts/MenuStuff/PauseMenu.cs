using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; //Add empty gameobject as spawnPoint
    [SerializeField] GameObject player; //Add your player
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject progressMenuUI;
    [SerializeField] GameObject optionsMenuUI;
    [SerializeField] GameObject audioMenuUI;
    [SerializeField] GameObject controlsMenuUI;
    [SerializeField] GameObject quitMenuUI;
    [SerializeField] GameObject displayMenuUI;
    [SerializeField] Slider _slider;

    //forcing cursor to appear
    [SerializeField] bool forceCursor = false;
    [SerializeField] GameObject cursorMessage;


    public enableRespawn enableRespawnScript;
    public PlayerController playerController;

    //variables to prevent pause menu from displaying when in the display menu (it has a transparent backgrounds)
    private bool inDisplayMenu = false;

    [Header("Audio")]
    AudioManager audioManager;

    [Header("Clipboard Animation")]
    public bool clipboardActive = false;


    [Header("RespawnTimer")]
    public bool respawnReady = true;
    public float respawnTime; //seconds to respawn
    public float currentRespawnTime;
    public bool notTouchingButton = true;


    private void Awake()
    {

        //get player script

        //set which UI elements are active on startup
        progressMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);


        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        cursorMessage.SetActive(false);

        //set respawn timer
       // currentRespawnTime = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //toggle cursor state manually
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (forceCursor == false) forceCursor = true;
        }
        else
        {
            if (forceCursor == true) forceCursor = false;
        }

        if (forceCursor == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        //escape key for pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !audioMenuUI.activeSelf)
        {
            if (inDisplayMenu == false) Pause(); // this if statement prevents a bug where the pause menu shows while in display menu


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
        if (enableRespawnScript.canRespawn == true) HoldToRespawn();

    }

    public void Resume()
    {
        audioManager.PlaySFX(2, audioManager.Pause);
        audioManager.AdjustVolume(6, 1f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        cursorMessage.SetActive(false);
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

        cursorMessage.SetActive(true);
    }

    public void QuitMenu()
    {
        quitMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionsMenuUI.SetActive(true);

        pauseMenuUI.SetActive(false);
        audioMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        displayMenuUI.SetActive(false);
        quitMenuUI.SetActive(false);


        inDisplayMenu = false;
    }

    public void AudioMenu()
    {
        audioMenuUI.SetActive(true);

        optionsMenuUI.SetActive(false);
        
    }

    public void ControlsMenu()
    {
        controlsMenuUI.SetActive(true);

        optionsMenuUI.SetActive(false);

    }

    public void DisplayMenu()
    {
        displayMenuUI.SetActive(true);

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);


        inDisplayMenu = true;
    }

    public void BackToPauseMenu()
    {
        pauseMenuUI.SetActive(true);

        optionsMenuUI.SetActive(false);
        audioMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        quitMenuUI.SetActive(false);
    }


    public void QuitGame()
    {

        Application.Quit();
    }

    public void Respawn()
    {
        //player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //StartCoroutine(Teleport());
        StartCoroutine(Teleport());
    }

    void HoldToRespawn()
    {
        //radial menu
        _slider.value = currentRespawnTime;


        //hold R for x seconds to respawn
        if (Input.GetKey(KeyCode.R))
        {

            notTouchingButton = false;
            //countdown

            if (currentRespawnTime >= respawnTime)
            {
                if (respawnReady == true)
                {
                    Respawn();
                    respawnReady = false;
                }
            }
            else
            {
                currentRespawnTime += Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            notTouchingButton = true;
            respawnReady = true; // prevents player from getting stuck in respawn loop by holding down R
        }


        //decrease radial menu progress smoothly
        //cancel countdown and reset timer

        if (notTouchingButton == true)
        {
            if (currentRespawnTime > 0)
            {
                currentRespawnTime -= Time.deltaTime;
            }
        }


    }

    IEnumerator Teleport()
    {
        //reference https://www.youtube.com/watch?v=xmhm5jGwonc

        //disable player
        playerController.disabled = true;
        player.GetComponent<Rigidbody>().isKinematic = true;//set player rigidbody kinematic

        yield return new WaitForSeconds(0.01f);

        //teleport player
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        yield return new WaitForSeconds(0.01f);

        //enable player
        playerController.disabled = false;
        player.GetComponent<Rigidbody>().isKinematic = false; //set player rigidbody kinematic

        Resume();



    }
}
