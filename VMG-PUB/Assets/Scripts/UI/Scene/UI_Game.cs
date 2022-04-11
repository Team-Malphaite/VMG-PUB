using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Photon.Pun;

public class UI_Game : UI_Scene
{
    enum Buttons
    {
        Ready,
        Exit,
    }

    // enum GameObjects
    // {
    //     TestObject,
    // }

    // enum Images
    // {
    //     ItemIcon,
    // }

    enum Texts
    {
        ReadyButtonText,
    }

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

        GetButton((int)Buttons.Ready).gameObject.BindEvent(OnButtonClickedReady);
        GetButton((int)Buttons.Exit).gameObject.BindEvent(OnButtonClickedExit);

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    // int _score = 0;

    public void OnButtonClickedReady(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;

        if (GetText((int)Texts.ReadyButtonText).text == "준비")
        {
            Debug.Log("준비 버튼 클릭");
            GetText((int)Texts.ReadyButtonText).text = "준비 완료";
            PlayerController.Instance._gameReady = true;
        }
        else if (GetText((int)Texts.ReadyButtonText).text == "준비 완료")
        {
            Debug.Log("준비 완료 버튼 클릭");
            GetText((int)Texts.ReadyButtonText).text = "준비";
            PlayerController.Instance._gameReady = false;
        }
        // _score++;
        // GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
        
    }

    public void OnButtonClickedExit(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        string title = "게임 나가기";
        string message = "게임을 나가시겠습니까?";
        Action yesAction = () => Debug.Log("On Click Yes Button");
        Action noAction = () => Debug.Log("On Click No Button");

        PopupWindowController.Instance.ShowYesNoGameExit(title, message, yesAction, noAction);
    }
}
