using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour ,IManager, IRicochet
{
    public static event Action<SRoundInfo> OnRoundsFinishedEvent;

    public enum ETeam
    {
        NONE = 0,
        ONE = 1,
        TWO = 2,
    }

    public struct SRoundInfo
    {
        public SRoundInfo(bool finished, ETeam team, int rounds)
        {
            HasRoundsFinished = finished;
            Team = team;
            Rounds = rounds;
        }

        public bool HasRoundsFinished { get; }
        public ETeam Team { get; }
        public int Rounds { get; }
    }
    
    [SerializeField] private int _currentRound = 0;
    private Dictionary<ETeam, int> _roundsWon = new Dictionary<ETeam, int>();
    private RoundSettings _settings = null;
    
    public void Construct()
    {
        _settings = SettingsManager.GetSettings<RoundSettings>();
    }

    public void Activate()
    {
        GameManager.OnGameStateChangedEvent += OnGameStateChanged;
    }

    public void Initialize()
    {
        
    }

    public void Deactivate()
    {
        GameManager.OnGameStateChangedEvent -= OnGameStateChanged;
    }

    public void Terminate()
    {
    }

    private void OnGameStateChanged(GameManager.EGameState gameState)
    {
        if (gameState != GameManager.EGameState.STARTED)
        {
            return;
        }
        NextRound();
    }

    private void NextRound(ETeam team = ETeam.NONE)
    {
        _currentRound++;
        int rounds = _roundsWon.ContainsKey(team) ? _roundsWon[team] : 0;
        SRoundInfo roundInfo = new SRoundInfo(_currentRound > _settings.MaxRounds, team, rounds);
        OnRoundsFinishedEvent?.Invoke(roundInfo);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PongBall ball = other.GetComponent<PongBall>();
        if (ball)
        {
            ETeam team = RoundWinner(other.transform.position.x);
            NextRound(team);
        }
    }

    private ETeam RoundWinner(float positionLeft)
    {
        ETeam team = positionLeft > 0 ? ETeam.ONE : ETeam.TWO;
        if (_roundsWon != null)
        {
            if (!_roundsWon.ContainsKey(team))
            {
                _roundsWon.Add(team, 1);
            }
            else
            {
                _roundsWon[team]++;
            }
        }

        return team;
    }

    public bool CanRicochet()
    {
        return true;
    }
}
