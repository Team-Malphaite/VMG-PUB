using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.EventSystems;

public class UI_Chat : UI_Popup
{
    public static UI_Chat Instance;
    public InputField inputs;
    public ScrollRect scrolls;
    public Text logs;
    public enum Buttons
    {
        SendButton,
    }

    public enum Texts
    {
        chatLog,
    }

    public enum InputFields
    {
        input,
    }

    public enum ScrollRects
    {
        ScrollRect,
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Update(){
         
    }

    public override void Init(){
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<InputField>(typeof(InputFields));
        Bind<ScrollRect>(typeof(ScrollRects));
        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        inputs = GetInputField((int)InputFields.input);
        scrolls = GetScrollRect((int)ScrollRects.ScrollRect);
        logs = GetText((int)Texts.chatLog);
        

        GetButton((int)Buttons.SendButton).gameObject.BindEvent(SendButtonOnclicked);
    }

    public void SendButtonOnclicked(PointerEventData data){
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if(go.name.Equals("SendButton")){
            if(inputs.text.Equals("")){
                Debug.Log("Empty");
                return;
            }
            ChatManager.Instance.chatUpdate();
        }
    }

    
}