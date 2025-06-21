using System;
using UnityEngine;

public abstract class Chunk : MonoBehaviour
{
    [SerializeField] private MeshFilter filter;
    [SerializeField] private new MeshCollider collider;

    protected Vector3 pos;
    protected float[] weights;
    
    private int triIndex;
    private Triangle[] tris = new Triangle[256];
    private float[] squareValues = new float[4];

    public MarchingTerrain Terrain { get; private set; }
    public Vector3Int Index { get; private set; }

    public void Init(MarchingTerrain terrain, Vector3Int index, Vector3 pos)
    {
        Index = index;
        Terrain = terrain;
        this.pos = pos;
    }

    public void ConstructMesh()
    {
        weights = Terrain.Weight.GenerateNoise(pos);
        UpdateMesh();
    }

    private void OnDestroy()
    {
        if (filter.sharedMesh != null)
        {
            Destroy(filter.sharedMesh);
        }
    }

    private void UpdateMesh()
    {
        var mesh = ConstructMesh(weights);
        filter.sharedMesh = mesh;
        collider.sharedMesh = mesh;
    }

    private Mesh ConstructMesh(float[] weights)
    {
        var iso = Terrain.Weight.isoLevel;
        triIndex = 0;
        for (var x = 0; x < GridMetrics.PointsPerChunk - 1; x += 1)
        {
            for (var z = 0; z < GridMetrics.PointsPerChunk - 1; z += 1)
            {
                var id = new Vector3(x, GridMetrics.PointsPerChunk / 2f - .5f, z);

                squareValues[0] = weights[(x + 0) + GridMetrics.PointsPerChunk * (z + 0)];
                squareValues[1] = weights[(x + 1) + GridMetrics.PointsPerChunk * (z + 0)];
                squareValues[2] = weights[(x + 0) + GridMetrics.PointsPerChunk * (z + 1)];
                squareValues[3] = weights[(x + 1) + GridMetrics.PointsPerChunk * (z + 1)];

                Triangle tri1;
                tri1.a = id + new Vector3(0, squareValues[0], 0);
                tri1.b = id + new Vector3(1, squareValues[3], 1);
                tri1.c = id + new Vector3(1, squareValues[1], 0);
                AddTri(tri1);
                
                Triangle tri2;
                tri2.a = id + new Vector3(0, squareValues[0], 0);
                tri2.b = id + new Vector3(0, squareValues[2], 1);
                tri2.c = id + new Vector3(1, squareValues[3], 1);
                AddTri(tri2);
            }
        }
        return CreateMeshFromTriangles(tris, triIndex);
    }

    private void AddTri(Triangle tri)
    {
        tris[triIndex] = tri;
        triIndex += 1;
        if (triIndex >= tris.Length)
        {
            Array.Resize(ref tris, tris.Length * 2);
        }
    }

    private Mesh CreateMeshFromTriangles(Triangle[] triangles, int triLength)
    {
        var verts = new Vector3[triLength * 3];
        var tris = new int[triLength * 3];

        for (var i = 0; i < triLength; i++)
        {
            var startIndex = i * 3;
            verts[startIndex + 0] = triangles[i].a;
            verts[startIndex + 1] = triangles[i].b;
            verts[startIndex + 2] = triangles[i].c;
            tris[startIndex + 0] = startIndex;
            tris[startIndex + 1] = startIndex + 1;
            tris[startIndex + 2] = startIndex + 2;
        }

        var mesh = new Mesh
        {
            vertices = verts,
            triangles = tris
        };
        mesh.RecalculateNormals();
        return mesh;
    }

    private struct Triangle
    {
        public Vector3 a;
        public Vector3 b;
        public Vector3 c;

        public static int SizeOf => sizeof(float) * 3 * 3;
    }
}
