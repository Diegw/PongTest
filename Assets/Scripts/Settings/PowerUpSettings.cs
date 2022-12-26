using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/PowerUp", fileName = "PowerUpConfiguration")]
public class PowerUpSettings : BaseSettings
{
    [SerializeField] private BasePowerUpSettings[] _powerUpSettings = null;
    
    public T GetPowerUpSetting<T>(int componentId) where T : BasePowerUpSettings
    {
        T powerUpSetting = null;
        if (_powerUpSettings != null)
        {
            foreach (BasePowerUpSettings basePowerUpSettings in _powerUpSettings)
            {
                if (basePowerUpSettings.ComponentId == componentId)
                {
                    powerUpSetting = basePowerUpSettings as T;
                    break;
                }
            }
        }
        return powerUpSetting;
    }
}
