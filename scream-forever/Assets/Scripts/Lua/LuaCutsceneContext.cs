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
        Lua.Globals["cs_speak"] = (Action<DynValue, DynValue>)Speak;
        Lua.Globals["cs_expr"] = (Action<DynValue, DynValue>)Express;
        Lua.Globals["cs_enter"] = (Action<DynValue, DynValue>)Enter;
        Lua.Globals["cs_exit"] = (Action<DynValue>)Exit;
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
        var raw = rawLua.IsNil() ? false : rawLua.Boolean;
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

    public void Speak(DynValue speakerNameLua, DynValue messageLua)
    {
        var speaker = IndexDatabase.Instance.Speakers.GetData(speakerNameLua.String);
        RunRoutineFromLua(SpeakRoutine(speaker, messageLua.String));
    }
    private IEnumerator SpeakRoutine(SpeakerData speaker, string message)
    {
        yield return MapOverlayUI.Instance.nvl.SpeakRoutine(speaker, message);
    }

    private void Enter(DynValue speakerTag, DynValue expression)
    {
        var speakerData = IndexDatabase.Instance.Speakers.GetData(speakerTag.String);
        RunRoutineFromLua(MapOverlayUI.Instance.nvl.EnterRoutine(speakerData, expression.String));
    }
    
    private void Exit(DynValue speakerTag)
    {
        var speakerData = IndexDatabase.Instance.Speakers.GetData(speakerTag.String);
        RunRoutineFromLua(MapOverlayUI.Instance.nvl.ExitRoutine(speakerData));
    }
}
