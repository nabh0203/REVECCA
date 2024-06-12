using UnityEngine;

public class AudioManagerHSY : MonoBehaviour
{
    public static AudioManagerHSY Instance { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource[] sfxSources;

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

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public float GetSFXVolume()
    {
        if (sfxSources.Length > 0)
        {
            return sfxSources[0].volume;
        }
        return 0f;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }
}
