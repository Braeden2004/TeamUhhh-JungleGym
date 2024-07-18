using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    public GameObject player;
    public CinemachineFreeLook vcam;

    [Header("Zoom")]
    //reduce FOV on right click mouse
    public float zoomSpeed;
    public float zoomMin;
    private float zoomMax = 60.0f;

    [Header("Sensitivity")]
    public float baseSensitivityX = 100f;
    public float baseSensitivityY = 3f;
    public GameObject sensitivitySlider;


    private void Update()
    {
        //Zoom
        if (Input.GetMouseButton(1))
        {
            //zoom in
            vcam.m_Lens.FieldOfView -= zoomSpeed;

            //clamp the zoom
            vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView, zoomMin, zoomMax);


            //disable mesh renderer on player 
        }
        else
        {
            if (vcam.m_Lens.FieldOfView < zoomMax)
            {
                //zoom out
                vcam.m_Lens.FieldOfView += zoomSpeed;
            }

            //enable mesh renderer on player
        }

        //Sensitivity
        vcam.m_XAxis.m_MaxSpeed = baseSensitivityX * (sensitivitySlider.GetComponent<sensitivitySlider>()._slider.value); //multiplies base sensitivity by the value of the slider object
    }

}
