using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTelemetry : MonoBehaviour
{
    public void OnTriggerEnter()
    {
        TelemetryLogger.Log(this, "Rope Missed", transform.position);
    }
}
