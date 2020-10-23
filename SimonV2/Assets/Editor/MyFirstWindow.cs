using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Net.Security;
using System.IO;

public class MyFirstWindow : EditorWindow
{
    LevelProfile currentProfile;



    [MenuItem("Window/MyFirstWindow %&#w")]
    public static void Init()
    {
        MyFirstWindow window = EditorWindow.GetWindow(typeof(MyFirstWindow)) as MyFirstWindow;

        // initialize window

        window.Show();
    }

    public static void InitWithContent(LevelProfile profile)
    {
        MyFirstWindow window = EditorWindow.GetWindow(typeof(MyFirstWindow)) as MyFirstWindow;

        window.currentProfile = profile;

        window.Show();
    }

    public void OnGUI()
    {

        float tileWidth = 50f;
        float tileHeight = 50f;



        if (currentProfile == null)
        {
            EditorGUILayout.LabelField("currentProfile displayed is null");
            return;
        }

        if( currentProfile.levelValues.Length > 0)
        {

            Event currentEvent = Event.current;

            for(int i = 0; i< currentProfile.levelValues.Length; i++)
            {
                Rect square = new Rect(30 + tileWidth * i, 30 + tileHeight * i, tileWidth, tileHeight);
                EditorGUI.DrawRect(square, Color.green);

                EditorGUIUtility.AddCursorRect(square, MouseCursor.Pan);

                if(square.Contains(currentEvent.mousePosition))
                {
                    EditorGUI.DrawRect(square, Color.blue);
                }
                else
                {
                    EditorGUI.DrawRect(square, Color.green);
                }


            }
            Rect closeButton = new Rect(tileWidth * 0.1f, tileHeight * 0.2f, tileWidth * 2.6f, tileHeight * 2.3f);
            if (GUI.Button(closeButton, "Export as JSON"))
            {
                string curveAsJSON = JsonUtility.ToJson(currentProfile, true);
                string filePath = "Assets/Cuvre.json";
                File.WriteAllText(filePath, curveAsJSON);
            }

            Repaint();

        }




    }

    public void OnoldGUI()
    {
        EditorGUI.DrawRect(new Rect(30, 30, 100, 100), Color.green);

        Rect pos = this.position;

        Rect closeButton = new Rect(30, 200, 60, 20);
        //Rect closeButton = new Rect(pos.x*0.1f, pos.y * 0.2f, pos.width * 0.6f, pos.height * 0.3f);
        if (GUI.Button(closeButton, "Close"))
            this.Close();
    }
}
