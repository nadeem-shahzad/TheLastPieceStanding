
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();   
        // Draw the default inspector fields

        LevelEditor levelEditor = (LevelEditor)target;

        if (GUILayout.Button("Spawn Piece"))
        {
            levelEditor.SpawnPiece();
        }

        if (GUILayout.Button("Show JSON"))
        {
            levelEditor.CreateLevelData();
        }
    }
}
