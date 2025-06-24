using UnityEngine;

public class DetachOnSpawnBehavior : MonoBehaviour
{
    [SerializeField] private string triggerSwitch;

    private bool spawned;

    public void OnEnable()
    {
        Global.Instance.Data.onSwitchChanged += OnSwitchChanged;
    }

    public void OnDisable()
    {
        Global.Instance.Data.onSwitchChanged -= OnSwitchChanged;
    }

    private void OnSwitchChanged(string switchName, bool value)
    {
        if (switchName == triggerSwitch && value && !spawned)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        var map = Global.Instance.Maps.ActiveMap;
        transform.SetParent(map.transform, true);
        spawned = true;
        var position = transform.position;
        var height = map.terrain.GetHeightAt(position);
        var newPos = new Vector3(position.x, height, position.z);
        transform.position = newPos;
    }
}