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

            if (player.GetComponent<PlayerController>()._mode == PlayerController.modeState.Game)
            {
                if (!GameManagerEx.Instance.getGameStart())
                    if (player.GetComponent<PlayerController>()._gameReady)
                        photonView.RPC("readyShowReady", RpcTarget.AllBuffered);
                    else
                        photonView.RPC("readyShowUnready", RpcTarget.AllBuffered);
                else
                    photonView.RPC("setNick", RpcTarget.AllBuffered);
            }
            else
            {
                photonView.RPC("setNick", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    void setNick()
    {
        // GetComponent<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName;
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        nicknameUI.text = photonView.Owner.NickName;
    }

    [PunRPC]
    void readyShowReady()
    {
        nicknameUI.text = photonView.Owner.NickName + '\n' + "ready";
    }

    [PunRPC]
    void readyShowUnready()
    {
        nicknameUI.text =  photonView.Owner.NickName + '\n' + "unready";
    }
}


