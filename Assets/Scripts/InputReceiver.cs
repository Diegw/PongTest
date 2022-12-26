using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    public static event Action<Vector2> OnMoveInputEvent;
    public static event Action<bool> OnClickInputEvent;

    [SerializeField] private bool _inputEnabled = true;
    private PlayerInput _playerInput = null;
    private InputAction _moveAction = null;
    private InputAction _clickAction = null;

    private void Awake()
    {
        if (!_playerInput)
        {
            _playerInput = GetComponentInChildren<PlayerInput>();
        }
        StartCoroutine(CheckMoveInputCoroutine());
        FindAction(ref _moveAction, "Move");
        FindAction(ref _clickAction, "Fire");
        if (_playerInput)
        {
            _playerInput.ActivateInput();
        }
    }

    private void FindAction(ref InputAction inputAction, string actionName)
    {
        if (!_playerInput)
        {
            return;
        }
        inputAction = _playerInput.actions.FindAction(actionName);
    }
    
    private void OnEnable()
    {
        if (_clickAction != null)
        {
            _clickAction.performed += OnClick;
            _clickAction.canceled += OnClick;
        }
    }

    private void OnDisable()
    {
        if (_clickAction != null)
        {
            _clickAction.performed -= OnClick;
            _clickAction.canceled -= OnClick;
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        OnClickInputEvent?.Invoke(context.phase == InputActionPhase.Performed);
    }

    private IEnumerator CheckMoveInputCoroutine()
    {
        if (!_playerInput)
        {
            yield break;
        }
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (_inputEnabled)
        {
            CheckMoveInput();
            yield return frame;
        }
    }

    private void CheckMoveInput()
    {
        Vector2 move = Vector2.zero;
        if (_moveAction != null)
        {
            move = _moveAction.ReadValue<Vector2>();
        }
        if (move != Vector2.zero)
        {
            OnMoveInputEvent?.Invoke(move);
        }
    }
}
