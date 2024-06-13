using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneControllerHSY : MonoBehaviour
{
    public CanvasGroup[] introScreens; // 1~5 �̹���
    public float fadeDuration = 1f; // ���̵� �ð�

    private int currentScreenIndex = 0;
    private bool isFading = false; // ���̵� ������ ���θ� ��Ÿ���� ����

    private void Start()
    {
        // �迭 ũ�� Ȯ��
        if (introScreens == null || introScreens.Length == 0)
        {
            Debug.LogError("Intro screens are not assigned or empty.");
            return;
        }

        // ù ȭ�鸸 ���̵��� ����
        for (int i = 1; i < introScreens.Length; i++)
        {
            introScreens[i].alpha = 0;
            introScreens[i].interactable = false;
            introScreens[i].blocksRaycasts = false;
        }
        introScreens[0].alpha = 1;
        introScreens[0].interactable = true;
        introScreens[0].blocksRaycasts = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFading) // ���콺 Ŭ�� ���� �� ���̵� ���� �ƴ� ��
        {
            ShowNextScreen();
        }
    }

    private void ShowNextScreen()
    {
        if (currentScreenIndex < introScreens.Length - 1)
        {
            StartCoroutine(FadeOutAndIn(introScreens[currentScreenIndex], introScreens[currentScreenIndex + 1]));
            currentScreenIndex++;
        }
        else
        {
            // ������ ȭ�鿡�� ���� ������ ��ȯ
            SceneManager.LoadScene("GameScene");
        }
    }

    private IEnumerator FadeOutAndIn(CanvasGroup currentScreen, CanvasGroup nextScreen)
    {
        isFading = true;

        // ���� ȭ�� ���̵� �ƿ�
        yield return StartCoroutine(Fade(currentScreen, 0));
        currentScreen.interactable = false;
        currentScreen.blocksRaycasts = false;

        // ���� ȭ�� ���̵� ��
        nextScreen.interactable = true;
        nextScreen.blocksRaycasts = true;
        yield return StartCoroutine(Fade(nextScreen, 1));

        isFading = false;
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float targetAlpha)
    {
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
