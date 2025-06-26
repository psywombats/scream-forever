using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{
    [SerializeField] protected string triggerSwitch;
    [SerializeField] protected List<GameObject> toEnable;
    [SerializeField] protected List<GameObject> toDisable;

    private bool spawned;
    private Vector3 offset;
    
    public void OnEnable()
    {
        Global.Instance.Data.onSwitchChanged += OnSwitchChanged;
        UpdateState();
    }

    public void OnDisable()
    {
        Global.Instance.Data.onSwitchChanged -= OnSwitchChanged;
    }

    private void UpdateState()
    {
        var isSwitchOn = Global.Instance.Data.GetSwitch(triggerSwitch);
        foreach (var obj in toEnable)
        {
            obj.SetActive(isSwitchOn);
        }
        foreach (var obj in toDisable)
        {
            obj.SetActive(!isSwitchOn);
        }
    }

    private void OnSwitchChanged(string switchName, bool value)
    {
        UpdateState();
    }
}