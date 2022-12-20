using System.Collections;
using UnityEngine;

public class BotPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private PongBall _ball = null;
    private BotSettings _settings = null;

    private void Awake()
    {
        _settings = SettingsManager.GetSettings<BotSettings>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChangedEvent += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChangedEvent -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.EGameState gameState)
    {
        if (gameState != GameManager.EGameState.STARTED)
        {
            return;
        }

        if (_settings && !_settings.DisableBot)
        {
            StartCoroutine(InitializeBot());
        }
    }

    private IEnumerator InitializeBot()
    {
        _ball = FindObjectOfType<PongBall>();
        if (!_ball || !_settings)
        {
            yield break;
        }

        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (_ball)
        {
            _speed = _settings ? _settings.MovementSpeed : 1.0f;
            _speed *= Time.deltaTime;
            float velocityY = _ball.transform.position.y - transform.position.y;
            velocityY = Mathf.Clamp(Mathf.Abs(velocityY), 0, _speed) * Mathf.Sign(velocityY);
            if (velocityY != 0)
            {
                Vector3 translation = new Vector3(0, velocityY, 0);
                Move(translation);
            }
            yield return frame;
        }
    }

    private void Move(Vector3 translation)
    {
        Transform thisTransform = transform;
        if (HasReachLimit(translation.y))
        {
            float limitY = thisTransform.position.y;
            if (_settings)
            {
                limitY = translation.y > 0 ? _settings.MovementLimits.y : _settings.MovementLimits.x;
            }
            Vector3 newPosition = new Vector3(thisTransform.position.x, limitY, 0);
            thisTransform.position = newPosition;
        }
        else
        {
            thisTransform.Translate(translation);
        }
    }

    private bool HasReachLimit(float directionY)
    {
        if (directionY > 0 && transform.position.y + directionY <= _settings.MovementLimits.y)
        {
            return false;
        }
        if (directionY < 0 && transform.position.y + directionY >= _settings.MovementLimits.x)
        {
            return false;
        }
        return true;
    }
}
