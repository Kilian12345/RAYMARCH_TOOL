using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    public Transform myTransform;
    public Transform camTransform;
    public AudioListener audioListener;
    
    [HideInInspector]
    public float mySpeed;

    public Color myColor;
    public Vector2 myVector2;
    public WrapMode myWrapMode;

#if UNITY_EDITOR
    public bool foldoutState;
#endif
}
