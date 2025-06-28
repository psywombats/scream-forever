using UnityEngine;

public class WebComponent : MonoBehaviour
{
    public GameObject target;
        
    public void Start()
    {
        target.SetActive(Application.platform == RuntimePlatform.WebGLPlayer);
    }
}