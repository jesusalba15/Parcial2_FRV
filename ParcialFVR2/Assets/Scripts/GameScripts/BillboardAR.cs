using UnityEngine;

public class BillboardAR : MonoBehaviour
{
    private Transform camaraPrincipal;

    void Start()
    {
        
        if (Camera.main != null)
            camaraPrincipal = Camera.main.transform;
    }

    
    void LateUpdate()
    {
        if (camaraPrincipal == null) return;

        
        transform.LookAt(transform.position + camaraPrincipal.rotation * Vector3.forward,
                         camaraPrincipal.rotation * Vector3.up);
    }
}