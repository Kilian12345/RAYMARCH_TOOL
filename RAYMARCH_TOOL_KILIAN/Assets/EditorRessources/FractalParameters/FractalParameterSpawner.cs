using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class FractalParameterSpawner
{
    ///////// SPAWN

    public static FractalLevels AddFractalLevel(int index)
    {
        FractalParameter assetObj = ScriptableObject.CreateInstance<FractalParameter>();
        AssetDatabase.CreateAsset(assetObj, String.Format("Assets/ScriptableObject/{0}.asset", "FractalParameters" + index.ToString()));

        FractalParameterPlayer assetPla = ScriptableObject.CreateInstance<FractalParameterPlayer>();
        AssetDatabase.CreateAsset(assetPla, String.Format("Assets/ScriptableObject/{0}.asset", "FractalParametersPlayer" + index.ToString()));

        FractalLevels asset = ScriptableObject.CreateInstance<FractalLevels>();
        AssetDatabase.CreateAsset(asset, String.Format("Assets/ScriptableObject/{0}.asset", "FractalLevels" + index.ToString()));
        EditorUtility.FocusProjectWindow();

        asset.fractalObjective = assetObj;
        asset.fractalPlayer = assetPla;

        return asset;
    }



    /// //////DELETE

    public static void DeleteFractalLevel(FractalLevels asset)
    {
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.fractalObjective));
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.fractalPlayer));
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
    }

}
