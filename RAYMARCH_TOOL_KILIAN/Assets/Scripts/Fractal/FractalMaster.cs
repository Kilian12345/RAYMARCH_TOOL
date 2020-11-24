using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class FractalMaster : MonoBehaviour 
{

    public ComputeShader fractalShader;

    [Range (1, 20)]
    public float fractalPower = 10;
    public float darkness = 70;

    [Header ("Colour mixing")]
    [Range (0, 1)] public float blackAndWhite;
    [Range (0, 1)] public float redA;
    [Range (0, 1)] public float greenA;
    [Range (0, 1)] public float blueA = 1;
    [Range (0, 1)] public float redB = 1;
    [Range (0, 1)] public float greenB;
    [Range (0, 1)] public float blueB;

    RenderTexture target;
    Camera cam;
    Light directionalLight;

    [Header ("Animation Settings")]
    public float powerIncreaseSpeed = 0.2f;


    public Color ColorPlayer1 = Color.white;
    public Color ColorPlayer2 = Color.white;

    public Color ColorObjective1 = Color.red;
    public Color ColorObjective2 = Color.red;


    [HideInInspector] public float _XRotationAxisObjectif;
   [HideInInspector] public float _ZRotationAxisObjectif;
   [HideInInspector] public float _XRotationAxisPlayer;
   [HideInInspector] public float _ZRotationAxisPlayer;


    public int FractalType = 1;

    [Range(0,10)]public float SeparationDistance;
    [Range(0, 1)] public float FractalVisibility;


    public Texture TestDegGetTexture()
    {
        return target;
    }


    void Start() {
        Application.targetFrameRate = 60;
    }
    
    void Init () {
        cam = Camera.current;
        directionalLight = FindObjectOfType<Light> ();
    }

    // Animate properties
    void Update () {
        if (Application.isPlaying) {
            fractalPower += powerIncreaseSpeed * Time.deltaTime;
        }

    }

    void OnRenderImage (RenderTexture source, RenderTexture destination) {
        Init ();
        InitRenderTexture ();
        SetParameters ();


        int threadGroupsX = Mathf.CeilToInt (cam.pixelWidth / 8.0f);
        int threadGroupsY = Mathf.CeilToInt (cam.pixelHeight / 8.0f);
        fractalShader.Dispatch (0, threadGroupsX, threadGroupsY, 1);

        Graphics.Blit (target, destination);

    }


    void SetParameters () {
        fractalShader.SetTexture (0, "Destination", target);
        fractalShader.SetFloat ("power", Mathf.Max (fractalPower, 1.01f));
        fractalShader.SetFloat ("darkness", darkness);
        fractalShader.SetFloat ("blackAndWhite", blackAndWhite);

        fractalShader.SetFloat ("_ZRotationAxisObjectif", _ZRotationAxisObjectif);
        fractalShader.SetFloat ("_XRotationAxisObjectif", _XRotationAxisObjectif);
        fractalShader.SetFloat("_ZRotationAxisPlayer", _ZRotationAxisPlayer);
        fractalShader.SetFloat("_XRotationAxisPlayer", _XRotationAxisPlayer);

        fractalShader.SetFloat ("SeparationDistance", SeparationDistance);
        fractalShader.SetFloat ("FractalVisibility", FractalVisibility);

        fractalShader.SetInt("FractalType", FractalType);

        fractalShader.SetVector ("colourAMixObj", new Vector3 (ColorObjective1.r, ColorObjective1.g, ColorObjective1.b));
        fractalShader.SetVector ("colourBMixObj", new Vector3(ColorObjective2.r, ColorObjective2.g, ColorObjective2.b));
        fractalShader.SetVector ("colourAMixPla", new Vector3(ColorPlayer1.r, ColorPlayer1.g, ColorPlayer1.b));
        fractalShader.SetVector ("colourBMixPla", new Vector3(ColorPlayer2.r, ColorPlayer2.g, ColorPlayer2.b));

        fractalShader.SetMatrix ("_CameraToWorld", cam.cameraToWorldMatrix);
        fractalShader.SetMatrix ("_CameraInverseProjection", cam.projectionMatrix.inverse);
        fractalShader.SetVector ("_LightDirection", directionalLight.transform.forward);

    }


    void InitRenderTexture () {
        if (target == null || target.width != cam.pixelWidth || target.height != cam.pixelHeight) {
            if (target != null) {
                target.Release ();
            }
            target = new RenderTexture (cam.pixelWidth, cam.pixelHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            target.enableRandomWrite = true;
            target.Create ();
        }
    }

}