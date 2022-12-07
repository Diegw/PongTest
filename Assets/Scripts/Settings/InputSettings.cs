using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Input", fileName = "InputConfiguration")]
public class InputSettings : BaseSettings
{
    public int MaxPlayers => _maxPlayers;
    
    [SerializeField] private int _maxPlayers = 1;
}
