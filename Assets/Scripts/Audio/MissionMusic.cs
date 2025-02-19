using UnityEngine;
using FMODUnity;

public class MissionMusic : MonoBehaviour
{
    [SerializeField] private MusicWrapper[] _musicWrapper;

    private void Start()
    {
        CustomEvents.OnPlayRandomLevelMusic += PlayRandomMusic;
        CustomEvents.OnPauseChanged += PauseMusicToggle;
    }

    private void PlayRandomMusic()
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().MusicTheme];

        sound.Music.Play();
        if (sound.Ambience != null) sound.Ambience.Play();
    }

    private void PauseMusicToggle(bool state)
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().MusicTheme];
        
        sound.Music.EventInstance.setPaused(state);
    }

    private void OnDestroy()
    {
        CustomEvents.OnPlayRandomLevelMusic -= PlayRandomMusic;
        CustomEvents.OnPauseChanged -= PauseMusicToggle;
    }
}

[System.Serializable]
public class MusicWrapper
{
    public MusicThemeEnum MusicTheme;
    public StudioEventEmitter Music;
    public StudioEventEmitter Ambience;
}

[System.Serializable]
public enum MusicThemeEnum
{
    WasteLand = 0,
}
