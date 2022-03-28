using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string NickName = "test1";
    public string gameVersion = "1.0";
    GameObject player;

    Define.Scene _scene = Define.Scene.Square;
    
    // void Awake()
    // {
    //      PhotonNetwork.AutomaticallySyncScene = true;
    // }

    void Start()
    {
        player = GameObject.Find("@Player");
    }

    public void OnLogin()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = this.NickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // base.OnConnectedToMaster();
        // RoomOptions roomOptions = new RoomOptions();
        // roomOptions.MaxPlayers = 3; // 인원 지정.
        // roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } }; // 게임 시간 지정.
        // roomOptions.CustomRoomPropertiesForLobby = new string[] { "Mode" }; // 여기에 키 값을 등록해야, 참가할 때 필터링이 가능하다.
        
        // PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } }, expectedMaxPlayers: 3, roomOptions: roomOptions);
        if(player == null || player.GetComponent<PlayerController>()._mode == PlayerController.modeState.Voting)
        {
            PhotonNetwork.JoinOrCreateRoom("Voting", new RoomOptions{MaxPlayers = 3}, null);
            Debug.Log("Connected !!!");
            Debug.Log(_scene);
        }
        else if(player.GetComponent<PlayerController>()._mode == PlayerController.modeState.Square)
        {
            PhotonNetwork.JoinOrCreateRoom("Square", new RoomOptions{MaxPlayers = 3}, null);
            Debug.Log("Connected !!!");
            Debug.Log(_scene);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Failed join room!!!");
        // this.CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // base.OnJoinedRoom();
        Debug.Log("Joined Room");
        GameObject go = PhotonNetwork.Instantiate("Prefabs/Character/TestCharacter", new Vector3(0, 0, -5), Quaternion.identity);
        DontDestroyOnLoad(go);
    }

    // void CreateRoom()
    // {
    //     PhotonNetwork.CreateRoom("Lobby", new RoomOptions { MaxPlayers = 3});
    //     Debug.Log("Create");
    // }
}
