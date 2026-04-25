using UnityEngine;

public class ControladorDisparoJugador : MonoBehaviour
{
    [Header("Recursos de Disparo")]
    public GameObject prefabBalaJugador; 
    public AudioClip sonidoDisparo;     

    [Header("Ajustes")]
    public float velocidadBala = 40f;
    public float escalaBala = 0.02f;  

    private AudioSource audioSource;

    void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    
    public void DispararDesdeCamara()
    {
       
        Transform cam = Camera.main.transform;

        
        GameObject bala = Instantiate(prefabBalaJugador, cam.position, cam.rotation);

        
        bala.transform.localScale = Vector3.one * escalaBala;

        
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.linearVelocity = cam.forward * velocidadBala;
        }

        
        if (sonidoDisparo != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }

            
        Destroy(bala, 2f);
    }
}
