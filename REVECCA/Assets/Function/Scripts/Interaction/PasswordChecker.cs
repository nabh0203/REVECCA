/*using UnityEngine;
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
}*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordChecker : Interactable
{
    public TextMeshProUGUI resultText;
    public TMP_InputField inputPassword;
    //public GameObject successObject;

    [SerializeField]
    private string correctPassword = "1234";

    protected override void Start()
    {
        dialogueText = null;
        base.Start();
        inputPassword.characterLimit = 4; // 4�ڸ��� ����
        //inputPassword.contentType = TMP_inputPassword.ContentType.IntegerNumber; // ���ڸ� �Է� �����ϵ��� ����
        inputPassword.onValueChanged.AddListener(OnPasswordChanged);
        inputPassword.onEndEdit.AddListener(OnPasswordEntered);

    }

    protected override void OnInteract()
    {
        isQuestCompleted = true;
        inputPassword.gameObject.SetActive(true);
    }

    protected override void OnExit()
    {
        isQuestCompleted = false;
        inputPassword.gameObject.SetActive(false);
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
                interactableObject.SetActive(true);
                inputPassword.ActivateInputField(); // �Է� �ʵ� Ȱ��ȭ
            }
        }
    }

    public void OnPasswordChanged(string password)
    {
        Debug.Log("��ȣ �Է� ��");
        // �Էµ� ��ȣ�� 4�ڸ��� ���� �ʵ��� ����
        if (password.Length > 4)
        {
            inputPassword.text = password.Substring(0, 4);
        }
    }

    public void OnPasswordEntered(string password)
    {
        Debug.Log("��ȣ �Է� ��");
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckPassword(password);
        }
    }

    void CheckPassword(string password)
    {
        // ��ȣ Ȯ�� ����
        if (password == correctPassword && isQuestCompleted)
        {
            resultText.text = "��ȣ�� ��Ȯ�մϴ�!";
            resultText.color = Color.green;
            //successObject.SetActive(false);
            ProceedQuest();
        }
        else
        {
            resultText.text = "��ȣ�� Ʋ�Ƚ��ϴ�.";
            resultText.color = Color.red;
        }
    }

    protected override bool CheckQuestCompletion()
    {
        // ����Ʈ �Ϸ� ���θ� Ȯ���ϴ� ������ �����ϼ���
        // ���� ���:
        return isQuestCompleted;
    }
}
