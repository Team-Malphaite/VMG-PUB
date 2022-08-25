using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading.Tasks;


public class GoalController : MonoBehaviourPunCallbacks
{
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("@Player") == null) return;
    }

    // Update is called once per frame
    async void Update()
    {
        go = GameObject.Find("@Player");

        if (go.GetComponent<PhotonView>().IsMine)
        {
            if (go.GetComponent<PlayerController>()._goalCheck)
            {
            
                Debug.Log("골인");

                GameManagerEx.Instance.setGameFinished();
            
                if(go.GetComponent<PlayerController>().getRank() == 1){
                     go.GetComponent<PlayerController>().setRank(0);
                     Task k = tokenManager.Instance.gameReward(Metamask.Instance.walletAddress, "2");
                      await k;
                    }
            // if(PlayerController.Instance.getRank() == 1){
            //     tokenManager.Instance.gameReward(Metamask.Instance.walletAddress, "2");
            // }
            // go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
            // Managers.Network._scene = Define.Scene.Square;
            // PhotonNetwork.LeaveRoom();
            // Managers.Network.OnLeftRoom();
            // Managers.Scene.LoadScene(Define.Scene.Square);
            // Managers.Network.OnLogin();
             }
         }
    }

    private void OnTriggerEnter(Collider other) {
        // if (go.GetComponent<PhotonView>().IsMine)
        {
            go.GetComponent<PlayerController>()._goalCheck = true;
        }
    }
}
