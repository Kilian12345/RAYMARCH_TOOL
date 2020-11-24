using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private static Gameplay _instance;
    public static Gameplay instance { get { return _instance; } }

    [SerializeField] FractalMaster fractalRenderer;
    [SerializeField] UIManager uiManager;  

    public GameplayLevels gamePlayLevels;

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

    private float waitTimeForNextLevel = 3;
    private int levelIndex = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        if (gamePlayLevels != null && gamePlayLevels.fractalLevel.Length > 0)
        {
            SetupToRenderer(gamePlayLevels.fractalLevel[0]);
        }

        _XRotationAxisObjectif = gamePlayLevels.RotXObjectif;
        _ZRotationAxisObjectif = gamePlayLevels.RotYObjectif;
        _XRotationAxisPlayer = gamePlayLevels.RotXPlayer;
        _ZRotationAxisPlayer = gamePlayLevels.RotYPlayer;
    }

    private void Update()
    {
        CalculAlignment();
        SendInputToRenderer();
    }


    void SetupToRenderer(FractalLevels fl)
    {

        fractalRenderer._XRotationAxisPlayer = fl.FP_XRotationPlayer; 
        fractalRenderer._ZRotationAxisPlayer = fl.FP_ZRotationPlayer;
        fractalRenderer._XRotationAxisObjectif = fl.FP_XRotationObjective;
        fractalRenderer._ZRotationAxisObjectif = fl.FP_ZRotationObjective;

        fractalRenderer.ColorPlayer1 = fl.FPColorPlayer1;
        fractalRenderer.ColorPlayer2 = fl.FPColorPlayer2;

        fractalRenderer.ColorObjective1 = fl.FPColorObjective1;
        fractalRenderer.ColorObjective2 = fl.FPColorObjective2;

        fractalRenderer.FractalType = fl.FPFractalType;
        fractalRenderer.fractalPower = fl.FPFractalPower;
        //fractalRenderer.
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
            LevelVictory();
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

    void FullVictory()
    {

    }

    void LevelVictory()
    {
        uiManager.Victory();
    }

    IEnumerator WaitForNextLevel(int index)
    {
        yield return new WaitForSeconds(waitTimeForNextLevel);

        if(index <= gamePlayLevels.fractalLevel.Length)
        {
            SetupToRenderer(gamePlayLevels.fractalLevel[index]);
        }

    }
}
