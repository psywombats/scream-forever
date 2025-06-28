using DynamicFogAndMist2;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public const string ResourcePath = "Maps/";
    
    [SerializeField] public GameObject eventLayer;
    [SerializeField] public Material skybox;
    [SerializeField] public Light sun;
    [SerializeField] public PlayerController avatar;
    [SerializeField] public string firstScript;
    [Space]
    [Header("References")]
    [SerializeField] public MarchingTerrain terrain;
    [SerializeField] public RoadManager road;
    [SerializeField] public DynamicFogManager fogger;
    [SerializeField] public DynamicFog fog;

    public virtual void Start()
    {
        if (Application.isPlaying)
        {
            if (Global.Instance.Maps.ActiveMap == null)
            {
                Global.Instance.Maps.ActiveMap = this;
                Global.Instance.Maps.ActiveMapName = name;
                OnTeleportTo(null);
            }
        }
    }

    public virtual void OnTeleportTo(GameMap from)
    {
        if (Global.Instance.Avatar != null)
        {
            fogger.mainCamera = Global.Instance.Avatar.camera;
        }
        RenderSettings.skybox = skybox;
        RenderSettings.sun = sun;
        if (!string.IsNullOrEmpty(firstScript))
        {
            StartCoroutine(Global.Instance.Lua.RunRoutine($"play('{firstScript}')", canBlock: true));
        }
    }

    public virtual void OnTeleportAway(GameMap nextMap)
    {

    }
    
    public void Regenerate(Vector3Int radius, bool usePlayer = false)
    {
        DestroyChunks();
        terrain.EnsureChunks(radius, usePlayer: usePlayer);
    }

    public void DestroyChunks()
    {
        terrain.CullAll();
    }
}
