using UnityEngine;
using TMPro;

public class FinalUI : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoTiempo;

    void Start()
    {
        // Nombre
        string nombre = PlayerPrefs.GetString("PlayerName", "PILOTO");
        textoNombre.text = nombre;

        // Tiempo
        float tiempo = PlayerPrefs.GetFloat("TiempoRestante", 0f);

        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}