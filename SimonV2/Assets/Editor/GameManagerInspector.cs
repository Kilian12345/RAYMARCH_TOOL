using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GameManager))]
[CanEditMultipleObjects] // Used to select multiple objects and edit their same component variables
public class GameManagerInspector : Editor
{
    GameManager manager;

    public void OnEnable()
    {
        manager = target as GameManager;

        Undo.undoRedoPerformed += AfterUndo;
    }

    public void AfterUndo() {}

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField(EditorGUIUtility.labelWidth.ToString());
        //EditorGUILayout.LabelField("WIDTH");

        int oldIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel += 5;
        manager.myColor = EditorGUILayout.ColorField("My Color", manager.myColor);
        EditorGUI.indentLevel = oldIndent;

        string[] options = new string[] { "Option 1", "Option 2", "Option 3" };
        manager.myWrapMode = (WrapMode)EditorGUILayout.Popup((int)manager.myWrapMode, options);

        EditorGUILayout.HelpBox("This is a help box, read what it says or stay blind", MessageType.Info);

        EditorGUI.BeginChangeCheck();
        Transform transformResult = EditorGUILayout.ObjectField("My Transform", manager.myTransform, typeof(Transform), true) as Transform;
        bool userChanged = EditorGUI.EndChangeCheck();
        if (userChanged) // User changed something
        {
            Debug.Log("something changed");
            Undo.RecordObject(manager, "Reset transform");
            manager.myTransform = transformResult;
        }

        #region Buttons

        Color baseColor = GUI.color;
        GUI.color = Color.green;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Auto Set References"))
        {
            AutoSetReferences();
        }
        if (GUILayout.Button("Auto Clear References"))
        {
            AutoClearReferences();
        }
        if (GUILayout.Button("Test"))
        {
            AutoClearReferences();
        }
        GUILayout.EndHorizontal();

        GUI.color = baseColor;

        #endregion

        manager.foldoutState = EditorGUILayout.Foldout(manager.foldoutState, "Fold Here", true);
        if (manager.foldoutState)
        {
            EditorGUILayout.LabelField("Hello guys");
        }

        GUILayout.EndVertical();

        EditorUtility.SetDirty(manager);
        //EditorSceneManager.MarkAllScenesDirty();
    }

    void AutoSetReferences()
    {
        Undo.RecordObject(manager, "Set references");

        manager.audioListener = Object.FindObjectOfType<AudioListener>();
        manager.camera = Object.FindObjectOfType<Camera>();
        manager.myTransform = manager.transform;
        manager.camTransform = manager.camera.transform;
    }

    void AutoClearReferences()
    {
        Undo.RecordObject(manager, "Clear references");

        manager.audioListener = null;
        manager.camera = null;
        manager.myTransform = null;
        manager.camTransform = null;
    }

    public void OnDisable()
    {
        Undo.undoRedoPerformed -= AfterUndo;
    }
}
