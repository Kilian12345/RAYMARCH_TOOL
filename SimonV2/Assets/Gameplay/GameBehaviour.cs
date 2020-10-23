using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyProfile
{
    public Color color;
    public float speed;
    public Vector3 spawnPosition;
    public AnimationCurve acceleration;
}

public class GameBehaviour : MonoBehaviour
{
    public Color myColor;
    [Header("Texts")]
    public string myString;
    [TextArea]
    public string myBigString;
    [Header("Numbers")]
    [Range(0f, 1f)]
    public float mySlider;
    public Vector2 myVector2;

    public string[] myStrings;

    public EnemyProfile myEnemyProfile;
}
