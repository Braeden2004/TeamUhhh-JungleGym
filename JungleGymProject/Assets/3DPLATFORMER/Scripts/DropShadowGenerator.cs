using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadowGenerator : MonoBehaviour
{
    [SerializeField] GameObject shadow;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask slideLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            shadow.transform.position = hit.point + (Vector3.up * 0.01f);
            shadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, slideLayer))
        {
            shadow.transform.position = hit.point + (Vector3.up * 0.01f);
            shadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
}
