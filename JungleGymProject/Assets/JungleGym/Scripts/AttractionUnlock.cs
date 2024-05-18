using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionUnlock : MonoBehaviour
{
    public bool unlocked = false;

    public GameObject attraction;
    public GameObject cover;

    public float offsetY;
    public Vector3 attractionSpawnPos;

    //get script of button
    public buttonScript buttonScript;

    [Header("Particles")]
    public ParticleSystem unlockParticle;
    public bool particlePlayed = false;

    void Awake()
    {
        attractionSpawnPos = attraction.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //NOTE: The attraction starts active because it needs 1 frame at the beginning to tell the game how many clipboards are in the world, the update method will then turn it off after frame 1
        cover.SetActive(true); //make cover appear

        attraction.transform.position = new Vector3(transform.position.x, transform.position.y - offsetY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonScript.isPressed == true) unlocked = true;


        if (unlocked == true)
        {
            //change active states
            cover.SetActive(false); //make cover dissapear

            //teleport attraction above ground
            if (attraction.transform.position != attractionSpawnPos)
            {
                attraction.transform.position = attractionSpawnPos;
            }
            

            //Spawn Effects
            if (particlePlayed == false)
            {
                Instantiate(unlockParticle, transform.position, Quaternion.identity);
                particlePlayed = true;
            }
        }
        else
        {
            //teleport attraction underground
            
        }

        
    }
}
