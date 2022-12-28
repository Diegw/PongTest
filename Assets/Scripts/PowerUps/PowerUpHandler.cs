using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private BasePowerUp[] _powerUps;
    [SerializeField] private BoxCollider2D _boxCollider = null;
    private PowerUpSettings _settings = null;
    private PongBall _ball = null;

    private void Awake()
    {
        _ball = FindObjectOfType<PongBall>();
        _settings = SettingsManager.GetSettings<PowerUpSettings>();
        SetCollider();
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
            _boxCollider.size = new Vector2(_settings.ColliderSize, _settings.ColliderSize);
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
            AddPowerUpComponents();
        }
        if (gameState == GameManager.EGameState.FINISHED)
        {
            _boxCollider.enabled = false;
        }
    }

    private void OnRoundStart()
    {
        if (_boxCollider)
        {
            _boxCollider.enabled = true;
        }
    }

    private void AddPowerUpComponents()
    {
        _powerUps = GetComponents<BasePowerUp>();
        if (_powerUps == null)
        {
            return;
        }
        foreach (BasePowerUp powerUp in _powerUps)
        {
            powerUp.Trigger(_settings, _ball);
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
