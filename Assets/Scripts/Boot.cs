using UnityEngine;

public static class Boot
{
    private const string EXECUTION_MANAGER_PATH = "ExecutionManager";
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void ExecuteBoot()
    {
        Object executionManager = Resources.Load(EXECUTION_MANAGER_PATH);
        if (!executionManager)
        {
            Debug.LogError($"Boot - ExecuteBoot - Couldnt find object named {EXECUTION_MANAGER_PATH} inside a Resource folder");
            return;
        }
        Object executionManagerInstance = Object.Instantiate(executionManager);
        if (!executionManagerInstance)
        {
            Debug.LogError($"Boot - ExecuteBoot - {EXECUTION_MANAGER_PATH} instance is null");
            return;
        }
        Object.DontDestroyOnLoad(executionManagerInstance);
    }
}
