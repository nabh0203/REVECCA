using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManagerHSY : MonoBehaviour
{
    //public static FadeManagerHSY Instance { get; private set; }
    //�̱����� ����Ƽ �󿡼� �ΰ� �̻� �����ϸ� �ȵǸ� ���� �ϳ��� �̱��游�� �����ϰ� 
    //��� �Ŵ������� ���ս�Ų��.

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
            yield break; // CanvasGroup�� null�̰ų� �ı��� ��쿡�� �� �̻� �������� ����
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
    //��κ��� Fade ��ũ��Ʈ �ۼ� ����� ���ϴ� fade �Լ��� �ۼ��Ͽ� �ٸ� ��ũ��Ʈ���� �����Ͽ� �Լ��� ȣ���ϴ� ������� ���ȴ�.
    //���� : Fade ��ũ��Ʈ�� FadeOut �Լ��� MainManager�� ȣ���Ͽ� ���
}
