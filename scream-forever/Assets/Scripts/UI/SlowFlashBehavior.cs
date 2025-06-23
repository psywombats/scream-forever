using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class SlowFlashBehavior : MonoBehaviour {

    public float period = 1.0f;
    public float transitionDuration = 0.1f;

    public bool Enabled { get; set; }

    private float originalAlpha;
    private float elapsed;
    private bool off;

    public void Start() 
    {
        originalAlpha = GetAlpha();
        off = true;
        SetAlpha(0f);
    }

    public void TurnOn()
    {
        Enabled = true;
        SetAlpha(0f);
        elapsed = period / 2.0f;
    }

    public void TurnOff()
    {
        Enabled = false;
        elapsed = period / 2.0f;
    }

    public void Update() 
    {
        if (!Enabled && off)
        {
            return;
        }
        elapsed += Time.deltaTime;
        if (elapsed >= period / 2.0f) 
        {
            if (Enabled && !off) 
            {
                var tween = DOTween.To(GetAlpha, SetAlpha, 0.0f, transitionDuration);
                StartCoroutine(CoUtils.RunTween(tween));
            } 
            else 
            {
                var tween = DOTween.To(GetAlpha, SetAlpha, originalAlpha, transitionDuration);
                StartCoroutine(CoUtils.RunTween(tween));
            }
            off = !off;
            elapsed -= period / 2.0f;
        }
    }

    protected abstract float GetAlpha();
    protected abstract void SetAlpha(float alpha);
}
