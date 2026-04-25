using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ForzarSpawnAR : MonoBehaviour
{
    [Header("Configuración del Prefab")]
    public GameObject prefabLuna;

    private GameObject instanciaActual;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (instanciaActual != null) return;

        bool huboToque = false;
        Vector2 posicionInput = Vector2.zero;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            huboToque = true;
            posicionInput = Mouse.current.position.ReadValue();
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            huboToque = true;
            posicionInput = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        if (huboToque)
        {
            if (raycastManager != null && raycastManager.Raycast(posicionInput, hits, TrackableType.PlaneWithinPolygon))
            {
                ManejarPosicion(hits[0].pose.position, hits[0].pose.rotation);
            }
            else
            {
                Vector3 posicionFija = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
                ManejarPosicion(posicionFija, Quaternion.identity);
            }
        }
    }

    void ManejarPosicion(Vector3 pos, Quaternion rot)
    {
        
        if (instanciaActual == null)
        {
            instanciaActual = Instantiate(prefabLuna, pos, rot);
            Debug.Log("¡Estrella posicionada! Ahora puedes disparar tranquilo.");
        }
    }

    
    public void ReiniciarJuego()
    {
        if (instanciaActual != null)
        {
            
            Destroy(instanciaActual);

            
            instanciaActual = null;

            Debug.Log("¡Juego reiniciado! Toca la pantalla para colocar la Estrella en otro lugar.");
        }
    }
}