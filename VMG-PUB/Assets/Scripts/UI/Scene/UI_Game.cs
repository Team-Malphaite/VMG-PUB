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
        MusicOnOff,
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
        NextVictory,
        TimeExplain,
        GameOver,
    }

    enum Texts
    {
        ReadyButtonText,
        Rank,
        Timer,
        MusicText,
    }

    // int StartTimer = 0;
    // int FinishTimer = 0;

    // float m_TotalSeconds = 5 * 60;
    string m_Timer = @"00:00";

    RawImage Finished;
    RawImage Victory;
    RawImage ReturnSquare;
    RawImage NextVictory;
    RawImage TimeExplain;
    RawImage GameOver;
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
        GetRawImage((int)RawImages.NextVictory).gameObject.SetActive(false);
        GetRawImage((int)RawImages.TimeExplain).gameObject.SetActive(false);
        GetRawImage((int)RawImages.GameOver).gameObject.SetActive(false);
        GetText((int)Texts.Rank).gameObject.SetActive(false);
        GetText((int)Texts.Timer).gameObject.SetActive(false);
        GetButton((int)Buttons.MusicOnOff).gameObject.BindEvent(OnButtonClickedMusic);

        Finished = GetRawImage((int)RawImages.Finished);
        Victory = GetRawImage((int)RawImages.Victory);
        ReturnSquare = GetRawImage((int)RawImages.ReturnSquare);
        NextVictory = GetRawImage((int)RawImages.NextVictory);
        TimeExplain = GetRawImage((int)RawImages.TimeExplain);
        GameOver = GetRawImage((int)RawImages.GameOver);

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
            GetText((int)Texts.Timer).gameObject.SetActive(true);
            if (GameObject.Find("StartWall") != null)
                GameObject.Find("StartWall").gameObject.SetActive(false);
        }

        if (GetText((int)Texts.Rank).gameObject.activeSelf && !GameManagerEx.Instance.getGameFinished())
            RankUpdate();

        if (GetText((int)Texts.Timer).gameObject.activeSelf && !GameManagerEx.Instance.getGameFinished())
            TimerUpdate();

        GameFinishCountDown();
        GameOverUpdate();
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

    void CountDown()
    {
        // if (StartTimer == 0) Time.timeScale = 0.0f;
        if (GameManagerEx.Instance.getStarterTimer() <= 225)
        {
            GameManagerEx.Instance.setStarterTimerPlus();
            
            if (GameManagerEx.Instance.getStarterTimer() < 60) GetRawImage((int)RawImages.Explain).gameObject.SetActive(true);
            if (GameManagerEx.Instance.getStarterTimer() > 60) 
            {
                GetRawImage((int)RawImages.Explain).gameObject.SetActive(false);
                GetRawImage((int)RawImages.TimeExplain).gameObject.SetActive(true);
            }
            if (GameManagerEx.Instance.getStarterTimer() > 120) 
            {
                GetRawImage((int)RawImages.TimeExplain).gameObject.SetActive(false);
                GetRawImage((int)RawImages.Three).gameObject.SetActive(true);
            }
            if (GameManagerEx.Instance.getStarterTimer() > 150) 
            {
                GetRawImage((int)RawImages.Three).gameObject.SetActive(false);
                GetRawImage((int)RawImages.Two).gameObject.SetActive(true);
            }
            if (GameManagerEx.Instance.getStarterTimer() > 180) 
            {
                GetRawImage((int)RawImages.Two).gameObject.SetActive(false);
                GetRawImage((int)RawImages.One).gameObject.SetActive(true);
            }
            if (GameManagerEx.Instance.getStarterTimer() > 210) 
            {
                GetRawImage((int)RawImages.One).gameObject.SetActive(false);
                GetRawImage((int)RawImages.Start).gameObject.SetActive(true);
                // StartCoroutine(RunFadeOut());
            }
            if (GameManagerEx.Instance.getStarterTimer() >= 225)
            {
                GetRawImage((int)RawImages.Start).gameObject.SetActive(false);
                // Time.timeScale = 1.0f;
                GameManagerEx.Instance.setGameStart();
            }
        }
    }

    void GameFinishCountDown()
    {
        if (GameManagerEx.Instance.getGameFinished())
            if (rank == 1)
            {
                // if (FinishTimer == 0) Time.timeScale = 0.0f;
                if (GameManagerEx.Instance.getFinishTimer() <= 300)
                {
                    GameManagerEx.Instance.setFinishTimerPlus();
                    
                    if (GameManagerEx.Instance.getFinishTimer() < 60) Victory.gameObject.SetActive(true);
                    if (GameManagerEx.Instance.getFinishTimer() > 60) 
                    {
                        Victory.gameObject.SetActive(false);
                        Finished.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() > 90) 
                    {
                        Finished.gameObject.SetActive(false);
                        ReturnSquare.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() >= 240)
                    {
                        // Time.timeScale = 1.0f;
                        player.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                        Managers.Network._scene = Define.Scene.Square;
                        PhotonNetwork.LeaveRoom();
                        Managers.Network.OnLeftRoom();
                    }
                }
            }
            else if (rank != 1)
            {
                // if (FinishTimer == 0) Time.timeScale = 0.0f;
                if (GameManagerEx.Instance.getFinishTimer() <= 300)
                {
                    GameManagerEx.Instance.setFinishTimerPlus();
                    
                    if (GameManagerEx.Instance.getFinishTimer() < 60) Finished.gameObject.SetActive(true);
                    if (GameManagerEx.Instance.getFinishTimer() > 60) 
                    {
                        Finished.gameObject.SetActive(false);
                        NextVictory.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() > 90) 
                    {
                        NextVictory.gameObject.SetActive(false);
                        ReturnSquare.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() >= 240)
                    {
                        // Time.timeScale = 1.0f;
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
        player.GetComponent<PlayerController>().setRank(rank);
        GetText((int)Texts.Rank).text = rank + "  /  " + Managers.Network.getGameMaxPlayer();
        // Debug.Log(GameManagerEx.Instance.Players[0].GetComponent<PlayerController>().getRank());
    }

    void TimerUpdate()
    {
        GameManagerEx.Instance.setMinusTotalSecond(Time.deltaTime);

        TimeSpan timespan = TimeSpan.FromSeconds(GameManagerEx.Instance.getTotalSecond());
        string timer = string.Format("{0:00}:{1:00}", timespan.Minutes, timespan.Seconds);

        GetText((int)Texts.Timer).text = timer;

        if (GameManagerEx.Instance.getTotalSecond() <= 0) 
        { 
            SetZero(); 
            GameManagerEx.Instance.setGameOver();
        } 
    }

    void SetZero() 
    { 
        m_Timer = @"00:00";
        GameManagerEx.Instance.setZeroTotalSecond();
    }

    void GameOverUpdate()
    {
        //
        if (GameManagerEx.Instance.getGameOver())
        {
            if (GameManagerEx.Instance.getFinishTimer() <= 300)
                {
                    GameManagerEx.Instance.setFinishTimerPlus();
                    
                    if (GameManagerEx.Instance.getFinishTimer() < 60) GameOver.gameObject.SetActive(true);
                    if (GameManagerEx.Instance.getFinishTimer() > 60) 
                    {
                        GameOver.gameObject.SetActive(false);
                        Finished.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() > 90) 
                    {
                        Finished.gameObject.SetActive(false);
                        NextVictory.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() > 120) 
                    {
                        NextVictory.gameObject.SetActive(false);
                        ReturnSquare.gameObject.SetActive(true);
                    }
                    if (GameManagerEx.Instance.getFinishTimer() >= 270)
                    {
                        // Time.timeScale = 1.0f;
                        player.GetComponent<PlayerController>()._mode = PlayerController.modeState.Square;
                        Managers.Network._scene = Define.Scene.Square;
                        PhotonNetwork.LeaveRoom();
                        Managers.Network.OnLeftRoom();
                    }
                }
        }

    }
}
