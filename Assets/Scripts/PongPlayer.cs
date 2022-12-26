using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PongPlayer : MonoBehaviour, IRicochet
{
    [SerializeField] private bool _isLocalPlayer = false;
    private PlayerSettings _settings = null;
    private Transform _thisTransform = null;
    private Camera _camera = null;
    private PongBall _ball = null;
    private bool _clickHolding = false;

    private void Awake()
    {
        _thisTransform = transform;
        _settings = SettingsManager.GetSettings<PlayerSettings>();
        if (_settings)
        {
            _thisTransform.localScale = _settings.PlayerScale;
        }
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Start()
    {
        _camera = Camera.main;
        _ball = FindObjectOfType<PongBall>();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameManager.OnGameStateChangedEvent += CheckToUnsubscribeEvents;
        RoundManager.OnFinishEvent += OnNextRound;
        InputReceiver.OnClickInputEvent += OnClickInput;
        InputReceiver.OnMoveInputEvent += OnMoveInputChanged;
    }

    private void UnsubscribeEvents()
    {
        GameManager.OnGameStateChangedEvent -= CheckToUnsubscribeEvents;
        RoundManager.OnFinishEvent -= OnNextRound;
        InputReceiver.OnClickInputEvent -= OnClickInput;
        InputReceiver.OnMoveInputEvent -= OnMoveInputChanged;
    }

    private void CheckToUnsubscribeEvents(GameManager.EGameState state)
    {
        if (state == GameManager.EGameState.FINISHED)
        {
            UnsubscribeEvents();
        }
        StopCoroutine(ClickCoroutine());
    }

    public void SetLocalPlayer(bool newState)
    {
        _isLocalPlayer = newState;
        if (_isLocalPlayer)
        {
            _thisTransform.AddComponent<InputReceiver>();
        }
        else
        {
            _thisTransform.AddComponent<BotPlayer>();
        }
    }

    private void OnNextRound(RoundManager.SRoundInfo roundInfo)
    {
        _thisTransform.position = new Vector3(_thisTransform.position.x, 0, 0);
    }

    private void OnClickInput(bool isHolding)
    {
        if (!_isLocalPlayer)
        {
            return;
        }

        _clickHolding = isHolding;
        if (isHolding)
        {
            StartCoroutine(ClickCoroutine());
        }
    }
    
    private IEnumerator ClickCoroutine()
    {
        if (!_camera || !_ball)
        {
            yield break;
        }

        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (_clickHolding)
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            float deltaY = mousePosition.y - _thisTransform.position.y;
            if (deltaY != 0)
            {
                deltaY = Mathf.Abs(deltaY) > 1 ? Mathf.Sign(deltaY) : deltaY;
                Vector2 direction = new Vector2(0, deltaY);
                TryToMove(direction);
            }
            yield return frame;
        }
    }

    private void OnMoveInputChanged(Vector2 moveDirection)
    {
        if (!_isLocalPlayer)
        {
            return;
        }
        if (!_isLocalPlayer ||  moveDirection.y == 0 || _clickHolding)
        {
            return;
        }
        TryToMove(moveDirection);
    }

    private void TryToMove(Vector2 moveDirection)
    {
        float speed = _settings ? _settings.MovementSpeed : 1.0f;
        Vector3 translation = new Vector3(0, moveDirection.y, 0) * (Time.deltaTime * speed);
        Move(translation);
    }

    private void Move(Vector3 translation)
    {
        if (HasReachLimit(translation.y))
        {
            float limitY = _thisTransform.position.y;
            if (_settings)
            {
                limitY = translation.y > 0 ? _settings.MovementLimits.y : _settings.MovementLimits.x;
            }
            Vector3 newPosition = new Vector3(_thisTransform.position.x, limitY, 0);
            _thisTransform.position = newPosition;
        }
        else
        {
            _thisTransform.Translate(translation);
        }
    }

    private bool HasReachLimit(float directionY)
    {
        if (directionY > 0 && _thisTransform.position.y + directionY <= _settings.MovementLimits.y)
        {
            return false;
        }
        if (directionY < 0 && _thisTransform.position.y + directionY >= _settings.MovementLimits.x)
        {
            return false;
        }
        return true;
    }

    public bool CanRicochet()
    {
        return true;
    }
}
