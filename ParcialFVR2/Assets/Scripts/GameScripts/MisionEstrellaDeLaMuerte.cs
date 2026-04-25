using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MisionEstrellaDeLaMuerte : MonoBehaviour
{
    [Header("Configuración de Misión")]
    public int totalObjetivos = 5;
    private int objetivosRestantes;

    [Header("UI")]
    public Canvas canvasMision;
    public TextMeshProUGUI textoContador;

    [Header("Objetos")]
    public GameObject puntoCriticoVisual;
    public GameObject prefabExplosion;

    [Header("Posiciones")]
    public List<Transform> posicionesPosibles;
    private int indiceUltimaPosicion = -1;

    [Header("Efectos UI")]
    public float duracionTitileo = 0.5f;
    public Color colorNormal = Color.white;
    public Color colorDanio = Color.red;

    private bool misionCompletada = false;

    void Start()
    {
        objetivosRestantes = totalObjetivos;
        UpdateTextoUI();

        if (textoContador != null)
            textoContador.color = colorNormal;

        // 🔥 Iniciar Timer automáticamente
        var timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null) timer.IniciarTimer();

        // 🔥 Configurar punto crítico
        if (puntoCriticoVisual != null)
        {
            DetectorImpactoJugador detector = puntoCriticoVisual.GetComponent<DetectorImpactoJugador>();
            if (detector == null)
                detector = puntoCriticoVisual.AddComponent<DetectorImpactoJugador>();

            detector.misionManager = this;

            MoverPuntoCritico();
        }
        else
        {
            Debug.LogError("Falta asignar el Punto Critico Visual");
        }
    }

    public void RegistrarAcierto()
    {
        if (misionCompletada) return;

        if (objetivosRestantes > 0)
        {
            objetivosRestantes--;
            UpdateTextoUI();

            SpawnExplosionEnPunto();
            StartCoroutine(TitileoRojoUI());

            if (objetivosRestantes > 0)
            {
                MoverPuntoCritico();
            }
            else
            {
                FinalizarMision(true);
            }
        }
    }

    void MoverPuntoCritico()
    {
        if (posicionesPosibles.Count < 2)
        {
            Debug.LogWarning("Se necesitan al menos 2 posiciones.");
            return;
        }

        int nuevoIndice;
        do
        {
            nuevoIndice = Random.Range(0, posicionesPosibles.Count);
        } while (nuevoIndice == indiceUltimaPosicion);

        indiceUltimaPosicion = nuevoIndice;
        puntoCriticoVisual.transform.position = posicionesPosibles[nuevoIndice].position;
    }

    void SpawnExplosionEnPunto()
    {
        if (prefabExplosion != null)
        {
            GameObject exp = Instantiate(prefabExplosion, puntoCriticoVisual.transform.position, Quaternion.identity);
            exp.transform.localScale = Vector3.one * 0.1f;
            Destroy(exp, 2f);
        }
    }

    IEnumerator TitileoRojoUI()
    {
        if (textoContador != null)
        {
            textoContador.color = colorDanio;
            yield return new WaitForSeconds(duracionTitileo);
            textoContador.color = colorNormal;
        }
    }

    void UpdateTextoUI()
    {
        if (textoContador != null)
        {
            textoContador.text = objetivosRestantes + " / " + totalObjetivos + " PUNTOS CRITICOS";
        }
    }

    // 🔥 FINALIZACIÓN DE MISIÓN
    public void FinalizarMision(bool victoria)
    {
        if (misionCompletada || !victoria) return;

        misionCompletada = true;

        // 🔥 Marcar victoria
        GameState.gano = true;

        // 🔥 Calcular puntaje
        int puntosAcertados = totalObjetivos - objetivosRestantes;
        float porcentajeScore = ((float)puntosAcertados / totalObjetivos) * 100f;

        // 🔥 DETENER Y GUARDAR TIEMPO (CLAVE)
        var timerManager = Object.FindFirstObjectByType<TimerManager>();
        if (timerManager != null)
        {
            timerManager.DetenerYGuardarTiempo();
            Debug.Log("Tiempo guardado correctamente: " + timerManager.tiempoActual);
        }
        else
        {
            Debug.LogWarning("No se encontró TimerManager");
        }

        // 🔥 Guardar puntaje
        PlayerPrefs.SetFloat("PuntajeMision", porcentajeScore);
        PlayerPrefs.Save();

        // 🔥 CAMBIO DE ESCENA
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadScene("Victoria");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victoria");
        }
    }
}