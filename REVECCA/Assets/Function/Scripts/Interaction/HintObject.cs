using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintObject : MonoBehaviour
{
    public static HintObject instance;

    public bool[] isHints; // ��Ʈ Ȱ��ȭ ���� �迭
    public GameObject[] hintImages; // ��Ʈ �̹��� �迭

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
        // �迭 �ʱ�ȭ
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
                Debug.Log($"��Ʈ {i}");
                hintImages[i].SetActive(true);
            }
            else
            {
                hintImages[i].SetActive(false);
            }
        }
    }
}

