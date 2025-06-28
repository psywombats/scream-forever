using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CrashMonitor : MonoBehaviour
{
    [SerializeField] private MultibumpComponent bump;
    [SerializeField] private List<MonoBehaviour> crashBehaviors;
    [Space]
    [SerializeField] private float bumpCutoff = .1f;
    [SerializeField] private float crashCutoff = .3f;
    [SerializeField] private float flipAngle = -20;
    
    public void Update()
    {
        var avatar = Global.Instance.Avatar;
        if (transform.rotation.eulerAngles.x < flipAngle && avatar.SpeedRatio > crashCutoff)
        {
            StartCrash();
        }

        bump.ContinuousMode = !avatar.IsCrashing && avatar.DistanceFromRoad > bumpCutoff;

        if (!avatar.IsCrashing && avatar.DistanceFromRoad > 8f)
        {
            if (avatar.SpeedRatio > crashCutoff)
            {
                StartCrash();
            }
            else
            {
                avatar.IsSpeeding = true;
            }
        }
    }

    private void StartCrash()
    {
        if (Global.Instance.Lua.IsRunning())
        {
            Global.Instance.Lua.ForceTerminate();
        }
        Global.Instance.Avatar.IsCrashing = true;
        Global.Instance.Avatar.IsSpeeding = true;
        Global.Instance.Avatar.PauseInput();
        MapOverlayUI.Instance.screenViewGlitch.enabled = true;
        foreach (var component in crashBehaviors)
        {
            component.enabled = true;
        }

        Global.Instance.StartCoroutine(ResetRoutine());
    }

    public void StopCrash()
    {
        MapOverlayUI.Instance.screenViewGlitch.enabled = false;
        foreach (var component in crashBehaviors)
        {
            component.enabled = false;
        }
        MapOverlayUI.Instance.screenViewGlitch.color = Color.clear;
    }

    public IEnumerator ResetRoutine()
    {
        yield return CoUtils.Wait(1f);
        yield return CoUtils.RunTween(MapOverlayUI.Instance.screenViewGlitch.DOFade(1f, 1f));
        yield return CoUtils.Wait(1f);
        yield return CoUtils.RunTween(MapOverlayUI.Instance.fader.DOFade(1f, 1.5f));
        StopCrash();
        Global.Instance.StartCoroutine(Global.Instance.Audio.FadeOutRoutine(.5f));
        yield return CoUtils.Wait(1f);
        Global.Instance.Input.GameReset();
        yield return Global.Instance.Maps.TeleportRoutine(Global.Instance.Maps.ActiveMapName);
        
    }
}
