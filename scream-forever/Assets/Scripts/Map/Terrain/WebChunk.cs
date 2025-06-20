using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WebChunk : Chunk
{
    private int triIndex;
    private Triangle[] tris = new Triangle[256];
    private float[] cubeValues = new float[8];

    public override void AdjustWeights(Vector3 hit, float r, float mult, bool alwaysApply = false)
    {
        for (var x = 0; x < GridMetrics.PointsPerChunk - 1; x += 1)
        {
            for (var y = 0; y < GridMetrics.PointsPerChunk - 1; y += 1)
            {
                for (var z = 0; z < GridMetrics.PointsPerChunk - 1; z += 1)
                {
                    var id = new Vector3Int(x, y, z);
                    if (Vector3.SqrMagnitude(id - hit) <= r * r)
                    {
                        weights[id.x + GridMetrics.PointsPerChunk * (id.y + GridMetrics.PointsPerChunk * id.z)] += mult;
                    }
                }
            }
        }
    }

    protected override Mesh ConstructMesh(float[] weights)
    {
        var iso = Terrain.Noise.isoLevel;
        triIndex = 0;
            // TODO
        return CreateMeshFromTriangles(tris, triIndex);
    }
}
