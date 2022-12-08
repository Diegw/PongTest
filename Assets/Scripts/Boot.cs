using UnityEngine;

public static class Boot
{
    private const string SETTINGS_MANAGER_PATH = "SettingsManager";
    private const string EXECUTION_MANAGER_PATH = "ExecutionManager";
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void ExecuteBoot()
    {
        InitializeSettings();
        InitializeExecution();
    }

    private static void InitializeSettings()
    {
        Object settingsManager = Resources.Load(SETTINGS_MANAGER_PATH);
        if (!settingsManager)
        {
            Debug.LogError($"Boot - InitializeSettings - Couldnt find object named {SETTINGS_MANAGER_PATH} inside a Resource folder");
            return;
        }
        Object settingsManagerInstance = Object.Instantiate(settingsManager);
        if (!settingsManagerInstance)
        {
            Debug.LogError($"Boot - InitializeSettings - {EXECUTION_MANAGER_PATH} instance is null");
            return;
        }
        Object.DontDestroyOnLoad(settingsManagerInstance);
    }
    
    private static void InitializeExecution()
    {
        Object executionManager = Resources.Load(EXECUTION_MANAGER_PATH);
        if (!executionManager)
        {
            Debug.LogError($"Boot - InitializeExecution - Couldnt find object named {EXECUTION_MANAGER_PATH} inside a Resource folder");
            return;
        }
        Object executionManagerInstance = Object.Instantiate(executionManager);
        if (!executionManagerInstance)
        {
            Debug.LogError($"Boot - InitializeExecution - {EXECUTION_MANAGER_PATH} instance is null");
            return;
        }
        Object.DontDestroyOnLoad(executionManagerInstance);
    }
}
