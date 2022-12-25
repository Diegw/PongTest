using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Player", fileName = "PlayerConfiguration")]
public class PlayerSettings : BaseSettings
{
    public float MovementSpeed => _movementSpeed;
    public Vector2 MovementLimits => _movementLimits;
    public Vector2 PlayerScale => _playerScale;
    
    [SerializeField, Min(1.0f)] private float _movementSpeed = 1f;
    [SerializeField] private Vector2 _movementLimits = Vector2.zero;
    [SerializeField, Min(0f)] private Vector2 _playerScale = Vector2.zero;
}
