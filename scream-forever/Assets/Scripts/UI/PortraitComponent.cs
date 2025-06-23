using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class PortraitComponent : MonoBehaviour
{
    [SerializeField] private NVLComponent nvl;
    [SerializeField] private string speakerTag;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private List<Image> pointers;
    [SerializeField] private Light highlight;
    [SerializeField] private Transform offsetter;
    [Space] 
    [SerializeField] private float highlightTime = .3f;
    [SerializeField] private float highlightIntensity = .2f;
    [SerializeField] private float enterTime = .5f;

    public SpeakerData Speaker => IndexDatabase.Instance.Speakers.GetData(speakerTag);
    
    public bool IsHighlighted { get; private set; }

    public void Start()
    {
        offsetter.localScale = new Vector3(0, 0, 1);
    }

    private Sprite GetSpriteForExpr(string expr)
    {
        if (string.IsNullOrEmpty(expr))
        {
            return Speaker.sprite;
        }
        Sprite found = null;
        foreach (var sub in Speaker.exprs)
        {
            if (sub.key == expr)
            {
                found = sub.sprite;
                break;
            }
        }
        if (found == null)
        {
            found = Speaker.sprite;
            Debug.LogWarning("Couldn't find expression " + expr + " for " + Speaker.Key);
        }
        return found;
    }

    public IEnumerator HighlightRoutine(bool showPointers = true)
    {
        var tasks = new List<IEnumerator>();
        if (highlight.intensity != highlightIntensity)
        {
            tasks.Add(CoUtils.RunTween(highlight.DOIntensity(highlightIntensity, highlightTime)));
        }
        if (showPointers)
        {
            tasks.AddRange(pointers.Select(pointer => CoUtils.RunTween(pointer.DOFade(1f, highlightTime))));
        }
        yield return CoUtils.RunParallel(this, tasks.ToArray());

        IsHighlighted = true;
    }

    public IEnumerator UnhighlightRoutine()
    {
        if (highlight.intensity == 0)
        {
            yield break;
        }

        var tasks = new List<IEnumerator>()
        {
            CoUtils.RunTween(highlight.DOIntensity(0, highlightTime))
        };
        tasks.AddRange(pointers.Select(pointer => CoUtils.RunTween(pointer.DOFade(0f, highlightTime))));
        yield return CoUtils.RunParallel(this, tasks.ToArray());

        IsHighlighted = false;
    }
    
    public IEnumerator ExpressRoutine(string expr)
    {
        if (!IsHighlighted)
        {
            yield return nvl.SetHighlightRoutine(Speaker);
        }
        sprite.sprite = GetSpriteForExpr(expr);
    }
    
    public IEnumerator EnterRoutine(string expr = null)
    {
        sprite.sprite = GetSpriteForExpr(expr);
        offsetter.localScale = new Vector3(0, 0, 1);
        StartCoroutine(HighlightRoutine(showPointers: false));
        offsetter.DOScaleY(1f, enterTime).SetEase(Ease.OutBounce).Play();
        offsetter.DOScaleX(1f, enterTime).SetEase(Ease.OutCubic).Play();
        yield return CoUtils.Wait(enterTime);
    }

    public IEnumerator ExitRoutine()
    {
        offsetter.localScale = new Vector3(1, 1, 1);
        yield return HighlightRoutine(showPointers: false);
        offsetter.DOScaleY(0f, enterTime).SetEase(Ease.OutBounce).Play();
        offsetter.DOScaleX(0f, enterTime).SetEase(Ease.OutCubic).Play();
        yield return CoUtils.Wait(enterTime);
        StartCoroutine(UnhighlightRoutine());
    }
}
