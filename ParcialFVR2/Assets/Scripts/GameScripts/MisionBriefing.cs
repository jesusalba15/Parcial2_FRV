using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MisionBriefing : MonoBehaviour
{
    [Header("Configuración de UI")]
    public Image retratoCapitan;
    public TextMeshProUGUI textoUI;
    public Button botonContinuar;

    [Header("Sonido")]
    public AudioClip sonidoTecleo;
    private AudioSource audioSource;

    [Header("Assets Visuales")]
    public Sprite[] gestos;
    public float velocidadTexto = 0.04f;

    private string[] guion = {
        "¡Atención, {nombre}! Soy el Comandante Vance. No hay tiempo para simulaciones.",
        "Esa estación orbital... la Estrella de la Muerte... está activa. Detectamos una vulnerabilidad térmica.",
        "Es un tiro entre un millón. Muchos han fallado... pero tú tienes algo especial.",
        "¡Escucha bien! Sobrevuela la superficie y destruye los puntos críticos con todo tu arsenal.",
        "¡El destino de Tatooine depende de ti! Demuestra de qué estás hecho. ¡A volar!"
    };

    private int fraseActual = 0;
    private string nombreJugador;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        botonContinuar.interactable = false;

        // 🔥 Obtener nombre guardado
        nombreJugador = PlayerPrefs.GetString("PlayerName", "PILOTO");

        StartCoroutine(PresentarFrase());
    }

    IEnumerator PresentarFrase()
    {
        textoUI.text = "";

        // Evitar errores si faltan sprites
        if (gestos != null && gestos.Length > fraseActual)
        {
            retratoCapitan.sprite = gestos[fraseActual];
        }

        // 🔥 Reemplazo dinámico (NO modifica el array original)
        string frase = guion[fraseActual].Replace("{nombre}", nombreJugador);

        foreach (char letra in frase)
        {
            textoUI.text += letra;

            if (sonidoTecleo != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoTecleo);
            }

            yield return new WaitForSeconds(velocidadTexto);
        }

        // 🔥 Última frase → activar botón
        if (fraseActual == guion.Length - 1)
        {
            botonContinuar.interactable = true;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            fraseActual++;
            StartCoroutine(PresentarFrase());
        }
    }
}