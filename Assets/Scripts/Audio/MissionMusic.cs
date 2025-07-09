using UnityEngine;
using FMODUnity;
using System.Collections;

public class MissionMusic : MonoBehaviour
{
    [SerializeField] private MusicWrapper[] _musicWrapper;
    [SerializeField] private GameObject _escapeObject;
    private float _fadeOutDelay = 2f;

    private void Start()
    {
        CustomEvents.OnDataLoad += PreparePlayMusic;
        CustomEvents.OnCheckPause += PauseMusicToggle;
    }

    private void PreparePlayMusic()
    {
        StartCoroutine(nameof(PlayMusicCoroutine));
    }

    private IEnumerator PlayMusicCoroutine()
    {
        yield return new WaitForSecondsRealtime(_fadeOutDelay);

        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentLandscape().LandscapeEnum];

        sound.Music.Play();
        if (sound.Ambience != null) sound.Ambience.Play();
    }

    private void PauseMusicToggle(bool isPause)
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentLandscape().LandscapeEnum];
        
        sound.Music.EventInstance.setPaused(isPause && _escapeObject.activeInHierarchy);
    }

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= PreparePlayMusic;
        CustomEvents.OnCheckPause -= PauseMusicToggle;
    }
}

[System.Serializable]
public class MusicWrapper
{
    public LandscapeEnum MusicTheme;
    public StudioEventEmitter Music;
    public StudioEventEmitter Ambience;
}
