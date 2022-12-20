using Unity.VisualScripting;
using UnityEngine;

public class PongPlayer : MonoBehaviour, IRicochet
{
    [SerializeField] private bool _isLocalPlayer = false;
    private PlayerSettings _settings = null;
    private Transform _thisTransform = null;

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
        InputReceiver.OnMoveInputEvent += OnMoveInputChanged;
        RoundManager.OnRoundsFinishedEvent += OnNextRound;
    }

    private void OnNextRound(RoundManager.SRoundInfo roundInfo)
    {
        _thisTransform.position = new Vector3(_thisTransform.position.x, 0, 0);
    }

    private void OnDisable()
    { 
        InputReceiver.OnMoveInputEvent -= OnMoveInputChanged;
        RoundManager.OnRoundsFinishedEvent -= OnNextRound;
    }

    public void SetLocalPlayer(bool newState)
    {
        _isLocalPlayer = newState;
        if (!_isLocalPlayer)
        {
            _thisTransform.AddComponent<BotPlayer>();
        }
    }
    
    private void OnMoveInputChanged(Vector2 moveDirection)
    {
        if (!_isLocalPlayer ||  moveDirection.y == 0)
        {
            return;
        }
        float speed = _settings ? _settings.MovementSpeed : 1.0f;
        int directionY = Mathf.FloorToInt(moveDirection.y);
        Vector3 translation = new Vector3(0, directionY, 0) * (Time.deltaTime * speed);
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
