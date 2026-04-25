using UnityEngine;
using TMPro;

public class MenuResultados : MonoBehaviour
{
    public TextMeshProUGUI textoNombres;
    public TextMeshProUGUI textoTiempos;

    void Start()
    {
        string lista = PlayerPrefs.GetString("PlayerResults", "");

        Debug.Log("PlayerResults: " + lista);

        if (!string.IsNullOrEmpty(lista))
        {
            string[] registros = lista.Split('|');

            string nombres = "";
            string tiempos = "";

            foreach (string registro in registros)
            {
                string[] partes = registro.Split('-');

                if (partes.Length == 2)
                {
                    string nombre = partes[0];
                    int tiempo = int.Parse(partes[1]);

                    int minutos = tiempo / 60;
                    int segundos = tiempo % 60;

                    string tiempoFormateado = string.Format("{0:00}:{1:00}", minutos, segundos);

                    nombres += nombre + "\n";
                    tiempos += tiempoFormateado + "\n";
                }
            }

            textoNombres.text = nombres;
            textoTiempos.text = tiempos;
        }
        else
        {
            textoNombres.text = "NOMBRE...";
            textoTiempos.text = "TIEMPO...";
        }
    }
}