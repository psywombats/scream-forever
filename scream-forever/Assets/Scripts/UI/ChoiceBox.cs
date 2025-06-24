using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private ExpanderComponent expander;
    [SerializeField] private Text textbox;
    [SerializeField] private float transTime = .7f;

    public IEnumerator ShowRoutine(string option)
    {
        textbox.text = "";
        yield return CoUtils.RunParallel(this,
            expander.ShowRoutine(),
            CoUtils.RunTween(fader.DOFade(1f, transTime / 2f)));
        textbox.text = option;
        yield return textbox.DOFade(1f, transTime / 2f);
    }

    public IEnumerator HideRoutine()
    {
        yield return CoUtils.RunParallel(this,
            expander.HideRoutine(),
            CoUtils.RunTween(fader.DOFade(0f, transTime / 2f)),
            CoUtils.RunTween(textbox.DOFade(0f, transTime / 2f)));
    }
}