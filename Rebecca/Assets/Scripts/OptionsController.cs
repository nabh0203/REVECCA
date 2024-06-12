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
        // 슬라이더 초기값 설정
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        //brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0.5f);

        // 현재 값을 저장하여 비교
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
    //    //게임 화면의 밝기 조절
    //   // RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    //}

    public void OnClickSave()
    {
        // 설정 저장
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        
        PlayerPrefs.Save();

        // 현재 값을 저장하여 비교
        originalBGMVolume = bgmSlider.value;
        originalSFXVolume = sfxSlider.value;
        //originalBrightness = brightnessSlider.value;

        Debug.Log("Settings Saved");
    }

    public void OnClickClose()
    {
        // 원래 값으로 복원
        bgmSlider.value = originalBGMVolume;
        sfxSlider.value = originalSFXVolume;
        //brightnessSlider.value = originalBrightness;

        ApplySettings(); // 원래 설정 적용

        if (mainScene != null)
        {
            mainScene.OnClickCloseOptions();
        }
    }

    private void ApplySettings()
    {
        // 설정 적용
        AudioManager.Instance.SetBGMVolume(bgmSlider.value);
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
        //float brightness = brightnessSlider.value;
        //RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    }
}
