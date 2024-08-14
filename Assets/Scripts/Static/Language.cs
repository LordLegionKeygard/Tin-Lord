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
        _text[1, 0] = "";
        _text[1, 1] = "";

        _text[2, 0] = "Building";
        _text[2, 1] = "Здание";

        _text[3, 0] = "Building level";
        _text[3, 1] = "Уровень здания";

        _text[4, 0] = "";
        _text[4, 1] = "";

        _text[5, 0] = "";
        _text[5, 1] = "";

        _text[6, 0] = "Total production";
        _text[6, 1] = "Итоговая добыча";

        _text[7, 0] = "Base Resources";
        _text[7, 1] = "Базовые Ресурсы";

        _text[8, 0] = "Building Materials";
        _text[8, 1] = "Стройматериалы";

        _text[9, 0] = "Components";
        _text[9, 1] = "Компоненты";

        _text[10, 0] = "Bridge";
        _text[10, 1] = "Мост";

        _text[11, 0] = "Producation modifier";
        _text[11, 1] = "Модификатор добычи";

        _text[12, 0] = "Day";
        _text[12, 1] = "День";

        _text[13, 0] = "Buildings";
        _text[13, 1] = "Постройки";

        _text[14, 0] = "Required resource";
        _text[14, 1] = "Требуемый ресурс";

        _text[15, 0] = "Ground ecology: ";
        _text[15, 1] = "Экология земли: ";

        _text[16, 0] = "Building ecology: ";
        _text[16, 1] = "Экология здания: ";


        for (int x = 0; x < 100; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
