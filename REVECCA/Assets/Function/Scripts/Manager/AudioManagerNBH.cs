using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class AudioManagerNBH : MonoBehaviour
{
    public static AudioManagerNBH Audioinstance;

    [Header("BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    public int bgmChannels;
    AudioSource[] bgmPlayer;
    int bgmChannelIndex;

    public enum BGM { mainPage, main, Op, mainBGM, Room};

    [Header("PlayerSFX")]
    public AudioClip[] playerSfxClips;
    public float playerSfxVolume;
    public int playerSfxChannels;
    AudioSource[] playerSfxPlayers;
    int playerSFXchannelIndex;

    public enum PlayerSFX { Click, Get, OpenDoor, Eat, safe, Box, Cabinet, Clean, LockOn, LockOff, newsPaper, mail, KeyDrop, Vase };


    [Header("OtherSFX")]
    public AudioClip[] otherSfxClips;
    public float otherSfxVolume;
    public int otherSfxChannels;
    AudioSource[] otherSfxPlayers;
    int otherSFXchannelIndex;

    public enum OtherSFX { DanverseStep, MaidStep, DogStep,DanverseCloseDoor,DogBark , DogSound };

    void Awake()
    {
        if (Audioinstance == null)
        {
            Audioinstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        {
            Init();
        }
    }
    void Init()
    {
        /*----------------------------------BGM------------------------------------------*/
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = new AudioSource[bgmChannels];

        for (int index = 0; index < bgmPlayer.Length; index++)
        {
            bgmPlayer[index] = bgmObject.AddComponent<AudioSource>();
            bgmPlayer[index].playOnAwake = false;
            bgmPlayer[index].volume = bgmVolume;
            bgmPlayer[index].loop = true;
        }
        /*----------------------------------PlayerSFX------------------------------------------*/
        GameObject playerSFXbgmObject = new GameObject("PlayerSfxPlayer");
        playerSFXbgmObject.transform.parent = transform;
        playerSfxPlayers = new AudioSource[playerSfxChannels];

        for (int index = 0; index < playerSfxPlayers.Length; index++)
        {
            playerSfxPlayers[index] = playerSFXbgmObject.AddComponent<AudioSource>();
            playerSfxPlayers[index].playOnAwake = false;
            playerSfxPlayers[index].volume = playerSfxVolume;
        }
        /*----------------------------------OtherSFX------------------------------------------*/
        GameObject otherSFXbgmObject = new GameObject("OtherSfxPlayer");
        otherSFXbgmObject.transform.parent = transform;
        otherSfxPlayers = new AudioSource[otherSfxChannels];

        for (int index = 0; index < otherSfxPlayers.Length; index++)
        {
            otherSfxPlayers[index] = playerSFXbgmObject.AddComponent<AudioSource>();
            otherSfxPlayers[index].playOnAwake = false;
            otherSfxPlayers[index].volume = otherSfxVolume;
        }
    }
    //BGM ȣ���
    //AudioManagerNBH.instance.PlayBGM(AudioManagerNBH.BGM.������̸�); - �̸� ���� ���� �Լ� �� �ڵ忡 �ۼ�.
    //������ �Ʒ�ó�� ���
    //aM.PlayBGM(AudioManagerNBH.BGM.������̸�);
    public void PlayBGM(BGM Bgm)
    {
        for (int index = 0; index < bgmPlayer.Length; index++)
        {
            int loopIndex = (index + bgmChannelIndex) % bgmPlayer.Length;

            if (bgmPlayer[loopIndex].isPlaying)
                continue;

            bgmChannelIndex = loopIndex;
            bgmPlayer[loopIndex].clip = bgmClip[(int)Bgm];
            bgmPlayer[loopIndex].Play();
            break;//�ݺ��� ����

        }

    }
    public void StopBGM(BGM Bgm)
    {
        for (int index = 0; index < bgmPlayer.Length; index++)
        {
            if (bgmPlayer[index].clip == bgmClip[(int)Bgm] && bgmPlayer[index].isPlaying)
            {
                bgmPlayer[index].Stop();
                break; // �ݺ��� ����
            }
        }
    }


    //PlaySFX ȣ���
    public void PlaySFX(PlayerSFX Playersfx)
    {
        for (int index = 0; index < playerSfxPlayers.Length; index++)
        {
            int loopIndex = (index + playerSFXchannelIndex) % playerSfxPlayers.Length;

            if (playerSfxPlayers[loopIndex].isPlaying)
                continue;

            playerSFXchannelIndex = loopIndex;
            playerSfxPlayers[loopIndex].clip = playerSfxClips[(int)Playersfx];
            playerSfxPlayers[loopIndex].Play();
            break;//�ݺ��� ����

        }

    }
    //OtehrSFX ȣ���
    public void PlayOtehrSFX(OtherSFX otherSFX)
    {
        for (int index = 0; index < otherSfxPlayers.Length; index++)
        {
            int loopIndex = (index + otherSFXchannelIndex) % otherSfxPlayers.Length;

            if (otherSfxPlayers[loopIndex].isPlaying)
                continue;

            otherSFXchannelIndex = loopIndex;
            otherSfxPlayers[loopIndex].clip = otherSfxClips[(int)otherSFX];
            otherSfxPlayers[loopIndex].Play();
            break;//�ݺ��� ����

        }

    }
}
