using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FractalParameterPlayer", menuName = " Scriptable/FractalParameter/Player")]
public class FractalParameterPlayer : ScriptableObject
{
    public Texture FractalPreview;
    public float alignmentPrecision;
    public FractalParameterStruct FractalParams;
}
