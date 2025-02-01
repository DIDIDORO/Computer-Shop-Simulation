using UnityEngine;

public class KalendarTriggerForCalendar : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject uiImage;

    private bool isPlayerInTrigger = false; // ��������, �� ������� � ������

    void Start()
    {
        // �������� UI ��������
        if (uiImage != null)
        {
            uiImage.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI Image �� ���������� � ���������!");
        }

        // ������� ������ �� �������
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ���� ������� � ������ � ������� E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI(true); // ³������ ��������
        }

        // ���� �������� ������� � ��������� Escape (Q)
        if (uiImage != null && uiImage.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI(false); // ������� ��������
        }
    }

    private void ToggleUI(bool state)
    {
        if (uiImage != null)
        {
            uiImage.SetActive(state);

            // ³���������� ������� �������� �� ����� UI
            Cursor.visible = state;
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // ������� ������ � ������
            Debug.Log("������� ������ � ������.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // ������� ������ �� �������
            Debug.Log("������� ������ �� �������.");

            // ������� UI � ������, ���� ������� ������� ������
            ToggleUI(false);
        }
    }
}




