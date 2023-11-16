using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            SceneManager.LoadScene("Player2Win");
        }
        if (collider.tag == "Player2")
        {
            SceneManager.LoadScene("Player1Win");
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
