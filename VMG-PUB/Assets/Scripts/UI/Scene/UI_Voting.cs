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

    public Button chooseButton1;
    public Button chooseButton2;
    public Button chooseButton3;
    public Button chooseButton4;
    public Button chooseButton5;
    public Button sendContract;
    public Button closeBtn;

    public Text votingName;
    public Text chBtn1T;
    public Text chBtn2T;
    public Text chBtn3T;
    public Text chBtn4T;
    public Text chBtn5T;
    public string tmp=null;
    public int btnlength=3;


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
        chooseBtn1,
        chooseBtn2,
        chooseBtn3,
        chooseBtn4,
        chooseBtn5,
        sendContract,
        closeBtn
    }

    public enum Texts
    {
        walletAddress,
        MusicText,

        votingName,
        chBtn1T,
        chBtn2T,
        chBtn3T,
        chBtn4T,
        chBtn5T,
        Balance,
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
        closeBtn.gameObject.SetActive(false);
        

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

        votingName = GetText((int)Texts.votingName);
        chooseButton1 = GetButton((int)Buttons.chooseBtn1);
        chooseButton2 = GetButton((int)Buttons.chooseBtn2);
        chooseButton3 = GetButton((int)Buttons.chooseBtn3);
        chooseButton4 = GetButton((int)Buttons.chooseBtn4);
        chooseButton5 = GetButton((int)Buttons.chooseBtn5);
        sendContract = GetButton((int)Buttons.sendContract);
        closeBtn = GetButton((int)Buttons.closeBtn);




        chBtn1T = GetText((int)Texts.chBtn1T);
        chBtn2T = GetText((int)Texts.chBtn2T);
        chBtn3T = GetText((int)Texts.chBtn3T);
        chBtn4T = GetText((int)Texts.chBtn4T);
        chBtn5T = GetText((int)Texts.chBtn5T);


        
        //test1 = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //voteListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        //scrollListView = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        GetButton((int)Buttons.voteList).gameObject.BindEvent(OnButtonListClicked);
        GetButton((int)Buttons.voteMake).gameObject.BindEvent(OnButtonMakeClicked);
        GetButton((int)Buttons.makeButton).gameObject.BindEvent(OnMakeButtonCliked);
        GetButton((int)Buttons.MusicOnOff).gameObject.BindEvent(OnButtonClickedMusic);
      

        GetButton((int)Buttons.chooseBtn1).gameObject.BindEvent(vote1ChooseClicked);
        GetButton((int)Buttons.chooseBtn2).gameObject.BindEvent(vote2ChooseClicked);
        GetButton((int)Buttons.chooseBtn3).gameObject.BindEvent(vote3ChooseClicked);
        GetButton((int)Buttons.chooseBtn4).gameObject.BindEvent(vote4ChooseClicked);
        GetButton((int)Buttons.chooseBtn5).gameObject.BindEvent(vote5ChooseClicked);
        GetButton((int)Buttons.closeBtn).gameObject.BindEvent(closeBtnClicked);
        

        

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    private void Update()
    {
        // 메타마스크 테스트 시 주석 풀기
        //setWalletAddress(walletAddress.text);
        tokenManager.Instance.getBalance(Metamask.Instance.walletAddress);
        GetText((int)Texts.Balance).text = Convert.ToString(Metamask.Instance.balance);
    }
    public void AddNewUiObject(int j)
    {
        var newUi = Instantiate(uiPrefab, voteListView.content).GetComponent<RectTransform>();
        newUi.name = "serveybtn" +j;
        //파이어베이스 부분
        /*
        newUi.GetComponent<ServeyBtn>().buttondata = AuthHandler.Instance.voteSubjectData[j];
        newUi.GetChild(0).gameObject.GetComponent<Text>().text =AuthHandler.Instance.voteSubjectData[j];
        */


        
        uiObjects.Add(newUi);

        float y = 0f;
        for(int i =0; i< uiObjects.Count;i++)
        {
            uiObjects[i].anchoredPosition = new Vector2(0f,-y);
            y += uiObjects[i].sizeDelta.y + space;
        }

        voteListView.content.sizeDelta = new Vector2(voteListView.content.sizeDelta.x,y);
    }

    // int _score = 0;

    public void parsingData()
    {
            voteListView.gameObject.SetActive(true);
            //파이어베이스부분
            /*
            Debug.Log("보트서브젝트데이터 카운트 길이:"+AuthHandler.Instance.voteSubjectData.Count);
            for(int i=0;i<AuthHandler.Instance.voteSubjectData.Count;i++)
            {
                AddNewUiObject(i);
               // GameObject.find()

            }
            */
            //로컬부분
            for(int i=0;i<btnlength;i++)
            {
                AddNewUiObject(i);
               // GameObject.find()

            }

            


    }

    public void OnButtonListClicked(PointerEventData data)
    {

        // db에서 불러와서 출력 
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        if(voteListView.gameObject.activeSelf == true)
        {
            voteListView.gameObject.SetActive(false);
//파이어베이스부분
            /*
            uiObjects.Clear();
            for(int i =0 ;i<AuthHandler.Instance.voteSubjectData.Count;i++){

                Destroy(voteListView.content.GetChild(AuthHandler.Instance.voteSubjectData.Count - i -1).gameObject);


            }
            */
            uiObjects.Clear();
            for(int i =0 ;i<btnlength;i++){

                Destroy(voteListView.content.GetChild(btnlength - i -1).gameObject);


            }

        }
        else{
            //파이어베이스부분
            /*
            AuthHandler.Instance.voteSubjectData.Clear(); // 데이터 받아오기전 그전 데이터 남아있을수도있으니 삭제
            AuthHandler.Instance.GetAllVoteDocument(); // 데이터 받아서 votesubjectData에 저장
            */

            Invoke("parsingData",1f);//데이터를 읽어오는데 시간이 걸려서 invoke로 시간 지연 줌

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
    ///////////////////////////////////////////////////투표용지 껐다 키는 함수
    public void OnOffVotePaper(){
            chooseButton1.gameObject.SetActive(true);
            chooseButton2.gameObject.SetActive(true);
            chooseButton3.gameObject.SetActive(true);
            chooseButton4.gameObject.SetActive(true);
            chooseButton5.gameObject.SetActive(true);
            votingName.gameObject.SetActive(true);
            sendContract.gameObject.SetActive(true);
            closeBtn.gameObject.SetActive(true);
        

            votingName.text =AuthHandler.Instance.wantvote;
            chBtn1T.text=AuthHandler.Instance.vote1 + "  :  " + AuthHandler.Instance.voteCnt1;
            chBtn2T.text=AuthHandler.Instance.vote2 + "  :  " + AuthHandler.Instance.voteCnt2;
            chBtn3T.text=AuthHandler.Instance.vote3 + "  :  " + AuthHandler.Instance.voteCnt3;
            chBtn4T.text=AuthHandler.Instance.vote4 + "  :  " + AuthHandler.Instance.voteCnt4;
            chBtn5T.text=AuthHandler.Instance.vote5 + "  :  " + AuthHandler.Instance.voteCnt5;

    }





    ///////////////////////////////////////////////////

///////////////////////////////////////////////파이어 베이스 투표지 1~5번 까지 코드    
    public void doingvote1(){
        if(AuthHandler.Instance.voteCheck=="null"){
            AuthHandler.Instance.focusField="vote1Cnt";
            AuthHandler.Instance.IncrementFieldValue();
            Debug.Log(AuthHandler.Instance.voteCheck);

        }else{
            Debug.Log(" 이미 투표했네요");

        }

    }
    public void vote1ChooseClicked(PointerEventData data){
        ///파이어베이스 부분
        /*
        AuthHandler.Instance.focusDocument=votingName.text;
        AuthHandler.Instance.GetVoteCheckDocument();
        Invoke("doingvote1",1f);//데이터를 읽어오는데 시간이 걸려서 invoke로 시간 지연 줌
         */
         
    }
    public void doingvote2(){
        if(AuthHandler.Instance.voteCheck=="null"){
            AuthHandler.Instance.focusField="vote2Cnt";
            AuthHandler.Instance.IncrementFieldValue();

        }else{
            Debug.Log(" 이미 투표했네요");

        }

    }
    public void vote2ChooseClicked(PointerEventData data){
        ///파이어베이스 부분
        /*
       AuthHandler.Instance.focusDocument=votingName.text;
        AuthHandler.Instance.GetVoteCheckDocument();
        Invoke("doingvote2",1f);//데이터를 읽어오는데 시간이 걸려서 invoke로 시간 지연 줌
    */
    }
    public void doingvote3(){
        if(AuthHandler.Instance.voteCheck=="null"){
            AuthHandler.Instance.focusField="vote3Cnt";
            AuthHandler.Instance.IncrementFieldValue();

        }else{
            Debug.Log(" 이미 투표했네요");

        }

    }
    public void vote3ChooseClicked(PointerEventData data){
        ///파이어베이스 부분
        /*
        AuthHandler.Instance.focusDocument=votingName.text;
        AuthHandler.Instance.GetVoteCheckDocument();
        Invoke("doingvote3",1f);//데이터를 읽어오는데 시간이 걸려서 invoke로 시간 지연 줌
       
*/
    } 
    public void doingvote4(){
        if(AuthHandler.Instance.voteCheck=="null"){
            AuthHandler.Instance.focusField="vote4Cnt";
            AuthHandler.Instance.IncrementFieldValue();

        }else{
            Debug.Log(" 이미 투표했네요");

        }

    }
    public void vote4ChooseClicked(PointerEventData data){
        ///파이어베이스 부분
        /*
         AuthHandler.Instance.focusDocument=votingName.text;
        AuthHandler.Instance.GetVoteCheckDocument();
        Invoke("doingvote4",1f);//데이터를 읽어오는데 시간이 걸려서 invoke로 시간 지연 줌
      */

    }
    public void doingvote5(){
        if(AuthHandler.Instance.voteCheck=="null"){
            AuthHandler.Instance.focusField="vote5Cnt";
            AuthHandler.Instance.IncrementFieldValue();

        }else{
            Debug.Log(" 이미 투표했네요");

        }

    }
    public void vote5ChooseClicked(PointerEventData data){
        ///파이어베이스 부분
        /*
        AuthHandler.Instance.focusDocument=votingName.text;
        AuthHandler.Instance.GetVoteCheckDocument();
        Invoke("doingvote5",1f);//데이터를 읽어오는데 시간이 걸려서 invoke로 시간 지연 줌
        */

    }
///////////////////////////////////////////////파이어 베이스 투표지 1~5번 까지 코드
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
        AuthHandler.Instance.SetVoteDocument();

*/



        // 만들기 버튼 클릭시 list에 출력 가능. 
        // db에 각각의 데이터 저장 

        // 4개투표를 -> list -> list길이를 받아서 그것만큼 버튼생성해주는거지 
        // 데이터별로 (1번 -> 6개) 
        // 투표테이블 -> 이름 == 이름  -> 1번 투표에대한 내용을 호출 >> 

    }
    // 동적 버튼 생성 
    

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

    public void closeBtnClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        chooseButton1.gameObject.SetActive(false);
        chooseButton2.gameObject.SetActive(false);
        chooseButton3.gameObject.SetActive(false);
        chooseButton4.gameObject.SetActive(false);
        chooseButton5.gameObject.SetActive(false);
        sendContract.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(false);
        
        
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
