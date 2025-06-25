using System.Collections;
using DG.Tweening;
using UnityEngine;

// this class sucks, it should be two individual button listeners
public class MouseChoiceSelector : MonoBehaviour, IInputListener
{
    [SerializeField] private CanvasGroup transparencyBase;
    [SerializeField] private RectTransform movementBase;
    [SerializeField] private MouseSelectionSlider leftSlider;
    [SerializeField] private MouseSelectionSlider rightSlider;
    [Space] 
    [SerializeField] private ChoiceBox leftChoice;
    [SerializeField] private ChoiceBox rightChoice;
    [Space]
    [SerializeField] private float selectTime = 1f;
    [SerializeField] private float smoothTarget = .2f;
    [SerializeField] private float transTime = .7f;
    [SerializeField] private float transOffset = -120f;

    private float weightLeft;
    private float weightRight;

    public bool? Result; // right is false

    public IEnumerator ChooseRoutine(string choice1, string choice2)
    {
        weightLeft = 0;
        weightRight = 0;
        leftSlider.SetValue(0f);
        rightSlider.SetValue(0f);
        
        yield return MapOverlayUI.Instance.nvl.HideRoutine(preserveHighlight: true);
        Result = null;
        yield return ShowRoutine(choice1, choice2);
        Global.Instance.Input.PushListener(this);
        var crashes = Global.Instance.Avatar.CrashCount;
        while (Result == null && Global.Instance.Avatar.CrashCount == crashes)
        {
            yield return null;
        }
        Global.Instance.Input.RemoveListener(this);
        yield return HideRoutine();
        yield return MapOverlayUI.Instance.nvl.ShowRoutine();
    }

    public IEnumerator ShowRoutine(string opt1, string opt2)
    {
        yield return CoUtils.RunParallel(this,
            CoUtils.RunTween(transparencyBase.DOFade(1f, transTime)),
            CoUtils.RunTween(movementBase.DOAnchorPosY(movementBase.anchoredPosition.y + transOffset, transTime)));
        yield return CoUtils.RunParallel(this,
            leftChoice.ShowRoutine(opt1),
            rightChoice.ShowRoutine(opt2));
    }

    public IEnumerator HideRoutine()
    {
        yield return CoUtils.RunParallel(this,
            leftChoice.HideRoutine(),
            rightChoice.HideRoutine(),
            CoUtils.RunTween(transparencyBase.DOFade(0f, transTime)),
            CoUtils.RunTween(movementBase.DOAnchorPosY(movementBase.anchoredPosition.y - transOffset, transTime)));
    }

    public bool OnCommand(InputManager.Command command, InputManager.Event eventType)
    {
        bool isSecondary;
        if (command == InputManager.Command.Click || command == InputManager.Command.Primary)
        {
            isSecondary = false;
        }
        else if (command == InputManager.Command.Secondary)
        {
            isSecondary = true;
        }
        else
        {
            return false;
        }

        if (eventType == InputManager.Event.Up)
        {
            var at = isSecondary ? weightRight : weightLeft;
            if (at < smoothTarget)
            {
                if (!isSecondary)
                {
                    leftSlider.SetValue(smoothTarget);
                }
                else
                {
                    rightSlider.SetValue(smoothTarget);
                }
            }

            if (!isSecondary)
            {
                leftSlider.SmoothValue(0f);
                weightLeft = 0f;
            }
            else
            {
                rightSlider.SmoothValue(0f);
                weightRight = 0f;
            }
        }

        if (eventType == InputManager.Event.Hold)
        {
            var toAdd = Time.deltaTime / selectTime;
            if (!isSecondary)
            {
                weightLeft += toAdd;
                leftSlider.SetValue(weightLeft);
            }
            else
            {
                weightRight += toAdd;
                rightSlider.SetValue(weightRight);
            }
        }

        if (weightLeft > 1)
        {
            Result = true;
        }
        if (weightRight > 1)
        {
            Result = false;
        }

        return true;
    }
}