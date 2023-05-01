using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public static event EventHandler OnGameStart;

    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    public GameState state;
    GameState previousState;

    [Tooltip("Reference to UIPause object to access Pause and Unpause events")]
    [SerializeField] UIPause uiPause;
    [Tooltip("How long the game will run")]
    [SerializeField] float gameplayTimeMax;

    float gameplayTimer = 0f;

    void Awake()
    { Instance = this;

        state = GameState.Gameplay;
    }

    void Start()
    {
        uiPause.OnPaused += UIPause_OnPaused;
        uiPause.OnUnpaused += UIPause_OnUnpaused;
        OnGameStart?.Invoke(this, EventArgs.Empty);
        state = GameState.Gameplay;
        Time.timeScale = 1f;
    }

    void Update()
    {
        switch(state)
        {
            
            case GameState.Gameplay:
            gameplayTimer += Time.deltaTime;
            if(gameplayTimer >= gameplayTimeMax)
            {
                gameplayTimer = 0f;
                state = GameState.GameOver;
            }
            break;
        }
    }

    void UIPause_OnPaused(object sender, EventArgs e)
    {
        previousState = state;
        state = GameState.Paused;
        Time.timeScale = 0f;
    }

    void UIPause_OnUnpaused(object sender, EventArgs e)
    {
        state = previousState;
        Time.timeScale = 1f;
    }

    public float GetGameplayTimer()
    {
        return gameplayTimeMax - gameplayTimer;
    }

    public static void ResetStaticData()
    {
        OnGameStart = null;
        Time.timeScale = 1f;
    }
}
