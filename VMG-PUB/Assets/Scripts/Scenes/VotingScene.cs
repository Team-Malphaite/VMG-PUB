using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingScene : BaseScene
{
    public GameObject go;
    GameObject cam;
    protected override void Init()
    {
        base.Init();
        cam = GameObject.Find("@Main Camera");
        if (cam == null)
            cam = Managers.Resource.Instantiate("Camera/Main Camera");

        SceneType = Define.Scene.Square;

        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Voting>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
    }

    public override void Clear()
    {
        
    }
}
