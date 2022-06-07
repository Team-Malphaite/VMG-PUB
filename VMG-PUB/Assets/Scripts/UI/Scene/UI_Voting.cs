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
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button chooseButton1;
    public Button chooseButton2;
    public Button chooseButton3;
    public Button chooseButton4;
    public Button chooseButton5;
    public Button sendContract;
    public Text votingName;
    
    
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
        Btn1,
        Btn2,
        Btn3,
        chooseBtn1,
        chooseBtn2,
        chooseBtn3,
        chooseBtn4,
        chooseBtn5,
        sendContract
    }

    public enum Texts
    {
        walletAddress,
        MusicText,
        votingName
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
        chooseButton1.gameObject.SetActive(false);
        chooseButton2.gameObject.SetActive(false);
        chooseButton3.gameObject.SetActive(false);
        chooseButton4.gameObject.SetActive(false);
        chooseButton5.gameObject.SetActive(false);
        votingName.gameObject.SetActive(false);
        sendContract.gameObject.SetActive(false);

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

        // scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        

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
        btn1 = GetButton((int)Buttons.Btn1);
        btn2 = GetButton((int)Buttons.Btn2);
        btn3 = GetButton((int)Buttons.Btn3);
        votingName = GetText((int)Texts.votingName);
        chooseButton1 = GetButton((int)Buttons.chooseBtn1);
        chooseButton2 = GetButton((int)Buttons.chooseBtn2);
        chooseButton3 = GetButton((int)Buttons.chooseBtn3);
        chooseButton4 = GetButton((int)Buttons.chooseBtn4);
        chooseButton5 = GetButton((int)Buttons.chooseBtn5);
        sendContract = GetButton((int)Buttons.sendContract);
        
        

        
        //test1 = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //voteListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        GetButton((int)Buttons.voteList).gameObject.BindEvent(OnButtonListClicked);
        GetButton((int)Buttons.voteMake).gameObject.BindEvent(OnButtonMakeClicked);
        GetButton((int)Buttons.makeButton).gameObject.BindEvent(OnMakeButtonCliked);
        GetButton((int)Buttons.MusicOnOff).gameObject.BindEvent(OnButtonClickedMusic);
        GetButton((int)Buttons.Btn1).gameObject.BindEvent(OnBtn1ChooseClicked);
        

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

        // db에서 불러와서 출력 
        GameObject go = EventSystem.current.currentSelectedGameObject;

        ////////////////파이어 베이스부분
        //AuthHandler.Instance.voteSubjectData.Clear(); // 데이터 받아오기전 그전 데이터 남아있을수도있으니 삭제
       // AuthHandler.Instance.GetAllVoteDocument(); // 데이터 받아서 votesubjectData에 저장



        // db에서 받아온 이름들을 배열에 집어넣고 배열length만큼 버튼 동적 생성 addnewobject() 함수사용 동적으로 힐당된 버튼들에 이름배열에

        

        if(voteListView.gameObject.activeSelf == true)
        {
            voteListView.gameObject.SetActive(false);
            btn1.gameObject.SetActive(false);
            btn2.gameObject.SetActive(false);
            btn3.gameObject.SetActive(false);
        }
        else{
            voteListView.gameObject.SetActive(true);
            btn1.gameObject.SetActive(true);
            btn2.gameObject.SetActive(true);
            btn3.gameObject.SetActive(true);
        }



        //  AddNewUiObject(); -> 버튼 ui
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
    public void OnBtn1ChooseClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        // 투표용지를 여는 코드 
        if(btn1.gameObject.activeSelf == true)
        {
            chooseButton1.gameObject.SetActive(true);
            chooseButton2.gameObject.SetActive(true);
            chooseButton3.gameObject.SetActive(true);
            chooseButton4.gameObject.SetActive(true);
            chooseButton5.gameObject.SetActive(true);
            votingName.gameObject.SetActive(true);
            sendContract.gameObject.SetActive(true);
            
            
        }
        else{
            chooseButton1.gameObject.SetActive(false);
            chooseButton2.gameObject.SetActive(false);
            chooseButton3.gameObject.SetActive(false);
            chooseButton4.gameObject.SetActive(false);
            chooseButton5.gameObject.SetActive(false);
            votingName.gameObject.SetActive(false);
            sendContract.gameObject.SetActive(false);    
        }

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
