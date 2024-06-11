using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject interactableObject;
    public GameObject otehrInteractableObject;
    public Material outline;
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

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
            renderers = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactableObject.SetActive(false);
            OnExit();

            Renderer renderer = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderer.sharedMaterials);
            materialList.Remove(outline);

            renderer.materials = materialList.ToArray();
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
                otehrInteractableObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("����Ʈ ���� ��...");

            if (otehrInteractableObject != null)
            {
                otehrInteractableObject.SetActive(false);
            }
        }
    }

    protected abstract bool CheckQuestCompletion();
    protected abstract void OnInteract();
    protected abstract void OnExit();
}