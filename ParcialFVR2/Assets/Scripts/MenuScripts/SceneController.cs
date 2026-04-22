using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [Header("Fade Settings")]
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;

    
    
    

    void Start()
    {
        // Asegura que empieza en negro y hace fade in
        fadeGroup.alpha = 1;
        StartCoroutine(FadeIn());
    }

    // 🔹 Llamar desde botones
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        // Fade a negro
        yield return StartCoroutine(FadeOut());

        // Cargar escena
        SceneManager.LoadScene(sceneName);

        // Esperar un frame para asegurar carga
        yield return null;

        // Fade desde negro
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeGroup.blocksRaycasts = false;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        fadeGroup.alpha = 0;
    }

    IEnumerator FadeOut()
    {
        fadeGroup.blocksRaycasts = true;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = t / fadeDuration;
            yield return null;
        }

        fadeGroup.alpha = 1;
    }
}