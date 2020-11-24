using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FractalLevel", menuName = " Scriptable/FractalLevel")]
public class FractalLevels : ScriptableObject
{
    public FractalParameterPlayer fractalPlayer;
    public FractalParameter fractalObjective;
    public Rect levelWindowRect;
    public FractalPreview fractalPreviewer;

    [HideInInspector] public Color FPColorPlayer1;
    [HideInInspector] public Color FPColorPlayer2;

    [HideInInspector] public Color FPColorObjective1;
    [HideInInspector] public Color FPColorObjective2;

    [HideInInspector] public float FP_XRotationPlayer;
    [HideInInspector] public float FP_ZRotationPlayer;
    [HideInInspector] public float FP_XRotationObjective;
    [HideInInspector] public float FP_ZRotationObjective;
    [HideInInspector] public int FPFractalType = 1;
    [HideInInspector] public float FPFractalPower = 1;

}
