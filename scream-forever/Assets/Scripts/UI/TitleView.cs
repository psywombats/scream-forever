using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleFader;
    [SerializeField] private GameObject toNuke;
    [Space]
    [SerializeField] private List<string> orderedMapNames;
    [SerializeField] private List<Button> buttons;

    private bool selected = false;
    
    public void StartGame()
    {
        Global.Instance.StartCoroutine(StartGameRoutine(orderedMapNames[0]));
    }

    private void Continue(int bookmark)
    {
        Global.Instance.StartCoroutine(StartGameRoutine(orderedMapNames[bookmark]));
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void ContinueOne() => Continue(1);
    public void ContinueTwo() => Continue(2);
    public void ContinueThree() => Continue(3);
    public void ContinueFour() => Continue(4);
    public void ContinueFive() => Continue(5);
    public void ContinueSix() => Continue(6);

    private IEnumerator StartGameRoutine(string mapName)
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
        if (selected) yield break;
        selected = true;
        StartCoroutine(AudioManager.Instance.FadeOutRoutine(2.5f));
        yield return CoUtils.RunTween(titleFader.DOFade(0f, 2.5f));
        //Destroy(toNuke);
        yield return Global.Instance.Maps.TeleportRoutine(mapName);
    }
}
