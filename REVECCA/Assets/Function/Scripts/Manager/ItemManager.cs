using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Interactable
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInteract()
    {
        return;
    }

    protected override void OnExit()
    {
        return;
    }

    public void DeactivateItem()
    {
        GameManager.instance.DeactivateItem(this.gameObject);
        ProceedQuest();
    }

    
    protected override bool CheckQuestCompletion()
    {
        // 퀘스트 완료 여부를 확인하는 로직을 구현하세요
        // 예를 들어:
        isQuestCompleted = true;
        return isQuestCompleted;

    }
}
