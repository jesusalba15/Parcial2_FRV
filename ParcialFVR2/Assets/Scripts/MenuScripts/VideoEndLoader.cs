using UnityEngine;
using UnityEngine.Video;

public class VideoEndLoader : MonoBehaviour
{
    public string siguienteEscena = "SceneEpilogo";
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // 🔥 Evento cuando termina el video
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneController.instance.LoadScene(siguienteEscena);
    }
}