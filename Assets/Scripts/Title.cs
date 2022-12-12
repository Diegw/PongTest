using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public static event Action OnContinueEvent;
    
    [SerializeField] private Button _continueButton = null;

    private void OnEnable()
    {
        AddButtonEventListener(_continueButton, ContinueEvent);
    }

    private void OnDisable()
    {
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
    
    private void ContinueEvent()
    {
        OnContinueEvent?.Invoke();
    }
}
