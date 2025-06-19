using UnityEngine;
using UnityEngine.UI;

public class SlidePanel : MonoBehaviour
{
    public RectTransform panel;
    public float slideSpeed = 1000f;
    public Button toggleButton;
    public Button closeButton;

    public Vector2 closedPos = new Vector2(0, -500);
    public Vector2 openPos = new Vector2(0, 0);

    private bool isOpen = false;

    private void Start()
    {
        panel.anchoredPosition = closedPos;

        if (toggleButton != null)
            toggleButton.onClick.AddListener(TogglePanel);

        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel);
    }

    private void Update()
    {
        Vector2 targetPos = isOpen ? openPos : closedPos;
        panel.anchoredPosition = Vector2.MoveTowards(panel.anchoredPosition, targetPos, slideSpeed * Time.deltaTime);
    }

    public void TogglePanel()
    {
        isOpen = !isOpen;
    }

    public void ClosePanel()
    {
        isOpen = false;
    }

    public void OpenPanel()
    {
        isOpen = true;
    }
}
