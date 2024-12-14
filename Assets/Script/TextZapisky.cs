using UnityEngine;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour
{
    public GameObject hintUI; // ��'��� ��� ������� (UI-�������)
    public string hintText; // ����� �������
    public Text hintTextComponent; // ��������� Text ��� ����������� ������

    private void Start()
    {
        // ������ ������� ��� ������� ���
        if (hintUI != null)
        {
            hintUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ����������, �� ������ ������� � ������
        if (other.CompareTag("Player"))
        {
            if (hintUI != null)
            {
                hintUI.SetActive(true); // �������� �������
            }

            if (hintTextComponent != null && !string.IsNullOrEmpty(hintText))
            {
                hintTextComponent.text = hintText; // ������������ �����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ����������, �� ������� ������ �� �������
        if (other.CompareTag("Player"))
        {
            if (hintUI != null)
            {
                hintUI.SetActive(false); // ������ �������
            }
        }
    }
}

