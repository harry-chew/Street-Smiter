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
        //load the dialogue before slapping, turn on slapping in HandleDialogueEnd
        DialogueSystem.Instance.LoadDialogueGroup("PreSmiting");
        //slapping.TurnOnSlapping();
    }
}
