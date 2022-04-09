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
            Managers.Input.KeyAction -= OnKeyBoard;
            Managers.Input.KeyAction += OnKeyBoard;
        
            rigid = GetComponent<Rigidbody>();
        }
        else
            return;
    }

    [PunRPC]
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

    [PunRPC]
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
        isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Obstacle"));
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
                    photonView.RPC("UpdateMoving", RpcTarget.All);
                    break;
            }
            UpdateJumping();
            photonView.RPC("UpdateJumping", RpcTarget.All);
            if(SceneManager.GetActiveScene().name.Equals("Square"))
            {
                StopToWall();
                _mode = modeState.Square;
            }
            else if(SceneManager.GetActiveScene().name.Equals("Voting"))
            {
                StopToWall();
                _mode = modeState.Voting;
            }
            else if(SceneManager.GetActiveScene().name.Equals("Game"))
            {
                _mode = modeState.Game;
                // transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                // this._speed = 10.0f;
                // this.JumpPower = 2.0f;
                photonView.RPC("gameMode", RpcTarget.All);
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
        _speed = 5.0f;
        JumpPower = 1.0f;
    }
}
