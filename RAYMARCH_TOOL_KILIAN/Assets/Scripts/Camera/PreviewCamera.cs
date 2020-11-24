using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PreviewCamera : MonoBehaviour
{

    [SerializeField] Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam.rect = new Rect(0, 0, ((float)Screen.currentResolution.height / (float)Screen.currentResolution.width), 1);
    }

}
