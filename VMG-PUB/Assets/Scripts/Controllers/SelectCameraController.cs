using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCameraController : MonoBehaviour
{
    public static SelectCameraController Instance;
    public RaycastHit hit;
    public bool zoom = false;
    public string selectCharacterName = null;

    public GameObject[] character = new GameObject [28];
    public Vector3 defaultPosition = new Vector3(0.0f, 4.0f, 22.0f);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < character.Length + 1; i++)
        {
            character[i-1] = GameObject.Find("C" + i);
            Debug.Log(i+ "번째 성공");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!UI_CharacterSelect.Instance.yesButton.IsActive() && !UI_SelectInfoInput.Instance.nextButton.IsActive())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    selectCharacterName = hit.collider.gameObject.name;
                    Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
                    if (hit.collider.gameObject.CompareTag("Character"))
                    {
                        hit.collider.gameObject.GetComponent<SelectCharacterController>().clicked = true;
                        zoom = true;
                        
                        for (int i = 0; i < character.Length; i++)
                            character[i].SetActive(false);

                        hit.collider.gameObject.SetActive(true);
                        // UI_CharacterSelect.Instance.yesButton.gameObject.SetActive(true);
                        // UI_CharacterSelect.Instance.NoButton.gameObject.SetActive(true);
                        UI_CharacterSelect.Instance.ShowOn();
                        UI_CharacterSelect.Instance.selectCharacterName = selectCharacterName;
                        UI_SelectInfoInput.Instance.selectCharacterName = selectCharacterName;
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        if (selectCharacterName == null)
        {
            transform.position = defaultPosition;
        }
        else
        {
            if (GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().selected)
            {
                infoCamMove();
            }
            else if (zoom == true)
            {
                clickCharacterCamMove();
            }
        }
    }

    public void restoreCam()
    {
        GameObject.Find(selectCharacterName).GetComponent<SelectCharacterController>().clicked = false;
        zoom = false;

        for (int i = 0; i < character.Length; i++)
            character[i].SetActive(true);
    }

    public void clickCharacterCamMove()
    {
        transform.position = new Vector3(GameObject.Find(selectCharacterName).transform.position.x, hit.collider.gameObject.transform.position.y + 0.8f, 5.0f);
    }

    public void infoCamMove()
    {
        transform.position = Vector3.Lerp(transform.position, 
        new Vector3(GameObject.Find(selectCharacterName).transform.position.x - 0.83f, hit.collider.gameObject.transform.position.y + 0.8f, 5.0f), Time.deltaTime * 1.5f);
    }
}
