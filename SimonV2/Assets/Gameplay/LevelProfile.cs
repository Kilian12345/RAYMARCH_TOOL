using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Profile", menuName = " Scriptable/Level/Level Profile" )]
public class LevelProfile : ScriptableObject
{
    public float difficulty;
    public Color environmentColor;
    public int[] levelValues;
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
}
