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
    // ��������Ʈ ����
    public delegate void Action();
    public static event Action OnTextOn;
    public static event Action OnTextOff;

    // ������ ��ȣ�� �����ϴ� �迭
    public int[] itemNumbers;
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
        }
    }
}