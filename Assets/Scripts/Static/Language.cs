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
        _text[0, 0] = "Tin Lord";
        _text[0, 1] = "Жестяной Лорд";

        _text[1, 0] = "Recept:";
        _text[1, 1] = "Рецепт:";

        _text[2, 0] = "Building";
        _text[2, 1] = "Здание";

        _text[3, 0] = "Building level";
        _text[3, 1] = "Уровень здания";

        _text[4, 0] = "Repair";
        _text[4, 1] = "Починить";

        _text[5, 0] = "RADIATION";
        _text[5, 1] = "РАДИАЦИЯ";

        _text[6, 0] = "Production resource";
        _text[6, 1] = "Добываемый ресурс";

        _text[7, 0] = "Resources";
        _text[7, 1] = "Ресурсы";

        _text[8, 0] = "Materials";
        _text[8, 1] = "Материалы";

        _text[9, 0] = "Components";
        _text[9, 1] = "Компоненты";

        _text[10, 0] = "Select building type";
        _text[10, 1] = "Выберите тип здания";

        _text[11, 0] = "Production modifier";
        _text[11, 1] = "Модификатор добычи";

        _text[12, 0] = "DAY";
        _text[12, 1] = "ДЕНЬ";

        _text[13, 0] = "Buildings";
        _text[13, 1] = "Постройки";

        _text[14, 0] = "Required resource";
        _text[14, 1] = "Требуемый ресурс";

        _text[15, 0] = "Ground ecology: ";
        _text[15, 1] = "Экология земли: ";

        _text[16, 0] = "Building ecology: ";
        _text[16, 1] = "Экология здания: ";

        _text[17, 0] = "Other";
        _text[17, 1] = "Другое";

        _text[18, 0] = "Durability: ";
        _text[18, 1] = "Прочность: ";

        _text[19, 0] = "Melee damage: ";
        _text[19, 1] = "Урон в ближнем бою: ";

        _text[20, 0] = "Range damage: ";
        _text[20, 1] = "Урон в дальнем бою: ";

        _text[21, 0] = "Robots";
        _text[21, 1] = "Роботы";

        _text[22, 0] = "Demolish the building?";
        _text[22, 1] = "Разрушить здание?";

        _text[23, 0] = "After destruction you will receive:";
        _text[23, 1] = "После разрушения вы получите:";

        _text[24, 0] = "Destroy the landscape?";
        _text[24, 1] = "Уничтожить ландшафт?";

        _text[25, 0] = "Destruction requires:";
        _text[25, 1] = "Для уничтожения требуется:";

        _text[26, 0] = "New game";
        _text[26, 1] = "Новая игра";

        _text[27, 0] = "Continue game";
        _text[27, 1] = "Продолжить игру";

        _text[28, 0] = "Settings";
        _text[28, 1] = "Настройки";

        _text[29, 0] = "Quit";
        _text[29, 1] = "Выход";


        for (int x = 0; x < 100; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
