using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System.Threading.Tasks;

public class Metamask : MonoBehaviour
{
    public static Metamask Instance = null;
    public bool _metamaskCheck = false;

    public string walletAddress = "0x0";

    public BigInteger balance = 0;

    public void setcheckmetamask()
    {
        _metamaskCheck = true;
    }

    private void Awake() {
        Instance = this;
    }
    
}
