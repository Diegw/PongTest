using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static event Action OnContinueEvent;
    
    [SerializeField] private MenuUI _menuUI = null;
    [SerializeField] private int _currentSelectable = 0;
    
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

        _currentSelectable = 0;
    }

    private void OnEnable()
    {
        
    }
}
