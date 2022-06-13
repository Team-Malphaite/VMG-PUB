using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VotingScene : BaseScene
{
    GameObject cam;
    protected override void Init()
    {
        base.Init();
        cam = GameObject.Find("@Main Camera");
        if (cam == null)
        {
            cam = Managers.Resource.Instantiate("Camera/Main Camera");
            Debug.Log("make cam");
        }

        GameObject chat = null;
        chat = GameObject.Find("@Chatting");

        if (chat == null)
        {
            chat = new GameObject { name = "@Chatting"};
            chat.AddComponent<ChatManager>();
            chat.AddComponent<PhotonView>();
        }

        SceneType = Define.Scene.Voting;

        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Voting>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
        Managers.UI.ShowPopupUI<UI_Chat>();
    }

    public override void Clear()
    {
        
    }
}
