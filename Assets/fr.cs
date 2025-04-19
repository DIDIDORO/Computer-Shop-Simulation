using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    public Toggle fullScreenToggle;

    void Start()
    {
        if (fullScreenToggle != null)
        {
            // Встановлюємо стан Toggle згідно з поточним режимом екрану
            fullScreenToggle.isOn = Screen.fullScreen;

            // Додаємо слухача
            fullScreenToggle.onValueChanged.AddListener(OnToggleChanged);
        }
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            // Ввімкнути повноекранний режим
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else
        {
            // Вимкнути повноекранний режим (зробити віконним)
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }
    }
}
