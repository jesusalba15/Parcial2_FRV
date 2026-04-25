using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MisionEstrellaDeLaMuerte : MonoBehaviour
{
    [Header("Configuración de Misión")]
    [Tooltip("Cantidad total de puntos críticos a destruir")]
    public int totalObjetivos = 5;
    private int objetivosRestantes;

    [Header("Objetos de la Misión")]
    [Tooltip("El Canvas que flota sobre la Estrella con el texto")]
    public Canvas canvasMision;
    [Tooltip("El texto TMP que dice '5/5 Puntos Críticos'")]
    public TextMeshProUGUI textoContador;
    [Tooltip("La esfera que representa el punto crítico activo (debe tener un Collider Trigger)")]
    public GameObject puntoCriticoVisual;
    [Tooltip("El prefab de la explosión cuando aciertas")]
    public GameObject prefabExplosion;

    [Header("Puntos de Aparición")]
    [Tooltip("Arrastra aquí varios objetos vacíos (Empty) posicionados alrededor de la superficie de la Estrella")]
    public List<Transform> posicionesPosibles;
    private int indiceUltimaPosicion = -1;

    [Header("Efectos de UI")]
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

        var timer = Object.FindFirstObjectByType<TimerManager>();
        if (timer != null) timer.IniciarTimer();

        if (puntoCriticoVisual != null)
        {
            DetectorImpactoJugador detector = puntoCriticoVisual.GetComponent<DetectorImpactoJugador>();
            if (detector == null) detector = puntoCriticoVisual.AddComponent<DetectorImpactoJugador>();
            detector.misionManager = this;

            MoverPuntoCritico();
        }
        else
        {
            Debug.LogError("¡Falta el objeto Punto Critico Visual en el script!");
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
            Debug.LogWarning("Necesitas al menos 2 posiciones posibles para que el punto cambie.");
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
            textoContador.text = objetivosRestantes + " / " + totalObjetivos + " PUNTOS CRÍTICOS";
        }
    }

    // --- LÓGICA DE FINALIZACIÓN Y GUARDADO ---
    public void FinalizarMision(bool victoria)
    {
        if (misionCompletada || !victoria) return;
        misionCompletada = true;

        // 1. Calcular el Score basado en los objetivos
        int puntosAcertados = totalObjetivos - objetivosRestantes;
        float porcentajeScore = ((float)puntosAcertados / totalObjetivos) * 100f;

        // 2. BUSCAR EL TIMER Y GUARDAR DATOS EN EL DISCO
        var timerManager = Object.FindFirstObjectByType<TimerManager>();
        if (timerManager != null)
        {
            // Detenemos el reloj y guardamos el tiempo que quedaba (Ej: 00:45)
            // IMPORTANTE: Asegúrate que 'tiempoActual' en el script de tu amigo sea PUBLIC
            PlayerPrefs.SetFloat("TiempoRestante", timerManager.tiempoActual);
            timerManager.DetenerYGuardarTiempo();
            Debug.Log("Misión Finalizada. Tiempo guardado: " + timerManager.tiempoActual);
        }

        // Guardamos el puntaje final
        PlayerPrefs.SetFloat("PuntajeMision", porcentajeScore);

        // 3. FORCE SAVE: Esto asegura que el dato no se pierda entre las 4 escenas
        PlayerPrefs.Save();

        // 4. Iniciar transición a la escena "Victoria"
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadScene("Victoria");
        }
        else
        {
            // Plan B si no hay SceneController en la escena
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victoria");
        }
    }
}