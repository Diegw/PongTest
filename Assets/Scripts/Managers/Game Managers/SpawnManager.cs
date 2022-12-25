using UnityEngine;

public class SpawnManager : MonoBehaviour, IManager
{
    private GameplaySettings _settings = null;

    public void Construct()
    {
    }

    public void Activate()
    {
        GameManager.OnGameStateChangedEvent += OnGameStateChanged;
    }

    public void Initialize()
    {
        _settings = SettingsManager.GetSettings<GameplaySettings>();
        SpawnPlayers();
    }

    public void Deactivate()
    {
        GameManager.OnGameStateChangedEvent -= OnGameStateChanged;
    }

    public void Terminate()
    {
    }
    
    private void SpawnPlayers()
    {
        if (!_settings || !_settings.PlayerPrefab)
        {
            return;
        }

        for (int i = 0; i < _settings.MaxPlayers; i++)
        {
            bool isRight = i % 2 == 0;
            float positionX = isRight ? _settings.PositionX : -_settings.PositionX;
            PongPlayer player = Instantiate(_settings.PlayerPrefab, new Vector3(positionX, 0, 0), Quaternion.identity);
            player.SetLocalPlayer(isRight);
        }
    }

    private void OnGameStateChanged(GameManager.EGameState gameState)
    {
        if (gameState != GameManager.EGameState.STARTED)
        {
            return;
        }
        SpawnBall();
    }

    private void SpawnBall()
    {
        if (!_settings || !_settings.BallPrefab)
        {
            return;
        }
        
        PongBall ball = Instantiate(_settings.BallPrefab, Vector3.zero, Quaternion.identity);
    }
}
