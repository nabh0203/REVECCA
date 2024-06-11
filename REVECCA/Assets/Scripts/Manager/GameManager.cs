/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // �ؽ�Ʈ UI ������ ���� �迭
    public GameObject mainText;
    //public Text[] playerText;
    public GameObject[] Items;
    public GameObject[] itemImages; // ������ ��ȣ�� ���� �̹��� �迭
    // ��������Ʈ ����
    public delegate void Action();
    public static event Action OnTextOn;
    public static event Action OnTextOff;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        mainText.SetActive(false);
        // ��������Ʈ�� �޼ҵ� ����
        OnTextOn += ItemOn;
        OnTextOff += ItemOff;
    }
    private void OnDestroy()
    {
        // ��������Ʈ���� �޼ҵ� ���� ����
        OnTextOn -= ItemOn;
        OnTextOff -= ItemOff;
    }

    // �ؽ�Ʈ�� Ȱ��ȭ�ϴ� �޼ҵ�
    public void ItemOn()
    {
        mainText.SetActive(true);
    }

    // �ؽ�Ʈ�� ��Ȱ��ȭ�ϴ� �޼ҵ�
    public void ItemOff()
    {
        mainText.SetActive(false);
    }

    public void ActivateTextOn()
    {
        OnTextOn?.Invoke();
    }

    public void ActivateTextOff()
    {
        OnTextOff?.Invoke();
    }
    public void DeactivateItem(GameObject item)
    {
        int itemNumber = item.GetComponent<ItemManager>().itemNumber;
        item.SetActive(false);

        // �ش� ������ ��ȣ�� �´� �̹��� Ȱ��ȭ
        if (itemNumber >= 0 && itemNumber < itemImages.Length)
        {
            itemImages[itemNumber].SetActive(true);
        }
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // �ؽ�Ʈ UI ������ ���� �迭
    public GameObject mainText;
    //public Text[] playerText;
    public GameObject[] Items;
    public GameObject[] itemImages; // ������ ��ȣ�� ���� �̹��� �迭
    public GameObject[] inventoryImages; // ������ ��ȣ�� ���� �̹��� �迭
    public GameObject[] hintImages; // ��Ʈ �̹��� �迭c

    // ��������Ʈ ����
    public delegate void Action();
    public static event Action OnTextOn;
    public static event Action OnTextOff;

    // ������ ��ȣ�� �����ϴ� �迭
    public int[] itemNumbers;

    // ���� Ȱ��ȭ�� �ε����� ������ ����
    private int activeInventoryIndex = -1;
    //������ Ȱ��ȭ�� ���� bool����
    public bool[] IsitemActiveStates;
    public bool[] Ishint; // ��Ʈ Ȱ��ȭ ���� �迭

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        mainText.SetActive(false);
        // ��������Ʈ�� �޼ҵ� ����
        OnTextOn += ItemOn;
        OnTextOff += ItemOff;

        // itemActiveStates �迭 �ʱ�ȭ
        IsitemActiveStates = new bool[Items.Length];
        Ishint = new bool[hintImages.Length];
    }

    private void OnDestroy()
    {
        // ��������Ʈ���� �޼ҵ� ���� ����
        OnTextOn -= ItemOn;
        OnTextOff -= ItemOff;
    }

    // �ؽ�Ʈ�� Ȱ��ȭ�ϴ� �޼ҵ�
    public void ItemOn()
    {
        mainText.SetActive(true);
    }

    // �ؽ�Ʈ�� ��Ȱ��ȭ�ϴ� �޼ҵ�
    public void ItemOff()
    {
        mainText.SetActive(false);
    }

    public void ActivateTextOn()
    {
        OnTextOn?.Invoke();
    }

    public void ActivateTextOff()
    {
        OnTextOff?.Invoke();
    }

    public void DeactivateItem(GameObject item)
    {
        int index = System.Array.IndexOf(Items, item);
        if (index >= 0 && index < itemNumbers.Length)
        {
            int itemNumber = itemNumbers[index];
            item.SetActive(false);

            // �ش� ������ ��ȣ�� �´� �̹��� Ȱ��ȭ
            if (itemNumber >= 0 && itemNumber < itemImages.Length)
            {
                itemImages[itemNumber].SetActive(true);
            }

            // ������ ��ȣ�� �ش��ϴ� ��� ������Ʈ�� �˻��Ͽ� Ȱ��ȭ ���·� ����
            for (int i = 0; i < itemNumbers.Length; i++)
            {
                if (itemNumbers[i] == itemNumber)
                {
                    IsitemActiveStates[i] = true;
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (IsitemActiveStates[i])
                {
                    ActivateItemImage(i);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DeactivateActiveItemImage();
            DeactivateActiveHintImage();
        }

        for (int i = 0; i < Ishint.Length; i++)
        {
            if (Ishint[i] && i < hintImages.Length)
            {
                hintImages[i].SetActive(true);
            }
            else if (i < hintImages.Length)
            {
                hintImages[i].SetActive(false);
            }
        }
    }

    private void ActivateItemImage(int index)
    {
        // ������ Ȱ��ȭ�� �̹����� ������ ��Ȱ��ȭ
        if (activeInventoryIndex >= 0 && activeInventoryIndex < inventoryImages.Length)
        {
            inventoryImages[activeInventoryIndex].SetActive(false);
        }

        // ���ο� �̹����� Ȱ��ȭ
        if (index >= 0 && index < inventoryImages.Length)
        {
            inventoryImages[index].SetActive(true);
            activeInventoryIndex = index;
        }
    }

    private void DeactivateActiveItemImage()
    {
        // ���� Ȱ��ȭ�� �̹��� ��Ȱ��ȭ
        if (activeInventoryIndex >= 0 && activeInventoryIndex < inventoryImages.Length)
        {
            inventoryImages[activeInventoryIndex].SetActive(false);
            activeInventoryIndex = -1;
        }

        
    }
    private void DeactivateActiveHintImage()
    {
        // ��� ��Ʈ �̹����� ��Ȱ��ȭ
        for (int i = 0; i < hintImages.Length; i++)
        {
            hintImages[i].SetActive(false);
        }
    }

}
