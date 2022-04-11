using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using UnityEngine.EventSystems;

/// <summary>
/// 팝업 윈도우를 띄워주는 클래스
/// Ok 팝업, Ok/Cancel 팝업, Yes/No 팝업, Yes/No/Cancel 팝업 
/// </summary>
public class PopupWindowController : UI_Popup
{
    public static PopupWindowController Instance; // singleton 변수
    public GameObject go;

    Define.Scene _scene = Define.Scene.Square;

    public bool useBackground = true;
    public string DefaultOkName = "Ok";         // 기본 Ok 텍스트
    public string DefaultYesName = "Yes";       // 기본 Yes 텍스트
    public string DefaultNoName = "No";         // 기본 No 텍스트

    // [SerializeField]
    // private GameObject background;  // 배경 패널
    // [SerializeField]
    // private GameObject popupWindow; // 팝업 윈도우 패널

    enum GameObjects
    {
        Background,
        PopupWindow,
    }

    // [SerializeField]
    // private Button okButton;        // Ok 버튼
    // [SerializeField]
    // private Button cancelButton;    // Cancel 버튼
    // [SerializeField]
    // private Button yesButton;       // Yes 버튼
    // [SerializeField]
    // private Button NoPortalButton;        // No 버튼
    enum Buttons
    {
        OkLoginButton,
        CancelButton,
        YesPortalButton,
        NoPortalButton,
        YesGameButton,
        NoGameButton,
        YesGameExitButton,
        NoGameExitButton,
        YesMetamaskConnectButton,
        NoMetamaskConnectButton,
    }

    // [SerializeField]
    // private Text okText;            // Ok 텍스트
    // [SerializeField]
    // private Text cancelText;        // Cancel 텍스트
    // [SerializeField]
    // private Text yesText;           // Yes 텍스트
    // [SerializeField]
    // private Text noText;            // No 텍스트
    // [SerializeField]
    // private Text titleText;         // Title 텍스트
    // [SerializeField]
    // private Text messageText;       // Message 텍스트

    enum Texts
    {
        OkText,
        CancelText,
        YesText,
        NoText,
        TitleText,
        MessageText,
    }


