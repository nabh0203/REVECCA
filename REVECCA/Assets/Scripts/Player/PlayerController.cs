using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && other.CompareTag("Interaction"))
        {
            GameManager.instance.ActivateTextOn();
            currentItem = other.gameObject; // ���� ������ ����
            Debug.Log("�浹");
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Item":
                GameManager.instance.ActivateTextOn();
                currentItem = other.gameObject; // ���� ������ ����
                Debug.Log("�����۰� ��ȣ�ۿ� �浹");
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOn();
                
                Debug.Log("��ȣ�ۿ� �浹");
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("Item") && other.CompareTag("Interaction"))
        {
            // �����۰��� �浹�� ������ �ؽ�Ʈ�� ��Ȱ��ȭ�մϴ�.
            GameManager.instance.ActivateTextOff();
            isGetItem = false;
            Debug.Log("�浹����");
        }*/

        switch (other.tag)
        {
            case "Item":
                GameManager.instance.ActivateTextOff();
                Debug.Log("�����۰� ��ȣ�ۿ� �浹");
                break;
            case "Interaction":
                GameManager.instance.ActivateTextOff();
                Debug.Log("��ȣ�ۿ� �浹");
                break;
            default:
                break;
        }
    }
}
