using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Login : UI_Scene
{
   // public static FirebaseWebGL.Examples.Auth.AuthHandler instance;
    enum Buttons
    {
        LoginButton,
    }

    // enum GameObjects
    // {
    //     TestObject,
    // }

    enum Images
    {
        LoginImage,
    }

    private void Start()
    {
        Init();
    }
    //파이어베이스 부분
  /*
    private void Update() {
        if(AuthHandler.Instance.statusText =="Success: signed in with Google!")
            {   

              string title = "로그인 통과";
              string message = "로그인 체크";
              Action okAction = () => Debug.Log("On Click Login Ok Button");
              AuthHandler.Instance.GetUserAuthDataEmail();
              AuthHandler.Instance.GetDocument();
              Managers.Network.NickName = AuthHandler.Instance.name;

              Debug.Log("On Click Login Ok Button 어스 핸들러의 캐릭터값 = "+ AuthHandler.Instance.charcter);
             
              PopupWindowController.Instance.ShowOkLogin(title, message, okAction);


            }
            

    }
    */
    
    
  

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        // Bind<GameObject>(typeof(GameObjects));
        // Bind<Image>(typeof(Images));

        GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnButtonClicked);

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if(go.name.Equals("LoginButton"))
        {
            
           string title = "로그인 통과";
              string message = "로그인 체크";
              Action okAction = () => Debug.Log("On Click Login Ok Button");
               PopupWindowController.Instance.ShowOkLogin(title, message, okAction);
               
            
   //파이어베이스 부분
           //AuthHandler.Instance.SignInWithGoogle();
            


        }
    }
}
