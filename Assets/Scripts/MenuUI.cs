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
    public event Action<DifficultySettings.EDifficulty> OnDifficultyChangedEvent;
    
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
        StartCoroutine(SetButtonSelected());
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return waitForSeconds;
            if (!EventSystem.current.currentSelectedGameObject)
            {
                StartCoroutine(SetButtonSelected());
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
        AudioManager.PlayAudio(0);
        OnPlayButtonEvent?.Invoke();
        StartCoroutine(SetButtonSelected());
    }

    private void DifficultyButton()
    {
        AudioManager.PlayAudio(0);
        ToggleUI();
        StartCoroutine(SetButtonSelected());
    }

    private void ExitButton()
    {
        AudioManager.PlayAudio(0);
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

    private IEnumerator SetButtonSelected()
    {
        if (!EventSystem.current)
        {
            yield break;
        }
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
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
        DifficultySettings.EDifficulty difficulty = (DifficultySettings.EDifficulty)difficultyIndex;
        OnDifficultyChangedEvent?.Invoke(difficulty);
        DifficultyButton();
        StartCoroutine(SetButtonSelected());

    }
}
