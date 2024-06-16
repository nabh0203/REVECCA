using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintObject : MonoBehaviour
{
    public static HintObject instance;

    public bool[] isHints; // 힌트 활성화 여부 배열
    public GameObject[] hintImages; // 힌트 이미지 배열

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 배열 초기화
        isHints = new bool[2];
        for (int i = 0; i < isHints.Length; i++)
        {
            isHints[i] = false;
        }
    }

    public void ActivateHint()
    {
        for (int i = 0; i < isHints.Length; i++)
        {
            if (isHints[i])
            {
                GameManager.instance.ItemOff();
                Debug.Log($"힌트 {i}");
                hintImages[i].SetActive(true);
            }
            else
            {
                hintImages[i].SetActive(false);
            }
        }
    }
}

