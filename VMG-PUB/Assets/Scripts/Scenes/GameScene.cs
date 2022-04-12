using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class GameScene : BaseScene
{
    GameObject cam;
    GameObject gameManager = null;

    protected override void Init()
    {
        base.Init();

        gameManager = GameObject.Find("@GameManagerEx");

        if (gameManager == null)
        {
            gameManager = new GameObject { name = "@GameManagerEx"};
            gameManager.AddComponent<GameManagerEx>();
        }

        cam = GameObject.Find("@Main Camera");
        if (cam == null)
        {
            cam = Managers.Resource.Instantiate("Camera/Main Camera");
            Debug.Log("make cam");
        }

        SceneType = Define.Scene.Game;
        cam.name = "@Main Camera";
        // Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Game>();
        Managers.UI.ShowPopupUI<PopupWindowController>();
        // DontDestroyOnLoad(Managers.UI.ShowPopupUI<PopupWindowController>());
    }

    private void Update() {
        if (GameManagerEx.Instance.readyCheck() && !GameManagerEx.Instance.getGameStart())
        {
            // Debug.Log("start & change UI");
            GameManagerEx.Instance.setAllReady();
        }
    }

    public override void Clear()
    {
        Destroy(GameManagerEx.Instance);
        Debug.Log("GameManagerEx Destoryed");
    }
}
