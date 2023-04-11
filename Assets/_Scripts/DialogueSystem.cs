using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    public TMPro.TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;

    public bool isDialogueActive = false;

    public List<string> dialogueLines = new List<string>();

    public void OnEnable()
    {
        CameraTrigger.OnGameStart += HandleFirstDialogue;
    }

    private void HandleFirstDialogue()
    {
        string[] dialogueLinesArray = new string[dialogueLines.Count];

        for (int i = 0; i < dialogueLines.Count; i++)
        {
            dialogueLinesArray[i] = dialogueLines[i];
        }
        ReplaceDialogue(dialogueLinesArray);
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DisplayDialoguePanel(false);
    }

    public void DisplayDialoguePanel(bool display)
    {
        dialoguePanel.SetActive(display);
    }

    public void DisplayNextDialogueMessage()
    {
        if (dialogueLines.Count <= 0 || dialogueLines == null) return;
        if (isDialogueActive) return;

        textDisplay.text = "";
        StartCoroutine(DisplayTextByEachLetter());
    }

    public IEnumerator DisplayTextByEachLetter()
    {
        if (dialogueLines.Count <= 0 || dialogueLines == null) yield return null;
        isDialogueActive = true;
        foreach (char letter in dialogueLines[0].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        dialogueLines.RemoveAt(0);
        if (dialogueLines.Count <= 0 || dialogueLines == null)
        {
            yield return new WaitForSeconds(0.5f);
            DisplayDialoguePanel(false);
        }
        isDialogueActive = false;
    }


    public void AddNewDialogue(List<string> lines)
    {
        foreach (string line in lines)
        {
            dialogueLines.Add(line);
        }

        DisplayDialoguePanel(true);
        DisplayNextDialogueMessage();
    }

    public void ReplaceDialogue(string[] lines)
    {
        dialogueLines.Clear();
        foreach (string line in lines)
        {
            dialogueLines.Add(line);
        }

        DisplayDialoguePanel(true);
        DisplayNextDialogueMessage();
    }
}
