using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_SelectInfoInput : UI_Popup
{
    public static UI_SelectInfoInput Instance;
    public Button nextButton;
    public Button backButton;
    public InputField nick;
    GameObject background;
    public string selectCharacterName;
    enum Buttons
    {
        Next,
        Back,
    }
    enum GameObjects
    {
        Background,
    }
    enum InputFields
    {
        Nick,
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    void Start()
    {
        Init();
        nextButton = GetButton((int)Buttons.Next);
        backButton = GetButton((int)Buttons.Back);
        background = GetGameObject((int)GameObjects.Background).gameObject;
        nick = GetInputField((int)InputFields.Nick);
        ShowOff();
        selectCharacterName = Camera.main.GetComponent<SelectCameraController>().selectCharacterName;
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<InputField>(typeof(InputFields));

        GetButton((int)Buttons.Next).gameObject.BindEvent(OnButtonClickedNext);
        GetButton((int)Buttons.Back).gameObject.BindEvent(OnButtonClickedBack);
        GetInputField((int)InputFields.Nick).gameObject.BindEvent(OnInputFieldClicked);

    }

    public void OnButtonClickedNext(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        Debug.Log("click yes button");
        Debug.Log(selectCharacterName + "을 최종 선택했어요");
        Debug.Log(nick.text + "가 최종 이름했어요");
      /*  Debug.Log(AuthHandler.Instance.emailAddress + "가 현재 접속중인이메일주소");

        AuthHandler.Instance.name = nick.text;
        AuthHandler.Instance.charcter = selectCharacterName;
        AuthHandler.Instance.SetDocument();*/

        Managers.Scene.LoadScene(Define.Scene.Square);
        Managers.Network.OnLogin();
    }
    
    public void OnButtonClickedBack(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().selected = false;
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().clicked = false;
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().infoInput = false;
        Debug.Log("click back button");
        ShowOff();
        Camera.main.GetComponent<SelectCameraController>().restoreCam();
        Camera.main.GetComponent<SelectCameraController>().selectCharacterName = null;
    }

    public void OnInputFieldClicked(PointerEventData data)
    {
        Debug.Log("InputField Clicked");
    }

    public void ShowOn()
    {
        nextButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        nick.gameObject.SetActive(true);
    }

    public void ShowOff()
    {
        nextButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        nick.gameObject.SetActive(false);
    }
}
