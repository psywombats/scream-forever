using UnityEngine;

public class DetachOnSpawnBehavior : MonoBehaviour
{
    [SerializeField] protected string triggerSwitch;
    [SerializeField] protected GameObject disableChild;
    [SerializeField] protected bool playerX;

    private bool spawned;
    private Vector3 offset;

    public virtual void Start()
    {
        disableChild.gameObject.SetActive(false);
        offset = transform.position - Global.Instance.Avatar.transform.position;
    }

    public void OnEnable()
    {
        Global.Instance.Data.onSwitchChanged += OnSwitchChanged;
    }

    public void OnDisable()
    {
        Global.Instance.Data.onSwitchChanged -= OnSwitchChanged;
    }

    protected virtual void OnSwitchChanged(string switchName, bool value)
    {
        if (switchName == triggerSwitch && value && !spawned)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        disableChild.gameObject.SetActive(true);
        spawned = true;
        
        var map = Global.Instance.Maps.ActiveMap;
        var avAt = Global.Instance.Avatar.transform.position;

        var position = new Vector3(
            playerX ? avAt.x + offset.x : transform.position.x, 
            0, 
            avAt.z + offset.z);
        var height = map.terrain.GetHeightAt(position);
        transform.position = new Vector3(position.x, height, position.z);
    }
}