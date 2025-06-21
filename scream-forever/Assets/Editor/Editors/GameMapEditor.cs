using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameMap), editorForChildClasses: true)]
public class GameMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var cave = (GameMap)target;

        if (GUILayout.Button("Regenerate"))
        {
            cave.Regenerate(new Vector3Int(1, 0, 1));
        }
        if (GUILayout.Button("Destroy"))
        {
            cave.DestroyChunks();
        }
    }

    private IEnumerator DebugRoutine(NVLComponent nvl)
    {
        yield return nvl.ShowRoutine();
    }
}
