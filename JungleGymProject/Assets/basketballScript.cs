using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketballScript : MonoBehaviour
{
    public bool basket = false;
    public bool manualOveride = false;
    

    //net inside gauntlet hole
    public bool GauntletNet = false;
    public GauntletRise gauntletRiseScript;

    public float offsetY;

    public GameObject tickets;
    public GameObject squareObject;
    public GameObject circleObject;
    public GameObject basketObject;

    public Material greenPlastic;

    public Vector3 ticketSpawnPos;
    public Vector3 manualSpawnPos;

    [Header("Particles")]
    public ParticleSystem unlockParticle;
    public ParticleSystem auraParticle;
    public bool particlePlayed = false;


    [Header("Audio")]
    AudioManager audioManager;

    private void Awake()
    {
        //Sets the audio stuff up
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ticketSpawnPos = tickets.transform.position;

        //start tickets underground
        tickets.transform.position = new Vector3(transform.position.x, transform.position.y - offsetY, transform.position.z);
    }

    void Update()
    {
        if (GauntletNet == true)
        {
            if ((ScoreManager.instance.clipboardTotal == ScoreManager.instance.totalClipboardInScene - 1) || gauntletRiseScript.forceRise == true)
            {
                transform.position = new Vector3(999999, 99999, 99999); //teleport into the shadowrelm

                Debug.Log("Teleporting");
            }
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (basket == false)
            {
                //SFX
                audioManager.defaultPitchSFX(3);
                audioManager.AdjustVolume(3, 10f);
                audioManager.PlaySFX(3, audioManager.wallBreak);

                //teleport attraction above ground
                if (manualOveride == false)
                {
                    if (tickets.transform.position != ticketSpawnPos)
                    {
                        tickets.transform.position = ticketSpawnPos;
                    }
                }
                else
                {
                    tickets.transform.position = manualSpawnPos;
                }
                

                //Spawn Effects
                if (particlePlayed == false)
                {
                    Instantiate(unlockParticle, transform.position, Quaternion.identity);
                    particlePlayed = true;
                }

                //change material of basketball basket
                basketObject.GetComponent<Renderer>().material = greenPlastic;

                //replace backboard symbol
                squareObject.SetActive(false);
                circleObject.SetActive(true);


                //delete aura
                auraParticle.Stop();


                basket = true;
            }

        }
    }
}
