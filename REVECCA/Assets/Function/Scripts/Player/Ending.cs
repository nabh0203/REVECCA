using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject Dnaverse;
    public GameObject player;
    public Camera cinemachineCamera; // 시네머신 카메라를 이 변수에 할당하세요

    private Coroutine triggerCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (triggerCoroutine != null)
            {
                StopCoroutine(triggerCoroutine);
            }
            triggerCoroutine = StartCoroutine(WaitAndSwitchCamera());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (triggerCoroutine != null)
            {
                StopCoroutine(triggerCoroutine);
                triggerCoroutine = null;
            }
        }
    }

    private IEnumerator WaitAndSwitchCamera()
    {
        yield return new WaitForSeconds(3);

        Dnaverse.SetActive(true);

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }

        if (cinemachineCamera != null)
        {
            cinemachineCamera.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("MainScene");
    }
}
