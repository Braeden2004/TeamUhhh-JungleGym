using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisionSceneManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void toApproveScene()
    {
        //switch scenes
        SceneManager.LoadScene("ApproveCutscene");
    }

    public void toDenyScene()
    {
        //switch scenes
        SceneManager.LoadScene("DenyCutscene");
    }


}
