using UnityEngine;
using TMPro;

public class FinalUI : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoTiempo;

    void Start()
    {
        string nombre = PlayerPrefs.GetString("PlayerName", "PILOTO");
        float tiempo = PlayerPrefs.GetFloat("TiempoRestante", 0f);

        textoNombre.text = nombre;

        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        Debug.Log("Tiempo recibido: " + tiempo);

        // 🔥 GUARDAR SI GANÓ
        if (GameState.gano)
        {
            GuardarResultado(nombre, tiempo);
        }
    }

    void GuardarResultado(string nombre, float tiempo)
    {
        int tiempoEntero = Mathf.FloorToInt(tiempo);

        string lista = PlayerPrefs.GetString("PlayerResults", "");

        string nuevo = nombre + "-" + tiempoEntero;

        if (string.IsNullOrEmpty(lista))
            lista = nuevo;
        else
            lista += "|" + nuevo;

        PlayerPrefs.SetString("PlayerResults", lista);
        PlayerPrefs.Save();

        Debug.Log("GUARDADO EN HISTORIAL: " + lista);
    }
}