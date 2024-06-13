using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneHSY : MonoBehaviour
{
    public FadeEffect Fade;
    //아래와 같이 public / 연결시키고 싶은 스크립트 명 / 변수명; 으로 작성 시 해당 스크립트에서 선언된
    //함수 및 변수를 사용할수 있다.
    public FadeManagerHSY FadeManagerHSY;
    public GameObject optionsCanvas; 
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(Fade != null)
        {
            Fade.FadeOut();
        }
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
        FadeManagerHSY.FadeOutAndLoadScene("IntroScene");
        //위처럼 Instance를 참조하여 사용방식이 있고 아래처럼 스크립트를 연결해서 사용하는 방식이 있다.
       /* Fade.FadeOut();
        SceneManager.LoadScene("IntroScene");*/
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
