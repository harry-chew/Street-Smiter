using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSmitingPhase : MonoBehaviour
{
    [SerializeField] private SetDrawing drawing;
    [SerializeField] private SetSlapping slapping;
    
    public void StartSmiting()
    {
        drawing.TurnOffDrawing();
        slapping.TurnOnSlapping();
    }
}
