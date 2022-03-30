using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;
        Managers.UI.ShowSceneUI<UI_Login>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        if(Managers.Scene._logincheck == true)
        {
            Managers.Scene.LoadScene(Define.Scene.Square);
            GameObject net = GameObject.Find("@Network");
            GameObject chat = GameObject.Find("@Chatting");
            if (net == null)
            {
                net = new GameObject {name = "@Network"};
                net.AddComponent<NetworkManager>();
            }
            if (chat == null){
                chat = new GameObject {name = "@Chatting"};
                chat.AddComponent<ChatManager>();
            }
            DontDestroyOnLoad(net);
            DontDestroyOnLoad(chat);
            Managers.Network.OnLogin();
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear");
    }
}
