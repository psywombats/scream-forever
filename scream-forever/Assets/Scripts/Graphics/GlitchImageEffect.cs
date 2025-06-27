using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GlitchImageEffect : MonoBehaviour {

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private bool enableGlitch = true;

    public float Fade { get; set; } = 1f;
    private float elapsedSeconds;

    public void Update() 
    {
        AssignCommonShaderVariables();
        elapsedSeconds += Time.deltaTime;
    }

    public IEnumerator FadeInRoutine(float duration)
    {
        return CoUtils.RunTween(DOTween.To(() => Fade, v => Fade = v, 0f, duration));
    }
    public IEnumerator FadeOutRoutine(float duration)
    {
        return CoUtils.RunTween(DOTween.To(() => Fade, v => Fade = v, 1f, duration));
    }
    
    private void AssignCommonShaderVariables() 
    {
        mesh.material.SetFloat("_UniversalEnable", enableGlitch ? 1 : 0);
        mesh.material.SetFloat("_XFade", Fade);
    }
}
