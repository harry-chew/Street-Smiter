using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public SetDrawing drawing;
    public SetSlapping slapping;
    [SerializeField] private SmashBar smashBar;

    private void OnEnable()
    {
        DialogueSystem.OnDialogueStart += HandleDialogueStart;
        DialogueSystem.OnDialogueEnd += HandleDialogueEnd;
    }

    private void OnDisable()
    {
        DialogueSystem.OnDialogueStart -= HandleDialogueStart;
        DialogueSystem.OnDialogueEnd -= HandleDialogueEnd;
    }
    
    private void HandleDialogueStart(string dialogueGroupName)
    {
        Debug.Log("Dialogue started: " + dialogueGroupName);
        switch (dialogueGroupName)
        {
            case "Intro":
                //do something
                break;
            case "PreNameWriting":
                //do something

                break;
            case "PreSmiting":
                //do something
                break;
            case "PostSmiting":
                
                //do something
                break;
            default:
                break;
        }
    }

    private void HandleDialogueEnd(string dialogueGroupName)
    {
        Debug.Log("Dialogue ended: " + dialogueGroupName);
        switch (dialogueGroupName)
        {
            case "Intro":
                //start prename writing event
                DialogueSystem.Instance.LoadDialogueGroup("PreNameWriting");
                //move camera into sitting position
                break;
            case "PreNameWriting":
                //do something
                Debug.Log("Load drawing section");
                drawing.TurnOnDrawing();
                break;
            case "PreSmiting":
                //do something
                slapping.TurnOnSlapping();
                
                break;
            case "PostSmiting":
                //do something
                break;
            default:
                break;
        }
    }
}
