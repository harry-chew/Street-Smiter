using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSmitingPhase : MonoBehaviour
{
    [SerializeField] private SetDrawing drawing;
    [SerializeField] private SetSlapping slapping;
    [SerializeField] private AnimatorHandler _animator;
    [SerializeField] private GameObject shoe;

    public void StartSmiting()
    {
        shoe.SetActive(true);
        _animator.PlayTargetAnimation("Slapping");
        StartCoroutine(LoadDialogue());
        drawing.TurnOffDrawing();
    }

    IEnumerator LoadDialogue()
    {
        yield return new WaitForSeconds(2.5f);
        DialogueSystem.Instance.LoadDialogueGroup("PreSmiting");
        
    }


}
