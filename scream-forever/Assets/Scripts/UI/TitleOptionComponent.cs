using UnityEngine;

public class TitleOptionComponent : MonoBehaviour
{
    [SerializeField] private SlowFlashBehavior cursor;
    [SerializeField] private bool startEnabled;

    public void Start()
    {
        cursor.Enabled = startEnabled;
    }

    public void SetSelected(bool selected)
    {
        cursor.Enabled = selected;
    }
}