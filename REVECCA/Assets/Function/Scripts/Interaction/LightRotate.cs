using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LightRotate : MonoBehaviour
{
    public GameObject otherObject;
    public GameObject Mrkey;
    public GameObject Lightobject;
    public DOTweenAnimation LightAnimation; // 첫 번째 문에 대한 DOTweenAnimation 참조
    public bool isOpenActive = false;
    private bool RotateOn = true;
    // Start is called before the first frame update
    void Start()
    {
       Mrkey.SetActive(false);
       LightAnimation = Lightobject.GetComponent<DOTweenAnimation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpenActive = true;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isOpenActive && RotateOn)
        {
            RotateLight();
        }
    }
    private void RotateLight()
    {
        LightAnimation.DOPlay();
        Mrkey.SetActive(true);
        otherObject.SetActive(false);
        RotateOn = false;
    }
}
