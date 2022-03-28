using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingScene : BaseScene
{
    public GameObject go;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Square;

        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Voting>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
    }

    public override void Clear()
    {
        
    }
}
