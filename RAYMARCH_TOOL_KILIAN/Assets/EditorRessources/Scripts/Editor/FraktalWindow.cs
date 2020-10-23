using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Net.Security;
using System.IO;
using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

public class FraktalWindow : EditorWindow
{
    FraktalWindowEditor current;
    public Vector2 scrollPosition = Vector2.zero;
    private float mainWindowTotalHeight;
    private int listParamId;

    GUIStyle globalGuiStyle;





    /// /////////// RRECT
    Rect mainContainerRect;


    [MenuItem("Window/FraktalWindow %&#w")]
    public static void Init()
    {
        FraktalWindow window = EditorWindow.GetWindow(typeof(FraktalWindow)) as FraktalWindow;

        FraktalWindowEditor cur = FindObjectOfType<FraktalWindowEditor>();
        window.current = cur;

        window.Show();
    }

    public static void InitWithContent(FraktalWindowEditor cur)
    {
        FraktalWindow window = EditorWindow.GetWindow(typeof(FraktalWindow)) as FraktalWindow;

        window.current = cur;
        window.minSize = new Vector2(850, Screen.height);

        window.Show();
    }

    public void OnGUI()
    {
        mainWindowTotalHeight = position.height * 2;
        scrollPosition = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPosition, new Rect(0, 0, position.width, mainWindowTotalHeight), false, true);

        DisplayTitle();
        DisplayToolBar();
        DisplayLevelEdition();
        ToolBarButton();
        DisplayAllLevels();
        DeleteAllLevels();

        //GUI.Button(new Rect(80,1000, 100, 20), "Bottom-right");

        GUI.EndScrollView();


