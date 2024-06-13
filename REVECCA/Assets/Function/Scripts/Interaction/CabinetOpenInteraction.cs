using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CabinetOpenInteraction : MonoBehaviour
{
    public GameObject otherObject;
    public GameObject Cabinetobject;
    public DOTweenAnimation CabinetOpenAnimation; // 첫 번째 문에 대한 DOTweenAnimation 참조
    public Iteminteraction iteminteraction;

    private bool isOpenActive = false;
    private bool isOneopen = true;
    // Start is called before the first frame update
    void Start()
    {
        CabinetOpenAnimation = Cabinetobject.GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isOpenActive && isOneopen)
        {
            CabinetOpen();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpenActive = true;
            Debug.Log("오픈");
        }
    }

    private void CabinetOpen()
    {
        if (GameManager.instance.isMrKeyActive == true)
        {
            Debug.Log(GameManager.instance.isMrKeyActive);
            CabinetOpenAnimation.DOPlay();
            iteminteraction.PlaySoundByIndex(3);
            iteminteraction.PlaySoundByIndex(6);
            otherObject.SetActive(true);
        }
        isOneopen = false;
    }
}
