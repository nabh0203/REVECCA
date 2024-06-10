/*using UnityEngine;
using TMPro;

public class PasswordChecker : MonoBehaviour
{
    public TextMeshProUGUI resultText; // 결과를 출력하는 Text
    public TextMeshProUGUI inputDisplayText; // 사용자 입력을 실시간으로 보여주는 Text
    public GameObject password;

    private string inputPassword = ""; // 사용자가 입력한 암호
    private string correctPassword = "1234"; // 올바른 암호

    private void Start()
    {
        password.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            password.SetActive(true);
            inputPassword = ""; // 플레이어가 트리거 영역에 들어올 때마다 암호 입력 초기화
            UpdateDisplay(); // 입력 디스플레이 업데이트
        }
    }

    private void OnTriggerExit(Collider other)
    {
        password.SetActive(false);
    }

    void Update()
    {
        // 입력 처리 로직을 Update 메서드로 이동
        HandleInput();
    }

    void HandleInput()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                // 숫자 키 입력 감지
                if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
                {
                    inputPassword += (char)('0' + (keyCode - KeyCode.Alpha0));
                }
                else if (keyCode >= KeyCode.Keypad0 && keyCode <= KeyCode.Keypad9)
                {
                    inputPassword += (char)('0' + (keyCode - KeyCode.Keypad0));
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
            inputPassword = ""; // 암호 입력 초기화
            UpdateDisplay(); // 입력 디스플레이 업데이트
        }
    }

    void CheckPassword()
    {
        // 암호를 확인하고 결과를 출력
        if (inputPassword == correctPassword)
        {
            resultText.text = "암호가 정확합니다!";
            resultText.color = Color.green;
        }
        else if (inputPassword.Length >= correctPassword.Length)
        {
            resultText.text = "암호가 틀렸습니다.";
            resultText.color = Color.red;
        }
    }

    void UpdateDisplay()
    {
        // 입력된 암호를 디스플레이에 표시
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
}
