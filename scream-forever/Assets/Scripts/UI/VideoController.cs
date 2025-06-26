using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] protected List<GameObject> toEnable;
    [SerializeField] protected List<GameObject> toDisable;
    [Space]
    [SerializeField] private Vector2 altBoundsX;
    [SerializeField] private Vector2 altBoundsY;
    [SerializeField] private float altAllowance;

    public IEnumerator ShowRoutine()
    {
        Global.Instance.Maps.ActiveMap.fog.gameObject.SetActive(false);
        Global.Instance.Maps.ActiveMap.terrain.gameObject.SetActive(false);
        Global.Instance.Maps.ActiveMap.road.gameObject.SetActive(false);
        
        Global.Instance.Avatar.GetComponent<Rigidbody>().useGravity = false;
        var boundsX = Global.Instance.Avatar.rotationXBounds;
        var boundsY = Global.Instance.Avatar.rotationYBounds;
        var stickyAllowance = Global.Instance.Avatar.stickyAllowance;
        Global.Instance.Avatar.rotationXBounds = altBoundsX;
        Global.Instance.Avatar.rotationYBounds = altBoundsY;
        Global.Instance.Avatar.stickyAllowance = altAllowance;
        
        foreach (var obj in toEnable)
        {
            obj.SetActive(true);
        }
        foreach (var obj in toDisable)
        {
            obj.SetActive(false);
        }
        player.Play();
        yield return CoUtils.Wait(1f);
        while (player.isPlaying)
        {
            yield return null;
        }
        foreach (var obj in toEnable)
        {
            obj.SetActive(false);
        }
        foreach (var obj in toDisable)
        {
            obj.SetActive(true);
        }
        
        Global.Instance.Avatar.GetComponent<Rigidbody>().useGravity = true;
        Global.Instance.Avatar.rotationXBounds = boundsX;
        Global.Instance.Avatar.rotationYBounds = boundsY;
        Global.Instance.Avatar.stickyAllowance = stickyAllowance;
        
        Global.Instance.Maps.ActiveMap.fog.gameObject.SetActive(true);
        Global.Instance.Maps.ActiveMap.terrain.gameObject.SetActive(true);
        Global.Instance.Maps.ActiveMap.road.gameObject.SetActive(true);
    }
}