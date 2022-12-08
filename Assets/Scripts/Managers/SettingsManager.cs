using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance = null;
    [SerializeField] private string _settingsPath = "Settings";
    [SerializeField] BaseSettings[] _baseSettings = null;
    
    private void Awake()
    {
        _instance = this;
        _baseSettings = Resources.LoadAll<BaseSettings>(_settingsPath);
    }

    public static T GetSettings<T>() where T : BaseSettings
    {
        T settingsClass = null;
        if (_instance)
        {
            foreach (BaseSettings settings in _instance._baseSettings)
            {
                if (settings as T)
                {
                    settingsClass = (T)settings;
                    break;
                }
            }
        }
        return settingsClass;
    }
}
