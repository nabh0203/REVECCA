using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorInteraction : MonoBehaviour
{
    public DOTweenAnimation door1Animation; // ù ��° ���� ���� DOTweenAnimation ����
    public DOTweenAnimation door2Animation; // �� ��° ���� ���� DOTweenAnimation ����
    public MaidNPCManager NPCTrigger;

    void Start()
    {
        // ù ��° ���� �� ��° ���� ���� DOTweenAnimation ������Ʈ�� �����ɴϴ�.
        door1Animation = GameObject.Find("Door1").GetComponent<DOTweenAnimation>();
        door2Animation = GameObject.Find("Door2").GetComponent<DOTweenAnimation>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (NPCTrigger.doorTrigger)
        {
            if (other.CompareTag("Player")) // Ʈ���� ����� "Player" �±׸� ���� ���
            {
                AudioManagerNBH.Audioinstance.PlaySFX(AudioManagerNBH.PlayerSFX.OpenDoor);
                // ù ��° �� �ִϸ��̼� ���
                door1Animation.DOPlay();

                // �� ��° �� �ִϸ��̼� ���
                door2Animation.DOPlay();
            }
            else
            {
                AudioManagerNBH.Audioinstance.StopPlaySFX(AudioManagerNBH.PlayerSFX.OpenDoor);
            }
        }
        else
        {
            return;
        }
    }
}
