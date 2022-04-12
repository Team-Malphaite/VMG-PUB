using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // 초기화
        chat = GameObject.Find("@Chatting");

        if (chat == null)
        {
            chat = new GameObject { name = "@Chatting"};
            chat.AddComponent<ChatManager>();
        }
        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Square>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
        Managers.UI.ShowPopupUI<UI_Chat>();
        
        // DontDestroyOnLoad(Managers.UI.ShowPopupUI<PopupWindowController>());
    }

    public override void Clear()
    {
        
    }
}
