using UnityEngine;
using UnityEngine.SceneManagement;

public class MapOverlayUI : MonoBehaviour
{
    private static MapOverlayUI instance;
    private static Scene lastScene;
    public static MapOverlayUI Instance
    {
        get
        {
            Scene scene = SceneManager.GetActiveScene();
            if (lastScene != scene)
            {
                lastScene = scene;
                instance = null;
            }
            if (instance == null)
            {
                instance = FindObjectOfType<MapOverlayUI>();
            }
            return instance;
        }
    }

    [SerializeField] public NVLComponent nvl;
    [SerializeField] public CanvasGroup fader;
    [SerializeField] public MouseChoiceSelector selector;
}
