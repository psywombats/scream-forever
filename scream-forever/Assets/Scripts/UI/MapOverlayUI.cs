using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] public RawImage screenViewGlitch;

    private PamphletViewer pamphlet;
    public PamphletViewer Pamphlet
    {
        get
        {
            if (pamphlet == null)
            {
                pamphlet = FindObjectOfType<PamphletViewer>();
            }
            return pamphlet;
        }
    }
    
    private VideoController video;
    public VideoController Video
    {
        get
        {
            if (video == null)
            {
                video = FindObjectOfType<VideoController>();
            }
            return video;
        }
    }
    
    private AnimalHitController hit;
    public AnimalHitController Hit
    {
        get
        {
            if (hit == null)
            {
                hit = FindObjectOfType<AnimalHitController>();
            }
            return hit;
        }
    }
    
    private MomController mom;
    public MomController Mom
    {
        get
        {
            if (mom == null)
            {
                mom = FindObjectOfType<MomController>();
            }
            return mom;
        }
    }
}
