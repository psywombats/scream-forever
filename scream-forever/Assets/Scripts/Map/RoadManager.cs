using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private RoadNode firstNode;
    [SerializeField] private GameObject nodePrefab;
    [Space]
    [SerializeField] private float distBetweenNodes = 48f;
    [SerializeField] private int maxNodes = 3;

    private List<RoadNode> activeNodes = new();

    public RoadNode HeadNode => activeNodes.Last();

    public void Start()
    {
        activeNodes.Add(firstNode);
    }

    public void Update()
    {
        if (Vector3.Distance(Global.Instance.Avatar.transform.position, HeadNode.transform.position) < distBetweenNodes)
        {
            SpawnNextNode();
        }
        if (activeNodes.Count < maxNodes)
        {
            SpawnNextNode();
        }
    }

    private void SpawnNextNode()
    {
        var nextPos = HeadNode.transform.position;
        nextPos += new Vector3(0, 0, distBetweenNodes);
        var nextNodeObj = Instantiate(nodePrefab, transform, true);
        nextNodeObj.transform.SetPositionAndRotation(nextPos, Quaternion.identity);
        var nextNode = nextNodeObj.GetComponent<RoadNode>();
        activeNodes.Add(nextNode);
        if (activeNodes.Count > maxNodes)
        {
            var toDelete = activeNodes.First();
            activeNodes.Remove(firstNode);
            Destroy(toDelete);
        }
    }
}