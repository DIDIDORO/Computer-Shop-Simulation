using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UI елементи, які будемо активувати/деактивувати
    public GameObject uiFirst;  // Перший UI (при наведенні)
    public GameObject uiSecond; // ПК (при натисканні E)
    public GameObject uiThird;  // Счетчик грошей (при натисканні Escape)

    private GameObject currentUI;  // Змінна для відслідковування поточного UI
    private bool isPCActive = false; // Статус ПК UI

    // Коли гравець наводить курсор на об'єкт
    private void OnMouseEnter()
    {
        // Активуємо перший UI при наведенні
        if (currentUI == null)
        {
            currentUI = uiFirst;
            currentUI.SetActive(true);
        }
    }

    // Коли курсор залишає об'єкт
    private void OnMouseExit()
    {
        // Якщо курсор залишає об'єкт, приховуємо перший UI
        if (currentUI == uiFirst)
        {
            currentUI.SetActive(false);
        }
    }

    void Update()
    {
        // Перевірка на натискання клавіші E для активації ПК
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPCActive)
            {
                // Якщо ПК вже активний, показуємо третій UI (счетчик грошей)
                SwitchUI(uiThird);
                SetCursorState(false); // Приховуємо курсор
            }
            else
            {
                // Інакше активуємо ПК UI
                SwitchUI(uiSecond);
                isPCActive = true; // Встановлюємо, що ПК UI активний
                SetCursorState(true); // Включаємо курсор
            }
        }

        // Перевірка на натискання клавіші Escape для активації счетчика грошей
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPCActive)
            {
                // Якщо ПК активний, то сховати його і показати счетчик грошей
                SwitchUI(uiThird);
                isPCActive = false; // Відключаємо ПК UI
                SetCursorState(true); // Включаємо курсор для взаємодії зі счетчиком
            }
            else
            {
                // Якщо ПК не активний, то просто перемикаємось на счетчик грошей
                SwitchUI(uiThird);
                SetCursorState(true); // Включаємо курсор для счетчика
            }
        }
    }

    // Функція для перемикання між UI
    private void SwitchUI(GameObject newUI)
    {
        // Якщо вже активний UI, приховуємо його
        if (currentUI != null)
        {
            currentUI.SetActive(false);
        }

        // Активуємо новий UI
        currentUI = newUI;
        currentUI.SetActive(true);
    }

    // Функція для керування станом курсора
    private void SetCursorState(bool isVisible)
    {
        Cursor.visible = isVisible; // Встановлюємо видимість курсора
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked; // Розблоковуємо/блокуємо курсор
    }
}
