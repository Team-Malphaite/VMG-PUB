using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metamask : MonoBehaviour
{
    public static Metamask Instance = null;
    public bool _metamaskCheck = false;

    public string walletAddress;


    public void setcheckmetamask()
    {
        _metamaskCheck = true;
    }

    private void Awake() {
        Instance = this;
    }
    
}
