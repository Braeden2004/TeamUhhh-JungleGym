using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollanim : MonoBehaviour
{
    public GameObject cube;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (cube.GetComponent<rolling>().isDashing == true)
        {
            transform.Rotate(new Vector3(360*3 * Time.deltaTime,0,0));
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
