using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성 보장
    static Managers Instance { get { return s_instance; } } // 유일한 매니저 갖고 옴

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIMananger _ui = new UIMananger();
    ScreenManager _screen = new ScreenManager();
    NetworkManager _network = new NetworkManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIMananger UI { get { return Instance._ui; } }
    public static ScreenManager Screen { get { return Instance._screen; } }
    public static NetworkManager Network { get { return Instance._network; }}

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = null;
        if (s_instance == null)
        {
            // 초기화
            go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers"};
                go.AddComponent<Managers>();
            }
            s_instance = go.GetComponent<Managers>();
            // if (net == null)
            // {
            //     net = new GameObject {name = "@Network"};
            //     net.AddComponent<NetworkManager>();
            // }
            // DontDestroyOnLoad(net);
        }
        DontDestroyOnLoad(go);
    }
}
