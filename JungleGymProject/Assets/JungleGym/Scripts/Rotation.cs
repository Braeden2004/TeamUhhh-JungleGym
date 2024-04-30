using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Update is called once per frame
    private float rotation = 90f;
    void Update()
    {
        transform.Rotate(0, 0 + rotation * Time.deltaTime, 0);
    }
}
