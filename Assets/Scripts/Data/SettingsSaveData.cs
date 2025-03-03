using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsSaveData
{
    [Header("Video")]
    public int ScreenMode;
    public int Resolution;
    public int Quality;
    public int AntiAliasing;
    public int UpscalingFilter;
    public bool Glow;
    public int FrameRate;

    [Header("Audio")]
    public int MasterVolume;
    public int SfxVolume;
    public int UiVolume;
    public int MusicVolume;


    [Header("Gameplay")]
    public bool Blood;
}
