using UnityEngine;

public class InputManager : MonoBehaviour, IManager
{
    private InputSettings _settings = null;
    
    public void Construct()
    {
        _settings = SettingsManager.GetSettings<InputSettings>();
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
