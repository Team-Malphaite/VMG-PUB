using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObstructController : MonoBehaviourPunCallbacks
{
    private float force = 10.0f;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("@Player") == null) return;
    }

    // Update is called once per frame
    void Update()
    {
        go = GameObject.Find("@Player");
    }

    private void OnCollisionEnter(Collision collision) {
        // if (go.GetComponent<PhotonView>().IsMine)
            if (collision.collider.name == "@Player")
            {
                Debug.Log("장애물 충돌");
                go.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            }
    }
}
