using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameTest : MonoBehaviourPunCallbacks
{
    GameObject go;
    GameObject dest;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("@Player") == null) return;
        dest = GameObject.Find("test2");
    }

    void Update()
    {
        go = GameObject.Find("@Player");
    }

    private void OnTriggerEnter(Collider other) {
       // if (go.GetComponent<PhotonView>().IsMine)
        //{
            // go.GetComponent<PlayerController>()._goalCheck = true;
            go.transform.position = dest.transform.position;
       // }
    }
}
