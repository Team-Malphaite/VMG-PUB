using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScene : BaseScene
{
    public static SquareScene Instance;
    public GameObject go;
    GameObject cam;

    protected override void Init()
    {
        base.Init();
        cam = GameObject.Find("Main Camera");
        if (cam == null)
            Managers.Resource.Instantiate("Camera/Main Camera");
        Instance = this;
        go = GameObject.Find("portal");

        SceneType = Define.Scene.Square;

        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Square>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
    }

    public override void Clear()
    {
        
    }
}
