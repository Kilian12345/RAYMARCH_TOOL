using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestMenuFunctions
{
    [MenuItem("Tools/Basics/Find References %w")]
    public static void FindReferences()
    {
        Debug.Log("Initialize find references...");

        GameManager manager = Object.FindObjectOfType<GameManager>();

        Undo.RecordObject(manager, "Set references");

        manager.audioListener = Object.FindObjectOfType<AudioListener>();
        manager.camera = Object.FindObjectOfType<Camera>();
        manager.myTransform = manager.transform;
        manager.camTransform = manager.camera.transform;
    }
}
