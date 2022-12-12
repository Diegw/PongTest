using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Scenes", fileName = "ScenesConfiguration")]
public class ScenesSettings : BaseSettings
{
    [SerializeField] private string[] _nextScenes = null;

    public string GetNextSceneName(int currentSceneIndex)
    {
        string nextSceneName = "";
        if (_nextScenes != null && currentSceneIndex < _nextScenes.Length)
        {
            nextSceneName = _nextScenes[currentSceneIndex];
        }
        return nextSceneName;
    }
}
