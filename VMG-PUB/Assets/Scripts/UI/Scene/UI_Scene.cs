using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
        Managers.UI.SetUIResolution(gameObject);
        Managers.Screen.SetResolution(1920, 1080);
    }    
}
