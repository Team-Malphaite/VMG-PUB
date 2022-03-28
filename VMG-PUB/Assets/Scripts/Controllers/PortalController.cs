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
                go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Voting;
                OnLeftRoom();
                Managers.Scene.LoadScene(Define.Scene.Voting);
                Managers.Network.OnLogin();
                Managers.Scene._portalCheck = false;

                Debug.Log(PhotonNetwork.PlayerList.Length);
            }

            else if (go.GetComponent<PlayerController>()._mode == PlayerController.modeState.Voting)
            {
                _scene = Define.Scene.Square;
                go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                OnLeftRoom();
                Managers.Scene.LoadScene(Define.Scene.Square);
                Managers.Network.OnLogin();
                Managers.Scene._portalCheck = false;

                Debug.Log(PhotonNetwork.PlayerList.Length);
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
