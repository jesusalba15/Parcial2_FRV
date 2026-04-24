using UnityEngine;
using System.Collections;

public class PlayerShooterAR : MonoBehaviour
{
    [Header("Configuración del Láser")]
    public GameObject prefabLaserAzul;
    public float fuerzaDisparo = 40f; 
    public float tiempoVidaLaser = 1.5f;

    [Header("Efectos")]
    public GameObject prefabMuzzleFlash; 
    public AudioClip sonidoDisparo; 
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    public void Disparar()
    {
        if (prefabLaserAzul == null) return;

        
        GameObject laser = Instantiate(prefabLaserAzul, transform.position, transform.rotation);

       
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
           
            rb.linearVelocity = transform.forward * fuerzaDisparo;
        }

        
        Destroy(laser, tiempoVidaLaser);

        
        if (prefabMuzzleFlash != null)
        {
            GameObject flash = Instantiate(prefabMuzzleFlash, transform.position + transform.forward * 0.1f, transform.rotation);
            Destroy(flash, 0.1f);
        }

        if (audioSource != null && sonidoDisparo != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }
}