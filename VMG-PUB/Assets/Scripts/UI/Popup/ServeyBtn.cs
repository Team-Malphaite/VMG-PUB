using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class ServeyBtn : MonoBehaviour
{
    public string buttondata = null;
    public string tmp = null;
    public int num;
    /*
    public void touch(){
        UI_Voting.Instance.OnOffVotePaper();

    }
    */
    ////파이어베이스 

    async void touch()
    {

        tmp = buttondata;

        tmp =string.Join("" , tmp.Split('"'));
        AuthHandler.Instance.wantvote =tmp;
        Debug.Log("tmp 값 = "+tmp);
        
        await daa();
        UI_Voting.Instance.OnOffVotePaper();


    }
    async Task daa()
    {
        AuthHandler.Instance.GetVoteDocument();

        await new WaitForSeconds(1f);
    }
     



}
