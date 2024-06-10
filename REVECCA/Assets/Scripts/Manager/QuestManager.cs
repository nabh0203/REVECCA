using UnityEngine;

public abstract class QuestManager : MonoBehaviour
{
    // ����Ʈ�� ���� ���¸� ��Ÿ���� bool ����
    protected bool isQuestCompleted = false;

    // ����Ʈ�� ���� ���θ� �����ϴ� �޼ҵ�
    public abstract bool CheckQuestCompletion();

    // ����Ʈ�� �����ϴ� �޼ҵ�
    public virtual void ProceedQuest()
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
}
