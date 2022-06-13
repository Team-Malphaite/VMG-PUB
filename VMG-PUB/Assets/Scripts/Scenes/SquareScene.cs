using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SquareScene : BaseScene
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

        
        SceneType = Define.Scene.Square;
        cam.name = "@Main Camera";
        
        GameObject chat = null;
        GameObject metamask = null;
        // 초기화
        chat = GameObject.Find("@Chatting");
        metamask = GameObject.Find("@Metamask");

        if (chat == null)
        {
            chat = new GameObject { name = "@Chatting"};
            chat.AddComponent<ChatManager>();
            chat.AddComponent<PhotonView>();
        }
        
        if (metamask == null)
        {
            metamask = new GameObject { name = "@Metamask"};
            metamask.AddComponent<Metamask>();    
        }
        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Square>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
        Managers.UI.ShowPopupUI<UI_Chat>();
        Managers.UI.ShowPopupUI<UI_Loading>();
        DontDestroyOnLoad(metamask);
    }

    public override void Clear()
    {
        
    }
}
