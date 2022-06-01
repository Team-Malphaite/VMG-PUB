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
            // Debug.Log("Can't find player");
            return;
        }
        go = GameObject.Find("@Player");

        // if (Managers.Scene._portalCheck)
        if (PlayerController.Instance._portalCheck)
        {
            if(go.GetComponent<PhotonView>().IsMine)
            {
                if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Square)
                {
                    Managers.Network._scene = Define.Scene.Voting;
                    go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Voting;
                    PhotonNetwork.LeaveRoom();
                    // Managers.Scene.LoadScene(Define.Scene.Voting);
                    // Managers.Network.OnLogin();
                    // PlayerController.Instance._portalCheck = false;
                    Managers.Network.OnLeftRoom();
                }

                // if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Square)
                // {
                //     _scene = Define.Scene.Game;
                //     go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Game;
                //     Managers.Network.LeaveRoom();
                //     Managers.Scene.LoadScene(Define.Scene.Game);
                //     Managers.Network.OnLogin();
                //     // Managers.Scene._portalCheck = false;
                //     PlayerController.Instance._portalCheck = false;
                // }

                else if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Voting)
                {
                    Managers.Network._scene = Define.Scene.Square;
                    go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                    PhotonNetwork.LeaveRoom();
                    // Managers.Scene.LoadScene(Define.Scene.Square);
                    // Managers.Network.OnLogin();
                    // PlayerController.Instance._portalCheck = false;
                    Managers.Network.OnLeftRoom();
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "@Player")
        {
            if (go.GetComponent<PhotonView>().IsMine)
            {
                if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Square)
                {
                    // if (Metamask.Instance._metamaskCheck == true) {
                        string title = "이동";
                        string message = "Voting Space로 이동하시겠습니까?";
                        Action yesAction = () => Debug.Log("On Click Portal Ok Button");
                        Action noAction = () => Debug.Log("On Click Portal No Button");
                        PopupWindowController.Instance.ShowYesNoPortal(title, message, yesAction, noAction);
                    // }else {
                    //     string title = "메타마스크 로그인";
                    //     string message = "Metamask 로그인";
                    //     Action yesAction = () => Debug.Log("On Click MetamaskConnect Ok Button");
                    //     Action noAction = () => Debug.Log("On Click MetamaskConnect No Button");
                    //     PopupWindowController.Instance.ShowYesNoMetamaskConnect(title, message, yesAction, noAction);
                    // }
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
    
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "@Player")
        {
            if (go.GetComponent<PhotonView>().IsMine)
                PopupWindowController.Instance.ClosePopupUI();
        }
    }
}
