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
    bool musicChange = true;
    bool gameOver = false;
    int StartTimer = 0;
    int FinishTimer = 0;

    float m_TotalSeconds = 1 * 60;
    
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

        if (getGameStart() && musicChange)
        {
            Camera.main.GetComponent<AudioSource>().clip = Managers.Resource.Load<AudioClip>("BGM/Upbeat-Forever");
            Camera.main.GetComponent<AudioSource>().loop = true;
            Camera.main.GetComponent<AudioSource>().Play();
            musicChange = false;
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

    public void setGameOver()
    {
        gameOver = true;
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public float getTotalSecond()
    {
        return m_TotalSeconds;
    }

    public void setMinusTotalSecond(float time)
    {
        m_TotalSeconds -= time;
    }

    public void setZeroTotalSecond()
    {
        m_TotalSeconds = 0;
    }

    public void setStarterTimerPlus()
    {
        StartTimer ++;
    }

    public int getStarterTimer()
    {
        return StartTimer;
    }

    public void setFinishTimerPlus()
    {
        FinishTimer ++;
    }

    public int getFinishTimer()
    {
        return FinishTimer;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)        
    {
            if (stream.IsWriting)
            {             
                stream.SendNext(allReady);
                stream.SendNext(gameStart);
                stream.SendNext(isFinished);
                stream.SendNext(m_TotalSeconds);
                stream.SendNext(StartTimer);
                stream.SendNext(FinishTimer);
                Debug.Log("동기화 전송");
            }
            else if (stream.IsReading)
            {          
                this.allReady = (bool)stream.ReceiveNext();
                this.gameStart = (bool)stream.ReceiveNext();
                this.isFinished = (bool)stream.ReceiveNext();
                this.m_TotalSeconds = (float)stream.ReceiveNext();
                this.StartTimer = (int)stream.ReceiveNext();
                this.FinishTimer = (int)stream.ReceiveNext();
                Debug.Log("동기화 받음");
            }
    }
}
