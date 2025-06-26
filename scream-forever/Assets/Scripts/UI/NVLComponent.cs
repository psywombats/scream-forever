using System;
using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NVLComponent : MonoBehaviour
{
    [Serializable]
    public class PointerData
    {
        public Image image;
        public string tag;
    }
    
    private static float bgTime = 0.5f;

    public PortraitComponent slotA => Global.Instance.Avatar.slotA;
    public PortraitComponent slotB => Global.Instance.Avatar.slotB;
    public PortraitComponent slotC => Global.Instance.Avatar.slotC;
    public PortraitComponent slotD => Global.Instance.Avatar.slotD;

    public GameObject backerArea;
    public ExpanderComponent backer;
    public TextAutotyper text;
    public Text nameText;
    public CanvasGroup fader;
    public CanvasGroup background;
    public StudioEventEmitter eventEmitter;
    public SlowFlashImageBehavior advanceIcon;
    public List<PointerData> pointers;

    public bool IsShown { get; private set; }

    public Dictionary<SpeakerData, string> speakerNames = new Dictionary<SpeakerData, string>();

    public void Wipe()
    {
        text.Clear();
        nameText.text = "";
    }

    public IEnumerator ShowRoutine(bool hideBackers = false)
    {
        IsShown = true;
        if (eventEmitter != null)
        {
            eventEmitter.Play();
        }
        backer.Hide();
        fader.alpha = 0.0f;
        background.alpha = 0f;

        if (!hideBackers)
        {
            backerArea.gameObject.SetActive(true);
            StartCoroutine(CoUtils.RunTween(background.DOFade(1, bgTime)));
        }
        else
        {
            backerArea.gameObject.SetActive(false);
        }
        yield return backer.ShowRoutine();
        text.Clear();
        Wipe();
    }

    public IEnumerator HideRoutine(bool preserveHighlight = false)
    {
        if (eventEmitter != null)
        {
            eventEmitter.Stop();
        }
        var routines = new List<IEnumerator>();

        var highlighted = GetHighlightedPortrait();
        if (highlighted != null && !preserveHighlight)
        {
            routines.Add(highlighted.UnhighlightRoutine());
        }

        yield return CoUtils.RunParallel(routines.ToArray(), this);
        routines.Clear();
        routines.Add(backer.HideRoutine());
        routines.Add(CoUtils.RunTween(fader.DOFade(0.0f, backer.duration)));
        routines.Add(CoUtils.RunTween(background.DOFade(0, bgTime)));
        yield return CoUtils.RunParallel(routines.ToArray(), this);
        Wipe();
        IsShown = false;
    }

    public IEnumerator EnterRoutine(SpeakerData speaker, bool useAnim)
    {
        var portrait = GetPortrait(speaker);
        yield return portrait.EnterRoutine(useAnim);
    }

    public IEnumerator ExitRoutine(SpeakerData speaker, bool useAnim)
    {
        var portrait = GetPortrait(speaker);
        yield return portrait.ExitRoutine(useAnim);
    }

    public IEnumerator SpeakRoutine(SpeakerData speaker, string message, bool useAnims, bool useHighlight)
    {
        Wipe();
        var name = speaker.displayName;

        if (!IsShown && useAnims)
        {
            yield return ShowRoutine(hideBackers: true);
        }

        if (speaker != null && useHighlight)
        {
            yield return SetHighlightRoutine(speaker);
        }

        string toType = message;
        nameText.text = name;
        //yield return text.WriteLineRoutine(toType);
        if (useAnims)
        {
            GetPortrait(speaker).StartTalking();
        }
        yield return text.TypeRoutine(toType, waitForConfirm: false);
        advanceIcon.TurnOn();
        yield return null;
        yield return InputManager.Instance.ConfirmRoutine(eatsOthers: false);
        if (useAnims)
        {
            GetPortrait(speaker).StopTalking();
        }
        advanceIcon.TurnOff();
        //Global.Instance.Audio.PlaySFX("in_game/popups", null, AudioManager.Bank.UI);
    }

    public IEnumerator SetHighlightRoutine(SpeakerData speaker)
    {
        var portrait = GetPortrait(speaker);
        var routines = new List<IEnumerator>();
        if (portrait != null)
        {
            routines.Add(portrait.HighlightRoutine());
        }
        foreach (var other in GetPortraits())
        {
            if (other.Speaker != null && other.Speaker != speaker && other.IsHighlighted)
            {
                routines.Add(other.UnhighlightRoutine());
            }
        }
        yield return CoUtils.RunParallel(routines.ToArray(), this);
    }

    public PortraitComponent GetHighlightedPortrait()
    {
        return GetPortraits().Where(p => p.IsHighlighted).FirstOrDefault();
    }

    private PortraitComponent GetPortrait(string slot)
    {
        PortraitComponent portrait = null;
        switch (slot.ToLower())
        {
            case "a": portrait = slotA; break;
            case "b": portrait = slotB; break;
            case "c": portrait = slotC; break;
            case "d": portrait = slotD; break;
        }
        return portrait;
    }

    public PortraitComponent GetPortrait(SpeakerData speaker)
    {
        if (slotA.Speaker == speaker) return slotA;
        if (slotB.Speaker == speaker) return slotB;
        if (slotC.Speaker == speaker) return slotC;
        if (slotD.Speaker == speaker) return slotD;
        return null;
    }

    private List<PortraitComponent> GetPortraits()
    {
        return new List<PortraitComponent>() {
            slotA,
            slotB,
            slotC,
            slotD,
        };
    }
}
