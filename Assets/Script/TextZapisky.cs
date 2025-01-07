using UnityEngine;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour
{
    public GameObject hintUI; // Об'єкт для підказки (UI-елемент)
    public string hintText; // Текст підказки
    public Text hintTextComponent; // Компонент Text для відображення тексту

    private void Start()
    {
        // Ховаємо підказку при запуску гри
        if (hintUI != null)
        {
            hintUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Перевіряємо, чи увійшов гравець у тригер
        if (other.CompareTag("Player"))
        {
            if (hintUI != null)
            {
                hintUI.SetActive(true); // Показуємо підказку
            }

            if (hintTextComponent != null && !string.IsNullOrEmpty(hintText))
            {
                hintTextComponent.text = hintText; // Встановлюємо текст
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Перевіряємо, чи гравець вийшов із тригера
        if (other.CompareTag("Player"))
        {
            if (hintUI != null)
            {
                hintUI.SetActive(false); // Ховаємо підказку
            }
        }
    }
}

