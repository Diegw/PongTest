using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Scenes", fileName = "ScenesConfiguration")]
public class ScenesSettings : BaseSettings
{
    [SerializeField] private string[] _nextScenes = null;
    [SerializeField] private int[] _scenesIndex = null;

    public enum EScene
    {
        INITIAL = 0,
        TITLE = 1,
        MENU = 2,
        GAMEPLAY = 3,
    }
    
    public string GetNextSceneName(int currentSceneIndex)
    {
        string nextSceneName = "";
        if (_nextScenes != null && currentSceneIndex < _nextScenes.Length)
        {
            nextSceneName = _nextScenes[currentSceneIndex];
        }
        return nextSceneName;
    }

    public int GetSceneBuildIndex(EScene scene)
    {
        int sceneIndex = (int)scene;
        if (sceneIndex < _scenesIndex.Length)
        {
            sceneIndex = _scenesIndex[sceneIndex];
        }
        return sceneIndex;
    }
}
