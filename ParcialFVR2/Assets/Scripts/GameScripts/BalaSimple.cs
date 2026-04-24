using UnityEngine;

public class BalaSimple : MonoBehaviour
{
    public float velocidad = 20f; // Súbele a 50 si las ves lentas
    public float tiempoVida = 1.5f;
    public GameObject efectoExplosion; 

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
       
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Nave"))
        {
            InstanciarExplosion();

           

            Destroy(other.gameObject); 
            Destroy(gameObject); 
        }
    }

    void InstanciarExplosion()
    {
        if (efectoExplosion != null)
        {
            GameObject exp = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
           
            exp.transform.localScale = Vector3.one * 0.05f;
            Destroy(exp, 2f);
        }
    }
}