using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Photon.Pun;

public class UI_Game : UI_Scene
{
    public GameObject player;
    enum Buttons
    {
        Ready,
        Exit,
    }

    // enum GameObjects
    // {
    //     TestObject,
    // }

    enum RawImages
    {
        One,
        Two,
        Three,
        Start,
        Explain,
        Finished,
        Victory,
        ReturnSquare,
    }

    enum Texts
    {
        ReadyButtonText,
        Rank,
    }

    int StartTimer = 0;
    int FinishTimer = 0;

    public RawImage Finished;
    public RawImage Victory;
    public RawImage ReturnSquare;
    [SerializeField]
    int rank;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        // Bind<GameObject>(typeof(GameObjects));
        Bind<RawImage>(typeof(RawImages));

        GetButton((int)Buttons.Ready).gameObject.BindEvent(OnButtonClickedReady);
        GetButton((int)Buttons.Exit).gameObject.BindEvent(OnButtonClickedExit);

        // GameObject player = GetImage((int)Images.ItemIcon).gameObject;
        // BindEvent(player, (PointerEventData data) => { player.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    private void Start()
    {
        Init();

        GetRawImage((int)RawImages.One).gameObject.SetActive(false);
        GetRawImage((int)RawImages.Two).gameObject.SetActive(false);
        GetRawImage((int)RawImages.Three).gameObject.SetActive(false);
        GetRawImage((int)RawImages.Start).gameObject.SetActive(false);
        GetRawImage((int)RawImages.Explain).gameObject.SetActive(false);
        GetRawImage((int)RawImages.Finished).gameObject.SetActive(false);
        GetRawImage((int)RawImages.Victory).gameObject.SetActive(false);
        GetRawImage((int)RawImages.ReturnSquare).gameObject.SetActive(false);
        GetText((int)Texts.Rank).gameObject.SetActive(false);

        Finished = GetRawImage((int)RawImages.Finished);
        Victory = GetRawImage((int)RawImages.Victory);
        ReturnSquare = GetRawImage((int)RawImages.ReturnSquare);

        rank = GameManagerEx.Instance.Players.Count;
    }

    private void Update() {

        if (GameObject.Find("@Player") == null) return;
        else player = GameObject.Find("@Player");

        if (GameManagerEx.Instance.getAllReady() && !GameManagerEx.Instance.getGameStart())
        {
            GetButton((int)Buttons.Ready).gameObject.SetActive(false);
            GetButton((int)Buttons.Exit).gameObject.SetActive(false);
            CountDown();
        }
        if (GameManagerEx.Instance.getGameStart())
        {
            GetText((int)Texts.Rank).gameObject.SetActive(true);
            if (GameObject.Find("StartWall") != null)
                GameObject.Find("StartWall").gameObject.SetActive(false);
        }

        if (GetText((int)Texts.Rank).gameObject.activeSelf)
            RankUpdate();

        GameFinishCountDown();
    }

    public void OnButtonClickedReady(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;

        if (GetText((int)Texts.ReadyButtonText).text == "준비")
        {
            Debug.Log("준비 버튼 클릭");
            GetText((int)Texts.ReadyButtonText).text = "준비 완료";
            // PlayerController.Instance._gameReady = true;
            player.GetComponent<PlayerController>()._gameReady = true;
            
        }
        else if (GetText((int)Texts.ReadyButtonText).text == "준비 완료")
        {
            Debug.Log("준비 완료 버튼 클릭");
            GetText((int)Texts.ReadyButtonText).text = "준비";
            // PlayerController.Instance._gameReady = false;
            player.GetComponent<PlayerController>()._gameReady = false;
        }
    }

    public void OnButtonClickedExit(PointerEventData data)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        string title = "게임 나가기";
        string message = "게임을 나가시겠습니까?";
        Action yesAction = () => Debug.Log("On Click Yes Button");
        Action noAction = () => Debug.Log("On Click No Button");

