using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteminteraction : MonoBehaviour
{
    public NPCManager npcManager;
    public GameObject ItemInteraction;
    public bool isPlayerInTrigger = false;

    // 사용자가 입력할 수 있는 public 변수 추가
    public int soundIndex;

    [Header("ItemSFX")]
    public AudioClip[] itemSfxClips;
    public float itemSfxVolume;
    public int itemSfxChannels;
    AudioSource[] itemSfxPlayers;
    int itemSFXchannelIndex;

    public enum ItemSFX { Eat, safe, Box, Cabinet, Clean, LockOn, LockOff, newsPaper, mail, KeyDrop, Vase };
    private void Start()
    {
        // itemSfxPlayers 배열 초기화
        itemSfxPlayers = new AudioSource[itemSfxChannels];

        for (int i = 0; i < itemSfxChannels; i++)
        {
            GameObject audioObject = new GameObject("ItemSFXPlayer_" + i);
            audioObject.transform.SetParent(this.transform);
            itemSfxPlayers[i] = audioObject.AddComponent<AudioSource>();
            itemSfxPlayers[i].volume = itemSfxVolume;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
    public void ItemAction()
    {
        PlaySoundByIndex(soundIndex);
        ItemInteraction.SetActive(false);
/*        if (npcManager.isInteractionTrigger)
        {
            
        }*/
    }

    // 새로 추가된 메서드: 사용자가 입력한 인덱스에 해당하는 오디오 재생
    public void PlaySoundByIndex(int index)
    {
        Debug.Log("음악재생");
        if (index >= 0 && index < itemSfxClips.Length)
        {
            for (int i = 0; i < itemSfxPlayers.Length; i++)
            {
                int loopIndex = (i + itemSFXchannelIndex) % itemSfxPlayers.Length;

                if (itemSfxPlayers[loopIndex].isPlaying)
                    continue;

                itemSFXchannelIndex = loopIndex;
                itemSfxPlayers[loopIndex].clip = itemSfxClips[index];
                itemSfxPlayers[loopIndex].Play();

                // 오디오 소스 재생 확인용 로그 추가
                Debug.Log($"AudioSource {loopIndex}에서 재생 시작: {itemSfxClips[index].name}");
                break; // 반복문 끊기
            }
        }
        else
        {
            Debug.LogWarning("잘못된 인덱스: " + index);
        }
    }

}
