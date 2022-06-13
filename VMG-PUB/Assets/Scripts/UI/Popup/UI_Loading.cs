using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Loading : UI_Popup
{
    enum RawImages
    {
        Loading,
    }

    private void Start()
    {
        Init();
        
    }

    public override void Init()
    {
        base.Init();

        Bind<RawImage>(typeof(RawImages));
    }

    private void Update()
    {
        if (GameObject.Find("@Player") == null) return;
        else GetRawImage((int)RawImages.Loading).gameObject.SetActive(false);
    }

}
