using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] FractalMaster fractalRenderer;
    [SerializeField] UIManager uiManager;


    public float axisStrength = 0.33f;
    [Range(0,0.1f)]public float alignmentPrecision = 0.33f;
    public float _XRotationAxisObjectif;
    public float _ZRotationAxisObjectif;
    public float _XRotationAxisPlayer;
    public float _ZRotationAxisPlayer;

    //DEBUG
    public float _XRotOBJ;
    public float _ZRotOBJ;
    public float _XRotPLA;
    public float _ZRotPLA;

    private bool XisAligned;
    private bool ZisAligned;

    private void Start()
    {
        SetupInputToRenderer();
    }

    private void Update()
    {
        CalculAlignment();
        SendInputToRenderer();
    }


    void SetupInputToRenderer()
    {
        fractalRenderer._XRotationAxisPlayer = _XRotationAxisPlayer;
        fractalRenderer._ZRotationAxisPlayer = _ZRotationAxisPlayer;
        fractalRenderer._XRotationAxisObjectif = _XRotationAxisObjectif;
        fractalRenderer._ZRotationAxisObjectif = _ZRotationAxisObjectif;
    }

    void SendInputToRenderer()
    {
        fractalRenderer._XRotationAxisPlayer = _XRotationAxisPlayer;
        fractalRenderer._ZRotationAxisPlayer = _ZRotationAxisPlayer;
    }

    void CalculAlignment()
    {

        _ZRotationAxisPlayer += Input.GetAxis("Vertical") * axisStrength;
        _XRotationAxisPlayer += Input.GetAxis("Horizontal") * axisStrength;

        float TwoPI = 2 * Mathf.PI;

        _XRotOBJ = (_XRotationAxisObjectif / TwoPI) - (1 * Mathf.FloorToInt((_XRotationAxisObjectif) / TwoPI));
        _ZRotOBJ = (_ZRotationAxisObjectif / TwoPI) - (1 * Mathf.FloorToInt((_ZRotationAxisObjectif) / TwoPI));
        _XRotPLA = (_XRotationAxisPlayer / TwoPI) - (1 * Mathf.FloorToInt((_XRotationAxisPlayer) / TwoPI));
        _ZRotPLA = (_ZRotationAxisPlayer / TwoPI) - (1 * Mathf.FloorToInt((_ZRotationAxisPlayer) / TwoPI));



        CheckIfAlreadyAligned();


        Vector2 rotateOBJ = new Vector2(_XRotOBJ, _ZRotOBJ).normalized;
        Vector2 rotatePLA = new Vector2(_XRotPLA, _ZRotPLA).normalized;

        float dotResult = Mathf.Clamp(Vector2.Dot(rotateOBJ, rotatePLA), 0, 1);

        //Debug.Log("Actual XisAligned = " + XisAligned + " || ZisAligned = " + ZisAligned);
    }



    void CheckIfAlreadyAligned()
    {
        if(XisAligned == true && ZisAligned == true)
        {
            uiManager.Victory();
        }


        if (_XRotPLA > _XRotOBJ - alignmentPrecision && _XRotPLA < _XRotOBJ + alignmentPrecision && uiManager.RotationFull1Active == false)
        {
            XisAligned = true;
            uiManager.ActiveImageState(0);
        }
        else if((_XRotPLA < _XRotOBJ - alignmentPrecision || _XRotPLA > _XRotOBJ + alignmentPrecision) && uiManager.RotationFull1Active == true)
        {
            XisAligned = false;
            uiManager.DesactiveImageState(0);
        }


        if (_ZRotPLA > _ZRotOBJ - alignmentPrecision && _ZRotPLA < _ZRotOBJ + alignmentPrecision && uiManager.RotationFull2Active == false)
        {
            ZisAligned = true;
            uiManager.ActiveImageState(1);
        }
        else if ((_ZRotPLA < _ZRotOBJ - alignmentPrecision || _ZRotPLA > _ZRotOBJ + alignmentPrecision) && uiManager.RotationFull2Active == true)
        {
            ZisAligned = false;
            uiManager.DesactiveImageState(1);
        }
    }
}
