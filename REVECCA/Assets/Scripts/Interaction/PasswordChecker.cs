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
        // npcObject가 null이 아닌 경우에만 F키 입력을 확인
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
                Debug.Log("암호입력 중");
                // 숫자 키 입력 감지 및 4자리까지만 입력 허용
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

                UpdateDisplay(); // 입력 디스플레이 업데이트
            }
        }

        // Backspace 키를 누를 경우 마지막 숫자 삭제
        if (Input.GetKeyDown(KeyCode.Backspace) && inputPassword.Length > 0)
        {
            inputPassword = inputPassword.Substring(0, inputPassword.Length - 1);
            UpdateDisplay(); // 입력 디스플레이 업데이트
        }

        // Enter 또는 Return 키를 누를 경우 암호 확인
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckPassword();
            // inputPassword = ""; // 입력 초기화 제거
            UpdateDisplay(); // 입력 디스플레이 업데이트
        }
    }

    void CheckPassword()
    {
        // 암호 확인 로직
        if (inputPassword == correctPassword && isQuestCompleted)
        {
            resultText.text = "암호가 정확합니다!";
            resultText.color = Color.green;
            sucessObject.SetActive(false);
            ProceedQuest();
        }
        else if (inputPassword.Length >= correctPassword.Length && isQuestCompleted)
        {
            resultText.text = "암호가 틀렸습니다.";
            resultText.color = Color.red;
        }
    }

    void UpdateDisplay()
    {
        // 입력된 암호 디스플레이 업데이트
        inputDisplayText.text = inputPassword;
    }

    protected override bool CheckQuestCompletion()
    {
        // 퀘스트 완료 여부를 확인하는 로직을 구현하세요
        // 예를 들어:
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
        inputPassword.characterLimit = 4; // 4자리로 제한
        //inputPassword.contentType = TMP_inputPassword.ContentType.IntegerNumber; // 숫자만 입력 가능하도록 설정
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
        // npcObject가 null이 아닌 경우에만 F키 입력을 확인
        if (isQuestCompleted && Input.GetKeyDown(KeyCode.F))
        {
            GameManager.instance.ItemOff();
            isGetInteraction = true;
            Debug.Log(isGetInteraction);
            if (Input.GetKeyDown(KeyCode.F) && isGetInteraction)
            {
                interactableObject.SetActive(true);
                inputPassword.ActivateInputField(); // 입력 필드 활성화
            }
        }
    }

    public void OnPasswordChanged(string password)
    {
        Debug.Log("암호 입력 중");
        // 입력된 암호가 4자리를 넘지 않도록 제한
        if (password.Length > 4)
        {
            inputPassword.text = password.Substring(0, 4);
        }
    }

    public void OnPasswordEntered(string password)
    {
        Debug.Log("암호 입력 중");
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckPassword(password);
        }
    }

    void CheckPassword(string password)
    {
        // 암호 확인 로직
        if (password == correctPassword && isQuestCompleted)
        {
            resultText.text = "암호가 정확합니다!";
            resultText.color = Color.green;
            //successObject.SetActive(false);
            ProceedQuest();
        }
        else
        {
            resultText.text = "암호가 틀렸습니다.";
            resultText.color = Color.red;
        }
    }

    protected override bool CheckQuestCompletion()
    {
        // 퀘스트 완료 여부를 확인하는 로직을 구현하세요
        // 예를 들어:
        return isQuestCompleted;
    }
}
