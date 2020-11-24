using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalPreview : MonoBehaviour
{
    public ComputeShader fractalShaderPreview;

    private ComputeShader fractalShaderPreviewInstancePlayer;
    private ComputeShader fractalShaderPreviewInstanceObjective;

    [Range(1, 20)]
    public float fractalPower = 4.1f;
    public float darkness = 43;

    [Header("Colour mixing")]
    [Range(0, 1)] public float blackAndWhite;
    [Range(0, 1)] public float redA;
    [Range(0, 1)] public float greenA;
    [Range(0, 1)] public float blueA = 1;
    [Range(0, 1)] public float redB = 1;
    [Range(0, 1)] public float greenB;
    [Range(0, 1)] public float blueB;

    public RenderTexture targetPlayer;
    public RenderTexture targetObjective;

    public int textureWidthHeight = 200;

    [Header("Animation Settings")]
    public float powerIncreaseSpeed = 0.2f;

    public Color ColorPlayer1;
    public Color ColorPlayer2;

    public Color ColorObjective1;
    public Color ColorObjective2;


    public float _XRotationPlayer;
    public float _ZRotationPlayer;
    public float _XRotationObjective;
    public float _ZRotationObjective;


    [Range(0, 1)] public float FractalVisibility = 0.359f;

    public int FractalType = 1;

    public Camera cam;
    Light directionalLight;


    public FractalPreview(ComputeShader fractalShaderPreviewParam, Light directionalLightParam, Camera camParam)
    {
        fractalShaderPreview = fractalShaderPreviewParam;
        directionalLight = directionalLightParam;
        cam = camParam;

        fractalShaderPreviewInstancePlayer = Instantiate(fractalShaderPreviewParam);
        fractalShaderPreviewInstanceObjective = Instantiate(fractalShaderPreviewParam);
    }


    public void RenderLevelPreviewFractal()
    {
        InitRenderTexture();
        SetParametersPlayer();
        SetParametersObjective();

        int threadGroupsX = Mathf.CeilToInt(textureWidthHeight / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(textureWidthHeight / 8.0f);
        fractalShaderPreviewInstancePlayer.Dispatch(0, threadGroupsX, threadGroupsY, 1);
        fractalShaderPreviewInstanceObjective.Dispatch(0, threadGroupsX, threadGroupsY, 1);

    }


    void SetParametersPlayer()
    {
        fractalShaderPreviewInstancePlayer.SetTexture(0, "Destination", targetPlayer);
        fractalShaderPreviewInstancePlayer.SetFloat("power", Mathf.Max(fractalPower, 1.01f));
        fractalShaderPreviewInstancePlayer.SetFloat("darkness", darkness);
        fractalShaderPreviewInstancePlayer.SetFloat("blackAndWhite", blackAndWhite);

        fractalShaderPreviewInstancePlayer.SetFloat("_XRotation", _XRotationPlayer);
        fractalShaderPreviewInstancePlayer.SetFloat("_ZRotation", _ZRotationPlayer);

        fractalShaderPreviewInstancePlayer.SetFloat("FractalVisibility", FractalVisibility);

        fractalShaderPreviewInstancePlayer.SetInt("FractalType", FractalType);

        fractalShaderPreviewInstancePlayer.SetVector("colourAMix", new Vector3(ColorPlayer1.r, ColorPlayer1.g, ColorPlayer1.b));
        fractalShaderPreviewInstancePlayer.SetVector("colourBMix", new Vector3(ColorPlayer2.r, ColorPlayer2.g, ColorPlayer2.b));

        fractalShaderPreviewInstancePlayer.SetMatrix("_CameraToWorld", cam.cameraToWorldMatrix);
        fractalShaderPreviewInstancePlayer.SetMatrix("_CameraInverseProjection", cam.projectionMatrix.inverse);
        fractalShaderPreviewInstancePlayer.SetVector("_LightDirection", directionalLight.transform.forward);

    }

    void SetParametersObjective()
    {
        fractalShaderPreviewInstanceObjective.SetTexture(0, "Destination", targetObjective);
        fractalShaderPreviewInstanceObjective.SetFloat("power", Mathf.Max(fractalPower, 1.01f));
        fractalShaderPreviewInstanceObjective.SetFloat("darkness", darkness);
        fractalShaderPreviewInstanceObjective.SetFloat("blackAndWhite", blackAndWhite);

        fractalShaderPreviewInstanceObjective.SetFloat("_XRotation", _XRotationObjective);
        fractalShaderPreviewInstanceObjective.SetFloat("_ZRotation", _ZRotationObjective);

        fractalShaderPreviewInstanceObjective.SetFloat("FractalVisibility", FractalVisibility);

        fractalShaderPreviewInstanceObjective.SetInt("FractalType", FractalType);

        fractalShaderPreviewInstanceObjective.SetVector("colourAMix",new Vector3(ColorObjective1.r, ColorObjective1.g, ColorObjective1.b));
        fractalShaderPreviewInstanceObjective.SetVector("colourBMix", new Vector3(ColorObjective2.r, ColorObjective2.g, ColorObjective2.b));

        fractalShaderPreviewInstanceObjective.SetMatrix("_CameraToWorld", cam.cameraToWorldMatrix);
        fractalShaderPreviewInstanceObjective.SetMatrix("_CameraInverseProjection", cam.projectionMatrix.inverse);
        fractalShaderPreviewInstanceObjective.SetVector("_LightDirection", directionalLight.transform.forward);

    }

    void InitRenderTexture()
    {
        if (targetPlayer == null || targetPlayer.width != textureWidthHeight || targetPlayer.height != textureWidthHeight)
        {
            if (targetPlayer != null)
            {
                targetPlayer.Release();
            }

            targetPlayer = new RenderTexture(textureWidthHeight, textureWidthHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            targetPlayer.enableRandomWrite = true;
            targetPlayer.Create();
        }

        if (targetObjective == null || targetObjective.width != textureWidthHeight || targetObjective.height != textureWidthHeight)
        {
            if (targetObjective != null)
            {
                targetObjective.Release();
            }

            targetObjective = new RenderTexture(textureWidthHeight, textureWidthHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            targetObjective.enableRandomWrite = true;
            targetObjective.Create();
        }
    }
}
