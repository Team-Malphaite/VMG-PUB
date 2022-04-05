using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterController : MonoBehaviour
{
    public bool clicked = false;
    public bool selected = false;
    public bool infoInput = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
            clickCharacterAnim();
        else
            unclickCharacterAnim();
        
        if (selected)
            selectCharacterAnim();
        else
            unselectCharacterAnim();
        
        if (infoInput)
            infoInputAnim();
        else
            uninfoInputAnim();
    }

    void clickCharacterAnim()
    {
        anim.SetBool("click", true);
    }

    void unclickCharacterAnim()
    {
        anim.SetBool("click", false);
    }

    void selectCharacterAnim()
    {
        anim.SetBool("select", true);
    }

    void unselectCharacterAnim()
    {
        anim.SetBool("select", false);
    }

    void infoInputAnim()
    {
        anim.SetBool("infoInput", true);
    }

    void uninfoInputAnim()
    {
        anim.SetBool("infoInput", false);
    }
}
