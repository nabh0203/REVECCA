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
*/

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
