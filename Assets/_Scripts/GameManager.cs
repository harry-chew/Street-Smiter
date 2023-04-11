using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameState _currentGameState;
    [SerializeField] private GameState _previousGameState;


    private void OnEnable()
    {
        CameraTrigger.OnGameStart += HandleGameStart;
        PauseButton.OnPause += HandlePauseButtonPressed;
    }

    private void HandlePauseButtonPressed(bool isPaused)
    {
        SetGameState(GameState.Paused);
        PauseGame(isPaused);
    }

    private void PauseGame(bool isPaused)
    {
        if(isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
    }

    private void HandleGameStart()
    {
        SetGameState(GameState.InGame);
    }

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
        _currentGameState = GameState.Start;
    }
    
    public void SetGameState(GameState gameState, bool isPreviousState = false)
    {
        if (isPreviousState)
        {
            _previousGameState = gameState;
        }
        else
        {
            _previousGameState = _currentGameState;
            _currentGameState = gameState;
        }
    }

    public GameState GetGameState()
    {
        return _currentGameState;
    }
}