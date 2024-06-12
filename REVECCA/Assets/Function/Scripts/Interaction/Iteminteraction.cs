using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteminteraction : MonoBehaviour
{
    public NPCManager npcManager;
    public GameObject ItemInteraction;
    public bool isPlayerInTrigger = false;

    // ����ڰ� �Է��� �� �ִ� public ���� �߰�
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
        // itemSfxPlayers �迭 �ʱ�ȭ
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

    // ���� �߰��� �޼���: ����ڰ� �Է��� �ε����� �ش��ϴ� ����� ���
    public void PlaySoundByIndex(int index)
    {
        Debug.Log("�������");
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

                // ����� �ҽ� ��� Ȯ�ο� �α� �߰�
                Debug.Log($"AudioSource {loopIndex}���� ��� ����: {itemSfxClips[index].name}");
                break; // �ݺ��� ����
            }
        }
        else
        {
            Debug.LogWarning("�߸��� �ε���: " + index);
        }
    }

}
