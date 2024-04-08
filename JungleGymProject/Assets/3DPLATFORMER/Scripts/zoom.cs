using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class zoom : MonoBehaviour
{
    //reduce FOV on right click mouse
    public CinemachineFreeLook vcam;
    public float zoomSpeed;
    public float zoomMin;
    private float zoomMax = 60.0f;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //zoom in
            vcam.m_Lens.FieldOfView -= zoomSpeed;

            //clamp the zoom
            vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView, zoomMin, zoomMax);
        }
        else
        {
            if (vcam.m_Lens.FieldOfView < zoomMax)
            {
                //zoom out
                vcam.m_Lens.FieldOfView += zoomSpeed;
            }
        }
        
    }

}
