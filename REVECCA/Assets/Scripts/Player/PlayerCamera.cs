using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //카메라의 회전을 조정하는 스크립트.
    // Start is called before the first frame update
    private Camera cam;
    private float rotAroundX, rotAroundY;
    [Tooltip("감도")]
    public float sensitivity = 1; //감도 조정
    // Start is called before the first frame update
    void Start()
    {
        //위아래좌우 방향조절을 위한 기초 구현
        cam = GetComponent<Camera>();
        rotAroundX = transform.eulerAngles.x;
        rotAroundY = transform.eulerAngles.y;

        // 커서 숨기기
        Cursor.visible = false;
        // 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 좌표 설정 & 카메라의 감도 조정
        rotAroundX += Input.GetAxis("Mouse Y") * sensitivity;
        rotAroundY += Input.GetAxis("Mouse X") * sensitivity;

        //카메라 범위 부드럽게 제한
        rotAroundX = Mathf.Clamp(rotAroundX, -60, 60);

        CameraRotation();
    }

    public void CameraRotation()
    {
        transform.parent.rotation = Quaternion.Euler(0, rotAroundY, 0); //PlayerControlle(r(부모)의 회전
        cam.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0); //카메라의 회전
    }
}
