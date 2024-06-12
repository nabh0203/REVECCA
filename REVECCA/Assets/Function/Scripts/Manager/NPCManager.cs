/*using UnityEngine;
using TMPro;


public class NPCManager : MonoBehaviour
{
    public string dialogue; // NPC�� ���
    public TextMeshProUGUI dialogueText;
    public GameObject NPCtexbBox; // ��縦 ����� UI Text ������Ʈ

    private void Start()
    {
        NPCtexbBox.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �±׸� ���� ������Ʈ�� �浹���� ��
        if (other.CompareTag("Player"))
        {
            NPCtexbBox.SetActive(true);
            // ��縦 ���
            DisplayDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ Ʈ���Ÿ� ����� �� ��� �����
        if (other.CompareTag("Player"))
        {
            NPCtexbBox.SetActive(false);
            HideDialogue();
        }
    }

    private void DisplayDialogue()
    {
        // UI �ؽ�Ʈ ������Ʈ�� ��縦 ���
        dialogueText.text = dialogue;
        dialogueText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
    }
}
*/

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCManager : Interactable
{
    public List<string> dialogueList; // NPC�� ��ȭ ���
    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private GameObject npcObject; // ���� ��ȣ�ۿ� ���� NPC ������Ʈ
    public GameObject nextNPC;
    public bool foodTrigger = false;

    protected override void OnInteract()
    {
        npcObject = gameObject; // ���� ��ȣ�ۿ� ���� NPC ������Ʈ ����
    }

    protected override void OnExit()
    {
        
        npcObject = null; // ��ȣ�ۿ��� �������Ƿ� npcObject�� null�� �ʱ�ȭ
        HideDialogue();
    }

    protected override bool CheckQuestCompletion()
    {
        // ����Ʈ �Ϸ� ���θ� Ȯ���ϴ� ������ �����ϼ���
        // ���� ���:
        isQuestCompleted = true;
        return isQuestCompleted;
    }

    private void Update()
    {
        // npcObject�� null�� �ƴ� ��쿡�� FŰ �Է��� Ȯ��
        if (npcObject != null && Input.GetKeyDown(KeyCode.F))
        {
            GameManager.instance.ItemOff();
            isGetInteraction = true;
            Debug.Log(isGetInteraction);
            if (Input.GetKeyDown(KeyCode.F) && isGetInteraction)
            {
                DisplayNextDialogue();
            }
        }
    }

    private void DisplayNextDialogue()
    {
        interactableObject.SetActive(true);
        // ��ȭ ����� ���� ��ȭ�� ���
        if (currentDialogueIndex < dialogueList.Count)
        {
            dialogueText.text = dialogueList[currentDialogueIndex];
            dialogueText.gameObject.SetActive(true);
            currentDialogueIndex++;
            Debug.Log(currentDialogueIndex);
        }
        else 
        {
            CheckQuestCompletion();
            // ��� ��ȭ�� ��µǾ��ٸ� ��ȭ ����
            HideDialogue();
        }
    }

    public void HideDialogue()
    {
        Debug.Log("��ȭ ����");
        dialogueText.gameObject.SetActive(false);
        currentDialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
        ProceedQuest();
        foodTrigger = true;
        if (nextNPC != null)
        {
            NextNPC();
        }
        else
        {
            return;
        }
    }

    private void NextNPC()
    {
        nextNPC.SetActive(true);
    }
}


