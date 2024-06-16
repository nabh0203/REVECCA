using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStart : MonoBehaviour
{
    public AudioSource MainBGM;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainBGM.Stop();
            AudioManagerNBH.Audioinstance.PlayBGM(AudioManagerNBH.BGM.Room);
        }
    }
}
