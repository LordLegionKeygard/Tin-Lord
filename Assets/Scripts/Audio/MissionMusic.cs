using UnityEngine;
using FMODUnity;

public class MissionMusic : MonoBehaviour
{
    [SerializeField] private MusicWrapper[] _musicWrapper;
    private int _currentMusicNumber;

    private void Start()
    {
        CustomEvents.OnPlayRandomLevelMusic += PlayRandomMusic;
        CustomEvents.OnTurnOffLevelMusic += TurnOffMusic;
    }

    private void PlayRandomMusic()
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().Biome];

        // var rnd = Random.Range(0, sound.Musics.Length);
        // _currentMusicNumber = rnd;
        // sound.Musics[_currentMusicNumber].Play();
        sound.Ambience.Play();

    }

    private void TurnOffMusic()
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().Biome];
        // if (sound.Length == 0) return;

        // sound.Musics[_currentMusicNumber].Stop();
        // sound.Ambience.Stop();
    }

    private void OnDestroy()
    {
        CustomEvents.OnPlayRandomLevelMusic -= PlayRandomMusic;
        CustomEvents.OnTurnOffLevelMusic -= TurnOffMusic;
    }
}

[System.Serializable]
public class MusicWrapper
{
    public BiomeEnum Biom;
    public StudioEventEmitter[] Musics;
    public StudioEventEmitter Ambience;

}

[System.Serializable]
public enum BiomeEnum
{
    WasteLand = 0,
}
