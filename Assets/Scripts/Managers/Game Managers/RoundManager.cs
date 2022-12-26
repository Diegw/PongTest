using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour ,IManager, IRicochet
{
    public static event Action<SRoundInfo> OnFinishEvent;
    public static event Action OnStartEvent;

    public enum ETeam
    {
        NONE = 0,
        ONE = 1,
        TWO = 2,
    }

    public struct SRoundInfo
    {
        public SRoundInfo(bool finished, ETeam team, int rounds, float delay = 0)
        {
            HasRoundsFinished = finished;
            Team = team;
            Rounds = rounds;
            Delay = delay;
        }

        public bool HasRoundsFinished { get; }
        public ETeam Team { get; }
        public int Rounds { get; }
        public float Delay { get; }
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

    private void NextRound(ETeam team = ETeam.NONE)
    {
        _currentRound++;
        int rounds = _roundsWon.ContainsKey(team) ? _roundsWon[team] : 0;
        float delay = _settings == null ? 0 : _settings.RoundDelay;
        bool hasFinish = HasFinish();
        SRoundInfo roundInfo = new SRoundInfo(hasFinish, team, rounds, delay);
        if (hasFinish)
        {
            int audioIndex = 2;
            if (GetWinnerTeam() == ETeam.ONE)
            {
                audioIndex = 3;
            }
            AudioManager.PlayAudio(audioIndex);
        }
        OnFinishEvent?.Invoke(roundInfo);
        StartCoroutine(NextRoundDelay(delay));
    }

    private ETeam GetWinnerTeam()
    {
        int roundsWon = 0;
        ETeam team = ETeam.NONE;
        foreach (KeyValuePair<ETeam,int> round in _roundsWon)
        {
            if (round.Value > roundsWon)
            {
                team = round.Key;
            }
        }
        return team;
    }

    private IEnumerator NextRoundDelay(float delay)
    {
        yield return new WaitForSeconds(delay + 1f);
        OnStartEvent?.Invoke();
    }

    private bool HasFinish()
    {
        bool hasFinish = false;
        hasFinish = _currentRound > _settings.MaxRounds;
        if (!hasFinish)
        {
            foreach (int rounds in _roundsWon.Values)
            {
                if (rounds >= _settings.RoundsToWin)
                {
                    hasFinish = true;
                    break;
                }
            }
        }
        return hasFinish;
    }

    public bool CanRicochet()
    {
        return true;
    }
}
