using System.Collections;
using System;
using MoonSharp.Interpreter;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuaCutsceneContext : LuaContext
{
    private static readonly string DefinesPath = "Lua/Defines/CutsceneDefines";

    public override IEnumerator RunRoutine(LuaScript script, bool canBlock)
    {
        if (canBlock && Global.Instance.Avatar != null)
        {
            //Global.Instance.Avatar.PauseInput();
        }
        yield return base.RunRoutine(script, canBlock);
        if (canBlock && Global.Instance.Avatar != null)
        {
            //Global.Instance.Avatar.UnpauseInput();
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        LoadDefines(DefinesPath);
    }

    public void RunTextboxRoutineFromLua(IEnumerator routine)
    {
        base.RunRoutineFromLua(routine);
    }

    protected void ResumeNextFrame()
    {
        Global.Instance.StartCoroutine(ResumeRoutine());
    }
    protected IEnumerator ResumeRoutine()
    {
        yield return null;
        ResumeAwaitedScript();
    }

    protected override void AssignGlobals()
    {
        base.AssignGlobals();
        Lua.Globals["playBGM"] = (Action<DynValue>)PlayBGM;
        Lua.Globals["playSFX"] = (Action<DynValue>)PlaySound;
        Lua.Globals["sceneSwitch"] = (Action<DynValue, DynValue>)SetSwitch;
        Lua.Globals["cs_teleport"] = (Action<DynValue, DynValue>)Teleport;
        Lua.Globals["cs_fadeOutBGM"] = (Action<DynValue>)FadeOutBGM;
        Lua.Globals["cs_fade"] = (Action<DynValue, DynValue>)Fade;

        Lua.Globals["cs_enterNVL"] = (Action<DynValue>)EnterNVL;
        Lua.Globals["cs_exitNVL"] = (Action)ExitNVL;
        Lua.Globals["cs_speak"] = (Action<DynValue, DynValue, DynValue>)Speak;
        Lua.Globals["cs_expr"] = (Action<DynValue, DynValue>)Express;
        Lua.Globals["cs_enter"] = (Action<DynValue, DynValue>)Enter;
        Lua.Globals["cs_exit"] = (Action<DynValue, DynValue>)Exit;
        Lua.Globals["cs_choose"] = (Action<DynValue, DynValue>)Choose;
        Lua.Globals["cs_smoothBrake"] = (Action<DynValue>)SmoothBrake;
        Lua.Globals["cs_distBrake"] = (Action<DynValue>)DistBrake;
        Lua.Globals["cs_driveWait"] = (Action<DynValue>)DriveWait;
        Lua.Globals["cs_pamphlet"] = (Action<DynValue>)ViewPamphlet;
        Lua.Globals["cs_video"] = (Action<DynValue>)Video;

        Lua.Globals["bump"] = (Action)Bump;
        Lua.Globals["allowDriving"] = (Action<DynValue>)AllowDriving;
        Lua.Globals["setSpeed"] = (Action<DynValue>)SetSpeed;
        Lua.Globals["impact"] = (Action)Impact;
        Lua.Globals["mom"] = (Action<DynValue>)Mom;
    }

    // === LUA CALLABLE ============================================================================


    private void PlayBGM(DynValue bgmKey)
    {
        Global.Instance.Audio.PlayBGM(bgmKey.String);
    }

    private void PlaySound(DynValue soundKey)
    {
        Global.Instance.Audio.PlaySFX(soundKey.String);
    }

    private void Teleport(DynValue mapName, DynValue rawLua)
    {
        var raw = !rawLua.IsNil() && rawLua.Boolean;
        RunRoutineFromLua(Global.Instance.Maps.TeleportRoutine(mapName.String, raw));
    }

    private void FadeOutBGM(DynValue seconds)
    {
        RunRoutineFromLua(Global.Instance.Audio.FadeOutRoutine((float)seconds.Number));
    }

    private void Fade(DynValue type, DynValue dur)
    {
        var str = type.String;
        RunRoutineFromLua(FadeRoutine(str, dur.IsNil() ? .8f : (float)dur.Number));
    }
    private IEnumerator FadeRoutine(string str, float dur)
    {
        var targetAlpha = str == "black" ? 1f : 0f;
        yield return CoUtils.RunTween(MapOverlayUI.Instance.fader.DOFade(targetAlpha, dur));
    }

    public void EnterNVL(DynValue hideBackersLua)
    {
        var hideBackers = !hideBackersLua.IsNil() && hideBackersLua.Boolean;
        RunRoutineFromLua(EnterNVLRoutine(hideBackers));
    }
    private IEnumerator EnterNVLRoutine(bool hideBackers)
    {
        yield return MapOverlayUI.Instance.nvl.ShowRoutine(hideBackers);
    }

    public void ExitNVL()
    {
        RunRoutineFromLua(ExitNVLRoutine());
    }
    private IEnumerator ExitNVLRoutine()
    {
        yield return MapOverlayUI.Instance.nvl.HideRoutine();
    }

    public void Express(DynValue charaLu, DynValue expr)
    {
        RunRoutineFromLua(ExpressRoutine(charaLu.String, expr.String));
    }
    private IEnumerator ExpressRoutine(string charaTag, string expr)
    {
        var adv = MapOverlayUI.Instance.nvl;
        var speaker = IndexDatabase.Instance.Speakers.GetData(charaTag);
        return adv.GetPortrait(speaker).ExpressRoutine(expr);
    }

    public void ClearNVL()
    {
        MapOverlayUI.Instance.nvl.Wipe();
    }

    public void Speak(DynValue speakerNameLua, DynValue messageLua, DynValue mode)
    {
        var useAnims = mode.String != "no_anim";
        var useHighlight = mode.String != "no_highlight";
        var speaker = IndexDatabase.Instance.Speakers.GetData(speakerNameLua.String);
        RunRoutineFromLua(SpeakRoutine(speaker, messageLua.String, useAnims, useHighlight));
    }
    private IEnumerator SpeakRoutine(SpeakerData speaker, string message, bool useAnims, bool useHighlight)
    {
        yield return MapOverlayUI.Instance.nvl.SpeakRoutine(speaker, message, useAnims, useHighlight);
    }

    private void Enter(DynValue speakerTag, DynValue useAnim)
    {
        var speakerData = IndexDatabase.Instance.Speakers.GetData(speakerTag.String);
        RunRoutineFromLua(MapOverlayUI.Instance.nvl.EnterRoutine(speakerData, useAnim.IsNil() || useAnim.Boolean));
    }
    
    private void Exit(DynValue speakerTag, DynValue useAnim)
    {
        var speakerData = IndexDatabase.Instance.Speakers.GetData(speakerTag.String);
        RunRoutineFromLua(MapOverlayUI.Instance.nvl.ExitRoutine(speakerData, useAnim.IsNil() || useAnim.Boolean));
    }

    private void Bump()
    {
        //Global.Instance.Avatar.Bump();
    }

    private void Choose(DynValue opt1, DynValue opt2)
    {
        RunRoutineFromLua(ChooseRoutine(opt1.String, opt2.String));
    }
    private IEnumerator ChooseRoutine(string opt1, string opt2)
    {
        yield return MapOverlayUI.Instance.selector.ChooseRoutine(opt1, opt2);
        Lua.Globals["selection"] = Marshal(MapOverlayUI.Instance.selector.Result.Value);
    }

    private void SmoothBrake(DynValue duration)
    {
        RunRoutineFromLua(Global.Instance.Avatar.SmoothBrakeRoutine((float)duration.Number));
    }
    
    private void DistBrake(DynValue duration)
    {
        RunRoutineFromLua(Global.Instance.Avatar.DistBrakeRoutine((float)duration.Number));
    }

    private void DriveWait(DynValue dist)
    {
        RunRoutineFromLua(DriveWaitRoutine((float)dist.Number));
    }
    private IEnumerator DriveWaitRoutine(float dist)
    {
        var traversed = Global.Instance.Avatar.RoadTraversed;
        while (!Global.Instance.Avatar.IsCrashing && Global.Instance.Avatar.RoadTraversed < traversed + dist)
        {
            yield return null;
        }
    }

    private void SetSpeed(DynValue speed)
    {
        Global.Instance.Avatar.Speed = (float)speed.Number;
    }

    private void Impact()
    {
        Global.Instance.StartCoroutine(MapOverlayUI.Instance.Hit.HitRoutine());
    }

    private void AllowDriving(DynValue allow)
    {
        Global.Instance.Avatar.IsDrivingAllowed = allow.Boolean;
    }

    private void ViewPamphlet(DynValue pamphletTag)
    {
        RunRoutineFromLua(MapOverlayUI.Instance.Pamphlet.ViewPamphletRoutine(pamphletTag.String));
    }

    private void Video(DynValue arg)
    {
        RunRoutineFromLua(MapOverlayUI.Instance.Video.ShowRoutine(arg.IsNil() ? 0f : (float)arg.Number));
    }

    private void Mom(DynValue showhide)
    {
        if (showhide.Boolean)
        {
            MapOverlayUI.Instance.Mom.MoveInFog();
        }
        else
        {
            MapOverlayUI.Instance.Mom.CancelFog();
        }
    }
}
