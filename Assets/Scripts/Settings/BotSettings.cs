using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bot", fileName = "BotConfiguration")]
public class BotSettings : BaseSettings
{
    public bool DisableBot => _disableBot;
    public float MovementSpeed => _movementSpeed;
    public Vector2 MovementLimits => _movementLimits;

    [SerializeField] private bool _disableBot = false;
    [SerializeField] private float _movementSpeed = 3.0f;
    [SerializeField] private Vector2 _movementLimits = new Vector2(-4,4);
}
