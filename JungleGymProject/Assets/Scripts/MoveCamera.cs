using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float mouseSensitivity = 2.0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        // Rotate the camera on the X axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
