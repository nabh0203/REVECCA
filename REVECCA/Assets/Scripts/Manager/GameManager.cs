/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 텍스트 UI 참조를 위한 배열
    public GameObject mainText;
    //public Text[] playerText;
    public GameObject[] Items;
    public GameObject[] itemImages; // 아이템 번호에 따른 이미지 배열
    // 델리게이트 선언
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
        // 델리게이트에 메소드 연결
        OnTextOn += ItemOn;
        OnTextOff += ItemOff;
    }
    private void OnDestroy()
    {
        // 델리게이트에서 메소드 연결 해제
        OnTextOn -= ItemOn;
        OnTextOff -= ItemOff;
    }

    // 텍스트를 활성화하는 메소드
    public void ItemOn()
    {
        mainText.SetActive(true);
    }

    // 텍스트를 비활성화하는 메소드
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

        // 해당 아이템 번호에 맞는 이미지 활성화
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

    // 텍스트 UI 참조를 위한 배열
    public GameObject mainText;
    //public Text[] playerText;
    public GameObject[] Items;
    public GameObject[] itemImages; // 아이템 번호에 따른 이미지 배열
    public GameObject[] inventoryImages; // 아이템 번호에 따른 이미지 배열
    public GameObject[] hintImages; // 힌트 이미지 배열c

    // 델리게이트 선언
    public delegate void Action();
    public static event Action OnTextOn;
    public static event Action OnTextOff;

    // 아이템 번호를 관리하는 배열
    public int[] itemNumbers;

    // 현재 활성화된 인덱스를 저장할 변수
    private int activeInventoryIndex = -1;
    //아이템 활성화를 위한 bool형값
    public bool[] IsitemActiveStates;
    public bool[] Ishint; // 힌트 활성화 상태 배열

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
        // 델리게이트에 메소드 연결
        OnTextOn += ItemOn;
        OnTextOff += ItemOff;

        // itemActiveStates 배열 초기화
        IsitemActiveStates = new bool[Items.Length];
        Ishint = new bool[hintImages.Length];
    }

    private void OnDestroy()
    {
        // 델리게이트에서 메소드 연결 해제
        OnTextOn -= ItemOn;
        OnTextOff -= ItemOff;
    }

    // 텍스트를 활성화하는 메소드
    public void ItemOn()
    {
        mainText.SetActive(true);
    }

    // 텍스트를 비활성화하는 메소드
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

            // 해당 아이템 번호에 맞는 이미지 활성화
            if (itemNumber >= 0 && itemNumber < itemImages.Length)
            {
                itemImages[itemNumber].SetActive(true);
            }

            // 아이템 번호에 해당하는 모든 오브젝트를 검사하여 활성화 상태로 변경
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
        // 이전에 활성화된 이미지가 있으면 비활성화
        if (activeInventoryIndex >= 0 && activeInventoryIndex < inventoryImages.Length)
        {
            inventoryImages[activeInventoryIndex].SetActive(false);
        }

        // 새로운 이미지를 활성화
        if (index >= 0 && index < inventoryImages.Length)
        {
            inventoryImages[index].SetActive(true);
            activeInventoryIndex = index;
        }
    }

    private void DeactivateActiveItemImage()
    {
        // 현재 활성화된 이미지 비활성화
        if (activeInventoryIndex >= 0 && activeInventoryIndex < inventoryImages.Length)
        {
            inventoryImages[activeInventoryIndex].SetActive(false);
            activeInventoryIndex = -1;
        }

        
    }
    private void DeactivateActiveHintImage()
    {
        // 모든 힌트 이미지를 비활성화
        for (int i = 0; i < hintImages.Length; i++)
        {
            hintImages[i].SetActive(false);
        }
    }

}
