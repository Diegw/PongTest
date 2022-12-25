using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour, IManager
{
    private ScenesSettings _settings = null;

    public void Construct()
    {
        _settings = SettingsManager.GetSettings<ScenesSettings>();
    }

    public void Activate()
    {
        ExecutionManager.OnContinueEvent += NextScene;
        Title.OnContinueEvent += NextScene;
        Menu.OnContinueEvent += NextScene;
        GameplayUI.OnButtonPressedEvent += GoToScene;
    }

    public void Initialize()
    {
    }

    public void Deactivate()
    {
        ExecutionManager.OnContinueEvent -= NextScene;
        Title.OnContinueEvent -= NextScene;
        Menu.OnContinueEvent -= NextScene;
        GameplayUI.OnButtonPressedEvent -= GoToScene;
    }

    private void GoToScene(int newSceneIndex)
    {
        LoadScene(newSceneIndex);
    }

    public void Terminate()
    {
        _settings = null;
    }

    private void NextScene()
    {
        if (!_settings)
        {
            return;
        }
        string nextSceneName = _settings.GetNextSceneName(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetSceneByName(nextSceneName).isLoaded)
        {
            return;
        } 
        LoadScene(nextSceneName);
    }

    private static void LoadScene(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }
    
    private static void LoadScene(int nextSceneIndex)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
