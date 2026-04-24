using UnityEngine;
using System.Collections.Generic;

public class GeneradorBatalla : MonoBehaviour
{
    [Header("Configuración de Flota")]
    public List<GameObject> prefabsNaves;
    public int cantidadDeNaves = 15;
    public float radioDeBatalla = 3f;

    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Naves"), LayerMask.NameToLayer("Estrella"), true);

        GenerarEnjambre();
    }

    void GenerarEnjambre()
    {
        for (int i = 0; i < cantidadDeNaves; i++)
        {
            GameObject modeloElegido = prefabsNaves[Random.Range(0, prefabsNaves.Count)];

            
            Vector3 posicionAleatoria = transform.position + Random.onUnitSphere * (radioDeBatalla + 2f);

            GameObject nuevaNave = Instantiate(modeloElegido, posicionAleatoria, Quaternion.identity);

            NaveBatallaIA ia = nuevaNave.GetComponent<NaveBatallaIA>();
            if (ia == null) ia = nuevaNave.AddComponent<NaveBatallaIA>();

            ia.objetivoCentral = this.transform;

            
            ia.radioOrbita = Random.Range(radioDeBatalla, radioDeBatalla + 2f);

            
            ia.velocidadVuelo = Random.Range(8f, 15f);

            nuevaNave.transform.SetParent(this.transform);
        }
    }
}