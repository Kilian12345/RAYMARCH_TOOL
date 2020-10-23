using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Image RotationFull1;
    [SerializeField] Image RotationFull2;
    public bool RotationFull1Active;
    public bool RotationFull2Active;
    [SerializeField] Color FullColor;
    [SerializeField] Color VictoryColor;


    public void ActiveImageState(int rotationImage)
    {
        if(rotationImage == 0)
        {
            RotationFull1.color = FullColor;
            RotationFull1.enabled = true;
            RotationFull1Active = true;
        }
        else
        {
            RotationFull2.color = FullColor;
            RotationFull2.enabled = true;
            RotationFull2Active = true;
        }
    }

    public void DesactiveImageState(int rotationImage)
    {
        if (rotationImage == 0)
        {
            RotationFull1.enabled = false;
            RotationFull1Active = false;
        }
        else
        {
            RotationFull2.enabled = false;
            RotationFull2Active = false;
        }
    }

    public void Victory()
    {
        if (RotationFull1.enabled == false)
            RotationFull1.enabled = true;

        if (RotationFull2.enabled == false)
            RotationFull2.enabled = true;

        RotationFull1Active = true;
        RotationFull2Active = true;

        RotationFull1.color = VictoryColor;
        RotationFull2.color = VictoryColor;
    }

}
