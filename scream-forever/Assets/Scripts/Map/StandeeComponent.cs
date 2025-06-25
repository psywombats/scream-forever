using System.Collections;
using DG.Tweening;
using UnityEngine;

public class StandeeComponent : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteHolder;
    [SerializeField] public Transform offsetter;
    [SerializeField] public float defaultDuration;
    [SerializeField] public bool spawnOnEnable;

    public void Start()
    {
        if (spawnOnEnable)
        {
            Hide();
        }
    }

    public void OnEnable()
    {
        if (spawnOnEnable)
        {
            Hide();
            StartCoroutine(EnterRoutine());
        }
    }

    public void AssignBySpeaker(string speakerTag)
    {
        spriteHolder.sprite = IndexDatabase.Instance.Speakers.GetData(speakerTag).sprite;
    }

    public IEnumerator EnterRoutine(float duration = -1f)
    {
        var enterTime = duration < 0f ? defaultDuration : duration;
        return CoUtils.RunParallel(this,
    CoUtils.RunTween(offsetter.DOScaleY(1f, enterTime).SetEase(Ease.OutBounce)),
                CoUtils.RunTween(offsetter.DOScaleX(1f, enterTime).SetEase(Ease.OutCubic)));
    }
    
    public IEnumerator ExitRoutine(float duration = -1f)
    {
        var enterTime = duration < 0f ? defaultDuration : duration;
        return CoUtils.RunParallel(this,
            CoUtils.RunTween(offsetter.DOScaleY(0f, enterTime).SetEase(Ease.OutBounce)),
            CoUtils.RunTween(offsetter.DOScaleX(0f, enterTime).SetEase(Ease.OutCubic)));
    }

    public void Show()
    {
        offsetter.localScale = new Vector3(1, 1, 1);
    }

    public void Hide()
    {
        offsetter.localScale = new Vector3(0, 0, 1);
    }
}