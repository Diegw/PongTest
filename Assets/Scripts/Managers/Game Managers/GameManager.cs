using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public static event Action<EGameState> OnGameStateChangedEvent;
    
    public enum EGameState
    {
        PREPARING = 0,
        STARTED = 1,
        ENDING = 2,
        FINISHED = 3,
    }

    [SerializeField] private EGameState _gameState = EGameState.PREPARING;
    
    public void Construct()
    {
    }

    public void Activate()
    {
    }

    public void Initialize()
    {
        SetGameState(EGameState.STARTED);
    }

    public void Deactivate()
    {
    }

    public void Terminate()
    {
    }

    private void SetGameState(EGameState gameState)
    {
        _gameState = gameState;
        OnGameStateChangedEvent?.Invoke(gameState);
    }
}
