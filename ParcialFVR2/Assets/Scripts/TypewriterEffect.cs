using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Configuración de texto")]
    public TextMeshProUGUI textoUI;
    public float velocidad = 0.05f;

    [TextArea]
    public string mensaje;

    [Header("Referencia")]
    public DialogManager dialogManager;

    void Start()
    {
        StartCoroutine(EscribirTexto());
    }

    IEnumerator EscribirTexto()
    {
        textoUI.text = "";

        foreach (char letra in mensaje)
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(velocidad);
        }

        // Cuando termina el texto → mostrar input
        dialogManager.ShowInput();
    }
}