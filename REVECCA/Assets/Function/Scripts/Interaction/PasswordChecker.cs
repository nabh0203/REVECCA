using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;

public class PasswordChecker : Interactable
{
    [SerializeField]
    private int SoundIndex;
    public DOTweenAnimation passwordAnimation; // ù ��° ���� ���� DOTweenAnimation ����
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
        inputPassword.characterLimit = 4; // 4�ڸ��� ����
        //inputPassword.contentType = TMP_inputPassword.ContentType.IntegerNumber; // ���ڸ� �Է� �����ϵ��� ����
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
        // npcObject�� null�� �ƴ� ��쿡�� FŰ �Է��� Ȯ��
        if (isQuestCompleted && Input.GetKeyDown(KeyCode.F) && isopen)
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
