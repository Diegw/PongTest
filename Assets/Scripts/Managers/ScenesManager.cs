using UnityEngine;

public class ScenesManager : MonoBehaviour, IManager
{
    private ScenesSettings _settings = null;
    
    public void Construct()
    {
        _settings = SettingsManager.GetSettings<ScenesSettings>();
    }

    public void Activate()
    {
    }

    public void Initialize()
    {
    }

    public void Deactivate()
    {
    }

    public void Terminate()
    {
    }
}
