using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TSVFromatFixezr : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets == null) return;

        if (importedAssets.Length == 0) return;

        for (int i = 0; i < importedAssets.Length; i++)
        {

            string str = importedAssets[i];
            if (!importedAssets[i].EndsWith(".tsv")) continue;


            str = str.Substring(0, str.Length - 4);
            str += ".csv";
            File.Move(importedAssets[i], str);
            File.Move(importedAssets[i] + ".meta", str + ".meta");


            //ConvertToCSV(importedAssets[i]);

        }
    }

    public static void ConvertToCSV(string assetPath)
    {
        AssetDatabase.RenameAsset(assetPath, assetPath.Replace(".tsv", ".csv"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
