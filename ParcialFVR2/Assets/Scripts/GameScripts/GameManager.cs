using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void IniciarPartida()
    {
        GameState.gano = false;
        PlayerPrefs.DeleteKey("ResultadoGuardado");
    }
}