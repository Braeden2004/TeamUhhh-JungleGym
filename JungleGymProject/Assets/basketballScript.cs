using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketballScript : MonoBehaviour
{
    public bool basket = false;
    public float offsetY;

    public GameObject tickets;
    public GameObject squareObject;
    public GameObject circleObject;
    public GameObject basketObject;

    public Material greenPlastic;

    public Vector3 ticketSpawnPos;

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
                if (tickets.transform.position != ticketSpawnPos)
                {
                    tickets.transform.position = ticketSpawnPos;
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
