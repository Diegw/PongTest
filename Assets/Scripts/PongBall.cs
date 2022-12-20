using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PongBall : MonoBehaviour
{
    [SerializeField] private Vector2 _currentVelocity = Vector2.zero; 
    private Rigidbody2D _rigidbody = null;
    private BallSettings _settings = null;
    
    private void Awake()
    {
        _settings = SettingsManager.GetSettings<BallSettings>();
        if (_settings)
        {
            transform.localScale = _settings.Size;
        }
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        StartCoroutine(InitialImpulse());
    }

    private void OnEnable()
    {
        RoundManager.OnRoundsFinishedEvent += OnNextRound;
    }

    private void OnDisable()
    {
        RoundManager.OnRoundsFinishedEvent -= OnNextRound;
    }

    private void OnNextRound(RoundManager.SRoundInfo roundInfo)
    {
        Reset();
        if (roundInfo.HasRoundsFinished)
        {
            return;
        }
        StartCoroutine(InitialImpulse());
    }

    private void Reset()
    {
        SetVelocity(Vector2.zero);
        transform.position = Vector2.zero;
    }
    
    private IEnumerator InitialImpulse()
    {
        yield return null;
        if (!_rigidbody)
        {
            yield break;
        }

        if (_settings)
        {
            yield return new WaitForSeconds(_settings.InitialDelay);
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

    private void SetVelocity(Vector2 newVelocity)
    {
        if (!_rigidbody)
        {
            return;
        }

        float increment = 0.1f;
        if (_settings)
        {
            increment = _settings.SpeedIncrement;
        }
        _currentVelocity = newVelocity + (newVelocity * increment);
        _rigidbody.velocity = _currentVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IRicochet ricochet = collision.gameObject.GetComponent<IRicochet>();
        if (ricochet != null && ricochet.CanRicochet())
        {
            Vector2 normal = collision.GetContact(0).normal;
            Ricochet(normal);
        }
    }

    private void Ricochet(Vector2 normal)
    {
        Vector2 reflect = Vector2.Reflect(_currentVelocity, normal);
        SetVelocity(reflect);
    }
}
