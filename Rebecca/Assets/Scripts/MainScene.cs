using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public GameObject optionsCanvas; 
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (optionsCanvas != null)
        {
            optionsCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        Debug.Log("시작");
        //  SceneManager.LoadScene("IntroScene");
        FadeManager.Instance.FadeOutAndLoadScene("IntroScene");
       // FadeManager.Instance.FadeOut(() => SceneManager.LoadScene("IntroScene")); //FadeOut없음
    }
    public void OnClickOption()
    {
        {
            if (optionsCanvas != null && mainMenu != null)
            {
                optionsCanvas.SetActive(true);
                mainMenu.SetActive(false);
            }
            Debug.Log("옵션");
        }
    }

    public void OnClickCloseOptions()
    {
        if (optionsCanvas != null && mainMenu != null)
        {
            optionsCanvas.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

        public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("종료");
#endif
    }
}
