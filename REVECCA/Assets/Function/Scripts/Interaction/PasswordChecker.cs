using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;

public class PasswordChecker : Interactable
{
    [SerializeField]
    private int SoundIndex;
    public DOTweenAnimation passwordAnimation; // 첫 번째 문에 대한 DOTweenAnimation 참조
    public GameObject Passwordobject;
    public Iteminteraction iteminteraction;
    public TextMeshProUGUI resultText;
    public TMP_InputField inputPassword;
    private bool isopen;
    
    //public GameObject successObject;

    [SerializeField]
    private string correctPassword = "1234";

    protected override void Start()
    {
        isopen = true;
        dialogueText = null;
        base.Start();
        inputPassword.characterLimit = 4; // 4자리로 제한
        //inputPassword.contentType = TMP_inputPassword.ContentType.IntegerNumber; // 숫자만 입력 가능하도록 설정
        inputPassword.onValueChanged.AddListener(OnPasswordChanged);
        inputPassword.onEndEdit.AddListener(OnPasswordEntered);
        passwordAnimation = Passwordobject.GetComponent<DOTweenAnimation>();
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
        if (isQuestCompleted && Input.GetKeyDown(KeyCode.F) && isopen)
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
            iteminteraction.PlaySoundByIndex(6);
            resultText.color = Color.green;
            //successObject.SetActive(false);
            ProceedQuest();
            passwordAnimation.DOPlay();
            iteminteraction.PlaySoundByIndex(SoundIndex);
            isopen = false;
        }
        else
        {
            iteminteraction.PlaySoundByIndex(5);
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
