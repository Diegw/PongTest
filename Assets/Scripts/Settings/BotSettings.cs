using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bot", fileName = "BotConfiguration")]
public class BotSettings : BaseSettings
{
    [Serializable] public struct SBotData
    {
        public SBotData(float speed, Vector2 limits)
        {
            _movementSpeed = speed;
            _movementLimits = limits;
        }
        public float MovementSpeed => _movementSpeed;
        public Vector2 MovementLimits => _movementLimits;
        
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Vector2 _movementLimits;
    }
    
    public bool DisableBot => _disableBot;
    
    [SerializeField] private bool _disableBot = false;
    [SerializeField] private SBotData _defaultDifficultyData;
    [SerializeField] private SBotData[] _difficultiesData = null;

    public SBotData GetBotData()
    {
        SBotData data = _defaultDifficultyData;
        DifficultySettings difficultySettings = SettingsManager.GetSettings<DifficultySettings>();
        if (difficultySettings)
        {
            int difficultyIndex = (int)difficultySettings.CurrentDifficulty;
            data = _difficultiesData[difficultyIndex];
            Debug.Log(difficultySettings.CurrentDifficulty);
        }
        return data;
    }
}
