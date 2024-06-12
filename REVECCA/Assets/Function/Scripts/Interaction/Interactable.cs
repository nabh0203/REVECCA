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

    // ����Ʈ�� ���� ���¸� ��Ÿ���� bool ����
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
            Debug.Log("����Ʈ �Ϸ�!");
            interactableObject.SetActive(false);

            if (otehrInteractableObject != null)
            {
                
                Debug.Log("����Ʈ �����۵���");
                otehrInteractableObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("����Ʈ ���� ��...");

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
