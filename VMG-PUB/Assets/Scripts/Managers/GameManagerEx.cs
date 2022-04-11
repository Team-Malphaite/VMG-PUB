using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManagerEx : MonoBehaviourPunCallbacks
{
    public static GameManagerEx Instance;
    [SerializeField]
    List<GameObject> Players = new List<GameObject>();
    bool allReady = false;
    
    private void Awake()
    { 
        if (Instance == null) 
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(player);
            // Debug.Log("add player");
            Players = Players.Distinct().ToList();
        }
    }

    public bool readyCheck()
    {
        if (Players.Count == 0) return false;
        int readyCount = 0;

        if (Managers.Network.getGameMaxPlayer() == Players.Count)
            for (int i = 0; i < Players.Count; i++)
                if (Players[i].GetComponent<PlayerController>()._gameReady)
                    readyCount++;
        
        if (readyCount == Players.Count)
            return true;
        else
            return false;
    }
}
