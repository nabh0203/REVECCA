using UnityEngine;

public abstract class QuestManager : MonoBehaviour
{
    // ����Ʈ�� ���� ���¸� ��Ÿ���� bool ����
    protected bool isQuestCompleted = false;
    public GameObject otehrInteractableObject;

    // ����Ʈ�� ���� ���θ� �����ϴ� �޼ҵ�
    public abstract bool CheckQuestCompletion();

    // ����Ʈ�� �����ϴ� �޼ҵ�
    protected virtual void ProceedQuest()
    {
        if (isQuestCompleted)
        {
            Debug.Log("����Ʈ �Ϸ�!");
            otehrInteractableObject.SetActive(true);
        }
        else
        {
            Debug.Log("����Ʈ ���� ��...");
            otehrInteractableObject.SetActive(false);
        }
    }
}
