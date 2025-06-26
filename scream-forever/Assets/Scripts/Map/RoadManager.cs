using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private RoadNode firstNode;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject toTrack;
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
        if (Global.Instance.Avatar == null && toTrack == null)
        {
            return;
        }

        var myTrack = Global.Instance.Avatar != null ? Global.Instance.Avatar.gameObject : toTrack;
        if (Vector3.Distance(myTrack.transform.position, HeadNode.transform.position) < distBetweenNodes)
        {
            SpawnNextNode();
        }
        if (activeNodes.Count < maxNodes)
        {
            SpawnNextNode();
        }

        if (Global.Instance.Avatar.transform.position.z > 2500)
        {
            var toTransform = new List<Transform>();
            toTransform.AddRange( activeNodes.Select(node => node.transform));
            //toTransform.Add( Global.Instance.Maps.ActiveMap.terrain.transform ); //buggy
            foreach (Transform trans in Global.Instance.Maps.ActiveMap.eventLayer.transform)
            {
                toTransform.Add( trans );
            }
            
            foreach (var trans in toTransform)
            {
                var position = trans.position;
                trans.position = new Vector3(
                    position.x,
                    position.y,
                    position.z - 2500);
            }
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
            activeNodes.Remove(toDelete);
            Destroy(toDelete.gameObject);
        }
    }
}