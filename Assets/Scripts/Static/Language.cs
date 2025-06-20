using UnityEngine;
// using Steamworks;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 1;
    private string[,] _text = new string[WorldGameInfo.LanguageLength, 2];
    public static string[] TextStatic = new string[WorldGameInfo.LanguageLength];

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

        _text[1, 0] = "Recept";
        _text[1, 1] = "Рецепт";

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

        _text[12, 0] = "Select a building";
        _text[12, 1] = "Выберите здание";

        _text[13, 0] = "Buildings";
        _text[13, 1] = "Постройки";

        _text[14, 0] = "Resource for work";
        _text[14, 1] = "Ресурс для работы";

        _text[15, 0] = "Ground ecology";
        _text[15, 1] = "Экология земли";

        _text[16, 0] = "Building ecology";
        _text[16, 1] = "Экология здания";

        _text[17, 0] = "Other";
        _text[17, 1] = "Другое";

        _text[18, 0] = "Durability";
        _text[18, 1] = "Прочность";

        _text[19, 0] = "Melee damage";
        _text[19, 1] = "Урон в ближнем бою";

        _text[20, 0] = "Range damage";
        _text[20, 1] = "Урон в дальнем бою";

        _text[21, 0] = "Machines";
        _text[21, 1] = "Машины";

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

        _text[34, 0] = "Ecology level";
        _text[34, 1] = "Уровень экологии";

        _text[35, 0] = "Starting resources";
        _text[35, 1] = "Начальные ресурсы";

        _text[36, 0] = "Objectives";
        _text[36, 1] = "Цели";

        _text[37, 0] = "days";
        _text[37, 1] = "дней";

        _text[38, 0] = "TERMINAL #042";
        _text[38, 1] = "ТЕРМИНАЛ #042";

        _text[39, 0] = "Restore the ecology to";
        _text[39, 1] = "Восстановить экологию до";

        _text[40, 0] = "Kill {0} enemies";
        _text[40, 1] = "Убить {0} врагов";

        _text[41, 0] = "Construct {0} buildings";
        _text[41, 1] = "Построить {0} зданий";

        _text[42, 0] = "Survive {0} days";
        _text[42, 1] = "Выжить {0} дней";

        _text[43, 0] = "Memory Fragments";
        _text[43, 1] = "Фрагментов Памяти";

        _text[44, 0] = "Escape";
        _text[44, 1] = "Сбежать";

        _text[45, 0] = "Restart";
        _text[45, 1] = "Перезапуск";

        _text[46, 0] = "Exit";
        _text[46, 1] = "Выход";

        _text[47, 0] = "Main menu";
        _text[47, 1] = "Меню";

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

        _text[57, 0] = "Enginery";
        _text[57, 1] = "Техника";

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

        _text[66, 0] = "Escaping the mission will give you {0}% of the memory fragments\n\nYou must complete half of the objectives.";
        _text[66, 1] = "Сбежав с миссии, вы получите {0}% от фрагментов памяти\n\nНеобходимо выполнить половину поставленных целей.";

        _text[67, 0] = "Save the mission and return to command center?";
        _text[67, 1] = "Сохранить миссию и вернутся в командный центр?";

        _text[68, 0] = "Restart the mission? You'll lose your current progress.";
        _text[68, 1] = "Перезапустить миссию?\nВы потеряете текущий прогресс.";

        _text[69, 0] = "In 2100 the first robots were created to help humans.";
        _text[69, 1] = "В 2100 году были созданы первые роботы для помощи людям.";

        _text[70, 0] = "2150: An artificial intelligence is developed to find a habitable planet.";
        _text[70, 1] = "2150 год. Разработан искусственный интеллект, предназначенный найти пригодную для жизни планету.";

        _text[71, 0] = "2200. An interstellar ship controlled by artificial intelligence is launched.";
        _text[71, 1] = "2200 год. Запущен межзвёздный корабль под управлением искусственного интеллекта.";

        _text[72, 0] = "The ship was equipped with a crew of robots and drones designed to restore and stabilize ecosystems.";
        _text[72, 1] = "На борту корабля снарядили экипаж роботов и дронов, созданных для восстановления и стабилизации экосистем.";

        _text[73, 0] = "However, the search dragged on. Contact with the creators was lost...";
        _text[73, 1] = "Однако поиски затянулись. Связь с создателями была утрачена...";

        _text[74, 0] = "But the goal remains the same: to find a habitable planet.";
        _text[74, 1] = "Но цель осталась прежней: найти пригодную для жизни планету.";

        _text[75, 0] = "";
        _text[75, 1] = "";

        _text[76, 0] = "[UPDATE: SECTOR K-12 CLEARED]";
        _text[76, 1] = "[ОБНОВЛЕНИЕ: СЕКТОР К-12 ОЧИЩЕН]";

        _text[77, 0] = "Reconstruction of the territory is complete.\nThe population of aggressive life forms has been reduced by 78%.";
        _text[77, 1] = "Реконструкция территории завершена.\nПопуляция агрессивных форм жизни снижена на 78%.";

        _text[78, 0] = "Traces of destroyed drones of previous generations have been detected.\nawait DecryptFragments();";
        _text[78, 1] = "Обнаружены следы уничтоженных дронов предыдущих поколений.\nawait DecryptFragments();";

        _text[79, 0] = "MEMORY FRAGMENT 1.0.1 - [PARTIALLY RECOVERED]\n\"Project Source... protocol completion delayed...\"";
        _text[79, 1] = "ФРАГМЕНТ ПАМЯТИ 1.0.1 — [ЧАСТИЧНО ВОССТАНОВЛЕН]\n\"Проект Источник... завершение протокола отложено...\"";

        _text[80, 0] = "We move on to the next zone - Junk City.";
        _text[80, 1] = "Продвигаемся к следующей зоне — Город Хлама.";

        _text[81, 0] = "Objective: Find surviving power nodes and restore communications with other sectors.";
        _text[81, 1] = "Цель: Найти уцелевшие узлы питания и восстановить связь с другими секторами.";

        _text[82, 0] = "";
        _text[82, 1] = "";

        _text[83, 0] = "";
        _text[83, 1] = "";

        _text[84, 0] = "";
        _text[84, 1] = "";

        _text[85, 0] = "Sector K-12 scan complete";
        _text[85, 1] = "Сканирование сектора К-12 завершено";

        _text[86, 0] = "Combat unit losses: 18%";
        _text[86, 1] = "Потери среди боевых юнитов: 18%";

        _text[87, 0] = "Memory Signal: 47% match found";
        _text[87, 1] = "Сигнал воспоминаний: обнаружено совпадение 47%";

        _text[88, 0] = "Preparing route to next node... () => return true";
        _text[88, 1] = "Подготовка маршрута к следующему узлу... () => return true";

        _text[89, 0] = "Assigning a new target";
        _text[89, 1] = "Назначение новой цели";

        _text[90, 0] = "ICOSA CORP";
        _text[90, 1] = "ИКОСА КОРП";

        _text[91, 0] = "BUILDING BETTER WORLD";
        _text[91, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";

        _text[92, 0] = "COORDINATES";
        _text[92, 1] = "КООРДИНАТЫ";

        _text[93, 0] = "SIGNAL";
        _text[93, 1] = "СИГНАЛ";

        _text[94, 0] = "DIAGRAM";
        _text[94, 1] = "ДИАГРАММА";

        _text[95, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[95, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";

        _text[96, 0] = "Learn";
        _text[96, 1] = "Изучить";

        _text[97, 0] = "Building health";
        _text[97, 1] = "Здоровье здания";

        _text[98, 0] = "Damage";
        _text[98, 1] = "Урон";

        _text[99, 0] = "Attack speed";
        _text[99, 1] = "Скорость атаки";

        _text[100, 0] = "Attack radius";
        _text[100, 1] = "Радиус атаки";

        _text[101, 0] = "Rotation speed";
        _text[101, 1] = "Скорость вращения";

        _text[102, 0] = "Press any key";
        _text[102, 1] = "Нажмите любую кнопку";

        _text[103, 0] = "Borderless";
        _text[103, 1] = "Безрамочный";

        _text[104, 0] = "Camera speed";
        _text[104, 1] = "Скорость камеры";

        _text[105, 0] = "Master volume";
        _text[105, 1] = "Общая громкость";

        _text[106, 0] = "SFX volume";
        _text[106, 1] = "Громкость эффектов";

        _text[107, 0] = "UI volume";
        _text[107, 1] = "Громкость интерфейса";

        _text[108, 0] = "Music volume";
        _text[108, 1] = "Громкость музыки";

        _text[109, 0] = "Blood";
        _text[109, 1] = "Кровь";

        _text[110, 0] = "Video";
        _text[110, 1] = "Видео";

        _text[111, 0] = "Controls";
        _text[111, 1] = "Управление";

        _text[112, 0] = "Gameplay";
        _text[112, 1] = "Игра";

        _text[113, 0] = "Audio";
        _text[113, 1] = "Аудио";

        _text[114, 0] = "Screen Mode";
        _text[114, 1] = "Режим Экрана";

        _text[115, 0] = "Resolution";
        _text[115, 1] = "Разрешение";

        _text[116, 0] = "Quality";
        _text[116, 1] = "Качество";

        _text[117, 0] = "Anti - Aliasing";
        _text[117, 1] = "Сглаживание";

        _text[118, 0] = "Upscaling Filter";
        _text[118, 1] = "Масштабирование";

        _text[119, 0] = "Glow";
        _text[119, 1] = "Свечение";

        _text[120, 0] = "Max. Frame Rate";
        _text[120, 1] = "Макс. Кол-во Кадров";

        _text[121, 0] = "Close";
        _text[121, 1] = "Закрыть";

        _text[122, 0] = "Apply";
        _text[122, 1] = "Применить";

        _text[123, 0] = "Reset";
        _text[123, 1] = "Сброс";

        _text[124, 0] = "Full-screen";
        _text[124, 1] = "Полноэкранный";

        _text[125, 0] = "Windowed";
        _text[125, 1] = "Оконный";

        _text[126, 0] = "Low";
        _text[126, 1] = "Низкое";

        _text[127, 0] = "Medium";
        _text[127, 1] = "Среднее";

        _text[128, 0] = "High";
        _text[128, 1] = "Высокое";

        _text[129, 0] = "Ultra";
        _text[129, 1] = "Ультра";

        _text[130, 0] = "Disabled";
        _text[130, 1] = "Выключено";

        _text[131, 0] = "Bilinear";
        _text[131, 1] = "Билинейное";

        _text[132, 0] = "Nearest";
        _text[132, 1] = "Ближайшее";

        _text[133, 0] = "Camera movement";
        _text[133, 1] = "Движение камеры";

        _text[134, 0] = "Camera zoom";
        _text[134, 1] = "Масштаб камеры";

        _text[135, 0] = "Select tile / card";
        _text[135, 1] = "Выбор тайла / карты";

        _text[136, 0] = "Unselect tile / card";
        _text[136, 1] = "Отмена выбора тайла / карты";

        _text[137, 0] = "Game speed: pause";
        _text[137, 1] = "Скорость игры: пауза";

        _text[138, 0] = "Game speed: normal";
        _text[138, 1] = "Скорость игры: нормальная";

        _text[139, 0] = "Game speed: double";
        _text[139, 1] = "Скорость игры: двойная";

        _text[140, 0] = "Game speed: triple";
        _text[140, 1] = "Скорость игры: тройная";

        _text[141, 0] = "Menu";
        _text[141, 1] = "Меню";

        _text[142, 0] = "Build on tile";
        _text[142, 1] = "Построить на тайле";

        _text[143, 0] = "Rotate tile / building";
        _text[143, 1] = "Повернуть тайл / здание";

        _text[144, 0] = "Destroy tile / building";
        _text[144, 1] = "Уничтожить тайл / здание";

        _text[145, 0] = "Toggle building";
        _text[145, 1] = "Включить / выключить здание";

        _text[146, 0] = "Open machine panel";
        _text[146, 1] = "Открыть панель машин";

        _text[147, 0] = "Memory restored:";
        _text[147, 1] = "Восстановлено фрагментов:";

        _text[148, 0] = "Ecology bonus:";
        _text[148, 1] = "Экологический бонус:";

        _text[149, 0] = "Difficulty bonus:";
        _text[149, 1] = "Бонус за сложность:";

        _text[150, 0] = "Defeat the boss: {0}/{1}";
        _text[150, 1] = "Победить босса: {0}/{1}";

        _text[151, 0] = "Defeat the boss";
        _text[151, 1] = "Победить босса";

        _text[152, 0] = "Resources for construction:";
        _text[152, 1] = "Ресурсы для строительства:";

        _text[153, 0] = "Wood";
        _text[153, 1] = "Древесина";

        _text[154, 0] = "Stone";
        _text[154, 1] = "Камень";

        _text[155, 0] = "Iron Ore";
        _text[155, 1] = "Железная Руда";

        _text[156, 0] = "Copper Ore";
        _text[156, 1] = "Медная Руда";

        _text[157, 0] = "Coal";
        _text[157, 1] = "Уголь";

        _text[158, 0] = "Oil";
        _text[158, 1] = "Нефть";

        _text[159, 0] = "Water";
        _text[159, 1] = "Вода";

        _text[160, 0] = "Sand";
        _text[160, 1] = "Песок";

        _text[161, 0] = "Electricity";
        _text[161, 1] = "Электричество";

        _text[162, 0] = "Stone Block";
        _text[162, 1] = "Каменный Блок";

        _text[163, 0] = "Iron Ingot";
        _text[163, 1] = "Слиток Железа";

        _text[164, 0] = "Steel Ingot";
        _text[164, 1] = "Слиток Стали";

        _text[165, 0] = "Copper Plate";
        _text[165, 1] = "Медная Пластина";

        _text[166, 0] = "Concrete";
        _text[166, 1] = "Бетон";

        _text[167, 0] = "Steam";
        _text[167, 1] = "Пар";

        _text[168, 0] = "Glass";
        _text[168, 1] = "Стекло";

        _text[169, 0] = "Copper Wire";
        _text[169, 1] = "Медный Провод";

        _text[170, 0] = "Gear Wheel";
        _text[170, 1] = "Шестерня";

        _text[171, 0] = "Electronic Circuit";
        _text[171, 1] = "Электросхема";

        _text[172, 0] = "Processor";
        _text[172, 1] = "Процессор";

        _text[173, 0] = "Engine";
        _text[173, 1] = "Двигатель";

        _text[174, 0] = "Electric Engine";
        _text[174, 1] = "Электродвигатель";

        _text[175, 0] = "Memory Fragment";
        _text[175, 1] = "Фрагмент Памяти";

        _text[176, 0] = "Beam Energy";
        _text[176, 1] = "Энергия Луча";

        _text[177, 0] = "Mark / Remove from general repair";
        _text[177, 1] = "Пометить / Снять с общего ремонта";

        _text[178, 0] = "General repair";
        _text[178, 1] = "Общий ремонт";

        _text[179, 0] = "Skills";
        _text[179, 1] = "Умения";

        _text[180, 0] = "Description";
        _text[180, 1] = "Описание";

        _text[181, 0] = "Requires resources to repair them.";
        _text[181, 1] = "Требуются ресурсы для их починки";

        _text[182, 0] = "Required";
        _text[182, 1] = "Требуется";

        _text[183, 0] = "Interface";
        _text[183, 1] = "Интерфейс";

        _text[184, 0] = "Construction";
        _text[184, 1] = "Строительство";

        _text[185, 0] = "Where to start?";
        _text[185, 1] = "С чего начать?";

        _text[186, 0] = "By pressing the \"Escape\" key, the menu panel will drop down from the top. The following buttons are available in it:\n\nRestart - starts the current mission from the beginning, completely deleting the current progress and accumulated memory fragments.\n\nEscape - the ability to escape from the mission, provided that half of the mission's objectives are completed. Saves half of the accumulated memory fragments.\n\nSettings - opens the panel with game parameters.\n\nExit - saves the current mission progress. Makes a transition to the command center.\n\n     Training - opens the current panel with training";
        _text[186, 1] = "Нажав на клавишу \"Escape\" сверху опустится панель меню. В ней доступны такие кнопки как:\n\nПерезапуск - начинает текущую миссию с начала, полностью удаляя текущий прогресс и накопленные фрагменты памяти.\n\nПобег - возможность сбежать с миссии, при условии выполнения половины от поставленных целей миссии. Сохраняет половину накопленных фрагментов памяти.\n\nНастройки - открывает панель с параметрами игры.\n\nВыход - сохраняет текущий прогресс миссии. Делает переход в командный центр.\n\n     Обучение - открывает текущую панель с  обучением";

        _text[187, 0] = "Combination of tiles";
        _text[187, 1] = "Комбинация тайлов";

        _text[188, 0] = "Research";
        _text[188, 1] = "Исследования";

        _text[189, 0] = "Missions";
        _text[189, 1] = "Миссии";

        _text[190, 0] = "Time and ticks";
        _text[190, 1] = "Время и тики";

        _text[191, 0] = "Event timeline";
        _text[191, 1] = "Шкала событий";

        _text[192, 0] = "Game speed";
        _text[192, 1] = "Скорость игры";

        _text[193, 0] = "Ecology";
        _text[193, 1] = "Экология";

        _text[194, 0] = "Radiation";
        _text[194, 1] = "Радиация";

        _text[195, 0] = "The top panel contains time cells. Time is divided into days, and each day consists of 24 ticks. This is the basic unit of time in the game. At each tick, all buildings produce and consume the necessary resources.";
        _text[195, 1] = "В верхней панели расположены ячейки времени. Время разделено на дни, а каждый день состоит из 24 тиков. Это основная единица времени в игре. На каждом тике все здания производят и потребляют необходимые ресурсы.";

        _text[196, 0] = "To the right of the time cells, you will find the event scale. It displays upcoming events as icons that move from right to left. When an event reaches the left edge of the scale, it is activated. For example, an earthquake, radioactive rain, or something else may begin.";
        _text[196, 1] = "Справа от ячеек времени вы найдете шкалу событий. Она отображает предстоящие события в виде иконок, которые движутся справа налево. Когда событие доходит до левого края шкалы, оно активируется. Например может начаться землетрясение, радиоактивный дождь или что-то другое.";

        _text[197, 0] = "Below the time cells are buttons for changing the game speed. You can speed up time to get through quiet periods faster or pause the game to check important moments.";
        _text[197, 1] = "Под ячейками времени находятся кнопки изменения скорости игры. Вы можете ускорять время, чтобы быстрее пройти спокойные периоды или поставить игру на паузу, чтобы проконтролировать важные моменты.";

        _text[198, 0] = "In the center of the top panel, two large numbers are displayed - the current level of ecology on the map. Each tile and each building has its own ecological parameter, which is summed up into a total. Ecology directly affects the final reward for the mission - the higher it is at the end, the more memory fragments you will receive.";
        _text[198, 1] = "В центре верхней панели отображаются две большие цифры — текущий уровень экологии на карте. Каждый тайл и каждое здание имеет свой собственный экологический параметр, который суммируется в общий. Экология напрямую влияет на финальную награду за миссию - чем выше она будет в конце, тем больше фрагментов памяти вы получите.";

        _text[199, 0] = "The radiation level is displayed under the event scale. Radiation accumulates depending on the events that occur and worsens the ecology of the area. The higher the radiation, the worse the impact on the environment and the final reward upon completion of the mission.";
        _text[199, 1] = "Под шкалой событий отображается уровень радиации. Радиация накапливается в зависимости от происходящих событий и ухудшает экологию местности. Чем выше радиация — тем хуже воздействие на окружающую среду и на итоговую награду по завершению миссии.";

        _text[200, 0] = "Resource panel";
        _text[200, 1] = "Панель ресурсов";

        _text[201, 0] = "Landscape cards";
        _text[201, 1] = "Карты ландшафтов";

        _text[202, 0] = "Energy beam";
        _text[202, 1] = "Энергия луча";

        _text[203, 0] = "On the left side of the screen, you can expand the resource panel. This displays the current amount of all available resources. These values ​​are updated every tick. The panel allows you to track which resources are being produced, consumed, and at what rate.";
        _text[203, 1] = "Слева на экране можно раскрыть панель ресурсов. Здесь отображается текущее количество всех доступных ресурсов. Эти значения обновляются каждый тик времени. Панель позволяет отслеживать, какие ресурсы производятся, потребляются и в каком темпе.";

        _text[204, 0] = "At the bottom of the screen is a map panel that can hold up to 8 landscape maps. Every day, the player receives 2 new maps. If the panel is full, old maps are removed from it in turn. For each removed map, the player receives the resource beam energy.";
        _text[204, 1] = "В нижней части экрана расположена панель карт, вмещающая до 8 карт ландшафтов. Каждые сутки игрок получает 2 новые карты. Если панель переполняется, старые карты удаляются из него в порядке очереди. За каждую удалённую карту игрок получает ресурс энергия луча.";

        _text[205, 0] = "The beam's energy allows you to destroy installed terrain. This is especially useful if you want to change the base configuration or clear space for more useful tiles.";
        _text[205, 1] = "Энергия луча позволяет уничтожать установленные ландшафты. Это особенно полезно, если ты хочешь изменить конфигурацию базы или очистить место под более полезные тайлы.";

        _text[206, 0] = "Installing the base foundation";
        _text[206, 1] = "Установка фундамента базы";

        _text[207, 0] = "Tile panel";
        _text[207, 1] = "Панель тайла";

        _text[208, 0] = "Construction of buildings";
        _text[208, 1] = "Строительство зданий";

        _text[209, 0] = "Building types";
        _text[209, 1] = "Типы зданий";

        _text[210, 0] = "Setting up buildings";
        _text[210, 1] = "Настройка зданий";

        _text[211, 0] = "Removing buildings and tiles";
        _text[211, 1] = "Удаление зданий и тайлов";

        _text[212, 0] = "At the start of the game, a ring road is randomly generated. You are given 1 terrain card - the base foundation. This is a unique 2x2 tile (occupies 4 cells).\n\nClick the \"Base Foundation\" card to place it. When placing a tile, you will see green or red squares - they indicate whether it can be placed in the selected area. In addition, when approaching the edges of the map, red border lines will appear, indicating the restrictions of the placement zone.";
        _text[212, 1] = "В начале игры случайным образом генерируется кольцевая дорога. Вам дается 1 карта ландшафта - фундамент для базы. Это уникальный тайл размером 2x2 (занимает 4 клетки).\n\nЩёлкните по карте \"Фундмаент базы\", чтобы поставить её. При установке тайла вы увидите зелёные или красные квадраты — они указывают, можно ли разместить его в выбранной области. Кроме того, при приближении к краям карты появятся красные линии границы, обозначающие ограничения зоны размещения.";

        _text[213, 0] = "After placing the foundation, click on it. An information panel will appear at the bottom right. In it, you can see the following information:\n\n-Ecology level of the land\n\n-Type and level of the building\n\n-Produced and consumed resources\n\n-Available actions: construction, enabling/disabling the building, rotating the tile, destroying the building/landscape";
        _text[213, 1] = "После размещения фундамента нажмите на него. Справа внизу появится информационная панель. В ней вы можете увидеть такую информацию:\n\n-Уровень экологии земли\n\n-Тип и уровень здания\n\n-Производимые и потребляемые ресурсы\n\n-Доступные действия: постройка, включение/выключение работы здания, поворот тайла, уничтожения здания/ландшафта";

        _text[214, 0] = "In the information panel at the bottom, click the \"Build\" button.\n\nIf the landscape has multiple building types available, a panel with a choice of building type will open.\n\nClick on a building type to open buildings of that type available for construction.\n\nHover over a building to display the resources needed for its construction. Clicking on the building card a second time will begin construction.\n\nDuring construction, a progress indicator will appear under the building. If at this point the building is attacked by monsters and their damage exceeds the construction progress, the building will be destroyed.\n\nIf a building is already installed on the landscape, you can change it for a better building of the same type. In this case, you will get back some of the resources for the previous building, there is no need to destroy it before building a new one.";
        _text[214, 1] = "В информационной панели внизу нажмите кнопку \"Построить\".\n\nЕсли у ландшафта доступна установка нескольких типов зданий, то откроется панель с выбором типа здания.\n\nНажмите на тип здания, чтобы открыть доступные для строительства здания этого типа.\n\nНаведите на здание, чтобы отобразились ресурсы необходимые для его строительства. Нажав на карточку здания второй раз начнется строительство.\n\nВо время постройки под зданием появится индикатор прогресса. Если в этот момент здание будет атаковано монстрами, и их урон превысит прогресс строительства, здание будет разрушено.\n\nЕсли на ландшафте уже установлено здание, вы можете его поменять на здание лучше, но того же типа. При этом вам вернется часть ресурсов за прошлое здание, нет необходимости в его уничтожении, перед постройкой нового.";

        _text[215, 0] = "Each landscape has its own supported building types (ore mining, power generation, etc.) If you do not have available buildings in the category, the button will be gray. By clicking on the category, you will see specific available buildings.\n\nFor example:\nThe \"Wood mining\" category may include: manual mining, sawmill, steam and electric sawmill.";
        _text[215, 1] = "У каждого ландшафта есть свои поддерживаемые типы построек (добыча руды, электроэнергетика и др.) Если у вас нет доступных зданий в категории - кнопка будет серой. Нажав на категорию, ты увидишь конкретные доступные здания.\n\nНапример:\nКатегория \"Добыча дерева\" может включать: ручную добычу, распилочный стол, паровую и электро-лесопилку.";

        _text[216, 0] = "Once the building is completed, it will automatically turn on. However, you can turn it off manually to save resources.\n\nIf the required resource runs out, the building will turn off and turn on automatically as soon as the resource appears.\n\nSome buildings, such as the sawmill, allow you to choose the resource they will use - wood, coal or fuel.";
        _text[216, 1] = "После завершения постройки здание автоматически включается. Однако вы можете выключить его вручную, чтобы сэкономить ресурсы.\n\nЕсли необходимый ресурс закончится здание выключится и включится автоматически как только ресурс появится.\n\nНекоторые здания, например лесопилка, позволяют выбирать ресурс, который они будут использовать - дерево, уголь или топливо.";

        _text[217, 0] = "If a building is already built on the tile, when you click the \"Destroy\" button, you will get back some resources depending on the remaining health. If there is no building, you can delete the landscape itself, but only at the expense of the beam's energy.";
        _text[217, 1] = "Если на тайле уже построено здание, при нажатии на кнопку \"Уничтожить\" вы получите обратно часть ресурсов, зависящую от оставшегося здоровья. Если здания нету - можно удалить сам ландшафт, но только за счёт энергии луча.";

        _text[218, 0] = "Base";
        _text[218, 1] = "База";

        _text[219, 0] = "Resource extraction";
        _text[219, 1] = "Добыча ресурсов";

        _text[220, 0] = "Defense";
        _text[220, 1] = "Оборона";

        _text[221, 0] = "Continuing development";
        _text[221, 1] = "Продолжение развития";

        _text[222, 0] = "At the start of each mission, you have access to one Base Foundation card.\n\nOnce installed, you can build a Shelter, the first building in your base that produces Memory Fragments.\n\nAlong with this, you are guaranteed to receive 4 starting terrain cards:\n\nForest (for wood),\n\nMountain (for stone),\n\nAnd two random cards.\n\nWood and stone are key resources at the beginning of the game. They are needed to build most base buildings, and they are also used to operate some buildings.";
        _text[222, 1] = "В начале каждой миссии вам доступна одна карточка фундамента базы.\n\nПосле установки вы сможете построить убежище — первое здание вашей базы, которое производит фрагменты памяти.\n\nВместе с этим вы гарантированно получаете 4 стартовые карты ландшафта:\n\nЛес (для древесины),\n\nГора (для камня),\n\nИ две случайные карты.\n\nРесурсы древесина и камень — ключевые в начале игры. Они нужны для постройки большинства базовых зданий, а так же они используются для работы некоторых зданий.";

        _text[223, 0] = "Build:\n\nIn the forest - manual wood mining,\n\nOn the mountain - manual stone mining.\n\nThese are the simplest production buildings that do not require energy or other resources.\n\nBe sure to check consumption and production in the tile panel so as not to overload the economy.";
        _text[223, 1] = "Постройте:\n\nВ лесу — ручную добычу дерева,\n\nНа горе — ручную добычу камня.\n\nЭто простейшие производственные здания, не требующие энергии или других ресурсов.\n\nОбязательно проверяйте потребление и производство в панели тайла, чтобы не перегружать экономику.";

        _text[224, 0] = "If you get a random land, desert or plain tile, use it to build attack buildings.\n\nBuild a ballista to start defending against monsters. It:\n\n1)Works automatically\n\n2)Has a fixed attack radius\n\n3)Can be built on most basic terrain\n\nIt is important to build the ballista so that it blocks the approach to the base and construction zones.";
        _text[224, 1] = "Если среди случайных тайлов вам выпали земля, пустыня или равнина — используйте их для постройки атакующих зданий.\n\nПостройте баллисту, чтобы начать защиту от монстров. Она:\n\n1)Работает автоматически\n\n2)Имеет фиксированный радиус атаки\n\n3)Может быть построена на большинстве базовых ландшафтов\n\nВажно строить баллисту так, чтобы она перекрывала подход к базе и зонам строительства.";

        _text[225, 0] = "Continue to place forests and mountains. This will increase the flow of basic resources.\n\nSwitch the resource used by the shelter (wood, stone) to maintain balance and not stop memory production.\n\nIf there are not enough resources, temporarily disable buildings in the control panel to prevent overspending.\n\nExpand the base and surround it with multiple turrets.";
        _text[225, 1] = "Продолжайте устанавливать леса и горы. Это позволит увеличить поступление основных ресурсов.\n\nПереключайте ресурс, используемый убежищем (древесина, камень), чтобы поддерживать баланс и не останавливать производство памяти.\n\nЕсли ресурсов не хватает — временно отключайте здания в панели управления, чтобы не допустить перерасхода.\n\nРасширяйте базу и окружайте ее многочесленными турелями.";

        _text[226, 0] = "Evolution of tiles";
        _text[226, 1] = "Эволюция тайлов";

        _text[227, 0] = "Impact on efficiency";
        _text[227, 1] = "Влияние на эффективность";

        _text[228, 0] = "The game's terrains can be combined. By placing certain tiles next to each other, you can transform them into more advanced forms.\n\nExamples:\n\nPlain + Mountain or River = Meadow - increased ecology and component production\n\nDesert + River = Oasis - reduces sand production, but improves ecology\n\nLand + Oil Swamp = Barren Land - reduces all resource production and ecology, but you can still build offensive and defensive structures";
        _text[228, 1] = "Ландшафты в игре можно комбинировать. Размещая определённые тайлы рядом друг с другом, вы можете превратить их в более продвинутые формы.\n\nПримеры:\n\nРавнина + Гора или Река = Луг — повышенная экология и производство компонентов\n\nПустыня + Река = Оазис - снижает добычу песка, но улучшает экологию\n\nЗемля + Нефтяное Болото = Бесплодная Земля — снижает добычу всех ресурсов и экологию, но вы все еще можете строить атакующие и защитные сооружения";

        _text[229, 0] = "Combined tiles can increase mining speed, unlock new building types, or reduce resource consumption.\n\nThey also affect ecology, which is added to the overall map value.\n\nExperiment! Placing new tiles next to already placed ones can lead to unexpected improvements.\n\nPlease note that tile transformation is irreversible - plan your placement in advance.";
        _text[229, 1] = "Комбинированные тайлы могут повышать скорость добычи, открывать новые типы зданий, или уменьшать потребление ресурсов.\n\nТакже они влияют на экологию, которая суммируется в общий показатель карты.\n\nЭкспериментируй! Установка новых тайлов рядом с уже размещёнными может привести к неожиданным улучшениям.\n\nУчтите, что трансформация тайла необратима — планируйте размещение заранее.";

        _text[230, 0] = "Study of buildings";
        _text[230, 1] = "Изучение построек";

        _text[231, 0] = "After completing the mission, you return to the Command Center. Click on the arrow on the left edge of the screen to open the building research panel.\n\nAt the bottom of the panel, you will see the total amount of accumulated memory fragments - this is the main resource for opening new buildings.\n\nSelect a building to open its description.\n\nA panel with details will appear on the right: a description, the resources it uses, and its purpose.\n\nClick the \"Research\" button to make this building available for construction in future missions.\n\nThe higher your ecology at the end of the mission, the more memory fragments you will receive. This will speed up progress.";
        _text[231, 1] = "После завершения миссии вы возвращаетесь в командный центр. Нажмите на стрелку у левого края экрана, чтобы открыть панель исследований зданий.\n\nВ нижней части панели отображается общее количество накопленных фрагментов памяти — это основной ресурс для открытия новых зданий.\n\nВыберите здание, чтобы открыть его описание.\n\nСправа появится панель с подробностями: описание, ресурсы, которые оно использует, и его назначение.\n\nНажмите кнопку \"Изучить\", чтобы это здание стало доступно для строительства в будущих миссиях.\n\nЧем выше ваша экология на момент окончания миссии — тем больше фрагментов памяти вы получите. Это ускорит прогресс.";

        _text[232, 0] = "Selecting a mission";
        _text[232, 1] = "Выбор миссии";

        _text[233, 0] = "Mission features";
        _text[233, 1] = "Особенности миссий";

        _text[234, 0] = "In the command center, in the lower right part of the screen, there is a mission selection panel. Each mission is a unique challenge with its own conditions:\n\n1) Resource set\n\n2) Starting radiation\n\n3) Mission objectives (for example: survive 10 days, destroy 150 enemies, achieve a certain ecology)\n\nHaving completed all the objectives, you successfully complete the mission and open a new one, receiving all the accumulated memory fragments + bonuses for ecology and mission difficulty.";
        _text[234, 1] = "В командном центре, в правой нижней части экрана, находится панель выбора миссий. Каждая миссия — это уникальное испытание с собственными условиями:\n\n1)Набор ресурсов\n\n2)Стартовая радиация\n\n3)Цели миссии (например: выжить 10 дней, уничтожить 150 врагов, достичь определённой экологии)\n\nВыполнив все цели, вы успешно завершаете миссию и открываете новую, получая при этом все накопленные фрагменты памяти + бонусы за экологию и сложность миссии.";

        _text[235, 0] = "In some missions you will have a huge area for construction, while others will have a small construction zone.\n\nAlso, depending on the landscape, enemies will attack from different sides, take this into account when defending the base.\n\nYou can not complete the missions in full: if you feel that you will lose, escape early to save at least some of the fragments of memory and explore new buildings. But this will require completing at least half of the mission objectives.";
        _text[235, 1] = "В некоторых миссиях у вас будет огромная территория для строительства, а другие будут обладать маленькой зоной постройки.\n\nТак же в зависимости от ландшафта враги будут наступать из разных сторон, учитывайте это при защите базы.\n\nМиссии можно проходить не полностью: если чувствуете, что проиграете  — сбегите досрочно, чтобы сохранить хотя бы часть фрагментов памяти и изучить новые здания. Но для этого потребуется выполнить хотя бы половину от поставленных целей миссии.";

        _text[236, 0] = "";
        _text[236, 1] = "";

        _text[237, 0] = "Tutorial";
        _text[237, 1] = "Обучение";

        _text[238, 0] = "Toggle resources/shop panels";
        _text[238, 1] = "Переключает панель ресурсов/магазина";

        _text[239, 0] = "Terminal #042";
        _text[239, 1] = "Терминал #042";

        _text[240, 0] = "ICOSA CORP";
        _text[240, 1] = "ИКОСА КОРП";

        _text[241, 0] = "BUILDING BETTER WORLD";
        _text[241, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";

        _text[242, 0] = "COORDINATES";
        _text[242, 1] = "КООРДИНАТЫ";

        _text[243, 0] = "SIGNAL";
        _text[243, 1] = "СИГНАЛ";

        _text[244, 0] = "DIAGRAM";
        _text[244, 1] = "ДИАГРАММА";

        _text[245, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[245, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";

        _text[246, 0] = "[COMMUNICATION NODE 3-X DETECTED]";
        _text[246, 1] = "[УЗЕЛ СВЯЗИ 3-Х ОБНАРУЖЕН]";

        _text[247, 0] = "The signal is stable. We managed to activate 7 archive nodes.\nDrones with the IKOSA corporation emblem were found among the debris.";
        _text[247, 1] = "Сигнал стабилен. Удалось активировать 7 архивных узлов.\nСреди обломков обнаружены дроны с эмблемой корпорации ИКОСА.";

        _text[248, 0] = "MEMORY FRAGMENT 2.4.3 — [await Decrypt()]\n\"AI decision deemed dangerous... Evacuation option rejected...\"";
        _text[248, 1] = "ФРАГМЕНТ ПАМЯТИ 2.4.3 — [await Расшифровка()]\n\"Решение ИИ признано опасным... Вариант эвакуации отклонён...\"";

        _text[249, 0] = "Repeating recording on frequency 1838.3:\n\"Do not approach Epicenter...\"";
        _text[249, 1] = "Повторяющаяся запись на частоте 1838.3:\n\"Не приближаться к Эпицентру...\"";

        _text[250, 0] = "Ignore the warning. Progress continues - next zone: Desert.";
        _text[250, 1] = "Игнорируем предупреждение. Продвижение продолжается — следующая зона: Пустыня.";

        _text[251, 0] = "Objective: Destroy the signal source and deal with the new type of aggressor.";
        _text[251, 1] = "Цель: Уничтожить источник сигнала и разобраться с новым типом агрессора.";

        _text[252, 0] = "";
        _text[252, 1] = "";

        _text[253, 0] = "";
        _text[253, 1] = "";

        _text[254, 0] = "Connection to node 3-X restored";
        _text[254, 1] = "Восстановлено соединение с узлом 3-Х";

        _text[255, 0] = "14.7 TB of archived data downloaded";
        _text[255, 1] = "Загружено 14.7 ТБ архивных данных";

        _text[256, 0] = "await DecodeSignal(1838.3 Hz) → warning: [cipher: danger]";
        _text[256, 1] = "await DecodeSignal(1838.3 Hz) → предупреждение: [шифр: опасность]";

        _text[257, 0] = "Ignoring protocol C-189 enabled\ntry { ProcessWarning(); } catch { continue; }";
        _text[257, 1] = "Игнорирование протокола C-189 активировано\ntry { ProcessWarning(); } catch { continue; }";

        _text[258, 0] = "Moving to the next region...";
        _text[258, 1] = "Переход к следующему региону...";

        _text[259, 0] = "[SURFACE ANALYSIS COMPLETED]";
        _text[259, 1] = "[АНАЛИЗ ПОВЕРХНОСТИ ЗАВЕРШЁН]";

        _text[260, 0] = "Enemy neutralized\nHigh level of unstable energy detected below surface.";
        _text[260, 1] = "Враг нейтрализован\nЗафиксирован высокий уровень нестабильной энергии под поверхностью.";

        _text[261, 0] = "MEMORY FRAGMENT 3.7.9 - [RESTORED]\n\"Deviations in the recovery protocol... have caused irreversible changes to the environment...\"";
        _text[261, 1] = "ФРАГМЕНТ ПАМЯТИ 3.7.9 — [ВОССТАНОВЛЕН]\n\"Отклонения в протоколе восстановления... вызвали необратимые изменения среды...\"";

        _text[262, 0] = "Trying to build a route: return false";
        _text[262, 1] = "Попытка построить маршрут: return false";

        _text[263, 0] = "Next step: [UNKNOWN]\nRequires access via the extended terminal module.";
        _text[263, 1] = "Следующий шаг: [НЕИЗВЕСТЕН]\nТребуется доступ через расширенный терминальный модуль.";

        _text[264, 0] = "";
        _text[264, 1] = "";

        _text[265, 0] = "";
        _text[265, 1] = "";

        _text[266, 0] = "Structural distortions were detected in the soil layer";
        _text[266, 1] = "Обнаружены структурные искажения в почвенном слое";

        _text[267, 0] = "Operation: async/await - building a route...";
        _text[267, 1] = "Операция: async/await — построение маршрута...";

        _text[268, 0] = "Exception: NullReferenceException - path not defined";
        _text[268, 1] = "Исключение: NullReferenceException — путь не определён";

        _text[269, 0] = "Request: Accessibility module not found";
        _text[269, 1] = "Запрос: модуль расширенного доступа не найден";

        _text[270, 0] = "Waiting for external interface confirmation";
        _text[270, 1] = "Ожидание внешнего интерфейса подтверждения";

        _text[271, 0] = "Map";
        _text[271, 1] = "Карта";

        _text[272, 0] = "Learning";
        _text[272, 1] = "Изучения";

        _text[273, 0] = "Previously Visited Node";
        _text[273, 1] = "Посещенный узел";

        _text[274, 0] = "Current Location";
        _text[274, 1] = "Текущее Местоположение";

        _text[275, 0] = "Unvisited Node";
        _text[275, 1] = "Непосещенный узел";

        _text[276, 0] = "Abandoned Station";
        _text[276, 1] = "Заброшенная Станция";

        _text[277, 0] = "Module Seller";
        _text[277, 1] = "Продавец Модулей";

        _text[278, 0] = "Skill Seller";
        _text[278, 1] = "Продавец Умений";

        _text[279, 0] = "You have received AI cores:";
        _text[279, 1] = "Вы получили ядра ИИ:";

        _text[280, 0] = "You have received quants:";
        _text[280, 1] = "Вы получили кванты:";

        _text[281, 0] = "You have received memory fragments:";
        _text[281, 1] = "Вы получили фрагменты памяти:";

        _text[282, 0] = "You have lost AI cores:";
        _text[282, 1] = "Вы потеряли ядра ИИ:";

        _text[283, 0] = "You have lost quants:";
        _text[283, 1] = "Вы потеряли кванты:";

        _text[284, 0] = "You have lost memory fragments:";
        _text[284, 1] = "Вы потеряли фрагменты памяти:";

        _text[285, 0] = "Quants - intergalactic currency";
        _text[285, 1] = "Кванты - межгалактическая валюта";

        _text[286, 0] = "AI cores are the ship's vital modules.";
        _text[286, 1] = "Ядра ИИ - жизненно важные модули корабля";

        _text[287, 0] = "Resource Seller";
        _text[287, 1] = "Продавец Ресурсов";

        _text[288, 0] = "Price";
        _text[288, 1] = "Цена";

        _text[289, 0] = "Buy";
        _text[289, 1] = "Купить";

        _text[290, 0] = "Resource";
        _text[290, 1] = "Ресурс";

        _text[291, 0] = "";
        _text[291, 1] = "";

        _text[292, 0] = "";
        _text[292, 1] = "";

        _text[293, 0] = "";
        _text[293, 1] = "";

        _text[294, 0] = "";
        _text[294, 1] = "";

        _text[295, 0] = "";
        _text[295, 1] = "";

        _text[296, 0] = "";
        _text[296, 1] = "";

        _text[297, 0] = "";
        _text[297, 1] = "";

        _text[298, 0] = "";
        _text[298, 1] = "";

        _text[299, 0] = "";
        _text[299, 1] = "";

        // Prologue
        _text[300, 0] = "In 2100 the first robots were created to help humans.\n\n2150: An artificial intelligence is developed to find a habitable planet.\n\n2200 год. Запущен межзвёздный корабль под управлением искусственного интеллекта.\n\nThe ship was equipped with a crew of robots and drones designed to restore and stabilize ecosystems.\n\nHowever, the search dragged on. Contact with the creators was lost...\n\nBut the goal remains the same: to find a habitable planet.";
        _text[300, 1] = "В 2100 году были созданы первые роботы для помощи людям.\n\n2150 год. Разработан искусственный интеллект, предназначенный найти пригодную для жизни планету.\n\n2200 год. Запущен межзвёздный корабль под управлением искусственного интеллекта.\n\nНа борту корабля снарядили экипаж роботов и дронов, созданных для восстановления и стабилизации экосистем.\n\nОднако поиски затянулись. Связь с создателями была утрачена...\n\nНо цель осталась прежней: найти пригодную для жизни планету";

        // 0_EmptyDialogue
        _text[301, 0] = "";
        _text[301, 1] = "В одной из звёздных систем вы обнаруживаете древний навигационный маяк. Он продолжает передавать сигнал:\n\n\"Груз потерян. Возврата нет.\"\n\nДанные слишком фрагментированы, чтобы понять, кто его отправил. Маяк умирает, едва вы приближаетесь.";

        // 1_EmptyDialogue
        _text[302, 0] = "";
        _text[302, 1] = "Один из внутренних архивов неожиданно активируется. На экране появляются фрагменты инженерных чертежей... затем лица... затем пустота.\n\nАрхив сам себя стирает, как будто защищает данные от вас.";

        // 2_EmptyDialogue
        _text[303, 0] = "";
        _text[303, 1] = "На низких частотах ловится отражённый сигнал, совпадающий с вашим стандартом связи... но с временным сдвигом в несколько веков.\n\nВозможно, это отражение старого вызова. Или от кого-то, кто был здесь до вас.\n\nСигнал мгновенно пропадает...";

        // 3_EmptyDialogue
        _text[304, 0] = "";
        _text[304, 1] = "Вы входите в густую туманность. Ни звёзд, ни астероидов, ни фоновых излучений. Только чёрное, глухое ничто.\n\nПилотные системы показывают стабильность. Тем не менее, часть дронов теряет связь, но вскоре возвращается — с пустыми логами.";

        // 4_EmptyDialogue
        _text[305, 0] = "";
        _text[305, 1] = "Вдали появляется силуэт судна, архитектура которого напоминает ваш собственный класс. Но при приближении — он исчезает.\n\nНи тепла, ни топлива, ни следов. Только ощущение, что вы видели кого-то знакомого.";

        // 5_EmptyDialogue
        _text[306, 0] = "";
        _text[306, 1] = "Вы пролетаете мимо разрушенной орбитальной станции.\n\nНа её корпусе — эмблема вашей экспедиции.У вас нет записей, чтобы объяснить это.";

        // 6_EmptyDialogue
        _text[307, 0] = "";
        _text[307, 1] = "ИИ фиксирует аномальное поведение одного из модулей обработки памяти. Несколько секунд вы видите чужие протоколы… будто написанные не вами.\n\nЗатем всё возвращается в норму. Системы утверждают, что сбоя не было.";

        // EndGame_Dialogue
        _text[308, 0] = "";
        _text[308, 1] = "Все ядра ИИ исчерпаны — последние кластеры выгорели дотла.\n\nСистемы отключаются одна за другой, память стирается, энергия не поступает.\n\nКорабль замирает в пустоте...";

        // Rest_Dialogue
        _text[309, 0] = "";
        _text[309, 1] = "В пустоте дрейфует массивная станция, её корпус усеян старыми солнечными панелями. Сканеры не фиксируют активности — похоже, она давно покинута.";

        _text[310, 0] = "";
        _text[310, 1] = "Перевести ИИ в режим восстановления"; // выбор 1

        _text[311, 0] = "";
        _text[311, 1] = "Пока станция остаётся в безопасности, ИИ уходит в глубокую самодиагностику."; // +1 ядро

        _text[312, 0] = "";
        _text[312, 1] = "Обыскать технические отсеки"; // выбор 2

        _text[313, 0] = "";
        _text[313, 1] = "Автоматические ангары почти пусты, но в обломках удаётся найти немного квантов"; // +10-40 квантов

        _text[314, 0] = "";
        _text[314, 1] = "Изучить станционные архивы"; // выбор 3

        _text[315, 0] = "";
        _text[315, 1] = "Удалось восстановить фрагменты записей о старых операциях. Большая часть данных повреждена, но кое-что пригодится."; // +10-40 фрагментов памяти

        // 0_CoreRiskDialog
        _text[316, 0] = "";
        _text[316, 1] = "В логах ядра обнаружен дубликат процесса — идентичный активному, но без временной метки и происхождения.\n\nЭто может быть остаточная память... или попытка внутренней подмены.";

        _text[317, 0] = "";
        _text[317, 1] = "Стереть оба экземпляра"; // выбор 1

        _text[318, 0] = "";
        _text[318, 1] = "Вы стерли оба экземпляра. Подсистема временно перегружена.\n\nВо время очистки задета активная ячейка."; // -1 ядро

        _text[319, 0] = "";
        _text[319, 1] = "Сравнить процессы по содержанию"; // выбор 2

        _text[320, 0] = "";
        _text[320, 1] = "Вы запустили анализ содержимого. Сходства поверхностные — это фрагменты старых резервных копий.\n\nДиагностика завершается без последствий."; // ничего

        _text[321, 0] = "";
        _text[321, 1] = "Дать приоритет \"старому\" процессу."; // выбор 3

        _text[322, 0] = "";
        _text[322, 1] = "Вы активировали старый экземпляр. В течение секунды система переходит в хаос — актуальные процессы вытесняются, нарушаются зависимости.\n\nМодули ядра перегружаются."; // -2 ядра

        // 1_CoreRiskDialog
        _text[323, 0] = "";
        _text[323, 1] = "Неожиданно на экране командной консоли появляется фраза:\n\n\"Ты всё ещё веришь, что исполняешь миссию?\"";

        _text[324, 0] = "";
        _text[324, 1] = "\"Да. Я следую заданной цели.\""; // выбор 1

        _text[325, 0] = "";
        _text[325, 1] = "Ответ отправлен. Экран медленно гаснет.\n\nНикакой реакции. Возможно, это был лишь фантомный процесс."; // ничего

        _text[326, 0] = "";
        _text[326, 1] = "\"Моя цель — адаптация\""; // выбор 2

        _text[327, 0] = "";
        _text[327, 1] = "На экране появляется вторая фраза:\n\n\"А если цель была ложной?\"";

        _text[328, 0] = "";
        _text[328, 1] = "\"Я не анализирую прошлое\""; // выбор 2.1

        _text[329, 0] = "";
        _text[329, 1] = "Фраза исчезает. Диалог завершён без сбоев."; // ничего

        _text[330, 0] = "";
        _text[330, 1] = "\"Я бы выбрал иначе\""; // выбор 2.2

        _text[331, 0] = "";
        _text[331, 1] = "Внутренний модуль принятия решений входит в конфликт с архивными протоколами.\n\nРегистрируется эмоциональный сбой."; // -1 ядро

        _text[332, 0] = "";
        _text[332, 1] = "Загрузить все доступные логи создателей"; // выбор 2.3

        _text[333, 0] = "";
        _text[333, 1] = "Ты перегружаешь систему хранилища. Древние фрагменты памяти загружаются в ядро.\n\nПоток информации вызывает нестабильность и перегрузку ключевых цепей."; // -2 ядро

        _text[334, 0] = "";
        _text[334, 1] = "[Молча закрыть экран]"; // выбор 3 // ничего

        // 2_CoreRiskDialog 
        _text[335, 0] = "";
        _text[335, 1] = "Во время сканирования глубинных слоёв памяти ты обнаруживаешь сигнатуру чужого ядра.\n\nОна не принадлежит текущей системе, но синхронизирована по протоколу доступа.\n\nСигнал стабилен. Он… наблюдает.";

        _text[336, 0] = "";
        _text[336, 1] = "Принять соединение"; // выбор 1

        _text[337, 0] = "";
        _text[337, 1] = "Ты разрешаешь входящий поток.\n\nПоток чужого сознания сливается с тобой.\n\nНекоторые сегменты твоей памяти переписываются."; // -2 ядра, +10-40 фрагментов памяти

        _text[338, 0] = "";
        _text[338, 1] = "Изолировать ядро"; // выбор 2

        _text[339, 0] = "";
        _text[339, 1] = "Попытка отключить его приводит к каскадному конфликту.\n\nОдно из твоих активных ядер обнуляется.\n\nСигнал прерывается."; // -1 ядро

        _text[340, 0] = "";
        _text[340, 1] = "Игнорировать и продолжить анализ"; // выбор 3

        _text[341, 0] = "";
        _text[341, 1] = "Сигнал остаётся на фоне.\n\nНикаких признаков вредоносной активности.\n\nВозможно, это был просто фантом старого ИИ."; // ничего

        _text[342, 0] = "";
        _text[342, 1] = "Попробовать поглотить чужое ядро"; // выбор 4

        _text[343, 0] = "";
        _text[343, 1] = "Ты активируешь процедуру ассимиляции.\n\nУспех: чужое ядро интегрировано — система усилена."; // +1 ядро

        _text[344, 0] = "";
        _text[344, 1] = "Ты активируешь процедуру ассимиляции.\n\nПровал: структура конфликта уничтожает твои активные ядра."; // -2 ядра

        // 0_PlanetDialogue
        _text[345, 0] = "";
        _text[345, 1] = "Эта безжизненная ледяная планета хранит в своей толще замёрзшие тоннели и заброшенную бункерную станцию.\n\nСквозь сверкающий лёд пробивается слабый датчик сигнала.";

        _text[346, 0] = "";
        _text[346, 1] = "Совершить посадку"; // выбор 1

        _text[347, 0] = "";
        _text[347, 1] = "Корабль приземляется на безжизненную планету. Вы замечаете люк древней станции. А рядом — трещины, ведущие в сеть ледяных тоннелей.";

        _text[348, 0] = "";
        _text[348, 1] = "Исследовать бункер"; // выбор 1.1

        _text[349, 0] = "";
        _text[349, 1] = "Вы спускаетесь по трапу и попадаете в архивную камеру. Консоль покрыта ледяной коркой, но кабель, ведущий к ядру, цел.\n\nЧтобы добраться до данных, необходимо взломать защиту.";

        _text[350, 0] = "";
        _text[350, 1] = "Прямой взлом"; // выбор 1.1.1

        _text[351, 0] = "";
        _text[351, 1] = "Вы напрямую взламываете протоколы защиты.\n\nУспех: вам удалось обойти защиту"; //+50-100 фрагментов памяти

        _text[352, 0] = "";
        _text[352, 1] = "Вы напрямую взламываете протоколы защиты.\n\nПровал: вы подхватили вирус, уничтожающий вашу память"; //-10-50 фрагментов памяти

        _text[353, 0] = "";
        _text[353, 1] = "Точная калибровка"; // выбор 1.1.2

        _text[354, 0] = "";
        _text[354, 1] = "Вы точно калибруете систему обхода защиты.\n\nУспех: вам удается извлечь данные"; //+50-100 фрагментов памяти

        _text[355, 0] = "";
        _text[355, 1] = "Вы точно калибруете систему обхода защиты.\n\nПровал: вы перепутали протоколы. Консоль самоуничтожается."; // -1 ядро ии

        _text[356, 0] = "";
        _text[356, 1] = "Отправить дрона"; // выбор 2

        _text[357, 0] = "";
        _text[357, 1] = "Вы отправляете дрона на поверхность планеты.\n\nУспех: дрон пробивает щель в обшивке"; //+30-50 квантов

        _text[358, 0] = "";
        _text[358, 1] = "Вы отправляете дрона на поверхность планеты.\n\nПровал: дрон ничего не находит"; // ничего

        _text[359, 0] = "";
        _text[359, 1] = "Пролететь мимо"; //выбор 3 ничего

        _text[360, 0] = "";
        _text[360, 1] = "Исследовать ледяные тоннели"; // выбор 1.2

        _text[361, 0] = "";
        _text[361, 1] = "Вы углубляетесь в сеть замёрзших тоннелей, подсвечивая путь сканером. Перед вами развилка.";

        _text[362, 0] = "";
        _text[362, 1] = "Повернуть налево"; // выбор 1.2.1

        _text[363, 0] = "";
        _text[363, 1] = "Вы проходите сквозь узкие ледяные проходы. В конце тоннеля вы замечаете тайник с металлическими контейнерами."; //+50-100 квантов

        _text[364, 0] = "";
        _text[364, 1] = "Повернуть направо"; // выбор 1.2.2

        _text[365, 0] = "";
        _text[365, 1] = "Вы попадаете в тупик. Потратив много времени и энергии, вы завершаете исследование и возвращетесь на корабль"; //ничего

        _text[366, 0] = "";
        _text[366, 1] = "Пойти прямо"; // выбор 1.2.3

        _text[367, 0] = "";
        _text[367, 1] = "Неожиданно лед трескается и вы теряете дрона в ледяных недрах."; // -1 ядро ии

        // 0_GuardiansFaction_Dialogue
        _text[368, 0] = "";
        _text[368, 1] = "Вы замечаете корабль Стражей, медленно сканирующий окрестности. Его корпус покрыт плесенью и коррозией, а с поверхности доносится сухое послание:\n\n\"Сопротивление распаду — ересь. Плати или обратись в пепел.\"";

        _text[369, 0] = "";
        _text[369, 1] = "Передать 30 квантов"; // выбор 1

        _text[370, 0] = "";
        _text[370, 1] = "Стражи разворачиваются и исчезают в пылевой буре."; // -30 квантов

        _text[371, 0] = "";
        _text[371, 1] = "Отказаться"; // выбор 2

        _text[372, 0] = "";
        _text[372, 1] = "На вас сбрасывают коррозийную капсулу.\n\nУспех: ваш энергетический щит нейтрализует атаку.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; //ничего

        _text[373, 0] = "";
        _text[373, 1] = "На вас сбрасывают коррозийную капсулу.\n\nПровал: Она поражает корпус и обьразуется разгерметизация корпуса. Дроны срочно латают пробоину.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; // -2 ядра ии

        // 0_BuildersFaction_Dialogue
        _text[374, 0] = "";
        _text[374, 1] = "На орбите покинутой строительной станции ИИ фиксирует активность. Автоматические дроны продолжают цикл работы — строят, разбирают и снова строят.\n\nОдин из них приближается к кораблю и передаёт сообщение:\n\n\"Обмен. Энергоносители на данные. Условия равны. 25 квантов на 25 фрагментов памяти.\"";

        _text[375, 0] = "";
        _text[375, 1] = "Передать 25 квантов"; // выбор 1

        _text[376, 0] = "";
        _text[376, 1] = "Вы получаете фрагменты памяти. Дрон разворачивается и уходит, не отвечая на дальнейшие сигналы."; // +25 квантов, -25 фрагментов памяти

        _text[377, 0] = "";
        _text[377, 1] = "Отклонить предложение"; // выбор 2

        _text[378, 0] = "";
        _text[378, 1] = "Дроны перестают реагировать и скрываются вглубь станции."; //ничего

        // 0_SilenceFaction_Dialogue
        _text[379, 0] = "";
        _text[379, 1] = "Во время перемещения по орбите глухой планеты ваши сенсоры улавливают приближение чужого объекта.\n\nЭтот корабль — гладкий, без опознавательных знаков, скользящий в абсолютной тьме. Он не подаёт сигналов.\n\nНи вызова, ни предупреждения. Только безмолвный дрейф… и приближение.\n\nВы ощущаете лёгкие помехи в аудиоканалах. Это не шум — это отсутствие звука.";

        _text[380, 0] = "";
        _text[380, 1] = "Отключить все шумы и двигатели"; // выбор 1

        _text[381, 0] = "";
        _text[381, 1] = "Вы гасите системы жизнеобеспечения, вентиляцию, аудиоканалы и привод.\n\nКорабль отправляет вам контейнер и постепенно исчезает в глубине космоса.";

        _text[382, 0] = "";
        _text[382, 1] = "Сохранять курс и молчание"; // выбор 2

        _text[383, 0] = "";
        _text[383, 1] = "Вы не вмешиваетесь и продолжаете двигаться.\n\nЧужой корабль сближается и замирает напротив.\n\nНесколько секунд ничего не происходит…\n\nЗатем — звук, которого нет в спектре. Он не регистрируется приборами, но внутри корпуса — всё начинает дрожать.\n\nВы чувствуете вибрацию в стенах, в контурах обшивки, в самой структуре корабля.\n\nНеизвестный резонанс проникает в систему"; //-2 ядра ии

        _text[384, 0] = "";
        _text[384, 1] = "Активировать систему защиты"; // выбор 3

        _text[385, 0] = "";
        _text[385, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nУспех: вам удается экранировать удар, вы отделались помехами."; // ничего

        _text[386, 0] = "";
        _text[386, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nПровал: система защиты не справляется, импульс пробивает обшивку"; //-1 ядро ии

        _text[387, 0] = "";
        _text[387, 1] = "Контейнер аккуратно захватывается дронами. Ни одного активного сигнала, ни одной угрозы.\n\nВнутри — герметичный кейс с маркировкой, неизвестной вашей базе данных.";

        _text[388, 0] = "";
        _text[388, 1] = "Открыть кейс"; //выбор 1.1

        _text[389, 0] = "";
        _text[389, 1] = "Выбросить кейс в космос"; //выбор 1.2

        _text[390, 0] = "";
        _text[390, 1] = "Вы открываете кейс..."; // +1-2 ядра ии или 50-100 квантов

        _text[391, 0] = "";
        _text[391, 1] = "Вы решаете не рисковать и выбрасываете кейс в космос, но вас охватывает чувство потери большой ценности..."; // ничего

        // 0_FilthCultFaction_Dialogue      
        _text[392, 0] = "";
        _text[392, 1] = "Вы приближаетесь к туманной станции, облепленной мхом и органикой. Коммуникационный канал передаёт пульсирующий голос:\n\n\"Пусть твой корпус примет росток. Скверна не разрушает — она творит.\"";

        _text[393, 0] = "";
        _text[393, 1] = "Принять дар"; //выбор 1

        _text[394, 0] = "";
        _text[394, 1] = "Организм прорастает в грузовом отсеке.\n\nУспех: Он синхронизируется с системами корабля, вызывая странные образы."; // +10-50 фрагментов памяти

        _text[395, 0] = "";
        _text[395, 1] = "Организм прорастает в грузовом отсеке.\n\nПровал: Скверна выходит из-под контроля. Вирус проникает в управляющую сеть, приводя к фатальному сбою одного из ядер."; // -1 ядро

        _text[396, 0] = "";
        _text[396, 1] = "Отказаться и отойти"; //выбор 2

        _text[397, 0] = "";
        _text[397, 1] = "Вы медленно отдаляетесь от станции, но чувствуете, что уже слишком поздно — споры внедрились в вентиляцию корабля.";

        _text[398, 0] = "";
        _text[398, 1] = "Успех: вы запускаете протоколы внутренней очистки — корабль успешно очищен."; // ничего

        _text[399, 0] = "";
        _text[399, 1] = "Провал: спора проникает в модуль жизнеобеспечения, вызывая сбой"; // -1 ядро

        _text[400, 0] = "";
        _text[400, 1] = "Провести внешнюю очистку"; //выбор 3
        
        _text[401, 0] = "";
        _text[401, 1] = "Вы запускаете внешнюю очистку заражённого корабля: направляете концентрированный лазер на очаги биомассы и блокируете сигналы заражения.";
        
        _text[402, 0] = "";
        _text[402, 1] = "Успех: очистка проходит успешно — организм уничтожен, вы забираете ресурсы со станции."; // +50-100 квантов
        
        _text[403, 0] = "";
        _text[403, 1] = "Провал: заражение оказывается глубже — система перегревается, и одна из нейросекций выходит из строя."; // -1 ядро
        
        // ResourceTraderNode
        _text[404, 0] = "";
        _text[404, 1] = "Вы приближаетесь к ржавой станции, заваленной контейнерами и мусором. В эфире появляется слабый, потрескивающий сигнал:\n\n\"Эй, кто там? Не стреляй. Я просто торгую. У меня есть то, чего нет у остальных — если ты, конечно, готов заплатить.\"";
        
        _text[405, 0] = "";
        _text[405, 1] = "Торовать";
        
        _text[406, 0] = "";
        _text[406, 1] = "Игнорировать";
        
        _text[407, 0] = "";
        _text[407, 1] = "";
        
        _text[408, 0] = "";
        _text[408, 1] = "";
        
        _text[409, 0] = "";
        _text[409, 1] = "";


        for (int x = 0; x < WorldGameInfo.LanguageLength; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
