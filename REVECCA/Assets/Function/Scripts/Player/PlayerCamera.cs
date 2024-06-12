using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //ī�޶��� ȸ���� �����ϴ� ��ũ��Ʈ.
    // Start is called before the first frame update
    private Camera cam;
    private float rotAroundX, rotAroundY;
    [Tooltip("����")]
    public float sensitivity = 1; //���� ����
    // Start is called before the first frame update
    void Start()
    {
        //���Ʒ��¿� ���������� ���� ���� ����
        cam = GetComponent<Camera>();
        rotAroundX = transform.eulerAngles.x;
        rotAroundY = transform.eulerAngles.y;

        // Ŀ�� �����
        Cursor.visible = false;
        // Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //���콺 ��ǥ ���� & ī�޶��� ���� ����
        rotAroundX += Input.GetAxis("Mouse Y") * sensitivity;
        rotAroundY += Input.GetAxis("Mouse X") * sensitivity;

        //ī�޶� ���� �ε巴�� ����
        rotAroundX = Mathf.Clamp(rotAroundX, -60, 60);

        CameraRotation();
    }

    public void CameraRotation()
    {
        transform.parent.rotation = Quaternion.Euler(0, rotAroundY, 0); //PlayerControlle(r(�θ�)�� ȸ��
        cam.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0); //ī�޶��� ȸ��
    }
}
