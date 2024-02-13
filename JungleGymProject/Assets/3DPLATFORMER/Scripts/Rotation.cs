using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Update is called once per frame
    public float rotation = 0f;
    void Update()
    {
        transform.Rotate(0, 0 + rotation * Time.deltaTime, 0);
    }
}
