using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FraktalWindowEditor))]
public class FraktalWindowEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Open Editor"))
            FraktalWindow.InitWithContent(target as FraktalWindowEditor);
    }
}
