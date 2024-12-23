using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Volume")]
    [Range(0, 1)] public float MasterVolume = 0.6f;
    [Range(0, 1)] public float SfxVolume = 0.6f;
    [Range(0, 1)] public float UiVolume = 0.6f;
    [Range(0, 1)] public float MusicVolume = 0.6f;

    private Bus _masterBus;
    private Bus _sfxBus;
    private Bus _uiBus;
    private Bus _musicBus;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Two Audio Manager");
        Instance = this;

        _masterBus = RuntimeManager.GetBus("bus:/");
        _sfxBus = RuntimeManager.GetBus("bus:/Sfx");
        _uiBus = RuntimeManager.GetBus("bus:/Ui");
        _musicBus = RuntimeManager.GetBus("bus:/Music");
    }

    private void Update()
    {
        _masterBus.setVolume(MasterVolume);
        _sfxBus.setVolume(SfxVolume);
        _uiBus.setVolume(UiVolume);
        _musicBus.setVolume(MusicVolume);
    }

    public void PlayerOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    internal void PlayerOneShot(object eventReference, Vector3 position)
    {
        throw new NotImplementedException();
    }
}
