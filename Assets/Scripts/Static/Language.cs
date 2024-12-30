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

        _text[26, 0] = "Continue game";
        _text[26, 1] = "Продолжить игру";

        _text[27, 0] = "New game";
        _text[27, 1] = "Новая игра";

        _text[28, 0] = "Settings";
        _text[28, 1] = "Настройки";

        _text[29, 0] = "Quit";
        _text[29, 1] = "Выход";

        _text[30, 0] = "Loading";
        _text[30, 1] = "Загрузка";

        _text[31, 0] = "Are you sure you want to start a new game?\n\nYour past save will be overwritten.";
        _text[31, 1] = "Вы уверены, что хотите начать новую игру?\n\nВаше прошлое сохранения будет перезаписано.";

        _text[32, 0] = "Command Center";
        _text[32, 1] = "Командный Центр";

        _text[33, 0] = "Duration:";
        _text[33, 1] = "Длительность:";

        _text[34, 0] = "Ecology level:";
        _text[34, 1] = "Уровень экологии:";

        _text[35, 0] = "Starting resources:";
        _text[35, 1] = "Начальные ресурсы:";

        _text[36, 0] = "Objectives:";
        _text[36, 1] = "Цели:";

        _text[37, 0] = "days";
        _text[37, 1] = "дней";

        _text[38, 0] = "unlimited";
        _text[38, 1] = "неограниченно";

        _text[39, 0] = "Restore the ecology to";
        _text[39, 1] = "Восстановить экологию до";

        _text[40, 0] = "Kill {0} enemies";
        _text[40, 1] = "Убить {0} врагов";

        _text[41, 0] = "Construct {0} buildings";
        _text[41, 1] = "Построить {0} зданий";

        _text[42, 0] = "Survive {0} days";
        _text[42, 1] = "Выжить {0} дней";

        _text[43, 0] = "";
        _text[43, 1] = "";

        _text[44, 0] = "";
        _text[44, 1] = "";

        _text[45, 0] = "";
        _text[45, 1] = "";

        _text[46, 0] = "";
        _text[46, 1] = "";

        _text[47, 0] = "";
        _text[47, 1] = "";

        _text[48, 0] = "";
        _text[48, 1] = "";

        _text[49, 0] = "";
        _text[49, 1] = "";

        _text[50, 0] = "";
        _text[50, 1] = "";

        _text[51, 0] = "";
        _text[51, 1] = "";

        _text[52, 0] = "";
        _text[52, 1] = "";


        for (int x = 0; x < 100; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
