using UnityEngine;

public class VictoriaManager : MonoBehaviour
{
    public void Victoria()
    {
        GameState.gano = true;

        // Reemplaza FindObjectOfType por FindFirstObjectByType para evitar el uso de API obsoleta
        var timerManager = Object.FindFirstObjectByType<TimerManager>();
        if (timerManager != null)
        {
            timerManager.DetenerYGuardarTiempo();
        }

        SceneController.instance.LoadScene("Epilogo");
    }
}