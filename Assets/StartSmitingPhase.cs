using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSmitingPhase : MonoBehaviour
{
    [SerializeField] private SetDrawing drawing;
    [SerializeField] private SetSlapping slapping;
    [SerializeField] private AnimatorHandler _animator;
    
    public void StartSmiting()
    {
        _animator.PlayTargetAnimation("Slapping");
        StartCoroutine(LoadDialogue()); //currently does not work because drawingObject is disabled w/ next statement
        drawing.TurnOffDrawing();
    }

    IEnumerator LoadDialogue()
    {
        yield return new WaitForSeconds(3);
        DialogueSystem.Instance.LoadDialogueGroup("PreSmiting");
        
    }


}
