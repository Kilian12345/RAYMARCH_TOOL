using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FraktalWindowEditor", menuName = " Scriptable/Window/Fraktal Window Editor")]
public class FraktalWindowEditor : ScriptableObject
{
    public FraktalWindowRessources fraktalRessource;
    public string windowTitle => fraktalRessource.windowTitle;
    public Font windowTitleFont => fraktalRessource.windowTitleFont;
    public Color windowTitleContainerColor => fraktalRessource.windowTitleContainerColor;
    public Color windowMainContainerColor => fraktalRessource.windowMainContainerColor;
    public Color windowLevelContainerColor => fraktalRessource.windowLevelContainerColor;
    public Texture windowMainImage => fraktalRessource.windowMainImage;

    [Space(50)]
    public List<FractalLevels> Levels = new List<FractalLevels>();


    [Header("RectPosition")]
    public Rect toolBarRect;
    public Rect spawnRect;
    public Rect helpUiALLRect;
    public Rect timerALLRect;
    public Rect deleteALL;




}
