using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider brightnessSlider;
    public MainScene mainScene;

    private float originalBGMVolume;
    private float originalSFXVolume;
    private float originalBrightness;

    private void Start()
    {
        // �����̴� �ʱⰪ ����
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        //brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0.5f);

        // ���� ���� �����Ͽ� ��
        originalBGMVolume = bgmSlider.value;
        originalSFXVolume = sfxSlider.value;
       // originalBrightness = brightnessSlider.value;

        ApplySettings();
    }

    public void OnBGMVolumeChange()
    {
        AudioManager.Instance.SetBGMVolume(bgmSlider.value);
    }

    public void OnSFXVolumeChange()
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    //public void OnBrightnessChange()
    //{
    //    float brightness = brightnessSlider.value;
    //    //���� ȭ���� ��� ����
    //   // RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    //}

    public void OnClickSave()
    {
        // ���� ����
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        
        PlayerPrefs.Save();

        // ���� ���� �����Ͽ� ��
        originalBGMVolume = bgmSlider.value;
        originalSFXVolume = sfxSlider.value;
        //originalBrightness = brightnessSlider.value;

        Debug.Log("Settings Saved");
    }

    public void OnClickClose()
    {
        // ���� ������ ����
        bgmSlider.value = originalBGMVolume;
        sfxSlider.value = originalSFXVolume;
        //brightnessSlider.value = originalBrightness;

        ApplySettings(); // ���� ���� ����

        if (mainScene != null)
        {
            mainScene.OnClickCloseOptions();
        }
    }

    private void ApplySettings()
    {
        // ���� ����
        AudioManager.Instance.SetBGMVolume(bgmSlider.value);
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
        //float brightness = brightnessSlider.value;
        //RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    }
}
