using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IManager
{
    private static AudioManager _instance = null;
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();
    private AudioSettings _settings = null;
    
    public void Construct()
    {
        _settings = SettingsManager.GetSettings<AudioSettings>();
    }

    public void Activate()
    {
    }

    public void Initialize()
    {
        _instance = this;
    }
    
    public void Deactivate()
    {
    }

    public void Terminate()
    {
    }

    public static void PlayAudio(int audioId)
    {
        if (!_instance)
        {
            return;
        }
        AudioSource audioSource = _instance.GetAudioSource();
        if (audioSource)
        {
            audioSource.PlayOneShot(_instance.GetAudioClip(audioId));
            _instance.StartCoroutine(_instance.DisableAudioSource(audioSource));
        }
    }

    private AudioClip GetAudioClip(int audioIndex)
    {
        AudioClip audioClip = null;
        if (_settings)
        {
            audioClip = _settings.GetAudioClip(audioIndex);
        }
        return audioClip;
    }

    private IEnumerator DisableAudioSource(AudioSource audioSource)
    {
        float disableDelay = 1f;
        if (_settings)
        {
            disableDelay = _settings.DisableDelay;
        }
        yield return new WaitForSeconds(disableDelay);
        audioSource.gameObject.SetActive(false);
    }

    private AudioSource GetAudioSource()
    {
        AudioSource audioSource = GetInactiveAudioSource();
        if (!audioSource)
        {
            audioSource = InstantiateAudioSource();
        }
        return audioSource;
    }
    
    private AudioSource GetInactiveAudioSource()
    {
        AudioSource audioSource = null;
        foreach (AudioSource source in _audioSources)
        {
            if (!source.gameObject.activeSelf)
            {
                audioSource = source;
                break;
            }
        }
        if (audioSource)
        {
            audioSource.gameObject.SetActive(true);
        }
        return audioSource;
    }

    private AudioSource InstantiateAudioSource()
    {
        if (!_settings || !_settings.AudioSourcePrefab || _audioSources.Count >= _settings.MaxAudioSources)
        {
            return null;
        }
        if (_audioSources == null)
        {
            _audioSources = new List<AudioSource>();
        }
        AudioSource audioSource = Instantiate(_settings.AudioSourcePrefab, Vector3.zero, Quaternion.identity);
        _audioSources.Add(audioSource);
        audioSource.transform.SetParent(this.transform);
        return audioSource;
    }
}
