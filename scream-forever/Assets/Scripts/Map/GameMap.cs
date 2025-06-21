using UnityEngine;

public class GameMap : MonoBehaviour
{
    public const string ResourcePath = "Maps/";
    
    [SerializeField] public GameObject eventLayer;
    [Space]
    [Header("References")]
    [SerializeField] public MarchingTerrain terrain;

    public virtual void Start()
    {
        if (Application.isPlaying)
        {
            if (Global.Instance.Maps.ActiveMap == null)
            {
                Global.Instance.Maps.ActiveMap = this;
                OnTeleportTo(null);
            }
        }
    }

    public virtual void OnTeleportTo(GameMap from)
    {

    }

    public virtual void OnTeleportAway(GameMap nextMap)
    {

    }
}
