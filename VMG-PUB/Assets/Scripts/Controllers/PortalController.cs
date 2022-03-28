using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class PortalController : MonoBehaviourPunCallbacks
{
    public GameObject go;
    Define.Scene _scene = Define.Scene.Voting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("@Player") == null)
        {
            Debug.Log("Can't find player");
            return;
        }
        go = GameObject.Find("@Player");

        if (Managers.Scene._portalCheck)
        {
            if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Square)
            {
                _scene = Define.Scene.Voting;
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 3; // 인원 지정.
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "Mode" }; // 여기에 키 값을 등록해야, 참가할 때 필터링이 가능하다.
                
                go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Voting;
                // Managers.Scene.LoadScene(Define.Scene.Voting);
                Managers.Scene._portalCheck = false;

                Debug.Log(PhotonNetwork.PlayerList.Length);

                if (PhotonNetwork.PlayerList.Length == 1)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } });
                    Managers.Scene.LoadScene(Define.Scene.Voting);
                }
                else
                {
                    PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } }, expectedMaxPlayers: 3, roomOptions: roomOptions);
                    Debug.Log("Connected !!!");
                    Debug.Log(_scene);
                }
            }

            else if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Voting)
            {
                _scene = Define.Scene.Square;
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 3; // 인원 지정.
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "Mode" }; // 여기에 키 값을 등록해야, 참가할 때 필터링이 가능하다.

                go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                // Managers.Scene.LoadScene(Define.Scene.Square);
                Managers.Scene._portalCheck = false;

                if (PhotonNetwork.PlayerList.Length == 1)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } });
                    Managers.Scene.LoadScene(Define.Scene.Square);
                }
                else
                {
                    PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties: new ExitGames.Client.Photon.Hashtable() { { "Mode", _scene } }, expectedMaxPlayers: 3, roomOptions: roomOptions);
                    Debug.Log("Connected !!!");
                    Debug.Log(_scene);
                }
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            if (go.GetComponent<PhotonView>().IsMine)
            {
                if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Square)
                {
                    string title = "이동";
                    string message = "Voting Space로 이동하시겠습니까?";
                    Action yesAction = () => Debug.Log("On Click Portal Ok Button");
                    Action noAction = () => Debug.Log("On Click Portal No Button");
                    PopupWindowController.Instance.ShowYesNoPortal(title, message, yesAction, noAction); 
                }
                if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Voting)
                {
                    string title = "이동";
                    string message = "Square Space로 이동하시겠습니까?";
                    Action yesAction = () => Debug.Log("On Click Portal Ok Button");
                    Action noAction = () => Debug.Log("On Click Portal No Button");
                    PopupWindowController.Instance.ShowYesNoPortal(title, message, yesAction, noAction);
                }
            }

            else return;
            
        }
    }
}
