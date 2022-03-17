using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_YesNo : UI_Popup
{
    enum Buttons
    {
        Yes,
        No,
    }

    enum Texts
    {
        Ask,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetButton((int)Buttons.Yes).gameObject.BindEvent(OnButtonClicked);
        GetButton((int)Buttons.No).gameObject.BindEvent(OnButtonClicked);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if(go.name.Equals("Yes"))
            Debug.Log("yes");
        if(go.name.Equals("No"))
            Debug.Log("no");
    }
}
