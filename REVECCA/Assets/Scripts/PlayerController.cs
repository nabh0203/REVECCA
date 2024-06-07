using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb; // Rigidbody 컴포넌트
    public Transform cam; // 카메라 Transform
    public bool isGetItem = false;

    public float speed = 6.0f; // 이동 속도
    public float turnSmoothTime = 0.1f; // 회전이 부드럽게 이루어지는 시간
    float turnSmoothVelocity; // 부드러운 회전을 위한 변수

    private GameObject currentItem = null; // 현재 가까이 있는 아이템
    private bool isCrouching = false; // 앉기 상태를 나타내는 변수
    [SerializeField]
    private float originalHeight; // 원래 캐릭터 높이
    [SerializeField]
    private float crouchHeight = 0.5f; // 앉기 시 캐릭터 높이

    void Start()
    {
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentItem != null)
        {
            GameManager.instance.ActivateTextOff();
            GameManager.instance.DeactivateItem(currentItem);
            isGetItem = true;
            currentItem = null; // 참조 제거
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching; // 앉기 상태 토글
            if (isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z);
            }
        }
    }
    void FixedUpdate()
    {
        // 플레이어 입력 받기
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // 입력이 있을 때만 이동 및 회전 실행
        if (direction.magnitude >= 0.1f)
        {
            // 목표 회전 각도 계산 (카메라 회전 각도 기준)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // 부드러운 회전 처리
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // 이동 방향 계산 (회전 각도 반영)
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Rigidbody를 이용한 캐릭터 이동
            rb.MovePosition(transform.position + moveDir.normalized * speed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            GameManager.instance.ActivateTextOn();
            currentItem = other.gameObject; // 현재 아이템 설정
            Debug.Log("충돌");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            // 아이템과의 충돌이 끝나면 텍스트를 비활성화합니다.
            GameManager.instance.ActivateTextOff();
            isGetItem = false;
            Debug.Log("충돌안함");
        }
    }
}
