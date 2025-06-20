﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class TextAutotyper : MonoBehaviour, IInputListener
{
    [SerializeField] public Text textbox;
    [SerializeField] public float charsPerSecond = 120f;
    [SerializeField] protected GameObject advanceArrow;

    public bool mode2 = false;

    public int LinesTyped { get; private set; } = 0;

    protected int typingStartIndex = 0;
    protected bool hurried;
    protected bool confirmed;

    public virtual void Clear()
    {
        textbox.text = "";
    }

    public bool OnCommand(InputManager.Command command, InputManager.Event eventType)
    {
        switch (eventType)
        {
            case InputManager.Event.Hold:
                if (command == InputManager.Command.Primary || command == InputManager.Command.Click)
                {
                    //hurried = true;
                }
                break;
            case InputManager.Event.Up:
                if (command == InputManager.Command.Primary || command == InputManager.Command.Click)
                {
                    hurried = true;
                    confirmed = true;
                }
                break;
        }
        return true;
    }

    //public void StartGlitch()
    //{
    //    StartCoroutine(MapOverlayUI.Instance.adv.GetHighlightedPortrait().JoltRoutine());
    //    StartCoroutine(AudioManager.Instance.JumpscareRoutine());
    //}

    public IEnumerator TypeRoutine(string text, bool waitForConfirm = true)
    {
        Global.Instance.Input.PushListener(this);

        hurried = false;
        confirmed = false;
        float elapsed = 0.0f;
        float total = (text.Length - typingStartIndex) / charsPerSecond;
        textbox.GetComponent<CanvasGroup>().alpha = 1.0f;

        var containsGlitch = text.IndexOf("____") >= 0;
        var glitchesDelayed = 0;
        var glitchStartedAt = -1;

        while (elapsed <= total)
        {
            elapsed += Time.deltaTime;
            int charsToShow = Mathf.FloorToInt(elapsed * charsPerSecond) + typingStartIndex;
            int cutoff = charsToShow > text.Length ? text.Length : charsToShow;
            textbox.text = text.Substring(0, cutoff);

            var uCount = 0;
            foreach (var c in textbox.text)
            {
                if (c == '_')
                {
                    uCount += 1;
                }
            }
            if (glitchesDelayed < uCount)
            {
                var tryg = textbox.text.Length;
                while (tryg > 0 && textbox.text[tryg - 1] == '_')
                {
                    tryg -= 1;
                }
                if (tryg > glitchStartedAt)
                {
                    glitchStartedAt = tryg;
                    //StartGlitch();
                }
                yield return CoUtils.Wait(.175f);
                glitchesDelayed = uCount;
            }

            if (!mode2)
            {
                textbox.text += "<color=#aa000000>";
                textbox.text += text.Substring(cutoff);
                textbox.text += "</color>";
            }
            yield return null;

            elapsed += Time.deltaTime;
            if (hurried)
            {
                hurried = false;
                elapsed += 10000;
            }
        }
        textbox.text = text;

        if (waitForConfirm)
        {
            confirmed = false;
            if (advanceArrow != null) advanceArrow.SetActive(true);
            while (!confirmed)
            {
                yield return null;
            }
            if (advanceArrow != null) advanceArrow.SetActive(false);
        }

        Global.Instance.Input.RemoveListener(this);
    }
}
