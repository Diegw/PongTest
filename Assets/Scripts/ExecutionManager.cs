using UnityEngine;

public class ExecutionManager : MonoBehaviour
{
    [SerializeField] private IManager[] _managers = null;

    private void Awake()
    {
        _managers = GetComponentsInChildren<IManager>();
        if (_managers is not { Length: > 0 })
        {
            Debug.LogError($"ExecutionManager - Awake - Couldnt find any managers in ExecutionManager Prefab");
            return;
        }
        ConstructManagers();
    }

    private void OnEnable()
    {
        ActivateManagers();
    }

    private void Start()
    {
        InitializeManagers();
    }

    private void OnDisable()
    {
        DeactivateManagers();
    }

    private void OnDestroy()
    {
        TerminateManagers();
    }

    private void ConstructManagers()
    {
        foreach (IManager manager in _managers)
        {
            manager.Construct();
        }
    }

    private void ActivateManagers()
    {
        foreach (IManager manager in _managers)
        {
            manager.Activate();
        }
    }

    private void InitializeManagers()
    {
        foreach (IManager manager in _managers)
        {
            manager.Initialize();
        }
    }

    private void DeactivateManagers()
    {
        foreach (IManager manager in _managers)
        {
            manager.Deactivate();
        }
    }

    private void TerminateManagers()
    {
        foreach (IManager manager in _managers)
        {
            manager.Terminate();
        }
    }
}
