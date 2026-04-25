using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [Header("Fade Settings")]
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        // 🔥 Cada escena tiene su propio controller
        instance = this;
    }

    void Start()
    {
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 1;
            StartCoroutine(FadeIn());
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        if (fadeGroup != null)
            yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);
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
        fadeGroup.blocksRaycasts = false;
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