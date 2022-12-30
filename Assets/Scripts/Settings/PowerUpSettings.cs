using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/PowerUp", fileName = "PowerUpConfiguration")]
public class PowerUpSettings : BaseSettings
{
    [Serializable] private struct SPowerUp
    {
        public int Id => _id;
        public BasePowerUpSettings Settings => _settings;
        
        [SerializeField] private int _id;
        [SerializeField] private BasePowerUpSettings _settings;
    }
    public float Size => _size;
    public float Speed => _speed;
    public Vector2 MovementRange => _movementRange;
    
    [SerializeField, Min(0f)] private float _size = 1f;
    [SerializeField, Min(0f)] private float _speed = 1f;
    [SerializeField] private Vector2 _movementRange = Vector2.zero;
    [SerializeField] private SPowerUp[] _powerUps;

    public T GetPowerUpSetting<T>(int componentId) where T : BasePowerUpSettings
    {
        T powerUpSetting = null;
        foreach (SPowerUp powerUp in _powerUps)
        {
            if (powerUp.Id == componentId)
            {
                powerUpSetting = (T)powerUp.Settings;
                break;
            }
        }
        return powerUpSetting;
    }
}
