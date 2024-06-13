using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManagerHSY : MonoBehaviour
{
    //public static FadeManagerHSY Instance { get; private set; }
    //싱글톤은 유니티 상에서 두개 이상 존재하면 안되며 보통 하나의 싱글톤만이 존재하고 
    //모든 매니저들을 통합시킨다.

    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;


    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndIn(sceneName));
    }

    private IEnumerator FadeOutAndIn(string sceneName)
    {
        // Fade out
        yield return StartCoroutine(Fade(1f));
        // Load the new scene
        SceneManager.LoadScene(sceneName);
        // Fade in
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup == null || fadeCanvasGroup.gameObject == null)
        {
            yield break; // CanvasGroup이 null이거나 파괴된 경우에는 더 이상 진행하지 않음
        }

        fadeCanvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.alpha = targetAlpha;
        fadeCanvasGroup.blocksRaycasts = false;
    }
    //대부분의 Fade 스크립트 작성 방법은 원하는 fade 함수를 작성하여 다른 스크립트에서 참조하여 함수를 호출하는 방식으로 사용된다.
    //예시 : Fade 스크립트의 FadeOut 함수를 MainManager로 호출하여 사용
}
