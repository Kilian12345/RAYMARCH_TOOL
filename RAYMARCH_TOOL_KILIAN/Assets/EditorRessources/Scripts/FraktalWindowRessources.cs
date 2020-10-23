using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFraktalWindowRessources", menuName = " Scriptable/Window/Fraktal Window Ressources")]
public class FraktalWindowRessources : ScriptableObject
{
    public string windowTitle;
    public Font windowTitleFont;
    public Color windowTitleContainerColor;
    public Color windowMainContainerColor;
    public Color windowLevelContainerColor;
    public Texture windowMainImage;
}
