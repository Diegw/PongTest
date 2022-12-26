using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PongBall : MonoBehaviour
{
    [SerializeField] private Vector2 _currentVelocity = Vector2.zero; 
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
    }

    private void OnDisable()
    {
        RoundManager.OnFinishEvent -= OnEndRound;
        RoundManager.OnStartEvent -= OnStartRound;
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
            AudioManager.PlayAudio(1);
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
