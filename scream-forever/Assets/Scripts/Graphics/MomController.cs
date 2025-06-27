using System.Collections.Generic;
using DG.Tweening;
using DynamicFogAndMist2;
using UnityEngine;

public class MomController : MonoBehaviour
{
    [SerializeField] private DynamicFog fog;
    [SerializeField] private float toVal = .8f;
    [SerializeField] private float duration = 4f;
    [SerializeField] private float durationIn = 1f;
    [Space] 
    [SerializeField] private List<Light> lights;
    [SerializeField] private List<GameObject> backerQuads;

    private Dictionary<Light, float> origIntensities = new();
    private float fromVal;

    public void MoveInFog()
    {
        fromVal = fog.profile.densityExponential;
        DOTween.To(() => fog.profile.densityExponential, 
                v => fog.profile.densityExponential = v, toVal, duration).Play();
        foreach (var light in lights)
        {
            origIntensities[light] = light.intensity;
            light.DOIntensity(0, duration);
        }

        foreach (var quad in backerQuads)
        {
            quad.gameObject.SetActive(true);
        }
    }

    public void CancelFog()
    {
        DOTween.To(() => fog.profile.densityExponential, 
            v => fog.profile.densityExponential = v, fromVal, durationIn).Play();
        foreach (var light in lights)
        {
            origIntensities[light] = light.intensity;
            light.DOIntensity(0, origIntensities[light]);
        }
        
        foreach (var quad in backerQuads)
        {
            quad.gameObject.SetActive(false);
        }
    }
}