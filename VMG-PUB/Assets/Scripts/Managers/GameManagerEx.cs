using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManagerEx : MonoBehaviour, IPunObservable
{
    public static GameManagerEx Instance;
    [SerializeField]
    public List<GameObject> Players = new List<GameObject>();
    [SerializeField]
    bool allReady = false;
    [SerializeField]
    bool gameStart = false;
    bool isFinished = false;
    
    private void Awake()
    { 
        if (Instance == null) 
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("@Player") == null) return;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (!Players.Contains(player) && player.name != "Player")
            {
                Players.Add(player);
                Debug.Log("add player");
            }
        }

        if (isFinished) Debug.Log(isFinished);
    }

    public bool readyCheck()
    {
        if (Players.Count == 0) return false;
        int readyCount = 0;

        for (int j = 0; j < Players.Count; j++)
            if (Players[j] == null)
            {
                Players.RemoveAt(j);
                Debug.Log("null 제거");
            }

        if (Managers.Network.getGameMaxPlayer() == Players.Count)
            for (int i = 0; i < Players.Count; i++)
                if (Players[i].GetComponent<PlayerController>()._gameReady)
                    readyCount++;
        
        if (readyCount == Managers.Network.getGameMaxPlayer()) return true;
        else return false;
    }

    public void setAllReady()
    {
        allReady = true;
    }

    public void setAllReadyFalse()
    {
        allReady = true;
    }


    public bool getAllReady()
    {
        return allReady;
    }
    
    public void setGameStart()
    {
        gameStart = true;
    }

    public void setGameStartFalse()
    {
        gameStart = false;
    }

    public bool getGameStart()
    {
        return gameStart;
    }

    public void setGameFinished()
    {
        isFinished = true;
    }

    public bool getGameFinished()
    {
        return isFinished;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)        
    {
            if (stream.IsWriting)
            {             
                stream.SendNext(allReady);
                stream.SendNext(gameStart);
                stream.SendNext(isFinished);
                Debug.Log("동기화 전송");
            }
            else if (stream.IsReading)
            {          
                this.allReady = (bool)stream.ReceiveNext();
                this.gameStart = (bool)stream.ReceiveNext();
                this.isFinished = (bool)stream.ReceiveNext();
                Debug.Log("동기화 받음");
            }
    }
}
