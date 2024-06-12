using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Awake()
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
    }

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

}
