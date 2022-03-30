using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAutoFocus : MonoBehaviour
{
    // [SerializeField]
    // Define.CameraMode _mode = Define.CameraMode.QuarterView;
    // [SerializeField]
    // Vector3 _delta = new Vector3(0.0f, 4.6f, -5.3f);
    // [SerializeField]
    // GameObject _player = null;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void LateUpdate()
    // {
    //     if (_mode == Define.CameraMode.QuarterView)
    //     {
    //         RaycastHit hit;
    //         if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
    //         {
    //             float dist = (hit.point - _player.transform.position).magnitude * 0.5f;
    //             transform.position = _player.transform.position + _delta.normalized * dist;
    //         }
    //         else
    //         {
    //             transform.position = _player.transform.position + _delta;
    //             transform.LookAt(_player.transform);
    //         }
    //     }
    // }

    // public void SetQuaterView(Vector3 delta)
    // {
    //     _mode = Define.CameraMode.QuarterView;
    //     _delta = delta;
    // }
    //추적할 대상
    //추적할 대상
    public Transform target;
    //카메라와의 거리   
    public float dist = 2f;

    //카메라 회전 속도
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    //카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    //y값 제한
    public float yMinLimit = 13f;
    public float yMaxLimit = 80f;

    //앵글의 최소,최대 제한
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    //---------------------------------------------------------------------------------void Start-----------------------------------------------
    // Use this for initialization
    void Start()
    {
        //커서 숨기기"//"를 지우세요
        // Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

     }


    //--------------------------------------------------------------------------------void update-----------------------------------------------
    //Update is called once per frame
    void Update()
    {

    }

    //---------------------------------------------------------------------------------LateUpdate=

    void LateUpdate()
    {
        if (target && (SceneManager.GetActiveScene().name == "Square" || SceneManager.GetActiveScene().name == "Voting"))
        {
            //마우스 스크롤과의 거리계산
            dist -= 1 * Input.mouseScrollDelta.y;

            //마우스 스크롤했을경우 카메라 거리의 Min과Max
            if (dist < 2f)
            {
                dist = 2f;

            }

            if (dist >= 7f)
            {
                dist = 7f;
            }

            //카메라 회전속도 계산
            x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

            //앵글값 정하기
            //y값의 Min과 MaX 없애면 y값이 360도 계속 돎
            //x값은 계속 돌고 y값만 제한
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            //카메라 위치 변화 계산
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

            transform.rotation = rotation;
            transform.position = position;
        }
        else if (target && SceneManager.GetActiveScene().name == "Game")
        {
            //마우스 스크롤과의 거리계산
            dist -= 1 * Input.mouseScrollDelta.y;

            //마우스 스크롤했을경우 카메라 거리의 Min과Max
            if (dist < 4f)
            {
                dist = 4f;

            }

            if (dist >= 10f)
            {
                dist = 10f;
            }

            //카메라 회전속도 계산
            x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

            //앵글값 정하기
            //y값의 Min과 MaX 없애면 y값이 360도 계속 돎
            //x값은 계속 돌고 y값만 제한
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            //카메라 위치 변화 계산
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
