using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FractalLevel", menuName = " Scriptable/FractalLevel")]
public class FractalLevels : ScriptableObject
{
    public FractalParameterPlayer fractalPlayer;
    public FractalParameter fractalObjective;
    public Rect levelWindowRect;
}
