/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int itemNumber; // 아이템의 고유 번호
    public Material outline; // 아웃라인을 적용할 머티리얼
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

    void Start()
    {
        //outline = new Material(Shader.Find("Custom/Outline"));
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 트리거될 때
        if (other.CompareTag("Player"))
        {
            renderers = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거에서 벗어날 때
        if (other.CompareTag("Player"))
        {
            Renderer renderer = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderer.sharedMaterials);
            materialList.Remove(outline);

            renderer.materials = materialList.ToArray();
        }
    }
}
*//*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Material outline; // 아웃라인을 적용할 머티리얼
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

    void Start()
    {
        //outline = new Material(Shader.Find("Custom/Outline"));
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 트리거될 때
        if (other.CompareTag("Player"))
        {
            renderers = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);

            renderers.materials = materialList.ToArray();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거에서 벗어날 때
        if (other.CompareTag("Player"))
        {
            Renderer renderer = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderer.sharedMaterials);
            materialList.Remove(outline);

            renderer.materials = materialList.ToArray();
        }
    }

    // 특정 이벤트 시 GameManager에게 아이템 비활성화 요청
    public void DeactivateItem()
    {
        GameManager.instance.DeactivateItem(this.gameObject);
    }
}
*/


using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Interactable
{
    /*public Material outline;
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();*/

    protected override void Start()
    {
        base.Start();
        //outline = new Material(Shader.Find("Custom/Outline"));
    }

    protected override void OnInteract()
    {/*
        renderers = this.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Add(outline);

        renderers.materials = materialList.ToArray();*/
        return;
    }

    protected override void OnExit()
    {
        /*Renderer renderer = this.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);
        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();*/
        return;
    }

    public void DeactivateItem()
    {
        GameManager.instance.DeactivateItem(this.gameObject);
    }


    protected override bool CheckQuestCompletion()
    {
        // 퀘스트 완료 여부를 확인하는 로직을 구현하세요
        // 예를 들어:
        return isQuestCompleted;
    }
}
