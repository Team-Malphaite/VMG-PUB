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
    // private Button noButton;        // No 버튼
    enum Buttons
    {
        OkButton,
        CancelButton,
        YesButton,
        NoButton,
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
        GetButton((int)Buttons.OkButton).gameObject.BindEvent(OnClickOkButtonLogin);
        GetButton((int)Buttons.YesButton).gameObject.BindEvent(OnClickYesButtonPortal);
        GetButton((int)Buttons.NoButton).gameObject.BindEvent(OnClickNoButtonPortal);

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
        Button okButton = GetButton((int)Buttons.OkButton);
        Button cancelButton = GetButton((int)Buttons.CancelButton);
        Button yesButton = GetButton((int)Buttons.YesButton);
        Button noButton = GetButton((int)Buttons.NoButton);
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

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
        Button okButton = GetButton((int)Buttons.OkButton);
        Button cancelButton = GetButton((int)Buttons.CancelButton);
        Button yesButton = GetButton((int)Buttons.YesButton);
        Button noButton = GetButton((int)Buttons.NoButton);
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

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
        Button okButton = GetButton((int)Buttons.OkButton);
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
        Button yesButton = GetButton((int)Buttons.YesButton);
        Button noButton = GetButton((int)Buttons.NoButton);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        noButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = noButton;
        navigation.selectOnRight = noButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        noButton.navigation = navigation;

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
        Button okButton = GetButton((int)Buttons.OkButton);
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
        Button yesButton = GetButton((int)Buttons.YesButton);
        Button noButton = GetButton((int)Buttons.NoButton);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        noButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = noButton;
        navigation.selectOnRight = noButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        noButton.navigation = navigation;

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

    public void OnClickNoButtonPortal(PointerEventData data)
    {
        if (okAction != null)
            okAction();
        // Managers.Scene._portalCheck = false;
        // ClosePopupWindow();
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