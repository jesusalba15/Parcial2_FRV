using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] panels;

    public void OpenPanel(GameObject panelToOpen)
    {
        // Apagar todos
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // Encender el que quieres
        panelToOpen.SetActive(true);
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}