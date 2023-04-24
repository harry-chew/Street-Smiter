using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueGroup
    //Different dialogue groups, as there needs to be separation between the start dialogue, then the dialogue after writing name, then dialogue after beating name 
    {
        public string name;
        public List<string> dialogueLines = new List<string>();
    }

    public static DialogueSystem Instance { get; private set; }

    public TMPro.TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;

    public bool isDialogueActive = false;

    public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();
    public List<string> dialogueLines = new List<string>();

    public AudioClip[] audioClips = new AudioClip[8];
    public GameObject player;

    public void OnEnable()
    {
        CameraTrigger.OnGameStart += HandleFirstDialogue;
    }

    private void HandleFirstDialogue()
    {
        dialogueLines.Clear();
        //to handle first dialogue, load the first/intro dialogueGroup
        LoadDialogueGroup("Intro");
        
        //ReplaceDialogue(dialogueLinesArray);
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
        PlayDialogueAudio();
        DisplayNextDialogueMessage();
    }

    public void PlayDialogueAudio()
    {
        if (audioClips.Length == 0)
        {
            Debug.LogError("Audio clip array is empty!");
            return;
        }

        // Choose a random clip from the array 
        int index = UnityEngine.Random.Range(0, audioClips.Length);
        AudioClip clip = audioClips[index];

        // Play the chosen clip
        AudioSource.PlayClipAtPoint(clip, player.transform.position);

    }

    public void LoadDialogueGroup(string name)
    {
        //get the dialogueGroup by name (from list in dialogueGroups
        //replace the dialogueLines array/List w the one from dialogueGroup
        foreach (var dialogueGroup in dialogueGroups)
        {
            if (dialogueGroup.name == name)
            {
                AddNewDialogue(dialogueGroup.dialogueLines);
            }
        }
    }

}
