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
        scrollRect,
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
        Debug.Log("Button Debug");
        Bind<Text>(typeof(Texts));
        Debug.Log("Text Debug");
        Bind<InputField>(typeof(InputFields));
        Debug.Log("InputField Debug");
        Bind<ScrollRect>(typeof(ScrollRects));
        Debug.Log("ScrollRect Debug");
        // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        inputs = GetInputField((int)InputFields.input);
        scrolls = GetScrollRect((int)ScrollRects.scrollRect);
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