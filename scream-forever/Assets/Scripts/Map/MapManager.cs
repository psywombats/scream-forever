using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class MapManager : SingletonBehavior
{
    public static MapManager Instance => Global.Instance.Maps;

    private PlayerController avatar;
    public PlayerController Avatar
    {
        get
        {
            if (avatar == null)
            {
                avatar = FindObjectOfType<PlayerController>();
            }
            return avatar;
        }
        set => avatar = value;
    }
    public GameMap ActiveMap { get; set; }
    public string ActiveMapName { get; set; }

    private new Camera camera;
    public Camera Camera
    {
        get
        {
            if (camera == null)
            {
                camera = Global.Instance.Avatar.camera;
            }
            return camera;
        }
    }
    
    public void Teleport(string mapName)
    {
        StartCoroutine(TeleportRoutine(mapName, isRaw: true));
    }

    public IEnumerator TeleportRoutine(string mapName, bool isRaw = false)
    {
        var avatarExists = Avatar != null;
        if (avatarExists)
        {
            Avatar.PauseInput();
        }
        if (!isRaw)
        {
            yield return CoUtils.RunTween(MapOverlayUI.Instance.fader.DOFade(1f, .5f));
        }

        RawTeleport(mapName);

        if (!isRaw)
        {
            yield return CoUtils.RunTween(MapOverlayUI.Instance.fader.DOFade(0f, .5f));
        }
        if (avatarExists)
        {
            Avatar.UnpauseInput();
        }
    }

    private void RawTeleport(string mapName)
    {
        var oldMap = ActiveMap;
        var newMapInstance = InstantiateMap(mapName);
        if (ActiveMap != null)
        {
            ActiveMap.OnTeleportAway(newMapInstance);
        }
        
        if (ActiveMap != null)
        {
            Destroy(ActiveMap.gameObject);
        }

        ActiveMap = newMapInstance;
        ActiveMapName = mapName;
        AddInitialAvatar();

        Avatar.OnTeleport();
        ActiveMap.OnTeleportTo(oldMap);
    }

    private GameMap InstantiateMap(string mapName)
    {
        var newMapObject = Resources.Load<GameObject>("Maps/" + mapName);
        Assert.IsNotNull(newMapObject);
        var obj = Instantiate(newMapObject);
        obj.name = newMapObject.name;
        obj.transform.position = Vector3.zero;
        return obj.GetComponent<GameMap>();
    }

    private void AddInitialAvatar()
    {
        if (Avatar == null)
        {
            var av = Instantiate(ActiveMap.playerPrefab.gameObject);
            av.gameObject.SetActive(true);
            Avatar = av.GetComponent<PlayerController>();
        }
        Avatar.transform.SetParent(ActiveMap.eventLayer.transform, false);
    }
}
