using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        
    }

    public void Move()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
