using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuTweening : MonoBehaviour
{
    public enum MenuType {MainMenuElement, BushElement, TutorialElement}
    [SerializeField] MenuType menuType;

    public bool useManualStartPos = false;
    public static bool reachedLocation = false;
    

    private Vector3 startPos; // location to move from
    public Vector3 targetPos; // location to move to

    public Image targetObject; // object to move to

    public GameObject startingCutscene;
    //public GameObject Monkeycutscene;

    public float speed = 1f; // speed of movement

    //Bush acceleration
    private float currentVerticalSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //get start pos
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuType == MenuType.MainMenuElement)
        {
            MainMenuElements();
        }

        if (menuType == MenuType.BushElement)
        {
            BushElement();
        }

        if (menuType == MenuType.TutorialElement)
        {
            TutorialElement();
        }
    }


    void MainMenuElements()
    {
        if (PressToStart.gameStarted == true)
        {

            if (useManualStartPos)
            {
                //move towards target with lerp
                transform.position = Vector3.Lerp(transform.position, startPos + targetPos, speed * Time.deltaTime);
            }
            else
            {
                //move towards target with lerp
                transform.position = Vector3.Lerp(transform.position, targetObject.rectTransform.position, speed * Time.deltaTime);
            }

        }
    }

    void BushElement()
    {
        if (mainMenu.GameIsStarted == true)
        {
            //set up speed for acceleration
            //currentVerticalSpeed += speed * Time.deltaTime;
            currentVerticalSpeed += Mathf.Pow(speed, 2) * Time.deltaTime;

            //Move the bush
            transform.position = new Vector3(transform.position.x, transform.position.y + currentVerticalSpeed, transform.position.z);

            //say if location is reached
            if (transform.position.y > targetObject.rectTransform.position.y)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //startingCutscene.SetActive(false);
                //Monkeycutscene.SetActive(true);
                

                Debug.Log("Reached Location");reachedLocation = true;

            }
        }

    }

    void TutorialElement()
    {
        if (useManualStartPos)
        {
            //move towards target with lerp
            transform.position = Vector3.Lerp(transform.position, startPos + targetPos, speed * Time.deltaTime);
        }
        else
        {
            //move towards target with lerp
            transform.position = Vector3.Lerp(transform.position, targetObject.rectTransform.position, speed * Time.deltaTime);
        }
    }
}
