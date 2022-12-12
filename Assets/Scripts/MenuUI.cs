using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public event Action OnPlayButtonEvent;
    public event Action OnExitButtonEvent;
    public event Action<EDifficulty> OnDifficultyChangedEvent;
    
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _difficultyButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _easyButton;
    [SerializeField] private GameObject _principalUI;
    [SerializeField] private GameObject _difficultyUI;

    private void Awake()
    {
        StartCoroutine(CheckButtonSelected());
    }

    private IEnumerator CheckButtonSelected()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.0f);
        while (true)
        {
            yield return waitForSeconds;
            if (!EventSystem.current.currentSelectedGameObject)
            {
                SetButtonSelected();
            }
        }
    }

    private void OnEnable()
    {
        AddButtonListener(_playButton, PlayButton);
        AddButtonListener(_difficultyButton, DifficultyButton);
        AddButtonListener(_exitButton, ExitButton);
    }

    private void OnDisable()
    {
        RemoveButtonListener(_playButton, PlayButton);
        RemoveButtonListener(_difficultyButton, DifficultyButton);
        RemoveButtonListener(_exitButton, ExitButton); 
    }

    private void AddButtonListener(Button button, UnityAction action)
    {
        if (button)
        {
            button.onClick.AddListener(action);
        }
    }
    
    private void RemoveButtonListener(Button button, UnityAction action)
    {
        if (button)
        {
            button.onClick.AddListener(action);
        }
    }

    private void PlayButton()
    {
        OnPlayButtonEvent?.Invoke();
    }

    private void DifficultyButton()
    {
        ToggleUI();
    }

    private void ExitButton()
    {
        OnExitButtonEvent?.Invoke();
    }

    private void ToggleUI()
    {
        if (_principalUI)
        {
            _principalUI.SetActive(!_principalUI.activeSelf);
        }

        if (_difficultyUI)
        {
            _difficultyUI.SetActive(!_difficultyUI.activeSelf);
        }
    }

    private void SetButtonSelected()
    {
        if (_principalUI && _principalUI.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(_playButton.gameObject);
        }
        if (_difficultyUI && _difficultyUI.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(_easyButton.gameObject);
        }
    }

    public void Difficulty(int difficultyIndex)
    {
        EDifficulty difficulty = (EDifficulty)difficultyIndex;
        OnDifficultyChangedEvent?.Invoke(difficulty);
        DifficultyButton();
    }
}
