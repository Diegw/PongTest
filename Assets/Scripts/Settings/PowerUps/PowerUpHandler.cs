using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    public static event Action OnTriggeredEvent;
    
    [SerializeField] private List<IPowerUp> _components;
    [SerializeField] private BoxCollider2D _boxCollider = null;

    private void Awake()
    {
        if (!_boxCollider)
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChangedEvent += CheckPowerUps;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChangedEvent -= CheckPowerUps;
    }

    private void CheckPowerUps(GameManager.EGameState gameState)
    {
        if (gameState == GameManager.EGameState.STARTED)
        {
            AddPowerUpComponent<PadSizePowerUp>();
            AddPowerUpComponent<BallSpeedPowerUp>();
        }
        if (gameState == GameManager.EGameState.FINISHED)
        {
            foreach (IPowerUp component in _components)
            {
                // component.enabled = false;
            }
        }
    }

    private void AddPowerUpComponent<T>() where T : Component
    {
        T component = gameObject.AddComponent<T>();
        if (_components == null)
        {
            _components = new List<IPowerUp>();
        }

        IPowerUp powerUp = component.GetComponent<IPowerUp>();
        if (_components.Contains(powerUp))
        {
            return;
        }
        _components.Add(powerUp);
    }
    
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        PongBall ball = collider2d.GetComponent<PongBall>();
        if (ball)
        {
            TriggerPowerUp();
        }
    }
    
    private void TriggerPowerUp()
    {
        OnTriggeredEvent?.Invoke();
    }
}
