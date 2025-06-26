using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class PortraitComponent : MonoBehaviour
{
    [SerializeField] private string speakerTag;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Light highlight;
    [SerializeField] private StandeeComponent standee;
    [SerializeField] private Sprite primarySprite;
    [SerializeField] private Sprite secondarySprite;
    [Space] 
    [SerializeField] private float highlightTime = .3f;
    [SerializeField] private float highlightIntensity = .2f;
    [SerializeField] private float enterTime = .5f;

    public SpeakerData Speaker => IndexDatabase.Instance.Speakers.GetData(speakerTag);
    
    public bool IsHighlighted { get; private set; }

    public IEnumerable<Image> Pointers => MapOverlayUI.Instance.nvl.pointers.Where(p => p.tag == speakerTag)
        .Select(p => p.image);

    public void Start()
    {
        standee.Hide();
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
            tasks.AddRange(Pointers.Select(pointer => CoUtils.RunTween(pointer.DOFade(1f, highlightTime))));
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
        tasks.AddRange(Pointers.Select(pointer => CoUtils.RunTween(pointer.DOFade(0f, highlightTime))));
        yield return CoUtils.RunParallel(this, tasks.ToArray());

        IsHighlighted = false;
    }
    
    public IEnumerator ExpressRoutine(string expr)
    {
        yield return null;
        // if (!IsHighlighted)
        // {
        //     yield return MapOverlayUI.Instance.nvl.SetHighlightRoutine(Speaker);
        // }
        // sprite.sprite = GetSpriteForExpr(expr);
    }
    
    public IEnumerator EnterRoutine(bool useAnim)
    {
        sprite.sprite = primarySprite;
        standee.Hide();
        if (useAnim)
        {
            StartCoroutine(HighlightRoutine(showPointers: false));
        }
        yield return standee.EnterRoutine(useAnim ? enterTime : 0.05f);
    }

    public IEnumerator ExitRoutine(bool useAnim)
    {
        standee.Show();
        if (useAnim)
        {
            yield return HighlightRoutine(showPointers: false);
        }
        yield return standee.ExitRoutine(useAnim ? enterTime : .05f);
        StartCoroutine(UnhighlightRoutine());
    }

    public void StartTalking()
    {
        standee.spriteHolder.sprite = secondarySprite;
    }
    
    public void StopTalking()
    {
        standee.spriteHolder.sprite = primarySprite;
    }
}
