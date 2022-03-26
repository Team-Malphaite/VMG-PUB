using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController30 : MonoBehaviour
{
    GameObject go;
    Animator anim;
    MeshCollider mesh;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("Gate30");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider col) {
        anim = go.GetComponent<Animator>();
        mesh = go.GetComponent<MeshCollider>();
        if(col.gameObject.tag == "Player") {
            anim.SetBool("open", true);
            mesh.convex = false;
        }
    }

    void OnTriggerExit(Collider col) {
        mesh = go.GetComponent<MeshCollider>();
        anim = go.GetComponent<Animator>();
        anim.SetBool("open", false);
        mesh.convex = true;

    }
}
