using UnityEngine;

public class EscToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject playerControllerObject;
    [SerializeField] private GameObject alwaysVisibleUI;

    private bool isVisible = false;

    void Start()
    {
        if (uiPanel == null)
        {
            Debug.LogError("UI Panel не призначено в інспекторі!");
            return;
        }

        uiPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (alwaysVisibleUI != null)
        {
            alwaysVisibleUI.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isVisible = !isVisible;
        uiPanel.SetActive(isVisible);

        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isVisible;

        if (playerControllerObject != null)
        {
            var controller = playerControllerObject.GetComponent<FirstPersonController>();
            if (controller != null)
            {
                controller.enabled = !isVisible;
            }
        }

        if (alwaysVisibleUI != null)
        {
            alwaysVisibleUI.SetActive(true);
        }
    }

    // 🟢 ВИКЛИКАТИ ЦЕЙ МЕТОД З КНОПКИ PLAY
    public void CloseMenu()
    {
        isVisible = false;
        uiPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerControllerObject != null)
        {
            var controller = playerControllerObject.GetComponent<FirstPersonController>();
            if (controller != null)
            {
                controller.enabled = true;
            }
        }

        if (alwaysVisibleUI != null)
        {
            alwaysVisibleUI.SetActive(true);
        }
    }
}
