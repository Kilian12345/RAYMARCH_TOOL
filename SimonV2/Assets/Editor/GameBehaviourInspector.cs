using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameBehaviour))]
public class GameBehaviourInspector : Editor
{
    GameBehaviour behaviour;
    SerializedProperty myColorProperty;
    SerializedProperty redLevel;
    SerializedProperty myStrings;
    SerializedProperty myEnemyProfile;

    public void OnEnable()
    {
        behaviour = target as GameBehaviour;

        myColorProperty = serializedObject.FindProperty(nameof(behaviour.myColor));
        redLevel = myColorProperty.FindPropertyRelative("r");

        myStrings = serializedObject.FindProperty(nameof(behaviour.myStrings));
        myEnemyProfile = serializedObject.FindProperty(nameof(behaviour.myEnemyProfile));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
    
        // Stuff

        if (GUILayout.Button("Turn Red")) myColorProperty.colorValue = Color.red;
        if (GUILayout.Button("Turn Green")) myColorProperty.colorValue = Color.green;
        EditorGUILayout.PropertyField(myColorProperty);

        EditorGUILayout.LabelField(redLevel.floatValue.ToString());

        EditorGUI.BeginChangeCheck();
        float tempRedLevel = EditorGUILayout.Slider("Red", redLevel.floatValue, 0f, 1f);
        if (EditorGUI.EndChangeCheck()) redLevel.floatValue = tempRedLevel;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add one")) myStrings.arraySize++;
        if (GUILayout.Button("Remove one") && myStrings.arraySize > 0) myStrings.arraySize--;
        EditorGUILayout.EndHorizontal();
        if (myStrings.arraySize > 0)
        {
            for (int i = 0; i < myStrings.arraySize; i++)
            {
                EditorGUILayout.PropertyField(myStrings.GetArrayElementAtIndex(i), new GUIContent("Text Field", "This is a really nice tool tip"));
            }
        }

        EditorGUILayout.PropertyField(myEnemyProfile);

        serializedObject.ApplyModifiedProperties();
    }
}
