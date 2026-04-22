using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelInput;
    public TMP_InputField inputNombre;

    void Start()
    {
        panelInput.SetActive(false);
    }

    // Mostrar input cuando termina el diálogo
    public void ShowInput()
    {
        panelInput.SetActive(true);
        inputNombre.ActivateInputField();
    }

    // Guardar nombre
    public void SaveName()
    {
        string nombre = inputNombre.text;

        if (!string.IsNullOrEmpty(nombre))
        {
            PlayerPrefs.SetString("PlayerName", nombre);
            PlayerPrefs.Save();

            Debug.Log("Nombre guardado: " + nombre);

            // OPCIONAL: cambiar de escena
            // SceneController.instance.LoadScene("ARScene");
        }
        else
        {
            Debug.Log("Ingrese un nombre válido");
        }
    }
}