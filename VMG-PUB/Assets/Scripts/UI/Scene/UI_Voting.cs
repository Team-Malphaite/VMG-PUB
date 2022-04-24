using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Voting : UI_Scene
{
    // enum voteListViews
    // {
    //     scrollListView
    // }
    //public ScrollRect scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
    public ScrollRect voteListView;
    public InputField voteNames;
    public InputField voteFirsts;
    public InputField voteSeconds;
    public InputField voteThirds;
    public InputField voteFourths;
    public InputField voteFifths;
    public Button makeButtons;

    
    public enum ScrollRects
    {
        scrollListView,
    }

    public enum Buttons
    {
        voteList,
        voteMake,
        makeButton
    }

    public enum Texts
    {
        Balance,
        Account,

    }

    public enum InputFields
    {
        voteName,
        voteFirst,
        voteSecond,
        voteThird,
        voteFourth,
        voteFifth,
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

        voteListView.gameObject.SetActive(false);
        voteNames.gameObject.SetActive(false);
        voteFirsts.gameObject.SetActive(false);
        voteSeconds.gameObject.SetActive(false);
        voteThirds.gameObject.SetActive(false);
        voteFourths.gameObject.SetActive(false);
        voteFifths.gameObject.SetActive(false);
        makeButtons.gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<ScrollRect>(typeof(ScrollRects));
        Bind<InputField>(typeof(InputFields));

        //scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        

        //Image presentStageImage = scrollListView.content.GetChild(i).GetComponent<Image>();
        //나중에 scrollView안에 컨텐츠 접근할때 사용
        
        // Bind<GameObject>(typeof(GameObjects));
        // Bind<Image>(typeof(Images));

        voteListView = GetScrollRect((int)ScrollRects.scrollListView);
        voteNames = GetInputField((int)InputFields.voteName);
        voteFirsts = GetInputField((int)InputFields.voteFirst);
        voteSeconds = GetInputField((int)InputFields.voteSecond);
        voteThirds = GetInputField((int)InputFields.voteThird);
        voteFourths = GetInputField((int)InputFields.voteFourth);
        voteFifths = GetInputField((int)InputFields.voteFifth);
        makeButtons = GetButton((int)Buttons.makeButton);
        //test1 = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //voteListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        GetButton((int)Buttons.voteList).gameObject.BindEvent(OnButtonListClicked);
        GetButton((int)Buttons.voteMake).gameObject.BindEvent(OnButtonMakeClicked);
        GetButton((int)Buttons.makeButton).gameObject.BindEvent(OnMakeButtonCliked);
        

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    // int _score = 0;

    public void OnButtonListClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        if(voteListView.gameObject.activeSelf == true)
        {
            voteListView.gameObject.SetActive(false);
        }
        else{
            voteListView.gameObject.SetActive(true);
        }
        // GameObject go = EventSystem.current.currentSelectedGameObject;
        // if(go.name.Equals("voteList"))
        // {
        //     string title = "투표리스트";
        //     string message = "투표 출력";
        //     Action okAction = () => Debug.Log("On Click Ok Button");

        //     PopupWindowController.Instance.ShowOk(title, message, okAction);
        // }
        // if(go.name.Equals("voteMake"))
        // {
        //     string title = "투표만들기";
        //     string message = "투표 만들기";
        //     Action okAction = () => Debug.Log("On Click Ok Button");

        //     PopupWindowController.Instance.ShowOk(title, message, okAction);
        // }
        // _score++;
        // GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
        
    }
    public void OnButtonMakeClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        // if(go.name.Equals("voteMake")){
        //     string title = "투표만들기";
        //     string message = "투표 만들기";
        //     Action okAction = () => Debug.Log("On Click Ok Button");

        //     PopupWindowController.Instance.ShowOk(title, message, okAction);
        // }

        if(voteNames.gameObject.activeSelf == true)
        {
            voteNames.gameObject.SetActive(false);
            voteFirsts.gameObject.SetActive(false);
            voteSeconds.gameObject.SetActive(false);
            voteThirds.gameObject.SetActive(false);
            voteFourths.gameObject.SetActive(false);
            voteFifths.gameObject.SetActive(false);
            makeButtons.gameObject.SetActive(false);
        }
        else{
            voteNames.gameObject.SetActive(true);
            voteFirsts.gameObject.SetActive(true);
            voteSeconds.gameObject.SetActive(true);
            voteThirds.gameObject.SetActive(true);
            voteFourths.gameObject.SetActive(true);
            voteFifths.gameObject.SetActive(true);
            makeButtons.gameObject.SetActive(true);
        }
        
        
    }
    public void OnMakeButtonCliked(PointerEventData data)
    {
        string name = voteNames.text;
        string first = voteFirsts.text;
        string second = voteSeconds.text;    
        string third = voteThirds.text;
        string fourth = voteFourths.text;
        string fifth = voteFifths.text;
        Debug.Log(name+first+second+third+fourth+fifth);
    }
}
