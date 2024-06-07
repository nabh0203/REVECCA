/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int itemNumber; // �������� ���� ��ȣ
    public Material outline; // �ƿ������� ������ ��Ƽ����
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

    void Start()
    {
        //outline = new Material(Shader.Find("Custom/Outline"));
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� Ʈ���ŵ� ��
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
        // �÷��̾ Ʈ���ſ��� ��� ��
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
    public Material outline; // �ƿ������� ������ ��Ƽ����
    private Renderer renderers;
    private List<Material> materialList = new List<Material>();

    void Start()
    {
        //outline = new Material(Shader.Find("Custom/Outline"));
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� Ʈ���ŵ� ��
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
        // �÷��̾ Ʈ���ſ��� ��� ��
        if (other.CompareTag("Player"))
        {
            Renderer renderer = this.GetComponent<Renderer>();

            materialList.Clear();
            materialList.AddRange(renderer.sharedMaterials);
            materialList.Remove(outline);

            renderer.materials = materialList.ToArray();
        }
    }

    // Ư�� �̺�Ʈ �� GameManager���� ������ ��Ȱ��ȭ ��û
    public void DeactivateItem()
    {
        GameManager.instance.DeactivateItem(this.gameObject);
    }
}
