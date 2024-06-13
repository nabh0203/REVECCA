using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneControllerHSY : MonoBehaviour
{
    public CanvasGroup[] introScreens; // 1~5 이미지
    public float fadeDuration = 1f; // 페이드 시간

    private int currentScreenIndex = 0;
    private bool isFading = false; // 페이드 중인지 여부를 나타내는 변수

    private void Start()
    {
        // 배열 크기 확인
        if (introScreens == null || introScreens.Length == 0)
        {
            Debug.LogError("Intro screens are not assigned or empty.");
            return;
        }

        // 첫 화면만 보이도록 설정
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
        if (Input.GetMouseButtonDown(0) && !isFading) // 마우스 클릭 감지 및 페이드 중이 아닐 때
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
            // 마지막 화면에서 게임 씬으로 전환
            SceneManager.LoadScene("GameScene");
        }
    }

    private IEnumerator FadeOutAndIn(CanvasGroup currentScreen, CanvasGroup nextScreen)
    {
        isFading = true;

        // 현재 화면 페이드 아웃
        yield return StartCoroutine(Fade(currentScreen, 0));
        currentScreen.interactable = false;
        currentScreen.blocksRaycasts = false;

        // 다음 화면 페이드 인
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
