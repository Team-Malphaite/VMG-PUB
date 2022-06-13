using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class UI_Loading : UI_Popup
{
    enum RawImages
    {
        Loading,
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<RawImage>(typeof(RawImages));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("@Player") == null) return;
        else GetRawImage((int)RawImages.Loading).gameObject.SetActive(false);
    }
}
