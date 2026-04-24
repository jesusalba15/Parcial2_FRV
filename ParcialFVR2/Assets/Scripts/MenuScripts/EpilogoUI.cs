using UnityEngine;
using TMPro;
using System.Collections;

public class EpilogoManager : MonoBehaviour
{
    public TextMeshProUGUI textoEpilogo;
    public GameObject botonContinuar;

    [TextArea]
    public string[] mensajes;

    public float velocidad = 0.04f;
    public float tiempoEntreMensajes = 1.5f;

    void Start()
    {
        botonContinuar.SetActive(false);

        string nombre = PlayerPrefs.GetString("PlayerName", "PILOTO");

        // Reemplazar {nombre} en todos los mensajes
        for (int i = 0; i < mensajes.Length; i++)
        {
            mensajes[i] = mensajes[i].Replace("{nombre}", nombre);
        }

        StartCoroutine(MostrarMensajes());
    }

    IEnumerator MostrarMensajes()
    {
        foreach (string mensaje in mensajes)
        {
            yield return StartCoroutine(EscribirTexto(mensaje));
            yield return new WaitForSeconds(tiempoEntreMensajes);
        }

        // 🔥 Cuando termina TODO → aparece botón
        botonContinuar.SetActive(true);
    }

    IEnumerator EscribirTexto(string mensaje)
    {
        textoEpilogo.text = "";

        foreach (char letra in mensaje)
        {
            textoEpilogo.text += letra;
            yield return new WaitForSeconds(velocidad);
        }
    }
}