using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameplayUI : MonoBehaviour
{
    public static event Action<int> OnButtonPressedEvent;

    [SerializeField] private TMP_Text _roundDisplay = null;
    [SerializeField] private TMP_Text _teamOneDisplay = null;
    [SerializeField] private TMP_Text _teamTwoDisplay = null;
    [SerializeField] private GameObject _endScreen = null;
    [SerializeField] private Button _restartButton = null;
    [SerializeField] private Button _menuButton = null;

    private void Awake()
    {
        SetEndScreen(false);
    }

    private void OnEnable()
    {
        SubscribeButton(_restartButton, Restart);
        SubscribeButton(_menuButton, Menu);
        RoundManager.OnFinishEvent += OnRoundEnd;
    }

    private void OnDisable()
    {
        UnsubscribeButton(_restartButton, Restart);
        UnsubscribeButton(_menuButton, Menu);
        RoundManager.OnFinishEvent -= OnRoundEnd;
    }

    private void SubscribeButton(Button button, UnityAction action)
    {
        if (button)
        {
            button.onClick.AddListener(action);
        }
    }
    
    private void UnsubscribeButton(Button button, UnityAction action)
    {
        if (button)
        {
            button.onClick.RemoveListener(action);
        }
    }

    private void Resume()
    {
        AudioManager.PlayAudio(0);
        SetEndScreen(false);
    }

    private void Restart()
    {
        AudioManager.PlayAudio(0);
        OnButtonPressedEvent?.Invoke(GetSceneBuildIndex(ScenesSettings.EScene.GAMEPLAY));
    }

    private void Menu()
    {
        AudioManager.PlayAudio(0);
        OnButtonPressedEvent?.Invoke(GetSceneBuildIndex(ScenesSettings.EScene.MENU));
    }

    private int GetSceneBuildIndex(ScenesSettings.EScene scene)
    {
        int index = 0;
        ScenesSettings settings = SettingsManager.GetSettings<ScenesSettings>();
        if (settings)
        {
            index = settings.GetSceneBuildIndex(scene);
        }

        return index;
    }
    
    private void OnRoundEnd(RoundManager.SRoundInfo roundInfo)
    {
        StartCoroutine(StartCountdown(roundInfo.Delay));
        if (roundInfo.Team == RoundManager.ETeam.ONE && _teamOneDisplay)
        {
            _teamOneDisplay.text = ScoreText(roundInfo.Rounds);
        }
        if (roundInfo.Team == RoundManager.ETeam.TWO && _teamTwoDisplay)
        {
            _teamTwoDisplay.text = ScoreText(roundInfo.Rounds);
        }
        SetEndScreen(roundInfo.HasRoundsFinished);
    }
    
    private IEnumerator StartCountdown(float time)
    {
        if (!_roundDisplay)
        {
            yield break;
        }
        
        float countdown = time;
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (countdown >= 0)
        {
            string number = countdown <= 0 ? "Go" : countdown.ToString(CultureInfo.InvariantCulture);
            _roundDisplay.text = number;
            yield return wait;
            countdown--;
        }
        _roundDisplay.enabled = false;
    }

    private string ScoreText(int score)
    {
        return $"( {score} )";
    }

    private void SetEndScreen(bool hasFinish)
    {
        StartCoroutine(SetButtonSelected(hasFinish));
        if (!_endScreen || _endScreen.activeSelf == hasFinish)
        {
            return;
        }
        _endScreen.SetActive(hasFinish);
    }

    private IEnumerator SetButtonSelected(bool hasRoundsFinished)
    {
        if (!EventSystem.current)
        {
            yield break;
        }
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(_restartButton.gameObject);
    }
}
