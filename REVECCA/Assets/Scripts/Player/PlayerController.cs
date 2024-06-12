using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody rb; // Rigidbody ������Ʈ
    public Transform cam; // ī�޶� Transform

    public float speed = 6.0f; // �̵� �ӵ�
    public float turnSmoothTime = 0.1f; // ȸ���� �ε巴�� �̷������ �ð�
    float turnSmoothVelocity; // �ε巯�� ȸ���� ���� ����

    private GameObject currentItem = null; // ���� ������ �ִ� ������
    private bool isCrouching = false; // �ɱ� ���¸� ��Ÿ���� ����
    [SerializeField]
    private float originalHeight; // ���� ĳ���� ����
    [SerializeField]
    private float crouchHeight = 0.5f; // �ɱ� �� ĳ���� ����
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
        // Rigidbody ������Ʈ ��������
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
                currentItem = null; // ���� ����
            }
            else
            {
                // ��Ʈ Ȱ��ȭ
                GameManager.instance.ActivateHint();
            }

            if (isSound)
            {
                AudioManagerNBH.Audioinstance.PlaySFX(AudioManagerNBH.PlayerSFX.Click);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching; // �ɱ� ���� ���
            if (isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z);
            }
        }

        // �÷��̾� �Է� �ޱ�
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // �Է��� ���� ���� �̵� �� ȸ�� ����
        if (direction.magnitude >= 0.1f)
        {
            // ��ǥ ȸ�� ���� ��� (ī�޶� ȸ�� ���� ����)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // �ε巯�� ȸ�� ó��
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // �̵� ���� ��� (ȸ�� ���� �ݿ�)
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Rigidbody�� �̿��� ĳ���� �̵�
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
                currentItem = other.gameObject; // ���� ������ ����
                Debug.Log("�����۰� ��ȣ�ۿ� �浹");
                SetRendererMaterials(other);
                isSound = true;
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOn();
                Debug.Log("��ȣ�ۿ� �浹");
                SetRendererMaterials(other);
                isSound = true;
                break;
            case "newsPaper":
                GameManager.instance.ActivateTextOn();
                Debug.Log("��ȣ�ۿ� �浹");
                SetRendererMaterials(other);
                GameManager.instance.isHints[0] = true; // �Ź� ��Ʈ Ȱ��ȭ
                isSound = true;
                //HintObject.instance.ActivateHint();
                break;
            case "mail":
                GameManager.instance.ActivateTextOn();
                Debug.Log("��ȣ�ۿ� �浹");
                SetRendererMaterials(other);
                GameManager.instance.isHints[1] = true; // �Ź� ��Ʈ Ȱ��ȭ
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
                Debug.Log("�����۰� ��ȣ�ۿ� �浹");
                CloseRendererMaterials(other);
                isSound = false;
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOff();
                Debug.Log("��ȣ�ۿ� �浹");
                CloseRendererMaterials(other);
                isSound = false;
                break;
            case "newsPaper":
                GameManager.instance.ActivateTextOff();
                Debug.Log("��ȣ�ۿ� �浹");
                CloseRendererMaterials(other);
                GameManager.instance.isHints[0] = false;
                isSound = false;
                break;
            case "mail":
                GameManager.instance.ActivateTextOff();
                Debug.Log("��ȣ�ۿ� �浹");
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
