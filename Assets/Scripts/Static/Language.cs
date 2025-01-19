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

        _text[33, 0] = "Continue";
        _text[33, 1] = "Продолжить";

        _text[34, 0] = "Ecology level:";
        _text[34, 1] = "Уровень экологии:";

        _text[35, 0] = "Starting resources:";
        _text[35, 1] = "Начальные ресурсы:";

        _text[36, 0] = "Objectives:";
        _text[36, 1] = "Цели:";

        _text[37, 0] = "days";
        _text[37, 1] = "дней";

        _text[38, 0] = "";
        _text[38, 1] = "";

        _text[39, 0] = "Restore the ecology to";
        _text[39, 1] = "Восстановить экологию до";

        _text[40, 0] = "Kill {0} enemies";
        _text[40, 1] = "Убить {0} врагов";

        _text[41, 0] = "Construct {0} buildings";
        _text[41, 1] = "Построить {0} зданий";

        _text[42, 0] = "Survive {0} days";
        _text[42, 1] = "Выжить {0} дней";

        _text[43, 0] = "Memory Fragments:";
        _text[43, 1] = "Фрагментов Памяти:";

        _text[44, 0] = "Escape";
        _text[44, 1] = "Сбежать";

        _text[45, 0] = "Restart";
        _text[45, 1] = "Перезапуск";

        _text[46, 0] = "Exit";
        _text[46, 1] = "Выход";

        _text[47, 0] = "Main menu";
        _text[47, 1] = "Главное меню";

        _text[48, 0] = "Are you sure you want to restart the mission?\n\nYour current save will be overwritten.";
        _text[48, 1] = "Вы уверены, что хотите перезапустить миссию?\n\nВаше текущее сохранение будет перезаписано.";

        _text[49, 0] = "Yes";
        _text[49, 1] = "Да";

        _text[50, 0] = "No";
        _text[50, 1] = "Нет";

        _text[51, 0] = "Start mission";
        _text[51, 1] = "Начать миссию";

        _text[52, 0] = "Load mission";
        _text[52, 1] = "Загрузить миссию";

        _text[53, 0] = "Construct";
        _text[53, 1] = "Построить";

        _text[54, 0] = "On / Off";
        _text[54, 1] = "Вкл / Выкл";

        _text[55, 0] = "Rotate";
        _text[55, 1] = "Повернуть";

        _text[56, 0] = "Destroy";
        _text[56, 1] = "Разрушить";

        _text[57, 0] = "Robots";
        _text[57, 1] = "Роботы";

        _text[58, 0] = "Ecology restored: {0}/{1}";
        _text[58, 1] = "Экология восстановлена: {0}/{1}";

        _text[59, 0] = "Enemies killed: {0}/{1}";
        _text[59, 1] = "Убито врагов: {0}/{1}";

        _text[60, 0] = "Buildings constructed: {0}/{1}";
        _text[60, 1] = "Построено зданий: {0}/{1}";

        _text[61, 0] = "Days lived: {0}/{1}";
        _text[61, 1] = "Прожито дней: {0}/{1}";

        _text[62, 0] = "Memory fragments received:";
        _text[62, 1] = "Получено фрагментов памяти:";

        _text[63, 0] = "Victory";
        _text[63, 1] = "Победа";

        _text[64, 0] = "Defeat";
        _text[64, 1] = "Поражение";

        _text[65, 0] = "Escape";
        _text[65, 1] = "Сбежал";

        _text[66, 0] = "Escape the mission?\n\nYou will only receive {0}% of the memory fragments\n\nTo escape, you must complete at least half of the objectives";
        _text[66, 1] = "Сбежать с миссии?\n\nВы получите только {0}% от фрагментов памяти\n\nДля побега необходимо выполнить хотя бы половину поставленных целей";

        _text[67, 0] = "Save the mission and return to command center?";
        _text[67, 1] = "Сохранить миссию и вернутся в командный центр?";

        _text[68, 0] = "Restart the mission? You'll lose your current progress";
        _text[68, 1] = "Перезапустить миссию?\nВы потеряете текущий прогресс";

        _text[69, 0] = "";
        _text[69, 1] = "2100 год -  были созданы первые роботы для помощи людям";

        _text[70, 0] = "";
        _text[70, 1] = "2150 год - был разработан искусственный интеллект, предназначенный для восстановления экологии планеты, истощенной многолетним разрушением природы";

        _text[71, 0] = "";
        _text[71, 1] = "Однако ИИ не успел завершить обучение — в этот момент произошла глобальная катастрофа, оставившая планету в руинах";

        _text[72, 0] = "";
        _text[72, 1] = "Люди не смогли пережить катастрофу, а роботы стали бесцельно скитаться по пустоши до тех пор, пока это не приводило к поломке";

        _text[73, 0] = "";
        _text[73, 1] = "Спустя 100 лет ИИ отправляет сигнал всем выжившим роботам. Все это время он обучался чтобы назначить им новую цель";

        _text[74, 0] = "";
        _text[74, 1] = "Восстановление экологии всей планеты";

        _text[75, 0] = "";
        _text[75, 1] = "Получение данных...";

        _text[76, 0] = "";
        _text[76, 1] = "Анализ повреждений...";

        _text[77, 0] = "";
        _text[77, 1] = "Идентификация объектов завершена";

        _text[78, 0] = "";
        _text[78, 1] = "Запуск алгоритмов восстановления";

        _text[79, 0] = "";
        _text[79, 1] = "Соединение с удаленным узлом...";

        _text[80, 0] = "";
        _text[80, 1] = "Данные успешно переданы";

        _text[81, 0] = "";
        _text[81, 1] = "Ошибка: повреждено 12% архива";

        _text[82, 0] = "";
        _text[82, 1] = "Инициализация экосистемного протокола";

        _text[83, 0] = "";
        _text[83, 1] = "Обновление программного ядра завершено";

        _text[84, 0] = "";
        _text[84, 1] = "Поиск уцелевших роботов...";

        _text[85, 0] = "";
        _text[85, 1] = "Назначение новой задачи...";

        _text[86, 0] = "";
        _text[86, 1] = "Критическая ошибка: системное восстановление";

        _text[87, 0] = "";
        _text[87, 1] = "Проектирование защитной структуры...";

        _text[88, 0] = "";
        _text[88, 1] = "Внимание: высокая радиация";

        _text[89, 0] = "";
        _text[89, 1] = "";


        for (int x = 0; x < 100; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
