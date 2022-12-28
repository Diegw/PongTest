using System;
using UnityEngine;

[Serializable] public class PadSizePowerUp : BasePowerUp
{
    public override void Trigger(PowerUpSettings settings, PongBall ball)
    {
        base.Trigger(settings, ball);
        Debug.Log("pad size");
    }
}
