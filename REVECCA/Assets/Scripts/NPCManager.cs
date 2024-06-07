using UnityEngine;
using TMPro;


public class NPCManager : MonoBehaviour
{
    public string dialogue; // NPC�� ���
    public TextMeshProUGUI dialogueText;
    public GameObject NPCtexbBox; // ��縦 ����� UI Text ������Ʈ

    private void Start()
    {
        NPCtexbBox.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �±׸� ���� ������Ʈ�� �浹���� ��
        if (other.CompareTag("Player"))
        {
            NPCtexbBox.SetActive(true);
            // ��縦 ���
            DisplayDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ Ʈ���Ÿ� ����� �� ��� �����
        if (other.CompareTag("Player"))
        {
            NPCtexbBox.SetActive(false);
            HideDialogue();
        }
    }

    private void DisplayDialogue()
    {
        // UI �ؽ�Ʈ ������Ʈ�� ��縦 ���
        dialogueText.text = dialogue;
        dialogueText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
    }
}
