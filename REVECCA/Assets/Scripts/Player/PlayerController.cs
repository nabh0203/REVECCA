using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody rb; // Rigidbody 컴포넌트
    public Transform cam; // 카메라 Transform

    public float speed = 6.0f; // 이동 속도
    public float turnSmoothTime = 0.1f; // 회전이 부드럽게 이루어지는 시간
    float turnSmoothVelocity; // 부드러운 회전을 위한 변수

    private GameObject currentItem = null; // 현재 가까이 있는 아이템
    private bool isCrouching = false; // 앉기 상태를 나타내는 변수
    [SerializeField]
    private float originalHeight; // 원래 캐릭터 높이
    [SerializeField]
    private float crouchHeight = 0.5f; // 앉기 시 캐릭터 높이
    private bool isSound;

    public AudioClip Walk;
    private AudioSource WalkSound;


    [Header("OutLine")]
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();
    public Material outline;


    void Start()
    {
        WalkSound = GetComponent<AudioSource>();
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();

        WalkSound.loop = true;
        WalkSound.clip = Walk;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentItem != null)
            {
                GameManager.instance.ActivateTextOff();
                GameManager.instance.DeactivateItem(currentItem);
                currentItem = null; // 참조 제거
            }
            else
            {
                // 힌트 활성화
                GameManager.instance.ActivateHint();
            }

            if (isSound)
            {
                AudioManagerNBH.Audioinstance.PlaySFX(AudioManagerNBH.PlayerSFX.Click);
            }
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

        if (direction != Vector3.zero && !WalkSound.isPlaying)
        {
            WalkSound.Play();
        }
        else if (direction == Vector3.zero)
        {
            WalkSound.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Item":
                GameManager.instance.ActivateTextOn();
                currentItem = other.gameObject; // 현재 아이템 설정
                Debug.Log("아이템과 상호작용 충돌");
                SetRendererMaterials(other);
                isSound = true;
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOn();
                Debug.Log("상호작용 충돌");
                SetRendererMaterials(other);
                isSound = true;
                break;
            case "newsPaper":
                GameManager.instance.ActivateTextOn();
                Debug.Log("상호작용 충돌");
                SetRendererMaterials(other);
                GameManager.instance.isHints[0] = true; // 신문 힌트 활성화
                isSound = true;
                //HintObject.instance.ActivateHint();
                break;
            case "mail":
                GameManager.instance.ActivateTextOn();
                Debug.Log("상호작용 충돌");
                SetRendererMaterials(other);
                GameManager.instance.isHints[1] = true; // 신문 힌트 활성화
                isSound = true;
                //HintObject.instance.ActivateHint();
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Item":
                GameManager.instance.ActivateTextOff();
                Debug.Log("아이템과 상호작용 충돌");
                CloseRendererMaterials(other);
                isSound = false;
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOff();
                Debug.Log("상호작용 충돌");
                CloseRendererMaterials(other);
                isSound = false;
                break;
            case "newsPaper":
                GameManager.instance.ActivateTextOff();
                Debug.Log("상호작용 충돌");
                CloseRendererMaterials(other);
                GameManager.instance.isHints[0] = false;
                isSound = false;
                break;
            case "mail":
                GameManager.instance.ActivateTextOff();
                Debug.Log("상호작용 충돌");
                CloseRendererMaterials(other);
                GameManager.instance.isHints[1] = false;
                isSound = false;
                break;
            default:
                break;
        }
    }
    private void SetRendererMaterials(Collider other)
    {
        Renderer renderers = other.gameObject.GetComponent<Renderer>();

        if (renderers != null)
        {
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
    }
    private void CloseRendererMaterials(Collider other)
    {
        Renderer renderer = other.gameObject.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);
        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();
    }
}
