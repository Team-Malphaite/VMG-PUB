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

            AuthHandler firebaseHelper = GameObject.Find("firebaseHandler").GetComponent<AuthHandler>();
            if(firebaseHelper !=null){
                Debug.Log("오브젝트 찾음");
            }
            Action okAction = () => Debug.Log("On Click Login Ok Button");

            /*firebaseHelper.SignInWithGoogle(); //파이어베이스 로그인 실행함수
            Action okAction = () => Debug.Log("On Click Login Ok Button");

            firebaseHelper.GetUserAuthDataEmail(); // 플레이어 이메일 데이터 가져오기
            firebaseHelper.GetDocument(); // 플레이어의 이메일 데이터를 바탕으로 플레이어가 첫로그인인지 아닌지 판별하기 위해 데이터베이스를 읽어온다.


            if(firebaseHelper.playerDataBuffer == "null"){// 첫로그인일시 null
                firebaseHelper.SetDocument(); // 아직 플레이어가 캐릭터선택을 안했을때 이 문장이 실행된다 . 여기에 캐릭터선택창 이동 부분구현하면된다.
            }
        */  
            PopupWindowController.Instance.ShowOkLogin(title, message, okAction);

           /* if(firebaseHelper.statusText =="Success: signed in with Google!")
            {   문제 : 처음에 파이어베이스 팝업창이 뜨고 로그인을 하면 statusText가 sucess되는데 팝업창이랑 유니티랑 별게의 존재여서
                버튼을 한번더 눌러야 팝업창이뜸 (코드가 바로 실행되기때문 ) - 즉, sleep을 주던지 다른 방법을 찾아야함


                PopupWindowController.Instance.ShowOkLogin(title, message, okAction);


            }*/


        }
    }
}
