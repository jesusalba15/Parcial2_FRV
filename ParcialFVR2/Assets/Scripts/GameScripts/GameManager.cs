using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void IniciarPartida()
    {
        GameState.gano = false;

        // 🔥 CLAVE: permitir guardar nuevo resultado
        PlayerPrefs.DeleteKey("ResultadoGuardado");
    }
}