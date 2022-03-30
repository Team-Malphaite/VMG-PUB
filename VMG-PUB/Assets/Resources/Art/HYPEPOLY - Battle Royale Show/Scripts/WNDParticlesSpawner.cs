using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WNDParticlesSpawner : MonoBehaviour
{
    public GameObject particle0, particle1, go1, go2;
    
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("Env/Spawn");
            if (root == null)
            {
                root = new GameObject {name = "Env/Spawn"};
            }
            return root;
        }
    }

    public void Particle0()
    {
        go1 = Instantiate(particle0, transform.position, new Quaternion());
        go1.AddComponent<WNDParticlesDestroyer>();
        go1.transform.SetParent(Root.transform);
    }
    public void Particle1()
    {
        go2 = Instantiate(particle1, transform.position, new Quaternion());
        go2.AddComponent<WNDParticlesDestroyer>();
        go2.transform.SetParent(Root.transform);
    }
}
