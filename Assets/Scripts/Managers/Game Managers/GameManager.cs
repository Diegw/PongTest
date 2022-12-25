using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public static event Action<EGameState> OnGameStateChangedEvent;
    
    public enum EGameState
    {
        PREPARING = 0,
        STARTED = 1,
        FINISHED = 2,
        ENDING = 3,
    }

    [SerializeField] private EGameState _gameState = EGameState.PREPARING;
    
    public void Construct()
    {
    }

    public void Activate()
    {
        RoundManager.OnFinishEvent += CheckEndMatch;
    }

    public void Initialize()
    {
        SetGameState(EGameState.STARTED);
    }

    public void Deactivate()
    {
        RoundManager.OnFinishEvent -= CheckEndMatch;
    }

    private void CheckEndMatch(RoundManager.SRoundInfo roundInfo)
    {
        if (!roundInfo.HasRoundsFinished)
        {
            return;
        }
        SetGameState(EGameState.FINISHED);
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