        PopupWindowController.Instance.ShowYesNoGameExit(title, message, yesAction, noAction);
    }

    void CountDown()
    {
        if (StartTimer == 0) Time.timeScale = 0.0f;
        if (StartTimer <= 165)
        {
            StartTimer++;
            
            if (StartTimer < 60) GetRawImage((int)RawImages.Explain).gameObject.SetActive(true);
            if (StartTimer > 60) 
            {
                GetRawImage((int)RawImages.Explain).gameObject.SetActive(false);
                GetRawImage((int)RawImages.Three).gameObject.SetActive(true);
            }
            if (StartTimer > 90) 
            {
                GetRawImage((int)RawImages.Three).gameObject.SetActive(false);
                GetRawImage((int)RawImages.Two).gameObject.SetActive(true);
            }
            if (StartTimer > 120) 
            {
                GetRawImage((int)RawImages.Two).gameObject.SetActive(false);
                GetRawImage((int)RawImages.One).gameObject.SetActive(true);
            }
            if (StartTimer > 150) 
            {
                GetRawImage((int)RawImages.One).gameObject.SetActive(false);
                GetRawImage((int)RawImages.Start).gameObject.SetActive(true);
                // StartCoroutine(RunFadeOut());
            }
            if (StartTimer >= 165)
            {
                GetRawImage((int)RawImages.Start).gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                GameManagerEx.Instance.setGameStart();
            }
        }
    }

    void GameFinishCountDown()
    {
        if (GameManagerEx.Instance.getGameFinished())
            if (rank == 1)
            {
                if (FinishTimer == 0) Time.timeScale = 0.0f;
                if (FinishTimer <= 300)
                {
                    FinishTimer++;
                    
                    if (FinishTimer < 90) Victory.gameObject.SetActive(true);
                    if (FinishTimer > 90) 
                    {
                        Victory.gameObject.SetActive(false);
                        Finished.gameObject.SetActive(true);
                    }
                    if (FinishTimer > 150) 
                    {
                        Finished.gameObject.SetActive(false);
                        ReturnSquare.gameObject.SetActive(true);
                    }
                    if (FinishTimer >= 300)
                    {
                        Time.timeScale = 1.0f;
                        player.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                        Managers.Network._scene = Define.Scene.Square;
                        PhotonNetwork.LeaveRoom();
                        Managers.Network.OnLeftRoom();
                    }
                }
            }
            else if (rank != 1)
            {
                if (FinishTimer == 0) Time.timeScale = 0.0f;
                if (FinishTimer <= 300)
                {
                    FinishTimer++;
                    
                    if (FinishTimer < 90) Finished.gameObject.SetActive(true);
                    if (FinishTimer > 150) 
                    {
                        Finished.gameObject.SetActive(false);
                        ReturnSquare.gameObject.SetActive(true);
                    }
                    if (FinishTimer >= 300)
                    {
                        Time.timeScale = 1.0f;
                        player.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                        Managers.Network._scene = Define.Scene.Square;
                        PhotonNetwork.LeaveRoom();
                        Managers.Network.OnLeftRoom();
                    }
                }
            }
    }

    void RankUpdate()
    {
        rank = GameManagerEx.Instance.Players.Count;
        for(int i = 1; i < GameManagerEx.Instance.Players.Count; i++)
        {
            if (player.GetComponent<PlayerController>().getDist() < GameManagerEx.Instance.Players[i].GetComponent<PlayerController>().getDist()) rank--;
            // Debug.Log("본인"+player.GetComponent<PlayerController>().getDist());
            // Debug.Log(i+"번째"+GameManagerEx.Instance.Players[i].GetComponent<PlayerController>().getDist());
        }

        // Debug.Log(rank);
        GetText((int)Texts.Rank).text = rank + "  /  " + Managers.Network.getGameMaxPlayer();
    }
}
