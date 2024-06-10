using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // ���̵� ȿ���� ����� Image ������Ʈ
    public float fadeSpeed = 1.0f; // ���̵� �ӵ�

    void Start()
    {
        // ���� ���� �� ���̵� �ƿ����� ����
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
