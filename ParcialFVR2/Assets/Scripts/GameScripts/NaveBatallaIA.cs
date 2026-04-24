using UnityEngine;
using System.Collections;

public class NaveBatallaIA : MonoBehaviour
{
    [Header("Vuelo")]
    public Transform objetivoCentral;
    public float velocidadVuelo = 5f;
    public float radioOrbita = 2f;
    public float distanciaMaximaDesaparecer = 25f; 

    private Vector3 ejeRotacion;
    private float offsetRuido;
    private bool estaDerribada = false;
    private Rigidbody rb;

    [Header("Combate")]
    public GameObject prefabLaser;
    public GameObject prefabExplosion; 
    public Transform[] puntosDisparo;
    public float tiempoMinEntreDisparos = 3f; 
    public float tiempoMaxEntreDisparos = 8f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ejeRotacion = Random.onUnitSphere;
        offsetRuido = Random.Range(0f, 100f);

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false; 
        }

        StartCoroutine(RutinaDisparo());
    }

    void Update()
    {
        
        if (estaDerribada)
        {
            CheckLimpieza();
            return;
        }

        if (objetivoCentral == null) return;

        ManejarVuelo();
    }

    void ManejarVuelo()
    {
        Vector3 direccionAlCentro = transform.position - objetivoCentral.position;

        
        if (direccionAlCentro.magnitude > distanciaMaximaDesaparecer)
        {
            Destroy(gameObject);
        }

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

    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Laser") && !estaDerribada)
        {
            
            Destroy(collision.gameObject);

            ExplotarYCaer();
        }
    }

    void ExplotarYCaer()
    {
        estaDerribada = true;
        StopAllCoroutines(); 

        
        if (prefabExplosion != null)
        {
            
            GameObject exp = Instantiate(prefabExplosion, transform.position, transform.rotation);
            Destroy(exp, 2f);
        }

        // 2. Física de caída
        if (rb != null)
        {
            rb.useGravity = true;
            
            rb.linearVelocity = transform.forward * 1.5f;
            rb.AddTorque(Random.insideUnitSphere * 12f, ForceMode.Impulse);
        }

        
        Destroy(gameObject, 3.5f);
    }

    void CheckLimpieza()
    {
        
        if (Vector3.Distance(transform.position, objetivoCentral.position) > distanciaMaximaDesaparecer)
        {
            Destroy(gameObject);
        }
    }

    // --- DISPAROS ---
    IEnumerator RutinaDisparo()
    {
        while (!estaDerribada)
        {
            yield return new WaitForSeconds(Random.Range(tiempoMinEntreDisparos, tiempoMaxEntreDisparos));
            Disparar();
        }
    }

    void Disparar()
    {
        if (prefabLaser != null && puntosDisparo.Length > 0)
        {
            foreach (Transform punto in puntosDisparo)
            {
                GameObject laser = Instantiate(prefabLaser, punto.position, punto.rotation);
                Destroy(laser, 1.2f);

                Rigidbody laserRb = laser.GetComponent<Rigidbody>();
                if (laserRb != null)
                {
                    laserRb.linearVelocity = punto.forward * 30f;
                }
            }
        }
    }
}