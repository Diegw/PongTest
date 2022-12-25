using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Rounds", fileName = "RoundsConfiguration")]
public class RoundSettings : BaseSettings
{
    public int RoundsToWin => _roundsToWin;
    public int MaxRounds => _maxRounds;
    public float RoundDelay => _roundDelay;

    [SerializeField, Min(0)] private float _roundDelay = 3;
    [SerializeField, Min(0)] private int _roundsToWin = 10;
    [SerializeField, Min(0)] private int _maxRounds = 20;
}
