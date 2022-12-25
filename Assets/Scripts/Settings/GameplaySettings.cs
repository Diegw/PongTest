using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Gameplay", fileName = "GameplayConfiguration")]
public class GameplaySettings : BaseSettings
{
    public PongPlayer PlayerPrefab => _playerPrefab;
    public PongBall BallPrefab => _ballPrefab;
    public int MaxPlayers => _maxPlayers;
    public float PositionX => _positionX;

    [SerializeField] private PongPlayer _playerPrefab = null;
    [SerializeField] private PongBall _ballPrefab = null;
    [SerializeField, Min(0)] private int _maxPlayers = 2;
    [SerializeField, Min(0f)] private float _positionX = 6;
}
