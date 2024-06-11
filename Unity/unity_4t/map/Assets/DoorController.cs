using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    private void OnCollisionEnter(Collision collision)
    {

        //������
        door.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();
        door.transform.DORotate(new Vector3(0, 0, 0), 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //������
            door.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();
            door.transform.DORotate(new Vector3(0, 0, 0), 1f);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            door.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();
            door.transform.DORotate(new Vector3(0, 90, 0), 1f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //���ݱ�
    }
}
