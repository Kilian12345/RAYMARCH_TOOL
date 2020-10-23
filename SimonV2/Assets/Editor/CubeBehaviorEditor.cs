using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeBehavior))]
public class CubeBehaviorEditor : Editor
{
    CubeBehavior cube;
    Transform cubeTransform;

    public void OnEnable()
    {
        cube = target as CubeBehavior;
        cubeTransform = (target as CubeBehavior).transform;
    }

    public void OnDisable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Ceci s'affiche dans l'insector");
    }

    public void OnSceneGUI()
    {


        Handles.BeginGUI();
        EditorGUILayout.LabelField("Ceci s'affiche dans la scene");
        Handles.EndGUI();


        Vector3 pos = cubeTransform.position;
        Quaternion rot = cubeTransform.rotation;
        float size = 0.2f;
        Vector3 snap = Vector3.zero;



        for (int i = 0; i < cube.outputVector.Length; i++)
        {
            Handles.DrawLine(pos + cube.outputVector[i],
                pos + cube.outputVector[(i + 1) % cube.outputVector.Length]);

            Handles.color = Color.blue;

            cube.outputVector[i] = Handles.FreeMoveHandle(
                pos + cube.outputVector[i],
                rot,
                size * HandleUtility.GetHandleSize(pos + cube.outputVector[i]),
                snap,
                Handles.SphereHandleCap) - pos;

            cube.outputQuaternion[i] = Handles.RotationHandle(
                cube.outputQuaternion[i],
                pos + cube.outputVector[i]);
                
        }


        //cubeTransform.position = Handles.FreeMoveHandle(pos, rot, size, snap, Handles.CubeHandleCap);



        EditorUtility.SetDirty(cubeTransform);
        EditorUtility.SetDirty(cube);
    }
}
