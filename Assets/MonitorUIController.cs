using UnityEngine;

public class MonitorUIController : MonoBehaviour
{
    public GameObject pcUIPanel;

    public void CloseMonitorUI()
    {
        pcUIPanel.SetActive(false);
    }
}
