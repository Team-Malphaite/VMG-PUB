using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WNDBallDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("Destroy") || collision.collider.CompareTag("RespawnPlayer1")
        || collision.collider.CompareTag("RespawnPlayer2") || collision.collider.CompareTag("RespawnPlayer3"))
        {
            Destroy(gameObject);
        }
    }
}
