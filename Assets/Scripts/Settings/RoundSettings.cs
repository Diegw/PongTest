using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Rounds", fileName = "RoundsConfiguration")]
public class RoundSettings : BaseSettings
{
    public int RoundsToWin => _roundsToWin;
    public int MaxRounds => _maxRounds;
    
    [SerializeField] private int _roundsToWin = 10;
    [SerializeField] private int _maxRounds = 20;
}
