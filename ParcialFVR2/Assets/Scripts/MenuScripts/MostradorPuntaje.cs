using UnityEngine;
using TMPro;
using System.Collections;

public class MostradorPuntaje : MonoBehaviour
{
    [Header("UI - Textos")]
    public TextMeshProUGUI textoPuntajeUI;
    public TextMeshProUGUI textoTiempoUI;

    [Header("Configuración de Tiempos")]
    [Tooltip("El tiempo total del cronómetro en la misión (ej: 60)")]
    public float tiempoMaxMision = 60f;

    [Header("Velocidad de Animación")]
    public float velocidadAnimScore = 100f;
    public float velocidadAnimTiempo = 30f;

    void Start()
    {
        // Limpiamos los textos antes de empezar
        if (textoPuntajeUI != null) textoPuntajeUI.text = "0%";
        if (textoTiempoUI != null) textoTiempoUI.text = "00:00";

        StartCoroutine(CargarYAnimarResultados());
    }

    IEnumerator CargarYAnimarResultados()
    {
        
        yield return new WaitForSeconds(0.3f);

        
        float scoreFinal = PlayerPrefs.GetFloat("PuntajeMision", 0f);
        float tiempoSobrante = PlayerPrefs.GetFloat("TiempoRestante", tiempoMaxMision);

        
        float tiempoTardadoFinal = tiempoMaxMision - tiempoSobrante;

        
        if (tiempoTardadoFinal <= 0)
        {
            Debug.LogWarning("El tiempo tardado dio 0. Asegúrate de que el TimerManager guardó el dato antes de cambiar de escena.");
            tiempoTardadoFinal = 0.1f;
        }

        
        StartCoroutine(AnimarNumeroScore(scoreFinal));
        StartCoroutine(AnimarRelojTiempo(tiempoTardadoFinal));
    }

    IEnumerator AnimarNumeroScore(float meta)
    {
        float actual = 0;
        while (actual < meta)
        {
            actual += Time.deltaTime * velocidadAnimScore;
            textoPuntajeUI.text = Mathf.Min(actual, meta).ToString("F0") + "%";
            yield return null;
        }
        textoPuntajeUI.text = meta.ToString("F0") + "%";
    }

    IEnumerator AnimarRelojTiempo(float metaTiempo)
    {
        float actual = 0;
        while (actual < metaTiempo)
        {
            actual += Time.deltaTime * velocidadAnimTiempo;
            FormatearYMostrarTiempo(Mathf.Min(actual, metaTiempo));
            yield return null;
        }
        FormatearYMostrarTiempo(metaTiempo);
    }

    void FormatearYMostrarTiempo(float t)
    {
        int min = Mathf.FloorToInt(t / 60);
        int seg = Mathf.FloorToInt(t % 60);
        textoTiempoUI.text = string.Format("{0:00}:{1:00}", min, seg);
    }
}