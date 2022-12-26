using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Difficulty", fileName = "DifficultyConfiguration")]
public class DifficultySettings : BaseSettings
{
    public enum EDifficulty
    {
        EASY = 0,
        NORMAL = 1,
        HARD = 2,
    }

    [SerializeField] private EDifficulty _currentDifficulty = EDifficulty.EASY;

    public EDifficulty CurrentDifficulty => _currentDifficulty;
    public void SetDifficulty(EDifficulty difficulty)
    {
        _currentDifficulty = difficulty;
    }
}
