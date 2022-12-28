using System;
using UnityEngine;

[Serializable] public abstract class BasePowerUp : MonoBehaviour
{
    [SerializeField] protected int _Id = 0;
    
    public virtual void Trigger(PowerUpSettings settings, PongBall ball)
    {
        
    }
}
