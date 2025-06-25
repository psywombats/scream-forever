using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PamphletViewer : MonoBehaviour, IInputListener
{
    [Serializable]
    public struct PamphletData
    {
        public string tag;
        public Sprite leaf1, leaf2, leaf3;
    }
    
    [SerializeField] private Vector3 fromPos;
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform translate;
    [SerializeField] private float transDuration = .5f;
    [SerializeField] private float raiseDuration = .3f;
    [SerializeField] private float upJump = .1f;
    [Space] 
    [SerializeField] private SpriteRenderer leaf1;
    [SerializeField] private SpriteRenderer leaf2;
    [SerializeField] private SpriteRenderer leaf3;
    [Space]
    [SerializeField] private List<PamphletData> pamphlets;

    private bool upThisFrame;
    private float up;
    private Vector3 showPos;
    private bool finished;

    public void OnEnable()
    {
        showPos = translate.transform.localPosition;
        translate.transform.localPosition = fromPos;
    }

    public IEnumerator ViewPamphletRoutine(string pamphletTag)
    {
        yield return ShowRoutine(pamphletTag);
        var crashes = Global.Instance.Avatar.CrashCount;
        while (crashes == Global.Instance.Avatar.CrashCount && !finished)
        {
            if (!finished)
            {
                StartCoroutine(HideRoutine());
            }
            yield return null;
        }
    }

    public IEnumerator ShowRoutine(string pamphletTag)
    {
        finished = false;
        foreach (var pamphlet in pamphlets)
        {
            if (pamphlet.tag == pamphletTag)
            {
                leaf1.sprite = pamphlet.leaf1;
                leaf2.sprite = pamphlet.leaf2;
                leaf3.sprite = pamphlet.leaf3;
                break;
            }
        }
        translate.transform.localPosition = fromPos;
        Global.Instance.Input.PushListener(this);
        return CoUtils.RunTween(translate.transform.DOLocalMove(showPos, transDuration).Play());
    }    
    
    public IEnumerator HideRoutine()
    {
        Global.Instance.Input.RemoveListener(this);
        yield return CoUtils.RunTween(translate.transform.DOLocalMove(fromPos, transDuration).Play());
        finished = true;
    }

    public void Update()
    {
        if (upThisFrame)
        {
            up += Time.deltaTime / raiseDuration * (1 - up);
            upThisFrame = false;
        }
        else
        {
            up -= Time.deltaTime / raiseDuration;
            if (up < 0) up = 0;
        }

        var t = (1 - up);
        pivot.localRotation = Quaternion.Euler(new Vector3(t * 85 + 5, 0, 0));
    }

    public bool OnCommand(InputManager.Command command, InputManager.Event eventType)
    {
        switch (command)
        {
            case InputManager.Command.Click:
            case InputManager.Command.Primary:
                if (eventType == InputManager.Event.Hold)
                {
                    upThisFrame = true;
                }
                else if (eventType == InputManager.Event.Up && up > 0 && up < upJump)
                {
                    up = upJump;
                }
                return true;
            case InputManager.Command.Secondary:
            case InputManager.Command.Menu:
                if (eventType == InputManager.Event.Down || eventType == InputManager.Event.Hold)
                {
                    StartCoroutine(HideRoutine());
                }
                return true;
        }

        return false;
    }
}