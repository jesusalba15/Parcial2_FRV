using UnityEngine;
using TMPro;

public class MenuResultados : MonoBehaviour
{
    public TextMeshProUGUI textoResultados;

    void Start()
    {
        string lista = PlayerPrefs.GetString("PlayerList", "");

        if (!string.IsNullOrEmpty(lista))
        {
            string[] nombres = lista.Split('|');

            string resultado = "";

            int contador = 1;

            foreach (string nombre in nombres)
            {
                resultado += contador + ". " + nombre + "\n";
                contador++;
            }

            textoResultados.text = resultado;
        }
        else
        {
            textoResultados.text = "SIN REGISTROS";
        }
    }
}