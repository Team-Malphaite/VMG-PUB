using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string NickName = "test1";
    public string gameVersion = "1.0";
    GameObject player = null;

    Define.Scene _scene = Define.Scene.Square;
    
    // void Awake()
    // {
    //      PhotonNetwork.AutomaticallySyncScene = true;
    // }

    void Start()
    {

    }

    void Update() {
        player = GameObject.Find("@Player");
    }

    public void OnLogin()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = this.NickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (SceneManager.GetActiveScene().name == "Square")
        {
            _scene = Define.Scene.Square;
            PhotonNetwork.JoinOrCreateRoom("Square", new RoomOptions{MaxPlayers = 3}, null);
            Debug.Log("Joined Square !!!");
            Debug.Log(_scene);
        }
        else if (SceneManager.GetActiveScene().name == "Voting")
        {
            _scene = Define.Scene.Voting;
            PhotonNetwork.JoinOrCreateRoom("Voting", new RoomOptions{MaxPlayers = 3}, null);
            Debug.Log("Joined Voting !!!");
            Debug.Log(_scene);
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            _scene = Define.Scene.Game;
            PhotonNetwork.JoinOrCreateRoom("Game", new RoomOptions{MaxPlayers = 3}, null);
            Debug.Log("Joined Game !!!");
            Debug.Log(_scene);
        }
    }

    public override void OnJoinedRoom()
    {
        // base.OnJoinedRoom();
        Debug.Log("Joined Room");
        if (player == null)
        {
            player = PhotonNetwork.Instantiate("Prefabs/Character/TestCharacter", new Vector3(0, 0, -5), Quaternion.identity);
            if(SceneManager.GetActiveScene().name == "Game")
                player.AddComponent<RespawnController>();
            DontDestroyOnLoad(player);
        }
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Leave Room");
        PhotonNetwork.JoinLobby();
    }
}
