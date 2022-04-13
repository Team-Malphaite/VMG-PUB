using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NickNameController : MonoBehaviourPunCallbacks
{
    GameObject player;
    void Start()
    {
        if (GameObject.Find("@Player") == null) return;
    }
    void Update()
    {
        player = GameObject.Find("@Player");
        transform.rotation = Camera.main.transform.rotation;

        if (player.GetComponent<PlayerController>()._mode == PlayerController.modeState.Game && !GameManagerEx.Instance.getGameStart())
            photonView.RPC("readyShow", RpcTarget.All);
        else photonView.RPC("setNick", RpcTarget.All);
    }

    [PunRPC]
    void setNick()
    {
        GetComponent<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    void readyShow()
    {
        if (player.GetComponent<PlayerController>()._gameReady) gameObject.GetComponent<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName + '\n' + "ready";
        else GetComponent<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName + '\n' + "unready";
    }
}
