using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PongBall : MonoBehaviour
{
    public Vector2 CurrentVelocity => _currentVelocity;
    public float LastPadCollidePosition => _lastPadCollidePosition;
    
    [SerializeField] private Vector2 _currentVelocity = Vector2.zero; 
    [SerializeField] private float _lastPadCollidePosition = 0; 
    private Rigidbody2D _rigidbody = null;
    private BallSettings _settings = null;
    private bool _canMove = true;
    
    private void Awake()
    {
        _settings = SettingsManager.GetSettings<BallSettings>();
        if (_settings)
        {
            transform.localScale = _settings.Size;
        }
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    private void OnEnable()
    {
        RoundManager.OnFinishEvent += OnEndRound;
        RoundManager.OnStartEvent += OnStartRound;
        BallSpeedPowerUp.OnPowerUpEvent += SetVelocity;
    }

    private void OnDisable()
    {
        RoundManager.OnFinishEvent -= OnEndRound;
        RoundManager.OnStartEvent -= OnStartRound;
        BallSpeedPowerUp.OnPowerUpEvent -= SetVelocity;
    }
    
    private void OnEndRound(RoundManager.SRoundInfo roundInfo)
    {
        Reset();
        if (roundInfo.HasRoundsFinished)
        {
            _canMove = false;
        }
    }

    private void Reset()
    {
        SetVelocity(Vector2.zero);
        transform.position = Vector2.zero;
    }

    private void OnStartRound()
    {
        if (!_canMove)
        {
            return;
        }
        StartCoroutine(InitialImpulse());
    }
    
    private IEnumerator InitialImpulse()
    {
        yield return null;
        if (!_rigidbody)
        {
            yield break;
        }

        float speed = 1.0f;
        if (_settings)
        {
            speed = _settings.InitialSpeed;
        }
        Vector2 initialVelocity = new Vector2(1.0f*RandomSign(),0.5f*RandomSign()) * speed;
        SetVelocity(initialVelocity);
    }

    private int RandomSign()
    {
        return Random.value > 0.5f ? 1 : -1;
    }

    private void SetVelocity(Vector2 newVelocity, float modifier = 0f)
    {
        if (!_rigidbody)
        {
            return;
        }

        float increment = modifier;
        if (_settings && modifier == 0f)
        {
            increment = _settings.SpeedIncrement;
        }
        _currentVelocity = newVelocity + (newVelocity * increment);
        _rigidbody.velocity = CurrentVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IRicochet ricochet = collision.gameObject.GetComponent<IRicochet>();
        if (ricochet != null && ricochet.CanRicochet())
        {
            PongPlayer player = collision.gameObject.GetComponent<PongPlayer>();
            if (player)
            {
                _lastPadCollidePosition = player.transform.position.x;
            }
            AudioManager.PlayAudio(1);
            Vector2 normal = collision.GetContact(0).normal;
            Ricochet(normal);
        }
    }

    private void Ricochet(Vector2 normal)
    {
        Vector2 reflect = Vector2.Reflect(CurrentVelocity, normal);
        SetVelocity(reflect);
    }
}
