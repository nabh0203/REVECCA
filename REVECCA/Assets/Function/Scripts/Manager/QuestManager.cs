using UnityEngine;

public abstract class QuestManager : MonoBehaviour
{
    // 퀘스트의 진행 상태를 나타내는 bool 변수
    protected bool isQuestCompleted = false;
    public GameObject otehrInteractableObject;

    // 퀘스트의 진행 여부를 결정하는 메소드
    public abstract bool CheckQuestCompletion();

    // 퀘스트를 진행하는 메소드
    protected virtual void ProceedQuest()
    {
        if (isQuestCompleted)
        {
            Debug.Log("퀘스트 완료!");
            otehrInteractableObject.SetActive(true);
        }
        else
        {
            Debug.Log("퀘스트 진행 중...");
            otehrInteractableObject.SetActive(false);
        }
    }
}
