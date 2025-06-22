using System.Collections.Generic;
using UnityEngine;

public class MarchingTerrain : MonoBehaviour
{
    [SerializeField] public WeightGenerator weight;
    [SerializeField] public Chunk chunkPrefab;
    [Space]
    [SerializeField] private Vector3Int spawnRadius = Vector3Int.one;
    [SerializeField] private int cullDist = 2;
    [Space]
    [SerializeField] private GameObject toFollow;
    [SerializeField] private Vector3Int bias;

    private readonly Dictionary<Vector3Int, Chunk> chunks = new();
    private readonly Dictionary<int, GameObject> layers = new();

    public WeightGenerator Weight => weight;

    public GameObject Target => toFollow != null ? toFollow : (Global.Instance.Avatar == null ? null : Global.Instance.Avatar.gameObject);

    public void Update()
    {
        if (Target != null)
        {
            CullChunks();
            EnsureChunks(spawnRadius, usePlayer: true, allowInterrupts: true);
        }
    }

    /// <returns>True if everything was already built</returns>
    public bool EnsureChunks(Vector3Int radius, bool usePlayer = false, bool allowInterrupts = false)
    {
        var index = usePlayer ? GetPlayerIndex() : Vector3Int.zero;
        if (radius == Vector3Int.zero)
        {
            //radius = spawnRadius;
        }
        for (var x = index.x - radius.x; x <= index.x + radius.x; x += 1)
        {
            for (var y = index.y - radius.y; y <= index.y + radius.y; y += 1)
            {
                for (var z = index.z - radius.z; z <= index.z + radius.z; z += 1)
                {
                    var checkIndex = new Vector3Int(x, y, z);
                    if (EnsureChunk(checkIndex) && !allowInterrupts)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void CullAll()
    {
        foreach (var chunk in chunks)
        {
            DestroyImmediate(chunk.Value);
        }
        foreach (var layer in layers)
        {
            DestroyImmediate(layer.Value);
        }
        chunks.Clear();
        layers.Clear();
    }

    public float GetHeightAt(Vector3 point)
    {
        var index = GetIndexForPos(point);
        EnsureChunk(index);
        return chunks[index].GetWeightAt(point);
    }

    private void EnsureLayer(int layerIndex)
    {
        if (!layers.ContainsKey(layerIndex))
        {
            var layer = new GameObject();
            layer.name = "Level " + layerIndex;
            layers[layerIndex] = layer;
            layer.transform.SetParent(transform);
            layer.transform.position = Vector3.zero;
        }
    }

    private List<Vector3Int> toCull = new();
    private void CullChunks()
    {
        var index = GetPlayerIndex();
        
        foreach (var entry in chunks)
        {
            if (Mathf.Abs(index.x - entry.Key.x) + Mathf.Abs(index.y - entry.Key.y) + Mathf.Abs(index.z - entry.Key.z) > cullDist * 3)
            {
                toCull.Add(entry.Key);
            }
        }

        foreach (var c in toCull)
        {
            var chunk = chunks[c];
            Destroy(chunk.gameObject);
            chunks.Remove(c);
        }
        toCull.Clear();
    }

    private Vector3Int GetPlayerIndex()
    {
        var playerPos = Target.transform.position;
        var myPos = transform.position;
        var atPos = playerPos - myPos;
        var index = GetIndexForPos(atPos);
        index += bias;
        return index;
    }

    /// <param name="checkIndex">True if the chunk already exists</param>
    private bool EnsureChunk(Vector3Int checkIndex)
    {
        if (!chunks.ContainsKey(checkIndex))
        {
            var chunk = MakeChunk(checkIndex);
            chunk.ConstructMesh();
            return false;
        }
        return true;
    }

    private Vector3Int GetIndexForPos(Vector3 pos)
    {
        var off = GridMetrics.PointsPerChunk / 2f;
        var adjPos = pos - new Vector3(off, off, off);
        var atPos = new Vector3Int(
            Mathf.CeilToInt(adjPos.x / GridMetrics.ChunkSize),
            Mathf.CeilToInt(adjPos.y / GridMetrics.ChunkSize),
            Mathf.CeilToInt(adjPos.z / GridMetrics.ChunkSize));
        return atPos;
    }

    private Chunk MakeChunk(Vector3Int chunkIndex)
    {
        var chunk = Instantiate(chunkPrefab).GetComponent<Chunk>();
        var pos = (Vector3)(chunkIndex * GridMetrics.ChunkSize);
        var off = GridMetrics.ChunkSize / 2f;
        chunk.Init(this, chunkIndex, pos);
        chunks[chunkIndex] = chunk;

        chunk.gameObject.SetActive(true);
        chunk.gameObject.name = $"{chunkIndex.x}, {chunkIndex.z}";

        EnsureLayer(chunkIndex.y);
        chunk.transform.SetParent(layers[chunkIndex.y].transform);
        chunk.gameObject.transform.position = pos - new Vector3(off, off, off);
        return chunk;
    }
}
