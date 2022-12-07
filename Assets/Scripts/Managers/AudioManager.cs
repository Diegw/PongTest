using UnityEngine;

public class AudioManager : MonoBehaviour, IManager
{
    private AudioSettings _settings = null;
    
    public void Construct()
    {
        _settings = SettingsManager.GetSettings<AudioSettings>();
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
