using System;
using UnityEngine;

[Serializable] public class BallSpeedPowerUp : BasePowerUp
{
    public override void Trigger(PowerUpSettings settings, PongBall ball)
    {
        base.Trigger(settings, ball);
        Debug.Log("ball speed");
    }
}
