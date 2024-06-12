using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public CinemachineVirtualCamera virtualCamera1;
    public CinemachineVirtualCamera virtualCamera2;
    public CinemachineBlendDefinition blendDefinition;

    private void Start()
    {
        virtualCamera1.gameObject.SetActive(false);
        virtualCamera2.gameObject.SetActive(false);
    }
    private void Update()
    {
        // 스페이스바를 누르면 카메라 시점 변경
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleCameraView();
            cam.gameObject.SetActive(false);
        }
    }

    private void ToggleCameraView()
    {
        // 현재 활성화된 가상 카메라를 비활성화하고 다른 가상 카메라를 활성화
        if (virtualCamera1.gameObject.activeSelf)
        {
            virtualCamera1.gameObject.SetActive(false);
            virtualCamera2.gameObject.SetActive(true);

            // 부드러운 전환 효과 설정
            virtualCamera1.Priority = 9;
            virtualCamera2.Priority = 10;
            virtualCamera1.m_Priority = 9;
            virtualCamera2.m_Priority = 10;
            virtualCamera2.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera1.m_Lens.FieldOfView, virtualCamera2.m_Lens.FieldOfView, blendDefinition.m_Time);
        }
        else
        {
            virtualCamera1.gameObject.SetActive(true);
            virtualCamera2.gameObject.SetActive(false);

            // 부드러운 전환 효과 설정
            virtualCamera1.Priority = 10;
            virtualCamera2.Priority = 9;
            virtualCamera1.m_Priority = 10;
            virtualCamera2.m_Priority = 9;
            virtualCamera1.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera2.m_Lens.FieldOfView, virtualCamera1.m_Lens.FieldOfView, blendDefinition.m_Time);
        }
    }
}
