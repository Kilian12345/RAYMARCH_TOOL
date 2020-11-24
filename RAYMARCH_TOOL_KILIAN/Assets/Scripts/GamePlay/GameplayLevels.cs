using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayLevels", menuName = " Scriptable/GameplayLevels")]
public class GameplayLevels : ScriptableObject
{
    public FractalLevels[] fractalLevel;
    public float RotXPlayer;
    public float RotYPlayer;
    public float RotXObjectif;
    public float RotYObjectif;
}
