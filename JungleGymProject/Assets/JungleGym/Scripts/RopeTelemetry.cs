using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTelemetry : MonoBehaviour
{
    float ropesMissed;

    public void OnTriggerEnter()
    {
        ropesMissed++;
    }

    private void OnApplicationQuit()
    {
        TelemetryLogger.Log(this, "Total Ropes Missed", ropesMissed);
    }
}
