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
        if (photonView.IsMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = GetComponent<Transform>();
            Managers.Input.KeyAction -= OnKeyBoard;
            Managers.Input.KeyAction += OnKeyBoard;
        
            rigid = GetComponent<Rigidbody>();
        }
    }

    void UpdateMoving()
    {
        //moving mode

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _roatationSpeed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
            
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _roatationSpeed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
            
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _roatationSpeed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
            
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _roatationSpeed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        

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
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
            Animator anim = GetComponent<Animator>();
            anim.SetBool("jump", IsJumping);
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
