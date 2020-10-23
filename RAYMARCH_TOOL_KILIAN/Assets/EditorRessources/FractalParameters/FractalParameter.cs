using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FractalParameter", menuName = " Scriptable/FractalParameter/Objective")]
public class FractalParameter : ScriptableObject
{
    public Texture FractalPreview;
    public bool helpUI;
    public bool timer;
    public float timeValue;
    public FractalParameterStruct FractalParams;
}
