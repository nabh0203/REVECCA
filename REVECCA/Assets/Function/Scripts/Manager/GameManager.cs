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
    public GameObject[] hintImages; // ��Ʈ �̹��� �迭

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

    // ��Ʈ Ȱ��ȭ ���� �迭
    public bool[] isHints;

    //Ȱ��ȭ �� �������� �����ϱ� ���� ���� �Լ�
    public openInteraction openTrigger;
    public bool isMrKeyActive;
    public bool isDrKeyActive;

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
        // ��Ʈ Ȱ��ȭ ���� �迭 �ʱ�ȭ
        isHints = new bool[hintImages.Length];
        for (int i = 0; i < isHints.Length; i++)
        {
            isHints[i] = false;
        }
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
            // ������ ��ȣ�� 2 �Ǵ� 3�� ��� Bool ���� ����
            if (itemNumber == 2)
            {
                Debug.Log("3�� Ȱ��ȭ");
                isDrKeyActive = true;
            }

            if (itemNumber == 3)
            {
                Debug.Log("4�� Ȱ��ȭ");
                isMrKeyActive = true;
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
            DeactivateHint();
        }
        // activeInventoryIndex�� 3�� �Ǵ� 4������ Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
        /*if (activeInventoryIndex == 3)
        {
            Debug.Log("3�� Ȱ��ȭ");
            isMrKeyActive = true;
        }
        else
        {
            isMrKeyActive = false;
        }

        if (activeInventoryIndex == 4)
        {
            Debug.Log("4�� Ȱ��ȭ");
            isDrKeyActive = true;
        }
        else
        {
            isDrKeyActive = false;
        }*/
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
            /*// 3�� �迭 ������ Ȱ��ȭ �� mrKey Ȱ��ȭ �� ���� bool ���� ����
            if (index == 3)
            {
                isMrKeyActive = true;
            }

            if (index == 4)
            {
               isDrKeyActive = true;
            }*/
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

    public void ActivateHint()
    {
        for (int i = 0; i < isHints.Length; i++)
        {
            if (isHints[i])
            {
                ItemOff();
                Debug.Log($"��Ʈ {i}");
                hintImages[i].SetActive(true);
            }
        }
    }

    public void DeactivateHint()
    {
        for (int i = 0; i < isHints.Length; i++)
        {
            hintImages[i].SetActive(false);
        }
    }

}
