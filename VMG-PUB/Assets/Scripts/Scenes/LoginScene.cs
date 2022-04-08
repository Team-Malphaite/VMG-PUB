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
        // if(Managers.Scene._logincheck == true)
        // {
        //     Managers.Scene.LoadScene(Define.Scene.Square);
        //     GameObject net = GameObject.Find("@Network");
        //     if (net == null)
        //     {
        //         net = new GameObject {name = "@Network"};
        //         net.AddComponent<NetworkManager>();
        //     }
        //     DontDestroyOnLoad(net);
        //     Managers.Network.OnLogin();
        // }
        if(Managers.Scene._logincheck == true)
        {
              //Managers.Scene.LoadScene(Define.Scene.Select);

            if(AuthHandler.Instance.playerDataBuffer=="null"){
                Managers.Scene.LoadScene(Define.Scene.Select);

            }
            else{
                 Debug.Log("현재 플레이어데이터버퍼 값 ="+AuthHandler.Instance.playerDataBuffer);

            }
            
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear");
    }
}
