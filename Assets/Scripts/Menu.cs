using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static event Action OnContinueEvent;
    
    [SerializeField] private MenuUI _menuUI = null;
    
    private void Awake()
    {
        if (!_menuUI)
        {
            _menuUI = FindObjectOfType<MenuUI>();
        }
        if (!_menuUI)
        {
            Debug.LogError("Menu - Awake - Cannot find MenuUI Instance");
        }
    }

    private void OnEnable()
    {
        if (_menuUI)
        {
            _menuUI.OnPlayButtonEvent += Continue;
            _menuUI.OnExitButtonEvent += Exit;
        }
    }

    private void OnDisable()
    {
        if (_menuUI)
        {
            _menuUI.OnPlayButtonEvent -= Continue;
            _menuUI.OnExitButtonEvent -= Exit;
        }
    }

    private void Continue()
    {
        OnContinueEvent?.Invoke();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
