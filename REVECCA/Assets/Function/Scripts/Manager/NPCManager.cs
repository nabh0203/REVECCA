using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCManager : Interactable
{
    public List<string> dialogueList; // NPC의 대화 목록
    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private GameObject npcObject; // 현재 상호작용 중인 NPC 오브젝트
    public bool isInteractionTrigger = false;
    public Iteminteraction Iteminteraction;


    protected override void OnInteract()
    {
        npcObject = gameObject; // 현재 상호작용 중인 NPC 오브젝트 저장
    }

    protected override void OnExit()
    {
        
        npcObject = null; // 상호작용이 끝났으므로 npcObject를 null로 초기화
        HideDialogue();
    }

    protected override bool CheckQuestCompletion()
    {
        // 퀘스트 완료 여부를 확인하는 로직을 구현하세요
        // 예를 들어:
        isQuestCompleted = true;
        return isQuestCompleted;
    }

    private void Update()
    {
        // npcObject가 null이 아닌 경우에만 F키 입력을 확인
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
        // 대화 목록의 다음 대화를 출력
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
            // 모든 대화가 출력되었다면 대화 종료
            HideDialogue();
            if (Iteminteraction != null)
            {
                Debug.Log("아이템인터렉션 실행");
                Iteminteraction.ItemAction();
            }
            else if (Iteminteraction == null)
            {
                return;
            }
        }
    }

    public void HideDialogue()
    {
        Debug.Log("대화 종료");
        dialogueText.gameObject.SetActive(false);
        currentDialogueIndex = 0; // 대화 인덱스 초기화
        ProceedQuest();
    }
}


