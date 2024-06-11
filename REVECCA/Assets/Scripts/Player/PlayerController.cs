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


    [Header("OutLine")]
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();
    public Material outline;


    void Start()
    {

        // Rigidbody ������Ʈ ��������
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentItem != null)
        {
            GameManager.instance.ActivateTextOff();
            GameManager.instance.DeactivateItem(currentItem);
            
            currentItem = null; // ���� ����
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
    }
    void FixedUpdate()
    {
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
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOn();
                Debug.Log("��ȣ�ۿ� �浹");
                SetRendererMaterials(other);
                break;
            case "Hint":
                GameManager.instance.ActivateTextOn();
                Debug.Log("��ȣ�ۿ� �浹");
                SetRendererMaterials(other);
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
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOff();
                Debug.Log("��ȣ�ۿ� �浹");
                CloseRendererMaterials(other);
                break;
            case "Hint":
                GameManager.instance.ActivateTextOff();
                Debug.Log("��ȣ�ۿ� �浹");
                CloseRendererMaterials(other);
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
