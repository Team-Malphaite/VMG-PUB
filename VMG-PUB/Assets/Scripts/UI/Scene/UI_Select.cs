using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Select : UI_Scene
{
    enum Texts
    {
        Select,
        MusicText,
    }
    
    enum Buttons
    {
        MusicOnOff,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.MusicOnOff).gameObject.BindEvent(OnButtonClickedMusic);
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
    // int _score = 0;
}
