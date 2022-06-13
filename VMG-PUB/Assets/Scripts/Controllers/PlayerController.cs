using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public static PlayerController Instance;
    [SerializeField]
    public float _speed = 2.0f;
    [SerializeField]
    public float JumpPower = 0.4f;
    [SerializeField]
    private float _roatationSpeed = 0.07f;
    private Rigidbody rigid;
    public bool IsJumping = false;
    public Transform tr;
    public GameObject Cam = null;
    private Vector3 MoveDir = new Vector3(0.0f, 0.0f, 0.0f);
    public bool _portalCheck = false;
    private bool isBorder;
    public bool _goalCheck = false;
    public bool _gameReady = false;
    public float Dist;
    Vector3 gate3location = new Vector3(415.91f, 30.86997f, 150.0001f);
    int rank = 0;
    public enum moveState
    {
        Moving,
        Idle,
    }   
    public enum modeState
    {
        Square,
        Game,
        Voting,
    }
    public moveState _move = moveState.Idle;
    public modeState _mode = modeState.Square;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        tr = GetComponent<Transform>();
        if (photonView.IsMine)
        {
            this.name = "@Player";
            // Camera.main.GetComponent<SmoothFollow>().target = tr;
            // DontDestroyOnLoad(Camera.main);
            Camera.main.GetComponent<CameraAutoFocus>().target = tr;
            Cam = Camera.main.gameObject;

            if (SceneManager.GetActiveScene().name == "Square")
            {
                Cam.GetComponent<AudioSource>().clip = Managers.Resource.Load<AudioClip>("BGM/Ghostrifter-Official-Soaring");
                Cam.GetComponent<AudioSource>().loop = true;
                Cam.GetComponent<AudioSource>().Play();
            }
            else if (SceneManager.GetActiveScene().name == "Voting")
            {
                Cam.GetComponent<AudioSource>().clip = Managers.Resource.Load<AudioClip>("BGM/fm-freemusic-cheerful-whistling");
                Cam.GetComponent<AudioSource>().loop = true;
                Cam.GetComponent<AudioSource>().Play();
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                Cam.GetComponent<AudioSource>().clip = Managers.Resource.Load<AudioClip>("BGM/alex-productions-happy-and-fun-background-music");
                Cam.GetComponent<AudioSource>().loop = true;
                Cam.GetComponent<AudioSource>().Play();
            }

            Managers.Input.KeyAction -= OnKeyBoard;
            Managers.Input.KeyAction += OnKeyBoard;
        
            rigid = GetComponent<Rigidbody>();
        }
        else
            return;
    }

    void UpdateMoving()
    {
        //moving mode

        // if (Input.GetKey(KeyCode.W))
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _roatationSpeed);
        //     transform.position += Vector3.forward * Time.deltaTime * _speed;
        // }
            
        // if (Input.GetKey(KeyCode.S))
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _roatationSpeed);
        //     transform.position += Vector3.back * Time.deltaTime * _speed;
        // }
            
        // if (Input.GetKey(KeyCode.A))
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _roatationSpeed);
        //     transform.position += Vector3.left * Time.deltaTime * _speed;
        // }
            
        // if (Input.GetKey(KeyCode.D))
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _roatationSpeed);
        //     transform.position += Vector3.right * Time.deltaTime * _speed;
        // }
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if(Cam == null) return;
            var offset = Cam.transform.forward;
            offset.y = 0;
            transform.LookAt(transform.position + offset);
            // if (Input.GetKey(KeyCode.W))
            // {
            //     var offset = Cam.transform.forward;
            //     offset.y = 0;
            //     transform.LookAt(transform.position + offset);
            // }
            
            // if (Input.GetKey(KeyCode.A))
            // {
            //     var offset = Cam.transform.forward;
            //     offset.y = 0;
            //     offset.x -= 90;
            //     transform.LookAt(transform.position + offset);
            // }
            // if (Input.GetKey(KeyCode.S))
            // {
            //     var offset = Cam.transform.forward;
            //     offset.y = 0;
            //     offset.z -= 180;
            //     transform.LookAt(transform.position + offset);
            // }
            // if (Input.GetKey(KeyCode.D))
            // {
            //     var offset = Cam.transform.forward;
            //     offset.y = 0;
            //     offset.x += 90;
            //     transform.LookAt(transform.position + offset);
            // }
        }

        // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정합니다.
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        MoveDir = transform.TransformDirection(MoveDir);

        if(!isBorder)
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveDir), _roatationSpeed);
            transform.position += MoveDir * _speed * Time.deltaTime;

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 3);
        anim.SetBool("jump", IsJumping);
        _move = moveState.Idle;
    }

    void UpdateIdle()
    {
        // idle idle
        Animator anim = GetComponent<Animator>();

        anim.SetFloat("speed", 0);
        anim.SetBool("jump", IsJumping);
    }

    void UpdateJumping()
    {
        if (photonView.IsMine)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!IsJumping)
                {
                    // jumping mode
                    if (rigid == null) return;
                    IsJumping = true;
                    rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                    Animator anim = GetComponent<Animator>();
                    anim.SetBool("jump", IsJumping);
                }
                else
                    return;
            }
            else
                IsJumping = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (photonView.IsMine)
        // {
            if (collision.gameObject.CompareTag("Untagged"))
            {
                IsJumping = false;
                Animator anim = GetComponent<Animator>();
                anim.SetBool("jump", IsJumping);
            }
        // }
    }
    
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
    }
    void StopToObstacle()
    {
        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Obstacle"));
    }

    void OnKeyBoard()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                _move = moveState.Moving;
            }
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            switch(_move)
            {
                case moveState.Idle:
                    UpdateIdle();
                    break;
                case moveState.Moving:
                    UpdateMoving();
                    break;
            }
            UpdateJumping();
            if(SceneManager.GetActiveScene().name.Equals("Square"))
            {
                _mode = modeState.Square;
                StopToWall();
                _gameReady = false;
            }
            else if(SceneManager.GetActiveScene().name.Equals("Voting"))
            {
                _mode = modeState.Voting;
                StopToWall();
                _gameReady = false;
            }
            else if(SceneManager.GetActiveScene().name.Equals("Game"))
            {
                _mode = modeState.Game;
                photonView.RPC("gameMode", RpcTarget.All);
                if (GameManagerEx.Instance.getGameStart())
                {
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    photonView.RPC("setDistChange", RpcTarget.All);
                    if (GameManagerEx.Instance.getGameFinished()) Managers.Input.KeyAction -= OnKeyBoard;
                }
                else
                {
                    if (_gameReady)
                    {
                        photonView.RPC("setReady", RpcTarget.All);
                    }
                    else
                    {
                        photonView.RPC("setUnReady", RpcTarget.All);
                    }
                }
                StopToObstacle();
            }
        }
    }

    void OnDestroy() {
        Managers.Input.KeyAction -= OnKeyBoard;
    }
    
    [PunRPC]
    void gameMode()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        _speed = 7.0f;
        JumpPower = 1.0f;
    }

    [PunRPC]
    void setReady()
    {
        _gameReady = true;
        // Debug.Log("전부 ready 송신");
    }

    [PunRPC]
    void setUnReady()
    {
        _gameReady = false;
        // Debug.Log("전부 unReady 송신");
    }

    // [PunRPC]
    // public void closeGameRoom()
    // {
    //     PhotonNetwork.CurrentRoom.IsOpen = false;
    //     // Debug.Log("현재 게임 방 닫힘");
    // }

    [PunRPC]
    void setDistChange()
    {
        Dist = Vector3.Distance(transform.position, gate3location);
    }

    [PunRPC]
    void setGoalCheckTrue()
    {
        _goalCheck = true;
    }

    [PunRPC]
    void setGoalCheckFalse()
    {
        _goalCheck = false;
    }

    public float getDist()
    {
        return Dist;
    }

    public void setRank(int changeRank)
    {
        rank = changeRank;
    }

    public int getRank()
    {
        return rank;
    }
}