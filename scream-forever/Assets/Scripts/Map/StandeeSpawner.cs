using UnityEngine;

public class StandeeSpawner : DetachOnSpawnBehavior
{
    [SerializeField] private string speakerTag;
    
    private StandeeComponent standee;
    private StandeeComponent Standee => standee ??= disableChild.GetComponent<StandeeComponent>();

    public override void Start()
    {
        base.Start();
        if (!string.IsNullOrEmpty(speakerTag))
        {
            Standee.AssignBySpeaker(speakerTag);
        }
    }

    protected override void OnSwitchChanged(string switchName, bool value)
    {
        base.OnSwitchChanged(switchName, value);
        if (switchName == triggerSwitch && !value)
        {
            StartCoroutine(Standee.ExitRoutine());
        }
    }
}