        Repaint();

    }

    void DisplayTitle()
    {
        float fontSize = 40;
        float labelContainerInputHeight = 50;

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = (int)Mathf.Clamp(6 * (position.width * 0.01f), 0, fontSize);
        guiStyle.font = current.windowTitleFont;
        guiStyle.normal.textColor = Color.white;
        guiStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUI.DrawRect(new Rect(0, 20, position.width, labelContainerInputHeight), current.windowTitleContainerColor);
        EditorGUI.LabelField(new Rect(position.width * 0.5f, 20, 0, 50), current.windowTitle, guiStyle);

        float textureWidth = Mathf.Clamp(3 * (position.width * 0.1f), 0, current.windowMainImage.width);
        float textureHeight = Mathf.Clamp(3 * (position.width * 0.1f), 0, current.windowMainImage.height);


        GUI.DrawTexture(new Rect(position.width * 0.5f - (textureWidth * 0.5f), 100, textureWidth, textureHeight), current.windowMainImage, ScaleMode.ScaleAndCrop, true);

    }

    void DisplayToolBar()
    {
        EditorGUI.DrawRect(new Rect(current.toolBarRect.x, GetRectYModification(current.toolBarRect.y), position.width, current.toolBarRect.height), current.windowTitleContainerColor);
    }

    void DisplayLevelEdition()
    {
        float scrollBarWidth = GUI.skin.verticalScrollbar.fixedWidth;
        mainContainerRect = new Rect(10, GetRectYModification(40), position.width - 10 - scrollBarWidth, mainWindowTotalHeight - 10 - scrollBarWidth);
        EditorGUI.DrawRect(mainContainerRect, current.windowMainContainerColor);
    }

    void ToolBarButton()
    {

        if (GUI.Button(new Rect(position.width / current.spawnRect.x, GetRectYModification(current.spawnRect.y), position.width / current.spawnRect.width, current.spawnRect.height), "ADD LEVEL"))
        {
            listParamId++;
            FractalLevels fp = FractalParameterSpawner.AddFractalLevel(listParamId);
            current.Levels.Add(fp);
        }

        if (GUI.Button(new Rect(position.width / current.helpUiALLRect.x, GetRectYModification(current.helpUiALLRect.y), position.width / current.helpUiALLRect.width, current.helpUiALLRect.height), "ALL HELP UI"))
        {

        }

        if (GUI.Button(new Rect(position.width / current.timerALLRect.x, GetRectYModification(current.timerALLRect.y), position.width / current.timerALLRect.width, current.timerALLRect.height), "ALL TIMER"))
        {

        }
    }

    #region DELETE

    void DeleteLevelButton(FractalLevels fp)
    {
        if (GUI.Button(new Rect(100, GetRectYModification(80), 100, 40), "Destroy"))
        {
            FractalParameterSpawner.DeleteFractalLevel(fp);
        }
    }

    void DeleteAllLevels()
    {
        if (GUI.Button(new Rect(position.width / current.deleteALL.x, GetRectYModification(current.deleteALL.y), position.width / current.deleteALL.width, current.deleteALL.height), "DELETE ALL LEVELS"))
        {
            for (int i = 0; i < current.Levels.Count; i++)
            {
                FractalParameterSpawner.DeleteFractalLevel(current.Levels[i]);
            }
            current.Levels.Clear();
        }
    }

    #endregion


    void DisplayAllLevels()
    {
        if(current.Levels != null && current.Levels.Count > 0)
        {
            for (int i = 0; i < current.Levels.Count; i++)
            {
                //DisplayEachLevelObjective(current.Levels[i].fractalObjective, i * 200);
                DisplayLevelContainer(current.Levels[i], mainContainerRect, i * 300); 
            }
        }
    }

    void DisplayEachLevelObjective(FractalParameter fractalParam, Rect levelContainerRect)
    {

        float baseY = 10;
        float spaceY = 20;

        float globalX = position.width - 400;
        float labelX = 20 + globalX;
        float toggleX = 60 + globalX;
        float valueX = 100 + globalX;
        float textureX = 165 + globalX;

        float textureYOffset = levelContainerRect.height / 15;
        float levelContainerNewHeight = (levelContainerRect.height - textureYOffset);
        float textureWidthHeight = levelContainerNewHeight -50;



        fractalParam.FractalParams.XAxis = EditorGUI.FloatField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY, 50, 15),
            Mathf.Clamp(fractalParam.FractalParams.XAxis, 0, 1)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY, 200, 15), "               XAxis"); 


        fractalParam.FractalParams.ZAxis = EditorGUI.FloatField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 1), 50, 15),
            Mathf.Clamp(fractalParam.FractalParams.ZAxis, 0, 1)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 1), 200, 15), "               ZAxis");


        fractalParam.FractalParams.FractalPower = EditorGUI.FloatField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 2), 50, 15),
            Mathf.Clamp(fractalParam.FractalParams.FractalPower, 0, 1)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 2), 200, 15), "FractalPower");


        fractalParam.FractalParams.FractalType = EditorGUI.IntField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 3), 50, 15), 
            (int)Mathf.Clamp(fractalParam.FractalParams.FractalType, 0, 10)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 3), 200, 15), "  FractalType");


        fractalParam.FractalParams.FractalColor1 = EditorGUI.ColorField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 4), 50, 15),
            fractalParam.FractalParams.FractalColor1
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 4), 200, 15), "FractalColor1");


        fractalParam.FractalParams.FractalColor2 = EditorGUI.ColorField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 5), 50, 15),
            fractalParam.FractalParams.FractalColor2
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 5), 200, 15), "FractalColor1");


        fractalParam.helpUI = GUI.Toggle(new Rect(levelContainerRect.x + toggleX, levelContainerRect.y + baseY + (spaceY * 6), 100, 20), fractalParam.helpUI, "UI HELP?");

        if (fractalParam.timer = GUI.Toggle(new Rect(levelContainerRect.x + toggleX, levelContainerRect.y + baseY + (spaceY * 7), 100, 20), fractalParam.timer, "TIMER?"))
        {
            fractalParam.timeValue = EditorGUI.FloatField(
                new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 8), 50, 20),
                Mathf.Clamp(fractalParam.timeValue, 0, 50)
                );
            EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 8), 200, 15), "TimerValue");
        }



        EditorGUI.DrawPreviewTexture(
            new Rect(levelContainerRect.x + textureX,
            levelContainerRect.y + (levelContainerNewHeight * 0.5f) - (textureWidthHeight * 0.5f),
            textureWidthHeight,
            textureWidthHeight),
            current.windowMainImage
            );


    }

    void DisplayEachLevelPlayer(FractalParameterPlayer fractalParamPlayer, Rect levelContainerRect)
    {

        float baseY = 50;
        float spaceY = 20;

        float globalX = 50;
        float sliderX = 150 + globalX;
        float labelX = 200 + globalX;
        float valueX = 150 + globalX;
        float textureX = globalX;

        float textureYOffset = levelContainerRect.height / 15;
        float levelContainerNewHeight = (levelContainerRect.height - textureYOffset);
        float textureWidthHeight = levelContainerNewHeight - 50;



        fractalParamPlayer.FractalParams.XAxis = EditorGUI.FloatField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY, 50, 15),
            Mathf.Clamp(fractalParamPlayer.FractalParams.XAxis, 0, 1)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY, 200, 15), "XAxis");


        fractalParamPlayer.FractalParams.ZAxis = EditorGUI.FloatField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 1), 50, 15),
            Mathf.Clamp(fractalParamPlayer.FractalParams.ZAxis, 0, 1)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 1), 200, 15), "ZAxis");


        //fractalParamPlayer.FractalParams.FractalPower = 
        //fractalParamPlayer.FractalParams.FractalType = 

        fractalParamPlayer.FractalParams.FractalColor1 = EditorGUI.ColorField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 2), 50, 15),
            fractalParamPlayer.FractalParams.FractalColor1
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 2), 200, 15), "FractalColor1");


        fractalParamPlayer.FractalParams.FractalColor2 = EditorGUI.ColorField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 3), 50, 15),
            fractalParamPlayer.FractalParams.FractalColor2
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 3), 200, 15), "FractalColor1");


        fractalParamPlayer.alignmentPrecision = EditorGUI.FloatField(
            new Rect(levelContainerRect.x + valueX, levelContainerRect.y + baseY + (spaceY * 4), 50, 15),
            Mathf.Clamp(fractalParamPlayer.alignmentPrecision, 0, 1)
            );
        EditorGUI.LabelField(new Rect(levelContainerRect.x + labelX, levelContainerRect.y + baseY + (spaceY * 4), 200, 15), "AlignmentPrecision");

        EditorGUI.Slider(new Rect(levelContainerRect.x + sliderX, levelContainerRect.y + baseY + (spaceY * 5), Mathf.Clamp( (position.width - 850) * 0.5f, 105, 200), 15), 0.5f, 0, 1);

        /*EditorGUI.DrawPreviewTexture(
            new Rect(levelContainerRect.x + textureX,
            levelContainerRect.y + (levelContainerNewHeight * 0.5f) - (textureWidthHeight * 0.5f),
            textureWidthHeight,
            textureWidthHeight),
            FindObjectOfType<FractalPreview>().TestDegGetTexture()
            ); */


    }

    void DisplayLevelContainer(FractalLevels fl ,Rect mainContainerRect, float heightDifference)
    {
        fl.levelWindowRect = new Rect(mainContainerRect.x + 5, mainContainerRect.y + 10 + heightDifference, mainContainerRect.width - mainContainerRect.x - 5, 200);
        EditorGUI.DrawRect(fl.levelWindowRect, current.windowLevelContainerColor);

        DisplayEachLevelObjective(fl.fractalObjective, fl.levelWindowRect);
        DisplayEachLevelPlayer(fl.fractalPlayer, fl.levelWindowRect);
    }

    float GetRectYModification(float currentRectY)
    {
        return currentRectY + ((Mathf.Clamp(position.width, 100, 740)) * 0.52f) + 75;
    }

}
