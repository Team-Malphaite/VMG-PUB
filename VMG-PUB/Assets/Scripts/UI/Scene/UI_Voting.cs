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
        voteList,
        voteMake
    }

    enum Texts
    {
        Balance, Account
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

        GetButton((int)Buttons.voteList).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.voteMake).gameObject.BindEvent(OnButtonClicked);

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    // int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if(go.name.Equals("voteList"))
        {
            string title = "투표리스트";
            string message = "투표 출력";
            Action okAction = () => Debug.Log("On Click Ok Button");

            PopupWindowController.Instance.ShowOk(title, message, okAction);
        }
        if(go.name.Equals("voteMake"))
        {
            string title = "투표만들기";
            string message = "투표 만들기";
            Action okAction = () => Debug.Log("On Click Ok Button");

            PopupWindowController.Instance.ShowOk(title, message, okAction);
        }
        // _score++;
        // GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
        
    }
}
