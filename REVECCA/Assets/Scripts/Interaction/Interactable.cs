using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject interactableObject;

    // 퀘스트의 진행 상태를 나타내는 bool 변수
    protected bool isQuestCompleted = false;

    protected virtual void Start()
    {
        interactableObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactableObject.SetActive(true);
            OnInteract();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactableObject.SetActive(false);
            OnExit();
        }
    }

    protected virtual void ProceedQuest()
    {
        if (CheckQuestCompletion())
        {
            Debug.Log("퀘스트 완료!");
            // 퀘스트 완료 관련 로직 구현
        }
        else
        {
            Debug.Log("퀘스트 진행 중...");
            // 퀘스트 진행 중 관련 로직 구현
        }
    }
    protected abstract bool CheckQuestCompletion();
    protected abstract void OnInteract();
    protected abstract void OnExit();
}
