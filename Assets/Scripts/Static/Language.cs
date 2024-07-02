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
        SetLanguage();
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
        _text[1, 0] = "Ecology";
        _text[1, 1] = "Экология";

        _text[2, 0] = "Building";
        _text[2, 1] = "Здание";

        _text[3, 0] = "Building level";
        _text[3, 1] = "Уровень здания";

        _text[4, 0] = "Build";
        _text[4, 1] = "Строить";

        _text[5, 0] = "Upgrade";
        _text[5, 1] = "Улучшить";

        _text[6, 0] = "Total production";
        _text[6, 1] = "Итоговая добыча";

        _text[7, 0] = "Base Resources";
        _text[7, 1] = "Базовые Ресурсы";

        _text[8, 0] = "Building Materials";
        _text[8, 1] = "Стройматериалы";

        _text[9, 0] = "Robotics";
        _text[9, 1] = "Робототехника";

        _text[10, 0] = "Bridge";
        _text[10, 1] = "Мост";

        _text[11, 0] = "Producation modifier";
        _text[11, 1] = "Модификатор добычи";

        for (int x = 0; x < 100; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
