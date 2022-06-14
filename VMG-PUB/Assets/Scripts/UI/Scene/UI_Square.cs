using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Square : UI_Scene
{
    enum Buttons
    {
        Game,
        Ranking,
        Explain,
        Logout,
        MusicOnOff,
    }

    enum Texts
    {
        Balance,
        MusicText,
        walletAddress,
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

        GetButton((int)Buttons.Game).gameObject.BindEvent(OnButtonClickedGame);
        GetButton((int)Buttons.Ranking).gameObject.BindEvent(OnButtonClickedRanking);
        GetButton((int)Buttons.Explain).gameObject.BindEvent(OnButtonClickedExplain);
        GetButton((int)Buttons.Logout).gameObject.BindEvent(OnButtonClickedLogout);
        GetButton((int)Buttons.MusicOnOff).gameObject.BindEvent(OnButtonClickedMusic);

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    // int _score = 0;

    public void OnButtonClickedRanking(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        string title = "게임 랭킹";
        string message = "랭킹 내용";
        Action okAction = () => Debug.Log("On Click Ok Button");

        PopupWindowController.Instance.ShowOk(title, message, okAction);
    }

    public void OnButtonClickedExplain(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        string title = "설명서";
        string message = "설명 내용";
        Action okAction = () => Debug.Log("On Click Ok Button");

        PopupWindowController.Instance.ShowOk(title, message, okAction);
    }

    public void OnButtonClickedLogout(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        string title = "로그아웃";
        string message = "로그아웃 하시겠습니까?";
        Action yesAction = () => Debug.Log("On Click Yes Button");
        Action noAction = () => Debug.Log("On Click No Button");

        PopupWindowController.Instance.ShowYesNo(title, message, yesAction, noAction);
    }

    public void OnButtonClickedGame(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        string title = "게임";
        string message = "게임에 입장하시겠습니까?";
        Action yesAction = () => Debug.Log("On Click Yes Button");
        Action noAction = () => Debug.Log("On Click No Button");

        PopupWindowController.Instance.ShowYesNoGame(title, message, yesAction, noAction);
    }

    public void OnButtonClickedMusic(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;

        if (GetText((int)Texts.MusicText).text == "음악끄기")
        {
            GetText((int)Texts.MusicText).text = "음악켜기";
            Camera.main.GetComponent<AudioSource>().Pause();
        }
        else
        {
            GetText((int)Texts.MusicText).text = "음악끄기";
            Camera.main.GetComponent<AudioSource>().Play();
        }
        
    }
    
}
