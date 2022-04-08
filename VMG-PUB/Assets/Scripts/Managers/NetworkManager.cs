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
            // PhotonNetwork.JoinRandomOrCreateRoom("Game", new RoomOptions{MaxPlayers = 3}, null);
            JoinGameRoom();
            Debug.Log("Joined Game !!!");
            Debug.Log(_scene);
        }
    }

    public void JoinGameRoom(){
        Debug.Log("Random Match Start!!!");
        

        // 해쉬테이블 값 지정
        byte maxPlayers = 4; // MAXPLAYER 지정
        int maxTime = 120;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers; // 인원 지정.
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "maxTime", maxTime } }; // 게임 시간 지정.
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "maxTime" }; // 여기에 키 값을 등록해야, 필터링이 가능하다.

        // 방 참가를 시도하고, 실패하면 생성해서 참가함.
        PhotonNetwork.JoinRandomOrCreateRoom(
            expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "maxTime", maxTime } }, expectedMaxPlayers: maxPlayers, // 참가할 때의 기준.
            roomOptions: roomOptions // 생성할 때의 기준.
        );
    }
    public override void OnJoinedRoom()
    {
        // base.OnJoinedRoom();
        Debug.Log("Joined Room");
        if (player == null)
        {//파이어베이스 할때 이거 활성화
          /* AuthHandler.Instance.GetDocument();//데베 읽어와서 캐릭터 정보 받아서 그것을 입력
           Debug.Log("AuthHandler.Instance.charcter ="+ AuthHandler.Instance.charcter) ;
           string buffer = string.Join("" , AuthHandler.Instance.charcter.Split('"'));
           Debug.Log("문자열 처리한 뒤에 AuthHandler.Instance.charcter ="+ buffer) ;
            player = PhotonNetwork.Instantiate("Prefabs/Character/" + AuthHandler.Instance.charcter, new Vector3(0, 0, -5), Quaternion.identity);
*/
            player = PhotonNetwork.Instantiate("Prefabs/Character/" +  UI_SelectInfoInput.Instance.selectCharacterName, new Vector3(0, 0, -5), Quaternion.identity);
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
