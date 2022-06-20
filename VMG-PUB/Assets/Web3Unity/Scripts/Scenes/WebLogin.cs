using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

// #if UNITY_WEBGL
public class WebLogin : MonoBehaviour
{
    public static WebLogin Instance = null;
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;

    public string getAccount;

    private void Awake()
    { 
        if (Instance == null) 
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        { 
            if (Instance != this)
            Destroy(this.gameObject); 
        } 
    }

    async public Task OnLogin()
    {
        Web3Connect();
        await OnConnected();
        
        PopupWindowController.Instance.setNum_login = 1;
        tokenManager.Instance.getBalance(getAccount);
        Debug.Log(getAccount);
        // Debug.Log(tokenManager.Instance.getBalance(PlayerPrefs.GetString("Account")).Result);
    }

    async private Task OnConnected()
    {
        account = ConnectAccount();
        while (account == "") {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        this.getAccount = account;
        Metamask.Instance.walletAddress = account;
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        SetConnectAccount("");
        // load next scene
        Debug.Log("web login connect");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // 이거 보팅룸에 들어 갔을때만 출력하는 예외처리
        //Metamask.Instance.setcheckmetamask(); 
        //UI_Voting.Instance.setWalletAddress(account);
    
    }

    public void OnSkip()
    {
        // burner account for skipped sign in screen
        PlayerPrefs.SetString("Account", "");
        // move to next scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
// #endif
