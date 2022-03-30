using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RespawnController : MonoBehaviourPunCallbacks
{
    public GameObject go;
    GameObject respawn1, respawn2, respawn3;
    // Start is called before the first frame update
    void Start()
    {
        respawn1 = GameObject.Find("Respawn1");
        respawn2 = GameObject.Find("Respawn2");
        respawn3 = GameObject.Find("Respawn3");

        if (GameObject.Find("@Player") == null) return;
    }

    // Update is called once per frame
    void Update()
    {
        go = GameObject.Find("@Player");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (go.GetComponent<PhotonView>().IsMine)
            if (collision.collider.CompareTag("RespawnPlayer1"))
                go.transform.position = respawn1.transform.position;
            if (collision.collider.CompareTag("RespawnPlayer2"))
                go.transform.position = respawn2.transform.position;
            if (collision.collider.CompareTag("RespawnPlayer3"))
                go.transform.position = respawn3.transform.position;
    }
}
