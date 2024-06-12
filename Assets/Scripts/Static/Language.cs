using UnityEngine;
// using Steamworks;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 1;
    private string[,] _text = new string[100, 2];
    public static string[] TextStatic = new string[100];

    private void Awake()
    {
        // if (SteamManager.Initialized) LanguageNumber = CheckSteamLanguage();
        // SetLanguage();
    }

    // private int CheckSteamLanguage()
    // {
    //     switch (SteamApps.GetCurrentGameLanguage())
    //     {
    //         case "english": default: return 0;
    //         case "russian": return 1;
    //     }
    // }

    public void SetLanguage()
    {
        _text[1, 0] = "";
        _text[1, 1] = "";

        _text[2, 0] = "";
        _text[2, 1] = "";

        for (int x = 0; x < 100; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
