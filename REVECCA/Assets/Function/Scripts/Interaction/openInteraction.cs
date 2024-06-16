using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openInteraction : MonoBehaviour
{
    //�ִϸ��̼� ������ ���� ������Ʈ
    public GameObject otherObject;
    //�ڹ��� ������Ʈ
    public GameObject LockObject;
    //���ڿ�����Ʈ
    public GameObject Boxobject;
    //������Ʈ
    public DOTweenAnimation BoxOpenAnimation; // ù ��° ���� ���� DOTweenAnimation ����
    public Iteminteraction iteminteraction;

    public bool Lightinteraction = false;

    private bool isOpenActive = false;

    // Start is called before the first frame update
    private void Start()
    {
        BoxOpenAnimation = Boxobject.GetComponent<DOTweenAnimation>();
        if (BoxOpenAnimation == null)
        {
            Debug.LogError("BoxOpenAnimation�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isOpenActive)
        {
            Debug.Log("F Ű�� ���Ȱ�, isOpenActive�� true�Դϴ�.");
            SafeOpen();
            Debug.Log(Lightinteraction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpenActive = true;
            Debug.Log("���� ���� �����Դϴ�.");
        }
    }

    public void SafeOpen()
    {
        Debug.Log("���� ���� �õ� ��...");
        if (GameManager.instance.isDrKeyActive == true)
        {
            Debug.Log("DrKey�� Ȱ��ȭ�Ǿ� �ֽ��ϴ�: " + GameManager.instance.isDrKeyActive);
            BoxOpenAnimation.DOPlay();
            iteminteraction.PlaySoundByIndex(2);
            iteminteraction.PlaySoundByIndex(6);
        }
        else
        {
            Debug.Log("DrKey�� Ȱ��ȭ�Ǿ� ���� �ʽ��ϴ�.");
        }
        LockObject.SetActive(false);
        otherObject.SetActive(true);
    }
}
