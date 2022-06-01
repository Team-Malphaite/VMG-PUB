using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Voting : UI_Scene
{
    public static UI_Voting Instance = null;
    // enum voteListViews
    // {
    //     scrollListView
    // }
    //public ScrollRect scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
    public ScrollRect voteListView;
    public float space = 50f;
    public GameObject uiPrefab;
    public List<RectTransform> uiObjects = new List<RectTransform>();
    public InputField voteNames;
    public InputField voteFirsts;
    public InputField voteSeconds;
    public InputField voteThirds;
    public InputField voteFourths;
    public InputField voteFifths;
    public Button makeButtons;
    public Text walletAddress;
    
    public enum ScrollRects
    {
        scrollListView,
    }

    public enum Buttons
    {
        voteList,
        voteMake,
        makeButton,
        MusicOnOff,
    }

    public enum Texts
    {
        walletAddress,
        MusicText,
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

        // voteListView = GetComponent<ScrollRect>();
    }

    private void Awake() {
        Instance = this;
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
        walletAddress = GetText((int)Texts.walletAddress);
        //test1 = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //voteListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        GetButton((int)Buttons.voteList).gameObject.BindEvent(OnButtonListClicked);
        GetButton((int)Buttons.voteMake).gameObject.BindEvent(OnButtonMakeClicked);
        GetButton((int)Buttons.makeButton).gameObject.BindEvent(OnMakeButtonCliked);
        GetButton((int)Buttons.MusicOnOff).gameObject.BindEvent(OnButtonClickedMusic);
        

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    private void Update()
    {
        //setWalletAddress(walletAddress.text);
    }

    // int _score = 0;

    public void OnButtonListClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;




        ////////////////파이어 베이스부분
        //AuthHandler.Instance.voteSubjectData.Clear(); // 데이터 받아오기전 그전 데이터 남아있을수도있으니 삭제
       // AuthHandler.Instance.GetAllVoteDocument(); // 데이터 받아서 votesubjectData에 저장





        
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





        ///파이어베이스부분 vote db에 쓰기 
        /*
        AuthHandler.Instance.voteSubject=voteNames.text;
        AuthHandler.Instance.vote1 = voteFirsts.text;
        AuthHandler.Instance.vote2= voteSeconds.text;
        AuthHandler.Instance.vote3= voteThirds.text;
        AuthHandler.Instance.vote4= voteFourths.text;
        AuthHandler.Instance.vote5= voteFifths.text;
        AuthHandler.Instance.SetVoteDocument();*/







        // 만들기 버튼 클릭시 list에 출력 가능. 
        // db에 각각의 데이터 저장 

        // 4개투표를 -> list -> list길이를 받아서 그것만큼 버튼생성해주는거지 
        // 데이터별로 (1번 -> 6개) 
        // 투표테이블 -> 이름 == 이름  -> 1번 투표에대한 내용을 호출 >> 

    }

    public void AddNewUiObject()
    {
        var newUi = Instantiate(uiPrefab, voteListView.content).GetComponent<RectTransform>();
        uiObjects.Add(newUi);

        float y = 0f;
        for(int i =0; i< uiObjects.Count;i++)
        {
            uiObjects[i].anchoredPosition = new Vector2(0f,-y);
            y += uiObjects[i].sizeDelta.y + space;
        }
        voteListView.content.sizeDelta = new Vector2(voteListView.content.sizeDelta.x,y);
    }

    public void setWalletAddress(string account)
    {
        walletAddress.text = account;
        Debug.Log(account);
        // if(Metamask.Instance._metamaskCheck == true)
        // {
        //     walletAddress.text = account;
        //     Debug.Log(account);
        // }
        // else
        // {
        //     walletAddress.text = "0x00000000000000000000000000000000000000";
        //     // Metamask.Instance.setcheckmetamask(true);
        // }
        
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
