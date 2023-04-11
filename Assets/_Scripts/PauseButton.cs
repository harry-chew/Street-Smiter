using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public static event Action<bool> OnPause;

    [SerializeField] private TMPro.TextMeshProUGUI _pauseButtonText;
    [SerializeField] private bool isPaused;

    public void Start() {
        isPaused = false;
    }
    public void ClickPauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            SceneAudio.Instance.PlayAudio();
            _pauseButtonText.text = "||";
            OnPause?.Invoke(false);
        }
        else
        {
            isPaused = true;
            SceneAudio.Instance.PauseAudio();
            _pauseButtonText.text = ">>";
            OnPause?.Invoke(true);
        }
        
    }
}
