using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelInput;
    public TMP_InputField inputNombre;
    public TextMeshProUGUI textoDialogo;
    public GameObject botonContinuar; // 👈 NUEVO

    [Header("Texto siguiente")]
    [TextArea]
    public string siguienteMensaje;

    public float velocidad = 0.05f;

    void Start()
    {
        panelInput.SetActive(false);
        botonContinuar.SetActive(false); // 👈 oculto al inicio
    }

    public void ShowInput()
    {
        panelInput.SetActive(true);
        inputNombre.ActivateInputField();
    }

    public void SaveName()
    {
        string nombre = inputNombre.text.ToUpper();

        if (!string.IsNullOrEmpty(nombre))
        {
            PlayerPrefs.SetString("PlayerName", nombre);
            PlayerPrefs.Save();

            // Ocultar input
            panelInput.SetActive(false);

            // Reemplazar nombre en texto
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

        // 👇 AQUÍ aparece el botón cuando termina el segundo diálogo
        botonContinuar.SetActive(true);
    }
}