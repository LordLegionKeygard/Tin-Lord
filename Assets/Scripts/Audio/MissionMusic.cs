using UnityEngine;
using FMODUnity;

public class MissionMusic : MonoBehaviour
{
    [SerializeField] private MusicWrapper[] _musicWrapper;
    [SerializeField] private GameObject _escapeObject;

    private void Start()
    {
        CustomEvents.OnPlayRandomLevelMusic += PlayRandomMusic;
        CustomEvents.OnCheckPause += PauseMusicToggle;
    }

    private void PlayRandomMusic()
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().MusicTheme];

        sound.Music.Play();
        if (sound.Ambience != null) sound.Ambience.Play();
    }

    private void PauseMusicToggle(bool isPause)
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().MusicTheme];
        
        sound.Music.EventInstance.setPaused(isPause && _escapeObject.activeInHierarchy);
    }

    private void OnDestroy()
    {
        CustomEvents.OnPlayRandomLevelMusic -= PlayRandomMusic;
        CustomEvents.OnCheckPause -= PauseMusicToggle;
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
