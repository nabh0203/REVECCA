using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject interactableObject;

    // ����Ʈ�� ���� ���¸� ��Ÿ���� bool ����
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
            Debug.Log("����Ʈ �Ϸ�!");
            // ����Ʈ �Ϸ� ���� ���� ����
        }
        else
        {
            Debug.Log("����Ʈ ���� ��...");
            // ����Ʈ ���� �� ���� ���� ����
        }
    }
    protected abstract bool CheckQuestCompletion();
    protected abstract void OnInteract();
    protected abstract void OnExit();
}
