using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatinteraction : MonoBehaviour
{
    public GameObject nextObject;
    public GameObject TextObject;
    public  NPCManager npcManager;
    //private bool isPlayerInTrigger = false;

    /*private void OnTriggerEnter(Collider other)
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
    }*/

    private void Update()
    {
        if (npcManager.foodTrigger)
        {
            EatSound();
            nextObject.SetActive(true);
            gameObject.SetActive(false);
            TextObject.SetActive(false);
        }
    }

    public void EatSound()
    {
        AudioManagerNBH.Audioinstance.PlaySFX(AudioManagerNBH.PlayerSFX.Eat);
    }
}
