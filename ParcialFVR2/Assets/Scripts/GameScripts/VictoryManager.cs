using UnityEngine;

public class VictoriaManager : MonoBehaviour
{
    
    public static float PuntajeFinal;

  
    public void Victoria(float scoreRecibido)
    {
        
        PuntajeFinal = scoreRecibido;

       
        EjecutarLogicaVictoria();
    }

    
    public void Victoria()
    {
        EjecutarLogicaVictoria();
    }

    private void EjecutarLogicaVictoria()
    {
        GameState.gano = true;

        // Detenemos el tiempo
        var timerManager = Object.FindFirstObjectByType<TimerManager>();
        if (timerManager != null)
        {
            timerManager.DetenerYGuardarTiempo();
        }

      
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadScene("Victoria");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victoria");
        }
    }
}