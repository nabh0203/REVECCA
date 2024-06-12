using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Interactable
{
    public GameObject NextQuest;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInteract()
    {
        NextQuest.SetActive(true);
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
        // ����Ʈ �Ϸ� ���θ� Ȯ���ϴ� ������ �����ϼ���
        // ���� ���:
        isQuestCompleted = true;
        return isQuestCompleted;

    }
}
