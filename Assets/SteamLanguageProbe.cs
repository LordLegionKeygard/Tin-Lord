// SteamLanguageProbe.cs
using System.Collections;
using System.Linq;
using UnityEngine;
using Steamworks;

public class SteamLanguageProbe : MonoBehaviour
{
    [SerializeField] private int _maxAttempts = 20;      // ~10 секунд
    [SerializeField] private float _delayBetween = 0.5f;

    private void Start() => StartCoroutine(Probe());

    private IEnumerator Probe()
    {
        for (int i = 0; i < _maxAttempts && !SteamManager.Initialized; i++)
            yield return new WaitForSeconds(_delayBetween);

        if (!SteamManager.Initialized)
        {
            Debug.LogError("[SteamLang] SteamManager not initialized. " +
                           "Проверь steam_appid.txt рядом с exe, запущен ли Steam, и AppID.");
            Debug.Log($"[SteamLang] Unity SystemLanguage: {Application.systemLanguage}");
            yield break;
        }

        var appId = SteamUtils.GetAppID(); // AppId_t
        var loggedOn = SteamUser.BLoggedOn();
        var persona = SteamFriends.GetPersonaName();

        string current = SteamApps.GetCurrentGameLanguage();           // напр. "english" / "russian"
        string availableRaw = SteamApps.GetAvailableGameLanguages();   // строка с ; между языками
        var available = availableRaw.Split(';').Select(s => s.Trim()).ToArray();

        Debug.Log($"[SteamLang] AppID: {appId.m_AppId}");
        Debug.Log($"[SteamLang] LoggedOn: {loggedOn}, User: {persona}");
        Debug.Log($"[SteamLang] Current: {current}");
        Debug.Log($"[SteamLang] AvailableRaw: {availableRaw}");
        Debug.Log($"[SteamLang] AvailableList: [{string.Join(", ", available)}]");
        Debug.Log($"[SteamLang] Unity SystemLanguage: {Application.systemLanguage}");
    }
}
