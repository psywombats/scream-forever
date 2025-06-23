using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BumpComponent : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float duration = .01f;
    [SerializeField] private float speedCutoff = .1f;
    [SerializeField] private float delay = .01f;
    [SerializeField] public float continuousMult = 5f;

    public bool IsBumping { get; private set; }

    public void Bump(float offset, float durationIn, float durationOut, float mult, bool useDelay = true)
    {
        StartCoroutine(BumpRoutine(offset, durationIn, durationOut, mult, useDelay));
    }

    public void Bump(float mult, bool useDelay) => Bump(offset, duration, duration, mult, useDelay);

    private IEnumerator BumpRoutine(float off, float durationIn, float durationOut, float mult = 1f, bool useDelay = true)
    {
        if (IsBumping)
        {
            yield break;
        }
        IsBumping = true;
        
        var jitter = Random.Range(.7f, 1f);
        var finalOff = off * jitter * mult;

        if (useDelay)
        {
            yield return CoUtils.Wait(delay);
        }
        var orig = transform.localPosition.y;
        var target = transform.localPosition.y + finalOff;
        yield return CoUtils.RunTween(transform.DOLocalMoveY(target, durationIn / 2).SetEase(Ease.OutQuad));
        yield return CoUtils.RunTween(transform.DOLocalMoveY(orig, durationOut * (1-jitter)).SetEase(Ease.OutBounce));
        
        IsBumping = false;
    }

    public IEnumerator SpeedBumpRoutine(float mult, bool useDelay = true)
    {
        var ratio = Global.Instance.Avatar.SpeedRatio;
        if (ratio < speedCutoff)
        {
            yield break;
        }

        ratio += .3f;
        yield return BumpRoutine(offset * ratio, duration,  duration, mult, useDelay);
    }
}