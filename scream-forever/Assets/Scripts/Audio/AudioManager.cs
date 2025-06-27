using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : SingletonBehavior
{
    public enum Bank
    {
        Testing,
        BGM,
        ENV,
        SFX,
        UI,
    }

    public static AudioManager Instance => Global.Instance.Audio;

    private EventInstance bgmEvent;
    private EventInstance sfxEvent;

    public const string NoBGMKey = "none";
    private const string NoChangeBGMKey = "no_change";
    private const float FadeSeconds = 0.5f;

    public float BaseVolume { get; set; } = 1.0f;

    public string CurrentBGMKey { get; private set; }

    public void Start()
    {
        CurrentBGMKey = NoBGMKey;
        SetVolume();
    }

    public void PlayBGM(string key)
    {
        if (Global.Instance.Data.GetSwitch("disable_bgm"))
        {
            return;
        }
        if (key != CurrentBGMKey && key != NoChangeBGMKey && key != null && key.Length > 0)
        {
            if (bgmEvent.hasHandle())
            {
                bgmEvent.stop(STOP_MODE.ALLOWFADEOUT);
                bgmEvent.clearHandle();
            }

            if (Global.Instance.Avatar != null)
            {
                foreach (var emitter in Global.Instance.Avatar.radios.emitters)
                {
                    emitter.emitter.Stop();
                }
            }
            if (key.StartsWith(NoBGMKey))
            {
                return;
            }
            BaseVolume = 1f;
            SetVolume();
            CurrentBGMKey = key;

            if (Global.Instance.Avatar == null || !Global.Instance.Avatar.radios.TryPlay(key))
            {
                bgmEvent = RuntimeManager.CreateInstance($"event:/BGM/{key}");
                bgmEvent.start();
            }
        }
    }

    public void PlaySFX(string sfxKey, Bank bank = Bank.SFX)
    {
        var avList = Global.Instance.Avatar.sfx;
        if (avList.TryPlay(sfxKey))
        {
            return;
        }
        
        //var banksy = bank == Bank.BGM ? "BGM" : "Other/" + bank;
        sfxEvent = RuntimeManager.CreateInstance($"event:/{bank}/{sfxKey}");
        sfxEvent.start();
    }

    public void SetGlobalParam(string key, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(key, value);
    }

    public void SetGlobalParam(string key, string value)
    {
        RuntimeManager.StudioSystem.setParameterByNameWithLabel(key, value);
    }

    public void StopSFX()
    {
        sfxEvent.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void SetVolume()
    {
        //var sfxBus = RuntimeManager.GetBus("bus:/SFX");
        var bgmBus = RuntimeManager.GetBus("bus:/BGM");
        var envBus = RuntimeManager.GetBus("bus:/ENV");
        //var uiBus = RuntimeManager.GetBus("bus:/UI");
        //sfxBus.setVolume(BaseVolume);
        bgmBus.setVolume(BaseVolume);
        //uiBus.setVolume(BaseVolume);
        envBus.setVolume(BaseVolume);
    }

    public IEnumerator FadeOutRoutine(float durationSeconds)
    {
        CurrentBGMKey = NoBGMKey;
        while (BaseVolume > 0.0f)
        {
            BaseVolume -= Time.deltaTime / durationSeconds;
            if (BaseVolume < 0.0f)
            {
                BaseVolume = 0.0f;
            }
            SetVolume();
            yield return null;
        }
        if (bgmEvent.hasHandle())
        {
            bgmEvent.stop(STOP_MODE.ALLOWFADEOUT);
            bgmEvent.clearHandle();
        }

        BaseVolume = 1.0f;
        SetVolume();
        PlayBGM(NoBGMKey);
    }
}
