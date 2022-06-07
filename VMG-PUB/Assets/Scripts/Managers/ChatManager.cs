using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public static ChatManager Instance;
    // public Button sendBtn;
    // public Text chatLog;
    // public Text chattingList;
    // public InputField input;
    // ScrollRect scroll_rect = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }
    
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        // UI_Chat.uiinstance.scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
        //if(GameObject.Find("scrollRect") == null) return;
        //UI_Chat.Instance.scrolls = GameObject.FindObjectOfType<ScrollRect>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //UI_Chat.Instance.scrolls = GameObject.Find("scrollRect");
        // if (Input.GetKeyDown(KeyCode.Return) && !UI_Chat.uiinstance.input.isFocused)
        //     UI_Chat.uiinstance.SendButtonOnclicked();
        // Debug.Log(UI_Chat.Instance.inputs.isFocused);
        if (Input.GetKeyDown(KeyCode.Return) && !UI_Chat.Instance.inputs.isFocused && UI_Chat.Instance.inputs.text != "")
            chatUpdate();
    }

    // [PunRPC]
    // public void ReceiveMsg(string msg){
    //     UI_Chat.uiinstance.text += "\n" + msg;
    //     UI_Chat.uiinstance.scroll_rect.verticalNormalizedPosition = 0.0f;
    // }

    public void chatUpdate(){
        //string msg = string.Format("[{0}] {1}", AuthHandler.Instance.name ,UI_Chat.Instance.inputs.text); //파이어 베이스 부분
        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName,UI_Chat.Instance.inputs.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        UI_Chat.Instance.inputs.ActivateInputField();
        UI_Chat.Instance.inputs.text = "";
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        UI_Chat.Instance.logs.text += "\n" + msg;
        UI_Chat.Instance.scrolls.verticalNormalizedPosition = 0.0f;
    }
}