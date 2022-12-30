using System;
using UnityEngine;

[Serializable] public class BallSpeedPowerUp : BasePowerUp
{
    public static event Action<Vector2, float> OnPowerUpEvent;

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
        OnPowerUpEvent?.Invoke(ball.CurrentVelocity, _settings.Modifier);
    }
}
