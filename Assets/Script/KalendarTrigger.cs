using UnityEngine;

public class KalendarTriggerForCalendar : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject uiImage;

    private bool isPlayerInTrigger = false; // Перевірка, чи гравець у тригері

    void Start()
    {
        // Вимкнути UI спочатку
        if (uiImage != null)
        {
            uiImage.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI Image не призначено в інспекторі!");
        }

        // Сховати курсор на початку
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Якщо гравець у тригері і натискає E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI(true); // Відкрити календар
        }

        // Якщо календар відкрито і натиснуто Escape (Q)
        if (uiImage != null && uiImage.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI(false); // Закрити календар
        }
    }

    private void ToggleUI(bool state)
    {
        if (uiImage != null)
        {
            uiImage.SetActive(state);

            // Відображення курсора залежить від стану UI
            Cursor.visible = state;
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Гравець увійшов у тригер
            Debug.Log("Гравець увійшов у тригер.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Гравець вийшов із тригера
            Debug.Log("Гравець вийшов із тригера.");

            // Сховати UI і курсор, якщо гравець залишив тригер
            ToggleUI(false);
        }
    }
}




