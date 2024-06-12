/*using UnityEngine;
using TMPro;


public class NPCManager : MonoBehaviour
{
    public string dialogue; // NPC의 대사
    public TextMeshProUGUI dialogueText;
    public GameObject NPCtexbBox; // 대사를 출력할 UI Text 컴포넌트

    private void Start()
    {
        NPCtexbBox.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그를 가진 오브젝트와 충돌했을 때
        if (other.CompareTag("Player"))
        {
            NPCtexbBox.SetActive(true);
            // 대사를 출력
            DisplayDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거를 벗어났을 때 대사 지우기
        if (other.CompareTag("Player"))
        {
            NPCtexbBox.SetActive(false);
            HideDialogue();
        }
    }

    private void DisplayDialogue()
    {
        // UI 텍스트 컴포넌트에 대사를 출력
        dialogueText.text = dialogue;
        dialogueText.gameObject.SetActive(true); // 텍스트 활성화
    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false); // 텍스트 비활성화
    }
}
*/

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCManager : Interactable
{
    public List<string> dialogueList; // NPC의 대화 목록
    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private GameObject npcObject; // 현재 상호작용 중인 NPC 오브젝트
    public GameObject nextNPC;
    public bool foodTrigger = false;

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
        }
    }

    public void HideDialogue()
    {
        Debug.Log("대화 종료");
        dialogueText.gameObject.SetActive(false);
        currentDialogueIndex = 0; // 대화 인덱스 초기화
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


