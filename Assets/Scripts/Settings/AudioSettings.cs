using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Configuration/Audio", fileName = "AudioConfiguration")]
public class AudioSettings : BaseSettings
{
    public int MaxAudioSources => _maxAudioSources;
    public float DisableDelay => _disableDelay;
    public AudioSource AudioSourcePrefab => _audioSourcePrefab;

    [SerializeField, Min(0)] private int _maxAudioSources = 10;
    [SerializeField, Min(0)] private float _disableDelay = 2f;
    [SerializeField] private AudioSource _audioSourcePrefab = null;
    [SerializeField] private SAudioClip[] _audioClips = null;

    [Serializable] private struct SAudioClip
    {
        [SerializeField] private AudioClip[] _audioClips;

        public AudioClip GetRandomAudioClip()
        {
            AudioClip audioClip = null;
            if (_audioClips == null)
            {
                return null;
            }
            int randomIndex = Random.Range(0, _audioClips.Length);
            audioClip = _audioClips[randomIndex];
            return audioClip;
        }
    }
    
    public AudioClip GetAudioClip(int audioIndex)
    {
        AudioClip audioClip = null;
        if (audioIndex < _audioClips!.Length)
        {
            audioClip = _audioClips[audioIndex].GetRandomAudioClip();
        }
        return audioClip;
    }
}
