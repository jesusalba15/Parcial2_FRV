using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelInput;
    public TMP_InputField inputNombre;
    public TextMeshProUGUI textoDialogo;
    public GameObject botonContinuar;

    [Header("Texto siguiente")]
    [TextArea]
    public string siguienteMensaje;

    public float velocidad = 0.05f;

    void Start()
    {
        panelInput.SetActive(false);
        botonContinuar.SetActive(false);
    }

    public void ShowInput()
    {
        panelInput.SetActive(true);
        inputNombre.ActivateInputField();
    }

    public void SaveName()
    {
        string nombre = inputNombre.text.Trim().ToUpper();

        if (!string.IsNullOrEmpty(nombre))
        {
            // 🔥 GUARDAR LISTA DE JUGADORES
            string lista = PlayerPrefs.GetString("PlayerList", "");

            if (lista == "")
            {
                lista = nombre;
            }
            else
            {
                lista += "|" + nombre;
            }

            PlayerPrefs.SetString("PlayerList", lista);

            // Guardar último jugador (para usar en otras escenas)
            PlayerPrefs.SetString("PlayerName", nombre);

            PlayerPrefs.Save();

            // Ocultar input
            panelInput.SetActive(false);

            // Mostrar siguiente diálogo con nombre
            string mensajeFinal = siguienteMensaje.Replace("{nombre}", nombre);

            StopAllCoroutines();
            StartCoroutine(EscribirTexto(mensajeFinal));
        }
    }

    IEnumerator EscribirTexto(string mensaje)
    {
        textoDialogo.text = "";

        foreach (char letra in mensaje)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidad);
        }

        botonContinuar.SetActive(true);
    }
}