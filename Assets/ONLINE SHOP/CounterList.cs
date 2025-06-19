using UnityEngine;
using UnityEngine.UI;

public class DoubleCounter : MonoBehaviour
{
    [Header("Кнопки")]
    public Button[] counterButtons;

    [Header("Тексти лічильників")]
    public Text counterText1;
    public Text counterText2;

    private int counter1 = 0;
    private int counter2 = 0;

    private void Start()
    {
        // Прив'язка подій до кнопок
        for (int i = 0; i < counterButtons.Length; i++)
        {
            counterButtons[i].onClick.AddListener(IncreaseBothCounters);
        }

        // Початкове оновлення текстів
        UpdateCounters();
    }

    public void IncreaseBothCounters()
    {
        counter1++;
        counter2++;
        UpdateCounters();
    }

    private void UpdateCounters()
    {
        counterText1.text = counter1.ToString();
        counterText2.text = counter2.ToString();
    }
}
