using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    public static event Action<Vector2> OnNavigationInputEvent; 
    public static event Action OnSubmitInputEvent; 
    public static event Action OnCancelInputEvent; 

    [SerializeField] private PlayerInput _playerInput = null;

    private void OnEnable()
    {
        SubscribePerformedAction("Navigate", OnNavigateAction);
        SubscribePerformedAction("Submit", OnSubmitAction);
        SubscribePerformedAction("Cancel", OnCancelAction);
    }

    private void OnDisable()
    {
        UnsubscribePerformedAction("Navigate", OnNavigateAction);
        UnsubscribePerformedAction("Submit", OnSubmitAction);
        UnsubscribePerformedAction("Cancel", OnCancelAction);
    }

    private void SubscribePerformedAction(string actionName, Action<InputAction.CallbackContext> actionMethod)
    {
        InputAction action = _playerInput.actions.FindAction(actionName);
        if (action != null)
        {
            action.performed += actionMethod;
        }
    }

    private void UnsubscribePerformedAction(string actionName, Action<InputAction.CallbackContext> actionMethod)
    {
        InputAction action = _playerInput.actions.FindAction(actionName);
        if (action != null)
        {
            action.performed -= actionMethod;
        }
    }
    
    private void OnNavigateAction(InputAction.CallbackContext obj)
    {
        Vector2 navigationDirection = obj.ReadValue<Vector2>();
        if (navigationDirection == Vector2.zero)
        {
            return;
        }
        OnNavigationInputEvent?.Invoke(navigationDirection);
    }

    private void OnSubmitAction(InputAction.CallbackContext obj)
    {
        OnSubmitInputEvent?.Invoke();
    }

    private void OnCancelAction(InputAction.CallbackContext obj)
    {
        OnCancelInputEvent?.Invoke();
    }
}
