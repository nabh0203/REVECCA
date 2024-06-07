using UnityEngine;
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
