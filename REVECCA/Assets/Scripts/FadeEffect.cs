using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // 페이드 효과에 사용할 Image 컴포넌트
    public float fadeSpeed = 1.0f; // 페이드 속도

    void Start()
    {
        // 게임 시작 시 페이드 아웃으로 시작
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        float alpha = fadeImage.color.a;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float alpha = fadeImage.color.a;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }
    }
}
