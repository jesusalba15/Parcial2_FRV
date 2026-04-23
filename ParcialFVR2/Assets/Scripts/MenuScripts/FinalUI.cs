using UnityEngine;
using TMPro;

public class FinalUI : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;

    void Start()
    {
        string nombre = PlayerPrefs.GetString("PlayerName", "PILOTO");
        textoNombre.text = nombre;
    }
}