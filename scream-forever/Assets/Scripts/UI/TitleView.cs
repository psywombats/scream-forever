using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TitleView : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleFader;
    [Space]
    [SerializeField] private List<string> orderedMapNames;

    private bool selected = false;
    
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine(orderedMapNames[0]));
    }

    private void Continue(int bookmark)
    {
        StartCoroutine(StartGameRoutine(orderedMapNames[bookmark]));
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void ContinueOne() => Continue(1);
    public void ContinueTwo() => Continue(2);
    public void ContinueThree() => Continue(3);
    public void ContinueFour() => Continue(4);

    private IEnumerator StartGameRoutine(string mapName)
    {
        if (selected) yield break;
        selected = true;
        yield return CoUtils.RunTween(titleFader.DOFade(0f, 2.5f));
        yield return Global.Instance.Maps.TeleportRoutine(mapName);
    }
}
