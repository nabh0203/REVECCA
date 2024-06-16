using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openInteraction : MonoBehaviour
{
    //애니메이션 끝나고 나올 오브젝트
    public GameObject otherObject;
    //자물쇠 오브젝트
    public GameObject LockObject;
    //상자오브젝트
    public GameObject Boxobject;
    //오븢게트
    public DOTweenAnimation BoxOpenAnimation; // 첫 번째 문에 대한 DOTweenAnimation 참조
    public Iteminteraction iteminteraction;

    public bool Lightinteraction = false;

    private bool isOpenActive = false;

    // Start is called before the first frame update
    private void Start()
    {
        BoxOpenAnimation = Boxobject.GetComponent<DOTweenAnimation>();
        if (BoxOpenAnimation == null)
        {
            Debug.LogError("BoxOpenAnimation이 초기화되지 않았습니다.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isOpenActive)
        {
            Debug.Log("F 키가 눌렸고, isOpenActive가 true입니다.");
            SafeOpen();
            Debug.Log(Lightinteraction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpenActive = true;
            Debug.Log("오픈 가능 상태입니다.");
        }
    }

    public void SafeOpen()
    {
        Debug.Log("상자 오픈 시도 중...");
        if (GameManager.instance.isDrKeyActive == true)
        {
            Debug.Log("DrKey가 활성화되어 있습니다: " + GameManager.instance.isDrKeyActive);
            BoxOpenAnimation.DOPlay();
            iteminteraction.PlaySoundByIndex(2);
            iteminteraction.PlaySoundByIndex(6);
        }
        else
        {
            Debug.Log("DrKey가 활성화되어 있지 않습니다.");
        }
        LockObject.SetActive(false);
        otherObject.SetActive(true);
    }
}
