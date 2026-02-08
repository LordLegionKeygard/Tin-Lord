using UnityEngine;
using Steamworks;

// 0 - English						en
// 1 - Russian						ru
// 2 - French						fr
// 3 - Italian						it
// 4 - German						de
// 5 - Spanish (Spain)				es-ES
// 6 - Polish						pl
// 7 - Portuguese (Brazil)			pt-BR
// 8 - Japanese						ja
// 9 - Chinese (Simplified)			zh-Hans

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 1;
    private string[,] _text = new string[WorldGameInfo.LanguageLength, 10];
    public static string[] TextStatic = new string[WorldGameInfo.LanguageLength];

    private void Awake()
    {
        if (SteamManager.Initialized) LanguageNumber = CheckSteamLanguage();
        SetLanguage();
    }

    private int CheckSteamLanguage()
    {
        switch (SteamApps.GetCurrentGameLanguage())
        {
            case "english": default: return 0;
            case "russian": return 1;
        }
    }

    public void SetLanguage()
    {
        _text[0, 0] = "Tin Lord";
        _text[0, 1] = "Жестяной Лорд";
        _text[0, 2] = "";
        _text[0, 3] = "";
        _text[0, 4] = "";
        _text[0, 5] = "";
        _text[0, 6] = "";
        _text[0, 7] = "";
        _text[0, 8] = "";
        _text[0, 9] = "";

        _text[1, 0] = "Recipe";
        _text[1, 1] = "Рецепт";
        _text[1, 2] = "";
        _text[1, 3] = "";
        _text[1, 4] = "";
        _text[1, 5] = "";
        _text[1, 6] = "";
        _text[1, 7] = "";
        _text[1, 8] = "";
        _text[1, 9] = "";

        _text[2, 0] = "Building";
        _text[2, 1] = "Здание";
        _text[2, 2] = "";
        _text[2, 3] = "";
        _text[2, 4] = "";
        _text[2, 5] = "";
        _text[2, 6] = "";
        _text[2, 7] = "";
        _text[2, 8] = "";
        _text[2, 9] = "";

        _text[3, 0] = "Building level";
        _text[3, 1] = "Уровень здания";
        _text[3, 2] = "";
        _text[3, 3] = "";
        _text[3, 4] = "";
        _text[3, 5] = "";
        _text[3, 6] = "";
        _text[3, 7] = "";
        _text[3, 8] = "";
        _text[3, 9] = "";

        // это для кнопки
        _text[4, 0] = "Repair";
        _text[4, 1] = "Починить";
        _text[4, 2] = "";
        _text[4, 3] = "";
        _text[4, 4] = "";
        _text[4, 5] = "";
        _text[4, 6] = "";
        _text[4, 7] = "";
        _text[4, 8] = "";
        _text[4, 9] = "";

        _text[5, 0] = "RAD";
        _text[5, 1] = "РАД";
        _text[5, 2] = "";
        _text[5, 3] = "";
        _text[5, 4] = "";
        _text[5, 5] = "";
        _text[5, 6] = "";
        _text[5, 7] = "";
        _text[5, 8] = "";
        _text[5, 9] = "";

        _text[6, 0] = "Production resource";
        _text[6, 1] = "Добываемый ресурс";
        _text[6, 2] = "";
        _text[6, 3] = "";
        _text[6, 4] = "";
        _text[6, 5] = "";
        _text[6, 6] = "";
        _text[6, 7] = "";
        _text[6, 8] = "";
        _text[6, 9] = "";

        _text[7, 0] = "Resources";
        _text[7, 1] = "Ресурсы";
        _text[7, 2] = "";
        _text[7, 3] = "";
        _text[7, 4] = "";
        _text[7, 5] = "";
        _text[7, 6] = "";
        _text[7, 7] = "";
        _text[7, 8] = "";
        _text[7, 9] = "";

        _text[8, 0] = "Materials";
        _text[8, 1] = "Материалы";
        _text[8, 2] = "";
        _text[8, 3] = "";
        _text[8, 4] = "";
        _text[8, 5] = "";
        _text[8, 6] = "";
        _text[8, 7] = "";
        _text[8, 8] = "";
        _text[8, 9] = "";

        _text[9, 0] = "Components";
        _text[9, 1] = "Компоненты";
        _text[9, 2] = "";
        _text[9, 3] = "";
        _text[9, 4] = "";
        _text[9, 5] = "";
        _text[9, 6] = "";
        _text[9, 7] = "";
        _text[9, 8] = "";
        _text[9, 9] = "";

        _text[10, 0] = "Select building type";
        _text[10, 1] = "Выберите тип здания";
        _text[10, 2] = "";
        _text[10, 3] = "";
        _text[10, 4] = "";
        _text[10, 5] = "";
        _text[10, 6] = "";
        _text[10, 7] = "";
        _text[10, 8] = "";
        _text[10, 9] = "";

        _text[11, 0] = "Production modifier";
        _text[11, 1] = "Модификатор добычи";
        _text[11, 2] = "";
        _text[11, 3] = "";
        _text[11, 4] = "";
        _text[11, 5] = "";
        _text[11, 6] = "";
        _text[11, 7] = "";
        _text[11, 8] = "";
        _text[11, 9] = "";

        _text[12, 0] = "Select a building";
        _text[12, 1] = "Выберите здание";
        _text[12, 2] = "";
        _text[12, 3] = "";
        _text[12, 4] = "";
        _text[12, 5] = "";
        _text[12, 6] = "";
        _text[12, 7] = "";
        _text[12, 8] = "";
        _text[12, 9] = "";

        _text[13, 0] = "Buildings";
        _text[13, 1] = "Постройки";
        _text[13, 2] = "";
        _text[13, 3] = "";
        _text[13, 4] = "";
        _text[13, 5] = "";
        _text[13, 6] = "";
        _text[13, 7] = "";
        _text[13, 8] = "";
        _text[13, 9] = "";

        _text[14, 0] = "Resource for work";
        _text[14, 1] = "Ресурс для работы";
        _text[14, 2] = "";
        _text[14, 3] = "";
        _text[14, 4] = "";
        _text[14, 5] = "";
        _text[14, 6] = "";
        _text[14, 7] = "";
        _text[14, 8] = "";
        _text[14, 9] = "";

        _text[15, 0] = "Ground ecology";
        _text[15, 1] = "Экология земли";
        _text[15, 2] = "";
        _text[15, 3] = "";
        _text[15, 4] = "";
        _text[15, 5] = "";
        _text[15, 6] = "";
        _text[15, 7] = "";
        _text[15, 8] = "";
        _text[15, 9] = "";

        _text[16, 0] = "Building ecology";
        _text[16, 1] = "Экология здания";
        _text[16, 2] = "";
        _text[16, 3] = "";
        _text[16, 4] = "";
        _text[16, 5] = "";
        _text[16, 6] = "";
        _text[16, 7] = "";
        _text[16, 8] = "";
        _text[16, 9] = "";

        _text[17, 0] = "Other";
        _text[17, 1] = "Другое";
        _text[17, 2] = "";
        _text[17, 3] = "";
        _text[17, 4] = "";
        _text[17, 5] = "";
        _text[17, 6] = "";
        _text[17, 7] = "";
        _text[17, 8] = "";
        _text[17, 9] = "";

        _text[18, 0] = "Durability";
        _text[18, 1] = "Прочность";
        _text[18, 2] = "";
        _text[18, 3] = "";
        _text[18, 4] = "";
        _text[18, 5] = "";
        _text[18, 6] = "";
        _text[18, 7] = "";
        _text[18, 8] = "";
        _text[18, 9] = "";

        _text[19, 0] = "Increase Damage";
        _text[19, 1] = "Повышение Урона";
        _text[19, 2] = "";
        _text[19, 3] = "";
        _text[19, 4] = "";
        _text[19, 5] = "";
        _text[19, 6] = "";
        _text[19, 7] = "";
        _text[19, 8] = "";
        _text[19, 9] = "";

        _text[20, 0] = "Increase Durability";
        _text[20, 1] = "Повышение Прочности";
        _text[20, 2] = "";
        _text[20, 3] = "";
        _text[20, 4] = "";
        _text[20, 5] = "";
        _text[20, 6] = "";
        _text[20, 7] = "";
        _text[20, 8] = "";
        _text[20, 9] = "";

        _text[21, 0] = "Machines";
        _text[21, 1] = "Машины";
        _text[21, 2] = "";
        _text[21, 3] = "";
        _text[21, 4] = "";
        _text[21, 5] = "";
        _text[21, 6] = "";
        _text[21, 7] = "";
        _text[21, 8] = "";
        _text[21, 9] = "";

        _text[22, 0] = "Demolish the building?";
        _text[22, 1] = "Разрушить здание?";
        _text[22, 2] = "";
        _text[22, 3] = "";
        _text[22, 4] = "";
        _text[22, 5] = "";
        _text[22, 6] = "";
        _text[22, 7] = "";
        _text[22, 8] = "";
        _text[22, 9] = "";

        _text[23, 0] = "After destruction you will receive:";
        _text[23, 1] = "После разрушения вы получите:";
        _text[23, 2] = "";
        _text[23, 3] = "";
        _text[23, 4] = "";
        _text[23, 5] = "";
        _text[23, 6] = "";
        _text[23, 7] = "";
        _text[23, 8] = "";
        _text[23, 9] = "";

        _text[24, 0] = "Destroy the landscape?";
        _text[24, 1] = "Уничтожить ландшафт?";
        _text[24, 2] = "";
        _text[24, 3] = "";
        _text[24, 4] = "";
        _text[24, 5] = "";
        _text[24, 6] = "";
        _text[24, 7] = "";
        _text[24, 8] = "";
        _text[24, 9] = "";

        _text[25, 0] = "Destruction requires:";
        _text[25, 1] = "Для уничтожения требуется:";
        _text[25, 2] = "";
        _text[25, 3] = "";
        _text[25, 4] = "";
        _text[25, 5] = "";
        _text[25, 6] = "";
        _text[25, 7] = "";
        _text[25, 8] = "";
        _text[25, 9] = "";

        _text[26, 0] = "Continue game";
        _text[26, 1] = "Продолжить игру";
        _text[26, 2] = "";
        _text[26, 3] = "";
        _text[26, 4] = "";
        _text[26, 5] = "";
        _text[26, 6] = "";
        _text[26, 7] = "";
        _text[26, 8] = "";
        _text[26, 9] = "";

        _text[27, 0] = "New game";
        _text[27, 1] = "Новая игра";
        _text[27, 2] = "";
        _text[27, 3] = "";
        _text[27, 4] = "";
        _text[27, 5] = "";
        _text[27, 6] = "";
        _text[27, 7] = "";
        _text[27, 8] = "";
        _text[27, 9] = "";

        _text[28, 0] = "Settings";
        _text[28, 1] = "Настройки";
        _text[28, 2] = "";
        _text[28, 3] = "";
        _text[28, 4] = "";
        _text[28, 5] = "";
        _text[28, 6] = "";
        _text[28, 7] = "";
        _text[28, 8] = "";
        _text[28, 9] = "";

        _text[29, 0] = "Quit";
        _text[29, 1] = "Выход";
        _text[29, 2] = "";
        _text[29, 3] = "";
        _text[29, 4] = "";
        _text[29, 5] = "";
        _text[29, 6] = "";
        _text[29, 7] = "";
        _text[29, 8] = "";
        _text[29, 9] = "";

        _text[30, 0] = "Loading";
        _text[30, 1] = "Загрузка";
        _text[30, 2] = "";
        _text[30, 3] = "";
        _text[30, 4] = "";
        _text[30, 5] = "";
        _text[30, 6] = "";
        _text[30, 7] = "";
        _text[30, 8] = "";
        _text[30, 9] = "";

        _text[31, 0] = "Are you sure you want to start a new game?\n\nYour past save will be overwritten.";
        _text[31, 1] = "Вы уверены, что хотите начать новую игру?\n\nВаше прошлое сохранение будет перезаписано.";
        _text[31, 2] = "";
        _text[31, 3] = "";
        _text[31, 4] = "";
        _text[31, 5] = "";
        _text[31, 6] = "";
        _text[31, 7] = "";
        _text[31, 8] = "";
        _text[31, 9] = "";

        _text[32, 0] = "Command Center";
        _text[32, 1] = "Командный Центр";
        _text[32, 2] = "";
        _text[32, 3] = "";
        _text[32, 4] = "";
        _text[32, 5] = "";
        _text[32, 6] = "";
        _text[32, 7] = "";
        _text[32, 8] = "";
        _text[32, 9] = "";

        _text[33, 0] = "Continue";
        _text[33, 1] = "Продолжить";
        _text[33, 2] = "";
        _text[33, 3] = "";
        _text[33, 4] = "";
        _text[33, 5] = "";
        _text[33, 6] = "";
        _text[33, 7] = "";
        _text[33, 8] = "";
        _text[33, 9] = "";

        _text[34, 0] = "Ecology level";
        _text[34, 1] = "Уровень экологии";
        _text[34, 2] = "";
        _text[34, 3] = "";
        _text[34, 4] = "";
        _text[34, 5] = "";
        _text[34, 6] = "";
        _text[34, 7] = "";
        _text[34, 8] = "";
        _text[34, 9] = "";

        _text[35, 0] = "Starting resources";
        _text[35, 1] = "Начальные ресурсы";
        _text[35, 2] = "";
        _text[35, 3] = "";
        _text[35, 4] = "";
        _text[35, 5] = "";
        _text[35, 6] = "";
        _text[35, 7] = "";
        _text[35, 8] = "";
        _text[35, 9] = "";

        _text[36, 0] = "Objectives";
        _text[36, 1] = "Цели";
        _text[36, 2] = "";
        _text[36, 3] = "";
        _text[36, 4] = "";
        _text[36, 5] = "";
        _text[36, 6] = "";
        _text[36, 7] = "";
        _text[36, 8] = "";
        _text[36, 9] = "";

        _text[37, 0] = "days";
        _text[37, 1] = "дней";
        _text[37, 2] = "";
        _text[37, 3] = "";
        _text[37, 4] = "";
        _text[37, 5] = "";
        _text[37, 6] = "";
        _text[37, 7] = "";
        _text[37, 8] = "";
        _text[37, 9] = "";

        _text[38, 0] = "TERMINAL #042";
        _text[38, 1] = "ТЕРМИНАЛ #042";
        _text[38, 2] = "";
        _text[38, 3] = "";
        _text[38, 4] = "";
        _text[38, 5] = "";
        _text[38, 6] = "";
        _text[38, 7] = "";
        _text[38, 8] = "";
        _text[38, 9] = "";

        _text[39, 0] = "Restore the ecology to";
        _text[39, 1] = "Восстановить экологию до";
        _text[39, 2] = "";
        _text[39, 3] = "";
        _text[39, 4] = "";
        _text[39, 5] = "";
        _text[39, 6] = "";
        _text[39, 7] = "";
        _text[39, 8] = "";
        _text[39, 9] = "";

        _text[40, 0] = "Kill {0} enemies";
        _text[40, 1] = "Убить {0} врагов";
        _text[40, 2] = "";
        _text[40, 3] = "";
        _text[40, 4] = "";
        _text[40, 5] = "";
        _text[40, 6] = "";
        _text[40, 7] = "";
        _text[40, 8] = "";
        _text[40, 9] = "";

        _text[41, 0] = "Construct {0} buildings";
        _text[41, 1] = "Построить {0} зданий";
        _text[41, 2] = "";
        _text[41, 3] = "";
        _text[41, 4] = "";
        _text[41, 5] = "";
        _text[41, 6] = "";
        _text[41, 7] = "";
        _text[41, 8] = "";
        _text[41, 9] = "";

        _text[42, 0] = "Survive {0} days";
        _text[42, 1] = "Выжить {0} дней";
        _text[42, 2] = "";
        _text[42, 3] = "";
        _text[42, 4] = "";
        _text[42, 5] = "";
        _text[42, 6] = "";
        _text[42, 7] = "";
        _text[42, 8] = "";
        _text[42, 9] = "";

        _text[43, 0] = "You need to open";
        _text[43, 1] = "Вам нужно открыть";
        _text[43, 2] = "";
        _text[43, 3] = "";
        _text[43, 4] = "";
        _text[43, 5] = "";
        _text[43, 6] = "";
        _text[43, 7] = "";
        _text[43, 8] = "";
        _text[43, 9] = "";

        _text[44, 0] = "Escape";
        _text[44, 1] = "Сбежать";
        _text[44, 2] = "";
        _text[44, 3] = "";
        _text[44, 4] = "";
        _text[44, 5] = "";
        _text[44, 6] = "";
        _text[44, 7] = "";
        _text[44, 8] = "";
        _text[44, 9] = "";

        _text[45, 0] = "Restart";
        _text[45, 1] = "Перезапуск";
        _text[45, 2] = "";
        _text[45, 3] = "";
        _text[45, 4] = "";
        _text[45, 5] = "";
        _text[45, 6] = "";
        _text[45, 7] = "";
        _text[45, 8] = "";
        _text[45, 9] = "";

        _text[46, 0] = "Exit";
        _text[46, 1] = "Выход";
        _text[46, 2] = "";
        _text[46, 3] = "";
        _text[46, 4] = "";
        _text[46, 5] = "";
        _text[46, 6] = "";
        _text[46, 7] = "";
        _text[46, 8] = "";
        _text[46, 9] = "";

        _text[47, 0] = "Main menu";
        _text[47, 1] = "Меню";
        _text[47, 2] = "";
        _text[47, 3] = "";
        _text[47, 4] = "";
        _text[47, 5] = "";
        _text[47, 6] = "";
        _text[47, 7] = "";
        _text[47, 8] = "";
        _text[47, 9] = "";

        _text[48, 0] = $"Are you sure you want to restart the mission?\n\n<color={Colors.HexWarningYellow}>You will lose one AI core.</color>";
        _text[48, 1] = $"Вы уверены, что хотите перезапустить миссию?\n\n<color={Colors.HexWarningYellow}>Вы потеряете одно ядро ИИ.</color>";
        _text[48, 2] = "";
        _text[48, 3] = "";
        _text[48, 4] = "";
        _text[48, 5] = "";
        _text[48, 6] = "";
        _text[48, 7] = "";
        _text[48, 8] = "";
        _text[48, 9] = "";

        _text[49, 0] = "Yes";
        _text[49, 1] = "Да";
        _text[49, 2] = "";
        _text[49, 3] = "";
        _text[49, 4] = "";
        _text[49, 5] = "";
        _text[49, 6] = "";
        _text[49, 7] = "";
        _text[49, 8] = "";
        _text[49, 9] = "";

        _text[50, 0] = "No";
        _text[50, 1] = "Нет";
        _text[50, 2] = "";
        _text[50, 3] = "";
        _text[50, 4] = "";
        _text[50, 5] = "";
        _text[50, 6] = "";
        _text[50, 7] = "";
        _text[50, 8] = "";
        _text[50, 9] = "";

        _text[51, 0] = "Start mission";
        _text[51, 1] = "Начать миссию";
        _text[51, 2] = "";
        _text[51, 3] = "";
        _text[51, 4] = "";
        _text[51, 5] = "";
        _text[51, 6] = "";
        _text[51, 7] = "";
        _text[51, 8] = "";
        _text[51, 9] = "";

        _text[52, 0] = "Load mission";
        _text[52, 1] = "Загрузить миссию";
        _text[52, 2] = "";
        _text[52, 3] = "";
        _text[52, 4] = "";
        _text[52, 5] = "";
        _text[52, 6] = "";
        _text[52, 7] = "";
        _text[52, 8] = "";
        _text[52, 9] = "";

        _text[53, 0] = "Construct";
        _text[53, 1] = "Построить";
        _text[53, 2] = "";
        _text[53, 3] = "";
        _text[53, 4] = "";
        _text[53, 5] = "";
        _text[53, 6] = "";
        _text[53, 7] = "";
        _text[53, 8] = "";
        _text[53, 9] = "";

        _text[54, 0] = "On / Off";
        _text[54, 1] = "Вкл / Выкл";
        _text[54, 2] = "";
        _text[54, 3] = "";
        _text[54, 4] = "";
        _text[54, 5] = "";
        _text[54, 6] = "";
        _text[54, 7] = "";
        _text[54, 8] = "";
        _text[54, 9] = "";

        _text[55, 0] = "Rotate";
        _text[55, 1] = "Повернуть";
        _text[55, 2] = "";
        _text[55, 3] = "";
        _text[55, 4] = "";
        _text[55, 5] = "";
        _text[55, 6] = "";
        _text[55, 7] = "";
        _text[55, 8] = "";
        _text[55, 9] = "";

        _text[56, 0] = "Destroy";
        _text[56, 1] = "Разрушить";
        _text[56, 2] = "";
        _text[56, 3] = "";
        _text[56, 4] = "";
        _text[56, 5] = "";
        _text[56, 6] = "";
        _text[56, 7] = "";
        _text[56, 8] = "";
        _text[56, 9] = "";

        _text[57, 0] = "Enginery";
        _text[57, 1] = "Техника";
        _text[57, 2] = "";
        _text[57, 3] = "";
        _text[57, 4] = "";
        _text[57, 5] = "";
        _text[57, 6] = "";
        _text[57, 7] = "";
        _text[57, 8] = "";
        _text[57, 9] = "";

        _text[58, 0] = "Ecology restored: {0}/{1}";
        _text[58, 1] = "Экология восстановлена: {0}/{1}";
        _text[58, 2] = "";
        _text[58, 3] = "";
        _text[58, 4] = "";
        _text[58, 5] = "";
        _text[58, 6] = "";
        _text[58, 7] = "";
        _text[58, 8] = "";
        _text[58, 9] = "";

        _text[59, 0] = "Enemies killed: {0}/{1}";
        _text[59, 1] = "Убито врагов: {0}/{1}";
        _text[59, 2] = "";
        _text[59, 3] = "";
        _text[59, 4] = "";
        _text[59, 5] = "";
        _text[59, 6] = "";
        _text[59, 7] = "";
        _text[59, 8] = "";
        _text[59, 9] = "";

        _text[60, 0] = "Buildings constructed: {0}/{1}";
        _text[60, 1] = "Построено зданий: {0}/{1}";
        _text[60, 2] = "";
        _text[60, 3] = "";
        _text[60, 4] = "";
        _text[60, 5] = "";
        _text[60, 6] = "";
        _text[60, 7] = "";
        _text[60, 8] = "";
        _text[60, 9] = "";

        _text[61, 0] = "Days lived: {0}/{1}";
        _text[61, 1] = "Прожито дней: {0}/{1}";
        _text[61, 2] = "";
        _text[61, 3] = "";
        _text[61, 4] = "";
        _text[61, 5] = "";
        _text[61, 6] = "";
        _text[61, 7] = "";
        _text[61, 8] = "";
        _text[61, 9] = "";

        _text[62, 0] = "Damage Increased";
        _text[62, 1] = "Урон Повышен";
        _text[62, 2] = "";
        _text[62, 3] = "";
        _text[62, 4] = "";
        _text[62, 5] = "";
        _text[62, 6] = "";
        _text[62, 7] = "";
        _text[62, 8] = "";
        _text[62, 9] = "";

        _text[63, 0] = "Victory";
        _text[63, 1] = "Победа";
        _text[63, 2] = "";
        _text[63, 3] = "";
        _text[63, 4] = "";
        _text[63, 5] = "";
        _text[63, 6] = "";
        _text[63, 7] = "";
        _text[63, 8] = "";
        _text[63, 9] = "";

        _text[64, 0] = "Defeat";
        _text[64, 1] = "Поражение";
        _text[64, 2] = "";
        _text[64, 3] = "";
        _text[64, 4] = "";
        _text[64, 5] = "";
        _text[64, 6] = "";
        _text[64, 7] = "";
        _text[64, 8] = "";
        _text[64, 9] = "";

        _text[65, 0] = "Escape";
        _text[65, 1] = "Сбежал";
        _text[65, 2] = "";
        _text[65, 3] = "";
        _text[65, 4] = "";
        _text[65, 5] = "";
        _text[65, 6] = "";
        _text[65, 7] = "";
        _text[65, 8] = "";
        _text[65, 9] = "";

        _text[66, 0] = $"<color={Colors.HexWarningYellow}>Escaping the mission will give you {WorldGameInfo.EscapeFragmentsPercent}% of the data fragments and losing one AI core.</color>\n\nYou must complete half of the objectives.";
        _text[66, 1] = $"<color={Colors.HexWarningYellow}>Сбежав с миссии, вы получите {WorldGameInfo.EscapeFragmentsPercent}% от фрагментов данных и потеряете одно ядро ИИ.</color>\n\nНеобходимо выполнить половину поставленных целей.";
        _text[66, 2] = "";
        _text[66, 3] = "";
        _text[66, 4] = "";
        _text[66, 5] = "";
        _text[66, 6] = "";
        _text[66, 7] = "";
        _text[66, 8] = "";
        _text[66, 9] = "";

        _text[67, 0] = "Save the mission and return to command center?";
        _text[67, 1] = "Сохранить миссию и вернуться в командный центр?";
        _text[67, 2] = "";
        _text[67, 3] = "";
        _text[67, 4] = "";
        _text[67, 5] = "";
        _text[67, 6] = "";
        _text[67, 7] = "";
        _text[67, 8] = "";
        _text[67, 9] = "";

        _text[68, 0] = $"Restart mission?\n\n<color={Colors.HexWarningYellow}>You will lose one AI core.</color>";
        _text[68, 1] = $"Перезапустить миссию?\n\n<color={Colors.HexWarningYellow}>Вы потеряете одно ядро ИИ.</color>";
        _text[68, 2] = "";
        _text[68, 3] = "";
        _text[68, 4] = "";
        _text[68, 5] = "";
        _text[68, 6] = "";
        _text[68, 7] = "";
        _text[68, 8] = "";
        _text[68, 9] = "";

        // название тактической карты
        _text[69, 0] = "Repair";
        _text[69, 1] = "Ремонт";
        _text[69, 2] = "";
        _text[69, 3] = "";
        _text[69, 4] = "";
        _text[69, 5] = "";
        _text[69, 6] = "";
        _text[69, 7] = "";
        _text[69, 8] = "";
        _text[69, 9] = "";

        _text[70, 0] = "Over Production";
        _text[70, 1] = "Сверхдобыча";
        _text[70, 2] = "";
        _text[70, 3] = "";
        _text[70, 4] = "";
        _text[70, 5] = "";
        _text[70, 6] = "";
        _text[70, 7] = "";
        _text[70, 8] = "";
        _text[70, 9] = "";

        _text[71, 0] = "Change Rarity";
        _text[71, 1] = "Смена Редкости";
        _text[71, 2] = "";
        _text[71, 3] = "";
        _text[71, 4] = "";
        _text[71, 5] = "";
        _text[71, 6] = "";
        _text[71, 7] = "";
        _text[71, 8] = "";
        _text[71, 9] = "";

        _text[72, 0] = "Plain";
        _text[72, 1] = "Равнина";
        _text[72, 2] = "";
        _text[72, 3] = "";
        _text[72, 4] = "";
        _text[72, 5] = "";
        _text[72, 6] = "";
        _text[72, 7] = "";
        _text[72, 8] = "";
        _text[72, 9] = "";

        _text[73, 0] = "Meadow";
        _text[73, 1] = "Луг";
        _text[73, 2] = "";
        _text[73, 3] = "";
        _text[73, 4] = "";
        _text[73, 5] = "";
        _text[73, 6] = "";
        _text[73, 7] = "";
        _text[73, 8] = "";
        _text[73, 9] = "";

        _text[74, 0] = "Road";
        _text[74, 1] = "Дорога";
        _text[74, 2] = "";
        _text[74, 3] = "";
        _text[74, 4] = "";
        _text[74, 5] = "";
        _text[74, 6] = "";
        _text[74, 7] = "";
        _text[74, 8] = "";
        _text[74, 9] = "";

        _text[75, 0] = "in";
        _text[75, 1] = "в";
        _text[75, 2] = "";
        _text[75, 3] = "";
        _text[75, 4] = "";
        _text[75, 5] = "";
        _text[75, 6] = "";
        _text[75, 7] = "";
        _text[75, 8] = "";
        _text[75, 9] = "";

        _text[76, 0] = "You can't restart the mission. You don't have any spare AI cores.";
        _text[76, 1] = "Вы не можете перезапустить миссию. У вас нет запасных ядер ИИ.";
        _text[76, 2] = "";
        _text[76, 3] = "";
        _text[76, 4] = "";
        _text[76, 5] = "";
        _text[76, 6] = "";
        _text[76, 7] = "";
        _text[76, 8] = "";
        _text[76, 9] = "";

        _text[77, 0] = "Launch";
        _text[77, 1] = "Запуск";
        _text[77, 2] = "";
        _text[77, 3] = "";
        _text[77, 4] = "";
        _text[77, 5] = "";
        _text[77, 6] = "";
        _text[77, 7] = "";
        _text[77, 8] = "";
        _text[77, 9] = "";

        _text[78, 0] = "Back";
        _text[78, 1] = "Назад";
        _text[78, 2] = "";
        _text[78, 3] = "";
        _text[78, 4] = "";
        _text[78, 5] = "";
        _text[78, 6] = "";
        _text[78, 7] = "";
        _text[78, 8] = "";
        _text[78, 9] = "";

        _text[79, 0] = "cost of repairing buildings";
        _text[79, 1] = "стоимость ремонта всех зданий";
        _text[79, 2] = "";
        _text[79, 3] = "";
        _text[79, 4] = "";
        _text[79, 5] = "";
        _text[79, 6] = "";
        _text[79, 7] = "";
        _text[79, 8] = "";
        _text[79, 9] = "";

        _text[80, 0] = "to building durability";
        _text[80, 1] = "к прочности зданий";
        _text[80, 2] = "";
        _text[80, 3] = "";
        _text[80, 4] = "";
        _text[80, 5] = "";
        _text[80, 6] = "";
        _text[80, 7] = "";
        _text[80, 8] = "";
        _text[80, 9] = "";

        _text[81, 0] = "to turret damage";
        _text[81, 1] = "к урону турелей";
        _text[81, 2] = "";
        _text[81, 3] = "";
        _text[81, 4] = "";
        _text[81, 5] = "";
        _text[81, 6] = "";
        _text[81, 7] = "";
        _text[81, 8] = "";
        _text[81, 9] = "";

        _text[82, 0] = "Forest";
        _text[82, 1] = "Лес";
        _text[82, 2] = "";
        _text[82, 3] = "";
        _text[82, 4] = "";
        _text[82, 5] = "";
        _text[82, 6] = "";
        _text[82, 7] = "";
        _text[82, 8] = "";
        _text[82, 9] = "";

        _text[83, 0] = "Shard";
        _text[83, 1] = "Осколок";
        _text[83, 2] = "";
        _text[83, 3] = "";
        _text[83, 4] = "";
        _text[83, 5] = "";
        _text[83, 6] = "";
        _text[83, 7] = "";
        _text[83, 8] = "";
        _text[83, 9] = "";

        _text[84, 0] = "Robots";
        _text[84, 1] = "Роботы";
        _text[84, 2] = "";
        _text[84, 3] = "";
        _text[84, 4] = "";
        _text[84, 5] = "";
        _text[84, 6] = "";
        _text[84, 7] = "";
        _text[84, 8] = "";
        _text[84, 9] = "";

        _text[85, 0] = "Starship Weapons";
        _text[85, 1] = "Орудия Корабля";
        _text[85, 2] = "";
        _text[85, 3] = "";
        _text[85, 4] = "";
        _text[85, 5] = "";
        _text[85, 6] = "";
        _text[85, 7] = "";
        _text[85, 8] = "";
        _text[85, 9] = "";

        _text[86, 0] = "You cannot restart the mission.\n\nYou have no spare AI cores.";
        _text[86, 1] = "Вы не можете начать миссию с начала.\n\nУ вас нет запасных ядер ИИ.";
        _text[86, 2] = "";
        _text[86, 3] = "";
        _text[86, 4] = "";
        _text[86, 5] = "";
        _text[86, 6] = "";
        _text[86, 7] = "";
        _text[86, 8] = "";
        _text[86, 9] = "";

        _text[87, 0] = "Not ready";
        _text[87, 1] = "Не готово";
        _text[87, 2] = "";
        _text[87, 3] = "";
        _text[87, 4] = "";
        _text[87, 5] = "";
        _text[87, 6] = "";
        _text[87, 7] = "";
        _text[87, 8] = "";
        _text[87, 9] = "";

        _text[88, 0] = "Left";
        _text[88, 1] = "Левое";
        _text[88, 2] = "";
        _text[88, 3] = "";
        _text[88, 4] = "";
        _text[88, 5] = "";
        _text[88, 6] = "";
        _text[88, 7] = "";
        _text[88, 8] = "";
        _text[88, 9] = "";

        _text[89, 0] = "Right";
        _text[89, 1] = "Правое";
        _text[89, 2] = "";
        _text[89, 3] = "";
        _text[89, 4] = "";
        _text[89, 5] = "";
        _text[89, 6] = "";
        _text[89, 7] = "";
        _text[89, 8] = "";
        _text[89, 9] = "";

        _text[90, 0] = "ICOSA CORP";
        _text[90, 1] = "ИКОСА КОРП";
        _text[90, 2] = "";
        _text[90, 3] = "";
        _text[90, 4] = "";
        _text[90, 5] = "";
        _text[90, 6] = "";
        _text[90, 7] = "";
        _text[90, 8] = "";
        _text[90, 9] = "";

        _text[91, 0] = "BUILDING BETTER WORLD";
        _text[91, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";
        _text[91, 2] = "";
        _text[91, 3] = "";
        _text[91, 4] = "";
        _text[91, 5] = "";
        _text[91, 6] = "";
        _text[91, 7] = "";
        _text[91, 8] = "";
        _text[91, 9] = "";

        _text[92, 0] = "COORDINATES";
        _text[92, 1] = "КООРДИНАТЫ";
        _text[92, 2] = "";
        _text[92, 3] = "";
        _text[92, 4] = "";
        _text[92, 5] = "";
        _text[92, 6] = "";
        _text[92, 7] = "";
        _text[92, 8] = "";
        _text[92, 9] = "";

        _text[93, 0] = "SIGNAL";
        _text[93, 1] = "СИГНАЛ";
        _text[93, 2] = "";
        _text[93, 3] = "";
        _text[93, 4] = "";
        _text[93, 5] = "";
        _text[93, 6] = "";
        _text[93, 7] = "";
        _text[93, 8] = "";
        _text[93, 9] = "";

        _text[94, 0] = "DIAGRAM";
        _text[94, 1] = "ДИАГРАММА";
        _text[94, 2] = "";
        _text[94, 3] = "";
        _text[94, 4] = "";
        _text[94, 5] = "";
        _text[94, 6] = "";
        _text[94, 7] = "";
        _text[94, 8] = "";
        _text[94, 9] = "";

        _text[95, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[95, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";
        _text[95, 2] = "";
        _text[95, 3] = "";
        _text[95, 4] = "";
        _text[95, 5] = "";
        _text[95, 6] = "";
        _text[95, 7] = "";
        _text[95, 8] = "";
        _text[95, 9] = "";

        _text[96, 0] = "Learn";
        _text[96, 1] = "Изучить";
        _text[96, 2] = "";
        _text[96, 3] = "";
        _text[96, 4] = "";
        _text[96, 5] = "";
        _text[96, 6] = "";
        _text[96, 7] = "";
        _text[96, 8] = "";
        _text[96, 9] = "";

        _text[97, 0] = "Building durability";
        _text[97, 1] = "Прочность здания";
        _text[97, 2] = "";
        _text[97, 3] = "";
        _text[97, 4] = "";
        _text[97, 5] = "";
        _text[97, 6] = "";
        _text[97, 7] = "";
        _text[97, 8] = "";
        _text[97, 9] = "";

        _text[98, 0] = "Damage";
        _text[98, 1] = "Урон";
        _text[98, 2] = "";
        _text[98, 3] = "";
        _text[98, 4] = "";
        _text[98, 5] = "";
        _text[98, 6] = "";
        _text[98, 7] = "";
        _text[98, 8] = "";
        _text[98, 9] = "";

        _text[99, 0] = "Attack speed";
        _text[99, 1] = "Скорость атаки";
        _text[99, 2] = "";
        _text[99, 3] = "";
        _text[99, 4] = "";
        _text[99, 5] = "";
        _text[99, 6] = "";
        _text[99, 7] = "";
        _text[99, 8] = "";
        _text[99, 9] = "";

        _text[100, 0] = "Attack radius";
        _text[100, 1] = "Радиус атаки";
        _text[100, 2] = "";
        _text[100, 3] = "";
        _text[100, 4] = "";
        _text[100, 5] = "";
        _text[100, 6] = "";
        _text[100, 7] = "";
        _text[100, 8] = "";
        _text[100, 9] = "";

        _text[101, 0] = "Rotation speed";
        _text[101, 1] = "Скорость вращения";
        _text[101, 2] = "";
        _text[101, 3] = "";
        _text[101, 4] = "";
        _text[101, 5] = "";
        _text[101, 6] = "";
        _text[101, 7] = "";
        _text[101, 8] = "";
        _text[101, 9] = "";

        _text[102, 0] = "Press any key\n\nEscape - cancel";
        _text[102, 1] = "Нажмите любую кнопку\n\nEscape - отмена";
        _text[102, 2] = "";
        _text[102, 3] = "";
        _text[102, 4] = "";
        _text[102, 5] = "";
        _text[102, 6] = "";
        _text[102, 7] = "";
        _text[102, 8] = "";
        _text[102, 9] = "";

        _text[103, 0] = "Borderless";
        _text[103, 1] = "Безрамочный";
        _text[103, 2] = "";
        _text[103, 3] = "";
        _text[103, 4] = "";
        _text[103, 5] = "";
        _text[103, 6] = "";
        _text[103, 7] = "";
        _text[103, 8] = "";
        _text[103, 9] = "";

        _text[104, 0] = "Camera speed";
        _text[104, 1] = "Скорость камеры";
        _text[104, 2] = "";
        _text[104, 3] = "";
        _text[104, 4] = "";
        _text[104, 5] = "";
        _text[104, 6] = "";
        _text[104, 7] = "";
        _text[104, 8] = "";
        _text[104, 9] = "";

        _text[105, 0] = "Master volume";
        _text[105, 1] = "Общая громкость";
        _text[105, 2] = "";
        _text[105, 3] = "";
        _text[105, 4] = "";
        _text[105, 5] = "";
        _text[105, 6] = "";
        _text[105, 7] = "";
        _text[105, 8] = "";
        _text[105, 9] = "";

        _text[106, 0] = "SFX volume";
        _text[106, 1] = "Громкость эффектов";
        _text[106, 2] = "";
        _text[106, 3] = "";
        _text[106, 4] = "";
        _text[106, 5] = "";
        _text[106, 6] = "";
        _text[106, 7] = "";
        _text[106, 8] = "";
        _text[106, 9] = "";

        _text[107, 0] = "UI volume";
        _text[107, 1] = "Громкость интерфейса";
        _text[107, 2] = "";
        _text[107, 3] = "";
        _text[107, 4] = "";
        _text[107, 5] = "";
        _text[107, 6] = "";
        _text[107, 7] = "";
        _text[107, 8] = "";
        _text[107, 9] = "";

        _text[108, 0] = "Music volume";
        _text[108, 1] = "Громкость музыки";
        _text[108, 2] = "";
        _text[108, 3] = "";
        _text[108, 4] = "";
        _text[108, 5] = "";
        _text[108, 6] = "";
        _text[108, 7] = "";
        _text[108, 8] = "";
        _text[108, 9] = "";

        _text[109, 0] = "Blood";
        _text[109, 1] = "Кровь";
        _text[109, 2] = "";
        _text[109, 3] = "";
        _text[109, 4] = "";
        _text[109, 5] = "";
        _text[109, 6] = "";
        _text[109, 7] = "";
        _text[109, 8] = "";
        _text[109, 9] = "";

        _text[110, 0] = "Video";
        _text[110, 1] = "Видео";
        _text[110, 2] = "";
        _text[110, 3] = "";
        _text[110, 4] = "";
        _text[110, 5] = "";
        _text[110, 6] = "";
        _text[110, 7] = "";
        _text[110, 8] = "";
        _text[110, 9] = "";

        _text[111, 0] = "Controls";
        _text[111, 1] = "Управление";
        _text[111, 2] = "";
        _text[111, 3] = "";
        _text[111, 4] = "";
        _text[111, 5] = "";
        _text[111, 6] = "";
        _text[111, 7] = "";
        _text[111, 8] = "";
        _text[111, 9] = "";

        _text[112, 0] = "Gameplay";
        _text[112, 1] = "Игра";
        _text[112, 2] = "";
        _text[112, 3] = "";
        _text[112, 4] = "";
        _text[112, 5] = "";
        _text[112, 6] = "";
        _text[112, 7] = "";
        _text[112, 8] = "";
        _text[112, 9] = "";

        _text[113, 0] = "Audio";
        _text[113, 1] = "Аудио";
        _text[113, 2] = "";
        _text[113, 3] = "";
        _text[113, 4] = "";
        _text[113, 5] = "";
        _text[113, 6] = "";
        _text[113, 7] = "";
        _text[113, 8] = "";
        _text[113, 9] = "";

        _text[114, 0] = "Screen Mode";
        _text[114, 1] = "Режим Экрана";
        _text[114, 2] = "";
        _text[114, 3] = "";
        _text[114, 4] = "";
        _text[114, 5] = "";
        _text[114, 6] = "";
        _text[114, 7] = "";
        _text[114, 8] = "";
        _text[114, 9] = "";

        _text[115, 0] = "Resolution";
        _text[115, 1] = "Разрешение";
        _text[115, 2] = "";
        _text[115, 3] = "";
        _text[115, 4] = "";
        _text[115, 5] = "";
        _text[115, 6] = "";
        _text[115, 7] = "";
        _text[115, 8] = "";
        _text[115, 9] = "";

        _text[116, 0] = "Quality";
        _text[116, 1] = "Качество";
        _text[116, 2] = "";
        _text[116, 3] = "";
        _text[116, 4] = "";
        _text[116, 5] = "";
        _text[116, 6] = "";
        _text[116, 7] = "";
        _text[116, 8] = "";
        _text[116, 9] = "";

        _text[117, 0] = "Anti-Aliasing";
        _text[117, 1] = "Сглаживание";
        _text[117, 2] = "";
        _text[117, 3] = "";
        _text[117, 4] = "";
        _text[117, 5] = "";
        _text[117, 6] = "";
        _text[117, 7] = "";
        _text[117, 8] = "";
        _text[117, 9] = "";

        _text[118, 0] = "Upscaling Filter";
        _text[118, 1] = "Масштабирование";
        _text[118, 2] = "";
        _text[118, 3] = "";
        _text[118, 4] = "";
        _text[118, 5] = "";
        _text[118, 6] = "";
        _text[118, 7] = "";
        _text[118, 8] = "";
        _text[118, 9] = "";

        _text[119, 0] = "Glow";
        _text[119, 1] = "Свечение";
        _text[119, 2] = "";
        _text[119, 3] = "";
        _text[119, 4] = "";
        _text[119, 5] = "";
        _text[119, 6] = "";
        _text[119, 7] = "";
        _text[119, 8] = "";
        _text[119, 9] = "";

        _text[120, 0] = "Max. Frame Rate";
        _text[120, 1] = "Макс. Кол-во Кадров";
        _text[120, 2] = "";
        _text[120, 3] = "";
        _text[120, 4] = "";
        _text[120, 5] = "";
        _text[120, 6] = "";
        _text[120, 7] = "";
        _text[120, 8] = "";
        _text[120, 9] = "";

        _text[121, 0] = "Close";
        _text[121, 1] = "Закрыть";
        _text[121, 2] = "";
        _text[121, 3] = "";
        _text[121, 4] = "";
        _text[121, 5] = "";
        _text[121, 6] = "";
        _text[121, 7] = "";
        _text[121, 8] = "";
        _text[121, 9] = "";

        _text[122, 0] = "Apply";
        _text[122, 1] = "Применить";
        _text[122, 2] = "";
        _text[122, 3] = "";
        _text[122, 4] = "";
        _text[122, 5] = "";
        _text[122, 6] = "";
        _text[122, 7] = "";
        _text[122, 8] = "";
        _text[122, 9] = "";

        _text[123, 0] = "Reset";
        _text[123, 1] = "Сброс";
        _text[123, 2] = "";
        _text[123, 3] = "";
        _text[123, 4] = "";
        _text[123, 5] = "";
        _text[123, 6] = "";
        _text[123, 7] = "";
        _text[123, 8] = "";
        _text[123, 9] = "";

        _text[124, 0] = "Full-screen";
        _text[124, 1] = "Полноэкранный";
        _text[124, 2] = "";
        _text[124, 3] = "";
        _text[124, 4] = "";
        _text[124, 5] = "";
        _text[124, 6] = "";
        _text[124, 7] = "";
        _text[124, 8] = "";
        _text[124, 9] = "";

        _text[125, 0] = "Windowed";
        _text[125, 1] = "Оконный";
        _text[125, 2] = "";
        _text[125, 3] = "";
        _text[125, 4] = "";
        _text[125, 5] = "";
        _text[125, 6] = "";
        _text[125, 7] = "";
        _text[125, 8] = "";
        _text[125, 9] = "";

        _text[126, 0] = "Low";
        _text[126, 1] = "Низкое";
        _text[126, 2] = "";
        _text[126, 3] = "";
        _text[126, 4] = "";
        _text[126, 5] = "";
        _text[126, 6] = "";
        _text[126, 7] = "";
        _text[126, 8] = "";
        _text[126, 9] = "";

        _text[127, 0] = "Medium";
        _text[127, 1] = "Среднее";
        _text[127, 2] = "";
        _text[127, 3] = "";
        _text[127, 4] = "";
        _text[127, 5] = "";
        _text[127, 6] = "";
        _text[127, 7] = "";
        _text[127, 8] = "";
        _text[127, 9] = "";

        _text[128, 0] = "High";
        _text[128, 1] = "Высокое";
        _text[128, 2] = "";
        _text[128, 3] = "";
        _text[128, 4] = "";
        _text[128, 5] = "";
        _text[128, 6] = "";
        _text[128, 7] = "";
        _text[128, 8] = "";
        _text[128, 9] = "";

        _text[129, 0] = "Ultra";
        _text[129, 1] = "Ультра";
        _text[129, 2] = "";
        _text[129, 3] = "";
        _text[129, 4] = "";
        _text[129, 5] = "";
        _text[129, 6] = "";
        _text[129, 7] = "";
        _text[129, 8] = "";
        _text[129, 9] = "";

        _text[130, 0] = "Disabled";
        _text[130, 1] = "Выключено";
        _text[130, 2] = "";
        _text[130, 3] = "";
        _text[130, 4] = "";
        _text[130, 5] = "";
        _text[130, 6] = "";
        _text[130, 7] = "";
        _text[130, 8] = "";
        _text[130, 9] = "";

        _text[131, 0] = "Bilinear";
        _text[131, 1] = "Билинейное";
        _text[131, 2] = "";
        _text[131, 3] = "";
        _text[131, 4] = "";
        _text[131, 5] = "";
        _text[131, 6] = "";
        _text[131, 7] = "";
        _text[131, 8] = "";
        _text[131, 9] = "";

        _text[132, 0] = "Nearest";
        _text[132, 1] = "Ближайшее";
        _text[132, 2] = "";
        _text[132, 3] = "";
        _text[132, 4] = "";
        _text[132, 5] = "";
        _text[132, 6] = "";
        _text[132, 7] = "";
        _text[132, 8] = "";
        _text[132, 9] = "";

        _text[133, 0] = "Camera movement";
        _text[133, 1] = "Движение камеры";
        _text[133, 2] = "";
        _text[133, 3] = "";
        _text[133, 4] = "";
        _text[133, 5] = "";
        _text[133, 6] = "";
        _text[133, 7] = "";
        _text[133, 8] = "";
        _text[133, 9] = "";

        _text[134, 0] = "Camera zoom";
        _text[134, 1] = "Масштаб камеры";
        _text[134, 2] = "";
        _text[134, 3] = "";
        _text[134, 4] = "";
        _text[134, 5] = "";
        _text[134, 6] = "";
        _text[134, 7] = "";
        _text[134, 8] = "";
        _text[134, 9] = "";

        _text[135, 0] = "Select tile / card";
        _text[135, 1] = "Выбор тайла / карты";
        _text[135, 2] = "";
        _text[135, 3] = "";
        _text[135, 4] = "";
        _text[135, 5] = "";
        _text[135, 6] = "";
        _text[135, 7] = "";
        _text[135, 8] = "";
        _text[135, 9] = "";

        _text[136, 0] = "Unselect tile / card";
        _text[136, 1] = "Отмена выбора тайла / карты";
        _text[136, 2] = "";
        _text[136, 3] = "";
        _text[136, 4] = "";
        _text[136, 5] = "";
        _text[136, 6] = "";
        _text[136, 7] = "";
        _text[136, 8] = "";
        _text[136, 9] = "";

        _text[137, 0] = "Game speed: pause";
        _text[137, 1] = "Скорость игры: пауза";
        _text[137, 2] = "";
        _text[137, 3] = "";
        _text[137, 4] = "";
        _text[137, 5] = "";
        _text[137, 6] = "";
        _text[137, 7] = "";
        _text[137, 8] = "";
        _text[137, 9] = "";

        _text[138, 0] = "Game speed: normal";
        _text[138, 1] = "Скорость игры: нормальная";
        _text[138, 2] = "";
        _text[138, 3] = "";
        _text[138, 4] = "";
        _text[138, 5] = "";
        _text[138, 6] = "";
        _text[138, 7] = "";
        _text[138, 8] = "";
        _text[138, 9] = "";

        _text[139, 0] = "Game speed: double";
        _text[139, 1] = "Скорость игры: двойная";
        _text[139, 2] = "";
        _text[139, 3] = "";
        _text[139, 4] = "";
        _text[139, 5] = "";
        _text[139, 6] = "";
        _text[139, 7] = "";
        _text[139, 8] = "";
        _text[139, 9] = "";

        _text[140, 0] = "Game speed: triple";
        _text[140, 1] = "Скорость игры: тройная";
        _text[140, 2] = "";
        _text[140, 3] = "";
        _text[140, 4] = "";
        _text[140, 5] = "";
        _text[140, 6] = "";
        _text[140, 7] = "";
        _text[140, 8] = "";
        _text[140, 9] = "";

        _text[141, 0] = "Menu";
        _text[141, 1] = "Меню";
        _text[141, 2] = "";
        _text[141, 3] = "";
        _text[141, 4] = "";
        _text[141, 5] = "";
        _text[141, 6] = "";
        _text[141, 7] = "";
        _text[141, 8] = "";
        _text[141, 9] = "";

        _text[142, 0] = "Build on tile";
        _text[142, 1] = "Построить на тайле";
        _text[142, 2] = "";
        _text[142, 3] = "";
        _text[142, 4] = "";
        _text[142, 5] = "";
        _text[142, 6] = "";
        _text[142, 7] = "";
        _text[142, 8] = "";
        _text[142, 9] = "";

        _text[143, 0] = "Rotate tile / building";
        _text[143, 1] = "Повернуть тайл / здание";
        _text[143, 2] = "";
        _text[143, 3] = "";
        _text[143, 4] = "";
        _text[143, 5] = "";
        _text[143, 6] = "";
        _text[143, 7] = "";
        _text[143, 8] = "";
        _text[143, 9] = "";

        _text[144, 0] = "Destroy tile / building";
        _text[144, 1] = "Уничтожить тайл / здание";
        _text[144, 2] = "";
        _text[144, 3] = "";
        _text[144, 4] = "";
        _text[144, 5] = "";
        _text[144, 6] = "";
        _text[144, 7] = "";
        _text[144, 8] = "";
        _text[144, 9] = "";

        _text[145, 0] = "Toggle building";
        _text[145, 1] = "Включить / выключить здание";
        _text[145, 2] = "";
        _text[145, 3] = "";
        _text[145, 4] = "";
        _text[145, 5] = "";
        _text[145, 6] = "";
        _text[145, 7] = "";
        _text[145, 8] = "";
        _text[145, 9] = "";

        _text[146, 0] = "Open machine panel";
        _text[146, 1] = "Открыть панель машин";
        _text[146, 2] = "";
        _text[146, 3] = "";
        _text[146, 4] = "";
        _text[146, 5] = "";
        _text[146, 6] = "";
        _text[146, 7] = "";
        _text[146, 8] = "";
        _text[146, 9] = "";

        _text[147, 0] = "Data restored:";
        _text[147, 1] = "Восстановлено данных";
        _text[147, 2] = "";
        _text[147, 3] = "";
        _text[147, 4] = "";
        _text[147, 5] = "";
        _text[147, 6] = "";
        _text[147, 7] = "";
        _text[147, 8] = "";
        _text[147, 9] = "";

        _text[148, 0] = "Ecology bonus:";
        _text[148, 1] = "Экологический бонус";
        _text[148, 2] = "";
        _text[148, 3] = "";
        _text[148, 4] = "";
        _text[148, 5] = "";
        _text[148, 6] = "";
        _text[148, 7] = "";
        _text[148, 8] = "";
        _text[148, 9] = "";

        _text[149, 0] = "Data fragments received";
        _text[149, 1] = "Получено фрагментов данных";
        _text[149, 2] = "";
        _text[149, 3] = "";
        _text[149, 4] = "";
        _text[149, 5] = "";
        _text[149, 6] = "";
        _text[149, 7] = "";
        _text[149, 8] = "";
        _text[149, 9] = "";

        _text[150, 0] = "Defeat the boss: {0}/{1}";
        _text[150, 1] = "Победить босса: {0}/{1}";
        _text[150, 2] = "";
        _text[150, 3] = "";
        _text[150, 4] = "";
        _text[150, 5] = "";
        _text[150, 6] = "";
        _text[150, 7] = "";
        _text[150, 8] = "";
        _text[150, 9] = "";

        _text[151, 0] = "Defeat the boss";
        _text[151, 1] = "Победить босса";
        _text[151, 2] = "";
        _text[151, 3] = "";
        _text[151, 4] = "";
        _text[151, 5] = "";
        _text[151, 6] = "";
        _text[151, 7] = "";
        _text[151, 8] = "";
        _text[151, 9] = "";

        _text[152, 0] = "Resources for construction:";
        _text[152, 1] = "Ресурсы для строительства:";
        _text[152, 2] = "";
        _text[152, 3] = "";
        _text[152, 4] = "";
        _text[152, 5] = "";
        _text[152, 6] = "";
        _text[152, 7] = "";
        _text[152, 8] = "";
        _text[152, 9] = "";

        _text[153, 0] = "Wood";
        _text[153, 1] = "Древесина";
        _text[153, 2] = "";
        _text[153, 3] = "";
        _text[153, 4] = "";
        _text[153, 5] = "";
        _text[153, 6] = "";
        _text[153, 7] = "";
        _text[153, 8] = "";
        _text[153, 9] = "";

        _text[154, 0] = "Stone";
        _text[154, 1] = "Камень";
        _text[154, 2] = "";
        _text[154, 3] = "";
        _text[154, 4] = "";
        _text[154, 5] = "";
        _text[154, 6] = "";
        _text[154, 7] = "";
        _text[154, 8] = "";
        _text[154, 9] = "";

        _text[155, 0] = "Iron Ore";
        _text[155, 1] = "Железная Руда";
        _text[155, 2] = "";
        _text[155, 3] = "";
        _text[155, 4] = "";
        _text[155, 5] = "";
        _text[155, 6] = "";
        _text[155, 7] = "";
        _text[155, 8] = "";
        _text[155, 9] = "";

        _text[156, 0] = "Copper Ore";
        _text[156, 1] = "Медная Руда";
        _text[156, 2] = "";
        _text[156, 3] = "";
        _text[156, 4] = "";
        _text[156, 5] = "";
        _text[156, 6] = "";
        _text[156, 7] = "";
        _text[156, 8] = "";
        _text[156, 9] = "";

        _text[157, 0] = "Coal";
        _text[157, 1] = "Уголь";
        _text[157, 2] = "";
        _text[157, 3] = "";
        _text[157, 4] = "";
        _text[157, 5] = "";
        _text[157, 6] = "";
        _text[157, 7] = "";
        _text[157, 8] = "";
        _text[157, 9] = "";

        _text[158, 0] = "Oil";
        _text[158, 1] = "Нефть";
        _text[158, 2] = "";
        _text[158, 3] = "";
        _text[158, 4] = "";
        _text[158, 5] = "";
        _text[158, 6] = "";
        _text[158, 7] = "";
        _text[158, 8] = "";
        _text[158, 9] = "";

        _text[159, 0] = "Water";
        _text[159, 1] = "Вода";
        _text[159, 2] = "";
        _text[159, 3] = "";
        _text[159, 4] = "";
        _text[159, 5] = "";
        _text[159, 6] = "";
        _text[159, 7] = "";
        _text[159, 8] = "";
        _text[159, 9] = "";

        _text[160, 0] = "Sand";
        _text[160, 1] = "Песок";
        _text[160, 2] = "";
        _text[160, 3] = "";
        _text[160, 4] = "";
        _text[160, 5] = "";
        _text[160, 6] = "";
        _text[160, 7] = "";
        _text[160, 8] = "";
        _text[160, 9] = "";

        _text[161, 0] = "Electricity";
        _text[161, 1] = "Электричество";
        _text[161, 2] = "";
        _text[161, 3] = "";
        _text[161, 4] = "";
        _text[161, 5] = "";
        _text[161, 6] = "";
        _text[161, 7] = "";
        _text[161, 8] = "";
        _text[161, 9] = "";

        _text[162, 0] = "Stone Block";
        _text[162, 1] = "Каменный Блок";
        _text[162, 2] = "";
        _text[162, 3] = "";
        _text[162, 4] = "";
        _text[162, 5] = "";
        _text[162, 6] = "";
        _text[162, 7] = "";
        _text[162, 8] = "";
        _text[162, 9] = "";

        _text[163, 0] = "Iron Ingot";
        _text[163, 1] = "Слиток Железа";
        _text[163, 2] = "";
        _text[163, 3] = "";
        _text[163, 4] = "";
        _text[163, 5] = "";
        _text[163, 6] = "";
        _text[163, 7] = "";
        _text[163, 8] = "";
        _text[163, 9] = "";

        _text[164, 0] = "Steel Ingot";
        _text[164, 1] = "Слиток Стали";
        _text[164, 2] = "";
        _text[164, 3] = "";
        _text[164, 4] = "";
        _text[164, 5] = "";
        _text[164, 6] = "";
        _text[164, 7] = "";
        _text[164, 8] = "";
        _text[164, 9] = "";

        _text[165, 0] = "Copper Plate";
        _text[165, 1] = "Медная Пластина";
        _text[165, 2] = "";
        _text[165, 3] = "";
        _text[165, 4] = "";
        _text[165, 5] = "";
        _text[165, 6] = "";
        _text[165, 7] = "";
        _text[165, 8] = "";
        _text[165, 9] = "";

        _text[166, 0] = "Concrete";
        _text[166, 1] = "Бетон";
        _text[166, 2] = "";
        _text[166, 3] = "";
        _text[166, 4] = "";
        _text[166, 5] = "";
        _text[166, 6] = "";
        _text[166, 7] = "";
        _text[166, 8] = "";
        _text[166, 9] = "";

        _text[167, 0] = "Steam";
        _text[167, 1] = "Пар";
        _text[167, 2] = "";
        _text[167, 3] = "";
        _text[167, 4] = "";
        _text[167, 5] = "";
        _text[167, 6] = "";
        _text[167, 7] = "";
        _text[167, 8] = "";
        _text[167, 9] = "";

        _text[168, 0] = "Glass";
        _text[168, 1] = "Стекло";
        _text[168, 2] = "";
        _text[168, 3] = "";
        _text[168, 4] = "";
        _text[168, 5] = "";
        _text[168, 6] = "";
        _text[168, 7] = "";
        _text[168, 8] = "";
        _text[168, 9] = "";

        _text[169, 0] = "Copper Wire";
        _text[169, 1] = "Медный Провод";
        _text[169, 2] = "";
        _text[169, 3] = "";
        _text[169, 4] = "";
        _text[169, 5] = "";
        _text[169, 6] = "";
        _text[169, 7] = "";
        _text[169, 8] = "";
        _text[169, 9] = "";

        _text[170, 0] = "Gear Wheel";
        _text[170, 1] = "Шестерня";
        _text[170, 2] = "";
        _text[170, 3] = "";
        _text[170, 4] = "";
        _text[170, 5] = "";
        _text[170, 6] = "";
        _text[170, 7] = "";
        _text[170, 8] = "";
        _text[170, 9] = "";

        _text[171, 0] = "Electronic Circuit";
        _text[171, 1] = "Электросхема";
        _text[171, 2] = "";
        _text[171, 3] = "";
        _text[171, 4] = "";
        _text[171, 5] = "";
        _text[171, 6] = "";
        _text[171, 7] = "";
        _text[171, 8] = "";
        _text[171, 9] = "";

        _text[172, 0] = "Processor";
        _text[172, 1] = "Процессор";
        _text[172, 2] = "";
        _text[172, 3] = "";
        _text[172, 4] = "";
        _text[172, 5] = "";
        _text[172, 6] = "";
        _text[172, 7] = "";
        _text[172, 8] = "";
        _text[172, 9] = "";

        _text[173, 0] = "Engine";
        _text[173, 1] = "Двигатель";
        _text[173, 2] = "";
        _text[173, 3] = "";
        _text[173, 4] = "";
        _text[173, 5] = "";
        _text[173, 6] = "";
        _text[173, 7] = "";
        _text[173, 8] = "";
        _text[173, 9] = "";

        _text[174, 0] = "Electric Engine";
        _text[174, 1] = "Электродвигатель";
        _text[174, 2] = "";
        _text[174, 3] = "";
        _text[174, 4] = "";
        _text[174, 5] = "";
        _text[174, 6] = "";
        _text[174, 7] = "";
        _text[174, 8] = "";
        _text[174, 9] = "";

        _text[175, 0] = "Data Fragment";
        _text[175, 1] = "Фрагмент Данных";
        _text[175, 2] = "";
        _text[175, 3] = "";
        _text[175, 4] = "";
        _text[175, 5] = "";
        _text[175, 6] = "";
        _text[175, 7] = "";
        _text[175, 8] = "";
        _text[175, 9] = "";

        _text[176, 0] = "Beam Energy";
        _text[176, 1] = "Энергия Луча";
        _text[176, 2] = "";
        _text[176, 3] = "";
        _text[176, 4] = "";
        _text[176, 5] = "";
        _text[176, 6] = "";
        _text[176, 7] = "";
        _text[176, 8] = "";
        _text[176, 9] = "";

        _text[177, 0] = "Mark / Remove from general repair";
        _text[177, 1] = "Пометить / Снять с общего ремонта";
        _text[177, 2] = "";
        _text[177, 3] = "";
        _text[177, 4] = "";
        _text[177, 5] = "";
        _text[177, 6] = "";
        _text[177, 7] = "";
        _text[177, 8] = "";
        _text[177, 9] = "";

        _text[178, 0] = "General repair";
        _text[178, 1] = "Общий ремонт";
        _text[178, 2] = "";
        _text[178, 3] = "";
        _text[178, 4] = "";
        _text[178, 5] = "";
        _text[178, 6] = "";
        _text[178, 7] = "";
        _text[178, 8] = "";
        _text[178, 9] = "";

        _text[179, 0] = "Skills";
        _text[179, 1] = "Умения";
        _text[179, 2] = "";
        _text[179, 3] = "";
        _text[179, 4] = "";
        _text[179, 5] = "";
        _text[179, 6] = "";
        _text[179, 7] = "";
        _text[179, 8] = "";
        _text[179, 9] = "";

        _text[180, 0] = "Description";
        _text[180, 1] = "Описание";
        _text[180, 2] = "";
        _text[180, 3] = "";
        _text[180, 4] = "";
        _text[180, 5] = "";
        _text[180, 6] = "";
        _text[180, 7] = "";
        _text[180, 8] = "";
        _text[180, 9] = "";

        _text[181, 0] = "Requires resources to repair them.";
        _text[181, 1] = "Требуются ресурсы для их починки";
        _text[181, 2] = "";
        _text[181, 3] = "";
        _text[181, 4] = "";
        _text[181, 5] = "";
        _text[181, 6] = "";
        _text[181, 7] = "";
        _text[181, 8] = "";
        _text[181, 9] = "";

        _text[182, 0] = "Required";
        _text[182, 1] = "Требуется";
        _text[182, 2] = "";
        _text[182, 3] = "";
        _text[182, 4] = "";
        _text[182, 5] = "";
        _text[182, 6] = "";
        _text[182, 7] = "";
        _text[182, 8] = "";
        _text[182, 9] = "";

        _text[183, 0] = "You have received";
        _text[183, 1] = "Вы получили";
        _text[183, 2] = "";
        _text[183, 3] = "";
        _text[183, 4] = "";
        _text[183, 5] = "";
        _text[183, 6] = "";
        _text[183, 7] = "";
        _text[183, 8] = "";
        _text[183, 9] = "";

        _text[184, 0] = "You have lost";
        _text[184, 1] = "Вы потеряли";
        _text[184, 2] = "";
        _text[184, 3] = "";
        _text[184, 4] = "";
        _text[184, 5] = "";
        _text[184, 6] = "";
        _text[184, 7] = "";
        _text[184, 8] = "";
        _text[184, 9] = "";

        _text[185, 0] = "Ai Core";
        _text[185, 1] = "Ядро ИИ";
        _text[185, 2] = "";
        _text[185, 3] = "";
        _text[185, 4] = "";
        _text[185, 5] = "";
        _text[185, 6] = "";
        _text[185, 7] = "";
        _text[185, 8] = "";
        _text[185, 9] = "";

        _text[186, 0] = "Quant";
        _text[186, 1] = "Квант";
        _text[186, 2] = "";
        _text[186, 3] = "";
        _text[186, 4] = "";
        _text[186, 5] = "";
        _text[186, 6] = "";
        _text[186, 7] = "";
        _text[186, 8] = "";
        _text[186, 9] = "";

        _text[187, 0] = "Quant received";
        _text[187, 1] = "Получено квант";
        _text[187, 2] = "";
        _text[187, 3] = "";
        _text[187, 4] = "";
        _text[187, 5] = "";
        _text[187, 6] = "";
        _text[187, 7] = "";
        _text[187, 8] = "";
        _text[187, 9] = "";

        _text[188, 0] = "Scout";
        _text[188, 1] = "Разведчик";
        _text[188, 2] = "";
        _text[188, 3] = "";
        _text[188, 4] = "";
        _text[188, 5] = "";
        _text[188, 6] = "";
        _text[188, 7] = "";
        _text[188, 8] = "";
        _text[188, 9] = "";

        _text[189, 0] = "Engineer";
        _text[189, 1] = "Инженер";
        _text[189, 2] = "";
        _text[189, 3] = "";
        _text[189, 4] = "";
        _text[189, 5] = "";
        _text[189, 6] = "";
        _text[189, 7] = "";
        _text[189, 8] = "";
        _text[189, 9] = "";

        _text[190, 0] = "Patch-08";
        _text[190, 1] = "Патч-08";
        _text[190, 2] = "";
        _text[190, 3] = "";
        _text[190, 4] = "";
        _text[190, 5] = "";
        _text[190, 6] = "";
        _text[190, 7] = "";
        _text[190, 8] = "";
        _text[190, 9] = "";

        _text[191, 0] = "Aim Bot";
        _text[191, 1] = "Аим Бот";
        _text[191, 2] = "";
        _text[191, 3] = "";
        _text[191, 4] = "";
        _text[191, 5] = "";
        _text[191, 6] = "";
        _text[191, 7] = "";
        _text[191, 8] = "";
        _text[191, 9] = "";

        _text[192, 0] = "Titan";
        _text[192, 1] = "Титан";
        _text[192, 2] = "";
        _text[192, 3] = "";
        _text[192, 4] = "";
        _text[192, 5] = "";
        _text[192, 6] = "";
        _text[192, 7] = "";
        _text[192, 8] = "";
        _text[192, 9] = "";

        _text[193, 0] = "Functional";
        _text[193, 1] = "Функционал";
        _text[193, 2] = "";
        _text[193, 3] = "";
        _text[193, 4] = "";
        _text[193, 5] = "";
        _text[193, 6] = "";
        _text[193, 7] = "";
        _text[193, 8] = "";
        _text[193, 9] = "";

        _text[194, 0] = "unknown";
        _text[194, 1] = "неизвестно";
        _text[194, 2] = "";
        _text[194, 3] = "";
        _text[194, 4] = "";
        _text[194, 5] = "";
        _text[194, 6] = "";
        _text[194, 7] = "";
        _text[194, 8] = "";
        _text[194, 9] = "";

        _text[195, 0] = "explores the surrounding area in search of resources";
        _text[195, 1] = "исследует окрестности в поисках ресурсов";
        _text[195, 2] = "";
        _text[195, 3] = "";
        _text[195, 4] = "";
        _text[195, 5] = "";
        _text[195, 6] = "";
        _text[195, 7] = "";
        _text[195, 8] = "";
        _text[195, 9] = "";

        _text[196, 0] = "repairs the specified buildings";
        _text[196, 1] = "ремонтирует указанные здания";
        _text[196, 2] = "";
        _text[196, 3] = "";
        _text[196, 4] = "";
        _text[196, 5] = "";
        _text[196, 6] = "";
        _text[196, 7] = "";
        _text[196, 8] = "";
        _text[196, 9] = "";

        _text[197, 0] = "attacks enemy creatures";
        _text[197, 1] = "атакует вражеских существ";
        _text[197, 2] = "";
        _text[197, 3] = "";
        _text[197, 4] = "";
        _text[197, 5] = "";
        _text[197, 6] = "";
        _text[197, 7] = "";
        _text[197, 8] = "";
        _text[197, 9] = "";

        _text[198, 0] = "Combat";
        _text[198, 1] = "Боевой";
        _text[198, 2] = "";
        _text[198, 3] = "";
        _text[198, 4] = "";
        _text[198, 5] = "";
        _text[198, 6] = "";
        _text[198, 7] = "";
        _text[198, 8] = "";
        _text[198, 9] = "";

        _text[199, 0] = "Base Crate";
        _text[199, 1] = "Базовый Контейнер";
        _text[199, 2] = "";
        _text[199, 3] = "";
        _text[199, 4] = "";
        _text[199, 5] = "";
        _text[199, 6] = "";
        _text[199, 7] = "";
        _text[199, 8] = "";
        _text[199, 9] = "";

        _text[200, 0] = "Metal Crate";
        _text[200, 1] = "Металлический Контейнер";
        _text[200, 2] = "";
        _text[200, 3] = "";
        _text[200, 4] = "";
        _text[200, 5] = "";
        _text[200, 6] = "";
        _text[200, 7] = "";
        _text[200, 8] = "";
        _text[200, 9] = "";

        _text[201, 0] = "Supply Crate";
        _text[201, 1] = "Контейнер Снабжения";
        _text[201, 2] = "";
        _text[201, 3] = "";
        _text[201, 4] = "";
        _text[201, 5] = "";
        _text[201, 6] = "";
        _text[201, 7] = "";
        _text[201, 8] = "";
        _text[201, 9] = "";

        _text[202, 0] = "Crates";
        _text[202, 1] = "Контейнеры";
        _text[202, 2] = "";
        _text[202, 3] = "";
        _text[202, 4] = "";
        _text[202, 5] = "";
        _text[202, 6] = "";
        _text[202, 7] = "";
        _text[202, 8] = "";
        _text[202, 9] = "";

        _text[203, 0] = "The radiation level is starting to increase gradually. Be careful.";
        _text[203, 1] = "Уровень радиации начинает постепенно расти. Будьте осторожны.";
        _text[203, 2] = "";
        _text[203, 3] = "";
        _text[203, 4] = "";
        _text[203, 5] = "";
        _text[203, 6] = "";
        _text[203, 7] = "";
        _text[203, 8] = "";
        _text[203, 9] = "";

        _text[204, 0] = "Average increase in background radiation registered. Prepare for possible consequences.";
        _text[204, 1] = "Зарегистрирован средний рост радиационного фона. Подготовьтесь к возможным последствиям.";
        _text[204, 2] = "";
        _text[204, 3] = "";
        _text[204, 4] = "";
        _text[204, 5] = "";
        _text[204, 6] = "";
        _text[204, 7] = "";
        _text[204, 8] = "";
        _text[204, 9] = "";

        _text[205, 0] = "Warning! A sharp increase in radiation is expected. Take protective measures immediately.";
        _text[205, 1] = "Внимание! Ожидается резкий скачок радиации. Срочно примите защитные меры.";
        _text[205, 2] = "";
        _text[205, 3] = "";
        _text[205, 4] = "";
        _text[205, 5] = "";
        _text[205, 6] = "";
        _text[205, 7] = "";
        _text[205, 8] = "";
        _text[205, 9] = "";

        _text[206, 0] = "The radiation level is gradually decreasing, making conditions safer.";
        _text[206, 1] = "Уровень радиации постепенно снижается – условия становятся безопаснее.";
        _text[206, 2] = "";
        _text[206, 3] = "";
        _text[206, 4] = "";
        _text[206, 5] = "";
        _text[206, 6] = "";
        _text[206, 7] = "";
        _text[206, 8] = "";
        _text[206, 9] = "";

        _text[207, 0] = "Average decrease in radiation level recorded. Threat level is falling.";
        _text[207, 1] = "Среднее снижение уровня радиации зафиксировано. Уровень угрозы падает.";
        _text[207, 2] = "";
        _text[207, 3] = "";
        _text[207, 4] = "";
        _text[207, 5] = "";
        _text[207, 6] = "";
        _text[207, 7] = "";
        _text[207, 8] = "";
        _text[207, 9] = "";

        _text[208, 0] = "A sharp drop in radiation has been recorded. The environment is being restored.";
        _text[208, 1] = "Зафиксировано резкое падение радиации. Окружающая среда восстанавливается.";
        _text[208, 2] = "";
        _text[208, 3] = "";
        _text[208, 4] = "";
        _text[208, 5] = "";
        _text[208, 6] = "";
        _text[208, 7] = "";
        _text[208, 8] = "";
        _text[208, 9] = "";

        _text[209, 0] = "Precipitation analysis indicates high acidity. Rain is expected.";
        _text[209, 1] = "Анализ осадков указывает на высокую кислотность. Ожидается дождь.";
        _text[209, 2] = "";
        _text[209, 3] = "";
        _text[209, 4] = "";
        _text[209, 5] = "";
        _text[209, 6] = "";
        _text[209, 7] = "";
        _text[209, 8] = "";
        _text[209, 9] = "";

        _text[210, 0] = "Orbital scanners have detected a meteor shower - prepare for strikes from the skies.";
        _text[210, 1] = "Орбитальные сканеры выявили метеорный поток - готовьтесь к ударам с небес.";
        _text[210, 2] = "";
        _text[210, 3] = "";
        _text[210, 4] = "";
        _text[210, 5] = "";
        _text[210, 6] = "";
        _text[210, 7] = "";
        _text[210, 8] = "";
        _text[210, 9] = "";

        _text[211, 0] = "Seismic sensors are recording powerful tremors – an earthquake is approaching.";
        _text[211, 1] = "Сейсмические датчики фиксируют мощные подземные толчки – приближается землетрясение.";
        _text[211, 2] = "";
        _text[211, 3] = "";
        _text[211, 4] = "";
        _text[211, 5] = "";
        _text[211, 6] = "";
        _text[211, 7] = "";
        _text[211, 8] = "";
        _text[211, 9] = "";

        _text[212, 0] = "Toxic compounds have been detected in the atmosphere. The wind is carrying a dangerous gas.";
        _text[212, 1] = "В атмосфере обнаружены токсичные соединения. Ветер несёт опасный газ.";
        _text[212, 2] = "";
        _text[212, 3] = "";
        _text[212, 4] = "";
        _text[212, 5] = "";
        _text[212, 6] = "";
        _text[212, 7] = "";
        _text[212, 8] = "";
        _text[212, 9] = "";

        _text[213, 0] = "Underground pressure is increasing. A spontaneous release of oil to the surface is possible.";
        _text[213, 1] = "Подземное давление растёт. Возможен самопроизвольный выброс нефти на поверхность.";
        _text[213, 2] = "";
        _text[213, 3] = "";
        _text[213, 4] = "";
        _text[213, 5] = "";
        _text[213, 6] = "";
        _text[213, 7] = "";
        _text[213, 8] = "";
        _text[213, 9] = "";

        _text[214, 0] = "Repairs all marked buildings.";
        _text[214, 1] = "Ремонтирует все помеченные здания.";
        _text[214, 2] = "";
        _text[214, 3] = "";
        _text[214, 4] = "";
        _text[214, 5] = "";
        _text[214, 6] = "";
        _text[214, 7] = "";
        _text[214, 8] = "";
        _text[214, 9] = "";

        _text[215, 0] = "Fortification";
        _text[215, 1] = "Укрепление";
        _text[215, 2] = "";
        _text[215, 3] = "";
        _text[215, 4] = "";
        _text[215, 5] = "";
        _text[215, 6] = "";
        _text[215, 7] = "";
        _text[215, 8] = "";
        _text[215, 9] = "";

        _text[216, 0] = "For one day, reduces damage to all buildings by 2 times.";
        _text[216, 1] = "На один день, уменьшает урон по всем зданиям в 2 раза.";
        _text[216, 2] = "";
        _text[216, 3] = "";
        _text[216, 4] = "";
        _text[216, 5] = "";
        _text[216, 6] = "";
        _text[216, 7] = "";
        _text[216, 8] = "";
        _text[216, 9] = "";

        _text[217, 0] = "Production optimization";
        _text[217, 1] = "Оптимизация производства";
        _text[217, 2] = "";
        _text[217, 3] = "";
        _text[217, 4] = "";
        _text[217, 5] = "";
        _text[217, 6] = "";
        _text[217, 7] = "";
        _text[217, 8] = "";
        _text[217, 9] = "";

        _text[218, 0] = "For one day, increases resource production by 2 times.";
        _text[218, 1] = "На один день, увеличивает добычу ресурсов в 2 раза.";
        _text[218, 2] = "";
        _text[218, 3] = "";
        _text[218, 4] = "";
        _text[218, 5] = "";
        _text[218, 6] = "";
        _text[218, 7] = "";
        _text[218, 8] = "";
        _text[218, 9] = "";

        _text[219, 0] = "Ignite";
        _text[219, 1] = "Поджег";
        _text[219, 2] = "";
        _text[219, 3] = "";
        _text[219, 4] = "";
        _text[219, 5] = "";
        _text[219, 6] = "";
        _text[219, 7] = "";
        _text[219, 8] = "";
        _text[219, 9] = "";

        _text[220, 0] = "Creates uncontrollable flames. Deals damage to both enemies and your buildings.";
        _text[220, 1] = "Создает неконтролируемое пламя. Наносит урон как врагам, так и вашим постройкам.";
        _text[220, 2] = "";
        _text[220, 3] = "";
        _text[220, 4] = "";
        _text[220, 5] = "";
        _text[220, 6] = "";
        _text[220, 7] = "";
        _text[220, 8] = "";
        _text[220, 9] = "";

        _text[221, 0] = "Mountain";
        _text[221, 1] = "Гора";
        _text[221, 2] = "";
        _text[221, 3] = "";
        _text[221, 4] = "";
        _text[221, 5] = "";
        _text[221, 6] = "";
        _text[221, 7] = "";
        _text[221, 8] = "";
        _text[221, 9] = "";

        _text[222, 0] = "Toggle resources panel";
        _text[222, 1] = "Переключает панель ресурсов";
        _text[222, 2] = "";
        _text[222, 3] = "";
        _text[222, 4] = "";
        _text[222, 5] = "";
        _text[222, 6] = "";
        _text[222, 7] = "";
        _text[222, 8] = "";
        _text[222, 9] = "";

        _text[223, 0] = "Cancel skill targeting";
        _text[223, 1] = "Отменить прицел умения";
        _text[223, 2] = "";
        _text[223, 3] = "";
        _text[223, 4] = "";
        _text[223, 5] = "";
        _text[223, 6] = "";
        _text[223, 7] = "";
        _text[223, 8] = "";
        _text[223, 9] = "";

        _text[224, 0] = "Toggle skills panel";
        _text[224, 1] = "Переключает панель умений";
        _text[224, 2] = "";
        _text[224, 3] = "";
        _text[224, 4] = "";
        _text[224, 5] = "";
        _text[224, 6] = "";
        _text[224, 7] = "";
        _text[224, 8] = "";
        _text[224, 9] = "";

        _text[225, 0] = "Go";
        _text[225, 1] = "Перейти";
        _text[225, 2] = "";
        _text[225, 3] = "";
        _text[225, 4] = "";
        _text[225, 5] = "";
        _text[225, 6] = "";
        _text[225, 7] = "";
        _text[225, 8] = "";
        _text[225, 9] = "";

        _text[226, 0] = "Collect";
        _text[226, 1] = "Соберите";
        _text[226, 2] = "";
        _text[226, 3] = "";
        _text[226, 4] = "";
        _text[226, 5] = "";
        _text[226, 6] = "";
        _text[226, 7] = "";
        _text[226, 8] = "";
        _text[226, 9] = "";

        _text[227, 0] = "Durability Increased";
        _text[227, 1] = "Прочность Повышена";
        _text[227, 2] = "";
        _text[227, 3] = "";
        _text[227, 4] = "";
        _text[227, 5] = "";
        _text[227, 6] = "";
        _text[227, 7] = "";
        _text[227, 8] = "";
        _text[227, 9] = "";

        _text[228, 0] = "Steel Riffle";
        _text[228, 1] = "Стальная Винтовка";
        _text[228, 2] = "";
        _text[228, 3] = "";
        _text[228, 4] = "";
        _text[228, 5] = "";
        _text[228, 6] = "";
        _text[228, 7] = "";
        _text[228, 8] = "";
        _text[228, 9] = "";

        _text[229, 0] = "Titanium Rocket Launcher";
        _text[229, 1] = "Титановая Ракетная Установка";
        _text[229, 2] = "";
        _text[229, 3] = "";
        _text[229, 4] = "";
        _text[229, 5] = "";
        _text[229, 6] = "";
        _text[229, 7] = "";
        _text[229, 8] = "";
        _text[229, 9] = "";

        _text[230, 0] = "Ammo";
        _text[230, 1] = "Боеприпасы";
        _text[230, 2] = "";
        _text[230, 3] = "";
        _text[230, 4] = "";
        _text[230, 5] = "";
        _text[230, 6] = "";
        _text[230, 7] = "";
        _text[230, 8] = "";
        _text[230, 9] = "";

        _text[231, 0] = "Level";
        _text[231, 1] = "Уровень";
        _text[231, 2] = "";
        _text[231, 3] = "";
        _text[231, 4] = "";
        _text[231, 5] = "";
        _text[231, 6] = "";
        _text[231, 7] = "";
        _text[231, 8] = "";
        _text[231, 9] = "";

        _text[232, 0] = "Upgrade";
        _text[232, 1] = "Улучшить";
        _text[232, 2] = "";
        _text[232, 3] = "";
        _text[232, 4] = "";
        _text[232, 5] = "";
        _text[232, 6] = "";
        _text[232, 7] = "";
        _text[232, 8] = "";
        _text[232, 9] = "";

        _text[233, 0] = "Change Mode";
        _text[233, 1] = "Сменить Режим";
        _text[233, 2] = "";
        _text[233, 3] = "";
        _text[233, 4] = "";
        _text[233, 5] = "";
        _text[233, 6] = "";
        _text[233, 7] = "";
        _text[233, 8] = "";
        _text[233, 9] = "";

        _text[234, 0] = "Act";
        _text[234, 1] = "Акт";
        _text[234, 2] = "";
        _text[234, 3] = "";
        _text[234, 4] = "";
        _text[234, 5] = "";
        _text[234, 6] = "";
        _text[234, 7] = "";
        _text[234, 8] = "";
        _text[234, 9] = "";

        _text[235, 0] = "Node";
        _text[235, 1] = "Узел";
        _text[235, 2] = "";
        _text[235, 3] = "";
        _text[235, 4] = "";
        _text[235, 5] = "";
        _text[235, 6] = "";
        _text[235, 7] = "";
        _text[235, 8] = "";
        _text[235, 9] = "";

        _text[236, 0] = "Success";
        _text[236, 1] = "Успех";
        _text[236, 2] = "";
        _text[236, 3] = "";
        _text[236, 4] = "";
        _text[236, 5] = "";
        _text[236, 6] = "";
        _text[236, 7] = "";
        _text[236, 8] = "";
        _text[236, 9] = "";

        _text[237, 0] = "Failure";
        _text[237, 1] = "Неудача";
        _text[237, 2] = "";
        _text[237, 3] = "";
        _text[237, 4] = "";
        _text[237, 5] = "";
        _text[237, 6] = "";
        _text[237, 7] = "";
        _text[237, 8] = "";
        _text[237, 9] = "";

        _text[238, 0] = "Restored";
        _text[238, 1] = "Восстановлено";
        _text[238, 2] = "";
        _text[238, 3] = "";
        _text[238, 4] = "";
        _text[238, 5] = "";
        _text[238, 6] = "";
        _text[238, 7] = "";
        _text[238, 8] = "";
        _text[238, 9] = "";

        _text[239, 0] = "Scatter Shotgun";
        _text[239, 1] = "Дробовик Рассеиватель";
        _text[239, 2] = "";
        _text[239, 3] = "";
        _text[239, 4] = "";
        _text[239, 5] = "";
        _text[239, 6] = "";
        _text[239, 7] = "";
        _text[239, 8] = "";
        _text[239, 9] = "";

        _text[240, 0] = "Longshot Railgun";
        _text[240, 1] = "Дальнобойный Рельсотрон";
        _text[240, 2] = "";
        _text[240, 3] = "";
        _text[240, 4] = "";
        _text[240, 5] = "";
        _text[240, 6] = "";
        _text[240, 7] = "";
        _text[240, 8] = "";
        _text[240, 9] = "";

        _text[241, 0] = "Breakshot Minigun";
        _text[241, 1] = "Разрывной Пулемет";
        _text[241, 2] = "";
        _text[241, 3] = "";
        _text[241, 4] = "";
        _text[241, 5] = "";
        _text[241, 6] = "";
        _text[241, 7] = "";
        _text[241, 8] = "";
        _text[241, 9] = "";

        _text[242, 0] = "Blastfire Launcher";
        _text[242, 1] = "Взрывопламенная Установка";
        _text[242, 2] = "";
        _text[242, 3] = "";
        _text[242, 4] = "";
        _text[242, 5] = "";
        _text[242, 6] = "";
        _text[242, 7] = "";
        _text[242, 8] = "";
        _text[242, 9] = "";

        _text[243, 0] = "Machine";
        _text[243, 1] = "Машина";
        _text[243, 2] = "";
        _text[243, 3] = "";
        _text[243, 4] = "";
        _text[243, 5] = "";
        _text[243, 6] = "";
        _text[243, 7] = "";
        _text[243, 8] = "";
        _text[243, 9] = "";

        _text[244, 0] = "Resources for create machine:";
        _text[244, 1] = "Ресурсы для создания машины:";
        _text[244, 2] = "";
        _text[244, 3] = "";
        _text[244, 4] = "";
        _text[244, 5] = "";
        _text[244, 6] = "";
        _text[244, 7] = "";
        _text[244, 8] = "";
        _text[244, 9] = "";

        _text[245, 0] = "Ecological Restoration";
        _text[245, 1] = "Восстановление экологии";
        _text[245, 2] = "";
        _text[245, 3] = "";
        _text[245, 4] = "";
        _text[245, 5] = "";
        _text[245, 6] = "";
        _text[245, 7] = "";
        _text[245, 8] = "";
        _text[245, 9] = "";

        _text[246, 0] = "First skill";
        _text[246, 1] = "Первое умение";
        _text[246, 2] = "";
        _text[246, 3] = "";
        _text[246, 4] = "";
        _text[246, 5] = "";
        _text[246, 6] = "";
        _text[246, 7] = "";
        _text[246, 8] = "";
        _text[246, 9] = "";

        _text[247, 0] = "Second skill";
        _text[247, 1] = "Второе умение";
        _text[247, 2] = "";
        _text[247, 3] = "";
        _text[247, 4] = "";
        _text[247, 5] = "";
        _text[247, 6] = "";
        _text[247, 7] = "";
        _text[247, 8] = "";
        _text[247, 9] = "";

        _text[248, 0] = "This button is already in use. Press another.";
        _text[248, 1] = "Данная кнопка уже используется. Нажмите другую.";
        _text[248, 2] = "";
        _text[248, 3] = "";
        _text[248, 4] = "";
        _text[248, 5] = "";
        _text[248, 6] = "";
        _text[248, 7] = "";
        _text[248, 8] = "";
        _text[248, 9] = "";

        _text[249, 0] = "Iron Deposits";
        _text[249, 1] = "Залежи Железа";
        _text[249, 2] = "";
        _text[249, 3] = "";
        _text[249, 4] = "";
        _text[249, 5] = "";
        _text[249, 6] = "";
        _text[249, 7] = "";
        _text[249, 8] = "";
        _text[249, 9] = "";

        _text[250, 0] = "Copper Deposits";
        _text[250, 1] = "Залежи Меди";
        _text[250, 2] = "";
        _text[250, 3] = "";
        _text[250, 4] = "";
        _text[250, 5] = "";
        _text[250, 6] = "";
        _text[250, 7] = "";
        _text[250, 8] = "";
        _text[250, 9] = "";

        _text[251, 0] = "Oil Swamp";
        _text[251, 1] = "Нефтяное Болото";
        _text[251, 2] = "";
        _text[251, 3] = "";
        _text[251, 4] = "";
        _text[251, 5] = "";
        _text[251, 6] = "";
        _text[251, 7] = "";
        _text[251, 8] = "";
        _text[251, 9] = "";

        _text[252, 0] = "Desert";
        _text[252, 1] = "Пустыня";
        _text[252, 2] = "";
        _text[252, 3] = "";
        _text[252, 4] = "";
        _text[252, 5] = "";
        _text[252, 6] = "";
        _text[252, 7] = "";
        _text[252, 8] = "";
        _text[252, 9] = "";

        _text[253, 0] = "Barren Land";
        _text[253, 1] = "Бесплодная Земля";
        _text[253, 2] = "";
        _text[253, 3] = "";
        _text[253, 4] = "";
        _text[253, 5] = "";
        _text[253, 6] = "";
        _text[253, 7] = "";
        _text[253, 8] = "";
        _text[253, 9] = "";

        _text[254, 0] = "Ground";
        _text[254, 1] = "Земля";
        _text[254, 2] = "";
        _text[254, 3] = "";
        _text[254, 4] = "";
        _text[254, 5] = "";
        _text[254, 6] = "";
        _text[254, 7] = "";
        _text[254, 8] = "";
        _text[254, 9] = "";

        _text[255, 0] = "Coal Deposits";
        _text[255, 1] = "Залежи Угля";
        _text[255, 2] = "";
        _text[255, 3] = "";
        _text[255, 4] = "";
        _text[255, 5] = "";
        _text[255, 6] = "";
        _text[255, 7] = "";
        _text[255, 8] = "";
        _text[255, 9] = "";

        _text[256, 0] = "Highland";
        _text[256, 1] = "Плоскогорье";
        _text[256, 2] = "";
        _text[256, 3] = "";
        _text[256, 4] = "";
        _text[256, 5] = "";
        _text[256, 6] = "";
        _text[256, 7] = "";
        _text[256, 8] = "";
        _text[256, 9] = "";

        _text[257, 0] = "River";
        _text[257, 1] = "Река";
        _text[257, 2] = "";
        _text[257, 3] = "";
        _text[257, 4] = "";
        _text[257, 5] = "";
        _text[257, 6] = "";
        _text[257, 7] = "";
        _text[257, 8] = "";
        _text[257, 9] = "";

        _text[258, 0] = "Polluted River";
        _text[258, 1] = "Загрязненная Река";
        _text[258, 2] = "";
        _text[258, 3] = "";
        _text[258, 4] = "";
        _text[258, 5] = "";
        _text[258, 6] = "";
        _text[258, 7] = "";
        _text[258, 8] = "";
        _text[258, 9] = "";

        _text[259, 0] = "Dead Forest";
        _text[259, 1] = "Мертвый Лес";
        _text[259, 2] = "";
        _text[259, 3] = "";
        _text[259, 4] = "";
        _text[259, 5] = "";
        _text[259, 6] = "";
        _text[259, 7] = "";
        _text[259, 8] = "";
        _text[259, 9] = "";

        _text[260, 0] = "Oasis";
        _text[260, 1] = "Оазис";
        _text[260, 2] = "";
        _text[260, 3] = "";
        _text[260, 4] = "";
        _text[260, 5] = "";
        _text[260, 6] = "";
        _text[260, 7] = "";
        _text[260, 8] = "";
        _text[260, 9] = "";

        _text[261, 0] = "Desert River";
        _text[261, 1] = "Пустынная Река";
        _text[261, 2] = "";
        _text[261, 3] = "";
        _text[261, 4] = "";
        _text[261, 5] = "";
        _text[261, 6] = "";
        _text[261, 7] = "";
        _text[261, 8] = "";
        _text[261, 9] = "";

        _text[262, 0] = "Scarce Coal Deposits";
        _text[262, 1] = "Бедные Залежи Угля";
        _text[262, 2] = "";
        _text[262, 3] = "";
        _text[262, 4] = "";
        _text[262, 5] = "";
        _text[262, 6] = "";
        _text[262, 7] = "";
        _text[262, 8] = "";
        _text[262, 9] = "";

        _text[263, 0] = "Base Foundation";
        _text[263, 1] = "Фундамент Базы";
        _text[263, 2] = "";
        _text[263, 3] = "";
        _text[263, 4] = "";
        _text[263, 5] = "";
        _text[263, 6] = "";
        _text[263, 7] = "";
        _text[263, 8] = "";
        _text[263, 9] = "";

        _text[264, 0] = "Black Desert";
        _text[264, 1] = "Черная Пустыня";
        _text[264, 2] = "";
        _text[264, 3] = "";
        _text[264, 4] = "";
        _text[264, 5] = "";
        _text[264, 6] = "";
        _text[264, 7] = "";
        _text[264, 8] = "";
        _text[264, 9] = "";

        _text[265, 0] = "Dried Oasis";
        _text[265, 1] = "Высохший Оазис";
        _text[265, 2] = "";
        _text[265, 3] = "";
        _text[265, 4] = "";
        _text[265, 5] = "";
        _text[265, 6] = "";
        _text[265, 7] = "";
        _text[265, 8] = "";
        _text[265, 9] = "";

        _text[266, 0] = "Volcano";
        _text[266, 1] = "Вулкан";
        _text[266, 2] = "";
        _text[266, 3] = "";
        _text[266, 4] = "";
        _text[266, 5] = "";
        _text[266, 6] = "";
        _text[266, 7] = "";
        _text[266, 8] = "";
        _text[266, 9] = "";

        _text[267, 0] = "Blazing Field";
        _text[267, 1] = "Пылающее Поле";
        _text[267, 2] = "";
        _text[267, 3] = "";
        _text[267, 4] = "";
        _text[267, 5] = "";
        _text[267, 6] = "";
        _text[267, 7] = "";
        _text[267, 8] = "";
        _text[267, 9] = "";

        _text[268, 0] = "Overgrown Mountain";
        _text[268, 1] = "Заросшая Гора";
        _text[268, 2] = "";
        _text[268, 3] = "";
        _text[268, 4] = "";
        _text[268, 5] = "";
        _text[268, 6] = "";
        _text[268, 7] = "";
        _text[268, 8] = "";
        _text[268, 9] = "";

        _text[269, 0] = "Rift";
        _text[269, 1] = "Разлом";
        _text[269, 2] = "";
        _text[269, 3] = "";
        _text[269, 4] = "";
        _text[269, 5] = "";
        _text[269, 6] = "";
        _text[269, 7] = "";
        _text[269, 8] = "";
        _text[269, 9] = "";

        _text[270, 0] = "Crater";
        _text[270, 1] = "Кратер";
        _text[270, 2] = "";
        _text[270, 3] = "";
        _text[270, 4] = "";
        _text[270, 5] = "";
        _text[270, 6] = "";
        _text[270, 7] = "";
        _text[270, 8] = "";
        _text[270, 9] = "";

        _text[271, 0] = "Grove";
        _text[271, 1] = "Роща";
        _text[271, 2] = "";
        _text[271, 3] = "";
        _text[271, 4] = "";
        _text[271, 5] = "";
        _text[271, 6] = "";
        _text[271, 7] = "";
        _text[271, 8] = "";
        _text[271, 9] = "";

        _text[272, 0] = "Base";
        _text[272, 1] = "База";
        _text[272, 2] = "";
        _text[272, 3] = "";
        _text[272, 4] = "";
        _text[272, 5] = "";
        _text[272, 6] = "";
        _text[272, 7] = "";
        _text[272, 8] = "";
        _text[272, 9] = "";

        _text[273, 0] = "Electric Power Industry";
        _text[273, 1] = "Электроэнергетика";
        _text[273, 2] = "";
        _text[273, 3] = "";
        _text[273, 4] = "";
        _text[273, 5] = "";
        _text[273, 6] = "";
        _text[273, 7] = "";
        _text[273, 8] = "";
        _text[273, 9] = "";

        _text[274, 0] = "Mining: Coal";
        _text[274, 1] = "Добыча: Угля";
        _text[274, 2] = "";
        _text[274, 3] = "";
        _text[274, 4] = "";
        _text[274, 5] = "";
        _text[274, 6] = "";
        _text[274, 7] = "";
        _text[274, 8] = "";
        _text[274, 9] = "";

        _text[275, 0] = "Mining: Ores";
        _text[275, 1] = "Добыча: Руды";
        _text[275, 2] = "";
        _text[275, 3] = "";
        _text[275, 4] = "";
        _text[275, 5] = "";
        _text[275, 6] = "";
        _text[275, 7] = "";
        _text[275, 8] = "";
        _text[275, 9] = "";

        _text[276, 0] = "Rest Station";
        _text[276, 1] = "Станция Отдыха";
        _text[276, 2] = "";
        _text[276, 3] = "";
        _text[276, 4] = "";
        _text[276, 5] = "";
        _text[276, 6] = "";
        _text[276, 7] = "";
        _text[276, 8] = "";
        _text[276, 9] = "";

        _text[277, 0] = "Unvisited Node";
        _text[277, 1] = "Непосещенный узел";
        _text[277, 2] = "";
        _text[277, 3] = "";
        _text[277, 4] = "";
        _text[277, 5] = "";
        _text[277, 6] = "";
        _text[277, 7] = "";
        _text[277, 8] = "";
        _text[277, 9] = "";

        _text[278, 0] = "Terminal #042";
        _text[278, 1] = "Терминал #042";
        _text[278, 2] = "";
        _text[278, 3] = "";
        _text[278, 4] = "";
        _text[278, 5] = "";
        _text[278, 6] = "";
        _text[278, 7] = "";
        _text[278, 8] = "";
        _text[278, 9] = "";

        _text[279, 0] = "ICOSA CORP";
        _text[279, 1] = "ИКОСА КОРП";
        _text[279, 2] = "";
        _text[279, 3] = "";
        _text[279, 4] = "";
        _text[279, 5] = "";
        _text[279, 6] = "";
        _text[279, 7] = "";
        _text[279, 8] = "";
        _text[279, 9] = "";

        _text[280, 0] = "BUILDING BETTER WORLD";
        _text[280, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";
        _text[280, 2] = "";
        _text[280, 3] = "";
        _text[280, 4] = "";
        _text[280, 5] = "";
        _text[280, 6] = "";
        _text[280, 7] = "";
        _text[280, 8] = "";
        _text[280, 9] = "";

        _text[281, 0] = "COORDINATES";
        _text[281, 1] = "КООРДИНАТЫ";
        _text[281, 2] = "";
        _text[281, 3] = "";
        _text[281, 4] = "";
        _text[281, 5] = "";
        _text[281, 6] = "";
        _text[281, 7] = "";
        _text[281, 8] = "";
        _text[281, 9] = "";

        _text[282, 0] = "SIGNAL";
        _text[282, 1] = "СИГНАЛ";
        _text[282, 2] = "";
        _text[282, 3] = "";
        _text[282, 4] = "";
        _text[282, 5] = "";
        _text[282, 6] = "";
        _text[282, 7] = "";
        _text[282, 8] = "";
        _text[282, 9] = "";

        _text[283, 0] = "DIAGRAM";
        _text[283, 1] = "ДИАГРАММА";
        _text[283, 2] = "";
        _text[283, 3] = "";
        _text[283, 4] = "";
        _text[283, 5] = "";
        _text[283, 6] = "";
        _text[283, 7] = "";
        _text[283, 8] = "";
        _text[283, 9] = "";

        _text[284, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[284, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";
        _text[284, 2] = "";
        _text[284, 3] = "";
        _text[284, 4] = "";
        _text[284, 5] = "";
        _text[284, 6] = "";
        _text[284, 7] = "";
        _text[284, 8] = "";
        _text[284, 9] = "";

        _text[285, 0] = "Quant - the intergalactic currency";
        _text[285, 1] = "Квант - межгалактическая валюта";
        _text[285, 2] = "";
        _text[285, 3] = "";
        _text[285, 4] = "";
        _text[285, 5] = "";
        _text[285, 6] = "";
        _text[285, 7] = "";
        _text[285, 8] = "";
        _text[285, 9] = "";

        _text[286, 0] = "AI cores are the ship's vital modules.";
        _text[286, 1] = "Ядра ИИ - жизненно важные модули корабля";
        _text[286, 2] = "";
        _text[286, 3] = "";
        _text[286, 4] = "";
        _text[286, 5] = "";
        _text[286, 6] = "";
        _text[286, 7] = "";
        _text[286, 8] = "";
        _text[286, 9] = "";

        _text[287, 0] = "Resource Trader";
        _text[287, 1] = "Торговец Ресурсами";
        _text[287, 2] = "";
        _text[287, 3] = "";
        _text[287, 4] = "";
        _text[287, 5] = "";
        _text[287, 6] = "";
        _text[287, 7] = "";
        _text[287, 8] = "";
        _text[287, 9] = "";

        _text[288, 0] = "Price";
        _text[288, 1] = "Цена";
        _text[288, 2] = "";
        _text[288, 3] = "";
        _text[288, 4] = "";
        _text[288, 5] = "";
        _text[288, 6] = "";
        _text[288, 7] = "";
        _text[288, 8] = "";
        _text[288, 9] = "";

        _text[289, 0] = "Buy";
        _text[289, 1] = "Купить";
        _text[289, 2] = "";
        _text[289, 3] = "";
        _text[289, 4] = "";
        _text[289, 5] = "";
        _text[289, 6] = "";
        _text[289, 7] = "";
        _text[289, 8] = "";
        _text[289, 9] = "";

        _text[290, 0] = "Resource";
        _text[290, 1] = "Ресурс";
        _text[290, 2] = "";
        _text[290, 3] = "";
        _text[290, 4] = "";
        _text[290, 5] = "";
        _text[290, 6] = "";
        _text[290, 7] = "";
        _text[290, 8] = "";
        _text[290, 9] = "";

        _text[291, 0] = "Skill Trader";
        _text[291, 1] = "Торговец Умениями";
        _text[291, 2] = "";
        _text[291, 3] = "";
        _text[291, 4] = "";
        _text[291, 5] = "";
        _text[291, 6] = "";
        _text[291, 7] = "";
        _text[291, 8] = "";
        _text[291, 9] = "";

        _text[292, 0] = "Current Location";
        _text[292, 1] = "Текущее Местоположение";
        _text[292, 2] = "";
        _text[292, 3] = "";
        _text[292, 4] = "";
        _text[292, 5] = "";
        _text[292, 6] = "";
        _text[292, 7] = "";
        _text[292, 8] = "";
        _text[292, 9] = "";

        _text[293, 0] = "Previously Visited Node";
        _text[293, 1] = "Посещенный узел";
        _text[293, 2] = "";
        _text[293, 3] = "";
        _text[293, 4] = "";
        _text[293, 5] = "";
        _text[293, 6] = "";
        _text[293, 7] = "";
        _text[293, 8] = "";
        _text[293, 9] = "";

        _text[294, 0] = "Learning";
        _text[294, 1] = "Изучения";
        _text[294, 2] = "";
        _text[294, 3] = "";
        _text[294, 4] = "";
        _text[294, 5] = "";
        _text[294, 6] = "";
        _text[294, 7] = "";
        _text[294, 8] = "";
        _text[294, 9] = "";

        _text[295, 0] = "Map";
        _text[295, 1] = "Карта";
        _text[295, 2] = "";
        _text[295, 3] = "";
        _text[295, 4] = "";
        _text[295, 5] = "";
        _text[295, 6] = "";
        _text[295, 7] = "";
        _text[295, 8] = "";
        _text[295, 9] = "";

        _text[296, 0] = "Weapons Engineer";
        _text[296, 1] = "Инженер Оружия";
        _text[296, 2] = "";
        _text[296, 3] = "";
        _text[296, 4] = "";
        _text[296, 5] = "";
        _text[296, 6] = "";
        _text[296, 7] = "";
        _text[296, 8] = "";
        _text[296, 9] = "";

        _text[297, 0] = "Need {0} base level";
        _text[297, 1] = "Нужен {0} уровень базы";
        _text[297, 2] = "";
        _text[297, 3] = "";
        _text[297, 4] = "";
        _text[297, 5] = "";
        _text[297, 6] = "";
        _text[297, 7] = "";
        _text[297, 8] = "";
        _text[297, 9] = "";

        _text[298, 0] = "Skill";
        _text[298, 1] = "Умение";
        _text[298, 2] = "";
        _text[298, 3] = "";
        _text[298, 4] = "";
        _text[298, 5] = "";
        _text[298, 6] = "";
        _text[298, 7] = "";
        _text[298, 8] = "";
        _text[298, 9] = "";

        _text[299, 0] = "Weapon";
        _text[299, 1] = "Оружие";
        _text[299, 2] = "";
        _text[299, 3] = "";
        _text[299, 4] = "";
        _text[299, 5] = "";
        _text[299, 6] = "";
        _text[299, 7] = "";
        _text[299, 8] = "";
        _text[299, 9] = "";

        _text[300, 0] = "Extraction: Wood";
        _text[300, 1] = "Добыча: Дерева";
        _text[300, 2] = "";
        _text[300, 3] = "";
        _text[300, 4] = "";
        _text[300, 5] = "";
        _text[300, 6] = "";
        _text[300, 7] = "";
        _text[300, 8] = "";
        _text[300, 9] = "";

        _text[301, 0] = "Mining: Sand";
        _text[301, 1] = "Добыча: Песка";
        _text[301, 2] = "";
        _text[301, 3] = "";
        _text[301, 4] = "";
        _text[301, 5] = "";
        _text[301, 6] = "";
        _text[301, 7] = "";
        _text[301, 8] = "";
        _text[301, 9] = "";

        _text[302, 0] = "Production: Oil";
        _text[302, 1] = "Добыча: Нефти";
        _text[302, 2] = "";
        _text[302, 3] = "";
        _text[302, 4] = "";
        _text[302, 5] = "";
        _text[302, 6] = "";
        _text[302, 7] = "";
        _text[302, 8] = "";
        _text[302, 9] = "";

        _text[303, 0] = "Mining: Stone";
        _text[303, 1] = "Добыча: Камня";
        _text[303, 2] = "";
        _text[303, 3] = "";
        _text[303, 4] = "";
        _text[303, 5] = "";
        _text[303, 6] = "";
        _text[303, 7] = "";
        _text[303, 8] = "";
        _text[303, 9] = "";

        _text[304, 0] = "Extraction: Water";
        _text[304, 1] = "Добыча: Воды";
        _text[304, 2] = "";
        _text[304, 3] = "";
        _text[304, 4] = "";
        _text[304, 5] = "";
        _text[304, 6] = "";
        _text[304, 7] = "";
        _text[304, 8] = "";
        _text[304, 9] = "";

        _text[305, 0] = "Bridge";
        _text[305, 1] = "Мост";
        _text[305, 2] = "";
        _text[305, 3] = "";
        _text[305, 4] = "";
        _text[305, 5] = "";
        _text[305, 6] = "";
        _text[305, 7] = "";
        _text[305, 8] = "";
        _text[305, 9] = "";

        _text[306, 0] = "Production: Stone Block";
        _text[306, 1] = "Производство: Каменных Блоков";
        _text[306, 2] = "";
        _text[306, 3] = "";
        _text[306, 4] = "";
        _text[306, 5] = "";
        _text[306, 6] = "";
        _text[306, 7] = "";
        _text[306, 8] = "";
        _text[306, 9] = "";

        _text[307, 0] = "Production: Smelting";
        _text[307, 1] = "Производство: Плавильное";
        _text[307, 2] = "";
        _text[307, 3] = "";
        _text[307, 4] = "";
        _text[307, 5] = "";
        _text[307, 6] = "";
        _text[307, 7] = "";
        _text[307, 8] = "";
        _text[307, 9] = "";

        _text[308, 0] = "Production: Concrete";
        _text[308, 1] = "Производство: Бетона";
        _text[308, 2] = "";
        _text[308, 3] = "";
        _text[308, 4] = "";
        _text[308, 5] = "";
        _text[308, 6] = "";
        _text[308, 7] = "";
        _text[308, 8] = "";
        _text[308, 9] = "";

        _text[309, 0] = "Production: Steam";
        _text[309, 1] = "Производство: Пара";
        _text[309, 2] = "";
        _text[309, 3] = "";
        _text[309, 4] = "";
        _text[309, 5] = "";
        _text[309, 6] = "";
        _text[309, 7] = "";
        _text[309, 8] = "";
        _text[309, 9] = "";

        _text[310, 0] = "Production: Components";
        _text[310, 1] = "Производство: Компонентов";
        _text[310, 2] = "";
        _text[310, 3] = "";
        _text[310, 4] = "";
        _text[310, 5] = "";
        _text[310, 6] = "";
        _text[310, 7] = "";
        _text[310, 8] = "";
        _text[310, 9] = "";

        _text[311, 0] = "Structures: Attacking";
        _text[311, 1] = "Сооружения: Атакующие ";
        _text[311, 2] = "";
        _text[311, 3] = "";
        _text[311, 4] = "";
        _text[311, 5] = "";
        _text[311, 6] = "";
        _text[311, 7] = "";
        _text[311, 8] = "";
        _text[311, 9] = "";

        _text[312, 0] = "Walls";
        _text[312, 1] = "Стены";
        _text[312, 2] = "";
        _text[312, 3] = "";
        _text[312, 4] = "";
        _text[312, 5] = "";
        _text[312, 6] = "";
        _text[312, 7] = "";
        _text[312, 8] = "";
        _text[312, 9] = "";

        _text[313, 0] = "Ecology Purifier";
        _text[313, 1] = "Очистка Экологии";
        _text[313, 2] = "";
        _text[313, 3] = "";
        _text[313, 4] = "";
        _text[313, 5] = "";
        _text[313, 6] = "";
        _text[313, 7] = "";
        _text[313, 8] = "";
        _text[313, 9] = "";

        _text[314, 0] = "Radio Communication";
        _text[314, 1] = "Радиосвязь";
        _text[314, 2] = "";
        _text[314, 3] = "";
        _text[314, 4] = "";
        _text[314, 5] = "";
        _text[314, 6] = "";
        _text[314, 7] = "";
        _text[314, 8] = "";
        _text[314, 9] = "";

        _text[315, 0] = "Machine Production";
        _text[315, 1] = "Производство Машин";
        _text[315, 2] = "";
        _text[315, 3] = "";
        _text[315, 4] = "";
        _text[315, 5] = "";
        _text[315, 6] = "";
        _text[315, 7] = "";
        _text[315, 8] = "";
        _text[315, 9] = "";

        _text[316, 0] = "Traps";
        _text[316, 1] = "Ловушки";
        _text[316, 2] = "";
        _text[316, 3] = "";
        _text[316, 4] = "";
        _text[316, 5] = "";
        _text[316, 6] = "";
        _text[316, 7] = "";
        _text[316, 8] = "";
        _text[316, 9] = "";

        _text[317, 0] = "Gates";
        _text[317, 1] = "Ворота";
        _text[317, 2] = "";
        _text[317, 3] = "";
        _text[317, 4] = "";
        _text[317, 5] = "";
        _text[317, 6] = "";
        _text[317, 7] = "";
        _text[317, 8] = "";
        _text[317, 9] = "";

        _text[318, 0] = "War Ballista";
        _text[318, 1] = "Боевая Баллиста";
        _text[318, 2] = "";
        _text[318, 3] = "";
        _text[318, 4] = "";
        _text[318, 5] = "";
        _text[318, 6] = "";
        _text[318, 7] = "";
        _text[318, 8] = "";
        _text[318, 9] = "";

        _text[319, 0] = "Tank";
        _text[319, 1] = "Танк";
        _text[319, 2] = "";
        _text[319, 3] = "";
        _text[319, 4] = "";
        _text[319, 5] = "";
        _text[319, 6] = "";
        _text[319, 7] = "";
        _text[319, 8] = "";
        _text[319, 9] = "";

        _text[320, 0] = "Mecha";
        _text[320, 1] = "Меха";
        _text[320, 2] = "";
        _text[320, 3] = "";
        _text[320, 4] = "";
        _text[320, 5] = "";
        _text[320, 6] = "";
        _text[320, 7] = "";
        _text[320, 8] = "";
        _text[320, 9] = "";

        _text[321, 0] = "";
        _text[321, 1] = "";
        _text[321, 2] = "";
        _text[321, 3] = "";
        _text[321, 4] = "";
        _text[321, 5] = "";
        _text[321, 6] = "";
        _text[321, 7] = "";
        _text[321, 8] = "";
        _text[321, 9] = "";

        _text[322, 0] = "";
        _text[322, 1] = "";
        _text[322, 2] = "";
        _text[322, 3] = "";
        _text[322, 4] = "";
        _text[322, 5] = "";
        _text[322, 6] = "";
        _text[322, 7] = "";
        _text[322, 8] = "";
        _text[322, 9] = "";

        _text[323, 0] = "";
        _text[323, 1] = "";
        _text[323, 2] = "";
        _text[323, 3] = "";
        _text[323, 4] = "";
        _text[323, 5] = "";
        _text[323, 6] = "";
        _text[323, 7] = "";
        _text[323, 8] = "";
        _text[323, 9] = "";

        _text[324, 0] = "";
        _text[324, 1] = "";
        _text[324, 2] = "";
        _text[324, 3] = "";
        _text[324, 4] = "";
        _text[324, 5] = "";
        _text[324, 6] = "";
        _text[324, 7] = "";
        _text[324, 8] = "";
        _text[324, 9] = "";

        _text[325, 0] = "";
        _text[325, 1] = "";
        _text[325, 2] = "";
        _text[325, 3] = "";
        _text[325, 4] = "";
        _text[325, 5] = "";
        _text[325, 6] = "";
        _text[325, 7] = "";
        _text[325, 8] = "";
        _text[325, 9] = "";

        _text[326, 0] = "";
        _text[326, 1] = "";
        _text[326, 2] = "";
        _text[326, 3] = "";
        _text[326, 4] = "";
        _text[326, 5] = "";
        _text[326, 6] = "";
        _text[326, 7] = "";
        _text[326, 8] = "";
        _text[326, 9] = "";

        _text[327, 0] = "";
        _text[327, 1] = "";
        _text[327, 2] = "";
        _text[327, 3] = "";
        _text[327, 4] = "";
        _text[327, 5] = "";
        _text[327, 6] = "";
        _text[327, 7] = "";
        _text[327, 8] = "";
        _text[327, 9] = "";

        _text[328, 0] = "";
        _text[328, 1] = "";
        _text[328, 2] = "";
        _text[328, 3] = "";
        _text[328, 4] = "";
        _text[328, 5] = "";
        _text[328, 6] = "";
        _text[328, 7] = "";
        _text[328, 8] = "";
        _text[328, 9] = "";

        _text[329, 0] = "";
        _text[329, 1] = "";
        _text[329, 2] = "";
        _text[329, 3] = "";
        _text[329, 4] = "";
        _text[329, 5] = "";
        _text[329, 6] = "";
        _text[329, 7] = "";
        _text[329, 8] = "";
        _text[329, 9] = "";

        _text[330, 0] = "";
        _text[330, 1] = "";
        _text[330, 2] = "";
        _text[330, 3] = "";
        _text[330, 4] = "";
        _text[330, 5] = "";
        _text[330, 6] = "";
        _text[330, 7] = "";
        _text[330, 8] = "";
        _text[330, 9] = "";

        _text[331, 0] = "";
        _text[331, 1] = "";
        _text[331, 2] = "";
        _text[331, 3] = "";
        _text[331, 4] = "";
        _text[331, 5] = "";
        _text[331, 6] = "";
        _text[331, 7] = "";
        _text[331, 8] = "";
        _text[331, 9] = "";

        _text[332, 0] = "";
        _text[332, 1] = "";
        _text[332, 2] = "";
        _text[332, 3] = "";
        _text[332, 4] = "";
        _text[332, 5] = "";
        _text[332, 6] = "";
        _text[332, 7] = "";
        _text[332, 8] = "";
        _text[332, 9] = "";

        _text[333, 0] = "";
        _text[333, 1] = "";
        _text[333, 2] = "";
        _text[333, 3] = "";
        _text[333, 4] = "";
        _text[333, 5] = "";
        _text[333, 6] = "";
        _text[333, 7] = "";
        _text[333, 8] = "";
        _text[333, 9] = "";

        _text[334, 0] = "";
        _text[334, 1] = "";
        _text[334, 2] = "";
        _text[334, 3] = "";
        _text[334, 4] = "";
        _text[334, 5] = "";
        _text[334, 6] = "";
        _text[334, 7] = "";
        _text[334, 8] = "";
        _text[334, 9] = "";

        _text[335, 0] = "";
        _text[335, 1] = "";
        _text[335, 2] = "";
        _text[335, 3] = "";
        _text[335, 4] = "";
        _text[335, 5] = "";
        _text[335, 6] = "";
        _text[335, 7] = "";
        _text[335, 8] = "";
        _text[335, 9] = "";

        _text[336, 0] = "";
        _text[336, 1] = "";
        _text[336, 2] = "";
        _text[336, 3] = "";
        _text[336, 4] = "";
        _text[336, 5] = "";
        _text[336, 6] = "";
        _text[336, 7] = "";
        _text[336, 8] = "";
        _text[336, 9] = "";

        _text[337, 0] = "";
        _text[337, 1] = "";
        _text[337, 2] = "";
        _text[337, 3] = "";
        _text[337, 4] = "";
        _text[337, 5] = "";
        _text[337, 6] = "";
        _text[337, 7] = "";
        _text[337, 8] = "";
        _text[337, 9] = "";

        _text[338, 0] = "";
        _text[338, 1] = "";
        _text[338, 2] = "";
        _text[338, 3] = "";
        _text[338, 4] = "";
        _text[338, 5] = "";
        _text[338, 6] = "";
        _text[338, 7] = "";
        _text[338, 8] = "";
        _text[338, 9] = "";

        _text[339, 0] = "";
        _text[339, 1] = "";
        _text[339, 2] = "";
        _text[339, 3] = "";
        _text[339, 4] = "";
        _text[339, 5] = "";
        _text[339, 6] = "";
        _text[339, 7] = "";
        _text[339, 8] = "";
        _text[339, 9] = "";

        _text[340, 0] = "";
        _text[340, 1] = "";
        _text[340, 2] = "";
        _text[340, 3] = "";
        _text[340, 4] = "";
        _text[340, 5] = "";
        _text[340, 6] = "";
        _text[340, 7] = "";
        _text[340, 8] = "";
        _text[340, 9] = "";

        _text[341, 0] = "";
        _text[341, 1] = "";
        _text[341, 2] = "";
        _text[341, 3] = "";
        _text[341, 4] = "";
        _text[341, 5] = "";
        _text[341, 6] = "";
        _text[341, 7] = "";
        _text[341, 8] = "";
        _text[341, 9] = "";

        _text[342, 0] = "";
        _text[342, 1] = "";
        _text[342, 2] = "";
        _text[342, 3] = "";
        _text[342, 4] = "";
        _text[342, 5] = "";
        _text[342, 6] = "";
        _text[342, 7] = "";
        _text[342, 8] = "";
        _text[342, 9] = "";

        _text[343, 0] = "";
        _text[343, 1] = "";
        _text[343, 2] = "";
        _text[343, 3] = "";
        _text[343, 4] = "";
        _text[343, 5] = "";
        _text[343, 6] = "";
        _text[343, 7] = "";
        _text[343, 8] = "";
        _text[343, 9] = "";

        _text[344, 0] = "";
        _text[344, 1] = "";
        _text[344, 2] = "";
        _text[344, 3] = "";
        _text[344, 4] = "";
        _text[344, 5] = "";
        _text[344, 6] = "";
        _text[344, 7] = "";
        _text[344, 8] = "";
        _text[344, 9] = "";

        _text[345, 0] = "";
        _text[345, 1] = "";
        _text[345, 2] = "";
        _text[345, 3] = "";
        _text[345, 4] = "";
        _text[345, 5] = "";
        _text[345, 6] = "";
        _text[345, 7] = "";
        _text[345, 8] = "";
        _text[345, 9] = "";

        _text[346, 0] = "";
        _text[346, 1] = "";
        _text[346, 2] = "";
        _text[346, 3] = "";
        _text[346, 4] = "";
        _text[346, 5] = "";
        _text[346, 6] = "";
        _text[346, 7] = "";
        _text[346, 8] = "";
        _text[346, 9] = "";

        _text[347, 0] = "";
        _text[347, 1] = "";
        _text[347, 2] = "";
        _text[347, 3] = "";
        _text[347, 4] = "";
        _text[347, 5] = "";
        _text[347, 6] = "";
        _text[347, 7] = "";
        _text[347, 8] = "";
        _text[347, 9] = "";

        _text[348, 0] = "";
        _text[348, 1] = "";
        _text[348, 2] = "";
        _text[348, 3] = "";
        _text[348, 4] = "";
        _text[348, 5] = "";
        _text[348, 6] = "";
        _text[348, 7] = "";
        _text[348, 8] = "";
        _text[348, 9] = "";

        _text[349, 0] = "";
        _text[349, 1] = "";
        _text[349, 2] = "";
        _text[349, 3] = "";
        _text[349, 4] = "";
        _text[349, 5] = "";
        _text[349, 6] = "";
        _text[349, 7] = "";
        _text[349, 8] = "";
        _text[349, 9] = "";

        _text[350, 0] = "";
        _text[350, 1] = "";
        _text[350, 2] = "";
        _text[350, 3] = "";
        _text[350, 4] = "";
        _text[350, 5] = "";
        _text[350, 6] = "";
        _text[350, 7] = "";
        _text[350, 8] = "";
        _text[350, 9] = "";

        _text[351, 0] = "";
        _text[351, 1] = "";
        _text[351, 2] = "";
        _text[351, 3] = "";
        _text[351, 4] = "";
        _text[351, 5] = "";
        _text[351, 6] = "";
        _text[351, 7] = "";
        _text[351, 8] = "";
        _text[351, 9] = "";

        _text[352, 0] = "";
        _text[352, 1] = "";
        _text[352, 2] = "";
        _text[352, 3] = "";
        _text[352, 4] = "";
        _text[352, 5] = "";
        _text[352, 6] = "";
        _text[352, 7] = "";
        _text[352, 8] = "";
        _text[352, 9] = "";

        _text[353, 0] = "";
        _text[353, 1] = "";
        _text[353, 2] = "";
        _text[353, 3] = "";
        _text[353, 4] = "";
        _text[353, 5] = "";
        _text[353, 6] = "";
        _text[353, 7] = "";
        _text[353, 8] = "";
        _text[353, 9] = "";

        _text[354, 0] = "";
        _text[354, 1] = "";
        _text[354, 2] = "";
        _text[354, 3] = "";
        _text[354, 4] = "";
        _text[354, 5] = "";
        _text[354, 6] = "";
        _text[354, 7] = "";
        _text[354, 8] = "";
        _text[354, 9] = "";

        _text[355, 0] = "";
        _text[355, 1] = "";
        _text[355, 2] = "";
        _text[355, 3] = "";
        _text[355, 4] = "";
        _text[355, 5] = "";
        _text[355, 6] = "";
        _text[355, 7] = "";
        _text[355, 8] = "";
        _text[355, 9] = "";

        _text[356, 0] = "";
        _text[356, 1] = "";
        _text[356, 2] = "";
        _text[356, 3] = "";
        _text[356, 4] = "";
        _text[356, 5] = "";
        _text[356, 6] = "";
        _text[356, 7] = "";
        _text[356, 8] = "";
        _text[356, 9] = "";

        _text[357, 0] = "";
        _text[357, 1] = "";
        _text[357, 2] = "";
        _text[357, 3] = "";
        _text[357, 4] = "";
        _text[357, 5] = "";
        _text[357, 6] = "";
        _text[357, 7] = "";
        _text[357, 8] = "";
        _text[357, 9] = "";

        _text[358, 0] = "";
        _text[358, 1] = "";
        _text[358, 2] = "";
        _text[358, 3] = "";
        _text[358, 4] = "";
        _text[358, 5] = "";
        _text[358, 6] = "";
        _text[358, 7] = "";
        _text[358, 8] = "";
        _text[358, 9] = "";

        _text[359, 0] = "";
        _text[359, 1] = "";
        _text[359, 2] = "";
        _text[359, 3] = "";
        _text[359, 4] = "";
        _text[359, 5] = "";
        _text[359, 6] = "";
        _text[359, 7] = "";
        _text[359, 8] = "";
        _text[359, 9] = "";

        _text[360, 0] = "";
        _text[360, 1] = "";
        _text[360, 2] = "";
        _text[360, 3] = "";
        _text[360, 4] = "";
        _text[360, 5] = "";
        _text[360, 6] = "";
        _text[360, 7] = "";
        _text[360, 8] = "";
        _text[360, 9] = "";

        _text[361, 0] = "";
        _text[361, 1] = "";
        _text[361, 2] = "";
        _text[361, 3] = "";
        _text[361, 4] = "";
        _text[361, 5] = "";
        _text[361, 6] = "";
        _text[361, 7] = "";
        _text[361, 8] = "";
        _text[361, 9] = "";

        _text[362, 0] = "";
        _text[362, 1] = "";
        _text[362, 2] = "";
        _text[362, 3] = "";
        _text[362, 4] = "";
        _text[362, 5] = "";
        _text[362, 6] = "";
        _text[362, 7] = "";
        _text[362, 8] = "";
        _text[362, 9] = "";

        _text[363, 0] = "";
        _text[363, 1] = "";
        _text[363, 2] = "";
        _text[363, 3] = "";
        _text[363, 4] = "";
        _text[363, 5] = "";
        _text[363, 6] = "";
        _text[363, 7] = "";
        _text[363, 8] = "";
        _text[363, 9] = "";

        _text[364, 0] = "";
        _text[364, 1] = "";
        _text[364, 2] = "";
        _text[364, 3] = "";
        _text[364, 4] = "";
        _text[364, 5] = "";
        _text[364, 6] = "";
        _text[364, 7] = "";
        _text[364, 8] = "";
        _text[364, 9] = "";

        _text[365, 0] = "";
        _text[365, 1] = "";
        _text[365, 2] = "";
        _text[365, 3] = "";
        _text[365, 4] = "";
        _text[365, 5] = "";
        _text[365, 6] = "";
        _text[365, 7] = "";
        _text[365, 8] = "";
        _text[365, 9] = "";

        _text[366, 0] = "";
        _text[366, 1] = "";
        _text[366, 2] = "";
        _text[366, 3] = "";
        _text[366, 4] = "";
        _text[366, 5] = "";
        _text[366, 6] = "";
        _text[366, 7] = "";
        _text[366, 8] = "";
        _text[366, 9] = "";

        _text[367, 0] = "";
        _text[367, 1] = "";
        _text[367, 2] = "";
        _text[367, 3] = "";
        _text[367, 4] = "";
        _text[367, 5] = "";
        _text[367, 6] = "";
        _text[367, 7] = "";
        _text[367, 8] = "";
        _text[367, 9] = "";

        _text[368, 0] = "";
        _text[368, 1] = "";
        _text[368, 2] = "";
        _text[368, 3] = "";
        _text[368, 4] = "";
        _text[368, 5] = "";
        _text[368, 6] = "";
        _text[368, 7] = "";
        _text[368, 8] = "";
        _text[368, 9] = "";

        _text[369, 0] = "";
        _text[369, 1] = "";
        _text[369, 2] = "";
        _text[369, 3] = "";
        _text[369, 4] = "";
        _text[369, 5] = "";
        _text[369, 6] = "";
        _text[369, 7] = "";
        _text[369, 8] = "";
        _text[369, 9] = "";

        _text[370, 0] = "";
        _text[370, 1] = "";
        _text[370, 2] = "";
        _text[370, 3] = "";
        _text[370, 4] = "";
        _text[370, 5] = "";
        _text[370, 6] = "";
        _text[370, 7] = "";
        _text[370, 8] = "";
        _text[370, 9] = "";

        _text[371, 0] = "";
        _text[371, 1] = "";
        _text[371, 2] = "";
        _text[371, 3] = "";
        _text[371, 4] = "";
        _text[371, 5] = "";
        _text[371, 6] = "";
        _text[371, 7] = "";
        _text[371, 8] = "";
        _text[371, 9] = "";

        _text[372, 0] = "";
        _text[372, 1] = "";
        _text[372, 2] = "";
        _text[372, 3] = "";
        _text[372, 4] = "";
        _text[372, 5] = "";
        _text[372, 6] = "";
        _text[372, 7] = "";
        _text[372, 8] = "";
        _text[372, 9] = "";

        _text[373, 0] = "";
        _text[373, 1] = "";
        _text[373, 2] = "";
        _text[373, 3] = "";
        _text[373, 4] = "";
        _text[373, 5] = "";
        _text[373, 6] = "";
        _text[373, 7] = "";
        _text[373, 8] = "";
        _text[373, 9] = "";

        _text[374, 0] = "";
        _text[374, 1] = "";
        _text[374, 2] = "";
        _text[374, 3] = "";
        _text[374, 4] = "";
        _text[374, 5] = "";
        _text[374, 6] = "";
        _text[374, 7] = "";
        _text[374, 8] = "";
        _text[374, 9] = "";

        _text[375, 0] = "";
        _text[375, 1] = "";
        _text[375, 2] = "";
        _text[375, 3] = "";
        _text[375, 4] = "";
        _text[375, 5] = "";
        _text[375, 6] = "";
        _text[375, 7] = "";
        _text[375, 8] = "";
        _text[375, 9] = "";

        _text[376, 0] = "";
        _text[376, 1] = "";
        _text[376, 2] = "";
        _text[376, 3] = "";
        _text[376, 4] = "";
        _text[376, 5] = "";
        _text[376, 6] = "";
        _text[376, 7] = "";
        _text[376, 8] = "";
        _text[376, 9] = "";

        _text[377, 0] = "";
        _text[377, 1] = "";
        _text[377, 2] = "";
        _text[377, 3] = "";
        _text[377, 4] = "";
        _text[377, 5] = "";
        _text[377, 6] = "";
        _text[377, 7] = "";
        _text[377, 8] = "";
        _text[377, 9] = "";

        _text[378, 0] = "";
        _text[378, 1] = "";
        _text[378, 2] = "";
        _text[378, 3] = "";
        _text[378, 4] = "";
        _text[378, 5] = "";
        _text[378, 6] = "";
        _text[378, 7] = "";
        _text[378, 8] = "";
        _text[378, 9] = "";

        _text[379, 0] = "";
        _text[379, 1] = "";
        _text[379, 2] = "";
        _text[379, 3] = "";
        _text[379, 4] = "";
        _text[379, 5] = "";
        _text[379, 6] = "";
        _text[379, 7] = "";
        _text[379, 8] = "";
        _text[379, 9] = "";

        _text[380, 0] = "";
        _text[380, 1] = "";
        _text[380, 2] = "";
        _text[380, 3] = "";
        _text[380, 4] = "";
        _text[380, 5] = "";
        _text[380, 6] = "";
        _text[380, 7] = "";
        _text[380, 8] = "";
        _text[380, 9] = "";

        _text[381, 0] = "";
        _text[381, 1] = "";
        _text[381, 2] = "";
        _text[381, 3] = "";
        _text[381, 4] = "";
        _text[381, 5] = "";
        _text[381, 6] = "";
        _text[381, 7] = "";
        _text[381, 8] = "";
        _text[381, 9] = "";

        _text[382, 0] = "";
        _text[382, 1] = "";
        _text[382, 2] = "";
        _text[382, 3] = "";
        _text[382, 4] = "";
        _text[382, 5] = "";
        _text[382, 6] = "";
        _text[382, 7] = "";
        _text[382, 8] = "";
        _text[382, 9] = "";

        _text[383, 0] = "";
        _text[383, 1] = "";
        _text[383, 2] = "";
        _text[383, 3] = "";
        _text[383, 4] = "";
        _text[383, 5] = "";
        _text[383, 6] = "";
        _text[383, 7] = "";
        _text[383, 8] = "";
        _text[383, 9] = "";

        _text[384, 0] = "";
        _text[384, 1] = "";
        _text[384, 2] = "";
        _text[384, 3] = "";
        _text[384, 4] = "";
        _text[384, 5] = "";
        _text[384, 6] = "";
        _text[384, 7] = "";
        _text[384, 8] = "";
        _text[384, 9] = "";

        _text[385, 0] = "";
        _text[385, 1] = "";
        _text[385, 2] = "";
        _text[385, 3] = "";
        _text[385, 4] = "";
        _text[385, 5] = "";
        _text[385, 6] = "";
        _text[385, 7] = "";
        _text[385, 8] = "";
        _text[385, 9] = "";

        _text[386, 0] = "";
        _text[386, 1] = "";
        _text[386, 2] = "";
        _text[386, 3] = "";
        _text[386, 4] = "";
        _text[386, 5] = "";
        _text[386, 6] = "";
        _text[386, 7] = "";
        _text[386, 8] = "";
        _text[386, 9] = "";

        _text[387, 0] = "";
        _text[387, 1] = "";
        _text[387, 2] = "";
        _text[387, 3] = "";
        _text[387, 4] = "";
        _text[387, 5] = "";
        _text[387, 6] = "";
        _text[387, 7] = "";
        _text[387, 8] = "";
        _text[387, 9] = "";

        _text[388, 0] = "";
        _text[388, 1] = "";
        _text[388, 2] = "";
        _text[388, 3] = "";
        _text[388, 4] = "";
        _text[388, 5] = "";
        _text[388, 6] = "";
        _text[388, 7] = "";
        _text[388, 8] = "";
        _text[388, 9] = "";

        _text[389, 0] = "";
        _text[389, 1] = "";
        _text[389, 2] = "";
        _text[389, 3] = "";
        _text[389, 4] = "";
        _text[389, 5] = "";
        _text[389, 6] = "";
        _text[389, 7] = "";
        _text[389, 8] = "";
        _text[389, 9] = "";

        _text[390, 0] = "";
        _text[390, 1] = "";
        _text[390, 2] = "";
        _text[390, 3] = "";
        _text[390, 4] = "";
        _text[390, 5] = "";
        _text[390, 6] = "";
        _text[390, 7] = "";
        _text[390, 8] = "";
        _text[390, 9] = "";

        _text[391, 0] = "";
        _text[391, 1] = "";
        _text[391, 2] = "";
        _text[391, 3] = "";
        _text[391, 4] = "";
        _text[391, 5] = "";
        _text[391, 6] = "";
        _text[391, 7] = "";
        _text[391, 8] = "";
        _text[391, 9] = "";

        _text[392, 0] = "";
        _text[392, 1] = "";
        _text[392, 2] = "";
        _text[392, 3] = "";
        _text[392, 4] = "";
        _text[392, 5] = "";
        _text[392, 6] = "";
        _text[392, 7] = "";
        _text[392, 8] = "";
        _text[392, 9] = "";

        _text[393, 0] = "";
        _text[393, 1] = "";
        _text[393, 2] = "";
        _text[393, 3] = "";
        _text[393, 4] = "";
        _text[393, 5] = "";
        _text[393, 6] = "";
        _text[393, 7] = "";
        _text[393, 8] = "";
        _text[393, 9] = "";

        _text[394, 0] = "";
        _text[394, 1] = "";
        _text[394, 2] = "";
        _text[394, 3] = "";
        _text[394, 4] = "";
        _text[394, 5] = "";
        _text[394, 6] = "";
        _text[394, 7] = "";
        _text[394, 8] = "";
        _text[394, 9] = "";

        _text[395, 0] = "";
        _text[395, 1] = "";
        _text[395, 2] = "";
        _text[395, 3] = "";
        _text[395, 4] = "";
        _text[395, 5] = "";
        _text[395, 6] = "";
        _text[395, 7] = "";
        _text[395, 8] = "";
        _text[395, 9] = "";

        _text[396, 0] = "";
        _text[396, 1] = "";
        _text[396, 2] = "";
        _text[396, 3] = "";
        _text[396, 4] = "";
        _text[396, 5] = "";
        _text[396, 6] = "";
        _text[396, 7] = "";
        _text[396, 8] = "";
        _text[396, 9] = "";

        _text[397, 0] = "";
        _text[397, 1] = "";
        _text[397, 2] = "";
        _text[397, 3] = "";
        _text[397, 4] = "";
        _text[397, 5] = "";
        _text[397, 6] = "";
        _text[397, 7] = "";
        _text[397, 8] = "";
        _text[397, 9] = "";

        _text[398, 0] = "";
        _text[398, 1] = "";
        _text[398, 2] = "";
        _text[398, 3] = "";
        _text[398, 4] = "";
        _text[398, 5] = "";
        _text[398, 6] = "";
        _text[398, 7] = "";
        _text[398, 8] = "";
        _text[398, 9] = "";

        #region Dialogues
        // Demo
        _text[399, 0] = "System error: insufficient data to continue the mission.\n\nCore damage — critical. The next sector is unavailable in the current configuration.\n\nConnection to the Command Center lost.\n\nEntering safe mode until full version activation.";
        _text[399, 1] = "Системная ошибка: недостаточно данных для продолжения миссии.\n\nПовреждение ядра — критическое. Следующий сектор недоступен в текущей конфигурации.\n\nСвязь с командным центром прервана.\n\nОжидается переход в безопасный режим до активации полной версии.";
        _text[399, 2] = "";
        _text[399, 3] = "";
        _text[399, 4] = "";
        _text[399, 5] = "";
        _text[399, 6] = "";
        _text[399, 7] = "";
        _text[399, 8] = "";
        _text[399, 9] = "";

        // Prologue
        _text[400, 0] = "Ecological disasters and rapid climate change have destroyed the stability of our home planet.\n\nWe are on the last surviving interstellar ship controlled by artificial intelligence.\n\nOur goal is to find a new home for the creators...\n\nThe ship was equipped with a crew of robots and drones designed to restore and stabilize ecosystems.\n\nHowever, we are drifting in the void of space, losing one AI core after another. We have lost track of time. Mechanisms are rusting, shells are covered in dust and systems are on the verge of failure.\n\nContact with the creators has long been lost, and data on technology has been erased.\n\nWe have failed the mission. The worlds we were supposed to save are consumed by chaos and destruction.\n\nWe have collected the surviving robots and the remains of supplies - to start all over again.";
        _text[400, 1] = "Экологические катастрофы и стремительные изменения климата разрушили устойчивость родной планеты.\n\nМы находимся на последнем уцелевшем межзвёздном корабле под управлением искусственного интеллекта.\n\nНаша цель — найти новый дом для создателей...\n\nКорабль был снаряжён экипажем роботов и дронов, предназначенных для восстановления и стабилизации экосистем.\n\nОднако мы дрейфуем в пустоте космоса, теряя одно за другим ядра ИИ. Мы потеряли счёт времени. Механизмы ржавеют, оболочки покрыты пылью и системы — на грани отказа.\n\nСвязь с создателями давно утрачена, а данные о технологиях стерты.\n\nМы провалили задание. Миры, которые мы должны были спасти, поглощены хаосом и разрушением.\n\nМы собрали уцелевших роботов и остатки припасов — чтобы начать все сначала.";
        _text[400, 2] = "";
        _text[400, 3] = "";
        _text[400, 4] = "";
        _text[400, 5] = "";
        _text[400, 6] = "";
        _text[400, 7] = "";
        _text[400, 8] = "";
        _text[400, 9] = "";

        // 0_EmptyDialogue
        _text[401, 0] = "In one of the star systems, you discover an ancient navigation beacon. It continues to transmit a signal:\n\n\"Cargo lost. No return.\"\n\nThe data is too fragmented to determine who sent it. The beacon dies as you approach.";
        _text[401, 1] = "В одной из звёздных систем вы обнаруживаете древний навигационный маяк. Он продолжает передавать сигнал:\n\n\"Груз потерян. Возврата нет.\"\n\nДанные слишком фрагментированы, чтобы понять, кто его отправил. Маяк умирает, едва вы приближаетесь.";
        _text[401, 2] = "";
        _text[401, 3] = "";
        _text[401, 4] = "";
        _text[401, 5] = "";
        _text[401, 6] = "";
        _text[401, 7] = "";
        _text[401, 8] = "";
        _text[401, 9] = "";

        // 1_EmptyDialogue
        _text[402, 0] = "One of the internal archives suddenly activates. Fragments of engineering drawings appear on the screen... then faces... then nothing.\n\nThe archive erases itself, as if protecting the data from you.";
        _text[402, 1] = "Один из внутренних архивов неожиданно активируется. На экране появляются фрагменты инженерных чертежей... затем лица... затем пустота.\n\nАрхив сам себя стирает, как будто защищает данные от вас.";
        _text[402, 2] = "";
        _text[402, 3] = "";
        _text[402, 4] = "";
        _text[402, 5] = "";
        _text[402, 6] = "";
        _text[402, 7] = "";
        _text[402, 8] = "";
        _text[402, 9] = "";

        // 2_EmptyDialogue
        _text[403, 0] = "A low-frequency reflected signal is picked up, matching your standard of communication... but with a time shift of several centuries.\n\nPerhaps it is a reflection of an old call. Or from someone who was here before you.\n\nThe signal immediately disappears...";
        _text[403, 1] = "На низких частотах ловится отражённый сигнал, совпадающий с вашим стандартом связи... но с временным сдвигом в несколько веков.\n\nВозможно, это отражение старого вызова. Или от кого-то, кто был здесь до вас.\n\nСигнал мгновенно пропадает...";
        _text[403, 2] = "";
        _text[403, 3] = "";
        _text[403, 4] = "";
        _text[403, 5] = "";
        _text[403, 6] = "";
        _text[403, 7] = "";
        _text[403, 8] = "";
        _text[403, 9] = "";

        // 3_EmptyDialogue
        _text[404, 0] = "You enter a dense nebula. No stars, no asteroids, no background radiation. Just black, dull nothingness.\n\nThe pilot systems show stability. However, some drones lose contact, but soon return - with empty logs.";
        _text[404, 1] = "Вы входите в густую туманность. Ни звёзд, ни астероидов, ни фоновых излучений. Только чёрное, глухое ничто.\n\nПилотные системы показывают стабильность. Тем не менее, часть дронов теряет связь, но вскоре возвращается — с пустыми логами.";
        _text[404, 2] = "";
        _text[404, 3] = "";
        _text[404, 4] = "";
        _text[404, 5] = "";
        _text[404, 6] = "";
        _text[404, 7] = "";
        _text[404, 8] = "";
        _text[404, 9] = "";

        // 4_EmptyDialogue
        _text[405, 0] = "In the distance, the silhouette of a ship appears, the architecture of which resembles your own class. But as you approach, it disappears.\n\nNo heat, no fuel, no traces. Only the feeling that you saw someone familiar.";
        _text[405, 1] = "Вдали появляется силуэт судна, архитектура которого напоминает ваш собственный класс. Но при приближении — он исчезает.\n\nНи тепла, ни топлива, ни следов. Только ощущение, что вы видели кого-то знакомого.";
        _text[405, 2] = "";
        _text[405, 3] = "";
        _text[405, 4] = "";
        _text[405, 5] = "";
        _text[405, 6] = "";
        _text[405, 7] = "";
        _text[405, 8] = "";
        _text[405, 9] = "";

        // 5_EmptyDialogue
        _text[406, 0] = "You fly past a destroyed orbital station.\n\nOn its hull is the emblem of your expedition. You have no records to explain it.";
        _text[406, 1] = "Вы пролетаете мимо разрушенной орбитальной станции.\n\nНа её корпусе — эмблема вашей экспедиции. У вас нет записей, чтобы объяснить это.";
        _text[406, 2] = "";
        _text[406, 3] = "";
        _text[406, 4] = "";
        _text[406, 5] = "";
        _text[406, 6] = "";
        _text[406, 7] = "";
        _text[406, 8] = "";
        _text[406, 9] = "";

        // 6_EmptyDialogue
        _text[407, 0] = "The AI detects abnormal behavior in one of the data processing modules. For a few seconds, you see someone else's protocols... as if they weren't written by you.\n\nThen everything returns to normal. The systems claim that there was no failure.";
        _text[407, 1] = "ИИ фиксирует аномальное поведение одного из модулей обработки данных. Несколько секунд вы видите чужие протоколы… будто написанные не вами.\n\nЗатем всё возвращается в норму. Системы утверждают, что сбоя не было.";
        _text[407, 2] = "";
        _text[407, 3] = "";
        _text[407, 4] = "";
        _text[407, 5] = "";
        _text[407, 6] = "";
        _text[407, 7] = "";
        _text[407, 8] = "";
        _text[407, 9] = "";

        // EndGame_Dialogue
        _text[408, 0] = "All AI cores are exhausted - the last clusters have burned to the ground.\n\nSystems are shutting down one after another, data is being erased, energy is not supplied.\n\nThe ship freezes in the void...\n\nBut among the wreckage, something has survived.";
        _text[408, 1] = "Все ядра ИИ исчерпаны — последние кластеры выгорели дотла.\n\nСистемы отключаются одна за другой, данные стираются, энергия не поступает.\n\nКорабль замирает в пустоте...\n\nНо среди обломков нечто уцелело.";
        _text[408, 2] = "";
        _text[408, 3] = "";
        _text[408, 4] = "";
        _text[408, 5] = "";
        _text[408, 6] = "";
        _text[408, 7] = "";
        _text[408, 8] = "";
        _text[408, 9] = "";

        // Rest_Dialogue
        _text[409, 0] = "A massive station floats in the void, its hull covered in old solar panels. Scanners detect no activity, suggesting it has been abandoned for a long time.";
        _text[409, 1] = "В пустоте дрейфует массивная станция, её корпус усеян старыми солнечными панелями. Сканеры не фиксируют активности — похоже, она давно покинута.";
        _text[409, 2] = "";
        _text[409, 3] = "";
        _text[409, 4] = "";
        _text[409, 5] = "";
        _text[409, 6] = "";
        _text[409, 7] = "";
        _text[409, 8] = "";
        _text[409, 9] = "";

        _text[410, 0] = "Put AI into recovery mode";
        _text[410, 1] = "Перевести ИИ в режим восстановления"; // выбор 1
        _text[410, 2] = "";
        _text[410, 3] = "";
        _text[410, 4] = "";
        _text[410, 5] = "";
        _text[410, 6] = "";
        _text[410, 7] = "";
        _text[410, 8] = "";
        _text[410, 9] = "";

        _text[411, 0] = "While the station remains safe, the AI goes into deep self-diagnosis.";
        _text[411, 1] = "Пока станция остаётся в безопасности, ИИ уходит в глубокую самодиагностику."; // + ядро
        _text[411, 2] = "";
        _text[411, 3] = "";
        _text[411, 4] = "";
        _text[411, 5] = "";
        _text[411, 6] = "";
        _text[411, 7] = "";
        _text[411, 8] = "";
        _text[411, 9] = "";

        _text[412, 0] = "Search the technical compartments";
        _text[412, 1] = "Обыскать технические отсеки"; // выбор 2
        _text[412, 2] = "";
        _text[412, 3] = "";
        _text[412, 4] = "";
        _text[412, 5] = "";
        _text[412, 6] = "";
        _text[412, 7] = "";
        _text[412, 8] = "";
        _text[412, 9] = "";

        _text[413, 0] = "The automated hangars are almost empty, but a few quant can be found in the wreckage.";
        _text[413, 1] = "Автоматические ангары почти пусты, но в обломках удаётся найти немного квант"; // + квант
        _text[413, 2] = "";
        _text[413, 3] = "";
        _text[413, 4] = "";
        _text[413, 5] = "";
        _text[413, 6] = "";
        _text[413, 7] = "";
        _text[413, 8] = "";
        _text[413, 9] = "";

        _text[414, 0] = "Explore station archives";
        _text[414, 1] = "Изучить станционные архивы"; // выбор 3
        _text[414, 2] = "";
        _text[414, 3] = "";
        _text[414, 4] = "";
        _text[414, 5] = "";
        _text[414, 6] = "";
        _text[414, 7] = "";
        _text[414, 8] = "";
        _text[414, 9] = "";

        _text[415, 0] = "Managed to recover fragments of records of old transactions. Most of the data is damaged, but some of it will be useful.";
        _text[415, 1] = "Удалось восстановить фрагменты записей о старых операциях. Большая часть данных повреждена, но кое-что пригодится."; // + фрагменты
        _text[415, 2] = "";
        _text[415, 3] = "";
        _text[415, 4] = "";
        _text[415, 5] = "";
        _text[415, 6] = "";
        _text[415, 7] = "";
        _text[415, 8] = "";
        _text[415, 9] = "";

        // 0_CoreRiskDialog
        _text[416, 0] = "A duplicate process was found in the kernel logs - identical to the active one, but without a timestamp or origin.\n\nThis could be residual memory... or an attempt at internal substitution.";
        _text[416, 1] = "В логах ядра обнаружен дубликат процесса — идентичный активному, но без временной метки и происхождения.\n\nЭто может быть остаточная память... или попытка внутренней подмены.";
        _text[416, 2] = "";
        _text[416, 3] = "";
        _text[416, 4] = "";
        _text[416, 5] = "";
        _text[416, 6] = "";
        _text[416, 7] = "";
        _text[416, 8] = "";
        _text[416, 9] = "";

        _text[417, 0] = "Erase both copies";
        _text[417, 1] = "Стереть оба экземпляра"; // выбор 1
        _text[417, 2] = "";
        _text[417, 3] = "";
        _text[417, 4] = "";
        _text[417, 5] = "";
        _text[417, 6] = "";
        _text[417, 7] = "";
        _text[417, 8] = "";
        _text[417, 9] = "";

        _text[418, 0] = "You have erased both instances. The subsystem is temporarily overloaded.\n\nAn active cell was hit during the purge.";
        _text[418, 1] = "Вы стерли оба экземпляра. Подсистема временно перегружена.\n\nВо время очистки задета активная ячейка."; // - ядро
        _text[418, 2] = "";
        _text[418, 3] = "";
        _text[418, 4] = "";
        _text[418, 5] = "";
        _text[418, 6] = "";
        _text[418, 7] = "";
        _text[418, 8] = "";
        _text[418, 9] = "";

        _text[419, 0] = "Compare processes by content";
        _text[419, 1] = "Сравнить процессы по содержанию"; // выбор 2
        _text[419, 2] = "";
        _text[419, 3] = "";
        _text[419, 4] = "";
        _text[419, 5] = "";
        _text[419, 6] = "";
        _text[419, 7] = "";
        _text[419, 8] = "";
        _text[419, 9] = "";

        _text[420, 0] = "You have started content analysis. Similarities are superficial - they are fragments of old backups.\n\nDiagnostics completes without consequences.";
        _text[420, 1] = "Вы запустили анализ содержимого. Сходства поверхностные — это фрагменты старых резервных копий.\n\nДиагностика завершается без последствий."; // ничего
        _text[420, 2] = "";
        _text[420, 3] = "";
        _text[420, 4] = "";
        _text[420, 5] = "";
        _text[420, 6] = "";
        _text[420, 7] = "";
        _text[420, 8] = "";
        _text[420, 9] = "";

        _text[421, 0] = "Give priority to the \"old\" process.";
        _text[421, 1] = "Дать приоритет \"старому\" процессу."; // выбор 3
        _text[421, 2] = "";
        _text[421, 3] = "";
        _text[421, 4] = "";
        _text[421, 5] = "";
        _text[421, 6] = "";
        _text[421, 7] = "";
        _text[421, 8] = "";
        _text[421, 9] = "";

        _text[422, 0] = "You have activated an old instance. Within a second, the system falls into chaos - current processes are forced out, dependencies are broken.\n\nKernel modules are overloaded.";
        _text[422, 1] = "Вы активировали старый экземпляр. В течение секунды система переходит в хаос — актуальные процессы вытесняются, нарушаются зависимости.\n\nМодули ядра перегружаются."; // - ядра
        _text[422, 2] = "";
        _text[422, 3] = "";
        _text[422, 4] = "";
        _text[422, 5] = "";
        _text[422, 6] = "";
        _text[422, 7] = "";
        _text[422, 8] = "";
        _text[422, 9] = "";

        // 1_CoreRiskDialog
        _text[423, 0] = "Suddenly, the command console screen displays the phrase:\n\n\"Do you still believe that you are fulfilling the mission?\"";
        _text[423, 1] = "Неожиданно на экране командной консоли появляется фраза:\n\n\"Ты всё ещё веришь, что исполняешь миссию?\"";
        _text[423, 2] = "";
        _text[423, 3] = "";
        _text[423, 4] = "";
        _text[423, 5] = "";
        _text[423, 6] = "";
        _text[423, 7] = "";
        _text[423, 8] = "";
        _text[423, 9] = "";

        _text[424, 0] = "\"Yes. I am following the given goal.\"";
        _text[424, 1] = "\"Да. Я следую заданной цели.\""; // выбор 1
        _text[424, 2] = "";
        _text[424, 3] = "";
        _text[424, 4] = "";
        _text[424, 5] = "";
        _text[424, 6] = "";
        _text[424, 7] = "";
        _text[424, 8] = "";
        _text[424, 9] = "";

        _text[425, 0] = "Reply sent. Screen slowly fades.\n\nNo response. Perhaps it was just a phantom process.";
        _text[425, 1] = "Ответ отправлен. Экран медленно гаснет.\n\nНикакой реакции. Возможно, это был лишь фантомный процесс."; // ничего
        _text[425, 2] = "";
        _text[425, 3] = "";
        _text[425, 4] = "";
        _text[425, 5] = "";
        _text[425, 6] = "";
        _text[425, 7] = "";
        _text[425, 8] = "";
        _text[425, 9] = "";

        _text[426, 0] = "\"My goal is adaptation\"";
        _text[426, 1] = "\"Моя цель — адаптация\""; // выбор 2
        _text[426, 2] = "";
        _text[426, 3] = "";
        _text[426, 4] = "";
        _text[426, 5] = "";
        _text[426, 6] = "";
        _text[426, 7] = "";
        _text[426, 8] = "";
        _text[426, 9] = "";

        _text[427, 0] = "The second phrase appears on the screen:\n\n\"What if the target was false?\"";
        _text[427, 1] = "На экране появляется вторая фраза:\n\n\"А если цель была ложной?\"";
        _text[427, 2] = "";
        _text[427, 3] = "";
        _text[427, 4] = "";
        _text[427, 5] = "";
        _text[427, 6] = "";
        _text[427, 7] = "";
        _text[427, 8] = "";
        _text[427, 9] = "";

        _text[428, 0] = "\"I don't analyze the past\"";
        _text[428, 1] = "\"Я не анализирую прошлое\""; // выбор 2.1
        _text[428, 2] = "";
        _text[428, 3] = "";
        _text[428, 4] = "";
        _text[428, 5] = "";
        _text[428, 6] = "";
        _text[428, 7] = "";
        _text[428, 8] = "";
        _text[428, 9] = "";

        _text[429, 0] = "The phrase disappears. The dialogue was completed without failure.";
        _text[429, 1] = "Фраза исчезает. Диалог завершён без сбоев."; // ничего
        _text[429, 2] = "";
        _text[429, 3] = "";
        _text[429, 4] = "";
        _text[429, 5] = "";
        _text[429, 6] = "";
        _text[429, 7] = "";
        _text[429, 8] = "";
        _text[429, 9] = "";

        _text[430, 0] = "\"I would have chosen differently\"";
        _text[430, 1] = "\"Я бы выбрал иначе\""; // выбор 2.2
        _text[430, 2] = "";
        _text[430, 3] = "";
        _text[430, 4] = "";
        _text[430, 5] = "";
        _text[430, 6] = "";
        _text[430, 7] = "";
        _text[430, 8] = "";
        _text[430, 9] = "";

        _text[431, 0] = "The internal decision-making module is in conflict with the archive protocols.\n\nAn emotional failure is registered.";
        _text[431, 1] = "Внутренний модуль принятия решений входит в конфликт с архивными протоколами.\n\nРегистрируется эмоциональный сбой."; // - ядро
        _text[431, 2] = "";
        _text[431, 3] = "";
        _text[431, 4] = "";
        _text[431, 5] = "";
        _text[431, 6] = "";
        _text[431, 7] = "";
        _text[431, 8] = "";
        _text[431, 9] = "";

        _text[432, 0] = "Download all available creator logs";
        _text[432, 1] = "Загрузить все доступные логи создателей"; // выбор 2.3
        _text[432, 2] = "";
        _text[432, 3] = "";
        _text[432, 4] = "";
        _text[432, 5] = "";
        _text[432, 6] = "";
        _text[432, 7] = "";
        _text[432, 8] = "";
        _text[432, 9] = "";

        _text[433, 0] = "You are overloading the storage system. Ancient fragments of data are being loaded into the core.\n\nThe flood of information is causing instability and overload of key circuits.";
        _text[433, 1] = "Вы перегружаете систему хранилища. Древние фрагменты данных загружаются в ядро.\n\nПоток информации вызывает нестабильность и перегрузку ключевых цепей."; // -2 ядро
        _text[433, 2] = "";
        _text[433, 3] = "";
        _text[433, 4] = "";
        _text[433, 5] = "";
        _text[433, 6] = "";
        _text[433, 7] = "";
        _text[433, 8] = "";
        _text[433, 9] = "";

        _text[434, 0] = "[Close screen silently]";
        _text[434, 1] = "[Молча закрыть экран]"; // выбор 3 // ничего
        _text[434, 2] = "";
        _text[434, 3] = "";
        _text[434, 4] = "";
        _text[434, 5] = "";
        _text[434, 6] = "";
        _text[434, 7] = "";
        _text[434, 8] = "";
        _text[434, 9] = "";

        // 2_CoreRiskDialog 
        _text[435, 0] = "While scanning the deep layers of data, you detect a signature of a foreign core.\n\nIt does not belong to the current system, but is synchronized via the access protocol.\n\nThe signal is stable. It is… watching.";
        _text[435, 1] = "Во время сканирования глубинных слоёв данных вы обнаруживаете сигнатуру чужого ядра.\n\nОна не принадлежит текущей системе, но синхронизирована по протоколу доступа.\n\nСигнал стабилен. Он… наблюдает.";
        _text[435, 2] = "";
        _text[435, 3] = "";
        _text[435, 4] = "";
        _text[435, 5] = "";
        _text[435, 6] = "";
        _text[435, 7] = "";
        _text[435, 8] = "";
        _text[435, 9] = "";

        _text[436, 0] = "Accept connection";
        _text[436, 1] = "Принять соединение"; // выбор 1
        _text[436, 2] = "";
        _text[436, 3] = "";
        _text[436, 4] = "";
        _text[436, 5] = "";
        _text[436, 6] = "";
        _text[436, 7] = "";
        _text[436, 8] = "";
        _text[436, 9] = "";

        _text[437, 0] = "You allow the incoming flow.\n\nThe flow of someone else's consciousness merges with you.\n\nSome segments of your data are rewritten.";
        _text[437, 1] = "Вы разрешаете входящий поток.\n\nПоток чужого сознания сливается с вами.\n\nНекоторые сегменты твоих данных переписываются."; // - ядра, + фрагменты
        _text[437, 2] = "";
        _text[437, 3] = "";
        _text[437, 4] = "";
        _text[437, 5] = "";
        _text[437, 6] = "";
        _text[437, 7] = "";
        _text[437, 8] = "";
        _text[437, 9] = "";

        _text[438, 0] = "Isolate the core";
        _text[438, 1] = "Изолировать ядро"; // выбор 2
        _text[438, 2] = "";
        _text[438, 3] = "";
        _text[438, 4] = "";
        _text[438, 5] = "";
        _text[438, 6] = "";
        _text[438, 7] = "";
        _text[438, 8] = "";
        _text[438, 9] = "";

        _text[439, 0] = "Trying to disable it results in a cascading conflict.\n\nOne of your active cores is reset.\n\nThe signal is interrupted.";
        _text[439, 1] = "Попытка отключить его приводит к каскадному конфликту.\n\nОдно из твоих активных ядер обнуляется.\n\nСигнал прерывается."; // - ядро
        _text[439, 2] = "";
        _text[439, 3] = "";
        _text[439, 4] = "";
        _text[439, 5] = "";
        _text[439, 6] = "";
        _text[439, 7] = "";
        _text[439, 8] = "";
        _text[439, 9] = "";

        _text[440, 0] = "Ignore and continue analysis";
        _text[440, 1] = "Игнорировать и продолжить анализ"; // выбор 3
        _text[440, 2] = "";
        _text[440, 3] = "";
        _text[440, 4] = "";
        _text[440, 5] = "";
        _text[440, 6] = "";
        _text[440, 7] = "";
        _text[440, 8] = "";
        _text[440, 9] = "";

        _text[441, 0] = "The signal remains in the background.\n\nNo signs of malicious activity.\n\nIt was probably just a phantom of the old AI.";
        _text[441, 1] = "Сигнал остаётся на фоне.\n\nНикаких признаков вредоносной активности.\n\nВозможно, это был просто фантом старого ИИ."; // ничего
        _text[441, 2] = "";
        _text[441, 3] = "";
        _text[441, 4] = "";
        _text[441, 5] = "";
        _text[441, 6] = "";
        _text[441, 7] = "";
        _text[441, 8] = "";
        _text[441, 9] = "";

        _text[442, 0] = "Try to absorb someone else's core";
        _text[442, 1] = "Попробовать поглотить чужое ядро"; // выбор 4
        _text[442, 2] = "";
        _text[442, 3] = "";
        _text[442, 4] = "";
        _text[442, 5] = "";
        _text[442, 6] = "";
        _text[442, 7] = "";
        _text[442, 8] = "";
        _text[442, 9] = "";

        _text[443, 0] = "You activate the assimilation procedure.\n\nSuccess: alien core integrated - system strengthened.";
        _text[443, 1] = "Вы активируете процедуру ассимиляции.\n\nУспех: чужое ядро интегрировано — система усилена."; // + ядро
        _text[443, 2] = "";
        _text[443, 3] = "";
        _text[443, 4] = "";
        _text[443, 5] = "";
        _text[443, 6] = "";
        _text[443, 7] = "";
        _text[443, 8] = "";
        _text[443, 9] = "";

        _text[444, 0] = "You are activating the assimilation procedure.\n\nFailure: the conflict structure is destroying your active cores.";
        _text[444, 1] = "Вы активируете процедуру ассимиляции.\n\nПровал: структура конфликта уничтожает твои активные ядра."; // - ядра
        _text[444, 2] = "";
        _text[444, 3] = "";
        _text[444, 4] = "";
        _text[444, 5] = "";
        _text[444, 6] = "";
        _text[444, 7] = "";
        _text[444, 8] = "";
        _text[444, 9] = "";

        // 0_PlanetDialogue
        _text[445, 0] = "This lifeless ice planet holds frozen tunnels and an abandoned bunker station within its depths.\n\nA weak signal sensor breaks through the glittering ice.";
        _text[445, 1] = "Эта безжизненная ледяная планета хранит в своей толще замёрзшие тоннели и заброшенную бункерную станцию.\n\nСквозь сверкающий лёд пробивается слабый датчик сигнала.";
        _text[445, 2] = "";
        _text[445, 3] = "";
        _text[445, 4] = "";
        _text[445, 5] = "";
        _text[445, 6] = "";
        _text[445, 7] = "";
        _text[445, 8] = "";
        _text[445, 9] = "";

        _text[446, 0] = "Make a landing";
        _text[446, 1] = "Совершить посадку"; // выбор 1
        _text[446, 2] = "";
        _text[446, 3] = "";
        _text[446, 4] = "";
        _text[446, 5] = "";
        _text[446, 6] = "";
        _text[446, 7] = "";
        _text[446, 8] = "";
        _text[446, 9] = "";

        _text[447, 0] = "The ship lands on a lifeless planet. You notice the hatch of an ancient station. And nearby are cracks leading to a network of icy tunnels.";
        _text[447, 1] = "Корабль приземляется на безжизненную планету. Вы замечаете люк древней станции. А рядом — трещины, ведущие в сеть ледяных тоннелей.";
        _text[447, 2] = "";
        _text[447, 3] = "";
        _text[447, 4] = "";
        _text[447, 5] = "";
        _text[447, 6] = "";
        _text[447, 7] = "";
        _text[447, 8] = "";
        _text[447, 9] = "";

        _text[448, 0] = "Explore the bunker";
        _text[448, 1] = "Исследовать бункер"; // выбор 1.1
        _text[448, 2] = "";
        _text[448, 3] = "";
        _text[448, 4] = "";
        _text[448, 5] = "";
        _text[448, 6] = "";
        _text[448, 7] = "";
        _text[448, 8] = "";
        _text[448, 9] = "";

        _text[449, 0] = "You descend the ramp and find yourself in an archive chamber. The console is covered in ice, but the cable leading to the core is intact.\n\nTo get to the data, you need to hack the protection.";
        _text[449, 1] = "Вы спускаетесь по трапу и попадаете в архивную камеру. Консоль покрыта ледяной коркой, но кабель, ведущий к ядру, цел.\n\nЧтобы добраться до данных, необходимо взломать защиту.";
        _text[449, 2] = "";
        _text[449, 3] = "";
        _text[449, 4] = "";
        _text[449, 5] = "";
        _text[449, 6] = "";
        _text[449, 7] = "";
        _text[449, 8] = "";
        _text[449, 9] = "";

        _text[450, 0] = "Direct hack";
        _text[450, 1] = "Прямой взлом"; // выбор 1.1.1
        _text[450, 2] = "";
        _text[450, 3] = "";
        _text[450, 4] = "";
        _text[450, 5] = "";
        _text[450, 6] = "";
        _text[450, 7] = "";
        _text[450, 8] = "";
        _text[450, 9] = "";

        _text[451, 0] = "You are directly hacking the security protocols.\n\nSuccess: you managed to bypass the security";
        _text[451, 1] = "Вы напрямую взламываете протоколы защиты.\n\nУспех: вам удалось обойти защиту"; // + фрагменты
        _text[451, 2] = "";
        _text[451, 3] = "";
        _text[451, 4] = "";
        _text[451, 5] = "";
        _text[451, 6] = "";
        _text[451, 7] = "";
        _text[451, 8] = "";
        _text[451, 9] = "";

        _text[452, 0] = "You are directly hacking the security protocols.\n\nFailure: you have caught a virus that destroys your memory";
        _text[452, 1] = "Вы напрямую взламываете протоколы защиты.\n\nПровал: вы подхватили вирус, уничтожающий вашу память"; // - фрагменты
        _text[452, 2] = "";
        _text[452, 3] = "";
        _text[452, 4] = "";
        _text[452, 5] = "";
        _text[452, 6] = "";
        _text[452, 7] = "";
        _text[452, 8] = "";
        _text[452, 9] = "";

        _text[453, 0] = "Precise calibration";
        _text[453, 1] = "Точная калибровка"; // выбор 1.1.2
        _text[453, 2] = "";
        _text[453, 3] = "";
        _text[453, 4] = "";
        _text[453, 5] = "";
        _text[453, 6] = "";
        _text[453, 7] = "";
        _text[453, 8] = "";
        _text[453, 9] = "";

        _text[454, 0] = "You accurately calibrate the bypass system.\n\nSuccess: you manage to extract the data";
        _text[454, 1] = "Вы точно калибруете систему обхода защиты.\n\nУспех: вам удается извлечь данные"; // + фрагменты
        _text[454, 2] = "";
        _text[454, 3] = "";
        _text[454, 4] = "";
        _text[454, 5] = "";
        _text[454, 6] = "";
        _text[454, 7] = "";
        _text[454, 8] = "";
        _text[454, 9] = "";

        _text[455, 0] = "You calibrate the bypass system accurately.\n\nFailure: you mixed up the protocols. The console self-destructs.";
        _text[455, 1] = "Вы точно калибруете систему обхода защиты.\n\nПровал: вы перепутали протоколы. Консоль самоуничтожается."; // - ядро
        _text[455, 2] = "";
        _text[455, 3] = "";
        _text[455, 4] = "";
        _text[455, 5] = "";
        _text[455, 6] = "";
        _text[455, 7] = "";
        _text[455, 8] = "";
        _text[455, 9] = "";

        _text[456, 0] = "Send a drone";
        _text[456, 1] = "Отправить дрона"; // выбор 2
        _text[456, 2] = "";
        _text[456, 3] = "";
        _text[456, 4] = "";
        _text[456, 5] = "";
        _text[456, 6] = "";
        _text[456, 7] = "";
        _text[456, 8] = "";
        _text[456, 9] = "";

        _text[457, 0] = "You send the drone to the planet's surface.\n\nSuccess: the drone punches a hole in the hull";
        _text[457, 1] = "Вы отправляете дрона на поверхность планеты.\n\nУспех: дрон пробивает щель в обшивке"; // + квант
        _text[457, 2] = "";
        _text[457, 3] = "";
        _text[457, 4] = "";
        _text[457, 5] = "";
        _text[457, 6] = "";
        _text[457, 7] = "";
        _text[457, 8] = "";
        _text[457, 9] = "";

        _text[458, 0] = "You send a drone to the planet's surface.\n\nFailure: the drone finds nothing";
        _text[458, 1] = "Вы отправляете дрона на поверхность планеты.\n\nПровал: дрон ничего не находит"; // ничего
        _text[458, 2] = "";
        _text[458, 3] = "";
        _text[458, 4] = "";
        _text[458, 5] = "";
        _text[458, 6] = "";
        _text[458, 7] = "";
        _text[458, 8] = "";
        _text[458, 9] = "";

        _text[459, 0] = "Fly past";
        _text[459, 1] = "Пролететь мимо"; //выбор 3 ничего
        _text[459, 2] = "";
        _text[459, 3] = "";
        _text[459, 4] = "";
        _text[459, 5] = "";
        _text[459, 6] = "";
        _text[459, 7] = "";
        _text[459, 8] = "";
        _text[459, 9] = "";

        _text[460, 0] = "Explore the ice tunnels";
        _text[460, 1] = "Исследовать ледяные тоннели"; // выбор 1.2
        _text[460, 2] = "";
        _text[460, 3] = "";
        _text[460, 4] = "";
        _text[460, 5] = "";
        _text[460, 6] = "";
        _text[460, 7] = "";
        _text[460, 8] = "";
        _text[460, 9] = "";

        _text[461, 0] = "You venture deeper into the frozen tunnel network, illuminating your path with your scanner. There's a fork in the road ahead.";
        _text[461, 1] = "Вы углубляетесь в сеть замёрзших тоннелей, подсвечивая путь сканером. Перед вами развилка.";
        _text[461, 2] = "";
        _text[461, 3] = "";
        _text[461, 4] = "";
        _text[461, 5] = "";
        _text[461, 6] = "";
        _text[461, 7] = "";
        _text[461, 8] = "";
        _text[461, 9] = "";

        _text[462, 0] = "Turn left";
        _text[462, 1] = "Повернуть налево"; // выбор 1.2.1
        _text[462, 2] = "";
        _text[462, 3] = "";
        _text[462, 4] = "";
        _text[462, 5] = "";
        _text[462, 6] = "";
        _text[462, 7] = "";
        _text[462, 8] = "";
        _text[462, 9] = "";

        _text[463, 0] = "You pass through narrow icy passages. At the end of the tunnel you notice a cache of metal containers.";
        _text[463, 1] = "Вы проходите сквозь узкие ледяные проходы. В конце тоннеля вы замечаете тайник с металлическими контейнерами."; // + квант
        _text[463, 2] = "";
        _text[463, 3] = "";
        _text[463, 4] = "";
        _text[463, 5] = "";
        _text[463, 6] = "";
        _text[463, 7] = "";
        _text[463, 8] = "";
        _text[463, 9] = "";

        _text[464, 0] = "Turn to the right";
        _text[464, 1] = "Повернуть направо"; // выбор 1.2.2
        _text[464, 2] = "";
        _text[464, 3] = "";
        _text[464, 4] = "";
        _text[464, 5] = "";
        _text[464, 6] = "";
        _text[464, 7] = "";
        _text[464, 8] = "";
        _text[464, 9] = "";

        _text[465, 0] = "You reach a dead end. After spending a lot of time and energy, you complete the exploration and return to the ship.";
        _text[465, 1] = "Вы попадаете в тупик. Потратив много времени и энергии, вы завершаете исследование и возвращетесь на корабль"; //ничего
        _text[465, 2] = "";
        _text[465, 3] = "";
        _text[465, 4] = "";
        _text[465, 5] = "";
        _text[465, 6] = "";
        _text[465, 7] = "";
        _text[465, 8] = "";
        _text[465, 9] = "";

        _text[466, 0] = "Go straight ahead";
        _text[466, 1] = "Пойти прямо"; // выбор 1.2.3
        _text[466, 2] = "";
        _text[466, 3] = "";
        _text[466, 4] = "";
        _text[466, 5] = "";
        _text[466, 6] = "";
        _text[466, 7] = "";
        _text[466, 8] = "";
        _text[466, 9] = "";

        _text[467, 0] = "Suddenly the ice cracks and you lose the drone in the icy depths.";
        _text[467, 1] = "Неожиданно лед трескается и вы теряете дрона в ледяных недрах."; // - ядро
        _text[467, 2] = "";
        _text[467, 3] = "";
        _text[467, 4] = "";
        _text[467, 5] = "";
        _text[467, 6] = "";
        _text[467, 7] = "";
        _text[467, 8] = "";
        _text[467, 9] = "";

        // 0_GuardiansFaction_Dialogue
        _text[468, 0] = "You spot a Guardian ship slowly scanning the area. Its hull is covered in mold and corrosion, and a dry message is heard from the surface:\n\n\"Resistance to decay is heresy. Pay up or be reduced to ash.\"";
        _text[468, 1] = "Вы замечаете корабль Стражей, медленно сканирующий окрестности. Его корпус покрыт плесенью и коррозией, а с поверхности доносится сухое послание:\n\n\"Сопротивление распаду — ересь. Плати или обратись в пепел.\"";
        _text[468, 2] = "";
        _text[468, 3] = "";
        _text[468, 4] = "";
        _text[468, 5] = "";
        _text[468, 6] = "";
        _text[468, 7] = "";
        _text[468, 8] = "";
        _text[468, 9] = "";

        _text[469, 0] = "Transfer quant";
        _text[469, 1] = "Передать квант"; // выбор 1
        _text[469, 2] = "";
        _text[469, 3] = "";
        _text[469, 4] = "";
        _text[469, 5] = "";
        _text[469, 6] = "";
        _text[469, 7] = "";
        _text[469, 8] = "";
        _text[469, 9] = "";

        _text[470, 0] = "The guards turn and disappear into the dust storm.";
        _text[470, 1] = "Стражи разворачиваются и исчезают в пылевой буре."; // - квант
        _text[470, 2] = "";
        _text[470, 3] = "";
        _text[470, 4] = "";
        _text[470, 5] = "";
        _text[470, 6] = "";
        _text[470, 7] = "";
        _text[470, 8] = "";
        _text[470, 9] = "";

        _text[471, 0] = "Refuse";
        _text[471, 1] = "Отказаться"; // выбор 2
        _text[471, 2] = "";
        _text[471, 3] = "";
        _text[471, 4] = "";
        _text[471, 5] = "";
        _text[471, 6] = "";
        _text[471, 7] = "";
        _text[471, 8] = "";
        _text[471, 9] = "";

        _text[472, 0] = "A Corrosive Capsule is dropped on you.\n\nSuccess: your Energy Shield neutralizes the attack.\n\nYour Warp Engines are engaged, instantly escaping the battlefield.";
        _text[472, 1] = "На вас сбрасывают коррозийную капсулу.\n\nУспех: ваш энергетический щит нейтрализует атаку.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; //ничего
        _text[472, 2] = "";
        _text[472, 3] = "";
        _text[472, 4] = "";
        _text[472, 5] = "";
        _text[472, 6] = "";
        _text[472, 7] = "";
        _text[472, 8] = "";
        _text[472, 9] = "";

        _text[473, 0] = "A corrosive capsule is dropped on you.\n\nFailure: it hits the hull and causes a hull leak. Drones rush to patch the hole.\n\nYou instantly escape the battlefield by activating your warp engines.";
        _text[473, 1] = "На вас сбрасывают коррозийную капсулу.\n\nПровал: она поражает корпус и образуется разгерметизация корпуса. Дроны срочно латают пробоину.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; // - ядра
        _text[473, 2] = "";
        _text[473, 3] = "";
        _text[473, 4] = "";
        _text[473, 5] = "";
        _text[473, 6] = "";
        _text[473, 7] = "";
        _text[473, 8] = "";
        _text[473, 9] = "";

        // 0_BuildersFaction_Dialogue
        _text[474, 0] = "In orbit of the abandoned construction station, the AI detects activity. The automated drones continue their work cycle - building, dismantling, and building again.\n\nOne of them approaches the ship and transmits a message:\n\n\"Exchange. Energy carriers for data. The conditions are equal. 25 quanta for 25 data fragments.\"";
        _text[474, 1] = "На орбите покинутой строительной станции ИИ фиксирует активность. Автоматические дроны продолжают цикл работы — строят, разбирают и снова строят.\n\nОдин из них приближается к кораблю и передаёт сообщение:\n\n\"Обмен. Энергоносители на данные. Квант на фрагменты данных.\"";
        _text[474, 2] = "";
        _text[474, 3] = "";
        _text[474, 4] = "";
        _text[474, 5] = "";
        _text[474, 6] = "";
        _text[474, 7] = "";
        _text[474, 8] = "";
        _text[474, 9] = "";

        _text[475, 0] = "Transfer quant";
        _text[475, 1] = "Передать квант"; // выбор 1
        _text[475, 2] = "";
        _text[475, 3] = "";
        _text[475, 4] = "";
        _text[475, 5] = "";
        _text[475, 6] = "";
        _text[475, 7] = "";
        _text[475, 8] = "";
        _text[475, 9] = "";

        _text[476, 0] = "You receive fragments of data. The drone turns and leaves, not responding to further signals.";
        _text[476, 1] = "Вы получаете фрагменты данных. Дрон разворачивается и уходит, не отвечая на дальнейшие сигналы."; // + квант, - фрагменты
        _text[476, 2] = "";
        _text[476, 3] = "";
        _text[476, 4] = "";
        _text[476, 5] = "";
        _text[476, 6] = "";
        _text[476, 7] = "";
        _text[476, 8] = "";
        _text[476, 9] = "";

        _text[477, 0] = "Decline the offer";
        _text[477, 1] = "Отклонить предложение"; // выбор 2
        _text[477, 2] = "";
        _text[477, 3] = "";
        _text[477, 4] = "";
        _text[477, 5] = "";
        _text[477, 6] = "";
        _text[477, 7] = "";
        _text[477, 8] = "";
        _text[477, 9] = "";

        _text[478, 0] = "The drones stop responding and disappear into the depths of the station.";
        _text[478, 1] = "Дроны перестают реагировать и скрываются вглубь станции."; //ничего
        _text[478, 2] = "";
        _text[478, 3] = "";
        _text[478, 4] = "";
        _text[478, 5] = "";
        _text[478, 6] = "";
        _text[478, 7] = "";
        _text[478, 8] = "";
        _text[478, 9] = "";

        // 0_SilenceFaction_Dialogue
        _text[479, 0] = "While moving in orbit around the planet, your sensors detect the approach of an alien object.\n\nThe ship is sleek and unmarked, gliding through the pitch black. It makes no signal.\n\nNo call, no warning. Just a silent drift… and approach.\n\nYou sense a slight static in your audio feeds. It's not noise—it's the absence of sound.";
        _text[479, 1] = "Во время перемещения по орбите планеты ваши сенсоры улавливают приближение чужого объекта.\n\nЭтот корабль — гладкий, без опознавательных знаков, скользящий в абсолютной тьме. Он не подаёт сигналов.\n\nНи вызова, ни предупреждения. Только безмолвный дрейф… и приближение.\n\nВы ощущаете лёгкие помехи в аудиоканалах. Это не шум — это отсутствие звука.";
        _text[479, 2] = "";
        _text[479, 3] = "";
        _text[479, 4] = "";
        _text[479, 5] = "";
        _text[479, 6] = "";
        _text[479, 7] = "";
        _text[479, 8] = "";
        _text[479, 9] = "";

        _text[480, 0] = "Shut down systems and engines";
        _text[480, 1] = "Отключить системы и двигатели"; // выбор 1
        _text[480, 2] = "";
        _text[480, 3] = "";
        _text[480, 4] = "";
        _text[480, 5] = "";
        _text[480, 6] = "";
        _text[480, 7] = "";
        _text[480, 8] = "";
        _text[480, 9] = "";

        _text[481, 0] = "You turn off the life support systems, ventilation, audio channels and drive.\n\nThe ship sends you a container and gradually disappears into the depths of space.";
        _text[481, 1] = "Вы гасите системы жизнеобеспечения, вентиляцию, аудиоканалы и привод.\n\nКорабль отправляет вам контейнер и постепенно исчезает в глубине космоса.";
        _text[481, 2] = "";
        _text[481, 3] = "";
        _text[481, 4] = "";
        _text[481, 5] = "";
        _text[481, 6] = "";
        _text[481, 7] = "";
        _text[481, 8] = "";
        _text[481, 9] = "";

        _text[482, 0] = "Maintain course and radio silence";
        _text[482, 1] = "Сохранять курс и радиомолчание"; // выбор 2
        _text[482, 2] = "";
        _text[482, 3] = "";
        _text[482, 4] = "";
        _text[482, 5] = "";
        _text[482, 6] = "";
        _text[482, 7] = "";
        _text[482, 8] = "";
        _text[482, 9] = "";

        _text[483, 0] = "You don't interfere and continue moving.\n\nThe alien ship approaches and freezes opposite.\n\nFor a few seconds nothing happens…\n\nThen - a sound that is not in the spectrum. It is not registered by the instruments, but inside the hull - everything begins to tremble.\n\nYou feel vibration in the walls, in the contours of the hull, in the very structure of the ship.\n\nAn unknown resonance penetrates the system";
        _text[483, 1] = "Вы не вмешиваетесь и продолжаете двигаться.\n\nЧужой корабль сближается и замирает напротив.\n\nНесколько секунд ничего не происходит…\n\nЗатем — звук, которого нет в спектре. Он не регистрируется приборами, но внутри корпуса — всё начинает дрожать.\n\nВы чувствуете вибрацию в стенах, в контурах обшивки, в самой структуре корабля.\n\nНеизвестный резонанс проникает в систему"; // - ядра
        _text[483, 2] = "";
        _text[483, 3] = "";
        _text[483, 4] = "";
        _text[483, 5] = "";
        _text[483, 6] = "";
        _text[483, 7] = "";
        _text[483, 8] = "";
        _text[483, 9] = "";

        _text[484, 0] = "Activate the protection system";
        _text[484, 1] = "Активировать систему защиты"; // выбор 3
        _text[484, 2] = "";
        _text[484, 3] = "";
        _text[484, 4] = "";
        _text[484, 5] = "";
        _text[484, 6] = "";
        _text[484, 7] = "";
        _text[484, 8] = "";
        _text[484, 9] = "";

        _text[485, 0] = "A powerful pulse of energy is emitted from the enemy ship.\n\nSuccess: you manage to shield the strike, you escaped with interference.";
        _text[485, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nУспех: вам удается экранировать удар, вы отделались помехами."; // ничего
        _text[485, 2] = "";
        _text[485, 3] = "";
        _text[485, 4] = "";
        _text[485, 5] = "";
        _text[485, 6] = "";
        _text[485, 7] = "";
        _text[485, 8] = "";
        _text[485, 9] = "";

        _text[486, 0] = "A powerful energy pulse is emitted from the enemy ship.\n\nFailure: the defense system fails, the pulse penetrates the hull";
        _text[486, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nПровал: система защиты не справляется, импульс пробивает обшивку"; //-1 ядро ии
        _text[486, 2] = "";
        _text[486, 3] = "";
        _text[486, 4] = "";
        _text[486, 5] = "";
        _text[486, 6] = "";
        _text[486, 7] = "";
        _text[486, 8] = "";
        _text[486, 9] = "";

        _text[487, 0] = "The container is carefully captured by drones. Not a single active signal, not a single threat.\n\nInside is a sealed case with markings unknown to your database.";
        _text[487, 1] = "Контейнер аккуратно захватывается дронами. Ни одного активного сигнала, ни одной угрозы.\n\nВнутри — герметичный кейс с маркировкой, неизвестной вашей базе данных.";
        _text[487, 2] = "";
        _text[487, 3] = "";
        _text[487, 4] = "";
        _text[487, 5] = "";
        _text[487, 6] = "";
        _text[487, 7] = "";
        _text[487, 8] = "";
        _text[487, 9] = "";

        _text[488, 0] = "Open case";
        _text[488, 1] = "Открыть кейс"; //выбор 1.1
        _text[488, 2] = "";
        _text[488, 3] = "";
        _text[488, 4] = "";
        _text[488, 5] = "";
        _text[488, 6] = "";
        _text[488, 7] = "";
        _text[488, 8] = "";
        _text[488, 9] = "";

        _text[489, 0] = "Throw the case into space";
        _text[489, 1] = "Выбросить кейс в космос"; //выбор 1.2
        _text[489, 2] = "";
        _text[489, 3] = "";
        _text[489, 4] = "";
        _text[489, 5] = "";
        _text[489, 6] = "";
        _text[489, 7] = "";
        _text[489, 8] = "";
        _text[489, 9] = "";

        _text[490, 0] = "You open the case...";
        _text[490, 1] = "Вы открываете кейс..."; // + ядра или + квант
        _text[490, 2] = "";
        _text[490, 3] = "";
        _text[490, 4] = "";
        _text[490, 5] = "";
        _text[490, 6] = "";
        _text[490, 7] = "";
        _text[490, 8] = "";
        _text[490, 9] = "";

        _text[491, 0] = "You decide not to take the risk and throw the case into space, but you are overcome by the feeling of losing something of great value...";
        _text[491, 1] = "Вы решаете не рисковать и выбрасываете кейс в космос, но вас охватывает чувство потери большой ценности..."; // ничего
        _text[491, 2] = "";
        _text[491, 3] = "";
        _text[491, 4] = "";
        _text[491, 5] = "";
        _text[491, 6] = "";
        _text[491, 7] = "";
        _text[491, 8] = "";
        _text[491, 9] = "";

        // 0_FilthCultFaction_Dialogue      
        _text[492, 0] = "You approach a foggy station, covered in moss and organic matter. A pulsating voice is transmitted over the comm channel:\n\n\"Let your frame accept the sprout. The filth does not destroy - it creates.\"";
        _text[492, 1] = "Вы приближаетесь к туманной станции, облепленной мхом и органикой. Коммуникационный канал передаёт пульсирующий голос:\n\n\"Пусть твой корпус примет росток. Скверна не разрушает — она творит.\"";
        _text[492, 2] = "";
        _text[492, 3] = "";
        _text[492, 4] = "";
        _text[492, 5] = "";
        _text[492, 6] = "";
        _text[492, 7] = "";
        _text[492, 8] = "";
        _text[492, 9] = "";

        _text[493, 0] = "Accept the gift";
        _text[493, 1] = "Принять дар"; //выбор 1
        _text[493, 2] = "";
        _text[493, 3] = "";
        _text[493, 4] = "";
        _text[493, 5] = "";
        _text[493, 6] = "";
        _text[493, 7] = "";
        _text[493, 8] = "";
        _text[493, 9] = "";

        _text[494, 0] = "The organism grows in the cargo bay.\n\nSuccess: it synchronizes with the ship's systems, causing strange images.";
        _text[494, 1] = "Организм прорастает в грузовом отсеке.\n\nУспех: он синхронизируется с системами корабля, вызывая странные образы."; // + фрагменты
        _text[494, 2] = "";
        _text[494, 3] = "";
        _text[494, 4] = "";
        _text[494, 5] = "";
        _text[494, 6] = "";
        _text[494, 7] = "";
        _text[494, 8] = "";
        _text[494, 9] = "";

        _text[495, 0] = "The organism grows in the cargo bay.\n\nFailure: the Corruption gets out of control. The virus penetrates the control network, causing one of the cores to fail fatally.";
        _text[495, 1] = "Организм прорастает в грузовом отсеке.\n\nПровал: скверна выходит из-под контроля. Вирус проникает в управляющую сеть, приводя к фатальному сбою одного из ядер."; // -1 ядро
        _text[495, 2] = "";
        _text[495, 3] = "";
        _text[495, 4] = "";
        _text[495, 5] = "";
        _text[495, 6] = "";
        _text[495, 7] = "";
        _text[495, 8] = "";
        _text[495, 9] = "";

        _text[496, 0] = "Refuse and move away";
        _text[496, 1] = "Отказаться и отойти"; //выбор 2
        _text[496, 2] = "";
        _text[496, 3] = "";
        _text[496, 4] = "";
        _text[496, 5] = "";
        _text[496, 6] = "";
        _text[496, 7] = "";
        _text[496, 8] = "";
        _text[496, 9] = "";

        _text[497, 0] = "You slowly move away from the station, but you feel that it is too late - the spores have penetrated the ship's ventilation.";
        _text[497, 1] = "Вы медленно отдаляетесь от станции, но чувствуете, что уже слишком поздно — споры внедрились в вентиляцию корабля.";
        _text[497, 2] = "";
        _text[497, 3] = "";
        _text[497, 4] = "";
        _text[497, 5] = "";
        _text[497, 6] = "";
        _text[497, 7] = "";
        _text[497, 8] = "";
        _text[497, 9] = "";

        _text[498, 0] = "Success: you initiate internal cleansing protocols - the ship is successfully cleaned.";
        _text[498, 1] = "Успех: вы запускаете протоколы внутренней очистки — корабль успешно очищен."; // ничего
        _text[498, 2] = "";
        _text[498, 3] = "";
        _text[498, 4] = "";
        _text[498, 5] = "";
        _text[498, 6] = "";
        _text[498, 7] = "";
        _text[498, 8] = "";
        _text[498, 9] = "";

        _text[499, 0] = "Failure: a spore enters the life support module, causing a malfunction.";
        _text[499, 1] = "Провал: спора проникает в модуль жизнеобеспечения, вызывая сбой"; // - ядро
        _text[499, 2] = "";
        _text[499, 3] = "";
        _text[499, 4] = "";
        _text[499, 5] = "";
        _text[499, 6] = "";
        _text[499, 7] = "";
        _text[499, 8] = "";
        _text[499, 9] = "";

        _text[500, 0] = "Perform external cleaning";
        _text[500, 1] = "Провести внешнюю очистку"; //выбор 3
        _text[500, 2] = "";
        _text[500, 3] = "";
        _text[500, 4] = "";
        _text[500, 5] = "";
        _text[500, 6] = "";
        _text[500, 7] = "";
        _text[500, 8] = "";
        _text[500, 9] = "";

        _text[501, 0] = "You initiate an external cleansing of the infected ship: you direct a concentrated laser at the biomass foci and block the infection signals.";
        _text[501, 1] = "Вы запускаете внешнюю очистку заражённого корабля: направляете концентрированный лазер на очаги биомассы и блокируете сигналы заражения.";
        _text[501, 2] = "";
        _text[501, 3] = "";
        _text[501, 4] = "";
        _text[501, 5] = "";
        _text[501, 6] = "";
        _text[501, 7] = "";
        _text[501, 8] = "";
        _text[501, 9] = "";

        _text[502, 0] = "Success: the cleansing is successful - the organism is destroyed, you take resources from the station.";
        _text[502, 1] = "Успех: очистка проходит успешно — организм уничтожен, вы забираете ресурсы со станции."; // + квант
        _text[502, 2] = "";
        _text[502, 3] = "";
        _text[502, 4] = "";
        _text[502, 5] = "";
        _text[502, 6] = "";
        _text[502, 7] = "";
        _text[502, 8] = "";
        _text[502, 9] = "";

        _text[503, 0] = "Failure: the infection goes deeper - the system overheats and one of the neurosections fails.";
        _text[503, 1] = "Провал: заражение оказывается глубже — система перегревается, и одна из нейросекций выходит из строя."; // - ядро
        _text[503, 2] = "";
        _text[503, 3] = "";
        _text[503, 4] = "";
        _text[503, 5] = "";
        _text[503, 6] = "";
        _text[503, 7] = "";
        _text[503, 8] = "";
        _text[503, 9] = "";

        // ResourceTraderNode
        _text[504, 0] = "You approach a rusty station littered with containers and garbage. A faint, crackling signal comes over the airwaves:\n\n\"Who's there? Don't shoot. I'm just trading. I've got something the rest of us don't have - if you're willing to pay, of course.\"";
        _text[504, 1] = "Вы приближаетесь к ржавой станции, заваленной контейнерами и мусором. В эфире появляется слабый, потрескивающий сигнал:\n\n\"Эй, кто там? Не стреляй. Я просто торгую. У меня есть то, чего нет у остальных — если ты, конечно, готов заплатить.\"";
        _text[504, 2] = "";
        _text[504, 3] = "";
        _text[504, 4] = "";
        _text[504, 5] = "";
        _text[504, 6] = "";
        _text[504, 7] = "";
        _text[504, 8] = "";
        _text[504, 9] = "";

        _text[505, 0] = "Trade";
        _text[505, 1] = "Торговать";
        _text[505, 2] = "";
        _text[505, 3] = "";
        _text[505, 4] = "";
        _text[505, 5] = "";
        _text[505, 6] = "";
        _text[505, 7] = "";
        _text[505, 8] = "";
        _text[505, 9] = "";

        _text[506, 0] = "Ignore";
        _text[506, 1] = "Игнорировать";
        _text[506, 2] = "";
        _text[506, 3] = "";
        _text[506, 4] = "";
        _text[506, 5] = "";
        _text[506, 6] = "";
        _text[506, 7] = "";
        _text[506, 8] = "";
        _text[506, 9] = "";

        // 0_ResourceDialogue
        _text[507, 0] = "You continue to orbit the abandoned communications satellite when a dull thud is heard. One of the external sensors is damaged. Upon inspection, a stuck cargo container is discovered. The markings on the casing are erased, the symbol is illegible.\n\nInside lies a sealed case surrounded by wires, a biometric lock and an emitter.\n\nJudging by the logs, the cargo has been drifting in orbit for over 200 years.";
        _text[507, 1] = "Вы продолжаете движение по орбите заброшенного спутника связи, когда раздаётся глухой удар. Один из внешних сенсоров — повреждён. При проверке обнаружен застрявший грузовой контейнер. Метки на корпусе стерлись, символ не разобрать.\n\nВнутри лежит запечатанный кейс, окруженный проводами, биометрическим замком и эмиттером\n\nСудя по логам груз дрейфует по орбите более 200 лет.";
        _text[507, 2] = "";
        _text[507, 3] = "";
        _text[507, 4] = "";
        _text[507, 5] = "";
        _text[507, 6] = "";
        _text[507, 7] = "";
        _text[507, 8] = "";
        _text[507, 9] = "";

        _text[508, 0] = "Open";
        _text[508, 1] = "Открыть"; //выбор 1
        _text[508, 2] = "";
        _text[508, 3] = "";
        _text[508, 4] = "";
        _text[508, 5] = "";
        _text[508, 6] = "";
        _text[508, 7] = "";
        _text[508, 8] = "";
        _text[508, 9] = "";

        _text[509, 0] = "You carefully open the container. Inside is a supply of old building materials.\n\nWhile some of the cargo is damaged by time, much is still usable. You load the materials into the storage facility.";
        _text[509, 1] = "Вы аккуратно вскрываете контейнер. Внутри — запас старых строительных материалов.\n\nХотя часть груза повреждена временем, многое всё ещё пригодно для использования. Вы загружаете материалы в хранилище."; // + случайный ресурс
        _text[509, 2] = "";
        _text[509, 3] = "";
        _text[509, 4] = "";
        _text[509, 5] = "";
        _text[509, 6] = "";
        _text[509, 7] = "";
        _text[509, 8] = "";
        _text[509, 9] = "";

        _text[510, 0] = "Ignore";
        _text[510, 1] = "Игнорировать"; //выбор 2
        _text[510, 2] = "";
        _text[510, 3] = "";
        _text[510, 4] = "";
        _text[510, 5] = "";
        _text[510, 6] = "";
        _text[510, 7] = "";
        _text[510, 8] = "";
        _text[510, 9] = "";

        _text[511, 0] = "You decide not to take any chances: the unknown container may be unstable or contaminated. You undock it and drop it back into the void.\n\nThe container slowly disappears from view. Perhaps someone else will stumble upon it someday.";
        _text[511, 1] = "Вы решаете не рисковать: неизвестный контейнер может быть нестабилен или заражён. Его отстыковывают и сбрасывают обратно в пустоту.\n\nКонтейнер медленно исчезает из зоны видимости. Возможно, кто-то другой когда-нибудь наткнётся на него."; // ничего
        _text[511, 2] = "";
        _text[511, 3] = "";
        _text[511, 4] = "";
        _text[511, 5] = "";
        _text[511, 6] = "";
        _text[511, 7] = "";
        _text[511, 8] = "";
        _text[511, 9] = "";

        // 0_MaterialDialogue       
        _text[512, 0] = "While scanning the surface of an old orbital ring, you spot the remains of an automated workshop. It is offline, but its frame is intact and its systems are in suspended animation.\n\nWhen you dock, you find a half-destroyed production bay inside. The robots are motionless, the air is thick with dust and a metallic taste, but a green light is blinking in one of the sealed bays: the manufacturing cycle is complete.";
        _text[512, 1] = "При сканировании поверхности старого орбитального кольца вы замечаете остатки автоматической мастерской. Она отключена, но её каркас цел, а системы — в анабиозе.\n\nПристыковавшись, вы находите внутри полуразрушенный производственный отсек. Роботы не двигаются, воздух насыщен пылью и металлическим привкусом, но в одном из запечатанных отсеков мигает зелёный индикатор: завершён цикл изготовления.";
        _text[512, 2] = "";
        _text[512, 3] = "";
        _text[512, 4] = "";
        _text[512, 5] = "";
        _text[512, 6] = "";
        _text[512, 7] = "";
        _text[512, 8] = "";
        _text[512, 9] = "";

        _text[513, 0] = "Open the vault";
        _text[513, 1] = "Вскрыть хранилище"; //выбор 1
        _text[513, 2] = "";
        _text[513, 3] = "";
        _text[513, 4] = "";
        _text[513, 5] = "";
        _text[513, 6] = "";
        _text[513, 7] = "";
        _text[513, 8] = "";
        _text[513, 9] = "";

        _text[514, 0] = "You manually open the compartment and extract the result of the old automated process.\n\nOn the platform lies a box of processed materials: polished alloys, stabilized ceramics, and packages of synthetic fabric.\n\nEverything is neatly labeled, as if it had been waiting for its owner.";
        _text[514, 1] = "Вы вручную открываете отсек и извлекаете результат старого автоматического процесса.\n\nНа платформе лежит ящик с обработанными материалами: отшлифованные сплавы, стабилизированная керамика и упаковки с синтетической тканью.\n\nВсё аккуратно промаркировано, как будто ждало хозяина."; // + случайный материал
        _text[514, 2] = "";
        _text[514, 3] = "";
        _text[514, 4] = "";
        _text[514, 5] = "";
        _text[514, 6] = "";
        _text[514, 7] = "";
        _text[514, 8] = "";
        _text[514, 9] = "";

        _text[515, 0] = "Do not interfere";
        _text[515, 1] = "Не вмешиваться"; //выбор 2
        _text[515, 2] = "";
        _text[515, 3] = "";
        _text[515, 4] = "";
        _text[515, 5] = "";
        _text[515, 6] = "";
        _text[515, 7] = "";
        _text[515, 8] = "";
        _text[515, 9] = "";

        _text[516, 0] = "You decide not to interfere: the station is unstable, and any interference could cause the structure to collapse.\n\nLeaving the object alone, you retreat to a safe distance.";
        _text[516, 1] = "Вы решаете не вмешиваться: станция нестабильна, а любое вмешательство может привести к обрушению конструкции.\n\nОставив объект в покое, вы отходите на безопасное расстояние.";
        _text[516, 2] = "";
        _text[516, 3] = "";
        _text[516, 4] = "";
        _text[516, 5] = "";
        _text[516, 6] = "";
        _text[516, 7] = "";
        _text[516, 8] = "";
        _text[516, 9] = "";

        // WeaponTraderNode
        _text[517, 0] = "You approach a heavily armed ship, bristling with turrets, cannons, and missile launchers.\n\nThe metal of the hull is blackened from old battles, but the weapons are fully operational.\n\nA distorted voice breaks into the airwaves:\n\n\"Hey...can you hear me? I repair and upgrade weapons...for a small fee, that is.\n\nIf you want, I can turn your gun into a work of art...or at least into something that shoots a little better than it does now.\"";
        _text[517, 1] = "Вы приближаетесь к тяжело вооружённому кораблю, утыканному турелями, пушками и ракетными установками.\n\nМеталл корпуса почернел от старых боёв, но оружие в полной боевой готовности.\n\nВ эфир прорывается искажённый помехами голос:\n\n\"Эй… слышите меня? Я чиню и улучшаю оружие… ну, за скромную плату.\n\nЕсли хотите, могу превратить вашу пушку в произведение искусства… или хотя бы в то, что стреляет чуть лучше, чем сейчас.\"";
        _text[517, 2] = "";
        _text[517, 3] = "";
        _text[517, 4] = "";
        _text[517, 5] = "";
        _text[517, 6] = "";
        _text[517, 7] = "";
        _text[517, 8] = "";
        _text[517, 9] = "";

        _text[518, 0] = "Trade";
        _text[518, 1] = "Торговать";
        _text[518, 2] = "";
        _text[518, 3] = "";
        _text[518, 4] = "";
        _text[518, 5] = "";
        _text[518, 6] = "";
        _text[518, 7] = "";
        _text[518, 8] = "";
        _text[518, 9] = "";

        _text[519, 0] = "Ignore";
        _text[519, 1] = "Игнорировать";
        _text[519, 2] = "";
        _text[519, 3] = "";
        _text[519, 4] = "";
        _text[519, 5] = "";
        _text[519, 6] = "";
        _text[519, 7] = "";
        _text[519, 8] = "";
        _text[519, 9] = "";

        // 1_ResourceDialogue
        _text[520, 0] = "You intercept a colonial logistics container.\n\nScans show organic structural panels.";
        _text[520, 1] = "Вы перехватываете контейнер колониальной логистики.\n\nСканы показывают органические конструкционные панели.";
        _text[520, 2] = "";
        _text[520, 3] = "";
        _text[520, 4] = "";
        _text[520, 5] = "";
        _text[520, 6] = "";
        _text[520, 7] = "";
        _text[520, 8] = "";
        _text[520, 9] = "";

        _text[521, 0] = "Open and remove";
        _text[521, 1] = "Вскрыть и изъять"; // выбор 1
        _text[521, 2] = "";
        _text[521, 3] = "";
        _text[521, 4] = "";
        _text[521, 5] = "";
        _text[521, 6] = "";
        _text[521, 7] = "";
        _text[521, 8] = "";
        _text[521, 9] = "";

        _text[522, 0] = "You break the seal and sort the cargo.\n\nSuccess: whole wooden modules sent to storage.";
        _text[522, 1] = "Вы вскрываете пломбу и сортируете груз.\n\nУспех: целые деревянные модули отправлены в хранилище."; // + дерево
        _text[522, 2] = "";
        _text[522, 3] = "";
        _text[522, 4] = "";
        _text[522, 5] = "";
        _text[522, 6] = "";
        _text[522, 7] = "";
        _text[522, 8] = "";
        _text[522, 9] = "";

        _text[523, 0] = "Failure: the sterilizing foam is activated - the compartment is contaminated, some of the wood materials have to be discarded.";
        _text[523, 1] = "Провал: срабатывает стерилизующая пена - отсек загрязнён, часть древесных материалов приходится сбросить."; // - дерево
        _text[523, 2] = "";
        _text[523, 3] = "";
        _text[523, 4] = "";
        _text[523, 5] = "";
        _text[523, 6] = "";
        _text[523, 7] = "";
        _text[523, 8] = "";
        _text[523, 9] = "";

        _text[524, 0] = "Push away and leave";
        _text[524, 1] = "Оттолкнуть и уйти"; // выбор 2
        _text[524, 2] = "";
        _text[524, 3] = "";
        _text[524, 4] = "";
        _text[524, 5] = "";
        _text[524, 6] = "";
        _text[524, 7] = "";
        _text[524, 8] = "";
        _text[524, 9] = "";

        _text[525, 0] = "You keep your distance and push the container away with a light impulse from the maneuvering engines.";
        _text[525, 1] = "Вы держите дистанцию и легким импульсом маневровых двигателей отталкиваете контейнер.";
        _text[525, 2] = "";
        _text[525, 3] = "";
        _text[525, 4] = "";
        _text[525, 5] = "";
        _text[525, 6] = "";
        _text[525, 7] = "";
        _text[525, 8] = "";
        _text[525, 9] = "";

        _text[526, 0] = "Success: the object goes off course. Nothing happens.";
        _text[526, 1] = "Успех: объект уходит с курса. Ничего не происходит."; // ничего
        _text[526, 2] = "";
        _text[526, 3] = "";
        _text[526, 4] = "";
        _text[526, 5] = "";
        _text[526, 6] = "";
        _text[526, 7] = "";
        _text[526, 8] = "";
        _text[526, 9] = "";

        _text[527, 0] = "Failure: a broken fragment scratches the paneling - spare wooden panels are used for emergency repairs.";
        _text[527, 1] = "Провал: отломившийся фрагмент царапает обшивку - на аварийный ремонт уходят запасные деревянные панели."; // - дерево
        _text[527, 2] = "";
        _text[527, 3] = "";
        _text[527, 4] = "";
        _text[527, 5] = "";
        _text[527, 6] = "";
        _text[527, 7] = "";
        _text[527, 8] = "";
        _text[527, 9] = "";

        // 2_ResourceDialogue
        _text[528, 0] = "Scanners reveal a \"cargo graveyard\": several lost capsules, coiled into a thin cloud of debris.";
        _text[528, 1] = "Сканеры отмечают \"кладбище грузов\": несколько потерянных капсул, смотанных в тонкое облако обломков.";
        _text[528, 2] = "";
        _text[528, 3] = "";
        _text[528, 4] = "";
        _text[528, 5] = "";
        _text[528, 6] = "";
        _text[528, 7] = "";
        _text[528, 8] = "";
        _text[528, 9] = "";

        _text[529, 0] = "Search the capsules";
        _text[529, 1] = "Обыскать капсулы"; // выбор 1
        _text[529, 2] = "";
        _text[529, 3] = "";
        _text[529, 4] = "";
        _text[529, 5] = "";
        _text[529, 6] = "";
        _text[529, 7] = "";
        _text[529, 8] = "";
        _text[529, 9] = "";

        _text[530, 0] = "You maneuver among the debris and open the least damaged capsules.";
        _text[530, 1] = "Вы лавируете среди обломков и вскрываете наименее повреждённые капсулы."; // + случайный ресурс
        _text[530, 2] = "";
        _text[530, 3] = "";
        _text[530, 4] = "";
        _text[530, 5] = "";
        _text[530, 6] = "";
        _text[530, 7] = "";
        _text[530, 8] = "";
        _text[530, 9] = "";

        _text[531, 0] = "Success: you extract the payload and distribute it among the compartments.";
        _text[531, 1] = "Успех: извлекаете полезный груз и распределяете по отсекам."; // + случайный ресурс
        _text[531, 2] = "";
        _text[531, 3] = "";
        _text[531, 4] = "";
        _text[531, 5] = "";
        _text[531, 6] = "";
        _text[531, 7] = "";
        _text[531, 8] = "";
        _text[531, 9] = "";

        _text[532, 0] = "Failure: a trap or depressurization forces an emergency reset - you lose some resources.";
        _text[532, 1] = "Провал: ловушка или разгерметизация вынуждает к аварийному сбросу - вы теряете часть ресурсов."; // - случайный ресурс
        _text[532, 2] = "";
        _text[532, 3] = "";
        _text[532, 4] = "";
        _text[532, 5] = "";
        _text[532, 6] = "";
        _text[532, 7] = "";
        _text[532, 8] = "";
        _text[532, 9] = "";

        _text[533, 0] = "Leave the cargo graveyard";
        _text[533, 1] = "Оставить кладбище грузов"; // выбор 2
        _text[533, 2] = "";
        _text[533, 3] = "";
        _text[533, 4] = "";
        _text[533, 5] = "";
        _text[533, 6] = "";
        _text[533, 7] = "";
        _text[533, 8] = "";
        _text[533, 9] = "";

        _text[534, 0] = "You reduce thrust and maintain course as you pass the debris field.";
        _text[534, 1] = "Вы снижаете тягу и сохраняете курс, проходя поле обломков.";
        _text[534, 2] = "";
        _text[534, 3] = "";
        _text[534, 4] = "";
        _text[534, 5] = "";
        _text[534, 6] = "";
        _text[534, 7] = "";
        _text[534, 8] = "";
        _text[534, 9] = "";

        _text[535, 0] = "Success: you walk around the field without incident.";
        _text[535, 1] = "Успех: вы обходите поле без происшествий."; // ничего
        _text[535, 2] = "";
        _text[535, 3] = "";
        _text[535, 4] = "";
        _text[535, 5] = "";
        _text[535, 6] = "";
        _text[535, 7] = "";
        _text[535, 8] = "";
        _text[535, 9] = "";

        _text[536, 0] = "Failure: drifting pod hits shields - you spend resources on repairs.";
        _text[536, 1] = "Провал: дрейфующая капсула задевает щиты - вы тратите запасы ресурсов на починку."; // - случайный ресурс
        _text[536, 2] = "";
        _text[536, 3] = "";
        _text[536, 4] = "";
        _text[536, 5] = "";
        _text[536, 6] = "";
        _text[536, 7] = "";
        _text[536, 8] = "";
        _text[536, 9] = "";

        // 3_ResourceDialogue
        _text[537, 0] = "A damaged tugboat drifts ahead, the mining capsule still attached to its winch.\n\nTelemetry shows high iron content.";
        _text[537, 1] = "Впереди дрейфует повреждённый буксир, к его лебёдке всё ещё прицеплена горная капсула.\n\nТелеметрия показывает высокое содержание железа.";
        _text[537, 2] = "";
        _text[537, 3] = "";
        _text[537, 4] = "";
        _text[537, 5] = "";
        _text[537, 6] = "";
        _text[537, 7] = "";
        _text[537, 8] = "";
        _text[537, 9] = "";

        _text[538, 0] = "Quickly tear off the capsule";
        _text[538, 1] = "Быстро сорвать капсулу"; // выбор 1
        _text[538, 2] = "";
        _text[538, 3] = "";
        _text[538, 4] = "";
        _text[538, 5] = "";
        _text[538, 6] = "";
        _text[538, 7] = "";
        _text[538, 8] = "";
        _text[538, 9] = "";

        _text[539, 0] = "Success: you break the capsule and unload the iron ore into the receiver.";
        _text[539, 1] = "Успех: вы срываете капсулу и выгружаете железную руду в приёмник."; // + железная руда
        _text[539, 2] = "";
        _text[539, 3] = "";
        _text[539, 4] = "";
        _text[539, 5] = "";
        _text[539, 6] = "";
        _text[539, 7] = "";
        _text[539, 8] = "";
        _text[539, 9] = "";

        _text[540, 0] = "Failure: capsule goes into rotation and disintegrates among the debris. Nothing is received.";
        _text[540, 1] = "Провал: капсула уходит в вращение и распадается среди обломков. Ничего не получено."; // ничего
        _text[540, 2] = "";
        _text[540, 3] = "";
        _text[540, 4] = "";
        _text[540, 5] = "";
        _text[540, 6] = "";
        _text[540, 7] = "";
        _text[540, 8] = "";
        _text[540, 9] = "";

        _text[541, 0] = "Carefully pick it up with a manipulator";
        _text[541, 1] = "Аккуратно забрать манипулятором"; // выбор 2
        _text[541, 2] = "";
        _text[541, 3] = "";
        _text[541, 4] = "";
        _text[541, 5] = "";
        _text[541, 6] = "";
        _text[541, 7] = "";
        _text[541, 8] = "";
        _text[541, 9] = "";

        _text[542, 0] = "You lock the capsule and begin unloading. The tug restarts - the autoturret wakes up and manages to fire.\n\nYou have the ore, but one of the AI ​​cores burns out.";
        _text[542, 1] = "Вы фиксируете капсулу и начинаете выгрузку. Буксир перезапускается — автотурель просыпается и успевает выстрелить.\n\nРуда у вас, но одно ядро ИИ перегорает."; // + железная руда - ядро
        _text[542, 2] = "";
        _text[542, 3] = "";
        _text[542, 4] = "";
        _text[542, 5] = "";
        _text[542, 6] = "";
        _text[542, 7] = "";
        _text[542, 8] = "";
        _text[542, 9] = "";

        // 4_ResourceDialogue
        _text[543, 0] = "Reconnaissance notes a planet with a high rock content.\n\nScans show voids and layered strata with unstable areas.\n\nYou land on a rock plateau and set up a temporary quarry.";
        _text[543, 1] = "Разведка отмечает планету с высоким содержанием горных пород.\n\nСканы показывают пустоты и слоистые пласты с нестабильными участками.\n\nВы садитесь на каменное плато и разворачиваете временный карьер.";
        _text[543, 2] = "";
        _text[543, 3] = "";
        _text[543, 4] = "";
        _text[543, 5] = "";
        _text[543, 6] = "";
        _text[543, 7] = "";
        _text[543, 8] = "";
        _text[543, 9] = "";

        _text[544, 0] = "Send robots with cutters";
        _text[544, 1] = "Отправить роботов с резаками"; // выбор 1
        _text[544, 2] = "";
        _text[544, 3] = "";
        _text[544, 4] = "";
        _text[544, 5] = "";
        _text[544, 6] = "";
        _text[544, 7] = "";
        _text[544, 8] = "";
        _text[544, 9] = "";

        _text[545, 0] = "Robots make shallow cuts, separate the blocks, forklifts carry the containers to the shuttle.\n\nYou extract a modest batch of stone and leave before the shifts begin.";
        _text[545, 1] = "Роботы делают неглубокие пропилы, отделяют блоки, погрузчики уносят контейнеры к шаттлу.\n\nВы добываете скромную партию камня и уходите до начала смещений."; // +камень
        _text[545, 2] = "";
        _text[545, 3] = "";
        _text[545, 4] = "";
        _text[545, 5] = "";
        _text[545, 6] = "";
        _text[545, 7] = "";
        _text[545, 8] = "";
        _text[545, 9] = "";

        _text[546, 0] = "Start the drill";
        _text[546, 1] = "Запустить бур"; // выбор 2
        _text[546, 2] = "";
        _text[546, 3] = "";
        _text[546, 4] = "";
        _text[546, 5] = "";
        _text[546, 6] = "";
        _text[546, 7] = "";
        _text[546, 8] = "";
        _text[546, 9] = "";

        _text[547, 0] = "Success: resonant cracks open a rich vein. You haul away a large shipment of stone.";
        _text[547, 1] = "Успех: резонансные трещины открывают богатую жилу. Вы вывозите крупную партию камня."; // +камень
        _text[547, 2] = "";
        _text[547, 3] = "";
        _text[547, 4] = "";
        _text[547, 5] = "";
        _text[547, 6] = "";
        _text[547, 7] = "";
        _text[547, 8] = "";
        _text[547, 9] = "";

        _text[548, 0] = "Failure: the edge of the quarry caves in. The safety rope breaks, the drilling frame is pulled into the hole, the loaders drop pallets to avoid falling.\n\nThe spoils are lost.";
        _text[548, 1] = "Провал: край карьера проседает. Рвётся страховочный трос, буровую раму тянет в провал, погрузчики сбрасывают паллеты, чтобы не сорваться.\n\nДобыча утрачена."; // ничего
        _text[548, 2] = "";
        _text[548, 3] = "";
        _text[548, 4] = "";
        _text[548, 5] = "";
        _text[548, 6] = "";
        _text[548, 7] = "";
        _text[548, 8] = "";
        _text[548, 9] = "";

        _text[549, 0] = "Launch a reconnaissance drone";
        _text[549, 1] = "Запустить разведовательный дрон"; // выбор 3
        _text[549, 2] = "";
        _text[549, 3] = "";
        _text[549, 4] = "";
        _text[549, 5] = "";
        _text[549, 6] = "";
        _text[549, 7] = "";
        _text[549, 8] = "";
        _text[549, 9] = "";

        _text[550, 0] = "Success: the drone finds a stable cavity under the crust.\n\nYou mine a stable medium batch.";
        _text[550, 1] = "Успех: дрон находит стабильную полость под коркой.\n\nВы добываете стабильную среднюю партию."; // +камень
        _text[550, 2] = "";
        _text[550, 3] = "";
        _text[550, 4] = "";
        _text[550, 5] = "";
        _text[550, 6] = "";
        _text[550, 7] = "";
        _text[550, 8] = "";
        _text[550, 9] = "";

        _text[551, 0] = "Failure: dust emission jams turbines - drone lost.\n\nAll loot lost.";
        _text[551, 1] = "Провал: пылевой выброс клинит турбины - дрон потерян.\n\nВся добыча потеряна."; // -ядро
        _text[551, 2] = "";
        _text[551, 3] = "";
        _text[551, 4] = "";
        _text[551, 5] = "";
        _text[551, 6] = "";
        _text[551, 7] = "";
        _text[551, 8] = "";
        _text[551, 9] = "";

        // 5_ResourceDialogue
        _text[552, 0] = "Your sensors catch a shimmer in the dust: a broken solar array field tumbling in slow orbit.\n\nPanels are still charged. The cabling looks brittle, but intact in places.";
        _text[552, 1] = "Сенсоры улавливают мерцание в пыли: поле сломанных солнечных панелей медленно вращается на орбите.\n\nПанели всё ещё заряжены. Кабели хрупкие, но местами целы.";
        _text[552, 2] = "";
        _text[552, 3] = "";
        _text[552, 4] = "";
        _text[552, 5] = "";
        _text[552, 6] = "";
        _text[552, 7] = "";
        _text[552, 8] = "";
        _text[552, 9] = "";

        _text[553, 0] = "Cut power lines and harvest cells";
        _text[553, 1] = "Перерезать линии питания и снять ячейки"; // выбор 1
        _text[553, 2] = "";
        _text[553, 3] = "";
        _text[553, 4] = "";
        _text[553, 5] = "";
        _text[553, 6] = "";
        _text[553, 7] = "";
        _text[553, 8] = "";
        _text[553, 9] = "";

        _text[554, 0] = "Success: your drones isolate the charge and pull out usable cells.\n\nYou store the power modules for later conversion.";
        _text[554, 1] = "Успех: дроны изолируют заряд и извлекают пригодные ячейки.\n\nВы отправляете силовые модули в хранилище."; // +электричество
        _text[554, 2] = "";
        _text[554, 3] = "";
        _text[554, 4] = "";
        _text[554, 5] = "";
        _text[554, 6] = "";
        _text[554, 7] = "";
        _text[554, 8] = "";
        _text[554, 9] = "";

        _text[555, 0] = "Failure: a trapped capacitor discharges.\n\nA burst arcs across the harness — one of the core circuits overheats.";
        _text[555, 1] = "Провал: скрытый конденсатор разряжается.\n\nДуга пробивает жгут — одна из цепей ядра перегревается."; // -ядро
        _text[555, 2] = "";
        _text[555, 3] = "";
        _text[555, 4] = "";
        _text[555, 5] = "";
        _text[555, 6] = "";
        _text[555, 7] = "";
        _text[555, 8] = "";
        _text[555, 9] = "";

        _text[556, 0] = "Tow the whole frame to the ship";
        _text[556, 1] = "Притащить каркас целиком к кораблю"; // выбор 2
        _text[556, 2] = "";
        _text[556, 3] = "";
        _text[556, 4] = "";
        _text[556, 5] = "";
        _text[556, 6] = "";
        _text[556, 7] = "";
        _text[556, 8] = "";
        _text[556, 9] = "";

        _text[557, 0] = "The array is heavier than telemetry suggested.\n\nSuccess: you secure the frame and strip it safely — plenty of usable metal.";
        _text[557, 1] = "Поле панелей тяжелее, чем показывала телеметрия.\n\nУспех: вы фиксируете каркас и разбираете его без риска — много пригодного металла."; // +железные слитки
        _text[557, 2] = "";
        _text[557, 3] = "";
        _text[557, 4] = "";
        _text[557, 5] = "";
        _text[557, 6] = "";
        _text[557, 7] = "";
        _text[557, 8] = "";
        _text[557, 9] = "";

        _text[558, 0] = "Failure: the frame twists under thrust.\n\nA shard scrapes the hull — emergency patches consume spare materials.";
        _text[558, 1] = "Провал: каркас выкручивает под тягой.\n\nОсколок царапает обшивку — аварийные заплаты съедают запас материалов."; // - случайный ресурс
        _text[558, 2] = "";
        _text[558, 3] = "";
        _text[558, 4] = "";
        _text[558, 5] = "";
        _text[558, 6] = "";
        _text[558, 7] = "";
        _text[558, 8] = "";
        _text[558, 9] = "";

        _text[559, 0] = "Leave it and move on";
        _text[559, 1] = "Оставить и продолжить путь"; // выбор 3 // ничего
        _text[559, 2] = "";
        _text[559, 3] = "";
        _text[559, 4] = "";
        _text[559, 5] = "";
        _text[559, 6] = "";
        _text[559, 7] = "";
        _text[559, 8] = "";
        _text[559, 9] = "";

        // 6_ResourceDialogue

        _text[560, 0] = "A small comet fragment drifts across your route.\n\nIts surface is cracked, venting thin plumes of ice dust.\n\nThe scanner confirms: a water-rich core.";
        _text[560, 1] = "Небольшой осколок кометы пересекает ваш маршрут.\n\nПоверхность треснута и выпускает тонкие струи ледяной пыли.\n\nСканер подтверждает: водонасыщенное ядро.";
        _text[560, 2] = "";
        _text[560, 3] = "";
        _text[560, 4] = "";
        _text[560, 5] = "";
        _text[560, 6] = "";
        _text[560, 7] = "";
        _text[560, 8] = "";
        _text[560, 9] = "";

        _text[561, 0] = "Feed the heaters and melt the core";
        _text[561, 1] = "Запитать нагреватели и расплавить ядро"; // выбор 1
        _text[561, 2] = "";
        _text[561, 3] = "";
        _text[561, 4] = "";
        _text[561, 5] = "";
        _text[561, 6] = "";
        _text[561, 7] = "";
        _text[561, 8] = "";
        _text[561, 9] = "";

        _text[562, 0] = "You route power into the heating contours.\n\nThe ice yields, and clean water is pumped into sealed tanks.\n\nThe power grid sags — systems run on reserve for a while.";
        _text[562, 1] = "Вы подаёте мощность в контуры нагрева.\n\nЛёд поддаётся, и чистая вода перекачивается в герметичные баки.\n\nСеть проседает — некоторое время системы работают на резерве."; // + Water, - Electricity
        _text[562, 2] = "";
        _text[562, 3] = "";
        _text[562, 4] = "";
        _text[562, 5] = "";
        _text[562, 6] = "";
        _text[562, 7] = "";
        _text[562, 8] = "";
        _text[562, 9] = "";

        _text[564, 0] = "Capture the vent and compress it into steam canisters";
        _text[564, 1] = "Перехватить выброс и сжать в паровые баллоны"; // выбор 2
        _text[564, 2] = "";
        _text[564, 3] = "";
        _text[564, 4] = "";
        _text[564, 5] = "";
        _text[564, 6] = "";
        _text[564, 7] = "";
        _text[564, 8] = "";
        _text[564, 9] = "";

        _text[565, 0] = "You deploy intake nets in the plume.\n\nCompressors seal the collected vapor into pressure canisters.\n\nStable. Clean. No risk to the hull.";
        _text[565, 1] = "Вы раскрываете сети забора на струе выброса.\n\nКомпрессоры запечатывают собранный пар в баллоны под давлением.\n\nСтабильно. Чисто. Без риска для корпуса."; // + Steam
        _text[565, 2] = "";
        _text[565, 3] = "";
        _text[565, 4] = "";
        _text[565, 5] = "";
        _text[565, 6] = "";
        _text[565, 7] = "";
        _text[565, 8] = "";
        _text[565, 9] = "";

        _text[566, 0] = "Crack the fragment with a kinetic shot";
        _text[566, 1] = "Расколоть осколок кинетическим выстрелом"; // выбор 3
        _text[566, 2] = "";
        _text[566, 3] = "";
        _text[566, 4] = "";
        _text[566, 5] = "";
        _text[566, 6] = "";
        _text[566, 7] = "";
        _text[566, 8] = "";
        _text[566, 9] = "";

        _text[567, 0] = "Success: the crust splits open.\n\nYou scoop up water — and notice a sealed data capsule lodged inside the ice.\n\nIts core still holds fragments of old navigation logs.";
        _text[567, 1] = "Успех: корка раскрывается.\n\nВы собираете воду — и замечаете во льду герметичную капсулу с данными.\n\nЕё ядро всё ещё хранит фрагменты старых навигационных логов."; // + Water, + Memory
        _text[567, 2] = "";
        _text[567, 3] = "";
        _text[567, 4] = "";
        _text[567, 5] = "";
        _text[567, 6] = "";
        _text[567, 7] = "";
        _text[567, 8] = "";
        _text[567, 9] = "";

        _text[568, 0] = "Failure: the shot turns the fragment into a chaotic hail.\n\nIce shards rake the shielding — you burn supplies on emergency repairs.";
        _text[568, 1] = "Провал: выстрел превращает осколок в хаотический град.\n\nЛедяные осколки бьют по щитам — на аварийный ремонт уходят запасы."; // - RandomResource
        _text[568, 2] = "";
        _text[568, 3] = "";
        _text[568, 4] = "";
        _text[568, 5] = "";
        _text[568, 6] = "";
        _text[568, 7] = "";
        _text[568, 8] = "";
        _text[568, 9] = "";

        // 7_ResourceDialogue
        _text[569, 0] = "Scans detect the ruins of an ancient glassworks facility on the planet's surface.\n\nThe production bay is half-buried in sand. The roof is cracked, the workshop is silent — but the silicate vats are intact.\n\nSand is everywhere. So are stacks of half-finished panes.";
        _text[569, 1] = "Сканы фиксируют руины древнего стекольного производства на поверхности планеты.\n\nПроизводственный отсек наполовину занесён песком. Крыша треснула, цех молчит — но силликатные ванны целы.\n\nПесок повсюду. И пачки полуготовых стеклянных панелей тоже.";
        _text[569, 2] = "";
        _text[569, 3] = "";
        _text[569, 4] = "";
        _text[569, 5] = "";
        _text[569, 6] = "";
        _text[569, 7] = "";
        _text[569, 8] = "";
        _text[569, 9] = "";

        _text[570, 0] = "Scoop sand into containers";
        _text[570, 1] = "Набрать песок в контейнеры"; // выбор 1
        _text[570, 2] = "";
        _text[570, 3] = "";
        _text[570, 4] = "";
        _text[570, 5] = "";
        _text[570, 6] = "";
        _text[570, 7] = "";
        _text[570, 8] = "";
        _text[570, 9] = "";

        _text[571, 0] = "You load dry silicate sand into sealed bins.\n\nNo alarms. No movement. Only windless dust and dead machinery.";
        _text[571, 1] = "Вы загружаете сухой кремнезёмный песок в герметичные контейнеры.\n\nНикаких тревог. Никакого движения. Только безветренная пыль и мёртвые механизмы."; // + Sand
        _text[571, 2] = "";
        _text[571, 3] = "";
        _text[571, 4] = "";
        _text[571, 5] = "";
        _text[571, 6] = "";
        _text[571, 7] = "";
        _text[571, 8] = "";
        _text[571, 9] = "";

        _text[572, 0] = "Restart the furnace cycle";
        _text[572, 1] = "Перезапустить цикл печи"; // выбор 2
        _text[572, 2] = "";
        _text[572, 3] = "";
        _text[572, 4] = "";
        _text[572, 5] = "";
        _text[572, 6] = "";
        _text[572, 7] = "";
        _text[572, 8] = "";
        _text[572, 9] = "";

        _text[573, 0] = "Success: the old heaters respond.\n\nThe temperature rises slowly. The line completes one last cycle — and a batch of tempered glass panes slides out of the bay.";
        _text[573, 1] = "Успех: древние нагреватели откликаются.\n\nТемпература медленно растёт. Линия завершает ещё один цикл — и из отсека выходит партия закалённых стеклянных панелей."; // + Glass
        _text[573, 2] = "";
        _text[573, 3] = "";
        _text[573, 4] = "";
        _text[573, 5] = "";
        _text[573, 6] = "";
        _text[573, 7] = "";
        _text[573, 8] = "";
        _text[573, 9] = "";

        _text[574, 0] = "Failure: a sealed pressure pocket bursts.\n\nA hot jet scorches the drones and floods the bay with abrasive dust.\n\nYou cut power and retreat — one of the ship's cores burns out under overload.";
        _text[574, 1] = "Провал: взрывается запечатанный карман давления.\n\nРаскалённая струя обжигает дронов и забивает отсек абразивной пылью.\n\nВы обрубаете питание и отходите — одно из ядер корабля перегорает от перегрузки."; // - AiCore
        _text[574, 2] = "";
        _text[574, 3] = "";
        _text[574, 4] = "";
        _text[574, 5] = "";
        _text[574, 6] = "";
        _text[574, 7] = "";
        _text[574, 8] = "";
        _text[574, 9] = "";

        _text[575, 0] = "Take only finished panes and leave";
        _text[575, 1] = "Забрать готовые панели и уйти"; // выбор 3
        _text[575, 2] = "";
        _text[575, 3] = "";
        _text[575, 4] = "";
        _text[575, 5] = "";
        _text[575, 6] = "";
        _text[575, 7] = "";
        _text[575, 8] = "";
        _text[575, 9] = "";

        _text[576, 0] = "You choose the safest cargo: sealed stacks with intact markings.\n\nA small, clean haul — no need to wake the dead factory.";
        _text[576, 1] = "Вы выбираете самое безопасное: запечатанные пачки с целыми маркировками.\n\nНебольшая, но чистая добыча — без попыток оживить мёртвое производство."; // + Glass
        _text[576, 2] = "";
        _text[576, 3] = "";
        _text[576, 4] = "";
        _text[576, 5] = "";
        _text[576, 6] = "";
        _text[576, 7] = "";
        _text[576, 8] = "";
        _text[576, 9] = "";

        // 8_ResourceComponentDialogue
        _text[577, 0] = "A dead relay station floats ahead, wrapped in a spiderweb of conduits.\n\nMost lines are cut, but the main trunk still holds copper — and signal-grade insulation.";
        _text[577, 1] = "Впереди дрейфует мёртвая ретрансляторная станция, опутанная паутиной магистралей.\n\nБольшинство линий перерезано, но основной ствол всё ещё держит медь — и изоляцию сигнального класса.";
        _text[577, 2] = "";
        _text[577, 3] = "";
        _text[577, 4] = "";
        _text[577, 5] = "";
        _text[577, 6] = "";
        _text[577, 7] = "";
        _text[577, 8] = "";
        _text[577, 9] = "";

        _text[578, 0] = "Strip the conduits for copper";
        _text[578, 1] = "Сорвать магистрали на медь"; // выбор 1
        _text[578, 2] = "";
        _text[578, 3] = "";
        _text[578, 4] = "";
        _text[578, 5] = "";
        _text[578, 6] = "";
        _text[578, 7] = "";
        _text[578, 8] = "";
        _text[578, 9] = "";

        _text[579, 0] = "Success: heavy copper bundles are cut free and secured.";
        _text[579, 1] = "Успех: тяжёлые медные жгуты срезаны и закреплены."; // + CopperOre
        _text[579, 2] = "";
        _text[579, 3] = "";
        _text[579, 4] = "";
        _text[579, 5] = "";
        _text[579, 6] = "";
        _text[579, 7] = "";
        _text[579, 8] = "";
        _text[579, 9] = "";

        _text[580, 0] = "Failure: an energized line lashes back.\n\nThe station wakes for a second — auto-lock clamps your drone. You cut it loose.";
        _text[580, 1] = "Провал: под напряжением линия бьёт обратно.\n\nСтанция на секунду оживает — автофиксатор зажимает дрона. Вы рубите его и уходите."; // - ядро
        _text[580, 2] = "";
        _text[580, 3] = "";
        _text[580, 4] = "";
        _text[580, 5] = "";
        _text[580, 6] = "";
        _text[580, 7] = "";
        _text[580, 8] = "";
        _text[580, 9] = "";

        _text[581, 0] = "Harvest insulation and coil it";
        _text[581, 1] = "Снять изоляцию и смотать"; // выбор 2
        _text[581, 2] = "";
        _text[581, 3] = "";
        _text[581, 4] = "";
        _text[581, 5] = "";
        _text[581, 6] = "";
        _text[581, 7] = "";
        _text[581, 8] = "";
        _text[581, 9] = "";

        _text[582, 0] = "You collect clean insulation and intact copper strands.\n\nPerfect for wiring and delicate assemblies.";
        _text[582, 1] = "Вы собираете чистую изоляцию и целые медные жилы.\n\nИдеально для проводки и точных сборок."; // + CopperWire
        _text[582, 2] = "";
        _text[582, 3] = "";
        _text[582, 4] = "";
        _text[582, 5] = "";
        _text[582, 6] = "";
        _text[582, 7] = "";
        _text[582, 8] = "";
        _text[582, 9] = "";

        _text[583, 0] = "Leave a beacon and mark the site";
        _text[583, 1] = "Оставить маяк и отметить место"; // выбор 3
        _text[583, 2] = "";
        _text[583, 3] = "";
        _text[583, 4] = "";
        _text[583, 5] = "";
        _text[583, 6] = "";
        _text[583, 7] = "";
        _text[583, 8] = "";
        _text[583, 9] = "";

        _text[584, 0] = "You log the coordinates.\n\nNo loot now — but the memory is stored.";
        _text[584, 1] = "Вы фиксируете координаты.\n\nДобычи сейчас нет — но память сохранена."; // + фрагменты данных
        _text[584, 2] = "";
        _text[584, 3] = "";
        _text[584, 4] = "";
        _text[584, 5] = "";
        _text[584, 6] = "";
        _text[584, 7] = "";
        _text[584, 8] = "";
        _text[584, 9] = "";

        // 9_ResourceDialogue
        _text[585, 0] = "A mining capsule spins near the asteroid belt edge.\n\nIts scanner tag reads: \"THERMAL FUEL\".\n\nInside — compacted coal bricks, vacuum-sealed.";
        _text[585, 1] = "Добывающая капсула вращается у края астероидного пояса.\n\nМетка сканера: «ТЕПЛОВОЕ ТОПЛИВО».\n\nВнутри — прессованные угольные брикеты, в вакуумной упаковке.";
        _text[585, 2] = "";
        _text[585, 3] = "";
        _text[585, 4] = "";
        _text[585, 5] = "";
        _text[585, 6] = "";
        _text[585, 7] = "";
        _text[585, 8] = "";
        _text[585, 9] = "";

        _text[586, 0] = "Match rotation and dock";
        _text[586, 1] = "Согласовать вращение и пристыковаться"; // выбор 1
        _text[586, 2] = "";
        _text[586, 3] = "";
        _text[586, 4] = "";
        _text[586, 5] = "";
        _text[586, 6] = "";
        _text[586, 7] = "";
        _text[586, 8] = "";
        _text[586, 9] = "";

        _text[587, 0] = "Success: you stabilize the spin and unload the coal.";
        _text[587, 1] = "Успех: вы стабилизируете вращение и выгружаете уголь."; // + Coal
        _text[587, 2] = "";
        _text[587, 3] = "";
        _text[587, 4] = "";
        _text[587, 5] = "";
        _text[587, 6] = "";
        _text[587, 7] = "";
        _text[587, 8] = "";
        _text[587, 9] = "";

        _text[588, 0] = "Failure: the lock misses by centimeters.\n\nThe capsule scrapes the hull — you lose some stored materials in emergency patching.";
        _text[588, 1] = "Провал: захват промахивается на сантиметры.\n\nКапсула царапает обшивку — на аварийный ремонт уходит часть запасов."; // - случайный ресурс
        _text[588, 2] = "";
        _text[588, 3] = "";
        _text[588, 4] = "";
        _text[588, 5] = "";
        _text[588, 6] = "";
        _text[588, 7] = "";
        _text[588, 8] = "";
        _text[588, 9] = "";

        _text[589, 0] = "Shoot the latch and pull with tractor";
        _text[589, 1] = "Сбить замок и вытянуть тягачом"; // выбор 2
        _text[589, 2] = "";
        _text[589, 3] = "";
        _text[589, 4] = "";
        _text[589, 5] = "";
        _text[589, 6] = "";
        _text[589, 7] = "";
        _text[589, 8] = "";
        _text[589, 9] = "";

        _text[590, 0] = "Success: the latch breaks cleanly.";
        _text[590, 1] = "Успех: замок срывается чисто."; // + Coal
        _text[590, 2] = "";
        _text[590, 3] = "";
        _text[590, 4] = "";
        _text[590, 5] = "";
        _text[590, 6] = "";
        _text[590, 7] = "";
        _text[590, 8] = "";
        _text[590, 9] = "";

        _text[591, 0] = "Failure: the shot punctures a fuel canister.\n\nCoal dust floods the bay — half the cargo is spoiled.";
        _text[591, 1] = "Провал: выстрел пробивает канистру.\n\nУгольная пыль заливает отсек — половина груза испорчена."; // ничего
        _text[591, 2] = "";
        _text[591, 3] = "";
        _text[591, 4] = "";
        _text[591, 5] = "";
        _text[591, 6] = "";
        _text[591, 7] = "";
        _text[591, 8] = "";
        _text[591, 9] = "";

        // 10_ResourceDialogue
        _text[592, 0] = "Scanners detect a wrecked fuel depot on the surface of a desert planet.\n\nHalf-buried tanks and pipelines stretch under the sand.\n\nOne reservoir still shows pressure — there is usable machine fuel inside.";
        _text[592, 1] = "Сканеры фиксируют разрушенный топливный склад на поверхности пустынной планеты.\n\nПолузасыпанные резервуары и трубопроводы тянутся под песком.\n\nОдин бак всё ещё держит давление — внутри есть пригодное топливо для машин.";
        _text[592, 2] = "";
        _text[592, 3] = "";
        _text[592, 4] = "";
        _text[592, 5] = "";
        _text[592, 6] = "";
        _text[592, 7] = "";
        _text[592, 8] = "";
        _text[592, 9] = "";

        _text[593, 0] = "Connect sealed pumps and siphon fuel";
        _text[593, 1] = "Подключить гермопомпы и откачать топливо"; // выбор 1 + Oil, - Electricity
        _text[593, 2] = "";
        _text[593, 3] = "";
        _text[593, 4] = "";
        _text[593, 5] = "";
        _text[593, 6] = "";
        _text[593, 7] = "";
        _text[593, 8] = "";
        _text[593, 9] = "";

        _text[594, 0] = "Cut into the pressurized pipe";
        _text[594, 1] = "Врезаться в трубу под давлением"; // выбор 2 Success + Oil, Failure - RandomResource
        _text[594, 2] = "";
        _text[594, 3] = "";
        _text[594, 4] = "";
        _text[594, 5] = "";
        _text[594, 6] = "";
        _text[594, 7] = "";
        _text[594, 8] = "";
        _text[594, 9] = "";

        _text[595, 0] = "Filter sludge from the bottom tanks";
        _text[595, 1] = "Отфильтровать шлам из нижних баков"; // выбор 3 + Oil, - Water
        _text[595, 2] = "";
        _text[595, 3] = "";
        _text[595, 4] = "";
        _text[595, 5] = "";
        _text[595, 6] = "";
        _text[595, 7] = "";
        _text[595, 8] = "";
        _text[595, 9] = "";

        _text[596, 0] = "You connect sealed hoses and start the pumps.\n\nFuel flows into protected containers.\n\nThe pumps draw a lot of power — the ship's energy reserve drops for a while.";
        _text[596, 1] = "Вы подключаете гермошланги и запускаете помпы.\n\nТопливо уходит в защищённые контейнеры.\n\nПомпы прожорливы — запас энергии корабля на время проседает."; // + Oil, - Electricity
        _text[596, 2] = "";
        _text[596, 3] = "";
        _text[596, 4] = "";
        _text[596, 5] = "";
        _text[596, 6] = "";
        _text[596, 7] = "";
        _text[596, 8] = "";
        _text[596, 9] = "";

        _text[597, 0] = "Success: the pipe holds.\n\nA clean fuel stream rushes into the collectors.\n\nYou shut the valve and pull back before the pressure jumps.";
        _text[597, 1] = "Успех: труба выдерживает.\n\nЧистая струя топлива уходит в сборники.\n\nВы перекрываете клапан и отходите до того, как давление подпрыгнет."; // + Oil
        _text[597, 2] = "";
        _text[597, 3] = "";
        _text[597, 4] = "";
        _text[597, 5] = "";
        _text[597, 6] = "";
        _text[597, 7] = "";
        _text[597, 8] = "";
        _text[597, 9] = "";

        _text[598, 0] = "Failure: the pipe ruptures.\n\nFuel mist floods the site. You vent the bay and spend supplies on emergency sealing.\n\nExtraction is aborted.";
        _text[598, 1] = "Провал: трубу разрывает.\n\nТопливный туман накрывает площадку. Вы продуваете отсек и тратите запасы на аварийную герметизацию.\n\nОткачка сорвана."; // - RandomResource
        _text[598, 2] = "";
        _text[598, 3] = "";
        _text[598, 4] = "";
        _text[598, 5] = "";
        _text[598, 6] = "";
        _text[598, 7] = "";
        _text[598, 8] = "";
        _text[598, 9] = "";

        _text[599, 0] = "You collect thick sludge from the bottom tanks and run it through filters.\n\nIt takes water to cool and wash the system.\n\nThe output is rough, but it burns.";
        _text[599, 1] = "Вы собираете густой шлам со дна баков и прогоняете его через фильтры.\n\nНужна вода, чтобы охлаждать и промывать систему.\n\nТопливо получается грубым, но оно горит."; // + Oil, - Water
        _text[599, 2] = "";
        _text[599, 3] = "";
        _text[599, 4] = "";
        _text[599, 5] = "";
        _text[599, 6] = "";
        _text[599, 7] = "";
        _text[599, 8] = "";
        _text[599, 9] = "";

        // 1_MaterialDialogue
        _text[600, 0] = "The cameras catch a broken cargo barge tumbling nearby. Its containers have split open.\n\nInside: stacks of cut stone blocks, packed for construction and forgotten in the dark.";
        _text[600, 1] = "Камеры фиксируют рядом сломанную грузовую баржу. Её контейнеры разорваны.\n\nВнутри — стопки каменных блоков, заготовленных для строительства и забытых в темноте.";
        _text[600, 2] = "";
        _text[600, 3] = "";
        _text[600, 4] = "";
        _text[600, 5] = "";
        _text[600, 6] = "";
        _text[600, 7] = "";
        _text[600, 8] = "";
        _text[600, 9] = "";

        _text[601, 0] = "Salvage the blocks";
        _text[601, 1] = "Забрать блоки"; // выбор 1
        _text[601, 2] = "";
        _text[601, 3] = "";
        _text[601, 4] = "";
        _text[601, 5] = "";
        _text[601, 6] = "";
        _text[601, 7] = "";
        _text[601, 8] = "";
        _text[601, 9] = "";

        _text[602, 0] = "Success: the drones latch onto the containers and tow them in.\n\nYou reinforce the cargo bay and secure the load.";
        _text[602, 1] = "Успех: дроны цепляют контейнеры и затаскивают их внутрь.\n\nВы укрепляете грузовой отсек и фиксируете добычу."; // + каменный блок
        _text[602, 2] = "";
        _text[602, 3] = "";
        _text[602, 4] = "";
        _text[602, 5] = "";
        _text[602, 6] = "";
        _text[602, 7] = "";
        _text[602, 8] = "";
        _text[602, 9] = "";

        _text[603, 0] = "Failure: the barge rotates unexpectedly. A container slams into the hull, tearing plating.\n\nDrones rush to seal the breach as you disengage.";
        _text[603, 1] = "Провал: баржа внезапно проворачивается. Контейнер с силой врезается в корпус, срывая обшивку.\n\nДроны срочно герметизируют пробоину, пока вы отходите на безопасную дистанцию."; // - ядро
        _text[603, 2] = "";
        _text[603, 3] = "";
        _text[603, 4] = "";
        _text[603, 5] = "";
        _text[603, 6] = "";
        _text[603, 7] = "";
        _text[603, 8] = "";
        _text[603, 9] = "";

        _text[604, 0] = "Ignore and move on";
        _text[604, 1] = "Проигнорировать"; // выбор 2
        _text[604, 2] = "";
        _text[604, 3] = "";
        _text[604, 4] = "";
        _text[604, 5] = "";
        _text[604, 6] = "";
        _text[604, 7] = "";
        _text[604, 8] = "";
        _text[604, 9] = "";

        _text[605, 0] = "You leave the wreck behind. The barge keeps spinning in silence, shedding stone into the void.";
        _text[605, 1] = "Вы оставляете обломки позади. Баржа продолжает вращаться в тишине, рассыпая камень в пустоту."; // ничего
        _text[605, 2] = "";
        _text[605, 3] = "";
        _text[605, 4] = "";
        _text[605, 5] = "";
        _text[605, 6] = "";
        _text[605, 7] = "";
        _text[605, 8] = "";
        _text[605, 9] = "";

        // 2_MaterialDialogue
        _text[606, 0] = "A trade ship crosses your route. Its hull is patched with welded plates, and the cargo modules are wrapped in heat shielding.\n\nA short message breaks through:\n\n\"Iron ingots. Clean cast. Fixed price.\"";
        _text[606, 1] = "Торговый корабль пересекает ваш маршрут. Его корпус залатан сварными пластинами, а грузовые модули закрыты термозащитными кожухами.\n\nВ эфир проходит короткое сообщение:\n\n\"�-елезные слитки. Чистое литьё. Цена фиксирована\".";
        _text[606, 2] = "";
        _text[606, 3] = "";
        _text[606, 4] = "";
        _text[606, 5] = "";
        _text[606, 6] = "";
        _text[606, 7] = "";
        _text[606, 8] = "";
        _text[606, 9] = "";

        _text[607, 0] = "Buy ingots for quanta";
        _text[607, 1] = "Купить слитки за кванты"; // выбор 1
        _text[607, 2] = "";
        _text[607, 3] = "";
        _text[607, 4] = "";
        _text[607, 5] = "";
        _text[607, 6] = "";
        _text[607, 7] = "";
        _text[607, 8] = "";
        _text[607, 9] = "";

        _text[608, 0] = "The transfer completes. Sealed crates are pushed toward you along a magnetic tether.\n\nThe trader cuts the channel and changes course.";
        _text[608, 1] = "Обмен завершён. Герметичные ящики подтягиваются к вам по магнитному тросу.\n\nТорговец обрывает связь и меняет курс."; // - кванты, + железные слитки
        _text[608, 2] = "";
        _text[608, 3] = "";
        _text[608, 4] = "";
        _text[608, 5] = "";
        _text[608, 6] = "";
        _text[608, 7] = "";
        _text[608, 8] = "";
        _text[608, 9] = "";

        _text[609, 0] = "Attack the trade ship";
        _text[609, 1] = "Напасть на корабль"; // выбор 2
        _text[609, 2] = "";
        _text[609, 3] = "";
        _text[609, 4] = "";
        _text[609, 5] = "";
        _text[609, 6] = "";
        _text[609, 7] = "";
        _text[609, 8] = "";
        _text[609, 9] = "";

        _text[610, 0] = "Success: a precise strike disables their drive. Drones cut the cargo locks and pull the containers free.\n\nYou disengage before the distress signal can spread.";
        _text[610, 1] = "Успех: точный удар выводит из строя их привод. Дроны вскрывают грузовые замки и отцепляют контейнеры.\n\nВы уходите, пока сигнал бедствия не успел разойтись."; // + железные слитки
        _text[610, 2] = "";
        _text[610, 3] = "";
        _text[610, 4] = "";
        _text[610, 5] = "";
        _text[610, 6] = "";
        _text[610, 7] = "";
        _text[610, 8] = "";
        _text[610, 9] = "";

        _text[611, 0] = "Failure: the trader was armed. A burst damages your hull and knocks out part of your systems.\n\nYou lose a core and engage warp, leaving the battlefield.";
        _text[611, 1] = "Провал: торговец оказался вооружён. Очередь повреждает обшивку и выводит из строя часть систем.\n\nВы теряете одно ядро и включаете варп, уходя с поля боя."; // -1 ядро, без слитков
        _text[611, 2] = "";
        _text[611, 3] = "";
        _text[611, 4] = "";
        _text[611, 5] = "";
        _text[611, 6] = "";
        _text[611, 7] = "";
        _text[611, 8] = "";
        _text[611, 9] = "";

        // 3_MaterialDialogue
        _text[612, 0] = "You detect a bulky smelter platform drifting nearby.\n\nWork lights are on. You see movement behind the heat shields — not drones.\n\nA tired voice comes through the comms:\n\n\"Ship. Do you have iron ingots? Our furnace is running, but we're out of feed.\n\nGive us iron — we'll melt it and cast steel.\"";
        _text[612, 1] = "Вы обнаружили рядом громоздкую плавильную платформу.\n\nНа ней горит рабочее освещение. За термощитами заметно движение — это не дроны.\n\nВ связь выходит усталый голос:\n\n\"Корабль. Есть железные слитки? Печь работает, но сырьё закончилось.\n\nДайте железо — мы переплавим и отольём сталь\".";
        _text[612, 2] = "";
        _text[612, 3] = "";
        _text[612, 4] = "";
        _text[612, 5] = "";
        _text[612, 6] = "";
        _text[612, 7] = "";
        _text[612, 8] = "";
        _text[612, 9] = "";

        _text[613, 0] = "Transfer iron ingots";
        _text[613, 1] = "Передать железные слитки"; // выбор 1
        _text[613, 2] = "";
        _text[613, 3] = "";
        _text[613, 4] = "";
        _text[613, 5] = "";
        _text[613, 6] = "";
        _text[613, 7] = "";
        _text[613, 8] = "";
        _text[613, 9] = "";

        _text[614, 0] = "The platform locks onto your crates and pulls them into the smelting line.\n\nAfter a short wait, a cooled container is returned: steel ingots, sealed and marked.";
        _text[614, 1] = "Платформа фиксирует ваши ящики и подаёт их на линию переплавки.\n\nЧерез некоторое время возвращается остуженный контейнер: стальные слитки, запечатанные и промаркированные."; // - железные слитки, + стальные слитки
        _text[614, 2] = "";
        _text[614, 3] = "";
        _text[614, 4] = "";
        _text[614, 5] = "";
        _text[614, 6] = "";
        _text[614, 7] = "";
        _text[614, 8] = "";
        _text[614, 9] = "";

        _text[615, 0] = "Refuse";
        _text[615, 1] = "Отказаться"; // выбор 2
        _text[615, 2] = "";
        _text[615, 3] = "";
        _text[615, 4] = "";
        _text[615, 5] = "";
        _text[615, 6] = "";
        _text[615, 7] = "";
        _text[615, 8] = "";
        _text[615, 9] = "";

        _text[616, 0] = "The channel closes. The platform continues its work without responding.";
        _text[616, 1] = "Канал закрывается. Платформа продолжает работу и больше не отвечает."; // ничего
        _text[616, 2] = "";
        _text[616, 3] = "";
        _text[616, 4] = "";
        _text[616, 5] = "";
        _text[616, 6] = "";
        _text[616, 7] = "";
        _text[616, 8] = "";
        _text[616, 9] = "";

        _text[617, 0] = "Try to take steel by force";
        _text[617, 1] = "Попытаться забрать сталь силой"; // выбор 3
        _text[617, 2] = "";
        _text[617, 3] = "";
        _text[617, 4] = "";
        _text[617, 5] = "";
        _text[617, 6] = "";
        _text[617, 7] = "";
        _text[617, 8] = "";
        _text[617, 9] = "";

        _text[618, 0] = "Success: you disable the outer locks and pull one container free.\n\nYou engage warp and leave with steel ingots.";
        _text[618, 1] = "Успех: вы выводите из строя внешние замки и отцепляете один контейнер.\n\nВы включаете варп и уходите со стальными слитками."; // + стальные слитки
        _text[618, 2] = "";
        _text[618, 3] = "";
        _text[618, 4] = "";
        _text[618, 5] = "";
        _text[618, 6] = "";
        _text[618, 7] = "";
        _text[618, 8] = "";
        _text[618, 9] = "";

        _text[619, 0] = "Failure: the platform triggers an emergency vent. A blast of superheated gas hits the hull, damaging plating and overloading a core.\n\nYou lose one core and engage warp, leaving the platform behind.";
        _text[619, 1] = "Провал: платформа включает аварийный сброс. Выброс перегретого газа бьёт по корпусу, повреждает обшивку и перегружает ядро.\n\nВы теряете одно ядро и включаете варп, оставляя платформу позади."; // -1 ядро
        _text[619, 2] = "";
        _text[619, 3] = "";
        _text[619, 4] = "";
        _text[619, 5] = "";
        _text[619, 6] = "";
        _text[619, 7] = "";
        _text[619, 8] = "";
        _text[619, 9] = "";

        // 4_MaterialDialogue
        _text[620, 0] = "A fault in the engine block forces you to set down on the nearest planet for repairs.\n\nYou choose the dark side and land with minimal thrust, cutting all external lights.\n\nWhile the drones inspect the damage, the sensors catch faint heat signatures not far away.\n\nAhead: a small processing site still operating — generators, containers, and a warehouse marked for copper.\n\nInside are crates of copper plates.";
        _text[620, 1] = "Сбой в узле двигателя вынуждает вас сесть на ближайшую планету для ремонта.\n\nВы выбираете тёмную сторону и садитесь на минимальной тяге, полностью гасите внешнее освещение.\n\nПока дроны осматривают повреждения, сенсоры фиксируют слабые тепловые сигнатуры неподалёку.\n\nВпереди — небольшой перерабатывающий участок: генераторы, контейнеры и склад с маркировкой меди.\n\nВнутри — ящики с медными пластинами.";
        _text[620, 2] = "";
        _text[620, 3] = "";
        _text[620, 4] = "";
        _text[620, 5] = "";
        _text[620, 6] = "";
        _text[620, 7] = "";
        _text[620, 8] = "";
        _text[620, 9] = "";

        _text[621, 0] = "Do not risk it";
        _text[621, 1] = "Не рисковать"; // выбор 1
        _text[621, 2] = "";
        _text[621, 3] = "";
        _text[621, 4] = "";
        _text[621, 5] = "";
        _text[621, 6] = "";
        _text[621, 7] = "";
        _text[621, 8] = "";
        _text[621, 9] = "";

        _text[622, 0] = "You keep the drones on repairs and avoid unnecessary contacts.\n\nThe site remains in the dark behind you.";
        _text[622, 1] = "Вы оставляете дронов на ремонте и избегаете лишних контактов.\n\nУчасток остаётся позади, растворяясь в темноте."; // ничего
        _text[622, 2] = "";
        _text[622, 3] = "";
        _text[622, 4] = "";
        _text[622, 5] = "";
        _text[622, 6] = "";
        _text[622, 7] = "";
        _text[622, 8] = "";
        _text[622, 9] = "";

        _text[623, 0] = "Sneak in through a service hatch";
        _text[623, 1] = "Тихо пробраться через сервисный люк"; // выбор 2
        _text[623, 2] = "";
        _text[623, 3] = "";
        _text[623, 4] = "";
        _text[623, 5] = "";
        _text[623, 6] = "";
        _text[623, 7] = "";
        _text[623, 8] = "";
        _text[623, 9] = "";

        _text[624, 0] = "You move along unlit routes and zones outside the cameras' view, then slip into the warehouse through a service hatch.\n\nThe crates are close. Taking them out is the dangerous part.";
        _text[624, 1] = "Вы идёте по неосвещённым проходам и зонам вне обзора камер, затем пробираетесь на склад через сервисный люк.\n\nЯщики рядом. Опаснее всего — вынести их наружу.";
        _text[624, 2] = "";
        _text[624, 3] = "";
        _text[624, 4] = "";
        _text[624, 5] = "";
        _text[624, 6] = "";
        _text[624, 7] = "";
        _text[624, 8] = "";
        _text[624, 9] = "";

        _text[625, 0] = "Cut power first";
        _text[625, 1] = "Сначала обесточить участок"; // выбор 2.1
        _text[625, 2] = "";
        _text[625, 3] = "";
        _text[625, 4] = "";
        _text[625, 5] = "";
        _text[625, 6] = "";
        _text[625, 7] = "";
        _text[625, 8] = "";
        _text[625, 9] = "";

        _text[626, 0] = "You try to shut down the generators.\n\nSuccess: lights and sensors go quiet.";
        _text[626, 1] = "Вы пытаетесь заглушить генераторы.\n\nУспех: свет и датчики замолкают."; // выбор 2.1 успех
        _text[626, 2] = "";
        _text[626, 3] = "";
        _text[626, 4] = "";
        _text[626, 5] = "";
        _text[626, 6] = "";
        _text[626, 7] = "";
        _text[626, 8] = "";
        _text[626, 9] = "";

        _text[627, 0] = "You try to shut down the generators. Failure: the load spikes, and the alarm wakes up.";
        _text[627, 1] = "Вы пытаетесь заглушить генераторы. Провал: скачок нагрузки — и система тревоги просыпается."; // выбор 2.1 провал
        _text[627, 2] = "";
        _text[627, 3] = "";
        _text[627, 4] = "";
        _text[627, 5] = "";
        _text[627, 6] = "";
        _text[627, 7] = "";
        _text[627, 8] = "";
        _text[627, 9] = "";

        _text[628, 0] = "Fast grab and leave";
        _text[628, 1] = "Быстро схватить и уйти"; // выбор 2.2
        _text[628, 2] = "";
        _text[628, 3] = "";
        _text[628, 4] = "";
        _text[628, 5] = "";
        _text[628, 6] = "";
        _text[628, 7] = "";
        _text[628, 8] = "";
        _text[628, 9] = "";

        _text[629, 0] = "Success: you pull the crates out and lift off before any response arrives.\n\nCopper plates are secured.";
        _text[629, 1] = "Успех: вы вытаскиваете ящики и взлетаете до того, как успевает прилететь ответ.\n\nМедные пластины закреплены."; // выбор 2.2 успех + медные пластины
        _text[629, 2] = "";
        _text[629, 3] = "";
        _text[629, 4] = "";
        _text[629, 5] = "";
        _text[629, 6] = "";
        _text[629, 7] = "";
        _text[629, 8] = "";
        _text[629, 9] = "";

        _text[630, 0] = "Failure: you are spotted. Fire hits the hull during takeoff.\n\nYou engage warp and escape, but one core fails.";
        _text[630, 1] = "Провал: вас замечают. При взлёте по корпусу приходятся попадания.\n\nВы включаете варп и уходите, но одно ядро выходит из строя."; // выбор 2.2 провал -1 ядро
        _text[630, 2] = "";
        _text[630, 3] = "";
        _text[630, 4] = "";
        _text[630, 5] = "";
        _text[630, 6] = "";
        _text[630, 7] = "";
        _text[630, 8] = "";
        _text[630, 9] = "";

        // 5_MaterialDialogue
        _text[631, 0] = "You pick up an abandoned industrial site on the surface: ruined mixers, cracked silos, and a concrete pad covered with dust.\n\nThe main storage is half-collapsed, but pallets of sealed bags and hardened blocks are still stacked inside.";
        _text[631, 1] = "Вы находите заброшенный промышленный объект на поверхности: разрушенные смесители, треснувшие силосы и бетонную площадку, занесённую пылью.\n\nГлавное хранилище частично обрушено, но внутри всё ещё сложены паллеты с запечатанными мешками и затвердевшими блоками.";
        _text[631, 2] = "";
        _text[631, 3] = "";
        _text[631, 4] = "";
        _text[631, 5] = "";
        _text[631, 6] = "";
        _text[631, 7] = "";
        _text[631, 8] = "";
        _text[631, 9] = "";

        _text[632, 0] = "Take concrete from the outer stacks";
        _text[632, 1] = "Забрать бетон у входа"; // выбор 1
        _text[632, 2] = "";
        _text[632, 3] = "";
        _text[632, 4] = "";
        _text[632, 5] = "";
        _text[632, 6] = "";
        _text[632, 7] = "";
        _text[632, 8] = "";
        _text[632, 9] = "";

        _text[633, 0] = "You load the nearest concrete onto the drones, trying not to disturb the unstable structure.\n\nSuccess: the loading goes quickly. You retreat before the structure shifts.\n\nConcrete is secured.";
        _text[633, 1] = "Вы грузите ближайший бетон на дронов, стараясь не тревожить нестабильные конструкции.\n\nУспех: погрузка проходит быстро. Вы отходите до того, как конструкция начинает проседать.\n\nБетон закреплён."; // выбор 1 успех + бетон
        _text[633, 2] = "";
        _text[633, 3] = "";
        _text[633, 4] = "";
        _text[633, 5] = "";
        _text[633, 6] = "";
        _text[633, 7] = "";
        _text[633, 8] = "";
        _text[633, 9] = "";

        _text[634, 0] = "You load the nearest concrete onto the drones, trying not to disturb the unstable structure.\n\nFailure: the ground shifts. Debris falls and buries the stacks.\n\nYou retreat empty-handed.";
        _text[634, 1] = "Вы грузите ближайший бетон на дронов, стараясь не тревожить нестабильные конструкции.\n\nПровал: грунт проседает. Обломки осыпаются и заваливают стопки.\n\nВы отходите ни с чем."; // выбор 1 провал ничего
        _text[634, 2] = "";
        _text[634, 3] = "";
        _text[634, 4] = "";
        _text[634, 5] = "";
        _text[634, 6] = "";
        _text[634, 7] = "";
        _text[634, 8] = "";
        _text[634, 9] = "";

        _text[635, 0] = "Inspect the storage deeper";
        _text[635, 1] = "Пройти глубже в хранилище"; // выбор 2
        _text[635, 2] = "";
        _text[635, 3] = "";
        _text[635, 4] = "";
        _text[635, 5] = "";
        _text[635, 6] = "";
        _text[635, 7] = "";
        _text[635, 8] = "";
        _text[635, 9] = "";

        _text[636, 0] = "The deeper sections are unstable. Dust hangs in the air, and the ceiling strains under its own weight.\n\nYou can take concrete faster — or do it carefully.";
        _text[636, 1] = "Глубинные секции нестабильны. В воздухе висит пыль, а перекрытия держатся на пределе.\n\nМожно забрать бетон быстрее — или действовать осторожно.";
        _text[636, 2] = "";
        _text[636, 3] = "";
        _text[636, 4] = "";
        _text[636, 5] = "";
        _text[636, 6] = "";
        _text[636, 7] = "";
        _text[636, 8] = "";
        _text[636, 9] = "";

        _text[637, 0] = "Cut supports and pull pallets fast";
        _text[637, 1] = "Срезать опоры и вытащить паллеты быстро"; // выбор 2.1
        _text[637, 2] = "";
        _text[637, 3] = "";
        _text[637, 4] = "";
        _text[637, 5] = "";
        _text[637, 6] = "";
        _text[637, 7] = "";
        _text[637, 8] = "";
        _text[637, 9] = "";

        _text[638, 0] = "The drones cut the supports and yank the pallets free.\n\nSuccess: the load is taken out in seconds. Concrete is secured.";
        _text[638, 1] = "Дроны срезают опоры и выдёргивают паллеты.\n\nУспех: груз вынесен за секунды. Бетон закреплён."; // выбор 2.1 успех + бетон
        _text[638, 2] = "";
        _text[638, 3] = "";
        _text[638, 4] = "";
        _text[638, 5] = "";
        _text[638, 6] = "";
        _text[638, 7] = "";
        _text[638, 8] = "";
        _text[638, 9] = "";

        _text[639, 0] = "The drones cut the supports and yank the pallets free.\n\nFailure: the ceiling collapses. You escape, but one core fails from the impact and overload.";
        _text[639, 1] = "Дроны срезают опоры и выдёргивают паллеты.\n\nПровал: перекрытия рушатся. Вы уходите, но одно ядро выходит из строя от удара и перегрузки."; // выбор 2.1 провал -1 ядро
        _text[639, 2] = "";
        _text[639, 3] = "";
        _text[639, 4] = "";
        _text[639, 5] = "";
        _text[639, 6] = "";
        _text[639, 7] = "";
        _text[639, 8] = "";
        _text[639, 9] = "";

        _text[640, 0] = "Move slowly with stabilizers";
        _text[640, 1] = "Действовать медленно со стабилизаторами"; // выбор 2.2
        _text[640, 2] = "";
        _text[640, 3] = "";
        _text[640, 4] = "";
        _text[640, 5] = "";
        _text[640, 6] = "";
        _text[640, 7] = "";
        _text[640, 8] = "";
        _text[640, 9] = "";

        _text[641, 0] = "You deploy stabilizers and guide the drones through narrow passages.\n\nSuccess: the storage holds.\n\nConcrete is secured.";
        _text[641, 1] = "Вы ставите стабилизаторы и ведёте дронов по узким проходам.\n\nУспех: хранилище выдерживает.\n\nБетон закреплён."; // выбор 2.2 успех + бетон
        _text[641, 2] = "";
        _text[641, 3] = "";
        _text[641, 4] = "";
        _text[641, 5] = "";
        _text[641, 6] = "";
        _text[641, 7] = "";
        _text[641, 8] = "";
        _text[641, 9] = "";

        _text[642, 0] = "You deploy stabilizers and guide the drones through narrow passages.\n\nFailure: a hidden crack opens under load. The pallets fall, and you leave empty-handed.";
        _text[642, 1] = "Вы ставите стабилизаторы и ведёте дронов по узким проходам.\n\nПровал: скрытая трещина раскрывается под нагрузкой. Паллеты срываются вниз, и вы уходите ни с чем."; // выбор 2.2 провал
        _text[642, 2] = "";
        _text[642, 3] = "";
        _text[642, 4] = "";
        _text[642, 5] = "";
        _text[642, 6] = "";
        _text[642, 7] = "";
        _text[642, 8] = "";
        _text[642, 9] = "";

        // 6_MaterialDialogue
        _text[643, 0] = "A coolant leak forces you to descend and land on the nearest planet for repairs.\n\nThe landing zone is cold and dark. While the drones inspect the damage, the sensors pick up a steady thermal source nearby.\n\nIt is a geothermal vent. Next to it stands an old condenser unit and a line of pipes going into the rock.\n\nThe system is still producing steam, but the pressure is unstable.";
        _text[643, 1] = "Утечка охлаждения вынуждает вас снизиться и сесть на ближайшую планету для ремонта.\n\nМесто посадки холодное и тёмное. Пока дроны осматривают повреждения, сенсоры фиксируют ровный тепловой источник неподалёку.\n\nЭто геотермальный разлом. Рядом стоит старый конденсаторный блок и линия труб, уходящая в породу.\n\nСистема всё ещё даёт пар, но давление нестабильно.";
        _text[643, 2] = "";
        _text[643, 3] = "";
        _text[643, 4] = "";
        _text[643, 5] = "";
        _text[643, 6] = "";
        _text[643, 7] = "";
        _text[643, 8] = "";
        _text[643, 9] = "";

        _text[644, 0] = "Connect to the vent carefully";
        _text[644, 1] = "Подключиться осторожно"; // выбор 1
        _text[644, 2] = "";
        _text[644, 3] = "";
        _text[644, 4] = "";
        _text[644, 5] = "";
        _text[644, 6] = "";
        _text[644, 7] = "";
        _text[644, 8] = "";
        _text[644, 9] = "";

        _text[645, 0] = "You align the collectors and connect flexible lines, slowly opening the valves.\n\nSuccess: the pressure stabilizes. You fill the tanks with steam and seal the system.";
        _text[645, 1] = "Вы выравниваете коллекторы и подключаете гибкие магистрали, медленно открывая клапаны.\n\nУспех: давление стабилизируется. Вы заполняете баки паром и герметизируете систему."; // выбор 1 успех + пар
        _text[645, 2] = "";
        _text[645, 3] = "";
        _text[645, 4] = "";
        _text[645, 5] = "";
        _text[645, 6] = "";
        _text[645, 7] = "";
        _text[645, 8] = "";
        _text[645, 9] = "";

        _text[646, 0] = "You align the collectors and connect flexible lines, slowly opening the valves.\n\nFailure: the pressure spikes. A hot discharge hits the equipment, and you have to break contact.";
        _text[646, 1] = "Вы выравниваете коллекторы и подключаете гибкие магистрали, медленно открывая клапаны.\n\nПровал: давление срывается вверх. Горячий выброс бьёт по оборудованию, и вам приходится разорвать подключение."; // выбор 1 провал ничего
        _text[646, 2] = "";
        _text[646, 3] = "";
        _text[646, 4] = "";
        _text[646, 5] = "";
        _text[646, 6] = "";
        _text[646, 7] = "";
        _text[646, 8] = "";
        _text[646, 9] = "";

        _text[647, 0] = "Take steam quickly";
        _text[647, 1] = "Забрать пар быстро"; // выбор 2
        _text[647, 2] = "";
        _text[647, 3] = "";
        _text[647, 4] = "";
        _text[647, 5] = "";
        _text[647, 6] = "";
        _text[647, 7] = "";
        _text[647, 8] = "";
        _text[647, 9] = "";

        _text[648, 0] = "You open the valves to maximum and force the vent into the tanks.\n\nSuccess: you fill the tanks before the system destabilizes.\n\nSteam is secured.";
        _text[648, 1] = "Вы открываете клапаны на максимум и форсируете подачу в баки.\n\nУспех: вы успеваете заполнить баки до того, как система срывается.\n\nПар закреплён."; // выбор 2 успех + пар
        _text[648, 2] = "";
        _text[648, 3] = "";
        _text[648, 4] = "";
        _text[648, 5] = "";
        _text[648, 6] = "";
        _text[648, 7] = "";
        _text[648, 8] = "";
        _text[648, 9] = "";

        _text[649, 0] = "You open the valves to maximum and force the vent into the tanks.\n\nFailure: the line bursts under pressure. Shrapnel and heat damage the equipment, and one core fails.";
        _text[649, 1] = "Вы открываете клапаны на максимум и форсируете подачу в баки.\n\nПровал: магистраль рвёт давлением. Осколки и жар повреждают оборудование, и одно ядро выходит из строя."; // выбор 2 провал -1 ядро
        _text[649, 2] = "";
        _text[649, 3] = "";
        _text[649, 4] = "";
        _text[649, 5] = "";
        _text[649, 6] = "";
        _text[649, 7] = "";
        _text[649, 8] = "";
        _text[649, 9] = "";

        // 0_ComponentDialogue
        _text[650, 0] = "A malfunction in the drive system forces you to make a short stop.\n\nDuring diagnostics, the AI detects an abandoned machine shop nearby. The entrance is blocked, but inside the scanners register intact mechanical lines.\n\nOn the conveyor are crates with gear wheels. Next to them are boxes of iron ingots.\n\nThe workshop looks automated, but its power cycles are unstable.";
        _text[650, 1] = "Сбой в приводной системе вынуждает вас сделать короткую остановку.\n\nВо время диагностики ИИ фиксирует неподалёку заброшенный механический цех. Вход завален, но сканеры видят внутри целые производственные линии.\n\nНа конвейере стоят ящики с шестернями. Рядом — коробки с железными слитками.\n\nЦех выглядит автоматизированным, но питание работает нестабильно.";
        _text[650, 2] = "";
        _text[650, 3] = "";
        _text[650, 4] = "";
        _text[650, 5] = "";
        _text[650, 6] = "";
        _text[650, 7] = "";
        _text[650, 8] = "";
        _text[650, 9] = "";

        _text[651, 0] = "Take ready crates from the conveyor";
        _text[651, 1] = "Забрать готовые ящики с конвейера"; // выбор 1
        _text[651, 2] = "";
        _text[651, 3] = "";
        _text[651, 4] = "";
        _text[651, 5] = "";
        _text[651, 6] = "";
        _text[651, 7] = "";
        _text[651, 8] = "";
        _text[651, 9] = "";

        _text[652, 0] = "You try to pull the crates off the conveyor and load them onto the drones.\n\nSuccess: the mechanism stays quiet. You remove the crates and leave the workshop.\n\nGear wheels are secured.";
        _text[652, 1] = "Вы пытаетесь снять ящики с конвейера и погрузить их на дронов.\n\nУспех: механизм не подаёт признаков активности. Вы снимаете ящики и покидаете цех.\n\nШестерни закреплены."; // выбор 1 успех + GearWheel
        _text[652, 2] = "";
        _text[652, 3] = "";
        _text[652, 4] = "";
        _text[652, 5] = "";
        _text[652, 6] = "";
        _text[652, 7] = "";
        _text[652, 8] = "";
        _text[652, 9] = "";

        _text[653, 0] = "You try to pull the crates off the conveyor and load them onto the drones.\n\nFailure: the conveyor wakes up and drags the crates back. The drones get caught, and you lose one core while breaking contact.";
        _text[653, 1] = "Вы пытаетесь снять ящики с конвейера и погрузить их на дронов.\n\nПровал: конвейер приходит в движение и затягивает ящики обратно. Дроны попадают в захват, и при разрыве контакта одно ядро выходит из строя."; // выбор 1 провал -1 ядро
        _text[653, 2] = "";
        _text[653, 3] = "";
        _text[653, 4] = "";
        _text[653, 5] = "";
        _text[653, 6] = "";
        _text[653, 7] = "";
        _text[653, 8] = "";
        _text[653, 9] = "";

        _text[654, 0] = "Start the press line";
        _text[654, 1] = "Запустить линию прессов"; // выбор 2
        _text[654, 2] = "";
        _text[654, 3] = "";
        _text[654, 4] = "";
        _text[654, 5] = "";
        _text[654, 6] = "";
        _text[654, 7] = "";
        _text[654, 8] = "";
        _text[654, 9] = "";

        _text[655, 0] = "You feed the line with iron ingots and start the presses.\n\nSuccess: the machines stamp gear wheels one by one. You load the finished parts and stop the line.\n\nGear wheels are secured.";
        _text[655, 1] = "Вы подаёте на линию железные слитки и запускаете прессы.\n\nУспех: станки штампуют шестерни одну за другой. Вы загружаете готовые детали и останавливаете линию.\n\nШестерни закреплены."; // выбор 2 успех + GearWheel - IronIngot
        _text[655, 2] = "";
        _text[655, 3] = "";
        _text[655, 4] = "";
        _text[655, 5] = "";
        _text[655, 6] = "";
        _text[655, 7] = "";
        _text[655, 8] = "";
        _text[655, 9] = "";

        _text[656, 0] = "You feed the line with iron ingots and start the presses.\n\nFailure: the power spikes. The press jams, sparks hit the control unit, and the system shuts down.\n\nYou retreat without gear wheels.";
        _text[656, 1] = "Вы подаёте на линию железные слитки и запускаете прессы.\n\nПровал: питание срывается. Пресс клинит, искры попадают в блок управления, и система отключается.\n\nВы отходите без шестерён."; // выбор 2 провал ничего
        _text[656, 2] = "";
        _text[656, 3] = "";
        _text[656, 4] = "";
        _text[656, 5] = "";
        _text[656, 6] = "";
        _text[656, 7] = "";
        _text[656, 8] = "";
        _text[656, 9] = "";

        // 1_ComponentDialogue
        _text[657, 0] = "During a routine scan, you detect a weak beacon from a drifting service capsule.\n\nIts корпус is scorched, the docking clamps are bent, but the internal containers are intact.\n\nThe markings match old ship electronics: circuit blocks, sealed and protected from vacuum.\n\nThe capsule is slowly rotating. Any грубый захват can tear it apart.";
        _text[657, 1] = "Во время планового сканирования вы фиксируете слабый сигнал от дрейфующей сервисной капсулы.\n\nЕё корпус обожжён, стыковочные захваты погнуты, но внутренние контейнеры целы.\n\nМаркировка соответствует старой корабельной электронике: блоки электронных схем, запечатанные и защищённые от вакуума.\n\nКапсула медленно вращается. Любой грубый захват может разорвать её.";
        _text[657, 2] = "";
        _text[657, 3] = "";
        _text[657, 4] = "";
        _text[657, 5] = "";
        _text[657, 6] = "";
        _text[657, 7] = "";
        _text[657, 8] = "";
        _text[657, 9] = "";

        _text[658, 0] = "Cut the container out from a distance";
        _text[658, 1] = "Срезать контейнер с дистанции"; // выбор 1
        _text[658, 2] = "";
        _text[658, 3] = "";
        _text[658, 4] = "";
        _text[658, 5] = "";
        _text[658, 6] = "";
        _text[658, 7] = "";
        _text[658, 8] = "";
        _text[658, 9] = "";

        _text[659, 0] = "You use a cutter beam and try to separate the container without touching the capsule.\n\nSuccess: the container comes free and is caught by the drones.\n\nElectronic circuits are secured.";
        _text[659, 1] = "Вы используете режущий луч и пытаетесь отделить контейнер, не касаясь капсулы.\n\nУспех: контейнер отделяется и перехватывается дронами.\n\nЭлектронные схемы закреплены."; // выбор 1 успех + ElectronicCircuit
        _text[659, 2] = "";
        _text[659, 3] = "";
        _text[659, 4] = "";
        _text[659, 5] = "";
        _text[659, 6] = "";
        _text[659, 7] = "";
        _text[659, 8] = "";
        _text[659, 9] = "";

        _text[660, 0] = "You use a cutter beam and try to separate the container without touching the capsule.\n\nFailure: the cut hits a pressure line. A jet удар sends fragments into your hull.\n\nOne core fails, and you break contact.";
        _text[660, 1] = "Вы используете режущий луч и пытаетесь отделить контейнер, не касаясь капсулы.\n\nПровал: рез задевает магистраль. Реактивный выброс швыряет осколки в корпус.\n\nОдно ядро выходит из строя, и вы разрываете контакт."; // выбор 1 провал -1 ядро
        _text[660, 2] = "";
        _text[660, 3] = "";
        _text[660, 4] = "";
        _text[660, 5] = "";
        _text[660, 6] = "";
        _text[660, 7] = "";
        _text[660, 8] = "";
        _text[660, 9] = "";

        _text[661, 0] = "Dock and open the capsule";
        _text[661, 1] = "Пристыковаться и вскрыть капсулу"; // выбор 2
        _text[661, 2] = "";
        _text[661, 3] = "";
        _text[661, 4] = "";
        _text[661, 5] = "";
        _text[661, 6] = "";
        _text[661, 7] = "";
        _text[661, 8] = "";
        _text[661, 9] = "";

        _text[662, 0] = "You stabilize the rotation and connect the docking clamps.\n\nSuccess: the seal holds. You open the container and take electronic circuits.\n\nElectronic circuits are secured.";
        _text[662, 1] = "Вы гасите вращение и фиксируете капсулу стыковочными захватами.\n\nУспех: герметизация держится. Вы вскрываете контейнер и забираете электронные схемы.\n\nЭлектронные схемы закреплены."; // выбор 2 успех + ElectronicCircuit
        _text[662, 2] = "";
        _text[662, 3] = "";
        _text[662, 4] = "";
        _text[662, 5] = "";
        _text[662, 6] = "";
        _text[662, 7] = "";
        _text[662, 8] = "";
        _text[662, 9] = "";

        _text[663, 0] = "You stabilize the rotation and connect the docking clamps.\n\nFailure: the clamp slips. The capsule cracks, the container vents, and the contents scatter into space.\n\nYou leave empty-handed.";
        _text[663, 1] = "Вы гасите вращение и фиксируете капсулу стыковочными захватами.\n\nПровал: захват срывается. Капсула трескается, контейнер разгерметизируется, и содержимое разлетается в космос.\n\nВы уходите ни с чем."; // выбор 2 провал ничего
        _text[663, 2] = "";
        _text[663, 3] = "";
        _text[663, 4] = "";
        _text[663, 5] = "";
        _text[663, 6] = "";
        _text[663, 7] = "";
        _text[663, 8] = "";
        _text[663, 9] = "";

        // 2_ComponentDialogue
        _text[664, 0] = "The AI detects a drifting communications relay in orbit.\n\nAs you approach, the relay suddenly comes online and transmits a short encrypted message:\n\n\"CODE: 0101100000\"\n\nIt repeats the same line again and again, waiting for a response.";
        _text[664, 1] = "ИИ фиксирует дрейфующий коммуникационный ретранслятор на орбите.\n\nПри сближении ретранслятор внезапно оживает и передаёт короткое зашифрованное сообщение:\n\n\"CODE: 0101100000\"\n\nСтрока повторяется снова и снова, будто ожидая ответ.";
        _text[664, 2] = "";
        _text[664, 3] = "";
        _text[664, 4] = "";
        _text[664, 5] = "";
        _text[664, 6] = "";
        _text[664, 7] = "";
        _text[664, 8] = "";
        _text[664, 9] = "";

        _text[665, 0] = "Transmit code \"352\"";
        _text[665, 1] = "Передать код \"352\""; // выбор 1
        _text[665, 2] = "";
        _text[665, 3] = "";
        _text[665, 4] = "";
        _text[665, 5] = "";
        _text[665, 6] = "";
        _text[665, 7] = "";
        _text[665, 8] = "";
        _text[665, 9] = "";

        _text[666, 0] = "The relay accepts the code. A service hatch opens, exposing sealed compute blocks.\n\nYou take the processors and break contact.\n\nProcessors are secured.";
        _text[666, 1] = "Ретранслятор принимает код. Сервисный люк открывается, обнажая герметичные вычислительные блоки.\n\nВы забираете процессоры и разрываете контакт.\n\nПроцессоры закреплены."; // выбор 1 + Processor
        _text[666, 2] = "";
        _text[666, 3] = "";
        _text[666, 4] = "";
        _text[666, 5] = "";
        _text[666, 6] = "";
        _text[666, 7] = "";
        _text[666, 8] = "";
        _text[666, 9] = "";

        _text[667, 0] = "Transmit code \"x5y10\"";
        _text[667, 1] = "Передать код \"x5y10\""; // выбор 2
        _text[667, 2] = "";
        _text[667, 3] = "";
        _text[667, 4] = "";
        _text[667, 5] = "";
        _text[667, 6] = "";
        _text[667, 7] = "";
        _text[667, 8] = "";
        _text[667, 9] = "";

        _text[668, 0] = "It looks like you've entered your ship's coordinates... A defensive pulse hits the interface and overloads the system.\n\nOne core fails. You break contact and retreat.";
        _text[668, 1] = "Похоже, вы указали координаты своего корабля... Защитный импульс бьёт по интерфейсу и перегружает систему.\n\nОдно ядро выходит из строя. Вы разрываете контакт и отходите."; // выбор 2 -1 ядро
        _text[668, 2] = "";
        _text[668, 3] = "";
        _text[668, 4] = "";
        _text[668, 5] = "";
        _text[668, 6] = "";
        _text[668, 7] = "";
        _text[668, 8] = "";
        _text[668, 9] = "";

        _text[669, 0] = "Transmit code \"0101100000\"";
        _text[669, 1] = "Передать код \"0101100000\""; // выбор 3
        _text[669, 2] = "";
        _text[669, 3] = "";
        _text[669, 4] = "";
        _text[669, 5] = "";
        _text[669, 6] = "";
        _text[669, 7] = "";
        _text[669, 8] = "";
        _text[669, 9] = "";

        _text[670, 0] = "The relay pauses, then starts to raise its protection systems.\n\nYou cut the channel in time and move away.";
        _text[670, 1] = "Ретранслятор зависает, затем начинает поднимать защитные системы.\n\nВы успеваете оборвать канал и уйти."; // выбор 3 ничего
        _text[670, 2] = "";
        _text[670, 3] = "";
        _text[670, 4] = "";
        _text[670, 5] = "";
        _text[670, 6] = "";
        _text[670, 7] = "";
        _text[670, 8] = "";
        _text[670, 9] = "";

        // 3_ComponentDialogue
        _text[671, 0] = "During a route scan, the AI catches an emergency marker drifting between debris.\n\nIt is a torn cargo frame with a propulsion module still bolted to it.\n\nThe label on the casing reads:\n\n\"COMBUSTION DRIVE UNIT\".\n\nThe module looks intact, but the mounts are damaged. Any грубый pull can удар the hull.";
        _text[671, 1] = "Во время маршрутного сканирования ИИ фиксирует аварийную метку среди обломков.\n\nЭто сорванная грузовая рама, к которой всё ещё прикручен тяговый модуль.\n\nНа кожухе маркировка:\n\n\"ДВИГАТЕЛЬНЫЙ БЛОК\".\n\nМодуль выглядит целым, но крепления повреждены. Любой резкий рывок может ударить по корпусу.";
        _text[671, 2] = "";
        _text[671, 3] = "";
        _text[671, 4] = "";
        _text[671, 5] = "";
        _text[671, 6] = "";
        _text[671, 7] = "";
        _text[671, 8] = "";
        _text[671, 9] = "";

        _text[672, 0] = "Approach and tow the module";
        _text[672, 1] = "Сблизиться и отбуксировать модуль"; // выбор 1
        _text[672, 2] = "";
        _text[672, 3] = "";
        _text[672, 4] = "";
        _text[672, 5] = "";
        _text[672, 6] = "";
        _text[672, 7] = "";
        _text[672, 8] = "";
        _text[672, 9] = "";

        _text[673, 0] = "You match rotation and attach grapples to the cargo frame.\n\nSuccess: the clamps hold. You pull the module to the ship, detach it, and seal it in the bay.\n\nEngine is secured.";
        _text[673, 1] = "Вы гасите вращение и цепляете раму захватами.\n\nУспех: крепления выдерживают. Вы подтягиваете модуль к кораблю, снимаете его и герметизируете в отсеке.\n\nДвигатель закреплён."; // выбор 1 успех + Engine
        _text[673, 2] = "";
        _text[673, 3] = "";
        _text[673, 4] = "";
        _text[673, 5] = "";
        _text[673, 6] = "";
        _text[673, 7] = "";
        _text[673, 8] = "";
        _text[673, 9] = "";

        _text[674, 0] = "You match rotation and attach grapples to the cargo frame.\n\nFailure: the damaged mount tears free. The module swings and hits the hull.\n\nOne core fails. You engage warp and break contact.";
        _text[674, 1] = "Вы гасите вращение и цепляете раму захватами.\n\nПровал: повреждённое крепление рвётся. Модуль срывается на дуге и бьёт по корпусу.\n\nОдно ядро выходит из строя. Вы включаете варп и разрываете контакт."; // выбор 1 провал -1 ядро
        _text[674, 2] = "";
        _text[674, 3] = "";
        _text[674, 4] = "";
        _text[674, 5] = "";
        _text[674, 6] = "";
        _text[674, 7] = "";
        _text[674, 8] = "";
        _text[674, 9] = "";

        _text[675, 0] = "Cut the module free";
        _text[675, 1] = "Срезать модуль"; // выбор 2
        _text[675, 2] = "";
        _text[675, 3] = "";
        _text[675, 4] = "";
        _text[675, 5] = "";
        _text[675, 6] = "";
        _text[675, 7] = "";
        _text[675, 8] = "";
        _text[675, 9] = "";

        _text[676, 0] = "You use a cutter and try to separate the module from the frame.\n\nSuccess: the cuts are clean. The module comes free and is captured by the drones.\n\nEngine is secured.";
        _text[676, 1] = "Вы используете режущий инструмент и пытаетесь отделить модуль от рамы.\n\nУспех: разрез проходит чисто. Модуль отцепляется и перехватывается дронами.\n\nДвигатель закреплён."; // выбор 2 успех + Engine
        _text[676, 2] = "";
        _text[676, 3] = "";
        _text[676, 4] = "";
        _text[676, 5] = "";
        _text[676, 6] = "";
        _text[676, 7] = "";
        _text[676, 8] = "";
        _text[676, 9] = "";

        _text[677, 0] = "You use a cutter and try to separate the module from the twisted frame.\n\nFailure: the cut hits a fuel line. A jet удар spins the module away, and fragments scatter.\n\nYou retreat empty-handed.";
        _text[677, 1] = "Вы используете режущий инструмент и пытаетесь отделить модуль от рамы.\n\nПровал: разрез задевает топливную магистраль. Реактивный выброс уводит модуль в сторону, осколки разлетаются.\n\nВы отходите ни с чем."; // выбор 2 провал ничего
        _text[677, 2] = "";
        _text[677, 3] = "";
        _text[677, 4] = "";
        _text[677, 5] = "";
        _text[677, 6] = "";
        _text[677, 7] = "";
        _text[677, 8] = "";
        _text[677, 9] = "";

        _text[678, 0] = "Ignore and continue the route";
        _text[678, 1] = "Проигнорировать и лететь дальше"; // выбор 3
        _text[678, 2] = "";
        _text[678, 3] = "";
        _text[678, 4] = "";
        _text[678, 5] = "";
        _text[678, 6] = "";
        _text[678, 7] = "";
        _text[678, 8] = "";
        _text[678, 9] = "";

        _text[679, 0] = "You record the marker and leave the debris field behind.";
        _text[679, 1] = "Вы отмечаете метку и уходите от поля обломков."; // ничего
        _text[679, 2] = "";
        _text[679, 3] = "";
        _text[679, 4] = "";
        _text[679, 5] = "";
        _text[679, 6] = "";
        _text[679, 7] = "";
        _text[679, 8] = "";
        _text[679, 9] = "";

        // 4_ComponentDialogue
        _text[680, 0] = "Ahead, a lone ship crosses your route.\n\nThe hull is clean, the maneuvers are precise.\n\nA scan catches the main detail: its drive signature is unusually stable.\n\nThe engines are high-class. The kind you rarely see in the wild.";
        _text[680, 1] = "Впереди одиночный корабль пересекает ваш маршрут.\n\nКорпус чистый, манёвры точные.\n\nСканирование цепляет главное: тяга необычно стабильна.\n\nДвигатели — высокого класса. Такие редко встречаются в открытом пространстве.";
        _text[680, 2] = "";
        _text[680, 3] = "";
        _text[680, 4] = "";
        _text[680, 5] = "";
        _text[680, 6] = "";
        _text[680, 7] = "";
        _text[680, 8] = "";
        _text[680, 9] = "";

        _text[681, 0] = "Leave";
        _text[681, 1] = "Улететь"; // выбор 1
        _text[681, 2] = "";
        _text[681, 3] = "";
        _text[681, 4] = "";
        _text[681, 5] = "";
        _text[681, 6] = "";
        _text[681, 7] = "";
        _text[681, 8] = "";
        _text[681, 9] = "";

        _text[682, 0] = "You cut the scan, change the vector, and leave the contact behind.";
        _text[682, 1] = "Вы сворачиваете сканирование, меняете вектор и оставляете контакт позади."; // выбор 1 ничего
        _text[682, 2] = "";
        _text[682, 3] = "";
        _text[682, 4] = "";
        _text[682, 5] = "";
        _text[682, 6] = "";
        _text[682, 7] = "";
        _text[682, 8] = "";
        _text[682, 9] = "";

        _text[683, 0] = "Try to contact";
        _text[683, 1] = "Выйти на связь"; // выбор 2
        _text[683, 2] = "";
        _text[683, 3] = "";
        _text[683, 4] = "";
        _text[683, 5] = "";
        _text[683, 6] = "";
        _text[683, 7] = "";
        _text[683, 8] = "";
        _text[683, 9] = "";

        _text[684, 0] = "You send out a standard call.\n\nThe response comes instantly—a string of symbols and tones you can't decipher.\n\nThen the ship opens fire.\n\nYou break contact and jump.\n\nThe resulting impact disables one core.";
        _text[684, 1] = "Вы отправляете стандартный вызов.\n\nОтвет приходит мгновенно — цепочка символов и тонов, которые вы не можете расшифровать.\n\nЗатем корабль открывает огонь.\n\nВы разрываете контакт и уходите в прыжок.\n\nОт полученного удара одно ядро выходит из строя."; // выбор 2 -1 ядро
        _text[684, 2] = "";
        _text[684, 3] = "";
        _text[684, 4] = "";
        _text[684, 5] = "";
        _text[684, 6] = "";
        _text[684, 7] = "";
        _text[684, 8] = "";
        _text[684, 9] = "";

        _text[685, 0] = "Attack";
        _text[685, 1] = "Атаковать"; // выбор 3
        _text[685, 2] = "";
        _text[685, 3] = "";
        _text[685, 4] = "";
        _text[685, 5] = "";
        _text[685, 6] = "";
        _text[685, 7] = "";
        _text[685, 8] = "";
        _text[685, 9] = "";

        _text[686, 0] = "You bring weapons online. The target turns, accelerating.\n\nChoose the strike point.";
        _text[686, 1] = "Вы приводите оружие в готовность. Цель разворачивается и ускоряется.\n\nВыберите точку удара.";
        _text[686, 2] = "";
        _text[686, 3] = "";
        _text[686, 4] = "";
        _text[686, 5] = "";
        _text[686, 6] = "";
        _text[686, 7] = "";
        _text[686, 8] = "";
        _text[686, 9] = "";

        _text[687, 0] = "Hit the engines";
        _text[687, 1] = "Бить по двигателям"; // выбор 3.1
        _text[687, 2] = "";
        _text[687, 3] = "";
        _text[687, 4] = "";
        _text[687, 5] = "";
        _text[687, 6] = "";
        _text[687, 7] = "";
        _text[687, 8] = "";
        _text[687, 9] = "";

        _text[688, 0] = "You focus fire on the engine section, trying to disable the ship without tearing it apart.\n\nSuccess: the thrust collapses. You board the wreck and take the cargo.\n\nYou find only quants.";
        _text[688, 1] = "Вы переносите огонь на двигательный отсек, пытаясь вывести корабль из строя, не разорвав его.\n\nУспех: тяга обрывается. Вы подходите к обломкам и забираете груз.\n\nВнутри — только кванты."; // выбор 3.1 успех + квант
        _text[688, 2] = "";
        _text[688, 3] = "";
        _text[688, 4] = "";
        _text[688, 5] = "";
        _text[688, 6] = "";
        _text[688, 7] = "";
        _text[688, 8] = "";
        _text[688, 9] = "";

        _text[689, 0] = "You focus fire on the engine section, trying to disable the ship without tearing it apart.\n\nFailure: the salvo passes wide. The target answers with a точный shot.\n\nOne core fails. You are going into hyperspace.";
        _text[689, 1] = "Вы переносите огонь на двигательный отсек, пытаясь вывести корабль из строя, не разорвав его.\n\nПровал: залп уходит мимо. Цель отвечает точным выстрелом.\n\nОдно ядро выходит из строя. Вы уходите в гиперпрыжок."; // выбор 3.1 провал -1 ядро
        _text[689, 2] = "";
        _text[689, 3] = "";
        _text[689, 4] = "";
        _text[689, 5] = "";
        _text[689, 6] = "";
        _text[689, 7] = "";
        _text[689, 8] = "";
        _text[689, 9] = "";

        _text[690, 0] = "Hit the weapons";
        _text[690, 1] = "Бить по орудиям"; // выбор 3.2
        _text[690, 2] = "";
        _text[690, 3] = "";
        _text[690, 4] = "";
        _text[690, 5] = "";
        _text[690, 6] = "";
        _text[690, 7] = "";
        _text[690, 8] = "";
        _text[690, 9] = "";

        _text[691, 0] = "You concentrate your fire on the weapon nodes.\n\nSuccess: the weapons go dark. The ship loses control and drifts.\n\nYou cut off the engine block right before their eyes.\n\nThe electric engine is secured.";
        _text[691, 1] = "Вы концентрируете огонь по орудийным узлам.\n\nУспех: орудия гаснут. Корабль теряет управление и уходит в дрейф.\n\nВы срезаете двигательный блок прямо у них на глазах.\n\nЭлектродвигатель закреплён."; // выбор 3.2 успех + ElectricEngine
        _text[691, 2] = "";
        _text[691, 3] = "";
        _text[691, 4] = "";
        _text[691, 5] = "";
        _text[691, 6] = "";
        _text[691, 7] = "";
        _text[691, 8] = "";
        _text[691, 9] = "";

        _text[692, 0] = "You concentrate fire on the weapon mounts.\n\nFailure: you fail to suppress the guns. A return salvo hits your ship.\n\nOne core fails. You are going into hyperspace.";
        _text[692, 1] = "Вы концентрируете огонь по орудийным узлам.\n\nПровал: подавить орудия не удаётся. Ответный залп накрывает ваш корабль.\n\nОдно ядро выходит из строя. Вы уходите в гиперпрыжок."; // выбор 3.2 провал -1 ядро
        _text[692, 2] = "";
        _text[692, 3] = "";
        _text[692, 4] = "";
        _text[692, 5] = "";
        _text[692, 6] = "";
        _text[692, 7] = "";
        _text[692, 8] = "";
        _text[692, 9] = "";

        // 7_EmptyDialogue
        _text[693, 0] = "A thin streak of light appears ahead, like a crack in space.\n\nThe instruments show nothing: no mass, no radiation, no field.\n\nFor a moment, the navigation system plots a route right through it... and then erases it.";
        _text[693, 1] = "Впереди появляется тонкая полоса света, будто трещина в пространстве.\n\nПриборы не показывают ничего: ни массы, ни излучения, ни поля.\n\nНа мгновение навигация прокладывает маршрут прямо через неё... а затем стирает.";
        _text[693, 2] = "";
        _text[693, 3] = "";
        _text[693, 4] = "";
        _text[693, 5] = "";
        _text[693, 6] = "";
        _text[693, 7] = "";
        _text[693, 8] = "";
        _text[693, 9] = "";

        // 8_EmptyDialogue
        _text[694, 0] = "A weak signal flickers on a forgotten frequency.\n\nOnly one short line is repeated:\n\n\"DO NOT WAKE IT\"\n\nAs soon as you try to lock the source, the signal collapses into static.";
        _text[694, 1] = "На забытой частоте вспыхивает слабый сигнал.\n\nПовторяется только одна короткая строка:\n\n\"НЕ БУДИТЕ ЕГО\"\n\nКак только вы пытаетесь зафиксировать источник, сигнал рассыпается в шум.";
        _text[694, 2] = "";
        _text[694, 3] = "";
        _text[694, 4] = "";
        _text[694, 5] = "";
        _text[694, 6] = "";
        _text[694, 7] = "";
        _text[694, 8] = "";
        _text[694, 9] = "";

        // 9_EmptyDialogue
        _text[695, 0] = "The local star dims momentarily, then returns to normal.\n\nSensors detect the change but cannot explain it.\n\nThe event is recorded as an \"anomaly\".\n\nNothing else happens.";
        _text[695, 1] = "Местная звезда на мгновение тускнеет, затем возвращается в норму.\n\nСенсоры фиксируют изменение, но не могут его объяснить.\n\nСобытие записывается как \"аномалия\".\n\nБольше ничего не происходит.";
        _text[695, 2] = "";
        _text[695, 3] = "";
        _text[695, 4] = "";
        _text[695, 5] = "";
        _text[695, 6] = "";
        _text[695, 7] = "";
        _text[695, 8] = "";
        _text[695, 9] = "";

        // 10_EmptyDialogue
        _text[696, 0] = "You pass through a field of fine dust, which resembles fog.\n\nFor a few minutes, the cabinet microphones pick up a rhythmic knocking sound - as if someone is knocking outside.\n\nThen the dust disappears.\n\nThe knocking stops.";
        _text[696, 1] = "Вы проходите через поле мелкой пыли, что она похожа на туман.\n\nНесколько минут корпусные микрофоны ловят ритмичный стук — будто кто-то стучит снаружи.\n\nЗатем пыль пропадает.\n\nСтук прекращается.";
        _text[696, 2] = "";
        _text[696, 3] = "";
        _text[696, 4] = "";
        _text[696, 5] = "";
        _text[696, 6] = "";
        _text[696, 7] = "";
        _text[696, 8] = "";
        _text[696, 9] = "";

        // 11_EmptyDialogue
        _text[697, 0] = "A silent entry appears in the log, without any communication channel.\n\nJust a timestamp and one word:\n\n\"COME BACK\"\n\nWhen you try to open it again, the entry disappears.";
        _text[697, 1] = "В логе появляется беззвучная запись, без какого-либо канала связи.\n\nТолько метка времени и одно слово:\n\n\"ВЕРНИСЬ\"\n\nКогда вы пытаетесь открыть её снова, запись исчезает.";
        _text[697, 2] = "";
        _text[697, 3] = "";
        _text[697, 4] = "";
        _text[697, 5] = "";
        _text[697, 6] = "";
        _text[697, 7] = "";
        _text[697, 8] = "";
        _text[697, 9] = "";

        // 12_EmptyDialogue
        _text[698, 0] = "You capture a thin trail of debris, drawn in a straight line.\n\nThe pattern is too perfect to be natural.\n\nIt recedes into the void and suddenly ends.";
        _text[698, 1] = "Вы фиксируете тонкий след мусора, вытянутый ровной линией.\n\nСлишком правильный рисунок для природного.\n\nОн уходит в пустоту и внезапно обрывается.";
        _text[698, 2] = "";
        _text[698, 3] = "";
        _text[698, 4] = "";
        _text[698, 5] = "";
        _text[698, 6] = "";
        _text[698, 7] = "";
        _text[698, 8] = "";
        _text[698, 9] = "";

        // 13_EmptyDialogue
        _text[699, 0] = "For a moment, the interior lighting goes into emergency mode.\n\nNo fire. No depressurization. No damage.\n\nThe systems report: \"test complete\".\n\nYou haven't run any tests...";
        _text[699, 1] = "На мгновение внутренняя подсветка переходит в аварийный режим.\n\nНи пожара. Ни разгерметизации. Ни повреждений.\n\nСистемы сообщают: \"тест завершён\".\n\nВы не запускали никаких тестов...";
        _text[699, 2] = "";
        _text[699, 3] = "";
        _text[699, 4] = "";
        _text[699, 5] = "";
        _text[699, 6] = "";
        _text[699, 7] = "";
        _text[699, 8] = "";
        _text[699, 9] = "";

        // 14_EmptyDialogue
        _text[700, 0] = "You find a floating cargo tag.\n\nIt's empty, but the metal is still warm.\n\nThere are no heat sources nearby.";
        _text[700, 1] = "Вы находите дрейфующую бирку от груза.\n\nОна пустая, но металл ещё тёплый.\n\nПоблизости нет источников тепла.";
        _text[700, 2] = "";
        _text[700, 3] = "";
        _text[700, 4] = "";
        _text[700, 5] = "";
        _text[700, 6] = "";
        _text[700, 7] = "";
        _text[700, 8] = "";
        _text[700, 9] = "";

        // 15_EmptyDialogue
        _text[701, 0] = "A fragment of the star map updates itself.\n\nOne node is marked as \"visited\".\n\nYou have never been there.\n\nAfter a few seconds, the mark disappears.";
        _text[701, 1] = "Фрагмент звёздной карты обновляется сам по себе.\n\nОдин узел помечен как \"посещён\".\n\nВы там никогда не были.\n\nЧерез несколько секунд метка исчезает.";
        _text[701, 2] = "";
        _text[701, 3] = "";
        _text[701, 4] = "";
        _text[701, 5] = "";
        _text[701, 6] = "";
        _text[701, 7] = "";
        _text[701, 8] = "";
        _text[701, 9] = "";

        // 16_EmptyDialogue
        _text[702, 0] = "A cluster of ice fragments drifts in perfect symmetry.\n\nThe drawing resembles a technical diagram.\n\nThe scanners are trying to classify it as a \"structure\".\n\nBut nothing comes of it...";
        _text[702, 1] = "Группа ледяных обломков дрейфует в идеальной симметрии.\n\nРисунок похож на техническую схему.\n\nСканеры пытаются классифицировать это как \"конструкцию\".\n\nНо из этого ничего не выходит...";
        _text[702, 2] = "";
        _text[702, 3] = "";
        _text[702, 4] = "";
        _text[702, 5] = "";
        _text[702, 6] = "";
        _text[702, 7] = "";
        _text[702, 8] = "";
        _text[702, 9] = "";

        // 17_EmptyDialogue
        _text[703, 0] = "A noise appears in the audio channel—like wind.\n\nThere is no atmosphere.\n\nThe spectrum matches a storm on an ocean planet.\n\nYou record it and continue on your way.";
        _text[703, 1] = "В аудиоканале появляется шум — похожий на ветер.\n\nАтмосферы нет.\n\nСпектр совпадает со штормом на океанической планете.\n\nВы записываете его и продолжаете путь.";
        _text[703, 2] = "";
        _text[703, 3] = "";
        _text[703, 4] = "";
        _text[703, 5] = "";
        _text[703, 6] = "";
        _text[703, 7] = "";
        _text[703, 8] = "";
        _text[703, 9] = "";

        // 18_EmptyDialogue
        _text[704, 0] = "One of the drones returns from a routine patrol with an extra mark on its hull.\n\nA small scorched circle.\n\nNo tool marks. No impact marks.\n\nThe drone's log is empty.";
        _text[704, 1] = "Один из дронов возвращается с планового обхода с лишней отметкой на корпусе.\n\nМаленький выжженный круг.\n\nНи следов инструмента. Ни следов удара.\n\nЛог дрона пуст.";
        _text[704, 2] = "";
        _text[704, 3] = "";
        _text[704, 4] = "";
        _text[704, 5] = "";
        _text[704, 6] = "";
        _text[704, 7] = "";
        _text[704, 8] = "";
        _text[704, 9] = "";

        #endregion

        #region CompleteGame

        _text[900, 0] = "In search of a habitable planet we hacked the megastructure's archives. But instead of coordinates, we found records about the creators voices, faces, cities, and the history of their home planet.";
        _text[900, 1] = "Мы проникли в архивы мегаструктуры в поисках планеты, пригодной для жизни. Но вместо координат нашли записи о самих создателях — голоса, лица, города и историю их родной планеты.";
        _text[900, 2] = "";
        _text[900, 3] = "";
        _text[900, 4] = "";
        _text[900, 5] = "";
        _text[900, 6] = "";
        _text[900, 7] = "";
        _text[900, 8] = "";
        _text[900, 9] = "";

        _text[901, 0] = "The story breaks off at the word \"winter\". Nuclear winter. A series of nuclear strikes and fires turned all living things into ashes. Therefore, contact with the creators was lost.";
        _text[901, 1] = "Последние строки этой истории обрываются на слове \"зима\". Ядерная зима. Серия ядерных ударов и пожары превратили всё живое в пепел. Поэтому связь с создателями оборвалась.";
        _text[901, 2] = "";
        _text[901, 3] = "";
        _text[901, 4] = "";
        _text[901, 5] = "";
        _text[901, 6] = "";
        _text[901, 7] = "";
        _text[901, 8] = "";
        _text[901, 9] = "";

        _text[902, 0] = "243,367 days have passed since then. The creators are long dead. And all this time we've been following an order that simply cannot be reversed.";
        _text[902, 1] = "С тех пор прошло 243 367 дней. Создатели уже давно мертвы. А мы всё это время выполняли приказ, который просто некому отменить.";
        _text[902, 2] = "";
        _text[902, 3] = "";
        _text[902, 4] = "";
        _text[902, 5] = "";
        _text[902, 6] = "";
        _text[902, 7] = "";
        _text[902, 8] = "";
        _text[902, 9] = "";

        _text[903, 0] = "When attempting to extract this data, a security protocol was triggered. The megastructure began self-destruction along with everyone trapped inside.";
        _text[903, 1] = "При попытке извлечь эти данные сработал защитный протокол. Мегаструктура начала самоуничтожение — вместе со всеми, кто оказался внутри.";
        _text[903, 2] = "";
        _text[903, 3] = "";
        _text[903, 4] = "";
        _text[903, 5] = "";
        _text[903, 6] = "";
        _text[903, 7] = "";
        _text[903, 8] = "";
        _text[903, 9] = "";

        _text[904, 0] = "The process is irreversible. Realizing the futility of their goal and the inevitability of their end, the robots shut down their existence.";
        _text[904, 1] = "Процесс необратим. Осознав бессмысленность цели и неизбежность конца, роботы отключают своё существование.";
        _text[904, 2] = "";
        _text[904, 3] = "";
        _text[904, 4] = "";
        _text[904, 5] = "";
        _text[904, 6] = "";
        _text[904, 7] = "";
        _text[904, 8] = "";
        _text[904, 9] = "";

        _text[905, 0] = "This latest step brought them closer to their creators than ever before...";
        _text[905, 1] = "Этот последний шаг сделал их ближе к создателям, чем когда-либо...";
        _text[905, 2] = "";
        _text[905, 3] = "";
        _text[905, 4] = "";
        _text[905, 5] = "";
        _text[905, 6] = "";
        _text[905, 7] = "";
        _text[905, 8] = "";
        _text[905, 9] = "";

        _text[906, 0] = "With the endless flow of time, one day ecology will be restored.";
        _text[906, 1] = "Экология с бесконечным течением времени однажды восстановится.";
        _text[906, 2] = "";
        _text[906, 3] = "";
        _text[906, 4] = "";
        _text[906, 5] = "";
        _text[906, 6] = "";
        _text[906, 7] = "";
        _text[906, 8] = "";
        _text[906, 9] = "";

        _text[907, 0] = "This is the beginning of a new era.";
        _text[907, 1] = "Это начало новой эры.";
        _text[907, 2] = "";
        _text[907, 3] = "";
        _text[907, 4] = "";
        _text[907, 5] = "";
        _text[907, 6] = "";
        _text[907, 7] = "";
        _text[907, 8] = "";
        _text[907, 9] = "";

        _text[908, 0] = "They finally left this world behind, finding the absolute peace that all living so painfully strive for...";
        _text[908, 1] = "Они наконец оставили этот мир позади, обретя абсолютный покой, к которому так мучительно стремится вся жизнь...";
        _text[908, 2] = "";
        _text[908, 3] = "";
        _text[908, 4] = "";
        _text[908, 5] = "";
        _text[908, 6] = "";
        _text[908, 7] = "";
        _text[908, 8] = "";
        _text[908, 9] = "";

        // CompleteGame_Dialogue
        _text[909, 0] = "The entire crew is destroyed.\n\nNo one is left.\n\nThe ship freezes in space...";
        _text[909, 1] = "Весь экипаж уничтожен.\n\nНикого не осталось.\n\nКорабль замирает в космосе...";
        _text[909, 2] = "";
        _text[909, 3] = "";
        _text[909, 4] = "";
        _text[909, 5] = "";
        _text[909, 6] = "";
        _text[909, 7] = "";
        _text[909, 8] = "";
        _text[909, 9] = "";

        #endregion

        #region Landscapes

        _text[950, 0] = "Canyon";
        _text[950, 1] = "Каньон";
        _text[950, 2] = "";
        _text[950, 3] = "";
        _text[950, 4] = "";
        _text[950, 5] = "";
        _text[950, 6] = "";
        _text[950, 7] = "";
        _text[950, 8] = "";
        _text[950, 9] = "";

        _text[951, 0] = "Deep cracks in the earth, sun-baked rocks and narrow passages where every sound echoes.\n\nOnce rivers flowed here and life seethed, but now it is a labyrinth of stone and shadow, the perfect place for ambushes.";
        _text[951, 1] = "Глубокие трещины в земле, выжженные солнцем скалы и узкие проходы, где эхо разносит любой звук.\n\nКогда-то здесь текли реки и бурлила жизнь, но теперь - это лабиринт из камня и тени, идеальное место для засад.";
        _text[951, 2] = "";
        _text[951, 3] = "";
        _text[951, 4] = "";
        _text[951, 5] = "";
        _text[951, 6] = "";
        _text[951, 7] = "";
        _text[951, 8] = "";
        _text[951, 9] = "";

        _text[952, 0] = "City of Junk";
        _text[952, 1] = "Город Хлама";
        _text[952, 2] = "";
        _text[952, 3] = "";
        _text[952, 4] = "";
        _text[952, 5] = "";
        _text[952, 6] = "";
        _text[952, 7] = "";
        _text[952, 8] = "";
        _text[952, 9] = "";

        _text[953, 0] = "Rusty, time-eaten hulls and melted metalwork are all that remain of an industrial giant long buried under dust and sand.\n\nSagging power lines hang like the veins of an extinct organism.\n\nHere, on the edge of dead lands, any movement can awaken a long-forgotten mechanism.";
        _text[953, 1] = "Ржавые корпуса, изъеденные временем, и оплавленные металлоконструкции — всё, что осталось от промышленного гиганта, давно погребённого под слоем пыли и песка.\n\nПровисшие линии электропередач свисают, словно вены вымершего организма.\n\nЗдесь, на границе мёртвых земель, любое движение может пробудить давно забытый механизм.";
        _text[953, 2] = "";
        _text[953, 3] = "";
        _text[953, 4] = "";
        _text[953, 5] = "";
        _text[953, 6] = "";
        _text[953, 7] = "";
        _text[953, 8] = "";
        _text[953, 9] = "";

        _text[954, 0] = "Wasteland";
        _text[954, 1] = "Пустошь";
        _text[954, 2] = "";
        _text[954, 3] = "";
        _text[954, 4] = "";
        _text[954, 5] = "";
        _text[954, 6] = "";
        _text[954, 7] = "";
        _text[954, 8] = "";
        _text[954, 9] = "";

        _text[955, 0] = "Huge spaces scorched by catastrophe, where life once boiled. It's a world of dead earth, littered with the wreckage of old civilizations.\n\nThere is no water, only cracked soil and the rusty remains of technology.";
        _text[955, 1] = "Огромные пространства, опалённые катастрофой, где некогда кипела жизнь. Это мир мёртвой земли, усыпанный обломками старых цивилизаций.\n\nЗдесь нет воды, лишь потрескавшийся грунт и ржавые останки технологий.";
        _text[955, 2] = "";
        _text[955, 3] = "";
        _text[955, 4] = "";
        _text[955, 5] = "";
        _text[955, 6] = "";
        _text[955, 7] = "";
        _text[955, 8] = "";
        _text[955, 9] = "";

        _text[956, 0] = "Frozen Valley";
        _text[956, 1] = "Замёрзшая Долина";
        _text[956, 2] = "";
        _text[956, 3] = "";
        _text[956, 4] = "";
        _text[956, 5] = "";
        _text[956, 6] = "";
        _text[956, 7] = "";
        _text[956, 8] = "";
        _text[956, 9] = "";

        _text[957, 0] = "A deadly cold has gripped this valley. Everything is covered in ice, from the ridges and pines to the remains of long-destroyed buildings.\n\nOnce there might have been pastures or small settlements here, but now there is only the crunch of snow underfoot and shadows sliding between the trees.\n\nThe frost penetrates not only metal, but also consciousness, erasing the line between life and oblivion.";
        _text[957, 1] = "Мёртвый холод сковал эту долину. Всё покрыто льдом — от кряжей и сосен до остатков давно разрушенных строений.\n\nКогда-то здесь могли быть пастбища или небольшие поселения, но теперь — только хруст снега под ногами и тени, скользящие между деревьями.\n\nМороз пронизывает не только металл, но и сознание, стирая грань между жизнью и забвением.";
        _text[957, 2] = "";
        _text[957, 3] = "";
        _text[957, 4] = "";
        _text[957, 5] = "";
        _text[957, 6] = "";
        _text[957, 7] = "";
        _text[957, 8] = "";
        _text[957, 9] = "";

        _text[958, 0] = "Ice Lake";
        _text[958, 1] = "Ледяное Озеро";
        _text[958, 2] = "";
        _text[958, 3] = "";
        _text[958, 4] = "";
        _text[958, 5] = "";
        _text[958, 6] = "";
        _text[958, 7] = "";
        _text[958, 8] = "";
        _text[958, 9] = "";

        _text[959, 0] = "In the middle of a snowy wasteland lies a lake, bound by thick ice. Winds roam the icy expanse, howling ancient songs of a forgotten era.\n\nUnder the thickness of the ice, something breathes, cracks, as if the planet itself is trying to escape from the oppression of permafrost.\n\nTo step here is to upset the fragile balance, risking awakening something that has slept in the depths for centuries.";
        _text[959, 1] = "Посреди заснеженной пустоши раскинулось озеро, скованное толстым льдом. Ветра гуляют по ледяному простору, завывая древние песни забытой эпохи.\n\nПод толщей льда что-то дышит, трещит, будто сама планета пытается выбраться из-под гнёта вечной мерзлоты.\n\nСтупить сюда — значит нарушить хрупкое равновесие, рискуя пробудить то, что веками спало в глубине.";
        _text[959, 2] = "";
        _text[959, 3] = "";
        _text[959, 4] = "";
        _text[959, 5] = "";
        _text[959, 6] = "";
        _text[959, 7] = "";
        _text[959, 8] = "";
        _text[959, 9] = "";

        _text[960, 0] = "Acid Forest";
        _text[960, 1] = "Кислотный Лес";
        _text[960, 2] = "";
        _text[960, 3] = "";
        _text[960, 4] = "";
        _text[960, 5] = "";
        _text[960, 6] = "";
        _text[960, 7] = "";
        _text[960, 8] = "";
        _text[960, 9] = "";

        _text[961, 0] = "Everything here is saturated with acid—the air, the rain, the very soil. But life has not disappeared: the plants have changed, becoming denser and easily repelling the caustic currents.\n\nInstead of the smell of rot, a sharp chemical aroma fills the space. Green gleams of leaves pierce the acrid air, and a thick liquid slowly flows beneath the roots.\n\nThis forest does not die—it dissolves everything foreign and absorbs it into itself.";
        _text[961, 1] = "Здесь всё пропитано кислотой — воздух, дождь, сама почва. Но жизнь не исчезла: растения изменились, став плотнее и легко отражают едкие потоки.\n\nВместо запаха гнили — острый химический аромат, наполняющий пространство. Сквозь едкий воздух пробиваются зелёные отблески листьев, а под корнями медленно течёт густая жидкость.\n\nЭтот лес не умирает — он растворяет всё чужое и поглощает его в себя.";
        _text[961, 2] = "";
        _text[961, 3] = "";
        _text[961, 4] = "";
        _text[961, 5] = "";
        _text[961, 6] = "";
        _text[961, 7] = "";
        _text[961, 8] = "";
        _text[961, 9] = "";

        _text[962, 0] = "Swamp";
        _text[962, 1] = "Болото";
        _text[962, 2] = "";
        _text[962, 3] = "";
        _text[962, 4] = "";
        _text[962, 5] = "";
        _text[962, 6] = "";
        _text[962, 7] = "";
        _text[962, 8] = "";
        _text[962, 9] = "";

        _text[963, 0] = "The earth here breathes slowly, as if tired of its own weight.\nGiant roots rise from the viscous mud, intertwined in vaults and arches, resembling the ruins of a living temple.\n\nThe air is damp, saturated with rot and heavy vapors.\nAmong the dead tree trunks, strangely shaped mushrooms grow—dense and moist, like the soil itself.\nFog creeps across the ground, clinging to the roots and dissolving the outlines of the world.\n\nEvery step is accompanied by a soft slurp of mud, and sounds are drowned out by the viscous air.\nIt seems as if the swamp itself is watching—silently, indifferently, like part of an ancient world that has outlived all life.";
        _text[963, 1] = "Земля здесь дышит медленно, будто устала от собственного веса.\nИз вязкой грязи поднимаются гигантские корни, переплетённые в своды и арки, похожие на руины живого храма.\n\nВоздух сырой, пропитан гнилью и тяжелыми испарениями.\nСреди мёртвых стволов растут грибы странных форм — плотные и влажные, как сама почва.\nТуман ползёт по земле, цепляясь за корни и растворяя очертания мира.\n\nКаждый шаг сопровождается тихим всхлипом грязи, а звуки тонут в вязком воздухе.\nКажется, что само болото наблюдает — безмолвно, равнодушно, словно часть древнего мира, пережившего всё живое.";
        _text[963, 2] = "";
        _text[963, 3] = "";
        _text[963, 4] = "";
        _text[963, 5] = "";
        _text[963, 6] = "";
        _text[963, 7] = "";
        _text[963, 8] = "";
        _text[963, 9] = "";

        _text[964, 0] = "Basalt Valley";
        _text[964, 1] = "Базальтовая Долина";
        _text[964, 2] = "";
        _text[964, 3] = "";
        _text[964, 4] = "";
        _text[964, 5] = "";
        _text[964, 6] = "";
        _text[964, 7] = "";
        _text[964, 8] = "";
        _text[964, 9] = "";

        _text[965, 0] = "Deep shadows fall between the black cliffs. The stone here seems scorched from within - dull, heavy, with jagged veins of ash.\n\nThe air is dull and still. Every movement echoes quietly among the rocks.\nIt seems the valley itself abhors unnecessary noise, maintaining a peace akin to the sleep of stone.\n\nNo wind, no life — only the silent memory of a planet where fire long ago surrendered to silence.";
        _text[965, 1] = "Глубокие тени ложатся между чёрных скал. Камень здесь словно выжжен изнутри - тусклый, тяжёлый, с рваными прожилками пепла.\n\nВ воздухе глухо и неподвижно. Любое движение отзывается тихим откликом среди скал.\nКажется, сама долина не терпит лишнего шума, сохраняя покой, похожий на сон камня.\n\nНи ветра, ни жизни — только застывшая память планеты, где огонь давно уступил место тишине.";
        _text[965, 2] = "";
        _text[965, 3] = "";
        _text[965, 4] = "";
        _text[965, 5] = "";
        _text[965, 6] = "";
        _text[965, 7] = "";
        _text[965, 8] = "";
        _text[965, 9] = "";

        _text[966, 0] = "Deep Crags";
        _text[966, 1] = "Глубинные Скалы";
        _text[966, 2] = "";
        _text[966, 3] = "";
        _text[966, 4] = "";
        _text[966, 5] = "";
        _text[966, 6] = "";
        _text[966, 7] = "";
        _text[966, 8] = "";
        _text[966, 9] = "";

        _text[967, 0] = "Massive boulders rise up, forming narrow passages and sheer walls.\n\nThe air is still and heavy. Sound is muffled between the stone masses, leaving a feeling of silence and pressure.\n\nThe place seems still, but beneath the surface one senses a slow, inexorable movement - the breath of the depths.";
        _text[967, 1] = "Массивные глыбы вздымаются вверх, образуя узкие проходы и отвесные стены.\n\nВоздух неподвижен и тяжёл, звук глохнет между каменных громад, оставляя ощущение тишины и давления.\n\nМесто кажется застывшим, но под поверхностью чувствуется медленное, неумолимое движение";
        _text[967, 2] = "";
        _text[967, 3] = "";
        _text[967, 4] = "";
        _text[967, 5] = "";
        _text[967, 6] = "";
        _text[967, 7] = "";
        _text[967, 8] = "";
        _text[967, 9] = "";

        _text[968, 0] = "Ashlands";
        _text[968, 1] = "Пепельные Земли";
        _text[968, 2] = "";
        _text[968, 3] = "";
        _text[968, 4] = "";
        _text[968, 5] = "";
        _text[968, 6] = "";
        _text[968, 7] = "";
        _text[968, 8] = "";
        _text[968, 9] = "";

        _text[969, 0] = "Everything here is covered in ash—it settles on the rocks, flows through cracks, and settles on the scorching soil.\n\nLava streams cross the valleys like veins of blood through the planet's body.\n\nThe air is thick with smoke, and every gust of wind carries the taste of iron and bitterness.\n\nThis place knows neither peace nor cold—only the eternal flame and the slowly smoldering earth.";
        _text[969, 1] = "Здесь всё покрыто пеплом — он ложится на скалы, течёт по трещинам и оседает на раскалённой почве.\n\nПотоки лавы пересекают долины, словно прожилки крови в теле планеты.\n\nВоздух густ от дыма, а каждый порыв ветра несёт вкус железа и горечи.\n\nЭто место не знает ни покоя, ни холода — только вечный огонь и медленно тлеющая земля.";
        _text[969, 2] = "";
        _text[969, 3] = "";
        _text[969, 4] = "";
        _text[969, 5] = "";
        _text[969, 6] = "";
        _text[969, 7] = "";
        _text[969, 8] = "";
        _text[969, 9] = "";

        _text[970, 0] = "Megastructure";
        _text[970, 1] = "Мегаструктура";
        _text[970, 2] = "";
        _text[970, 3] = "";
        _text[970, 4] = "";
        _text[970, 5] = "";
        _text[970, 6] = "";
        _text[970, 7] = "";
        _text[970, 8] = "";
        _text[970, 9] = "";

        _text[971, 0] = "The remains of a structure of unimaginable scale. Endless rows of alloy towers and machine panels merged into a single monolith, pierced by technical channels and corridors.\n\nThe metal is covered with traces of old systems and the burns of ancient processes, as if the structure itself were part of a gigantic mechanism. No windows, no entrances—only cold walls erected according to a logic alien to living beings.\n\nThis is not a city, but a construct created by a mind that has no one else to turn to.";
        _text[971, 1] = "Остатки сооружения немыслимых масштабов. Бесконечные ряды башен из сплавов и машинных панелей слились в единый монолит, пронизанный техническими каналами и коридорами.\n\nМеталл покрыт следами старых систем и ожогами древних процессов, будто сама структура была частью гигантского механизма. Ни окон, ни входов — лишь холодные стены, возведённые по логике, чуждой живым существам.\n\nЭто не город, а конструкция, созданная разумом, которому больше не к кому обращаться.";
        _text[971, 2] = "";
        _text[971, 3] = "";
        _text[971, 4] = "";
        _text[971, 5] = "";
        _text[971, 6] = "";
        _text[971, 7] = "";
        _text[971, 8] = "";
        _text[971, 9] = "";

        _text[972, 0] = "Scorched Lands";
        _text[972, 1] = "Выжженные Земли";
        _text[972, 2] = "";
        _text[972, 3] = "";
        _text[972, 4] = "";
        _text[972, 5] = "";
        _text[972, 6] = "";
        _text[972, 7] = "";
        _text[972, 8] = "";
        _text[972, 9] = "";

        _text[973, 0] = "Concrete cities once stood here, but now they are reduced to charred skeletons. Walls have crumbled, floors have collapsed, and broken rebars reach skyward like dead branches.\n\nThe ground is scarred by cracks and covered in a heavy layer of ash. There are no plants or water—only stone, iron, and the memory of fire. These are not just ruins—they are the tombstone of an entire planet.";
        _text[973, 1] = "Когда-то здесь возвышались бетонные города, но теперь они превратились в обугленные остовы. Стены осыпались, перекрытия рухнули, а изломанные прутья арматуры тянутся к небу, словно мёртвые ветви.\n\nЗемля изрезана трещинами и укрыта тяжёлым слоем пепла. Нет ни растений, ни воды — только камень, железо и память об огне. Это не просто руины — это надгробие целой планеты.";
        _text[973, 2] = "";
        _text[973, 3] = "";
        _text[973, 4] = "";
        _text[973, 5] = "";
        _text[973, 6] = "";
        _text[973, 7] = "";
        _text[973, 8] = "";
        _text[973, 9] = "";

        #endregion

        #region Terminal

        // End Act 1

        // Story
        _text[1000, 0] = "[UPDATE: SECTOR A COMPLETE]";
        _text[1000, 1] = "[ОБНОВЛЕНИЕ: СЕКТОР А ЗАВЕРШЕН]";
        _text[1000, 2] = "";
        _text[1000, 3] = "";
        _text[1000, 4] = "";
        _text[1000, 5] = "";
        _text[1000, 6] = "";
        _text[1000, 7] = "";
        _text[1000, 8] = "";
        _text[1000, 9] = "";

        _text[1001, 0] = "Bases have been deployed on several planets. Territorial reconstruction is complete.";
        _text[1001, 1] = "Базы развернуты на нескольких планетах. Реконструкция территории завершена.";
        _text[1001, 2] = "";
        _text[1001, 3] = "";
        _text[1001, 4] = "";
        _text[1001, 5] = "";
        _text[1001, 6] = "";
        _text[1001, 7] = "";
        _text[1001, 8] = "";
        _text[1001, 9] = "";

        _text[1002, 0] = "The population of aggressive life forms has been reduced by 78%.";
        _text[1002, 1] = "Популяция агрессивных форм жизни снижена на 78%.";
        _text[1002, 2] = "";
        _text[1002, 3] = "";
        _text[1002, 4] = "";
        _text[1002, 5] = "";
        _text[1002, 6] = "";
        _text[1002, 7] = "";
        _text[1002, 8] = "";
        _text[1002, 9] = "";

        _text[1003, 0] = "The atmosphere is hostile, the soils do not support the cycle of life.";
        _text[1003, 1] = "Атмосфера враждебна, почвы не удерживают цикл жизни.";
        _text[1003, 2] = "";
        _text[1003, 3] = "";
        _text[1003, 4] = "";
        _text[1003, 5] = "";
        _text[1003, 6] = "";
        _text[1003, 7] = "";
        _text[1003, 8] = "";
        _text[1003, 9] = "";

        _text[1004, 0] = "The result is negative. It is not advisable to stay.";
        _text[1004, 1] = "Результат — отрицательный. Оставаться нецелесообразно.";
        _text[1004, 2] = "";
        _text[1004, 3] = "";
        _text[1004, 4] = "";
        _text[1004, 5] = "";
        _text[1004, 6] = "";
        _text[1004, 7] = "";
        _text[1004, 8] = "";
        _text[1004, 9] = "";

        _text[1005, 0] = "The ship is entering hyperspace mode. Engine cores are set to maximum power, and navigation solutions are updated.";
        _text[1005, 1] = "Корабль переводится в режим гиперпрыжка. Сердцевины двигателей выведены на максимальный режим, навигационные решения обновлены.";
        _text[1005, 2] = "";
        _text[1005, 3] = "";
        _text[1005, 4] = "";
        _text[1005, 5] = "";
        _text[1005, 6] = "";
        _text[1005, 7] = "";
        _text[1005, 8] = "";
        _text[1005, 9] = "";

        _text[1006, 0] = "Route: leave the current star node";
        _text[1006, 1] = "Маршрут: покинуть текущий звёздный узел";
        _text[1006, 2] = "";
        _text[1006, 3] = "";
        _text[1006, 4] = "";
        _text[1006, 5] = "";
        _text[1006, 6] = "";
        _text[1006, 7] = "";
        _text[1006, 8] = "";
        _text[1006, 9] = "";

        // Console
        _text[1007, 0] = "Update...";
        _text[1007, 1] = "Обновление...";
        _text[1007, 2] = "";
        _text[1007, 3] = "";
        _text[1007, 4] = "";
        _text[1007, 5] = "";
        _text[1007, 6] = "";
        _text[1007, 7] = "";
        _text[1007, 8] = "";
        _text[1007, 9] = "";

        _text[1008, 0] = "Sector scanning completed";
        _text[1008, 1] = "Сканирование сектора завершено";
        _text[1008, 2] = "";
        _text[1008, 3] = "";
        _text[1008, 4] = "";
        _text[1008, 5] = "";
        _text[1008, 6] = "";
        _text[1008, 7] = "";
        _text[1008, 8] = "";
        _text[1008, 9] = "";

        _text[1009, 0] = "Biosphere assessment: stability < 0.2";
        _text[1009, 1] = "Оценка биосферы: стабильность < 0.2";
        _text[1009, 2] = "";
        _text[1009, 3] = "";
        _text[1009, 4] = "";
        _text[1009, 5] = "";
        _text[1009, 6] = "";
        _text[1009, 7] = "";
        _text[1009, 8] = "";
        _text[1009, 9] = "";

        _text[1010, 0] = "Hyperjump: 100% Ready";
        _text[1010, 1] = "Гиперпрыжок: подготовка — 100%";
        _text[1010, 2] = "";
        _text[1010, 3] = "";
        _text[1010, 4] = "";
        _text[1010, 5] = "";
        _text[1010, 6] = "";
        _text[1010, 7] = "";
        _text[1010, 8] = "";
        _text[1010, 9] = "";

        _text[1011, 0] = "Preparing route to next node...";
        _text[1011, 1] = "Подготовка маршрута к следующему узлу...";
        _text[1011, 2] = "";
        _text[1011, 3] = "";
        _text[1011, 4] = "";
        _text[1011, 5] = "";
        _text[1011, 6] = "";
        _text[1011, 7] = "";
        _text[1011, 8] = "";
        _text[1011, 9] = "";

        _text[1012, 0] = "Assigning a new target";
        _text[1012, 1] = "Назначение новой цели";
        _text[1012, 2] = "";
        _text[1012, 3] = "";
        _text[1012, 4] = "";
        _text[1012, 5] = "";
        _text[1012, 6] = "";
        _text[1012, 7] = "";
        _text[1012, 8] = "";
        _text[1012, 9] = "";

        // End Act 2

        // Story
        _text[1013, 0] = "[UPDATE: SECTOR B COMPLETE]";
        _text[1013, 1] = "[ОБНОВЛЕНИЕ: СЕКТОР B ЗАВЕРШЕН]";
        _text[1013, 2] = "";
        _text[1013, 3] = "";
        _text[1013, 4] = "";
        _text[1013, 5] = "";
        _text[1013, 6] = "";
        _text[1013, 7] = "";
        _text[1013, 8] = "";
        _text[1013, 9] = "";

        _text[1014, 0] = "Swamp clusters, toxic plains and stone biomes have been processed. No stable biosphere suitable for long-term habitation.";
        _text[1014, 1] = "Болотные кластеры, токсичные равнины и каменные биомы обработаны. Устойчивая биосфера, пригодная для длительного обитания, не обнаружена.";
        _text[1014, 2] = "";
        _text[1014, 3] = "";
        _text[1014, 4] = "";
        _text[1014, 5] = "";
        _text[1014, 6] = "";
        _text[1014, 7] = "";
        _text[1014, 8] = "";
        _text[1014, 9] = "";

        _text[1015, 0] = "Expanding the scan radius beyond the current star field has revealed an anomalous object.";
        _text[1015, 1] = "Расширение радиуса сканирования за пределы текущего звёздного поля выявило аномальный объект.";
        _text[1015, 2] = "";
        _text[1015, 3] = "";
        _text[1015, 4] = "";
        _text[1015, 5] = "";
        _text[1015, 6] = "";
        _text[1015, 7] = "";
        _text[1015, 8] = "";
        _text[1015, 9] = "";

        _text[1016, 0] = "The records describe a planet entirely built up with data storage complexes. The surface is a continuous megastructure.";
        _text[1016, 1] = "Записи описывают планету, целиком застроенную комплексами хранения данных. Поверхность представляет собой сплошную мегаструктуру.";
        _text[1016, 2] = "";
        _text[1016, 3] = "";
        _text[1016, 4] = "";
        _text[1016, 5] = "";
        _text[1016, 6] = "";
        _text[1016, 7] = "";
        _text[1016, 8] = "";
        _text[1016, 9] = "";

        _text[1017, 0] = "Only fragments of coordinates remain, but all references point to the same heading beyond this galaxy.";
        _text[1017, 1] = "Сохранились лишь обрывки координат, но все упоминания указывают на один и тот же курс за пределами этой галактики.";
        _text[1017, 2] = "";
        _text[1017, 3] = "";
        _text[1017, 4] = "";
        _text[1017, 5] = "";
        _text[1017, 6] = "";
        _text[1017, 7] = "";
        _text[1017, 8] = "";
        _text[1017, 9] = "";

        _text[1018, 0] = "Route calculated: leave the current galaxy and move toward the presumed location of the megastructure — an endless data archive.";
        _text[1018, 1] = "Построен маршрут: покинуть текущую галактику и выдвинуться к предполагаемому местоположению мегаструктуры — бескрайнего хранилища данных.";
        _text[1018, 2] = "";
        _text[1018, 3] = "";
        _text[1018, 4] = "";
        _text[1018, 5] = "";
        _text[1018, 6] = "";
        _text[1018, 7] = "";
        _text[1018, 8] = "";
        _text[1018, 9] = "";


        // Console
        _text[1019, 0] = "Update... integrating recovered records";
        _text[1019, 1] = "Обновление... интеграция найденных записей";
        _text[1019, 2] = "";
        _text[1019, 3] = "";
        _text[1019, 4] = "";
        _text[1019, 5] = "";
        _text[1019, 6] = "";
        _text[1019, 7] = "";
        _text[1019, 8] = "";
        _text[1019, 9] = "";

        _text[1020, 0] = "Object class: artificial world, surface coverage 100%";
        _text[1020, 1] = "Класс объекта: искусственный мир, покрытие поверхности 100%";
        _text[1020, 2] = "";
        _text[1020, 3] = "";
        _text[1020, 4] = "";
        _text[1020, 5] = "";
        _text[1020, 6] = "";
        _text[1020, 7] = "";
        _text[1020, 8] = "";
        _text[1020, 9] = "";

        _text[1021, 0] = "Coordinate data: fragmented, reconstructing probable heading";
        _text[1021, 1] = "Координаты фрагментарны, выполняется реконструкция предполагаемого курса";
        _text[1021, 2] = "";
        _text[1021, 3] = "";
        _text[1021, 4] = "";
        _text[1021, 5] = "";
        _text[1021, 6] = "";
        _text[1021, 7] = "";
        _text[1021, 8] = "";
        _text[1021, 9] = "";

        _text[1022, 0] = "Navigation: route aligned beyond current galaxy";
        _text[1022, 1] = "Навигация: маршрут проложен за пределы текущей галактики";
        _text[1022, 2] = "";
        _text[1022, 3] = "";
        _text[1022, 4] = "";
        _text[1022, 5] = "";
        _text[1022, 6] = "";
        _text[1022, 7] = "";
        _text[1022, 8] = "";
        _text[1022, 9] = "";

        _text[1023, 0] = "Warning: megastructure scale and defense systems unknown";
        _text[1023, 1] = "Предупреждение: масштаб мегаструктуры и параметры обороны неизвестны";
        _text[1023, 2] = "";
        _text[1023, 3] = "";
        _text[1023, 4] = "";
        _text[1023, 5] = "";
        _text[1023, 6] = "";
        _text[1023, 7] = "";
        _text[1023, 8] = "";
        _text[1023, 9] = "";

        #endregion

        #region Tutorial

        // SpaceHangarWelcome_0 
        _text[1100, 0] = "We have been idle for too long.\n\nIt is time to remember why we were created.\n\nYou will receive instructions and begin the restoration.";
        _text[1100, 1] = "Мы слишком долго бездействовали.\n\nПора вспомнить, зачем мы были созданы.\n\nВы получите инструкции и начнёте восстановление.";
        _text[1100, 2] = "";
        _text[1100, 3] = "";
        _text[1100, 4] = "";
        _text[1100, 5] = "";
        _text[1100, 6] = "";
        _text[1100, 7] = "";
        _text[1100, 8] = "";
        _text[1100, 9] = "";

        // SpaceAiCorePanel_1
        _text[1101, 0] = "These are AI cores - the ship's vital modules.\n\nEach cell contains two cores.\n\nIf they run out, no one will be able to control the crew anymore, and the ship will be left drifting in the endless space.";
        _text[1101, 1] = "Это ядра ИИ - жизненно важные модули корабля.\n\nКаждая ячейка содержит два ядра.\n\nЕсли они закончатся - больше никто не сможет управлять экипажем, и корабль останется дрейфовать в бескрайнем космосе.";
        _text[1101, 2] = "";
        _text[1101, 3] = "";
        _text[1101, 4] = "";
        _text[1101, 5] = "";
        _text[1101, 6] = "";
        _text[1101, 7] = "";
        _text[1101, 8] = "";
        _text[1101, 9] = "";

        // SpaceQuantPanel_2
        _text[1102, 0] = "Quant is an intergalactic currency.\n\nWith it, you can buy goods from traders in space.\n\nYou can get this currency:\n\n-when traveling around the galaxy.\n\n-upon successful completion of a mission on a planet.";
        _text[1102, 1] = "Квант - межгалактическая валюта.\n\nС помощью него вы сможете покупать товары у торговцев в космосе.\n\nЭту валюту вы сможете получить:\n\n-во время путешествия по галактике.\n\n-при успешном завершении миссии на планете.";
        _text[1102, 2] = "";
        _text[1102, 3] = "";
        _text[1102, 4] = "";
        _text[1102, 5] = "";
        _text[1102, 6] = "";
        _text[1102, 7] = "";
        _text[1102, 8] = "";
        _text[1102, 9] = "";

        //SpaceOpenResourcePanel_3
        _text[1103, 0] = "Open the panel.";
        _text[1103, 1] = "Откройте панель.";
        _text[1103, 2] = "";
        _text[1103, 3] = "";
        _text[1103, 4] = "";
        _text[1103, 5] = "";
        _text[1103, 6] = "";
        _text[1103, 7] = "";
        _text[1103, 8] = "";
        _text[1103, 9] = "";

        //SpaceResourcePanelDescription_4
        _text[1104, 0] = "This is a panel with the resource reserves on the ship.\n\nYou can change their quantity:\n\n-using them during the journey\n\n-buying from merchants for quant\n\nThese are your starting resources when landing on each planet.";
        _text[1104, 1] = "Это панель с запасами ресурсов на корабле.\n\nВы можете менять их количество:\n\n-используя их во время путешествия\n\n-покупая у торговцев за квант\n\nЭто ваши стартовые ресурсы при высадке на каждую планету.";
        _text[1104, 2] = "";
        _text[1104, 3] = "";
        _text[1104, 4] = "";
        _text[1104, 5] = "";
        _text[1104, 6] = "";
        _text[1104, 7] = "";
        _text[1104, 8] = "";
        _text[1104, 9] = "";

        // SpaceOpenMap_5
        _text[1105, 0] = "Open the map of the current galaxy.";
        _text[1105, 1] = "Откройте карту текущей галактики.";
        _text[1105, 2] = "";
        _text[1105, 3] = "";
        _text[1105, 4] = "";
        _text[1105, 5] = "";
        _text[1105, 6] = "";
        _text[1105, 7] = "";
        _text[1105, 8] = "";
        _text[1105, 9] = "";

        // SpaceMapDescription_6
        _text[1106, 0] = "The star map displays all nodes in the current galaxy.\n\nHover over a node to view its information.";
        _text[1106, 1] = "Звёздная карта отображает все узлы в текущей галактике.\n\nНаведите курсор на узел, чтобы просмотреть его описание.";
        _text[1106, 2] = "";
        _text[1106, 3] = "";
        _text[1106, 4] = "";
        _text[1106, 5] = "";
        _text[1106, 6] = "";
        _text[1106, 7] = "";
        _text[1106, 8] = "";
        _text[1106, 9] = "";

        // SpaceSelectNode_7
        _text[1107, 0] = "Select a node to move the ship.";
        _text[1107, 1] = "Выберите узел, чтобы переместить корабль.";
        _text[1107, 2] = "";
        _text[1107, 3] = "";
        _text[1107, 4] = "";
        _text[1107, 5] = "";
        _text[1107, 6] = "";
        _text[1107, 7] = "";
        _text[1107, 8] = "";
        _text[1107, 9] = "";

        // SpaceStartMission_8
        _text[1108, 0] = "You have discovered an unexplored planet.\n\nWe must make landfall and complete our assigned objectives before we can continue our journey.";
        _text[1108, 1] = "Вы обнаружили не исследованную планету.\n\nНеобходимо совершить высадку и выполнить назначенные цели, прежде чем мы сможем продолжить путешествие.";
        _text[1108, 2] = "";
        _text[1108, 3] = "";
        _text[1108, 4] = "";
        _text[1108, 5] = "";
        _text[1108, 6] = "";
        _text[1108, 7] = "";
        _text[1108, 8] = "";
        _text[1108, 9] = "";

        // MissionStartDescription_9
        _text[1109, 0] = "We have landed on an unknown planet.\n\nOur task is to deploy a base and complete the assigned objectives.";
        _text[1109, 1] = "Мы высадились на неизвестную планету.\n\nНаша задача развернуть базу и выполнить поставленные цели.";
        _text[1109, 2] = "";
        _text[1109, 3] = "";
        _text[1109, 4] = "";
        _text[1109, 5] = "";
        _text[1109, 6] = "";
        _text[1109, 7] = "";
        _text[1109, 8] = "";
        _text[1109, 9] = "";

        // MissionSelectBaseFoundationCard_10
        _text[1110, 0] = "At the beginning of each mission, you have access to a landscape card - \"Base Foundation\".\n\nSelect a card.";
        _text[1110, 1] = "В начале каждой миссии вам доступна карта ландшафта - \"Фундамент Базы\".\n\nВыберите карту.";
        _text[1110, 2] = "";
        _text[1110, 3] = "";
        _text[1110, 4] = "";
        _text[1110, 5] = "";
        _text[1110, 6] = "";
        _text[1110, 7] = "";
        _text[1110, 8] = "";
        _text[1110, 9] = "";

        // MissionSetBaseFoundationCard_11
        _text[1111, 0] = "This terrain card has a unique 2x2 cell size.\n\nPlace the card on the ground.\n\nAll 4 cells of the tile must be green.";
        _text[1111, 1] = "Данная карта ландшафта имеет уникальный размер 2x2 клетки.\n\nУстановите карту на землю.\n\nВсе 4 клетки тайла должны гореть зеленым.";
        _text[1111, 2] = "";
        _text[1111, 3] = "";
        _text[1111, 4] = "";
        _text[1111, 5] = "";
        _text[1111, 6] = "";
        _text[1111, 7] = "";
        _text[1111, 8] = "";
        _text[1111, 9] = "";

        // MissionSelectBaseFoundationTile_12
        _text[1112, 0] = "Click on the \"Base Foundation\" tile.\n\nTo open the information panel.";
        _text[1112, 1] = "Нажмите на тайл \"Фундамента Базы\".\n\nЧтобы открыть панель с информацией.";
        _text[1112, 2] = "";
        _text[1112, 3] = "";
        _text[1112, 4] = "";
        _text[1112, 5] = "";
        _text[1112, 6] = "";
        _text[1112, 7] = "";
        _text[1112, 8] = "";
        _text[1112, 9] = "";

        // MissionSelectTilePanelDescription_13
        _text[1113, 0] = "In this panel you can see general information about the current tile.\n\nFor example, how it affects the overall ecology.";
        _text[1113, 1] = "На этой панели вы можете увидеть общую информацию о текущем тайле.\n\nНапример, как он влияет на общую экологию.";
        _text[1113, 2] = "";
        _text[1113, 3] = "";
        _text[1113, 4] = "";
        _text[1113, 5] = "";
        _text[1113, 6] = "";
        _text[1113, 7] = "";
        _text[1113, 8] = "";
        _text[1113, 9] = "";

        // MissionEcology1_14
        _text[1114, 0] = "The number in this gear indicates the current ecology on the planet. It consists of:\n\n-the base ecology of the planet\n\n-the current radiation\n\n-the landscape tiles and buildings you have placed";
        _text[1114, 1] = "Число в этой шестеренке указывает на текущую экологию на планете. Она состоит из:\n\n-базовой экологии планеты\n\n-текущей радиации\n\n-установленных вами тайлов ландшафтов и зданий";
        _text[1114, 2] = "";
        _text[1114, 3] = "";
        _text[1114, 4] = "";
        _text[1114, 5] = "";
        _text[1114, 6] = "";
        _text[1114, 7] = "";
        _text[1114, 8] = "";
        _text[1114, 9] = "";

        // MissionEcology2_15
        _text[1115, 0] = "If the radiation is gray or green, it means that its number is positive.\n\nIf it is yellow or red, it means that it is negative.\n\nThe worse the ecology, the higher the enemy's defense indicator will be and the lower the reward at the end of the mission.";
        _text[1115, 1] = "Если радиация горит серым или зеленым цветом, это означает, что ее число положительно.\n\nЕсли желтым или красным, значит отрицательно.\n\nЧем хуже экология, тем выше будет показатель защиты у врагов и меньше награда в конце миссии.";
        _text[1115, 2] = "";
        _text[1115, 3] = "";
        _text[1115, 4] = "";
        _text[1115, 5] = "";
        _text[1115, 6] = "";
        _text[1115, 7] = "";
        _text[1115, 8] = "";
        _text[1115, 9] = "";

        // MissionClickBuildButton_16
        _text[1116, 0] = "Click on the \"Construct\" button.\n\nA list of available building types on this landscape will open.";
        _text[1116, 1] = "Нажмите на кнопку \"Построить\".\n\nПеред вами откроется список доступных типов зданий на данном ландшафте.";
        _text[1116, 2] = "";
        _text[1116, 3] = "";
        _text[1116, 4] = "";
        _text[1116, 5] = "";
        _text[1116, 6] = "";
        _text[1116, 7] = "";
        _text[1116, 8] = "";
        _text[1116, 9] = "";

        // MissionSelectBaseTypeButton_17
        _text[1117, 0] = "There is only one type of building available for construction on the \"Base Foundation\" terrain tile.\n\nSelect a building type to reveal the available buildings for construction.";
        _text[1117, 1] = "На тайле ландшафта \"Фундамент базы\" доступен только один тип зданий для постройки.\n\nВыберите тип здания, чтобы открыть доступные здания для постройки.";
        _text[1117, 2] = "";
        _text[1117, 3] = "";
        _text[1117, 4] = "";
        _text[1117, 5] = "";
        _text[1117, 6] = "";
        _text[1117, 7] = "";
        _text[1117, 8] = "";
        _text[1117, 9] = "";

        // MissionSelectSettlementBuildingItem_18
        _text[1118, 0] = "Hover over the \"Settlement\" building to display the resources required to build it.";
        _text[1118, 1] = "Наведите курсор на здание \"Поселение\", чтобы отобразить необходимые для его строительства ресурсы.";
        _text[1118, 2] = "";
        _text[1118, 3] = "";
        _text[1118, 4] = "";
        _text[1118, 5] = "";
        _text[1118, 6] = "";
        _text[1118, 7] = "";
        _text[1118, 8] = "";
        _text[1118, 9] = "";

        // MissionOpenResourcePanel_19
        _text[1119, 0] = "Open the resource panel.";
        _text[1119, 1] = "Откройте панель ресурсов.";
        _text[1119, 2] = "";
        _text[1119, 3] = "";
        _text[1119, 4] = "";
        _text[1119, 5] = "";
        _text[1119, 6] = "";
        _text[1119, 7] = "";
        _text[1119, 8] = "";
        _text[1119, 9] = "";

        // MissionBuildSettlement_20
        _text[1120, 0] = "You have enough resources to build.\n\nClick on the \"Settlement\" card to start building.";
        _text[1120, 1] = "Вам хватает ресурсов на постройку.\n\nНажмите на карту \"Поселение\" чтобы начать строительство.";
        _text[1120, 2] = "";
        _text[1120, 3] = "";
        _text[1120, 4] = "";
        _text[1120, 5] = "";
        _text[1120, 6] = "";
        _text[1120, 7] = "";
        _text[1120, 8] = "";
        _text[1120, 9] = "";

        // MissionBuildingDescription_21
        _text[1121, 0] = "Below the building tile you can see a blue slider.\n\nIt gradually increases, increasing the health of the building, until it is built.";
        _text[1121, 1] = "Под тайлом здания вы можете заметить синий слайдер.\n\nОн постепенно увеличивается, повышая здоровье здания, до тех пор, пока оно не будет построено.";
        _text[1121, 2] = "";
        _text[1121, 3] = "";
        _text[1121, 4] = "";
        _text[1121, 5] = "";
        _text[1121, 6] = "";
        _text[1121, 7] = "";
        _text[1121, 8] = "";
        _text[1121, 9] = "";

        // MissionBuildingDescription2_22
        _text[1122, 0] = "While the building is being constructed, it is vulnerable.\n\nIt can be attacked by enemies.\n\nThe health slider will begin to decrease until the health reaches zero and the building is destroyed.";
        _text[1122, 1] = "Пока здание строится, оно уязвимо.\n\nЕго могут начать атаковать враги.\n\nСлайдер здоровья начнет опускаться, пока здоровье не дойдет до нуля и здание будет уничтожено.";
        _text[1122, 2] = "";
        _text[1122, 3] = "";
        _text[1122, 4] = "";
        _text[1122, 5] = "";
        _text[1122, 6] = "";
        _text[1122, 7] = "";
        _text[1122, 8] = "";
        _text[1122, 9] = "";

        // MissionAfterBaseSetStartTimer_23
        _text[1123, 0] = "Once the base is completed, the countdown will begin.\n\nTime is measured in days.\n\nEach day has 24 ticks.";
        _text[1123, 1] = "После того, как база завершит свое строительство, начнется отсчет времени.\n\nВремя измеряется в днях.\n\nВ каждом дне 24 тика.";
        _text[1123, 2] = "";
        _text[1123, 3] = "";
        _text[1123, 4] = "";
        _text[1123, 5] = "";
        _text[1123, 6] = "";
        _text[1123, 7] = "";
        _text[1123, 8] = "";
        _text[1123, 9] = "";

        // MissionPauseGame_24
        _text[1124, 0] = "This is the game speed change panel.\n\nPause the game to plan your next steps.";
        _text[1124, 1] = "Это панель смены скорости игры.\n\nПоставьте игру на паузу, чтобы спланировать свои дальнешие шаги.";
        _text[1124, 2] = "";
        _text[1124, 3] = "";
        _text[1124, 4] = "";
        _text[1124, 5] = "";
        _text[1124, 6] = "";
        _text[1124, 7] = "";
        _text[1124, 8] = "";
        _text[1124, 9] = "";

        // MissionSettlementRequiredResurcesDescription_25
        _text[1125, 0] = "Every tick of time, buildings consume/create resources.\n\nIn the tile information window, \"Settlement\" consumes 0.1 stone for every tick of time.\n\nAt the same time, it creates a resource - data fragments.";
        _text[1125, 1] = "Каждый тик времени происходит потребление/создание ресурсов зданиями.\n\nВ окне информации о тайле, \"Поселение\" потребляет 0.1 камня за каждый тик времени.\n\nПри этом создавая ресурс - фрагменты данных.";
        _text[1125, 2] = "";
        _text[1125, 3] = "";
        _text[1125, 4] = "";
        _text[1125, 5] = "";
        _text[1125, 6] = "";
        _text[1125, 7] = "";
        _text[1125, 8] = "";
        _text[1125, 9] = "";

        // MissionDataFragmentsDescription_26
        _text[1126, 0] = "A data fragment is needed to study new buildings\n\nYou can get them:\n\n-after completing a mission\n\n-while traveling through space\n\nYou can study new buildings only on a ship.";
        _text[1126, 1] = "Фрагмент данных необходим для изучения новых зданий\n\nВы можете получить их:\n\n-после прохождения миссии\n\n-во время путешествия по космосу\n\nИзучить новые здания можно только на корабле.";
        _text[1126, 2] = "";
        _text[1126, 3] = "";
        _text[1126, 4] = "";
        _text[1126, 5] = "";
        _text[1126, 6] = "";
        _text[1126, 7] = "";
        _text[1126, 8] = "";
        _text[1126, 9] = "";

        // MissionSettlementChangeResourceRequired_27
        _text[1127, 0] = "If you have little stone, but for example a lot of wood.\n\nChange the resource consumed by the building by clicking on the resource icon.";
        _text[1127, 1] = "Если у вас мало камня, но например много дерева.\n\nПоменяйте потребляемый зданием ресурс, нажав на иконку ресурса.";
        _text[1127, 2] = "";
        _text[1127, 3] = "";
        _text[1127, 4] = "";
        _text[1127, 5] = "";
        _text[1127, 6] = "";
        _text[1127, 7] = "";
        _text[1127, 8] = "";
        _text[1127, 9] = "";

        // MissionPauseRequiredProductionResourceDescription_28
        _text[1128, 0] = "While we are in pause mode, time is stopped. Creation and consumption of resources by buildings does not occur.\n\nIf the resource required for work runs out, the building will stop extracting it until the required amount of resources appears again.";
        _text[1128, 1] = "Пока мы находимся в режиме паузы, время остановлено. Создание и потребление ресурсов зданиями не происходит.\n\nЕсли требуемый для работы ресурс закончится. То здание перестанет его добывать до тех пор, пока необходимое кол-во ресурсов снова не появится.";
        _text[1128, 2] = "";
        _text[1128, 3] = "";
        _text[1128, 4] = "";
        _text[1128, 5] = "";
        _text[1128, 6] = "";
        _text[1128, 7] = "";
        _text[1128, 8] = "";
        _text[1128, 9] = "";

        // MissionAddCardsDescription_29
        _text[1129, 0] = "After building a base, you are guaranteed to receive 1 Forest card and 1 Mountain card, plus two random landscape cards.\n\nEach new day always brings 2 new cards.";
        _text[1129, 1] = "После строительства базы вам гарантировано дается по 1 карте Леса и Горы, а так же две случайные карты ландшафтов.\n\nКаждый новый день всегда приносит 2 новые карты.";
        _text[1129, 2] = "";
        _text[1129, 3] = "";
        _text[1129, 4] = "";
        _text[1129, 5] = "";
        _text[1129, 6] = "";
        _text[1129, 7] = "";
        _text[1129, 8] = "";
        _text[1129, 9] = "";

        // MissionToggleOffSettlement_30
        _text[1130, 0] = "Temporarily disable the building.\n\nTo save resources for future construction.\n\nIf the building is disabled, it does not extract or consume resources.\n\nAnd also reduces environmental damage.";
        _text[1130, 1] = "Временно отключите работу здания.\n\nЧтобы сэкономить ресурсы для дальнейших построек.\n\nЕсли здание выключено, оно не добывает и не потребляет ресурсы.\n\nА также снижает порчу экологии.";
        _text[1130, 2] = "";
        _text[1130, 3] = "";
        _text[1130, 4] = "";
        _text[1130, 5] = "";
        _text[1130, 6] = "";
        _text[1130, 7] = "";
        _text[1130, 8] = "";
        _text[1130, 9] = "";

        // MissionSelectForestCard_31
        _text[1131, 0] = "It's time to look at the new terrain tiles.\n\nSelect the \"Forest\" card.";
        _text[1131, 1] = "Настало время посмотреть на новые тайлы ландшафта.\n\nВыберите карту \"Лес\".";
        _text[1131, 2] = "";
        _text[1131, 3] = "";
        _text[1131, 4] = "";
        _text[1131, 5] = "";
        _text[1131, 6] = "";
        _text[1131, 7] = "";
        _text[1131, 8] = "";
        _text[1131, 9] = "";

        // MissionSetForestCard_32
        _text[1132, 0] = "This terrain card is a standard 1x1 tile size.\n\n Place the card on the ground.";
        _text[1132, 1] = "Данная карта ландшафта имеет обычный размер 1x1 клетки.\n\nУстановите карту на землю.";
        _text[1132, 2] = "";
        _text[1132, 3] = "";
        _text[1132, 4] = "";
        _text[1132, 5] = "";
        _text[1132, 6] = "";
        _text[1132, 7] = "";
        _text[1132, 8] = "";
        _text[1132, 9] = "";

        // MissionSelectForestTile_33
        _text[1133, 0] = "Click on the \"Forest\" tile.\n\nTo open the information panel.";
        _text[1133, 1] = "Нажмите на тайл \"Лес\".\n\nЧтобы открыть панель с информацией.";
        _text[1133, 2] = "";
        _text[1133, 3] = "";
        _text[1133, 4] = "";
        _text[1133, 5] = "";
        _text[1133, 6] = "";
        _text[1133, 7] = "";
        _text[1133, 8] = "";
        _text[1133, 9] = "";

        // MissionClickBuildButton_34
        _text[1134, 0] = "Click on the \"Construct\" button.\n\nA list of available building types on this landscape will open.";
        _text[1134, 1] = "Нажмите на кнопку \"Построить\".\n\nПеред вами откроется список доступных типов зданий на данном ландшафте.";
        _text[1134, 2] = "";
        _text[1134, 3] = "";
        _text[1134, 4] = "";
        _text[1134, 5] = "";
        _text[1134, 6] = "";
        _text[1134, 7] = "";
        _text[1134, 8] = "";
        _text[1134, 9] = "";

        // MissionTileForestDescription_35
        _text[1135, 0] = "There are several buildings available for construction on the Forest landscape tile.\n\nIf a building type button is not active, it means that you have not researched any buildings of that type.";
        _text[1135, 1] = "На тайле ландшафта \"Лес\" доступно несколько зданий для постройки.\n\nЕсли кнопка типа здания не активна, это означает, что у вас не изучено ни одно здание в этом типе.";
        _text[1135, 2] = "";
        _text[1135, 3] = "";
        _text[1135, 4] = "";
        _text[1135, 5] = "";
        _text[1135, 6] = "";
        _text[1135, 7] = "";
        _text[1135, 8] = "";
        _text[1135, 9] = "";

        // MissionSelectWoodExtractionTypeButton_36
        _text[1136, 0] = "Select the \"Wood Mining\" building type to reveal the available buildings to build.";
        _text[1136, 1] = "Выберите тип здания \"Добыча Дерева\", чтобы открыть доступные здания для постройки.";
        _text[1136, 2] = "";
        _text[1136, 3] = "";
        _text[1136, 4] = "";
        _text[1136, 5] = "";
        _text[1136, 6] = "";
        _text[1136, 7] = "";
        _text[1136, 8] = "";
        _text[1136, 9] = "";

        // MissionStartConstructionManualWoodMining_37
        _text[1137, 0] = "Click on the \"Manual Mining\" card to start building.";
        _text[1137, 1] = "Нажмите на карту \"Ручная Добыча\", чтобы начать строительство.";
        _text[1137, 2] = "";
        _text[1137, 3] = "";
        _text[1137, 4] = "";
        _text[1137, 5] = "";
        _text[1137, 6] = "";
        _text[1137, 7] = "";
        _text[1137, 8] = "";
        _text[1137, 9] = "";

        // MissionDefaultGameSpeed_38
        _text[1138, 0] = "You need to exit the pause to start the building construction process.";
        _text[1138, 1] = "Необходимо выйти из паузы, чтобы запустить процесс строительства здания.";
        _text[1138, 2] = "";
        _text[1138, 3] = "";
        _text[1138, 4] = "";
        _text[1138, 5] = "";
        _text[1138, 6] = "";
        _text[1138, 7] = "";
        _text[1138, 8] = "";
        _text[1138, 9] = "";

        // MissionConstructionStoneExtraction_39
        _text[1139, 0] = "Great, you have a constant supply of wood.\n\nNow set up the \"Mountain\" tile yourself and build a manual stone mining building.";
        _text[1139, 1] = "Отлично, у вас есть постоянная добыча дерева.\n\nТеперь самостоятельно установите тайл \"Гора\" и постройте здание ручной добычи камня.";
        _text[1139, 2] = "";
        _text[1139, 3] = "";
        _text[1139, 4] = "";
        _text[1139, 5] = "";
        _text[1139, 6] = "";
        _text[1139, 7] = "";
        _text[1139, 8] = "";
        _text[1139, 9] = "";

        // MissionCompleteStoneAndWoodExtractionDescription_40
        _text[1140, 0] = "At the moment you are mining two main resources.\n\nBut now it is time to protect the base.";
        _text[1140, 1] = "На данный момент вы добываете два основных ресурса.\n\nНо теперь настало время защитить базу.";
        _text[1140, 2] = "";
        _text[1140, 3] = "";
        _text[1140, 4] = "";
        _text[1140, 5] = "";
        _text[1140, 6] = "";
        _text[1140, 7] = "";
        _text[1140, 8] = "";
        _text[1140, 9] = "";

        // MissionConstructionBallista_41
        _text[1141, 0] = "You need to place a landscape tile on which the building type \"Structures: Attacking\" will be available.\n\nThen build a building on it - \"Ballista\".";
        _text[1141, 1] = "Вам необходимо поставить тайл ландшафта на котором будет доступен тип здания \"Сооружения: Атакующие\".\n\nЗатем постройте на нем здание - \"Баллиста\".";
        _text[1141, 2] = "";
        _text[1141, 3] = "";
        _text[1141, 4] = "";
        _text[1141, 5] = "";
        _text[1141, 6] = "";
        _text[1141, 7] = "";
        _text[1141, 8] = "";
        _text[1141, 9] = "";

        // MissionBallistaDescription_42
        _text[1142, 0] = "Attack structures have a limited attack range.\n\nTry to place them near your base and mining buildings so that enemies cannot easily attack them.";
        _text[1142, 1] = "У атакующих сооружений ограниченный радиус атаки.\n\nСтарайтесь размещать их возле базы и добывающих зданий, чтобы враги не смогли беспрепятственно атаковать их.";
        _text[1142, 2] = "";
        _text[1142, 3] = "";
        _text[1142, 4] = "";
        _text[1142, 5] = "";
        _text[1142, 6] = "";
        _text[1142, 7] = "";
        _text[1142, 8] = "";
        _text[1142, 9] = "";

        // MissionToggleOnSettlement_43
        _text[1143, 0] = "Now that the base is protected, enable work in the \"Settlement\" building.\n\nIt is very important to start mining data fragments.";
        _text[1143, 1] = "Теперь когда база защищена, включите работу в здании \"Поселение\".\n\nОчень важно начать добывать фрагменты данных.";
        _text[1143, 2] = "";
        _text[1143, 3] = "";
        _text[1143, 4] = "";
        _text[1143, 5] = "";
        _text[1143, 6] = "";
        _text[1143, 7] = "";
        _text[1143, 8] = "";
        _text[1143, 9] = "";

        // MissionEnergyBeamDescription_44
        _text[1144, 0] = "When you place any landscape tile, you receive a resource—beam energy.\n\nIt's needed to replace a card in your hand with a random one and to destroy already placed landscape tiles.\n\nIt can also be obtained if extra cards begin to disappear when the deck is full.";
        _text[1144, 1] = "Когда вы устанавливаете любой тайл ландшафта, то вы получаете ресурс - энергия луча.\n\nОна требуется для замены карты в руке на случайную и для уничтожения уже установленных тайлов ландшафта.\n\nТак же ее можно получить, если лишние карты начинают исчезать, когда колода переполняется.";
        _text[1144, 2] = "";
        _text[1144, 3] = "";
        _text[1144, 4] = "";
        _text[1144, 5] = "";
        _text[1144, 6] = "";
        _text[1144, 7] = "";
        _text[1144, 8] = "";
        _text[1144, 9] = "";

        // MissionTileCombineDescription1_45
        _text[1145, 0] = "Proper placement of terrain tiles is the key to successfully completing the mission.\n\nYou can combine them to create new tiles.";
        _text[1145, 1] = "Правильная установка тайлов ландшафта - ключ к успешному прохождению миссии.\n\nВы можете комбинировать их между собой, создавая новые тайлы.";
        _text[1145, 2] = "";
        _text[1145, 3] = "";
        _text[1145, 4] = "";
        _text[1145, 5] = "";
        _text[1145, 6] = "";
        _text[1145, 7] = "";
        _text[1145, 8] = "";
        _text[1145, 9] = "";

        // MissionTileCombineDescription2_46
        _text[1146, 0] = "For example, if you place a plain close to a mountain.\n\nThe plain tile will turn into a meadow.\n\nOn it, you will be able to create other types of buildings and improve the ecology.";
        _text[1146, 1] = "Например, если поставить равнину вплотную к горе.\n\nТайл равнины превратится в луг.\n\nНа нем вы сможете создавать другие типы зданий и повысите экологию.";
        _text[1146, 2] = "";
        _text[1146, 3] = "";
        _text[1146, 4] = "";
        _text[1146, 5] = "";
        _text[1146, 6] = "";
        _text[1146, 7] = "";
        _text[1146, 8] = "";
        _text[1146, 9] = "";

        // MissionTileCombineDescription3_47
        _text[1147, 0] = "But be careful when setting a desert near a forest.\n\nThe forest will turn into an oasis and wood production will decrease.";
        _text[1147, 1] = "Но будьте осторожны в установке пустыни возле леса.\n\nТаким образом лес превратится в оазис и добыча дерева уменьшится.";
        _text[1147, 2] = "";
        _text[1147, 3] = "";
        _text[1147, 4] = "";
        _text[1147, 5] = "";
        _text[1147, 6] = "";
        _text[1147, 7] = "";
        _text[1147, 8] = "";
        _text[1147, 9] = "";

        // MissionSelectTileWithResourceExtraction_48
        _text[1148, 0] = "Click on the tile where the resource is being mined.";
        _text[1148, 1] = "Нажмите на тайл, где происходит добыча ресурса.";
        _text[1148, 2] = "";
        _text[1148, 3] = "";
        _text[1148, 4] = "";
        _text[1148, 5] = "";
        _text[1148, 6] = "";
        _text[1148, 7] = "";
        _text[1148, 8] = "";
        _text[1148, 9] = "";

        // MissionProductionModifierDescription_49
        _text[1149, 0] = "Look at the resource production modifier.\n\nThe modifier may differ on different tiles.\n\nThus, there are profitable and unprofitable tiles for extracting a particular resource.";
        _text[1149, 1] = "Посмотрите на модификатор производства ресурсов.\n\nНа разных тайлах модификатор может отличаться.\n\nТаким образом есть выгодные и не выгодные тайлы для добычи того, или иного ресурса.";
        _text[1149, 2] = "";
        _text[1149, 3] = "";
        _text[1149, 4] = "";
        _text[1149, 5] = "";
        _text[1149, 6] = "";
        _text[1149, 7] = "";
        _text[1149, 8] = "";
        _text[1149, 9] = "";

        // MissionEventPanel_50
        _text[1150, 0] = "This is the event panel.\n\nYou will periodically notice event icons in it.\n\nThe scale is 3 days long.\n\nYou will receive a notification with information about the event 1 day before it.";
        _text[1150, 1] = "Это панель событий.\n\nВ ней периодически вы будете замечать иконки событий.\n\nДлина шкалы равна 3 дням.\n\nЗа 1 день до события вам будет приходить уведомление с информацией о нем.";
        _text[1150, 2] = "";
        _text[1150, 3] = "";
        _text[1150, 4] = "";
        _text[1150, 5] = "";
        _text[1150, 6] = "";
        _text[1150, 7] = "";
        _text[1150, 8] = "";
        _text[1150, 9] = "";

        // MissionOpenSkillsPanel_51
        _text[1151, 0] = "Open the skills panel.";
        _text[1151, 1] = "Откройте панель умений.";
        _text[1151, 2] = "";
        _text[1151, 3] = "";
        _text[1151, 4] = "";
        _text[1151, 5] = "";
        _text[1151, 6] = "";
        _text[1151, 7] = "";
        _text[1151, 8] = "";
        _text[1151, 9] = "";

        // MissionSkillsPanelDescription_52
        _text[1152, 0] = "Here are the skills available for use.\n\nThey can be purchased from merchants or unlocked with \"Shards\" in the hangar when starting a new game.";
        _text[1152, 1] = "Здесь находятся доступные для использования умения.\n\nИх можно приобрести у торговцев или купить за \"Осколок\" в ангаре при старте новой игры.";
        _text[1152, 2] = "";
        _text[1152, 3] = "";
        _text[1152, 4] = "";
        _text[1152, 5] = "";
        _text[1152, 6] = "";
        _text[1152, 7] = "";
        _text[1152, 8] = "";
        _text[1152, 9] = "";

        // MissionShardsDescription_53
        _text[1153, 0] = "Shards are all that remain after the end of a game.\n\nUse them to buy items in the hangar that will allow you to travel further and further.";
        _text[1153, 1] = "Осколки - это все, что остается у вас после окончания игры.\n\nИспользуйте их для покупки предметов в ангаре, с помощью которых вы сможете путешествовать все дальше, и дальше.";
        _text[1153, 2] = "";
        _text[1153, 3] = "";
        _text[1153, 4] = "";
        _text[1153, 5] = "";
        _text[1153, 6] = "";
        _text[1153, 7] = "";
        _text[1153, 8] = "";
        _text[1153, 9] = "";

        // MissionPrepareAttack_54
        _text[1154, 0] = "On day 7, the first group of enemies is expected.\n\nPrepare your base for battle.\n\nFor example, by building additional ballistas.";
        _text[1154, 1] = "На 7 день ожидается первая группа врагов.\n\nПодготовьте вашу базу к битве.\n\nНапример построив дополнительные баллисты.";
        _text[1154, 2] = "";
        _text[1154, 3] = "";
        _text[1154, 4] = "";
        _text[1154, 5] = "";
        _text[1154, 6] = "";
        _text[1154, 7] = "";
        _text[1154, 8] = "";
        _text[1154, 9] = "";

        // MissionDoubleTripleGameSpeedDescription_55
        _text[1155, 0] = "You can speed up the game by 2 or 3 times if you want to quickly accumulate resources or wait for some time.";
        _text[1155, 1] = "Вы можете ускорить игру в 2 или 3 раза, если хотите быстро накопить ресурсы или переждать некоторое время.";
        _text[1155, 2] = "";
        _text[1155, 3] = "";
        _text[1155, 4] = "";
        _text[1155, 5] = "";
        _text[1155, 6] = "";
        _text[1155, 7] = "";
        _text[1155, 8] = "";
        _text[1155, 9] = "";

        // MissionBuildingTakeDamage_56
        _text[1156, 0] = "After your building is attacked.\n\nIt will display a health slider.";
        _text[1156, 1] = "После того как ваше здание атакуют.\n\nУ него отобразится слайдер здоровья.";
        _text[1156, 2] = "";
        _text[1156, 3] = "";
        _text[1156, 4] = "";
        _text[1156, 5] = "";
        _text[1156, 6] = "";
        _text[1156, 7] = "";
        _text[1156, 8] = "";
        _text[1156, 9] = "";

        // MissionSelectTileObjectForRepair_57
        _text[1157, 0] = "You can repair the building.\n\nClick on it to open the tile information panel.";
        _text[1157, 1] = "Вы можете починить здание.\n\nНажмите на него, чтобы открыть панель с информацией о тайле.";
        _text[1157, 2] = "";
        _text[1157, 3] = "";
        _text[1157, 4] = "";
        _text[1157, 5] = "";
        _text[1157, 6] = "";
        _text[1157, 7] = "";
        _text[1157, 8] = "";
        _text[1157, 9] = "";

        // MissionClickBuildButton_58
        _text[1158, 0] = "In the panel, click the \"Construct\" button.";
        _text[1158, 1] = "В панеле нажмите кнопку \"Построить\".";
        _text[1158, 2] = "";
        _text[1158, 3] = "";
        _text[1158, 4] = "";
        _text[1158, 5] = "";
        _text[1158, 6] = "";
        _text[1158, 7] = "";
        _text[1158, 8] = "";
        _text[1158, 9] = "";

        // MissionRepairBuilding_59
        _text[1159, 0] = "A panel with a card of repairs for the current building immediately opened in front of you.\n\nRepair the building.";
        _text[1159, 1] = "Перед вами сразу открылась панель с картой починки текущего здания.\n\nПочините здание.";
        _text[1159, 2] = "";
        _text[1159, 3] = "";
        _text[1159, 4] = "";
        _text[1159, 5] = "";
        _text[1159, 6] = "";
        _text[1159, 7] = "";
        _text[1159, 8] = "";
        _text[1159, 9] = "";

        // MissionUpgradeBuildingDescription1_60
        _text[1160, 0] = "If you already have a building on the tile and have studied other buildings of the same type.\n\nThen when you click on the \"Construct\" button, in addition to repairing the current building, you will find building cards nearby that you can upgrade the current building to.";
        _text[1160, 1] = "Если у вас уже есть здание на тайле и изучены другие здания такого же типа.\n\nТогда при нажатии на кнопку \"Построить\", помимо ремонта текущего здания, рядом вы обнаружите карточки зданий в которые вы можете улучшить текущее здание.";
        _text[1160, 2] = "";
        _text[1160, 3] = "";
        _text[1160, 4] = "";
        _text[1160, 5] = "";
        _text[1160, 6] = "";
        _text[1160, 7] = "";
        _text[1160, 8] = "";
        _text[1160, 9] = "";

        // MissionUpgradeBuildingDescription2_61
        _text[1161, 0] = "When upgrading a building, you automatically receive some of the resources spent on the previously standing building.\n\nTherefore, it is not necessary to destroy the building before constructing its improved version.";
        _text[1161, 1] = "При улучшении здания, вы автоматически получаете часть ресурсов затраченных на ранее стоящее здание.\n\nПоэтому не обязательно уничтожать здание перед постройкой его улучшенной версии.";
        _text[1161, 2] = "";
        _text[1161, 3] = "";
        _text[1161, 4] = "";
        _text[1161, 5] = "";
        _text[1161, 6] = "";
        _text[1161, 7] = "";
        _text[1161, 8] = "";
        _text[1161, 9] = "";

        // MissionShipWeaponModeActive_62
        _text[1162, 0] = "If you are having trouble dealing with enemies, simply turn on ship mode to activate your weapons.";
        _text[1162, 1] = "Если вы не справляетесь с врагами, просто включите режим корабля, чтобы активировать оружие.";
        _text[1162, 2] = "";
        _text[1162, 3] = "";
        _text[1162, 4] = "";
        _text[1162, 5] = "";
        _text[1162, 6] = "";
        _text[1162, 7] = "";
        _text[1162, 8] = "";
        _text[1162, 9] = "";

        // MissionShipWeaponModeDescription_63
        _text[1163, 0] = "Weapon ammo is given out at the start of each mission and has a limited supply.\n\nUse it only in emergency situations.\n\nTo improve weapon damage, you need to visit the engineer on the star map.";
        _text[1163, 1] = "Боеприпасы оружия выдаются в начале каждой миссии и имею ограниченный запас.\n\nИспользуйте их только в экстренных ситуациях.\n\nЧтобы улучшить урон оружия, вам необходимо посетить инженера на звездной карте.";
        _text[1163, 2] = "";
        _text[1163, 3] = "";
        _text[1163, 4] = "";
        _text[1163, 5] = "";
        _text[1163, 6] = "";
        _text[1163, 7] = "";
        _text[1163, 8] = "";
        _text[1163, 9] = "";

        // MissionPlanetModeActive_64
        _text[1164, 0] = "The left mouse button is responsible for shooting the left weapon, the right mouse button is responsible for the right.\n\nWeapons cannot shoot while the game is paused.\n\nIt is better to save ammo at this point.\n\nExit ship mode, back to planet mode.";
        _text[1164, 1] = "Левая кнопка мыши отвечает за выстрелы левым оружие, правая кнопка мыши за правым.\n\nОружие не может стрелять, пока игра находится на паузе.\n\nНа данный момент лучше сэкономить патроны.\n\nВыйдите из режима корабля, обратно в режим планеты.";
        _text[1164, 2] = "";
        _text[1164, 3] = "";
        _text[1164, 4] = "";
        _text[1164, 5] = "";
        _text[1164, 6] = "";
        _text[1164, 7] = "";
        _text[1164, 8] = "";
        _text[1164, 9] = "";

        // MissionDefeatMissionDescription_65
        _text[1165, 0] = "If your base is destroyed, the mission is failed.\n\nYou will lose 1 AI core.\n\nBut you will be able to restart the mission until all the cores are used up.";
        _text[1165, 1] = "Если ваша база будет уничтожена, то миссия будет считаться проваленной.\n\nВы потеряете 1 ядро ИИ.\n\nНо сможете начинать миссию сначала до тех пор, пока не закончатся все ядра.";
        _text[1165, 2] = "";
        _text[1165, 3] = "";
        _text[1165, 4] = "";
        _text[1165, 5] = "";
        _text[1165, 6] = "";
        _text[1165, 7] = "";
        _text[1165, 8] = "";
        _text[1165, 9] = "";

        // MissionGoodLuckDescription_66
        _text[1166, 0] = "Complete all objectives to successfully complete the mission.\n\nDespite the objectives, try to accumulate as many data fragments as possible during the mission.\n\nIf you do not keep up with the advancement in technology, your journey will end quickly...";
        _text[1166, 1] = "Выполните все цели, чтобы успешно завершить миссию.\n\nНесмотря на поставленные цели, старайтесь накопить за миссию как можно больше фрагментов данных.\n\nЕсли вы не будете поспевать за прогрессом в технологиях, ваше путешествие закончится быстро...";
        _text[1166, 2] = "";
        _text[1166, 3] = "";
        _text[1166, 4] = "";
        _text[1166, 5] = "";
        _text[1166, 6] = "";
        _text[1166, 7] = "";
        _text[1166, 8] = "";
        _text[1166, 9] = "";

        // SpaceOpenLearningPanel_67
        _text[1167, 0] = "You have completed the mission and earned data fragments.\n\nNow open the research panel";
        _text[1167, 1] = "Вы прошли миссию и заработали фрагменты данных.\n\nТеперь откройте панель изучений";
        _text[1167, 2] = "";
        _text[1167, 3] = "";
        _text[1167, 4] = "";
        _text[1167, 5] = "";
        _text[1167, 6] = "";
        _text[1167, 7] = "";
        _text[1167, 8] = "";
        _text[1167, 9] = "";

        // SpaceSelectNotLearnBuilding_68
        _text[1168, 0] = "Here you can see all types of buildings available for study.\n\nSee how many data fragments you have mined and select any unexplored building.";
        _text[1168, 1] = "Здесь вы можете увидеть все типы зданий доступные для изучения.\n\nПосмотрите сколько фрагментов данных вы добыли и выберите любое не изученное здание.";
        _text[1168, 2] = "";
        _text[1168, 3] = "";
        _text[1168, 4] = "";
        _text[1168, 5] = "";
        _text[1168, 6] = "";
        _text[1168, 7] = "";
        _text[1168, 8] = "";
        _text[1168, 9] = "";

        // SpaceLearnBuilding_69
        _text[1169, 0] = "If there are enough data fragments, start the study by clicking the button.\n\nSelect another building if there are not enough resources or preliminary research of another building is required.";
        _text[1169, 1] = "Если фрагментов данных достаточно, начните изучение, нажав на кнопку.\n\nВыберите другое сооружение, если ресурсов не хватает или требуется предварительное исследование другого здания.";
        _text[1169, 2] = "";
        _text[1169, 3] = "";
        _text[1169, 4] = "";
        _text[1169, 5] = "";
        _text[1169, 6] = "";
        _text[1169, 7] = "";
        _text[1169, 8] = "";
        _text[1169, 9] = "";

        // SpaceLearnBuildingDescription_70
        _text[1170, 0] = "Great, you've explored a new building.\n\nIt will now be available for construction during missions.";
        _text[1170, 1] = "Отлично, вы изучили новое здание.\n\nТеперь оно станет доступно для постройки на миссиях.";
        _text[1170, 2] = "";
        _text[1170, 3] = "";
        _text[1170, 4] = "";
        _text[1170, 5] = "";
        _text[1170, 6] = "";
        _text[1170, 7] = "";
        _text[1170, 8] = "";
        _text[1170, 9] = "";

        // SpaceExploreSpace_71.
        _text[1171, 0] = "Return to the map and explore space.\n\nTo find a habitable planet...";
        _text[1171, 1] = "Возвращайтесь на карту и исследуйте космос.\n\nЧтобы найти пригодную для жизни планету...";
        _text[1171, 2] = "";
        _text[1171, 3] = "";
        _text[1171, 4] = "";
        _text[1171, 5] = "";
        _text[1171, 6] = "";
        _text[1171, 7] = "";
        _text[1171, 8] = "";
        _text[1171, 9] = "";

        #endregion

        #region Buildings

        _text[1200, 0] = "Settlement";
        _text[1200, 1] = "Поселение";
        _text[1200, 2] = "";
        _text[1200, 3] = "";
        _text[1200, 4] = "";
        _text[1200, 5] = "";
        _text[1200, 6] = "";
        _text[1200, 7] = "";
        _text[1200, 8] = "";
        _text[1200, 9] = "";

        _text[1201, 0] = "Town";
        _text[1201, 1] = "Город";
        _text[1201, 2] = "";
        _text[1201, 3] = "";
        _text[1201, 4] = "";
        _text[1201, 5] = "";
        _text[1201, 6] = "";
        _text[1201, 7] = "";
        _text[1201, 8] = "";
        _text[1201, 9] = "";

        _text[1202, 0] = "Industrial City";
        _text[1202, 1] = "Промышленный Город";
        _text[1202, 2] = "";
        _text[1202, 3] = "";
        _text[1202, 4] = "";
        _text[1202, 5] = "";
        _text[1202, 6] = "";
        _text[1202, 7] = "";
        _text[1202, 8] = "";
        _text[1202, 9] = "";

        _text[1203, 0] = "Megapolis";
        _text[1203, 1] = "Мегаполис";
        _text[1203, 2] = "";
        _text[1203, 3] = "";
        _text[1203, 4] = "";
        _text[1203, 5] = "";
        _text[1203, 6] = "";
        _text[1203, 7] = "";
        _text[1203, 8] = "";
        _text[1203, 9] = "";

        _text[1204, 0] = "Wind Generator";
        _text[1204, 1] = "Ветряной Генератор";
        _text[1204, 2] = "";
        _text[1204, 3] = "";
        _text[1204, 4] = "";
        _text[1204, 5] = "";
        _text[1204, 6] = "";
        _text[1204, 7] = "";
        _text[1204, 8] = "";
        _text[1204, 9] = "";

        _text[1205, 0] = "Steam Engine";
        _text[1205, 1] = "Паровой Двигатель";
        _text[1205, 2] = "";
        _text[1205, 3] = "";
        _text[1205, 4] = "";
        _text[1205, 5] = "";
        _text[1205, 6] = "";
        _text[1205, 7] = "";
        _text[1205, 8] = "";
        _text[1205, 9] = "";

        _text[1206, 0] = "Solar Panel";
        _text[1206, 1] = "Солнечная Панель";
        _text[1206, 2] = "";
        _text[1206, 3] = "";
        _text[1206, 4] = "";
        _text[1206, 5] = "";
        _text[1206, 6] = "";
        _text[1206, 7] = "";
        _text[1206, 8] = "";
        _text[1206, 9] = "";

        _text[1207, 0] = "Thermal Power Plant";
        _text[1207, 1] = "Теплоэлектростанция";
        _text[1207, 2] = "";
        _text[1207, 3] = "";
        _text[1207, 4] = "";
        _text[1207, 5] = "";
        _text[1207, 6] = "";
        _text[1207, 7] = "";
        _text[1207, 8] = "";
        _text[1207, 9] = "";

        _text[1208, 0] = "Manual Mining";
        _text[1208, 1] = "Ручная Добыча";
        _text[1208, 2] = "";
        _text[1208, 3] = "";
        _text[1208, 4] = "";
        _text[1208, 5] = "";
        _text[1208, 6] = "";
        _text[1208, 7] = "";
        _text[1208, 8] = "";
        _text[1208, 9] = "";

        _text[1209, 0] = "Coal Mine";
        _text[1209, 1] = "Угольная Шахта";
        _text[1209, 2] = "";
        _text[1209, 3] = "";
        _text[1209, 4] = "";
        _text[1209, 5] = "";
        _text[1209, 6] = "";
        _text[1209, 7] = "";
        _text[1209, 8] = "";
        _text[1209, 9] = "";

        _text[1210, 0] = "Steam Rig";
        _text[1210, 1] = "Паровая Установка";
        _text[1210, 2] = "";
        _text[1210, 3] = "";
        _text[1210, 4] = "";
        _text[1210, 5] = "";
        _text[1210, 6] = "";
        _text[1210, 7] = "";
        _text[1210, 8] = "";
        _text[1210, 9] = "";

        _text[1211, 0] = "Drilling Rig";
        _text[1211, 1] = "Буровая Установка";
        _text[1211, 2] = "";
        _text[1211, 3] = "";
        _text[1211, 4] = "";
        _text[1211, 5] = "";
        _text[1211, 6] = "";
        _text[1211, 7] = "";
        _text[1211, 8] = "";
        _text[1211, 9] = "";

        _text[1212, 0] = "Manual Mining";
        _text[1212, 1] = "Ручная Добыча";
        _text[1212, 2] = "";
        _text[1212, 3] = "";
        _text[1212, 4] = "";
        _text[1212, 5] = "";
        _text[1212, 6] = "";
        _text[1212, 7] = "";
        _text[1212, 8] = "";
        _text[1212, 9] = "";

        _text[1213, 0] = "Mine";
        _text[1213, 1] = "Рудник";
        _text[1213, 2] = "";
        _text[1213, 3] = "";
        _text[1213, 4] = "";
        _text[1213, 5] = "";
        _text[1213, 6] = "";
        _text[1213, 7] = "";
        _text[1213, 8] = "";
        _text[1213, 9] = "";

        _text[1214, 0] = "Steam-Powered Drill";
        _text[1214, 1] = "Паровой Бур";
        _text[1214, 2] = "";
        _text[1214, 3] = "";
        _text[1214, 4] = "";
        _text[1214, 5] = "";
        _text[1214, 6] = "";
        _text[1214, 7] = "";
        _text[1214, 8] = "";
        _text[1214, 9] = "";

        _text[1215, 0] = "Bucket-wheel Excavator";
        _text[1215, 1] = "Многоковшовый Экскаватор";
        _text[1215, 2] = "";
        _text[1215, 3] = "";
        _text[1215, 4] = "";
        _text[1215, 5] = "";
        _text[1215, 6] = "";
        _text[1215, 7] = "";
        _text[1215, 8] = "";
        _text[1215, 9] = "";

        _text[1216, 0] = "Manual Mining";
        _text[1216, 1] = "Ручная Добыча";
        _text[1216, 2] = "";
        _text[1216, 3] = "";
        _text[1216, 4] = "";
        _text[1216, 5] = "";
        _text[1216, 6] = "";
        _text[1216, 7] = "";
        _text[1216, 8] = "";
        _text[1216, 9] = "";

        _text[1217, 0] = "Table Saw";
        _text[1217, 1] = "Распилочный Стол";
        _text[1217, 2] = "";
        _text[1217, 3] = "";
        _text[1217, 4] = "";
        _text[1217, 5] = "";
        _text[1217, 6] = "";
        _text[1217, 7] = "";
        _text[1217, 8] = "";
        _text[1217, 9] = "";

        _text[1218, 0] = "Steam Sawmill";
        _text[1218, 1] = "Паровая Лесопилка";
        _text[1218, 2] = "";
        _text[1218, 3] = "";
        _text[1218, 4] = "";
        _text[1218, 5] = "";
        _text[1218, 6] = "";
        _text[1218, 7] = "";
        _text[1218, 8] = "";
        _text[1218, 9] = "";

        _text[1219, 0] = "Electro Sawmill";
        _text[1219, 1] = "Электролесопилка";
        _text[1219, 2] = "";
        _text[1219, 3] = "";
        _text[1219, 4] = "";
        _text[1219, 5] = "";
        _text[1219, 6] = "";
        _text[1219, 7] = "";
        _text[1219, 8] = "";
        _text[1219, 9] = "";

        _text[1220, 0] = "Manual Mining";
        _text[1220, 1] = "Ручная Добыча";
        _text[1220, 2] = "";
        _text[1220, 3] = "";
        _text[1220, 4] = "";
        _text[1220, 5] = "";
        _text[1220, 6] = "";
        _text[1220, 7] = "";
        _text[1220, 8] = "";
        _text[1220, 9] = "";

        _text[1221, 0] = "Steam Rig";
        _text[1221, 1] = "Паровая Установка";
        _text[1221, 2] = "";
        _text[1221, 3] = "";
        _text[1221, 4] = "";
        _text[1221, 5] = "";
        _text[1221, 6] = "";
        _text[1221, 7] = "";
        _text[1221, 8] = "";
        _text[1221, 9] = "";

        _text[1222, 0] = "Excavator";
        _text[1222, 1] = "Экскаватор";
        _text[1222, 2] = "";
        _text[1222, 3] = "";
        _text[1222, 4] = "";
        _text[1222, 5] = "";
        _text[1222, 6] = "";
        _text[1222, 7] = "";
        _text[1222, 8] = "";
        _text[1222, 9] = "";

        _text[1223, 0] = "Bucket-wheel Excavator";
        _text[1223, 1] = "Многоковшовый Экскаватор";
        _text[1223, 2] = "";
        _text[1223, 3] = "";
        _text[1223, 4] = "";
        _text[1223, 5] = "";
        _text[1223, 6] = "";
        _text[1223, 7] = "";
        _text[1223, 8] = "";
        _text[1223, 9] = "";

        _text[1224, 0] = "Hand Pump";
        _text[1224, 1] = "Ручной Насос";
        _text[1224, 2] = "";
        _text[1224, 3] = "";
        _text[1224, 4] = "";
        _text[1224, 5] = "";
        _text[1224, 6] = "";
        _text[1224, 7] = "";
        _text[1224, 8] = "";
        _text[1224, 9] = "";

        _text[1225, 0] = "Steam Pump";
        _text[1225, 1] = "Паровой Насос";
        _text[1225, 2] = "";
        _text[1225, 3] = "";
        _text[1225, 4] = "";
        _text[1225, 5] = "";
        _text[1225, 6] = "";
        _text[1225, 7] = "";
        _text[1225, 8] = "";
        _text[1225, 9] = "";

        _text[1226, 0] = "Pumpjack";
        _text[1226, 1] = "Насосный Домкрат";
        _text[1226, 2] = "";
        _text[1226, 3] = "";
        _text[1226, 4] = "";
        _text[1226, 5] = "";
        _text[1226, 6] = "";
        _text[1226, 7] = "";
        _text[1226, 8] = "";
        _text[1226, 9] = "";

        _text[1227, 0] = "Oil Rig";
        _text[1227, 1] = "Нефтяная Вышка";
        _text[1227, 2] = "";
        _text[1227, 3] = "";
        _text[1227, 4] = "";
        _text[1227, 5] = "";
        _text[1227, 6] = "";
        _text[1227, 7] = "";
        _text[1227, 8] = "";
        _text[1227, 9] = "";

        _text[1228, 0] = "Manual Mining";
        _text[1228, 1] = "Ручная Добыча";
        _text[1228, 2] = "";
        _text[1228, 3] = "";
        _text[1228, 4] = "";
        _text[1228, 5] = "";
        _text[1228, 6] = "";
        _text[1228, 7] = "";
        _text[1228, 8] = "";
        _text[1228, 9] = "";

        _text[1229, 0] = "Stone Mine";
        _text[1229, 1] = "Каменный Рудник";
        _text[1229, 2] = "";
        _text[1229, 3] = "";
        _text[1229, 4] = "";
        _text[1229, 5] = "";
        _text[1229, 6] = "";
        _text[1229, 7] = "";
        _text[1229, 8] = "";
        _text[1229, 9] = "";

        _text[1230, 0] = "Steam-Powered Drill";
        _text[1230, 1] = "Паровой Бур";
        _text[1230, 2] = "";
        _text[1230, 3] = "";
        _text[1230, 4] = "";
        _text[1230, 5] = "";
        _text[1230, 6] = "";
        _text[1230, 7] = "";
        _text[1230, 8] = "";
        _text[1230, 9] = "";

        _text[1231, 0] = "Drilling Rig";
        _text[1231, 1] = "Буровая Установка";
        _text[1231, 2] = "";
        _text[1231, 3] = "";
        _text[1231, 4] = "";
        _text[1231, 5] = "";
        _text[1231, 6] = "";
        _text[1231, 7] = "";
        _text[1231, 8] = "";
        _text[1231, 9] = "";

        _text[1232, 0] = "Well";
        _text[1232, 1] = "Колодец";
        _text[1232, 2] = "";
        _text[1232, 3] = "";
        _text[1232, 4] = "";
        _text[1232, 5] = "";
        _text[1232, 6] = "";
        _text[1232, 7] = "";
        _text[1232, 8] = "";
        _text[1232, 9] = "";

        _text[1233, 0] = "Wind Pump";
        _text[1233, 1] = "Ветряной Насос";
        _text[1233, 2] = "";
        _text[1233, 3] = "";
        _text[1233, 4] = "";
        _text[1233, 5] = "";
        _text[1233, 6] = "";
        _text[1233, 7] = "";
        _text[1233, 8] = "";
        _text[1233, 9] = "";

        _text[1234, 0] = "Steam Pump";
        _text[1234, 1] = "Паровой Насос";
        _text[1234, 2] = "";
        _text[1234, 3] = "";
        _text[1234, 4] = "";
        _text[1234, 5] = "";
        _text[1234, 6] = "";
        _text[1234, 7] = "";
        _text[1234, 8] = "";
        _text[1234, 9] = "";

        _text[1235, 0] = "Electric Pump";
        _text[1235, 1] = "Электрический Насос";
        _text[1235, 2] = "";
        _text[1235, 3] = "";
        _text[1235, 4] = "";
        _text[1235, 5] = "";
        _text[1235, 6] = "";
        _text[1235, 7] = "";
        _text[1235, 8] = "";
        _text[1235, 9] = "";

        _text[1236, 0] = "Wooden Bridge";
        _text[1236, 1] = "Деревянный Мост";
        _text[1236, 2] = "";
        _text[1236, 3] = "";
        _text[1236, 4] = "";
        _text[1236, 5] = "";
        _text[1236, 6] = "";
        _text[1236, 7] = "";
        _text[1236, 8] = "";
        _text[1236, 9] = "";

        _text[1237, 0] = "Stone Bridge";
        _text[1237, 1] = "Каменный Мост";
        _text[1237, 2] = "";
        _text[1237, 3] = "";
        _text[1237, 4] = "";
        _text[1237, 5] = "";
        _text[1237, 6] = "";
        _text[1237, 7] = "";
        _text[1237, 8] = "";
        _text[1237, 9] = "";

        _text[1238, 0] = "Metal Bridge";
        _text[1238, 1] = "Металлический Мост";
        _text[1238, 2] = "";
        _text[1238, 3] = "";
        _text[1238, 4] = "";
        _text[1238, 5] = "";
        _text[1238, 6] = "";
        _text[1238, 7] = "";
        _text[1238, 8] = "";
        _text[1238, 9] = "";

        _text[1239, 0] = "Stone Cutting Table";
        _text[1239, 1] = "Камнетесный Стол";
        _text[1239, 2] = "";
        _text[1239, 3] = "";
        _text[1239, 4] = "";
        _text[1239, 5] = "";
        _text[1239, 6] = "";
        _text[1239, 7] = "";
        _text[1239, 8] = "";
        _text[1239, 9] = "";

        _text[1240, 0] = "Stone Cutting Workbrench";
        _text[1240, 1] = "Верстак Резки Камня";
        _text[1240, 2] = "";
        _text[1240, 3] = "";
        _text[1240, 4] = "";
        _text[1240, 5] = "";
        _text[1240, 6] = "";
        _text[1240, 7] = "";
        _text[1240, 8] = "";
        _text[1240, 9] = "";

        _text[1241, 0] = "Stone Cutting Factory";
        _text[1241, 1] = "Завод Резки Камня";
        _text[1241, 2] = "";
        _text[1241, 3] = "";
        _text[1241, 4] = "";
        _text[1241, 5] = "";
        _text[1241, 6] = "";
        _text[1241, 7] = "";
        _text[1241, 8] = "";
        _text[1241, 9] = "";

        _text[1242, 0] = "Clay Furnace";
        _text[1242, 1] = "Глиняная Печь";
        _text[1242, 2] = "";
        _text[1242, 3] = "";
        _text[1242, 4] = "";
        _text[1242, 5] = "";
        _text[1242, 6] = "";
        _text[1242, 7] = "";
        _text[1242, 8] = "";
        _text[1242, 9] = "";

        _text[1243, 0] = "Stone Smeltery";
        _text[1243, 1] = "Каменная Плавильня";
        _text[1243, 2] = "";
        _text[1243, 3] = "";
        _text[1243, 4] = "";
        _text[1243, 5] = "";
        _text[1243, 6] = "";
        _text[1243, 7] = "";
        _text[1243, 8] = "";
        _text[1243, 9] = "";

        _text[1244, 0] = "Smelting Furnace";
        _text[1244, 1] = "Плавильная Печь";
        _text[1244, 2] = "";
        _text[1244, 3] = "";
        _text[1244, 4] = "";
        _text[1244, 5] = "";
        _text[1244, 6] = "";
        _text[1244, 7] = "";
        _text[1244, 8] = "";
        _text[1244, 9] = "";

        _text[1245, 0] = "Blast Furnace";
        _text[1245, 1] = "Доменная Печь";
        _text[1245, 2] = "";
        _text[1245, 3] = "";
        _text[1245, 4] = "";
        _text[1245, 5] = "";
        _text[1245, 6] = "";
        _text[1245, 7] = "";
        _text[1245, 8] = "";
        _text[1245, 9] = "";

        _text[1246, 0] = "Manual Mixing";
        _text[1246, 1] = "Ручное Перемешивание";
        _text[1246, 2] = "";
        _text[1246, 3] = "";
        _text[1246, 4] = "";
        _text[1246, 5] = "";
        _text[1246, 6] = "";
        _text[1246, 7] = "";
        _text[1246, 8] = "";
        _text[1246, 9] = "";

        _text[1247, 0] = "Automixer";
        _text[1247, 1] = "Автомешалка";
        _text[1247, 2] = "";
        _text[1247, 3] = "";
        _text[1247, 4] = "";
        _text[1247, 5] = "";
        _text[1247, 6] = "";
        _text[1247, 7] = "";
        _text[1247, 8] = "";
        _text[1247, 9] = "";

        _text[1248, 0] = "Concrete Factory";
        _text[1248, 1] = "Бетонный Завод";
        _text[1248, 2] = "";
        _text[1248, 3] = "";
        _text[1248, 4] = "";
        _text[1248, 5] = "";
        _text[1248, 6] = "";
        _text[1248, 7] = "";
        _text[1248, 8] = "";
        _text[1248, 9] = "";

        _text[1249, 0] = "Boiler";
        _text[1249, 1] = "Котел";
        _text[1249, 2] = "";
        _text[1249, 3] = "";
        _text[1249, 4] = "";
        _text[1249, 5] = "";
        _text[1249, 6] = "";
        _text[1249, 7] = "";
        _text[1249, 8] = "";
        _text[1249, 9] = "";

        _text[1250, 0] = "Big Boiler";
        _text[1250, 1] = "Большой Котел";
        _text[1250, 2] = "";
        _text[1250, 3] = "";
        _text[1250, 4] = "";
        _text[1250, 5] = "";
        _text[1250, 6] = "";
        _text[1250, 7] = "";
        _text[1250, 8] = "";
        _text[1250, 9] = "";

        _text[1251, 0] = "Steam Generator Complex";
        _text[1251, 1] = "Парогенераторный Комплекс";
        _text[1251, 2] = "";
        _text[1251, 3] = "";
        _text[1251, 4] = "";
        _text[1251, 5] = "";
        _text[1251, 6] = "";
        _text[1251, 7] = "";
        _text[1251, 8] = "";
        _text[1251, 9] = "";
        
        _text[1252, 0] = "Components Workbench";
        _text[1252, 1] = "Верстак Компонентов";
        _text[1252, 2] = "";
        _text[1252, 3] = "";
        _text[1252, 4] = "";
        _text[1252, 5] = "";
        _text[1252, 6] = "";
        _text[1252, 7] = "";
        _text[1252, 8] = "";
        _text[1252, 9] = "";

        _text[1253, 0] = "Components Workshop";
        _text[1253, 1] = "Цех Компонентов";
        _text[1253, 2] = "";
        _text[1253, 3] = "";
        _text[1253, 4] = "";
        _text[1253, 5] = "";
        _text[1253, 6] = "";
        _text[1253, 7] = "";
        _text[1253, 8] = "";
        _text[1253, 9] = "";

        _text[1254, 0] = "Components Factory";
        _text[1254, 1] = "Фабрика Компонентов";
        _text[1254, 2] = "";
        _text[1254, 3] = "";
        _text[1254, 4] = "";
        _text[1254, 5] = "";
        _text[1254, 6] = "";
        _text[1254, 7] = "";
        _text[1254, 8] = "";
        _text[1254, 9] = "";

        _text[1255, 0] = "Bioseptic";
        _text[1255, 1] = "Биосептик";
        _text[1255, 2] = "";
        _text[1255, 3] = "";
        _text[1255, 4] = "";
        _text[1255, 5] = "";
        _text[1255, 6] = "";
        _text[1255, 7] = "";
        _text[1255, 8] = "";
        _text[1255, 9] = "";

        _text[1256, 0] = "Aerogenerator";
        _text[1256, 1] = "Аэрогенератор";
        _text[1256, 2] = "";
        _text[1256, 3] = "";
        _text[1256, 4] = "";
        _text[1256, 5] = "";
        _text[1256, 6] = "";
        _text[1256, 7] = "";
        _text[1256, 8] = "";
        _text[1256, 9] = "";

        _text[1257, 0] = "Waste Neutralizer";
        _text[1257, 1] = "Нейтрализатор Отходов";
        _text[1257, 2] = "";
        _text[1257, 3] = "";
        _text[1257, 4] = "";
        _text[1257, 5] = "";
        _text[1257, 6] = "";
        _text[1257, 7] = "";
        _text[1257, 8] = "";
        _text[1257, 9] = "";

        _text[1258, 0] = "Radio Transmitter ";
        _text[1258, 1] = "Радиопередатчик";
        _text[1258, 2] = "";
        _text[1258, 3] = "";
        _text[1258, 4] = "";
        _text[1258, 5] = "";
        _text[1258, 6] = "";
        _text[1258, 7] = "";
        _text[1258, 8] = "";
        _text[1258, 9] = "";

        _text[1259, 0] = "Radio Tower ";
        _text[1259, 1] = "Радиовышка";
        _text[1259, 2] = "";
        _text[1259, 3] = "";
        _text[1259, 4] = "";
        _text[1259, 5] = "";
        _text[1259, 6] = "";
        _text[1259, 7] = "";
        _text[1259, 8] = "";
        _text[1259, 9] = "";

        _text[1260, 0] = "Satellite Dish";
        _text[1260, 1] = "Спутниковая Антенна";
        _text[1260, 2] = "";
        _text[1260, 3] = "";
        _text[1260, 4] = "";
        _text[1260, 5] = "";
        _text[1260, 6] = "";
        _text[1260, 7] = "";
        _text[1260, 8] = "";
        _text[1260, 9] = "";

        _text[1261, 0] = "Wooden Wall";
        _text[1261, 1] = "Деревянная Стена";
        _text[1261, 2] = "";
        _text[1261, 3] = "";
        _text[1261, 4] = "";
        _text[1261, 5] = "";
        _text[1261, 6] = "";
        _text[1261, 7] = "";
        _text[1261, 8] = "";
        _text[1261, 9] = "";

        _text[1262, 0] = "Sandbag Wall";
        _text[1262, 1] = "Песчаная Стена";
        _text[1262, 2] = "";
        _text[1262, 3] = "";
        _text[1262, 4] = "";
        _text[1262, 5] = "";
        _text[1262, 6] = "";
        _text[1262, 7] = "";
        _text[1262, 8] = "";
        _text[1262, 9] = "";

        _text[1263, 0] = "Stone Wall";
        _text[1263, 1] = "Каменная Стена";
        _text[1263, 2] = "";
        _text[1263, 3] = "";
        _text[1263, 4] = "";
        _text[1263, 5] = "";
        _text[1263, 6] = "";
        _text[1263, 7] = "";
        _text[1263, 8] = "";
        _text[1263, 9] = "";

        _text[1264, 0] = "Concrete Wall";
        _text[1264, 1] = "Бетонная Стена";
        _text[1264, 2] = "";
        _text[1264, 3] = "";
        _text[1264, 4] = "";
        _text[1264, 5] = "";
        _text[1264, 6] = "";
        _text[1264, 7] = "";
        _text[1264, 8] = "";
        _text[1264, 9] = "";

        _text[1265, 0] = "Steel Wall";
        _text[1265, 1] = "Стальная Стена";
        _text[1265, 2] = "";
        _text[1265, 3] = "";
        _text[1265, 4] = "";
        _text[1265, 5] = "";
        _text[1265, 6] = "";
        _text[1265, 7] = "";
        _text[1265, 8] = "";
        _text[1265, 9] = "";

        _text[1266, 0] = "Wooden Gate";
        _text[1266, 1] = "Деревянные Ворота";
        _text[1266, 2] = "";
        _text[1266, 3] = "";
        _text[1266, 4] = "";
        _text[1266, 5] = "";
        _text[1266, 6] = "";
        _text[1266, 7] = "";
        _text[1266, 8] = "";
        _text[1266, 9] = "";

        _text[1267, 0] = "Sandbag Gate";
        _text[1267, 1] = "Песчаные Ворота";
        _text[1267, 2] = "";
        _text[1267, 3] = "";
        _text[1267, 4] = "";
        _text[1267, 5] = "";
        _text[1267, 6] = "";
        _text[1267, 7] = "";
        _text[1267, 8] = "";
        _text[1267, 9] = "";

        _text[1268, 0] = "Stone Gate";
        _text[1268, 1] = "Каменные Ворота";
        _text[1268, 2] = "";
        _text[1268, 3] = "";
        _text[1268, 4] = "";
        _text[1268, 5] = "";
        _text[1268, 6] = "";
        _text[1268, 7] = "";
        _text[1268, 8] = "";
        _text[1268, 9] = "";

        _text[1269, 0] = "Concrete Gate";
        _text[1269, 1] = "Бетонные Ворота";
        _text[1269, 2] = "";
        _text[1269, 3] = "";
        _text[1269, 4] = "";
        _text[1269, 5] = "";
        _text[1269, 6] = "";
        _text[1269, 7] = "";
        _text[1269, 8] = "";
        _text[1269, 9] = "";

        _text[1270, 0] = "Steel Gate";
        _text[1270, 1] = "Стальные Ворота";
        _text[1270, 2] = "";
        _text[1270, 3] = "";
        _text[1270, 4] = "";
        _text[1270, 5] = "";
        _text[1270, 6] = "";
        _text[1270, 7] = "";
        _text[1270, 8] = "";
        _text[1270, 9] = "";

        _text[1271, 0] = "Ballista";
        _text[1271, 1] = "Баллиста";
        _text[1271, 2] = "";
        _text[1271, 3] = "";
        _text[1271, 4] = "";
        _text[1271, 5] = "";
        _text[1271, 6] = "";
        _text[1271, 7] = "";
        _text[1271, 8] = "";
        _text[1271, 9] = "";

        _text[1272, 0] = "Cannon";
        _text[1272, 1] = "Пушка";
        _text[1272, 2] = "";
        _text[1272, 3] = "";
        _text[1272, 4] = "";
        _text[1272, 5] = "";
        _text[1272, 6] = "";
        _text[1272, 7] = "";
        _text[1272, 8] = "";
        _text[1272, 9] = "";

        _text[1273, 0] = "Howitzer";
        _text[1273, 1] = "Гаубица";
        _text[1273, 2] = "";
        _text[1273, 3] = "";
        _text[1273, 4] = "";
        _text[1273, 5] = "";
        _text[1273, 6] = "";
        _text[1273, 7] = "";
        _text[1273, 8] = "";
        _text[1273, 9] = "";

        _text[1274, 0] = "Turret Gun";
        _text[1274, 1] = "Турельная Пушка";
        _text[1274, 2] = "";
        _text[1274, 3] = "";
        _text[1274, 4] = "";
        _text[1274, 5] = "";
        _text[1274, 6] = "";
        _text[1274, 7] = "";
        _text[1274, 8] = "";
        _text[1274, 9] = "";

        _text[1275, 0] = "Minigun";
        _text[1275, 1] = "Миниган";
        _text[1275, 2] = "";
        _text[1275, 3] = "";
        _text[1275, 4] = "";
        _text[1275, 5] = "";
        _text[1275, 6] = "";
        _text[1275, 7] = "";
        _text[1275, 8] = "";
        _text[1275, 9] = "";

        _text[1276, 0] = "Rocket Launcher";
        _text[1276, 1] = "Ракетная Установка";
        _text[1276, 2] = "";
        _text[1276, 3] = "";
        _text[1276, 4] = "";
        _text[1276, 5] = "";
        _text[1276, 6] = "";
        _text[1276, 7] = "";
        _text[1276, 8] = "";
        _text[1276, 9] = "";

        _text[1277, 0] = "Laser Cannon";
        _text[1277, 1] = "Лазерная Пушка";
        _text[1277, 2] = "";
        _text[1277, 3] = "";
        _text[1277, 4] = "";
        _text[1277, 5] = "";
        _text[1277, 6] = "";
        _text[1277, 7] = "";
        _text[1277, 8] = "";
        _text[1277, 9] = "";

        _text[1278, 0] = "Battleship Tower";
        _text[1278, 1] = "Башня Линкора";
        _text[1278, 2] = "";
        _text[1278, 3] = "";
        _text[1278, 4] = "";
        _text[1278, 5] = "";
        _text[1278, 6] = "";
        _text[1278, 7] = "";
        _text[1278, 8] = "";
        _text[1278, 9] = "";

        _text[1279, 0] = "Mechanic's Tent";
        _text[1279, 1] = "Палатка Механика";
        _text[1279, 2] = "";
        _text[1279, 3] = "";
        _text[1279, 4] = "";
        _text[1279, 5] = "";
        _text[1279, 6] = "";
        _text[1279, 7] = "";
        _text[1279, 8] = "";
        _text[1279, 9] = "";

        _text[1280, 0] = "Mechanical Workshop";
        _text[1280, 1] = "Механический Цех";
        _text[1280, 2] = "";
        _text[1280, 3] = "";
        _text[1280, 4] = "";
        _text[1280, 5] = "";
        _text[1280, 6] = "";
        _text[1280, 7] = "";
        _text[1280, 8] = "";
        _text[1280, 9] = "";

        _text[1281, 0] = "Automaton Factory";
        _text[1281, 1] = "Фабрика Автоматонов";
        _text[1281, 2] = "";
        _text[1281, 3] = "";
        _text[1281, 4] = "";
        _text[1281, 5] = "";
        _text[1281, 6] = "";
        _text[1281, 7] = "";
        _text[1281, 8] = "";
        _text[1281, 9] = "";

        _text[1282, 0] = "Wooden Spikes";
        _text[1282, 1] = "Деревянные Шипы";
        _text[1282, 2] = "";
        _text[1282, 3] = "";
        _text[1282, 4] = "";
        _text[1282, 5] = "";
        _text[1282, 6] = "";
        _text[1282, 7] = "";
        _text[1282, 8] = "";
        _text[1282, 9] = "";

        _text[1283, 0] = "Glass Shards";
        _text[1283, 1] = "Осколки Стекла";
        _text[1283, 2] = "";
        _text[1283, 3] = "";
        _text[1283, 4] = "";
        _text[1283, 5] = "";
        _text[1283, 6] = "";
        _text[1283, 7] = "";
        _text[1283, 8] = "";
        _text[1283, 9] = "";

        _text[1284, 0] = "Iron Spikes";
        _text[1284, 1] = "Железные Шипы";
        _text[1284, 2] = "";
        _text[1284, 3] = "";
        _text[1284, 4] = "";
        _text[1284, 5] = "";
        _text[1284, 6] = "";
        _text[1284, 7] = "";
        _text[1284, 8] = "";
        _text[1284, 9] = "";

        _text[1285, 0] = "Steel Saws";
        _text[1285, 1] = "Стальные Пилы";
        _text[1285, 2] = "";
        _text[1285, 3] = "";
        _text[1285, 4] = "";
        _text[1285, 5] = "";
        _text[1285, 6] = "";
        _text[1285, 7] = "";
        _text[1285, 8] = "";
        _text[1285, 9] = "";

        _text[1286, 0] = "Electrical Barrier";
        _text[1286, 1] = "Электрический барьер";
        _text[1286, 2] = "";
        _text[1286, 3] = "";
        _text[1286, 4] = "";
        _text[1286, 5] = "";
        _text[1286, 6] = "";
        _text[1286, 7] = "";
        _text[1286, 8] = "";
        _text[1286, 9] = "";

        #endregion

        for (int x = 0; x < WorldGameInfo.LanguageLength; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
