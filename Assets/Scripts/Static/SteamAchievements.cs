using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{
    public static SteamAchievements Instance;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Two SteamAchievements");
        Instance = this;
    }

    // private IEnumerator Start()
    // {
    //     yield return new WaitForSeconds(4f);
    //     TryClearAchievement("ACHIEVEMENT_1");
    // }

    public void UnlockAchievement(string achName)
    {
        if (!SteamManager.Initialized) return;

        SteamUserStats.SetAchievement(achName);
        SteamUserStats.StoreStats();
    }

    // public void ClearAchievement(string achName)
    // {
    //     if (!SteamManager.Initialized) return;

    //     SteamUserStats.ClearAchievement(achName);
    //     SteamUserStats.StoreStats();
    // }
}

