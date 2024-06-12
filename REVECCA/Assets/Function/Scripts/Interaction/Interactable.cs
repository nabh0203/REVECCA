using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject interactableObject;
    public GameObject otehrInteractableObject;

    public bool isGetInteraction;

    // 퀘스트의 진행 상태를 나타내는 bool 변수
    protected bool isQuestCompleted;

    protected virtual void Start()
    {
        isGetInteraction = false;
        interactableObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        if (isQuestCompleted)
        {
            Debug.Log("퀘스트 완료!");
            interactableObject.SetActive(false);

            if (otehrInteractableObject != null)
            {
                
                Debug.Log("퀘스트 아이템등장");
                otehrInteractableObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("퀘스트 진행 중...");

            /*if (otehrInteractableObject == null)
            {
                otehrInteractableObject.SetActive(true);
            }*/
        }
    }

    protected abstract bool CheckQuestCompletion();
    protected abstract void OnInteract();
    protected abstract void OnExit();
}
