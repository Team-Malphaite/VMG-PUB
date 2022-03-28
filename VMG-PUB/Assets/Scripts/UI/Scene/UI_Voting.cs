using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Voting : UI_Scene
{
    enum Buttons
    {
        Ranking,
        Explain,
        Logout,
    }

    enum Texts
    {
        Balance,
    }

    // enum GameObjects
    // {
    //     TestObject,
    // }

    // enum Images
    // {
    //     ItemIcon,
    // }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        // Bind<GameObject>(typeof(GameObjects));
        // Bind<Image>(typeof(Images));

        GetButton((int)Buttons.Ranking).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.Explain).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.Logout).gameObject.BindEvent(OnButtonClicked);

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    // int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if(go.name.Equals("Ranking"))
        {
            string title = "게임 랭킹";
            string message = "랭킹 내용";
            Action okAction = () => Debug.Log("On Click Ok Button");

            PopupWindowController.Instance.ShowOk(title, message, okAction);
        }
        if(go.name.Equals("Explain"))
        {
            string title = "설명서";
            string message = "설명 내용";
            Action okAction = () => Debug.Log("On Click Ok Button");

            PopupWindowController.Instance.ShowOk(title, message, okAction);
        }
        if(go.name.Equals("Logout"))
        {
            string title = "로그아웃";
            string message = "로그아웃 하시겠습니까?";
            Action yesAction = () => Debug.Log("On Click Yes Button");
            Action noAction = () => Debug.Log("On Click No Button");

            PopupWindowController.Instance.ShowYesNo(title, message, yesAction, noAction);
        }
        // _score++;
        // GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
        
    }
}
