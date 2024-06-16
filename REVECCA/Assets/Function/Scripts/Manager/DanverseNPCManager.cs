using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DanverseNPCManager : Interactable
{
    public List<string> dialogueList; // NPC�� ��ȭ ���
    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private GameObject npcObject; // ���� ��ȣ�ۿ� ���� NPC ������Ʈ

    protected override void OnInteract()
    {
        npcObject = gameObject; // ���� ��ȣ�ۿ� ���� NPC ������Ʈ ����
    }

    protected override void OnExit()
    {
        
        npcObject = null; // ��ȣ�ۿ��� �������Ƿ� npcObject�� null�� �ʱ�ȭ
        //HideDialogue();
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

    private void HideDialogue()
    {
        Debug.Log("��ȭ ����");
        dialogueText.gameObject.SetActive(false);
        currentDialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
        ProceedQuest();
        AudioManagerNBH.Audioinstance.PlayOtehrSFX(AudioManagerNBH.OtherSFX.DanverseCloseDoor);
        gameObject.SetActive(false);
    }
}


