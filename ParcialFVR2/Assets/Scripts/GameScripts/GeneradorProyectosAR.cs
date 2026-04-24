using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class GeneradorProyectosAR : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabParaInstanciar;

    private GameObject objetoInstanciado;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        
        if (objetoInstanciado != null && !Application.isEditor) return;

#if UNITY_EDITOR
        // --- LÓGICA PARA PC (EDITOR) ---
        if (Input.GetMouseButtonDown(0))
        {
            if (objetoInstanciado == null)
            {
                Vector3 posicionFrente = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
                objetoInstanciado = Instantiate(prefabParaInstanciar, posicionFrente, Quaternion.identity);
                Debug.Log("Objeto instanciado en modo Editor");
            }
        }
#else
        // --- LÓGICA PARA CELULAR (AR REAL) ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    if (objetoInstanciado == null)
                    {
                        objetoInstanciado = Instantiate(prefabParaInstanciar, hitPose.position, hitPose.rotation);
                    }
                    else
                    {
                        objetoInstanciado.transform.position = hitPose.position;
                    }
                }
            }
        }
#endif
    }
}