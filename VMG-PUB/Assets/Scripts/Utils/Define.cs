using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Square,
        Voting,
        Game,
    }
    public enum CameraMode
    {
        QuarterView,
    }
    public enum MouseEvent
    {
        Press,
        Click,
    }

    public enum LoginCheck
    {
        Pass,
        NonPass,
    }
}
