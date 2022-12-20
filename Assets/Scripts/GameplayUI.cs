using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _teamOneDisplay = null;
    [SerializeField] private TMP_Text _teamTwoDisplay = null;

    private void OnEnable()
    {
        RoundManager.OnRoundsFinishedEvent += OnRoundEnd;
    }

    private void OnDisable()
    {
        RoundManager.OnRoundsFinishedEvent -= OnRoundEnd;
    }

    private void OnRoundEnd(RoundManager.SRoundInfo roundInfo)
    {
        if (roundInfo.Team == RoundManager.ETeam.ONE && _teamOneDisplay)
        {
            _teamOneDisplay.text = ScoreText(roundInfo.Rounds);
        }

        if (roundInfo.Team == RoundManager.ETeam.TWO && _teamTwoDisplay)
        {
            _teamTwoDisplay.text = ScoreText(roundInfo.Rounds);
        }
    }

    private string ScoreText(int score)
    {
        return $"( {score} )";
    }
}
