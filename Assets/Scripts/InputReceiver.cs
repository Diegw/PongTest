using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    public static event Action<Vector2> OnMoveInputEvent;

    [SerializeField] private bool _inputEnabled = true;
    [SerializeField] private PlayerInput _playerInput = null;
    private InputAction _moveAction = null;

    private void Awake()
    {
        StartCoroutine(CheckMoveInputCoroutine());
    }

    private IEnumerator CheckMoveInputCoroutine()
    {
        if (!_playerInput)
        {
            yield break;
        }
        _moveAction = _playerInput.currentActionMap.FindAction("Move");
        _moveAction.performed += MoveActionOnperformed;
        _playerInput.ActivateInput();
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (_inputEnabled)
        {
            CheckMoveInput();
            yield return frame;
        }
    }

    private void MoveActionOnperformed(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<Vector2>() == Vector2.zero)
        {
            return;
        }

        // Debug.Log(obj.ReadValue<Vector2>());
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
