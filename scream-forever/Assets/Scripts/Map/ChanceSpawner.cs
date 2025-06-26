using System;
using UnityEngine;

public class ChanceSpawner : MonoBehaviour
{
    [SerializeField] public float chance;
    [SerializeField] public GameObject prefab;
    [SerializeField] public Transform anchor;

    public void OnEnable()
    {
        if (UnityEngine.Random.Range(0, 1f) < chance)
        {
            var instance = Instantiate(prefab, anchor, false);
            instance.transform.localPosition = Vector3.zero;
            var map = Global.Instance.Maps.ActiveMap;
            var position = instance.transform.position;
            var height = map.terrain.GetHeightAt(position);
            instance.transform.position = new Vector3(position.x, height, position.z);
        }
    }
}