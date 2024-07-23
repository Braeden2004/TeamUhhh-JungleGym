using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphicsSettings : MonoBehaviour
{

    //reference https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=1s
    public void SetQuality(int qualityIndex)
    {
        //set unity quality to number of qualityIndex
        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
