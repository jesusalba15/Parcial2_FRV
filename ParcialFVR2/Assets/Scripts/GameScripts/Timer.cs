using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI textoTimer;
    public float tiempoMax = 60f;

    public float tiempoActual;
    private bool corriendo = false;

    void Start()
    {
        tiempoActual = tiempoMax;
        ActualizarTexto();
    }

    void Update()
    {
        if (corriendo)
        {
            tiempoActual -= Time.deltaTime;

            if (tiempoActual <= 0)
            {
                tiempoActual = 0;
                corriendo = false;

                GameState.gano = false;

                SceneController.instance.LoadScene("GameOver");
            }

            ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tiempoActual / 60);
        int segundos = Mathf.FloorToInt(tiempoActual % 60);

        textoTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void IniciarTimer()
    {
        corriendo = true;
    }

    public void DetenerYGuardarTiempo()
    {
        corriendo = false;
        
        PlayerPrefs.SetFloat("TiempoRestante", tiempoActual);
        PlayerPrefs.Save();
    }
}