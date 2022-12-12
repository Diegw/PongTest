using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IManager
{
    private PlayerInputManager _playerInputManager = null;
    private InputSettings _settings = null;
    
    public void Construct()
    {
        _settings = SettingsManager.GetSettings<InputSettings>();
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void Activate()
    {
    }

    public void Initialize()
    {
        ToggleJoinDevices(true);
    }

    public void Deactivate()
    {
    }

    public void Terminate()
    {
        ToggleJoinDevices(false);
    }

    private void ToggleJoinDevices(bool canJoin)
    {
        if (!_playerInputManager)
        {
            return;
        }

        if (canJoin)
        {
            _playerInputManager.EnableJoining();
        }
        else
        {
            _playerInputManager.DisableJoining();
        }
    }
}
