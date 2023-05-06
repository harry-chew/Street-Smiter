using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public SetDrawing drawing;
    public SetSlapping slapping;
    [SerializeField] private SmashBar smashBar;
    [SerializeField] private AnimatorHandler animatorHandler;
    [SerializeField] private WritingHelp drawingHelpPanel;
    [SerializeField] private GameObject shoe;
    
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
                break;
            case "PostSmiting":
                slapping.TurnOffSlapping();
                shoe.SetActive(false);
                animatorHandler.StopAnimation();
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
                drawingHelpPanel.DisplayPanel();
                break;
            case "PreSmiting":
                //do something
                slapping.TurnOnSlapping();
                
                break;
            case "PostSmiting":
                //do something
                Debug.Log("This is the end of the game for the moment.");
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
