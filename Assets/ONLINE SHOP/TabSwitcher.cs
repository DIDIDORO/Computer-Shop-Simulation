using UnityEngine;
using UnityEngine.UI;

public class TabSwitcher : MonoBehaviour
{
    [Header("Кнопки")]
    public Button[] tabButtons = new Button[8];

    [Header("Панелі")]
    public GameObject[] tabPanels = new GameObject[8];

    private void Start()
    {
        // Прив'язка подій до кнопок
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // Локальна копія індексу для лямбди
            tabButtons[i].onClick.AddListener(() => ShowTab(index));
        }

        // Початково — показати тільки першу панель
        ShowTab(0);
    }

    public void ShowTab(int tabIndex)
    {
        for (int i = 0; i < tabPanels.Length; i++)
        {
            tabPanels[i].SetActive(i == tabIndex);
        }
    }
}
