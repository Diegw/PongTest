using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public static event Action OnContinueEvent;
    [SerializeField] private InputActionAsset _inputActionAsset = null;
    [SerializeField] private Button _continueButton = null;
    private InputAction _submitAction = null;
    private InputAction _clickAction = null;

    private void Awake()
    {
        if (_inputActionAsset)
        {
            _submitAction = _inputActionAsset.FindAction("Submit");
            _clickAction = _inputActionAsset.FindAction("Click");
        }
    }

    private void OnEnable()
    {
        if (_submitAction != null)
        {
            _submitAction.performed += ContinueEvent;
        }
        AddButtonEventListener(_continueButton, ContinueEvent);
    }

    private void OnDisable()
    {
        if (_submitAction != null)
        {
            _submitAction.performed -= ContinueEvent;
        }
        RemoveButtonEventListener(_continueButton, ContinueEvent);
    }

    private void AddButtonEventListener(Button button, UnityAction action)
    {
        if (!button)
        {
            Debug.LogError($"Title - AddButtonEventListener - Button Reference is null");
            return;
        }
        button.onClick.AddListener(action);
    }

    private void RemoveButtonEventListener(Button button, UnityAction action)
    {
        if (!button)
        {
            return;
        }
        button.onClick.RemoveListener(action);
    }

    private void ContinueEvent(InputAction.CallbackContext context)
    {
        ContinueEvent();
    }
    
    private void ContinueEvent()
    {
        OnContinueEvent?.Invoke();
    }
}
