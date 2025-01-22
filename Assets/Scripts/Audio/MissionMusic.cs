using UnityEngine;
using FMODUnity;

public class MissionMusic : MonoBehaviour
{
    [SerializeField] private MusicWrapper[] _musicWrapper;

    private void Start()
    {
        CustomEvents.OnPlayRandomLevelMusic += PlayRandomMusic;
        CustomEvents.OnTurnOffLevelMusic += TurnOffMusic;
    }

    private void PlayRandomMusic()
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().Biome];


        sound.Music.Play();
        sound.Ambience.Play();

    }

    private void TurnOffMusic()
    {
        var sound = _musicWrapper[(int)CurrentMissionInfo.Instance.GetCurrentMission().Biome];

        sound.Music.Stop();
        sound.Ambience.Stop();
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
    public StudioEventEmitter Music;
    public StudioEventEmitter Ambience;

}

[System.Serializable]
public enum BiomeEnum
{
    WasteLand = 0,
}
