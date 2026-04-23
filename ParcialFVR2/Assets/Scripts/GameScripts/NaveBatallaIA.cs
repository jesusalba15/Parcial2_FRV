using UnityEngine;
using System.Collections;

public class NaveBatallaIA : MonoBehaviour
{
    [Header("Vuelo")]
    public Transform objetivoCentral;
    public float velocidadVuelo = 5f;
    public float radioOrbita = 2f;

    private Vector3 ejeRotacion;
    private float offsetRuido;

    [Header("Combate")]
    public GameObject prefabLaser;
    public Transform[] puntosDisparo;
    public float tiempoMinEntreDisparos = 2f;
    public float tiempoMaxEntreDisparos = 6f;

    void Start()
    {
      
        ejeRotacion = Random.onUnitSphere;
        offsetRuido = Random.Range(0f, 100f);

        StartCoroutine(RutinaDisparo());
    }

    void Update()
    {
        if (objetivoCentral == null) return;

        
        Vector3 direccionAlCentro = transform.position - objetivoCentral.position;
        Vector3 posicionDeseada = objetivoCentral.position + direccionAlCentro.normalized * radioOrbita;

        
        float ruidoY = Mathf.PerlinNoise(Time.time, offsetRuido) * 2f - 1f;
        posicionDeseada += transform.up * ruidoY * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, Time.deltaTime * 2f);

        
        transform.RotateAround(objetivoCentral.position, ejeRotacion, velocidadVuelo * Time.deltaTime * 10f);

       
        Vector3 direccionVuelo = Vector3.Cross(direccionAlCentro, ejeRotacion).normalized;
        if (direccionVuelo != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccionVuelo);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 5f);
        }
    }

    IEnumerator RutinaDisparo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(tiempoMinEntreDisparos, tiempoMaxEntreDisparos));
            Disparar();
        }
    }

    void Disparar()
    {
        if (prefabLaser != null && puntosDisparo.Length > 0)
        {
            // Recorremos todos los puntos de la lista y disparamos desde cada uno
            foreach (Transform punto in puntosDisparo)
            {
                GameObject laser = Instantiate(prefabLaser, punto.position, punto.rotation);
                Destroy(laser, 1.5f);

                Rigidbody rb = laser.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Usamos la propiedad actualizada para evitar el error CS0618
                    rb.linearVelocity = punto.forward * 25f;
                }
            }
        }
    }
}