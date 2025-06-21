using UnityEngine;
using UnityEngine.UI;

public class ComputerInteraction : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 3f;
    public GameObject interactionUI;
    public GameObject computerUI;
    public GameObject extraUI;

    private bool isLookingAtComputer = false;
    private bool isComputerActive = false;
    private float escapeMenuCooldown = 0f;
    private float forceHideCursorTime = 0f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Raycast перевірка
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform == transform)
            {
                isLookingAtComputer = true;
            }
            else
            {
                isLookingAtComputer = false;
            }
        }
        else
        {
            isLookingAtComputer = false;
        }

        // Показати interactionUI
        if (isLookingAtComputer && !isComputerActive)
        {
            interactionUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                computerUI.SetActive(true);
                isComputerActive = true;
                interactionUI.SetActive(false);

                // Показати курсор
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            interactionUI.SetActive(false);
        }

        // Вихід з ПК
        if (isComputerActive && Input.GetKeyDown(KeyCode.Escape))
        {
            computerUI.SetActive(false);
            isComputerActive = false;

            // Приховати курсор
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Блокувати Escape-меню на 5 сек
            escapeMenuCooldown = 5f;

            // І 5 сек форсовано ховати курсор
            forceHideCursorTime = 5f;
        }

        // Escape-меню (extraUI) — тільки якщо cooldown == 0
        if (!isComputerActive && Input.GetKeyDown(KeyCode.Escape) && escapeMenuCooldown <= 0f)
        {
            if (extraUI != null)
            {
                extraUI.SetActive(!extraUI.activeSelf);
            }
        }

        // Зменшуємо таймер cooldown
        if (escapeMenuCooldown > 0f)
        {
            escapeMenuCooldown -= Time.deltaTime;
        }

        // Примусово ховаємо курсор кожну мілісекунду протягом 5 сек
        if (forceHideCursorTime > 0f)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            forceHideCursorTime -= Time.deltaTime;
        }
    }
}
