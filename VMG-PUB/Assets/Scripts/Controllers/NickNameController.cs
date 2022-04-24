using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NickNameController : MonoBehaviourPunCallbacks
{
    GameObject player;
    public Text nicknameUI;
    void Start()
    {
        if (GameObject.Find("@Player") == null) return;
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            player = GameObject.Find("@Player");

            transform.rotation = Camera.main.transform.rotation;

            // if (player.GetComponent<PlayerController>()._mode == PlayerController.modeState.Game && !GameManagerEx.Instance.getGameStart())
            // {
            //     photonView.RPC("readyShow", RpcTarget.OthersBuffered, nick);
            //     // GetComponent<TextMesh>().text = nick;
            // }
            //     // if (player.GetComponent<PlayerController>()._gameReady) GetComponent<TextMesh>().text = nick + "\nready";
            //     // else GetComponent<TextMesh>().text = nick + "\nunready";
            // // else GetComponent<TextMesh>().text = nick;
            // else
            // {
            //     photonView.RPC("setNick", RpcTarget.OthersBuffered, nick);
            //     GetComponent<TextMesh>().text = nick;
            // }
        }

        if (player.GetComponent<PlayerController>()._mode == PlayerController.modeState.Game && !GameManagerEx.Instance.getGameStart())
            {
                photonView.RPC("readyShow", RpcTarget.AllBuffered);
                // GetComponent<TextMesh>().text = nick;
            }
                // if (player.GetComponent<PlayerController>()._gameReady) GetComponent<TextMesh>().text = nick + "\nready";
                // else GetComponent<TextMesh>().text = nick + "\nunready";
            // else GetComponent<TextMesh>().text = nick;
            else
            {
                // photonView.RPC("setNick", RpcTarget.OthersBuffered, nick);
                // GetComponent<TextMesh>().text = nick;
                nicknameUI.text = photonView.Owner.NickName;
            }
    }

    // [PunRPC]
    // void setNick(string nick)
    // {
    //     // GetComponent<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName;
    //     //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    //     this.nick = nick;
    //     GetComponent<TextMesh>().text = nick;
    // }

    [PunRPC]
    void readyShow()
    {
        if (player.GetComponent<PlayerController>()._gameReady) nicknameUI.text =  photonView.Owner.NickName + '\n' + "ready";
        else nicknameUI.text = photonView.Owner.NickName + '\n' + "unready";
    }
}


