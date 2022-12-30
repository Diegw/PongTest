using System;
using UnityEngine;

[Serializable] public class PadSizePowerUp : BasePowerUp
{
    public static event Action<float, float> OnPowerUpEvent;
    
    [SerializeField] private FloatModifierSetting _settings = null;
    
    public override void Trigger(PowerUpSettings settings, PongBall ball)
    {
        base.Trigger(settings, ball);
        if (settings)
        {
            _settings = settings.GetPowerUpSetting<FloatModifierSetting>(_id);
        }
        if (!_settings || !ball)
        {
            return;
        }
        OnPowerUpEvent?.Invoke(ball.LastPadCollidePosition, _settings.Modifier);
    }
}
