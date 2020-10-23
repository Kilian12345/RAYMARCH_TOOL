using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TransformInspector))]
public class TrasnformInpectorEditor : Editor
{

    SerializedProperty myProperty, forkProperty;

    private void OnEnable()
    {
        myProperty = serializedObject.GetIterator();
        myProperty.NextVisible(true);
        Debug.Log(myProperty.name);
        Debug.Log(myProperty.displayName);
        Debug.Log(myProperty.type);

        while (myProperty.NextVisible(false))
        {
            Debug.Log(myProperty.name);
            Debug.Log(myProperty.displayName);
            Debug.Log(myProperty.type);

            if (myProperty.type == "Color")
                forkProperty = myProperty.Copy();


        }

        myProperty.Reset();
        Debug.Log(myProperty.name);
        Debug.Log(myProperty.displayName);
        Debug.Log(myProperty.type);

    }

    public override void OnInspectorGUI()
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        base.OnInspectorGUI();

        sw.Stop();
        EditorGUILayout.LabelField(sw.ElapsedTicks.ToString());
    }
}
