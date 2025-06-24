using System.Collections;
using UnityEngine;

public class MouseSelectionSlider : MonoBehaviour
{
    [SerializeField] public RectTransform keyImage;
    [SerializeField] public Vector2 emptyPos;
    [SerializeField] public Vector2 fullPos;
    [Space]
    [SerializeField] public float smoothRate;

    private float targetVal;

    // from 0 to 1
    public void SetValue(float t)
    {
        t = Mathf.Clamp(t, 0, 1);
        targetVal = t;
        InternalSet(t);
    }

    public void SmoothValue(float t)
    {
        StartCoroutine(SmoothRoutine(t));
    }

    private IEnumerator SmoothRoutine(float newTarget)
    {
        var at = targetVal;
        targetVal = newTarget;
        while (targetVal == newTarget && at != targetVal)
        {
            if (at < targetVal)
            {
                at += Time.deltaTime * smoothRate;
                if (at > targetVal) at = targetVal;
            }
            else
            {
                at -= Time.deltaTime * smoothRate;
                if (at < targetVal) at = targetVal;
            }

            InternalSet(at);
            yield return null;
        }
    }

    private void InternalSet(float t)
    {
        var pos = Vector2.Lerp(emptyPos, fullPos, t);
        keyImage.anchoredPosition = pos;
    }
}
