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
    // 델리게이트 선언
    public delegate void Action();
    public static event Action OnTextOn;
    public static event Action OnTextOff;

    // 아이템 번호를 관리하는 배열
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
        }
    }
}
