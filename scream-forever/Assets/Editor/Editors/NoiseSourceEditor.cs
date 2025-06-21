using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NoiseSource), editorForChildClasses: true)]
public class NoiseSourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var component = (NoiseSource)target;
        
        if (GUILayout.Button("Generate"))
        {
            component.GenerateNoise(new float[GridMetrics.PointsPerChunk * GridMetrics.PointsPerChunk]);
        }
    }
}