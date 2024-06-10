/*using UnityEngine;
using TMPro;

public class PasswordChecker : MonoBehaviour
{
    public TextMeshProUGUI resultText; // ����� ����ϴ� Text
    public TextMeshProUGUI inputDisplayText; // ����� �Է��� �ǽð����� �����ִ� Text
    public GameObject password;

    private string inputPassword = ""; // ����ڰ� �Է��� ��ȣ
    private string correctPassword = "1234"; // �ùٸ� ��ȣ

    private void Start()
    {
        password.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            password.SetActive(true);
            inputPassword = ""; // �÷��̾ Ʈ���� ������ ���� ������ ��ȣ �Է� �ʱ�ȭ
            UpdateDisplay(); // �Է� ���÷��� ������Ʈ
        }
    }

    private void OnTriggerExit(Collider other)
    {
        password.SetActive(false);
    }

    void Update()
    {
        // �Է� ó�� ������ Update �޼���� �̵�
        HandleInput();
    }

    void HandleInput()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                // ���� Ű �Է� ����
                if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
                {
                    inputPassword += (char)('0' + (keyCode - KeyCode.Alpha0));
                }
                else if (keyCode >= KeyCode.Keypad0 && keyCode <= KeyCode.Keypad9)
                {
                    inputPassword += (char)('0' + (keyCode - KeyCode.Keypad0));
                }

                UpdateDisplay(); // �Է� ���÷��� ������Ʈ
            }
        }

        // Backspace Ű�� ���� ��� ������ ���� ����
        if (Input.GetKeyDown(KeyCode.Backspace) && inputPassword.Length > 0)
        {
            inputPassword = inputPassword.Substring(0, inputPassword.Length - 1);
            UpdateDisplay(); // �Է� ���÷��� ������Ʈ
        }

        // Enter �Ǵ� Return Ű�� ���� ��� ��ȣ Ȯ��
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckPassword();
            inputPassword = ""; // ��ȣ �Է� �ʱ�ȭ
            UpdateDisplay(); // �Է� ���÷��� ������Ʈ
        }
    }

    void CheckPassword()
    {
        // ��ȣ�� Ȯ���ϰ� ����� ���
        if (inputPassword == correctPassword)
        {
            resultText.text = "��ȣ�� ��Ȯ�մϴ�!";
            resultText.color = Color.green;
        }
        else if (inputPassword.Length >= correctPassword.Length)
        {
            resultText.text = "��ȣ�� Ʋ�Ƚ��ϴ�.";
            resultText.color = Color.red;
        }
    }

    void UpdateDisplay()
    {
        // �Էµ� ��ȣ�� ���÷��̿� ǥ��
        inputDisplayText.text = inputPassword;
    }
}
*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordChecker : Interactable
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI inputDisplayText;
    public TMP_InputField inputField;
    public GameObject sucessObject;
    private string inputPassword = "";
    [SerializeField]
    private string correctPassword = "1234";

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInteract()
    {
        isQuestCompleted = true;
    }

    protected override void OnExit()
    {
        isQuestCompleted = false;
    }

    void Update()
    {
        // npcObject�� null�� �ƴ� ��쿡�� FŰ �Է��� Ȯ��
        if (isQuestCompleted && Input.GetKeyDown(KeyCode.F))
        {
            GameManager.instance.ItemOff();
            isGetInteraction = true;
            Debug.Log(isGetInteraction);
            if (Input.GetKeyDown(KeyCode.F) && isGetInteraction)
            {
                HandleInput();
                
            }
        }
    }

    void HandleInput()
    {
        interactableObject.SetActive(true);
        inputPassword = "";
        UpdateDisplay();
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode) && isQuestCompleted)
            {
                Debug.Log("��ȣ�Է� ��");
                // ���� Ű �Է� ���� �� 4�ڸ������� �Է� ���
                if (inputPassword.Length < 4)
                {
                    if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
                    {
                        inputPassword += (char)('0' + (keyCode - KeyCode.Alpha0));
                    }
                    else if (keyCode >= KeyCode.Keypad0 && keyCode <= KeyCode.Keypad9)
                    {
                        inputPassword += (char)('0' + (keyCode - KeyCode.Keypad0));
                    }
                }

                UpdateDisplay(); // �Է� ���÷��� ������Ʈ
            }
        }

        // Backspace Ű�� ���� ��� ������ ���� ����
        if (Input.GetKeyDown(KeyCode.Backspace) && inputPassword.Length > 0)
        {
            inputPassword = inputPassword.Substring(0, inputPassword.Length - 1);
            UpdateDisplay(); // �Է� ���÷��� ������Ʈ
        }

        // Enter �Ǵ� Return Ű�� ���� ��� ��ȣ Ȯ��
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckPassword();
            // inputPassword = ""; // �Է� �ʱ�ȭ ����
            UpdateDisplay(); // �Է� ���÷��� ������Ʈ
        }
    }

    void CheckPassword()
    {
        // ��ȣ Ȯ�� ����
        if (inputPassword == correctPassword && isQuestCompleted)
        {
            resultText.text = "��ȣ�� ��Ȯ�մϴ�!";
            resultText.color = Color.green;
            sucessObject.SetActive(false);
            ProceedQuest();
        }
        else if (inputPassword.Length >= correctPassword.Length && isQuestCompleted)
        {
            resultText.text = "��ȣ�� Ʋ�Ƚ��ϴ�.";
            resultText.color = Color.red;
        }
    }

    void UpdateDisplay()
    {
        // �Էµ� ��ȣ ���÷��� ������Ʈ
        inputDisplayText.text = inputPassword;
    }

    protected override bool CheckQuestCompletion()
    {
        // ����Ʈ �Ϸ� ���θ� Ȯ���ϴ� ������ �����ϼ���
        // ���� ���:
        return isQuestCompleted; 
    }
}
