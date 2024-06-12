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
        // �����̽��ٸ� ������ ī�޶� ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleCameraView();
            cam.gameObject.SetActive(false);
        }
    }

    private void ToggleCameraView()
    {
        // ���� Ȱ��ȭ�� ���� ī�޶� ��Ȱ��ȭ�ϰ� �ٸ� ���� ī�޶� Ȱ��ȭ
        if (virtualCamera1.gameObject.activeSelf)
        {
            virtualCamera1.gameObject.SetActive(false);
            virtualCamera2.gameObject.SetActive(true);

            // �ε巯�� ��ȯ ȿ�� ����
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

            // �ε巯�� ��ȯ ȿ�� ����
            virtualCamera1.Priority = 10;
            virtualCamera2.Priority = 9;
            virtualCamera1.m_Priority = 10;
            virtualCamera2.m_Priority = 9;
            virtualCamera1.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera2.m_Lens.FieldOfView, virtualCamera1.m_Lens.FieldOfView, blendDefinition.m_Time);
        }
    }
}
