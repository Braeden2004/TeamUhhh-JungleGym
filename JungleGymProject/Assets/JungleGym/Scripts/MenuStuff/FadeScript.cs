using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class FadeScript : MonoBehaviour
{
    public float fadeSpeed = 0.1f;
    public float delay = 2f;
    public Image image;

    public bool canFade = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //fade  
        if (canFade == true)
        {
            fadeOut();
        }


        //start to fade after delay in seconds
        StartCoroutine(FadeAfterDelay(delay));

    }

    IEnumerator FadeAfterDelay(float delay)
    {
        //can fade after delay
        yield return new WaitForSeconds(delay);

        Debug.Log(canFade);
        canFade = true;

    }

    void fadeOut()
    {
        image.CrossFadeAlpha(0, fadeSpeed, false);
    }
}
