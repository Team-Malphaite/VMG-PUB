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
       
        
        if(Managers.Scene._logincheck == true)
        {
                Managers.Scene.LoadScene(Define.Scene.Select);
          
        }
        
        //파이어베이스부분
        
    /*    
        if(Managers.Scene._logincheck == true)
        {
              //Managers.Scene.LoadScene(Define.Scene.Select);

            if(AuthHandler.Instance.charcter=="null"){
                Managers.Scene.LoadScene(Define.Scene.Select);

            }
            else{
                    Debug.Log("로그인한 사람 이름"+AuthHandler.Instance.name);

                 Managers.Scene.LoadScene(Define.Scene.Square);
                  Managers.Network.OnLogin();
  

            }
            
        }
        */
        
        
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear");
    }
}
