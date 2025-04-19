using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject mainPanel;  // твій перший Panel (де Play, Settings, Exit)
    public GameObject settingsPanel;  // Panel2 (де слайдери, тумблери тощо)

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMain()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
