using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Square;

        Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Square>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
    }

    public override void Clear()
    {
        
    }
}
