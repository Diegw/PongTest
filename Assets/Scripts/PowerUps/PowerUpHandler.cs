using System.Collections;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private BasePowerUp[] _powerUps;
    [SerializeField] private BoxCollider2D _boxCollider = null;
    private PowerUpSettings _settings = null;
    private PongBall _ball = null;

    private void Awake()
    {
        _settings = SettingsManager.GetSettings<PowerUpSettings>();
        SetCollider();
        if (_settings)
        {
            transform.localScale = new Vector2(_settings.Size, _settings.Size);
        }
    }

    private void SetCollider()
    {
        if (!_boxCollider)
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }
        if (_boxCollider && _settings)
        {
            _boxCollider.enabled = false;
            _boxCollider.isTrigger = true;
        }
    }
    
    private void OnEnable()
    {
        GameManager.OnGameStateChangedEvent += CheckPowerUps;
        RoundManager.OnStartEvent += OnRoundStart;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChangedEvent -= CheckPowerUps;
        RoundManager.OnStartEvent -= OnRoundStart;
    }

    private void CheckPowerUps(GameManager.EGameState gameState)
    {
        if (gameState == GameManager.EGameState.STARTED)
        {
            _powerUps = GetComponents<BasePowerUp>();
            StartCoroutine(Movement());
        }
        if (gameState == GameManager.EGameState.FINISHED)
        {
            _boxCollider.enabled = false;
        }
    }

    private IEnumerator Movement()
    {
        if (!_settings || _settings.MovementRange == Vector2.zero)
        {
            yield break;
        }
        float speed = _settings.Speed * Time.deltaTime;
        float sign = 1;
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (this)
        {
            if (transform.position.y > _settings.MovementRange.y)
            {
                sign = -1;
            }
            else if(transform.position.y < _settings.MovementRange.x)
            {
                sign = 1;
            }
            transform.position += Vector3.up * (speed * sign);
            yield return frame;
        }
    }

    private void OnRoundStart()
    {
        if (!_ball)
        {
            _ball = FindObjectOfType<PongBall>();
        }
        if (_boxCollider)
        {
            _boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        PongBall ball = collider2d.GetComponent<PongBall>();
        if (ball)
        {
            TriggerPowerUps();
        }
    }
    
    private void TriggerPowerUps()
    {
        foreach (BasePowerUp powerUp in _powerUps)
        {
            powerUp.Trigger(_settings, _ball);
        }
    }
}
