using System;
using UnityEngine;
using UnityEngine.Serialization;

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
    public float ColliderSize => _colliderSize;
    
    [FormerlySerializedAs("_size")] [SerializeField, Min(0f)] private float _colliderSize = 1f;
    [SerializeField] private SPowerUp[] _powerUps;

    public T GetPowerUpSetting<T>(int componentId) where T : BasePowerUpSettings
    {
        T powerUpSetting = null;
        foreach (SPowerUp powerUp in _powerUps)
        {
            if (powerUp.Id == componentId)
            {
                powerUpSetting = powerUp.Settings as T;
                break;
            }
        }
        return powerUpSetting;
    }
}
