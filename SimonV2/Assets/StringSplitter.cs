using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringSplitter : MonoBehaviour
{

    public TextAsset myTextAsset;

    // Start is called before the first frame update
    void Start()
    {

        string fullFileContents = myTextAsset.text;
        string[] lines = fullFileContents.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        List<string>[] fullTable = new List<string>[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            fullTable[i] = new List<string>();
            string[] columns = lines[i].Split(new string[] { "," }, StringSplitOptions.None);

            for (int y = 0; y < columns.Length; y++)
            {
                fullTable[i].Add(columns[y]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
