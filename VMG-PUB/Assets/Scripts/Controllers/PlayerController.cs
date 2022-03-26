using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utility;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float _speed = 3.0f;
    [SerializeField]
    private float JumpPower = 0.4f;
    [SerializeField]
    private float _roatationSpeed = 0.07f;
    private Rigidbody rigid;
    public bool IsJumping = false;
    private Transform tr;
    public GameObject Cam;
    private Vector3 MoveDir;
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
    moveState _state = moveState.Idle;

    void Start()
    {
        tr = GetComponent<Transform>();
        if (photonView.IsMine)
        {
            // Camera.main.GetComponent<SmoothFollow>().target = tr;
            Camera.main.GetComponent<CameraAutoFocus>().target = tr;
            Cam = Camera.main.gameObject;
            Managers.Input.KeyAction -= OnKeyBoard;
            Managers.Input.KeyAction += OnKeyBoard;
        
            rigid = GetComponent<Rigidbody>();
        }
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
        
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정합니다.
        MoveDir = transform.TransformDirection(MoveDir);

        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveDir), _roatationSpeed);
        transform.position += MoveDir * _speed * Time.deltaTime;

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 3);
        anim.SetBool("jump", IsJumping);
        _state = moveState.Idle;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsJumping)
            {
                // jumping mode
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

    private void OnCollisionEnter(Collision collision) {
        if (photonView.IsMine)
        {
            if (collision.gameObject.CompareTag("Untagged"))
            {
                IsJumping = false;
                Animator anim = GetComponent<Animator>();
                anim.SetBool("jump", IsJumping);
            }
        }
    }

    void OnKeyBoard()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                _state = moveState.Moving;
            }
        }   
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            switch(_state)
            {
                case moveState.Idle:
                    UpdateIdle();
                    break;
                case moveState.Moving:
                    UpdateMoving();
                    break;
            }
            UpdateJumping();
        }
    }
}