    private Action okAction;        // Ok 이벤트
    private Action cancelAction;    // Cancel 이벤트
    private Action yesAction;       // Yes 이벤트
    private Action noAction;        // No 이벤트

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        // ClosePopupWindow();
        Init();
        ClosePopupUI();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        // GetButton((int)Buttons.okButton).gameObject.BindEvent(ShowOk);
        GetButton((int)Buttons.OkLoginButton).gameObject.BindEvent(OnClickOkButtonLogin);
        GetButton((int)Buttons.YesPortalButton).gameObject.BindEvent(OnClickYesButtonPortal);
        GetButton((int)Buttons.NoPortalButton).gameObject.BindEvent(OnClickNoPortalButtonPortal);
        GetButton((int)Buttons.YesMetamaskConnectButton).gameObject.BindEvent(OnClickYesButtonMetamask);
        GetButton((int)Buttons.NoMetamaskConnectButton).gameObject.BindEvent(OnClickNoButtonMetamask);
        GetButton((int)Buttons.YesGameButton).gameObject.BindEvent(OnClickYesButtonGame);
        GetButton((int)Buttons.NoGameButton).gameObject.BindEvent(OnClickNoButtonGame);
        GetButton((int)Buttons.YesGameExitButton).gameObject.BindEvent(OnClickYesButtonGameExit);
        GetButton((int)Buttons.NoGameExitButton).gameObject.BindEvent(OnClickNoButtonGameExit);

        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
    }

    void Update() {
        if (GameObject.Find("@Player") == null)
        {
            // Debug.Log("Can't find player");
            return;
        }
        go = GameObject.Find("@Player");
    }

    public override void ClosePopupUI()
    {
        // base.ClosePopupUI();
        okAction = null;
        cancelAction = null;
        yesAction = null;
        noAction = null;

        // 버튼 비활성화
        Button okButton = GetButton((int)Buttons.OkLoginButton);
        Button cancelButton = GetButton((int)Buttons.CancelButton);
        Button yesPortalButton = GetButton((int)Buttons.YesPortalButton);
        Button NoPortalButton = GetButton((int)Buttons.NoPortalButton);
        Button YesMetamaskConnectButton = GetButton((int)Buttons.YesMetamaskConnectButton);
        Button NoMetamaskConnectButton = GetButton((int)Buttons.NoMetamaskConnectButton);
        Button yesGameButton = GetButton((int)Buttons.YesGameButton);
        Button NoGameButton = GetButton((int)Buttons.NoGameButton);
        Button yesGameExitButton = GetButton((int)Buttons.YesGameExitButton);
        Button NoGameExitButton = GetButton((int)Buttons.NoGameExitButton);
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        yesPortalButton.gameObject.SetActive(false);
        NoPortalButton.gameObject.SetActive(false);
        yesGameButton.gameObject.SetActive(false);
        NoGameButton.gameObject.SetActive(false);
        yesGameExitButton.gameObject.SetActive(false);
        NoGameExitButton.gameObject.SetActive(false);
        YesMetamaskConnectButton.gameObject.SetActive(false);
        NoMetamaskConnectButton.gameObject.SetActive(false);
        
        GameObject background = GetGameObject((int)GameObjects.Background).gameObject;
        GameObject popupWindow = GetGameObject((int)GameObjects.PopupWindow).gameObject;

        // 배경 비활성화
        if(useBackground)
            background.SetActive(false);
        // 팝업 윈도우 비활성화
        popupWindow.SetActive(false);
        
        // 텍스트 초기화
        Text okText = GetText((int)Texts.OkText);
        Text yesText = GetText((int)Texts.YesText);
        Text noText = GetText((int)Texts.NoText);
        okText.text = DefaultOkName;
        yesText.text = DefaultYesName;
        noText.text = DefaultNoName;
    }

    private void ClosePopupWindow()
    {
        // 이벤트 초기화
        okAction = null;
        cancelAction = null;
        yesAction = null;
        noAction = null;
        
        // 버튼 비활성화
        Button okButton = GetButton((int)Buttons.OkLoginButton);
        Button cancelButton = GetButton((int)Buttons.CancelButton);
        Button YesPortalButton = GetButton((int)Buttons.YesPortalButton);
        Button NoPortalButton = GetButton((int)Buttons.NoPortalButton);
        Button YesGameButton = GetButton((int)Buttons.YesGameButton);
        Button NoGameButton = GetButton((int)Buttons.NoGameButton);
        Button YesGameExitButton = GetButton((int)Buttons.YesGameExitButton);
        Button NoGameExitButton = GetButton((int)Buttons.NoGameExitButton);
        Button YesMetamaskConnectButton = GetButton((int)Buttons.YesMetamaskConnectButton);
        Button NoMetamaskConnectButton = GetButton((int)Buttons.NoMetamaskConnectButton);
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        YesPortalButton.gameObject.SetActive(false);
        NoPortalButton.gameObject.SetActive(false);
        YesGameButton.gameObject.SetActive(false);
        NoGameButton.gameObject.SetActive(false);
        YesGameExitButton.gameObject.SetActive(false);
        NoGameExitButton.gameObject.SetActive(false);
        YesMetamaskConnectButton.gameObject.SetActive(false);
        NoMetamaskConnectButton.gameObject.SetActive(false);

        GameObject background = GetGameObject((int)GameObjects.Background).gameObject;
        GameObject popupWindow = GetGameObject((int)GameObjects.PopupWindow).gameObject;

        // 배경 비활성화
        if(useBackground)
            background.SetActive(false);
        // 팝업 윈도우 비활성화
        popupWindow.SetActive(false);
        
        // 텍스트 초기화
        Text okText = GetText((int)Texts.OkText);
        Text yesText = GetText((int)Texts.YesText);
        Text noText = GetText((int)Texts.NoText);
        okText.text = DefaultOkName;
        yesText.text = DefaultYesName;
        noText.text = DefaultNoName;
    }

    private void OpenPopupWindow()
    {
        GameObject background = GetGameObject((int)GameObjects.Background).gameObject;
        GameObject popupWindow = GetGameObject((int)GameObjects.PopupWindow).gameObject;

        // 배경 활성화
        if (useBackground)
            background.SetActive(true);
        // 팝업 윈도우 활성화
        popupWindow.SetActive(true);
    }

    /// <summary>
    /// Ok 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="okAction">Ok 이벤트</param>
    public void ShowOk(string title, string message, Action okAction = null)
    {
        // 이벤트 등록
        this.okAction = okAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button okButton = GetButton((int)Buttons.OkLoginButton);
        okButton.gameObject.SetActive(true);
        okButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = null;
        navigation.selectOnRight = null;
        okButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    public void ShowYesNoPortal(string title, string message, Action yesAction = null, Action noAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button yesButton = GetButton((int)Buttons.YesPortalButton);
        Button NoPortalButton = GetButton((int)Buttons.NoPortalButton);
        yesButton.gameObject.SetActive(true);
        NoPortalButton.gameObject.SetActive(true);
        NoPortalButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = NoPortalButton;
        navigation.selectOnRight = NoPortalButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        NoPortalButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    public void ShowOkLogin(string title, string message, Action okAction = null)
    {
        // 이벤트 등록
        this.okAction = okAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button okButton = GetButton((int)Buttons.OkLoginButton);
        okButton.gameObject.SetActive(true);
        okButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = null;
        navigation.selectOnRight = null;
        okButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    /// <summary>
    /// Ok/Cancel 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="okAction">Ok 이벤트</param>
    /// <param name="cancelAction">Cancel 이벤트</param>

    /// <summary>
    /// Yes/No 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="yesAction">Yes 이벤트</param>
    /// <param name="noAction">No 이벤트</param>
    public void ShowYesNoGame(string title, string message, Action yesAction = null, Action noAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button yesButton = GetButton((int)Buttons.YesGameButton);
        Button NoButton = GetButton((int)Buttons.NoGameButton);
        yesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        NoButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = NoButton;
        navigation.selectOnRight = NoButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        NoButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    public void ShowYesNoMetamaskConnect(string title, string message, Action yesAction = null, Action noAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button yesButton = GetButton((int)Buttons.YesMetamaskConnectButton);
        Button NoPortalButton = GetButton((int)Buttons.NoMetamaskConnectButton);
        yesButton.gameObject.SetActive(true);
        NoPortalButton.gameObject.SetActive(true);
        NoPortalButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = NoPortalButton;
        navigation.selectOnRight = NoPortalButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        NoPortalButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    public void OnClickYesButtonMetamask(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        
        // 메타마스크 sdk 실행 코드 
        WebLogin.Instance.OnLogin();
        
        // Managers.Scene._portalCheck = true;
        if (go.GetComponent<PhotonView>().IsMine)
            PlayerController.Instance._portalCheck = true;
            Debug.Log("눌림");
            // ClosePopupWindow();
            ClosePopupUI();
        
    }

    public void OnClickNoButtonMetamask(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        ClosePopupUI();
    }

    public void ShowYesNoGameExit(string title, string message, Action yesAction = null, Action noAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button yesButton = GetButton((int)Buttons.YesGameExitButton);
        Button NoButton = GetButton((int)Buttons.NoGameExitButton);
        yesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        NoButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = NoButton;
        navigation.selectOnRight = NoButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        NoButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    public void ShowYesNo(string title, string message, Action yesAction = null, Action noAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;

        // 타이틀 및 메시지 설정
        Text titleText = GetText((int)Texts.TitleText);
        Text messageText = GetText((int)Texts.MessageText);
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        Button yesButton = GetButton((int)Buttons.YesPortalButton);
        Button NoPortalButton = GetButton((int)Buttons.NoPortalButton);
        yesButton.gameObject.SetActive(true);
        NoPortalButton.gameObject.SetActive(true);
        NoPortalButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = NoPortalButton;
        navigation.selectOnRight = NoPortalButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        NoPortalButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    /// <summary>
    /// Yes/No/Cancel 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="yesAction">Yes 이벤트</param>
    /// <param name="noAction">No 이벤트</param>
    /// <param name="cancelAction">Cancel 이벤트</param>

    /// <summary>
    /// Ok 텍스트 설정
    /// </summary>
    /// <param name="okName">설정할 이름</param>
    public void SetOkText(string okName)
    {
        // okText.text = okName;
        GetText((int)Texts.OkText).text = okName;
    }

    /// <summary>
    /// Yes 텍스트 설정
    /// </summary>
    /// <param name="yesName">설정할 이름</param>
    public void SetYesText(string yesName)
    {
        // yesText.text = yesName;
        GetText((int)Texts.YesText).text = yesName;
    }

    /// <summary>
    /// No 텍스트 설정
    /// </summary>
    /// <param name="noName">설정할 이름</param>
    public void SetNoText(string noName)
    {
        // noText.text = noName;
        GetText((int)Texts.NoText).text = noName;
    }

    public void OnClickOkButton()
    {
        if (okAction != null)
            okAction();

        // ClosePopupWindow();
        ClosePopupUI();
    }
    public void OnClickOkButtonLogin(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        Managers.Scene._logincheck = true;
        // ClosePopupWindow();
        ClosePopupUI();
    }

    public void OnClickYesButtonPortal(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        // Managers.Scene._portalCheck = true;
        if (go.GetComponent<PhotonView>().IsMine)
            PlayerController.Instance._portalCheck = true;
        Debug.Log("눌림");
        // ClosePopupWindow();
        ClosePopupUI();
    }

    public void OnClickNoPortalButtonPortal(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        // Managers.Scene._portalCheck = false;
        // ClosePopupWindow();
        ClosePopupUI();
    }

    public void OnClickYesButtonGame(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        // Managers.Scene._portalCheck = true;
        if (go.GetComponent<PhotonView>().IsMine)
            PlayerController.Instance._mode = PlayerController.modeState.Game;
        Debug.Log("눌림");
        // ClosePopupWindow();
        ClosePopupUI();

        if(go.GetComponent<PhotonView>().IsMine)
        {
            _scene = Define.Scene.Game;
            go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Game;
            Managers.Network.LeaveRoom();
            Managers.Scene.LoadScene(_scene);
            Managers.Network.OnLogin();
            // Managers.Scene._portalCheck = false;
        }
    }

    public void OnClickNoButtonGame(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        // Managers.Scene._portalCheck = false;
        // ClosePopupWindow();
        ClosePopupUI();
    }

    public void OnClickYesButtonGameExit(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        // Managers.Scene._portalCheck = true;
        if (go.GetComponent<PhotonView>().IsMine)
            PlayerController.Instance._mode = PlayerController.modeState.Square;
        Debug.Log("눌림");
        // ClosePopupWindow();
        ClosePopupUI();

        if(go.GetComponent<PhotonView>().IsMine)
        {
            _scene = Define.Scene.Square;
            go.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
            Managers.Network.LeaveRoom();
            Managers.Scene.LoadScene(_scene);
            Managers.Network.OnLogin();
            // Managers.Scene._portalCheck = false;
        }
    }

    public void OnClickNoButtonGameExit(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        ClosePopupUI();
    }

    public void OnClickYesButton()
    {
        if (yesAction != null)
            yesAction();

        // ClosePopupWindow();
        ClosePopupUI();
    }

    public void OnClickNoButton()
    {
        if (noAction != null)
            noAction();

        // ClosePopupWindow();
        ClosePopupUI();
    }
}