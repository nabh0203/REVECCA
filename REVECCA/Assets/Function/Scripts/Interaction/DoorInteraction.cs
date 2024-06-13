using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorInteraction : MonoBehaviour
{
    public DOTweenAnimation door1Animation; // 첫 번째 문에 대한 DOTweenAnimation 참조
    public DOTweenAnimation door2Animation; // 두 번째 문에 대한 DOTweenAnimation 참조
    public MaidNPCManager NPCTrigger;
    public GameObject maid;
    private bool isSound;

    void Start()
    {
        isSound = false;
        // 첫 번째 문과 두 번째 문에 대한 DOTweenAnimation 컴포넌트를 가져옵니다.
        door1Animation = GameObject.Find("Door1").GetComponent<DOTweenAnimation>();
        door2Animation = GameObject.Find("Door2").GetComponent<DOTweenAnimation>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (NPCTrigger.doorTrigger && !isSound)
        {
            if (other.CompareTag("Player")) // 트리거 대상이 "Player" 태그를 가진 경우
            {
                AudioManagerNBH.Audioinstance.PlaySFX(AudioManagerNBH.PlayerSFX.OpenDoor);
                // 첫 번째 문 애니메이션 재생
                door1Animation.DOPlay();

                // 두 번째 문 애니메이션 재생
                door2Animation.DOPlay();
                maid.SetActive(false);
                isSound = true;
            }
            /*else
            {
                AudioManagerNBH.Audioinstance.StopPlaySFX(AudioManagerNBH.PlayerSFX.OpenDoor);
            }
        }
        else
        {
            return;*/
        }
    }
}
