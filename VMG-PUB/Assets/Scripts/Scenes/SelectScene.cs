using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectScene : BaseScene
{
    GameObject cam;
    public string selectCharacterName = null;

    protected override void Init()
    {
        base.Init();
        cam = GameObject.Find("Main Camera");

        SceneType = Define.Scene.Select;
        cam.name = "@Main Camera";
        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Select>();
        Managers.UI.ShowPopupUI<UI_CharacterSelect>();
        Managers.UI.ShowPopupUI<UI_SelectInfoInput>();
        // Managers.UI.ShowPopupUI<PopupWindowController>();
        // DontDestroyOnLoad(Managers.UI.ShowPopupUI<PopupWindowController>());
    }

    public override void Clear()
    {

    }
}
