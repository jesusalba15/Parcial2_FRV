using UnityEngine;

public class DetectorImpactoJugador : MonoBehaviour
{
    
    [HideInInspector]
    public MisionEstrellaDeLaMuerte misionManager;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("BalaJugador"))
        {
            
            if (misionManager != null)
            {
                misionManager.RegistrarAcierto();
            }

            
            Destroy(other.gameObject);
        }
    }
}