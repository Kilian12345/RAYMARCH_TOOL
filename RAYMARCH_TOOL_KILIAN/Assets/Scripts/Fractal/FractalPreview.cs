using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FractalPreview : MonoBehaviour
{
    public ComputeShader fractalShader;

    [Range(1, 20)]
    public float fractalPower = 10;
    public float darkness = 70;

    [Header("Colour mixing")]
    [Range(0, 1)] public float blackAndWhite;
    [Range(0, 1)] public float redA;
    [Range(0, 1)] public float greenA;
    [Range(0, 1)] public float blueA = 1;
    [Range(0, 1)] public float redB = 1;
    [Range(0, 1)] public float greenB;
    [Range(0, 1)] public float blueB;

    RenderTexture target;
    public int textureWidthHeight = 200;

    [Header("Animation Settings")]
    public float powerIncreaseSpeed = 0.2f;



    [HideInInspector] public float _XRotationAxisObjectif;
    [HideInInspector] public float _ZRotationAxisObjectif;
    [HideInInspector] public float _XRotationAxisPlayer;
    [HideInInspector] public float _ZRotationAxisPlayer;



    [Range(0, 1)] public float FractalVisibility;

    public int FractalType;

    public Vector3 _CameraToWorld;
    public Vector3 _CameraRotation;
    public Vector3 _LightDirection;



    public Texture TestDegGetTexture()
    {
        return target;
    }


    void Start()
    {
        Application.targetFrameRate = 60;
    }


    // Animate properties
    void Update()
    {
        if (Application.isPlaying)
        {
            fractalPower += powerIncreaseSpeed * Time.deltaTime;
        }

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        InitRenderTexture();
        SetParameters();


        int threadGroupsX = Mathf.CeilToInt(textureWidthHeight / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(textureWidthHeight / 8.0f);
        fractalShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        Graphics.Blit(target, destination);

    }


    void SetParameters()
    {
        fractalShader.SetTexture(0, "Destination", target);
        fractalShader.SetFloat("power", Mathf.Max(fractalPower, 1.01f));
        fractalShader.SetFloat("darkness", darkness);
        fractalShader.SetFloat("blackAndWhite", blackAndWhite);

        fractalShader.SetFloat("_ZRotationAxisObjectif", _ZRotationAxisObjectif);
        fractalShader.SetFloat("_XRotationAxisObjectif", _XRotationAxisObjectif);
        fractalShader.SetFloat("_ZRotationAxisPlayer", _ZRotationAxisPlayer);
        fractalShader.SetFloat("_XRotationAxisPlayer", _XRotationAxisPlayer);

        fractalShader.SetFloat("FractalVisibility", FractalVisibility);

        fractalShader.SetInt("FractalType", FractalType);

        fractalShader.SetVector("colourAMix", new Vector3(redA, greenA, blueA));
        fractalShader.SetVector("colourBMix", new Vector3(redB, greenB, blueB));

        fractalShader.SetVector("_CameraToWorld", _CameraToWorld);
        fractalShader.SetVector("_CameraRotation", _CameraRotation);
        fractalShader.SetVector("_LightDirection", _LightDirection);

    }


    void InitRenderTexture()
    {
        if (target == null || target.width != textureWidthHeight || target.height != textureWidthHeight)
        {
            if (target != null)
            {
                target.Release();
            }
            target = new RenderTexture(textureWidthHeight, textureWidthHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            target.enableRandomWrite = true;
            target.Create();
        }
    }
}
