using UnityEngine;
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
