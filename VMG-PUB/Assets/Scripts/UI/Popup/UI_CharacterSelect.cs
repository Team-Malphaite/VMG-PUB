using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_CharacterSelect : UI_Popup
{
    public static UI_CharacterSelect Instance;
    public Button yesButton;
    public Button NoButton;
    GameObject background;
    public string selectCharacterName;
    enum Buttons
    {
        Yes,
        No,
    }
    enum GameObjects
    {
        Background,
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
        yesButton = GetButton((int)Buttons.Yes);
        NoButton = GetButton((int)Buttons.No);
        background = GetGameObject((int)GameObjects.Background).gameObject;
        ShowOff();
        selectCharacterName = Camera.main.GetComponent<SelectCameraController>().selectCharacterName;
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Yes).gameObject.BindEvent(OnButtonClickedYes);
        GetButton((int)Buttons.No).gameObject.BindEvent(OnButtonClickedNo);

    }

    public void OnButtonClickedYes(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        Debug.Log("click yes button");
        characterSelect();
    }
    public void OnButtonClickedNo(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().selected = false;
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().clicked = false;
        Debug.Log("click no button");
        ShowOff();
        Camera.main.GetComponent<SelectCameraController>().restoreCam();
        Camera.main.GetComponent<SelectCameraController>().selectCharacterName = null;
    }

    public void ShowOn()
    {
        yesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    public void ShowOff()
    {
        yesButton.gameObject.SetActive(false);
        NoButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    void characterSelect()
    {
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().selected = true;
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().clicked = false;
        Debug.Log(selectCharacterName + "을 선택했어요");
        Debug.Log(GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().selected);
        ShowOff();
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().infoInput = true;
        UI_SelectInfoInput.Instance.ShowOn();
    }
}
