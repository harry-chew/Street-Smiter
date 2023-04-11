using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    public static SceneAudio Instance { get; private set; }
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        _audioSource.Play();
    }
    
    public void PauseAudio()
    {
        _audioSource.Pause();
    }
}
