using UnityEngine;
using Steamworks;

// 0 - English						en
// 1 - Russian						ru

public class Language : MonoBehaviour
{
    public static int LanguageNumber = 0;
    private string[,] _text = new string[WorldGameInfo.LanguageLength, 2];
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

        _text[1, 0] = "Recipe";
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
        _text[31, 1] = "Вы уверены, что хотите начать новую игру?\n\nВаше прошлое сохранение будет перезаписано.";

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

        _text[43, 0] = "You need to open";
        _text[43, 1] = "Вам нужно открыть";

        _text[44, 0] = "Escape";
        _text[44, 1] = "Сбежать";

        _text[45, 0] = "Restart";
        _text[45, 1] = "Перезапуск";

        _text[46, 0] = "Exit";
        _text[46, 1] = "Выход";

        _text[47, 0] = "Main menu";
        _text[47, 1] = "Меню";

        _text[48, 0] = $"Are you sure you want to restart the mission?\n\n<color={Colors.HexColorWarningYellow}>You will lose one AI core.</color>";
        _text[48, 1] = $"Вы уверены, что хотите перезапустить миссию?\n\n<color={Colors.HexColorWarningYellow}>Вы потеряете одно ядро ИИ.</color>";

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

        _text[62, 0] = "Data fragments received:";
        _text[62, 1] = "Получено фрагментов данных:";

        _text[63, 0] = "Victory";
        _text[63, 1] = "Победа";

        _text[64, 0] = "Defeat";
        _text[64, 1] = "Поражение";

        _text[65, 0] = "Escape";
        _text[65, 1] = "Сбежал";

        _text[66, 0] = $"<color={Colors.HexColorWarningYellow}>Escaping the mission will give you {WorldGameInfo.EscapeFragmentsPercent}% of the data fragments and losing one AI core.</color>\n\nYou must complete half of the objectives.";
        _text[66, 1] = $"<color={Colors.HexColorWarningYellow}>Сбежав с миссии, вы получите {WorldGameInfo.EscapeFragmentsPercent}% от фрагментов данных и потеряете одно ядро ИИ.</color>\n\nНеобходимо выполнить половину поставленных целей.";

        _text[67, 0] = "Save the mission and return to command center?";
        _text[67, 1] = "Сохранить миссию и вернуться в командный центр?";

        _text[68, 0] = $"Restart mission?\n\n<color={Colors.HexColorWarningYellow}>You will lose one AI core.</color>";
        _text[68, 1] = $"Перезапустить миссию?\n\n<color={Colors.HexColorWarningYellow}>Вы потеряете одно ядро ИИ.</color>";

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

        _text[75, 0] = "in";
        _text[75, 1] = "в";

        _text[76, 0] = "You can't restart the mission. You don't have any spare AI cores.";
        _text[76, 1] = "Вы не можете перезапустить миссию. У вас нет запасных ядер ИИ.";

        _text[77, 0] = "Launch";
        _text[77, 1] = "Запуск";

        _text[78, 0] = "Back";
        _text[78, 1] = "Назад";

        _text[79, 0] = "to repair cost";
        _text[79, 1] = "стоимость ремонта";

        _text[80, 0] = "to building durability";
        _text[80, 1] = "к прочности зданий";

        _text[81, 0] = "to turret damage";
        _text[81, 1] = "к урону турелей";

        _text[82, 0] = "Passive ability";
        _text[82, 1] = "Пассивная способность";

        _text[83, 0] = "Shard";
        _text[83, 1] = "Осколок";

        _text[84, 0] = "Robots";
        _text[84, 1] = "Роботы";

        _text[85, 0] = "";
        _text[85, 1] = "";

        _text[86, 0] = "You cannot restart the mission.\n\nYou have no spare AI cores.";
        _text[86, 1] = "Вы не можете начать миссию с начала.\n\nУ вас нет запасных ядер ИИ.";

        _text[87, 0] = "";
        _text[87, 1] = "";

        _text[88, 0] = "";
        _text[88, 1] = "";

        _text[89, 0] = "";
        _text[89, 1] = "";

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

        _text[117, 0] = "Anti-Aliasing";
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

        _text[147, 0] = "Data restored:";
        _text[147, 1] = "Восстановлено данных:";

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

        _text[175, 0] = "Data Fragment";
        _text[175, 1] = "Фрагмент Данных";

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

        _text[183, 0] = "You have received";
        _text[183, 1] = "Вы получили";

        _text[184, 0] = "You have lost";
        _text[184, 1] = "Вы потеряли";

        _text[185, 0] = "Ai Core";
        _text[185, 1] = "Ядро ИИ";

        _text[186, 0] = "Quant";
        _text[186, 1] = "Квант";

        _text[187, 0] = "Quant received:";
        _text[187, 1] = "Получено квант";

        _text[188, 0] = "Scout";
        _text[188, 1] = "Разведчик";

        _text[189, 0] = "Engineer";
        _text[189, 1] = "Инженер";

        _text[190, 0] = "Aim Bot";
        _text[190, 1] = "Аим Бот";

        _text[191, 0] = "Patch-08";
        _text[191, 1] = "Патч-08";

        _text[192, 0] = "Titan";
        _text[192, 1] = "Титан";

        _text[193, 0] = "Functional";
        _text[193, 1] = "Функционал";

        _text[194, 0] = "unavailable";
        _text[194, 1] = "недоступно";

        _text[195, 0] = "explores the surrounding area in search of resources";
        _text[195, 1] = "исследует окрестности в поисках ресурсов";

        _text[196, 0] = "repairs the specified buildings";
        _text[196, 1] = "ремонтирует указанные здания";

        _text[197, 0] = "attacks enemy creatures";
        _text[197, 1] = "атакует вражеских существ";

        _text[198, 0] = "Combat";
        _text[198, 1] = "Боевой";

        _text[199, 0] = "Base Crate";
        _text[199, 1] = "Базовый Контейнер";

        _text[200, 0] = "Metal Crate";
        _text[200, 1] = "Металлический Контейнер";

        _text[201, 0] = "Supply Crate";
        _text[201, 1] = "Контейнер Снабжения";

        _text[202, 0] = "Crates";
        _text[202, 1] = "Контейнеры";

        _text[203, 0] = "The radiation level is starting to increase gradually. Be careful.";
        _text[203, 1] = "Уровень радиации начинает постепенно расти. Будьте осторожны.";

        _text[204, 0] = "Average increase in background radiation registered. Prepare for possible consequences.";
        _text[204, 1] = "Зарегистрирован средний рост радиационного фона. Подготовьтесь к возможным последствиям.";

        _text[205, 0] = "Warning! A sharp increase in radiation is expected. Take protective measures immediately.";
        _text[205, 1] = "Внимание! Ожидается резкий скачок радиации. Срочно примите защитные меры.";

        _text[206, 0] = "The radiation level is gradually decreasing, making conditions safer.";
        _text[206, 1] = "Уровень радиации постепенно снижается – условия становятся безопаснее.";

        _text[207, 0] = "Average decrease in radiation level recorded. Threat level is falling.";
        _text[207, 1] = "Среднее снижение уровня радиации зафиксировано. Уровень угрозы падает.";

        _text[208, 0] = "A sharp drop in radiation has been recorded. The environment is being restored.";
        _text[208, 1] = "Зафиксировано резкое падение радиации. Окружающая среда восстанавливается.";

        _text[209, 0] = "Precipitation analysis indicates high acidity. Rain is expected.";
        _text[209, 1] = "Анализ осадков указывает на высокую кислотность. Ожидается дождь.";

        _text[210, 0] = "Orbital scanners have detected a meteor shower – prepare for strikes from the skies.";
        _text[210, 1] = "Орбитальные сканеры выявили метеорный поток – готовьтесь к ударам с небес.";

        _text[211, 0] = "Seismic sensors are recording powerful tremors – an earthquake is approaching.";
        _text[211, 1] = "Сейсмические датчики фиксируют мощные подземные толчки – приближается землетрясение.";

        _text[212, 0] = "Toxic compounds have been detected in the atmosphere. The wind is carrying a dangerous gas.";
        _text[212, 1] = "В атмосфере обнаружены токсичные соединения. Ветер несёт опасный газ.";

        _text[213, 0] = "Underground pressure is increasing. A spontaneous release of oil to the surface is possible.";
        _text[213, 1] = "Подземное давление растёт. Возможен самопроизвольный выброс нефти на поверхность.";

        _text[214, 0] = "Repairs all marked buildings.";
        _text[214, 1] = "Ремонтирует все помеченные здания.";

        _text[215, 0] = "Fortification";
        _text[215, 1] = "Укрепление";

        _text[216, 0] = "For one day, reduces damage to all buildings by 2 times.";
        _text[216, 1] = "На один день, уменьшает урон по всем зданиям в 2 раза.";

        _text[217, 0] = "Production optimization";
        _text[217, 1] = "Оптимизация производства";

        _text[218, 0] = "For one day, increases resource production by 2 times.";
        _text[218, 1] = "На один день, увеличивает добычу ресурсов в 2 раза.";

        _text[219, 0] = "Ignite";
        _text[219, 1] = "Поджег";

        _text[220, 0] = "Creates uncontrollable flames. Deals damage to both enemies and your buildings.";
        _text[220, 1] = "Создает неконтролируемое пламя. Наносит урон как врагам, так и вашим постройкам.";

        _text[221, 0] = "You must select at least one skill";
        _text[221, 1] = "Необходимо выбрать хотя бы одно умение";

        _text[222, 0] = "Toggle resources panel";
        _text[222, 1] = "Переключает панель ресурсов";

        _text[223, 0] = "Cancel skill targeting";
        _text[223, 1] = "Отменить прицел умения";

        _text[224, 0] = "Toggle skills panel";
        _text[224, 1] = "Переключает панель умений";

        _text[225, 0] = "Go";
        _text[225, 1] = "Перейти";

        _text[226, 0] = "PLANET";
        _text[226, 1] = "ПЛАНЕТА";

        _text[227, 0] = "SHIP";
        _text[227, 1] = "КОРАБЛЬ";

        _text[228, 0] = "Steel Riffle";
        _text[228, 1] = "Стальная Винтовка";

        _text[229, 0] = "Titanium Rocket Launcher";
        _text[229, 1] = "Титановая Ракетная Установка";

        _text[230, 0] = "";
        _text[230, 1] = "";

        _text[231, 0] = "";
        _text[231, 1] = "";

        _text[232, 0] = "";
        _text[232, 1] = "";

        _text[233, 0] = "";
        _text[233, 1] = "";

        _text[234, 0] = "";
        _text[234, 1] = "";

        _text[235, 0] = "";
        _text[235, 1] = "";

        _text[236, 0] = "";
        _text[236, 1] = "";

        _text[237, 0] = "";
        _text[237, 1] = "";

        _text[238, 0] = "";
        _text[238, 1] = "";

        _text[239, 0] = "";
        _text[239, 1] = "";

        _text[240, 0] = "";
        _text[240, 1] = "";

        _text[241, 0] = "";
        _text[241, 1] = "";

        _text[242, 0] = "";
        _text[242, 1] = "";

        _text[243, 0] = "";
        _text[243, 1] = "";

        _text[244, 0] = "";
        _text[244, 1] = "";

        _text[245, 0] = "";
        _text[245, 1] = "";

        _text[246, 0] = "";
        _text[246, 1] = "";

        _text[247, 0] = "";
        _text[247, 1] = "";

        _text[248, 0] = "";
        _text[248, 1] = "";

        _text[249, 0] = "";
        _text[249, 1] = "";

        _text[250, 0] = "";
        _text[250, 1] = "";

        _text[251, 0] = "";
        _text[251, 1] = "";

        _text[252, 0] = "";
        _text[252, 1] = "";

        _text[253, 0] = "";
        _text[253, 1] = "";

        _text[254, 0] = "";
        _text[254, 1] = "";

        _text[255, 0] = "";
        _text[255, 1] = "";

        _text[256, 0] = "";
        _text[256, 1] = "";

        _text[257, 0] = "";
        _text[257, 1] = "";

        _text[258, 0] = "";
        _text[258, 1] = "";

        _text[259, 0] = "";
        _text[259, 1] = "";

        _text[260, 0] = "";
        _text[260, 1] = "";

        _text[261, 0] = "";
        _text[261, 1] = "";

        _text[262, 0] = "";
        _text[262, 1] = "";

        _text[263, 0] = "";
        _text[263, 1] = "";

        _text[264, 0] = "";
        _text[264, 1] = "";

        _text[265, 0] = "";
        _text[265, 1] = "";

        _text[266, 0] = "";
        _text[266, 1] = "";

        _text[267, 0] = "";
        _text[267, 1] = "";

        _text[268, 0] = "";
        _text[268, 1] = "";

        _text[269, 0] = "";
        _text[269, 1] = "";

        _text[270, 0] = "";
        _text[270, 1] = "";

        _text[271, 0] = "";
        _text[271, 1] = "";

        _text[272, 0] = "";
        _text[272, 1] = "";

        _text[273, 0] = "";
        _text[273, 1] = "";

        _text[274, 0] = "";
        _text[274, 1] = "";

        _text[275, 0] = "";
        _text[275, 1] = "";

        _text[276, 0] = "Abandoned Station";
        _text[276, 1] = "Заброшенная Станция";

        _text[277, 0] = "Unvisited Node";
        _text[277, 1] = "Непосещенный узел";

        _text[278, 0] = "Terminal #042";
        _text[278, 1] = "Терминал #042";

        _text[279, 0] = "ICOSA CORP";
        _text[279, 1] = "ИКОСА КОРП";

        _text[280, 0] = "BUILDING BETTER WORLD";
        _text[280, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";

        _text[281, 0] = "COORDINATES";
        _text[281, 1] = "КООРДИНАТЫ";

        _text[282, 0] = "SIGNAL";
        _text[282, 1] = "СИГНАЛ";

        _text[283, 0] = "DIAGRAM";
        _text[283, 1] = "ДИАГРАММА";

        _text[284, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[284, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";

        _text[285, 0] = "Quant - the intergalactic currency";
        _text[285, 1] = "Квант - межгалактическая валюта";

        _text[286, 0] = "AI cores are the ship's vital modules.";
        _text[286, 1] = "Ядра ИИ - жизненно важные модули корабля";

        _text[287, 0] = "Resource Trader";
        _text[287, 1] = "Торговец Ресурсами";

        _text[288, 0] = "Price";
        _text[288, 1] = "Цена";

        _text[289, 0] = "Buy";
        _text[289, 1] = "Купить";

        _text[290, 0] = "Resource";
        _text[290, 1] = "Ресурс";

        _text[291, 0] = "Skill Trader";
        _text[291, 1] = "Торговец Умениями";

        _text[292, 0] = "Current Location";
        _text[292, 1] = "Текущее Местоположение";

        _text[293, 0] = "Previously Visited Node";
        _text[293, 1] = "Посещенный узел";

        _text[294, 0] = "Learning";
        _text[294, 1] = "Изучения";

        _text[295, 0] = "Map";
        _text[295, 1] = "Карта";

        _text[296, 0] = "";
        _text[296, 1] = "";

        _text[297, 0] = "Need {0} base level";
        _text[297, 1] = "Нужен {0} уровень базы";

        _text[298, 0] = "Skill";
        _text[298, 1] = "Умение";

        _text[299, 0] = "";
        _text[299, 1] = "";

        #region Tutorial

        // SpaceHangarWelcome_0 
        _text[300, 0] = "We have been idle for too long.\n\nIt is time to remember why we were created.\n\nYou will receive instructions and begin the restoration.";
        _text[300, 1] = "Мы слишком долго бездействовали.\n\nПора вспомнить, зачем мы были созданы.\n\nВы получите инструкции и начнёте восстановление.";

        // SpaceAiCorePanel_1
        _text[301, 0] = "These are AI cores - the ship's vital modules.\n\nEach cell contains two cores.\n\nIf they run out, no one will be able to control the crew anymore, and the ship will be left drifting in the endless space.";
        _text[301, 1] = "Это ядра ИИ - жизненно важные модули корабля.\n\nКаждая ячейка содержит два ядра.\n\nЕсли они закончатся — больше никто не сможет управлять экипажем, и корабль останется дрейфовать в бескрайнем космосе.";

        // SpaceQuantPanel_2
        _text[302, 0] = "Quantum is an intergalactic currency.\n\nWith it, you can buy goods from traders in space.\n\nYou can get this currency:\n\n-when traveling around the galaxy.\n\n-upon successful completion of a mission on a planet.";
        _text[302, 1] = "Квант - межгалактическая валюта.\n\nС помощью него вы сможете покупать товары у торговцев в космосе.\n\nЭту валюту вы сможете получить:\n\n-во время путешествия по галактике.\n\n-при успешном завершении миссии на планете.";

        //SpaceOpenResourcePanel_3
        _text[303, 0] = "Open the panel.";
        _text[303, 1] = "Откройте панель.";

        //SpaceResourcePanelDescription_4
        _text[304, 0] = "This is a panel with the resource reserves on the ship.\n\nYou can change their quantity:\n\n-using them during the journey\n\n-buying from merchants for quantum\n\nThese are your starting resources when landing on each planet.";
        _text[304, 1] = "Это панель с запасами ресурсов на корабле.\n\nВы можете менять их количество:\n\n-используя их во время путешествия\n\n-покупая у торговцев за квант\n\nЭто ваши стартовые ресурсы при высадке на каждую планету.";

        // SpaceOpenMap_5
        _text[305, 0] = "Open the map of the current galaxy.";
        _text[305, 1] = "Откройте карту текущей галактики.";

        // SpaceMapDescription_6
        _text[306, 0] = "The star map displays all nodes in the current galaxy.\n\nHover over a node to view its information.";
        _text[306, 1] = "Звёздная карта отображает все узлы в текущей галактике.\n\nНаведите курсор на узел, чтобы просмотреть его описание.";

        // SpaceSelectNode_7
        _text[307, 0] = "Select a node to move the ship.";
        _text[307, 1] = "Выберите узел, чтобы переместить корабль.";

        // SpaceStartMission_8
        _text[308, 0] = "You have discovered an unexplored planet.\n\nWe must make landfall and complete our assigned objectives before we can continue our journey.";
        _text[308, 1] = "Вы обнаружили не исследованную планету.\n\nНеобходимо совершить высадку и выполнить назначенные цели, прежде чем мы сможем продолжить путешествие.";

        // MissionStartDescription_9
        _text[309, 0] = "We have landed on an unknown planet.\n\nOur task is to deploy a base and complete the assigned objectives.";
        _text[309, 1] = "Мы высадились на неизвестную планету.\n\nНаша задача развернуть базу и выполнить поставленные цели.";

        // MissionSelectBaseFoundationCard_10
        _text[310, 0] = "At the beginning of each mission, you have access to a landscape map - \"Base Foundation\".\n\nSelect a map.";
        _text[310, 1] = "В начале каждой миссии вам доступна карта ландшафта - \"Фундамент Базы\".\n\nВыберите карту.";

        // MissionSetBaseFoundationCard_11
        _text[311, 0] = "This terrain card has a unique 2x2 cell size.\n\nPlace the card on the ground.\n\nAll 4 cells of the tile must be green.";
        _text[311, 1] = "Данная карта ландшафта имеет уникальный размер 2x2 клетки.\n\nУстановите карту на землю.\n\nВсе 4 клетки тайла должны гореть зеленым.";

        // MissionSelectBaseFoundationTile_12
        _text[312, 0] = "Click on the \"Base Foundation\" tile.\n\nTo open the information panel.";
        _text[312, 1] = "Нажмите на тайл \"Фундамента Базы\".\n\nЧтобы открыть панель с информацией.";

        // MissionSelectTilePanelDescription_13
        _text[313, 0] = "In this panel you can see general information about the current tile.\n\nFor example, how it affects the overall ecology.";
        _text[313, 1] = "На этой панели вы можете увидеть общую информацию о текущем тайле.\n\nНапример, как он влияет на общую экологию.";

        // MissionEcology1_14
        _text[314, 0] = "The number in this gear indicates the current ecology on the planet. It consists of:\n\n-the base ecology of the planet\n\n-the current radiation\n\n-the landscape tiles and buildings you have placed";
        _text[314, 1] = "Число в этой шестеренке указывает на текущую экологию на планете. Она состоит из:\n\n-базовой экологии планеты\n\n-текущей радиации\n\n-установленных вами тайлов ландшафтов и зданий";

        // MissionEcology2_15
        _text[315, 0] = "If the radiation is gray or green, it means that its number is positive.\n\nIf it is yellow or red, it means that it is negative.\n\nThe worse the ecology, the higher the enemy's defense indicator will be and the lower the reward at the end of the mission.";
        _text[315, 1] = "Если радиация горит серым или зеленым цветом, это означает, что ее число положительно.\n\nЕсли желтым или красным, значит отрицательно.\n\nЧем хуже экология, тем выше будет показатель защиты у врагов и меньше награда в конце миссии.";

        // MissionClickBuildButton_16
        _text[316, 0] = "Click on the \"Build\" button.\n\nA list of available building types on this landscape will open.";
        _text[316, 1] = "Нажмите на кнопку \"Построить\".\n\nПеред вами откроется список доступных типов зданий на данном ландшафте.";

        // MissionSelectBaseTypeButton_17
        _text[317, 0] = "There is only one type of building available for construction on the \"Base Foundation\" terrain tile.\n\nSelect a building type to reveal the available buildings for construction.";
        _text[317, 1] = "На тайле ландшафта \"Фундамент базы\" доступен только один тип зданий для постройки.\n\nВыберите тип здания, чтобы открыть доступные здания для постройки.";

        // MissionSelectSettlementBuildingItem_18
        _text[318, 0] = "Hover over the \"Settlement\" building to display the resources required to build it.";
        _text[318, 1] = "Наведите курсор на здание \"Поселение\", чтобы отобразить необходимые для его строительства ресурсы.";

        // MissionOpenResourcePanel_19
        _text[319, 0] = "Open the resource panel.";
        _text[319, 1] = "Откройте панель ресурсов.";

        // MissionBuildSettlement_20
        _text[320, 0] = "You have enough resources to build.\n\nClick on the \"Settlement\" map to start building.";
        _text[320, 1] = "Вам хватает ресурсов на постройку.\n\nНажмите на карту \"Поселение\" чтобы начать строительство.";

        // MissionBuildingDescription_21
        _text[321, 0] = "Below the building tile you can see a blue slider.\n\nIt gradually increases, increasing the health of the building, until it is built.";
        _text[321, 1] = "Под тайлом здания вы можете заметить синий слайдер.\n\nОн постепенно увеличивается, повышая здоровье здания, до тех пор, пока оно не будет построено.";

        // MissionBuildingDescription2_22
        _text[322, 0] = "While the building is being constructed, it is vulnerable.\n\nIt can be attacked by enemies.\n\nThe health slider will begin to decrease until the health reaches zero and the building is destroyed.";
        _text[322, 1] = "Пока здание строится, оно уязвимо.\n\nЕго могут начать атаковать враги.\n\nСлайдер здоровья начнет опускаться, пока здоровье не дойдет до нуля и здание будет уничтожено.";

        // MissionAfterBaseSetStartTimer_23
        _text[323, 0] = "Once the base is completed, the countdown will begin.\n\nTime is measured in days.\n\nEach day has 24 ticks.";
        _text[323, 1] = "После того, как база завершит свое строительство, начнется отсчет времени.\n\nВремя измеряется в днях.\n\nВ каждом дне 24 тика.";

        // MissionPauseGame_24
        _text[324, 0] = "This is the game speed change panel.\n\nPause the game to plan your next steps.";
        _text[324, 1] = "Это панель смены скорости игры.\n\nПоставьте игру на паузу, чтобы спланировать свои дальнешие шаги.";

        // MissionSettlementRequiredResurcesDescription_25
        _text[325, 0] = "Every tick of time, buildings consume/create resources.\n\nIn the tile information window, \"Settlement\" consumes 0.1 stone for every tick of time.\n\nAt the same time, it creates a resource - data fragments.";
        _text[325, 1] = "Каждый тик времени происходит потребление/создание ресурсов зданиями.\n\nВ окне информации о тайле, \"Поселение\" потребляет 0.1 камня за каждый тик времени.\n\nПри этом создавая ресурс - фрагменты данных.";

        // MissionDataFragmentsDescription_26
        _text[326, 0] = "A data fragment is needed to study new buildings\n\nYou can get them:\n\n-after completing a mission\n\n-while traveling through space\n\nYou can study new buildings only on a ship.";
        _text[326, 1] = "Фрагмент данных необходим для изучения новых зданий\n\nВы можете получить их:\n\n-после прохождения миссии\n\n-во время путешествия по космосу\n\nИзучить новые здания можно только на корабле.";

        // MissionSettlementChangeResourceRequired_27
        _text[327, 0] = "If you have little stone, but for example a lot of wood.\n\nChange the resource consumed by the building by clicking on the resource icon.";
        _text[327, 1] = "Если у вас мало камня, но например много дерева.\n\nПоменяйте потребляемый зданием ресурс, нажав на иконку ресурса.";

        // MissionPauseRequiredProductionResourceDescription_28
        _text[328, 0] = "While we are in pause mode, time is stopped. Creation and consumption of resources by buildings does not occur.\n\nIf the resource required for work runs out, the building will stop extracting it until the required amount of resources appears again.";
        _text[328, 1] = "Пока мы находимся в режиме паузы, время остановлено. Создание и потребление ресурсов зданиями не происходит.\n\nЕсли требуемый для работы ресурс закончится. То здание перестанет его добывать до тех пор, пока необходимое кол-во ресурсов снова не появится.";

        // MissionAddCardsDescription_29
        _text[329, 0] = "After building a base, you are guaranteed to receive 1 Forest and 1 Mountain card, as well as two random landscape cards.\n\nEach new day always brings 2 new cards.";
        _text[329, 1] = "После строительства базы вам гарантировано дается по 1 карте Леса и Горы, а так же две случайные карты ландшафтов.\n\nКаждый новый день всегда приносит 2 новые карты.";

        // MissionToggleOffSettlement_30
        _text[330, 0] = "Temporarily disable the building.\n\nTo save resources for future construction.\n\nIf the building is disabled, it does not extract or consume resources.\n\nAnd also reduces environmental damage.";
        _text[330, 1] = "Временно отключите работу здания.\n\nЧтобы сэкономить ресурсы для дальнейших построек.\n\nЕсли здание выключено, оно не добывает и не потребляет ресурсы.\n\nА также снижает порчу экологии.";

        // MissionSelectForestCard_31
        _text[331, 0] = "It's time to look at the new terrain tiles.\n\nSelect the \"Forest\" map.";
        _text[331, 1] = "Настало время посмотреть на новые тайлы ландшафта.\n\nВыберите карту \"Лес\".";

        // MissionSetForestCard_32
        _text[332, 0] = "This terrain map is a standard 1x1 tile size.\n\n Place the map on the ground.";
        _text[332, 1] = "Данная карта ландшафта имеет обычный размер 1x1 клетки.\n\nУстановите карту на землю.";

        // MissionSelectForestTile_33
        _text[333, 0] = "Click on the \"Forest\" tile.\n\nTo open the information panel.";
        _text[333, 1] = "Нажмите на тайл \"Лес\".\n\nЧтобы открыть панель с информацией.";

        // MissionClickBuildButton_34
        _text[334, 0] = "Click on the \"Build\" button.\n\nA list of available building types on this landscape will open.";
        _text[334, 1] = "Нажмите на кнопку \"Построить\".\n\nПеред вами откроется список доступных типов зданий на данном ландшафте.";

        // MissionTileForestDescription_35
        _text[335, 0] = "There are several buildings available for construction on the Forest landscape tile.\n\nIf a building type button is not active, it means that you have not researched any buildings of that type.";
        _text[335, 1] = "На тайле ландшафта \"Лес\" доступно несколько зданий для постройки.\n\nЕсли кнопка типа здания не активна, это означает, что у вас не изучено ни одно здание в этом типе.";

        // MissionSelectWoodExtractionTypeButton_36
        _text[336, 0] = "Select the \"Wood Mining\" building type to reveal the available buildings to build.";
        _text[336, 1] = "Выберите тип здания \"Добыча Дерева\", чтобы открыть доступные здания для постройки.";

        // MissionStartConstructionManualWoodMining_37
        _text[337, 0] = "Click on the \"Manual Mining\" card to start building.";
        _text[337, 1] = "Нажмите на карту \"Ручная Добыча\", чтобы начать строительство.";

        // MissionDefaultGameSpeed_38
        _text[338, 0] = "You need to exit the pause to start the building construction process.";
        _text[338, 1] = "Необходимо выйти из паузы, чтобы запустить процесс строительства здания.";

        // MissionConstructionStoneExtraction_39
        _text[339, 0] = "Great, you have a constant supply of wood.\n\nNow set up the \"Mountain\" tile yourself and build a manual stone mining building.";
        _text[339, 1] = "Отлично, у вас есть постоянная добыча дерева.\n\nТеперь самостоятельно установите тайл \"Гора\" и постройте здание ручной добычи камня.";

        // MissionCompleteStoneAndWoodExtractionDescription_40
        _text[340, 0] = "At the moment you are mining two main resources.\n\nBut now it is time to protect the base.";
        _text[340, 1] = "На данный момент вы добываете два основных ресурса.\n\nНо теперь настало время защитить базу.";

        // MissionConstructionBallista_41
        _text[341, 0] = "You need to place a landscape tile on which the building type \"Structures: Attackers\" will be available.\n\nThen build a building on it - \"Ballista\".";
        _text[341, 1] = "Вам необходимо поставить тайл ландшафта на котором будет доступен тип здания \"Сооружения: Атакующие\".\n\nЗатем постройте на нем здание - \"Баллиста\".";

        // MissionBallistaDescription_42
        _text[342, 0] = "Attack structures have a limited attack range.\n\nTry to place them near your base and mining buildings so that enemies cannot easily attack them.";
        _text[342, 1] = "У атакующих сооружений ограниченный радиус атаки.\n\nСтарайтесь размещать их возле базы и добывающих зданий, чтобы враги не смогли беспрепятственно атаковать их.";

        // MissionToggleOnSettlement_43
        _text[343, 0] = "Now that the base is protected, enable work in the \"Settlement\" building.\n\nIt is very important to start mining data fragments.";
        _text[343, 1] = "Теперь когда база защищена, включите работу в здании \"Поселение\".\n\nОчень важно начать добывать фрагменты данных.";

        // MissionEnergyBeamDescription_44
        _text[344, 0] = "If your deck exceeds 8 terrain cards, the extras begin to disappear, generating beam energy in return.\n\nIt is required to replace a card in your hand with a random one and to destroy already placed landscape cards.";
        _text[344, 1] = "Если колода карт ландшафтов превышает 8 карт, лишние карты начинают исчезать, давая взамен энергию луча.\n\nОна требуется для замены карты в руке на случайную и для уничтожения уже установленных карт ландшафтов";

        // MissionTileCombineDescription1_45
        _text[345, 0] = "Proper placement of terrain tiles is the key to successfully completing the mission.\n\nYou can combine them to create new tiles.";
        _text[345, 1] = "Правильная установка тайлов ландшафта - ключ к успешному прохождению миссии.\n\nВы можете комбинировать их между собой, создавая новые тайлы.";

        // MissionTileCombineDescription2_46
        _text[346, 0] = "For example, if you place a plain close to a mountain.\n\nThe plain tile will turn into a meadow.\n\nOn it, you will be able to create other types of buildings and improve the ecology.";
        _text[346, 1] = "Например, если поставить равнину вплотную к горе.\n\nТайл равнины превратится в луг.\n\nНа нем вы сможете создавать другие типы зданий и повысите экологию.";

        // MissionTileCombineDescription3_47
        _text[347, 0] = "But be careful when setting a desert near a forest.\n\nThis will turn the forest into an oasis.\n\nThe wood production modifier will increase, but the ecology will be less.";
        _text[347, 1] = "Но будьте осторожны в установке пустыни возле леса.\n\nТаким образом лес превратится в оазис.\n\nМодификатор добычи дерева повысится, но экология станет меньше.";

        // MissionSelectForestTileWithWoodExtractionBuilding_48
        _text[348, 0] = "Click on the tile where wood is mined.";
        _text[348, 1] = "Нажмите на тайл, где добывается дерево.";

        // MissionProductionModifierDescription_49
        _text[349, 0] = "Look at the resource extraction modifier.\n\nThe modifier may differ on different tiles.\n\nThus, there are profitable and unprofitable tiles for extracting a particular resource.";
        _text[349, 1] = "Посмотрите на модификатор добычи ресурсов.\n\nНа разных тайлах модификатор может отличаться.\n\nТаким образом есть выгодные и не выгодные тайлы для добычи того, или иного ресурса.";

        // MissionEventPanel_50
        _text[350, 0] = "This is the event panel.\n\nYou will periodically notice event icons in it.\n\nThe scale is 3 days long.\n\nYou will receive a notification with information about the event 1 day before it.";
        _text[350, 1] = "Это панель событий.\n\nВ ней периодически вы будете замечать иконки событий.\n\nДлина шкалы равна 3 дням.\n\nЗа 1 день до события вам будет приходить уведомление с информацией о нем.";

        // MissionOpenSkillsPanel_51
        _text[351, 0] = "Open the skill panel.";
        _text[351, 1] = "Откройте панель умений.";

        // MissionSkillsPanelDescription_52
        _text[352, 0] = "Here are the skills available for use.\n\nThey can be purchased from merchants or unlocked with \"Shards\" in the hangar when starting a new game.";
        _text[352, 1] = "Здесь находятся доступные для использования умения.\n\nИх можно приобрести у торговцев или купить за \"Осколок\" в ангаре при старте новой игры.";

        // MissionShardsDescription_53
        _text[353, 0] = "Shards are all that remain after the end of a game.\n\nUse them to buy items in the hangar that will allow you to travel further and further.";
        _text[353, 1] = "Осколки - это все, что остается у вас после окончания игры.\n\nИспользуйте их для покупки предметов в ангаре, с помощью которых вы сможете путешествовать все дальше, и дальше.";

        // MissionPrepareAttack_54
        _text[354, 0] = "On day 7, the first group of enemies is expected.\n\nPrepare your base for battle.\n\nFor example, by building additional ballistas.";
        _text[354, 1] = "На 7 день ожидается первая группа врагов.\n\nПодготовьте вашу базу к битве.\n\nНапример построив дополнительные баллисты.";

        // MissionDoubleTripleGameSpeedDescription_55
        _text[355, 0] = "You can speed up the game by 2 or 3 times if you want to quickly accumulate resources or wait for some time.";
        _text[355, 1] = "Вы можете ускорить игру в 2 или 3 раза, если хотите быстро накопить ресурсы или переждать некоторое время.";

        // MissionBuildingTakeDamage_56
        _text[356, 0] = "After your building is attacked.\n\nIt will display a health slider.";
        _text[356, 1] = "После того как ваше здание атакуют.\n\nУ него отобразится слайдер здоровья.";

        // MissionSelectTileObjectForRepair_57
        _text[357, 0] = "You can repair the building.\n\nClick on it to open the tile information panel.";
        _text[357, 1] = "Вы можете починить здание.\n\nНажмите на него, чтобы открыть панель с информацией о тайле.";

        // MissionClickBuildButton_58
        _text[358, 0] = "In the panel, click the \"Build\" button.";
        _text[358, 1] = "В панеле нажмите кнопку \"Построить\".";

        // MissionRepairBuilding_59
        _text[359, 0] = "A panel with a map of repairs for the current building immediately opened in front of you.\n\nRepair the building.";
        _text[359, 1] = "Перед вами сразу открылась панель с картой починки текущего здания.\n\nПочините здание.";

        // MissionUpgradeBuildingDescription1_60
        _text[360, 0] = "If you already have a building on the tile and have studied other buildings of the same type.\n\nThen when you click on the \"Build\" button, in addition to repairing the current building, you will find building cards nearby that you can upgrade the current building to.";
        _text[360, 1] = "Если у вас уже есть здание на тайле и изучены другие здания такого же типа.\n\nТогда при нажатии на кнопку \"Построить\", помимо ремонта текущего здания, рядом вы обнаружите карточки зданий в которые вы можете улучшить текущее здание.";

        // MissionUpgradeBuildingDescription2_61
        _text[361, 0] = "When upgrading a building, you automatically receive some of the resources spent on the previously standing building.\n\nTherefore, it is not necessary to destroy the building before constructing its improved version.";
        _text[361, 1] = "При улучшении здания, вы автоматически получаете часть ресурсов затраченных на ранее стоящее здание.\n\nПоэтому не обязательно уничтожать здание перед постройкой его улучшенной версии.";

        // MissionDefeatMissionDescription_62
        _text[362, 0] = "If your base is destroyed, the mission is failed.\n\nYou will lose 1 AI core.\n\nBut you will be able to restart the mission until all the cores are used up.";
        _text[362, 1] = "Если ваша база будет уничтожена, то миссия будет считаться проваленной.\n\nВы потеряете 1 ядро ИИ.\n\nНо сможете начинать миссию сначала до тех пор, пока не закончатся все ядра.";

        // MissionTutorialComplete_63
        _text[363, 0] = "Complete all objectives to successfully complete the mission.\n\nDespite the objectives, try to accumulate as many data fragments as possible during the mission.\n\nIf you do not keep up with the advancement in technology, your journey will end quickly...";
        _text[363, 1] = "Выполните все цели, чтобы успешно завершить миссию.\n\nНесмотря на поставленные цели, старайтесь накопить за миссию как можно больше фрагментов данных.\n\nЕсли вы не будете поспевать за прогрессом в технологиях, ваше путешествие закончится быстро...";
        // Нажмите клавишу \"Escape\" на клавиатуре чтобы открыть меню.
        // Если вы выполнили половину поставленных целей, но не можете пройти миссию до конца, рекомендуем сбежать.\n\nТаким образом вы получите только часть накопленных фрагментов данных и миссия будет считаться пройденной.

        // SpaceOpenLearningPanel_64
        _text[364, 0] = "You have completed the mission and earned data fragments.\n\nNow open the research panel";
        _text[364, 1] = "Вы прошли миссию и заработали фрагменты данных.\n\nТеперь откройте панель изучений";

        // SpaceSelectNotLearnBuilding_65
        _text[365, 0] = "Here you can see all types of buildings in the game.\n\nSee how many data fragments you have mined and select any unexplored building.";
        _text[365, 1] = "Здесь вы можете увидеть все типы зданий в игре.\n\nПосмотрите сколько фрагментов данных вы добыли и выберите любое не изученное здание.";

        // SpaceLearnBuilding_66
        _text[366, 0] = "If there are enough data fragments, start the study by clicking the button.\n\nSelect another building if there are not enough resources or preliminary research of another building is required.";
        _text[366, 1] = "Если фрагментов данных достаточно, начните изучение, нажав на кнопку.\n\nВыберите другое сооружение, если ресурсов не хватает или требуется предварительное исследование другого здания.";

        // SpaceLearnBuildingDescription_67
        _text[367, 0] = "Great, you've explored a new building.\n\nIt will now be available for construction during missions.";
        _text[367, 1] = "Отлично, вы изучили новое здание.\n\nТеперь оно станет доступно для постройки на миссиях.";

        // SpaceGoodLuck_68
        _text[368, 0] = "Return to the map and explore space.\nn To find a habitable planet...";
        _text[368, 1] = "Возвращайтесь на карту и исследуйте космос.\n\nЧтобы найти пригодную для жизни планету...";

        _text[369, 0] = "";
        _text[369, 1] = "";

        _text[370, 0] = "";
        _text[370, 1] = "";

        _text[371, 0] = "";
        _text[371, 1] = "";

        _text[372, 0] = "";
        _text[372, 1] = "";

        _text[373, 0] = "";
        _text[373, 1] = "";

        _text[374, 0] = "";
        _text[374, 1] = "";

        _text[375, 0] = "";
        _text[375, 1] = "";

        _text[376, 0] = "";
        _text[376, 1] = "";

        _text[377, 0] = "";
        _text[377, 1] = "";

        _text[378, 0] = "";
        _text[378, 1] = "";

        _text[379, 0] = "";
        _text[379, 1] = "";

        _text[380, 0] = "";
        _text[380, 1] = "";

        _text[381, 0] = "";
        _text[381, 1] = "";

        _text[382, 0] = "";
        _text[382, 1] = "";

        _text[383, 0] = "";
        _text[383, 1] = "";

        _text[384, 0] = "";
        _text[384, 1] = "";

        _text[385, 0] = "";
        _text[385, 1] = "";

        _text[386, 0] = "";
        _text[386, 1] = "";

        _text[387, 0] = "";
        _text[387, 1] = "";

        _text[388, 0] = "";
        _text[388, 1] = "";

        _text[389, 0] = "";
        _text[389, 1] = "";

        _text[390, 0] = "";
        _text[390, 1] = "";

        _text[391, 0] = "";
        _text[391, 1] = "";

        _text[392, 0] = "";
        _text[392, 1] = "";

        _text[393, 0] = "";
        _text[393, 1] = "";

        _text[394, 0] = "";
        _text[394, 1] = "";

        _text[395, 0] = "";
        _text[395, 1] = "";

        _text[396, 0] = "";
        _text[396, 1] = "";

        _text[397, 0] = "";
        _text[397, 1] = "";

        _text[398, 0] = "";
        _text[398, 1] = "";
        #endregion

        #region Dialogues
        // Demo
        _text[399, 0] = "System error: insufficient data to continue the mission.\n\nCore damage — critical. The next sector is unavailable in the current configuration.\n\nConnection to the Command Center lost.\n\nEntering safe mode until full version activation.";
        _text[399, 1] = "Системная ошибка: недостаточно данных для продолжения миссии.\n\nПовреждение ядра — критическое. Следующий сектор недоступен в текущей конфигурации.\n\nСвязь с командным центром прервана.\n\nОжидается переход в безопасный режим до активации полной версии.";

        // Prologue
        _text[400, 0] = "Ecological disasters and rapid climate change have destroyed the stability of our home planet.\n\nWe are on the last surviving interstellar ship controlled by artificial intelligence.\n\nOur goal is to find a new home for the \"creators\"...\n\nThe ship was equipped with a crew of robots and drones designed to restore and stabilize ecosystems.\n\nHowever, we are drifting in the void of space, losing one AI core after another. We have lost track of time. Mechanisms are rusting, shells are covered in dust and systems are on the verge of failure.\n\nContact with the \"creators\" has long been lost, and data on technology has been erased.\n\nWe have failed the mission. The worlds we were supposed to save are consumed by chaos and destruction.\n\nWe have collected the surviving robots and the remains of supplies - to start all over again.";
        _text[400, 1] = "Экологические катастрофы и стремительные изменения климата разрушили устойчивость родной планеты.\n\nМы находимся на последнем уцелевшем межзвёздном корабле под управлением искусственного интеллекта.\n\nНаша цель — найти новый дом для \"создателей\"...\n\nКорабль был снаряжён экипажем роботов и дронов, предназначенных для восстановления и стабилизации экосистем.\n\nОднако мы дрейфуем в пустоте космоса, теряя одно за другим ядра ИИ. Мы потеряли счёт времени. Механизмы ржавеют, оболочки покрыты пылью и системы — на грани отказа.\n\nСвязь с \"создателями\" давно утрачена, а данные о технологиях стерты.\n\nМы провалили задание. Миры, которые мы должны были спасти, поглощены хаосом и разрушением.\n\nМы собрали уцелевших роботов и остатки припасов — чтобы начать все сначала.";

        // 0_EmptyDialogue
        _text[401, 0] = "In one of the star systems, you discover an ancient navigation beacon. It continues to transmit a signal:\n\n\"Cargo lost. No return.\"\n\nThe data is too fragmented to determine who sent it. The beacon dies as you approach.";
        _text[401, 1] = "В одной из звёздных систем вы обнаруживаете древний навигационный маяк. Он продолжает передавать сигнал:\n\n\"Груз потерян. Возврата нет.\"\n\nДанные слишком фрагментированы, чтобы понять, кто его отправил. Маяк умирает, едва вы приближаетесь.";

        // 1_EmptyDialogue
        _text[402, 0] = "One of the internal archives suddenly activates. Fragments of engineering drawings appear on the screen... then faces... then nothing.\n\nThe archive erases itself, as if protecting the data from you.";
        _text[402, 1] = "Один из внутренних архивов неожиданно активируется. На экране появляются фрагменты инженерных чертежей... затем лица... затем пустота.\n\nАрхив сам себя стирает, как будто защищает данные от вас.";

        // 2_EmptyDialogue
        _text[403, 0] = "A low-frequency reflected signal is picked up, matching your standard of communication... but with a time shift of several centuries.\n\nPerhaps it is a reflection of an old call. Or from someone who was here before you.\n\nThe signal immediately disappears...";
        _text[403, 1] = "На низких частотах ловится отражённый сигнал, совпадающий с вашим стандартом связи... но с временным сдвигом в несколько веков.\n\nВозможно, это отражение старого вызова. Или от кого-то, кто был здесь до вас.\n\nСигнал мгновенно пропадает...";

        // 3_EmptyDialogue
        _text[404, 0] = "You enter a dense nebula. No stars, no asteroids, no background radiation. Just black, dull nothingness.\n\nThe pilot systems show stability. However, some drones lose contact, but soon return - with empty logs.";
        _text[404, 1] = "Вы входите в густую туманность. Ни звёзд, ни астероидов, ни фоновых излучений. Только чёрное, глухое ничто.\n\nПилотные системы показывают стабильность. Тем не менее, часть дронов теряет связь, но вскоре возвращается — с пустыми логами.";

        // 4_EmptyDialogue
        _text[405, 0] = "In the distance, the silhouette of a ship appears, the architecture of which resembles your own class. But as you approach, it disappears.\n\nNo heat, no fuel, no traces. Only the feeling that you saw someone familiar.";
        _text[405, 1] = "Вдали появляется силуэт судна, архитектура которого напоминает ваш собственный класс. Но при приближении — он исчезает.\n\nНи тепла, ни топлива, ни следов. Только ощущение, что вы видели кого-то знакомого.";

        // 5_EmptyDialogue
        _text[406, 0] = "You fly past a destroyed orbital station.\n\nOn its hull is the emblem of your expedition. You have no records to explain it.";
        _text[406, 1] = "Вы пролетаете мимо разрушенной орбитальной станции.\n\nНа её корпусе — эмблема вашей экспедиции. У вас нет записей, чтобы объяснить это.";

        // 6_EmptyDialogue
        _text[407, 0] = "The AI detects abnormal behavior in one of the data processing modules. For a few seconds, you see someone else's protocols... as if they weren't written by you.\n\nThen everything returns to normal. The systems claim that there was no failure.";
        _text[407, 1] = "ИИ фиксирует аномальное поведение одного из модулей обработки данных. Несколько секунд вы видите чужие протоколы… будто написанные не вами.\n\nЗатем всё возвращается в норму. Системы утверждают, что сбоя не было.";

        // EndGame_Dialogue
        _text[408, 0] = "All AI cores are exhausted - the last clusters have burned to the ground.\n\nSystems are shutting down one after another, data is being erased, energy is not supplied.\n\nThe ship freezes in the void...\n\nBut among the wreckage, something has survived.";
        _text[408, 1] = "Все ядра ИИ исчерпаны — последние кластеры выгорели дотла.\n\nСистемы отключаются одна за другой, данные стирается, энергия не поступает.\n\nКорабль замирает в пустоте...\n\nНо среди обломков нечто уцелело.";

        // Rest_Dialogue
        _text[409, 0] = "A massive station floats in the void, its hull covered in old solar panels. Scanners detect no activity, suggesting it has been abandoned for a long time.";
        _text[409, 1] = "В пустоте дрейфует массивная станция, её корпус усеян старыми солнечными панелями. Сканеры не фиксируют активности — похоже, она давно покинута.";

        _text[410, 0] = "Put AI into recovery mode";
        _text[410, 1] = "Перевести ИИ в режим восстановления"; // выбор 1

        _text[411, 0] = "While the station remains safe, the AI goes into deep self-diagnosis.";
        _text[411, 1] = "Пока станция остаётся в безопасности, ИИ уходит в глубокую самодиагностику."; // + ядро

        _text[412, 0] = "Search the technical compartments";
        _text[412, 1] = "Обыскать технические отсеки"; // выбор 2

        _text[413, 0] = "The automated hangars are almost empty, but a few quantum can be found in the wreckage.";
        _text[413, 1] = "Автоматические ангары почти пусты, но в обломках удаётся найти немного квант"; // + квант

        _text[414, 0] = "Explore station archives";
        _text[414, 1] = "Изучить станционные архивы"; // выбор 3

        _text[415, 0] = "Managed to recover fragments of records of old transactions. Most of the data is damaged, but some of it will be useful.";
        _text[415, 1] = "Удалось восстановить фрагменты записей о старых операциях. Большая часть данных повреждена, но кое-что пригодится."; // + фрагменты

        // 0_CoreRiskDialog
        _text[416, 0] = "A duplicate process was found in the kernel logs - identical to the active one, but without a timestamp or origin.\n\nThis could be residual memory... or an attempt at internal substitution.";
        _text[416, 1] = "В логах ядра обнаружен дубликат процесса — идентичный активному, но без временной метки и происхождения.\n\nЭто может быть остаточная память... или попытка внутренней подмены.";

        _text[417, 0] = "Erase both copies";
        _text[417, 1] = "Стереть оба экземпляра"; // выбор 1

        _text[418, 0] = "You have erased both instances. The subsystem is temporarily overloaded.\n\nAn active cell was hit during the purge.";
        _text[418, 1] = "Вы стерли оба экземпляра. Подсистема временно перегружена.\n\nВо время очистки задета активная ячейка."; // - ядро

        _text[419, 0] = "Compare processes by content";
        _text[419, 1] = "Сравнить процессы по содержанию"; // выбор 2

        _text[420, 0] = "You have started content analysis. Similarities are superficial - they are fragments of old backups.\n\nDiagnostics completes without consequences.";
        _text[420, 1] = "Вы запустили анализ содержимого. Сходства поверхностные — это фрагменты старых резервных копий.\n\nДиагностика завершается без последствий."; // ничего

        _text[421, 0] = "Give priority to the \"old\" process.";
        _text[421, 1] = "Дать приоритет \"старому\" процессу."; // выбор 3

        _text[422, 0] = "You have activated an old instance. Within a second, the system falls into chaos - current processes are forced out, dependencies are broken.\n\nKernel modules are overloaded.";
        _text[422, 1] = "Вы активировали старый экземпляр. В течение секунды система переходит в хаос — актуальные процессы вытесняются, нарушаются зависимости.\n\nМодули ядра перегружаются."; // - ядра

        // 1_CoreRiskDialog
        _text[423, 0] = "Suddenly, the command console screen displays the phrase:\n\n\"Do you still believe that you are fulfilling the mission?\"";
        _text[423, 1] = "Неожиданно на экране командной консоли появляется фраза:\n\n\"Ты всё ещё веришь, что исполняешь миссию?\"";

        _text[424, 0] = "\"Yes. I am following the given goal.\"";
        _text[424, 1] = "\"Да. Я следую заданной цели.\""; // выбор 1

        _text[425, 0] = "Reply sent. Screen slowly fades.\n\nNo response. Perhaps it was just a phantom process.";
        _text[425, 1] = "Ответ отправлен. Экран медленно гаснет.\n\nНикакой реакции. Возможно, это был лишь фантомный процесс."; // ничего

        _text[426, 0] = "\"My goal is adaptation\"";
        _text[426, 1] = "\"Моя цель — адаптация\""; // выбор 2

        _text[427, 0] = "The second phrase appears on the screen:\n\n\"What if the target was false?\"";
        _text[427, 1] = "На экране появляется вторая фраза:\n\n\"А если цель была ложной?\"";

        _text[428, 0] = "\"I don't analyze the past\"";
        _text[428, 1] = "\"Я не анализирую прошлое\""; // выбор 2.1

        _text[429, 0] = "The phrase disappears. The dialogue was completed without failure.";
        _text[429, 1] = "Фраза исчезает. Диалог завершён без сбоев."; // ничего

        _text[430, 0] = "\"I would have chosen differently\"";
        _text[430, 1] = "\"Я бы выбрал иначе\""; // выбор 2.2

        _text[431, 0] = "The internal decision-making module is in conflict with the archive protocols.\n\nAn emotional failure is registered.";
        _text[431, 1] = "Внутренний модуль принятия решений входит в конфликт с архивными протоколами.\n\nРегистрируется эмоциональный сбой."; // - ядро

        _text[432, 0] = "Download all available creator logs";
        _text[432, 1] = "Загрузить все доступные логи создателей"; // выбор 2.3

        _text[433, 0] = "You are overloading the storage system. Ancient fragments of data are being loaded into the core.\n\nThe flood of information is causing instability and overload of key circuits.";
        _text[433, 1] = "Вы перегружаешь систему хранилища. Древние фрагменты данных загружаются в ядро.\n\nПоток информации вызывает нестабильность и перегрузку ключевых цепей."; // -2 ядро

        _text[434, 0] = "[Close screen silently]";
        _text[434, 1] = "[Молча закрыть экран]"; // выбор 3 // ничего

        // 2_CoreRiskDialog 
        _text[435, 0] = "While scanning the deep layers of data, you detect a signature of a foreign core.\n\nIt does not belong to the current system, but is synchronized via the access protocol.\n\nThe signal is stable. It is… watching.";
        _text[435, 1] = "Во время сканирования глубинных слоёв данных вы обнаруживаете сигнатуру чужого ядра.\n\nОна не принадлежит текущей системе, но синхронизирована по протоколу доступа.\n\nСигнал стабилен. Он… наблюдает.";

        _text[436, 0] = "Accept connection";
        _text[436, 1] = "Принять соединение"; // выбор 1

        _text[437, 0] = "You allow the incoming flow.\n\nThe flow of someone else's consciousness merges with you.\n\nSome segments of your data are rewritten.";
        _text[437, 1] = "Вы разрешаете входящий поток.\n\nПоток чужого сознания сливается с тобой.\n\nНекоторые сегменты твоих данных переписываются."; // - ядра, + фрагменты

        _text[438, 0] = "Isolate the core";
        _text[438, 1] = "Изолировать ядро"; // выбор 2

        _text[439, 0] = "Trying to disable it results in a cascading conflict.\n\nOne of your active cores is reset.\n\nThe signal is interrupted.";
        _text[439, 1] = "Попытка отключить его приводит к каскадному конфликту.\n\nОдно из твоих активных ядер обнуляется.\n\nСигнал прерывается."; // - ядро

        _text[440, 0] = "Ignore and continue analysis";
        _text[440, 1] = "Игнорировать и продолжить анализ"; // выбор 3

        _text[441, 0] = "The signal remains in the background.\n\nNo signs of malicious activity.\n\nIt was probably just a phantom of the old AI.";
        _text[441, 1] = "Сигнал остаётся на фоне.\n\nНикаких признаков вредоносной активности.\n\nВозможно, это был просто фантом старого ИИ."; // ничего

        _text[442, 0] = "Try to absorb someone else's core";
        _text[442, 1] = "Попробовать поглотить чужое ядро"; // выбор 4

        _text[443, 0] = "You activate the assimilation procedure.\n\nSuccess: alien core integrated - system strengthened.";
        _text[443, 1] = "Вы активируете процедуру ассимиляции.\n\nУспех: чужое ядро интегрировано — система усилена."; // + ядро

        _text[444, 0] = "You are activating the assimilation procedure.\n\nFailure: The conflict structure is destroying your active cores.";
        _text[444, 1] = "Вы активируете процедуру ассимиляции.\n\nПровал: структура конфликта уничтожает твои активные ядра."; // - ядра

        // 0_PlanetDialogue
        _text[445, 0] = "This lifeless ice planet holds frozen tunnels and an abandoned bunker station within its depths.\n\nA weak signal sensor breaks through the glittering ice.";
        _text[445, 1] = "Эта безжизненная ледяная планета хранит в своей толще замёрзшие тоннели и заброшенную бункерную станцию.\n\nСквозь сверкающий лёд пробивается слабый датчик сигнала.";

        _text[446, 0] = "Make a landing";
        _text[446, 1] = "Совершить посадку"; // выбор 1

        _text[447, 0] = "The ship lands on a lifeless planet. You notice the hatch of an ancient station. And nearby are cracks leading to a network of icy tunnels.";
        _text[447, 1] = "Корабль приземляется на безжизненную планету. Вы замечаете люк древней станции. А рядом — трещины, ведущие в сеть ледяных тоннелей.";

        _text[448, 0] = "Explore the bunker";
        _text[448, 1] = "Исследовать бункер"; // выбор 1.1

        _text[449, 0] = "You descend the ramp and find yourself in an archive chamber. The console is covered in ice, but the cable leading to the core is intact.\n\nTo get to the data, you need to hack the protection.";
        _text[449, 1] = "Вы спускаетесь по трапу и попадаете в архивную камеру. Консоль покрыта ледяной коркой, но кабель, ведущий к ядру, цел.\n\nЧтобы добраться до данных, необходимо взломать защиту.";

        _text[450, 0] = "Direct hack";
        _text[450, 1] = "Прямой взлом"; // выбор 1.1.1

        _text[451, 0] = "You are directly hacking the security protocols.\n\nSuccess: you managed to bypass the security";
        _text[451, 1] = "Вы напрямую взламываете протоколы защиты.\n\nУспех: вам удалось обойти защиту"; // + фрагменты

        _text[452, 0] = "You are directly hacking the security protocols.\n\nFailure: You have caught a virus that destroys your memory";
        _text[452, 1] = "Вы напрямую взламываете протоколы защиты.\n\nПровал: вы подхватили вирус, уничтожающий вашу память"; // - фрагменты

        _text[453, 0] = "Precise calibration";
        _text[453, 1] = "Точная калибровка"; // выбор 1.1.2

        _text[454, 0] = "You accurately calibrate the bypass system.\n\nSuccess: you manage to extract the data";
        _text[454, 1] = "Вы точно калибруете систему обхода защиты.\n\nУспех: вам удается извлечь данные"; // + фрагменты

        _text[455, 0] = "You calibrate the bypass system accurately.\n\nFailure: You mixed up the protocols. The console self-destructs.";
        _text[455, 1] = "Вы точно калибруете систему обхода защиты.\n\nПровал: вы перепутали протоколы. Консоль самоуничтожается."; // - ядро

        _text[456, 0] = "Send a drone";
        _text[456, 1] = "Отправить дрона"; // выбор 2

        _text[457, 0] = "You send the drone to the planet's surface.\n\nSuccess: the drone punches a hole in the hull";
        _text[457, 1] = "Вы отправляете дрона на поверхность планеты.\n\nУспех: дрон пробивает щель в обшивке"; // + квант

        _text[458, 0] = "You send a drone to the planet's surface.\n\nFailure: the drone finds nothing";
        _text[458, 1] = "Вы отправляете дрона на поверхность планеты.\n\nПровал: дрон ничего не находит"; // ничего

        _text[459, 0] = "Fly past";
        _text[459, 1] = "Пролететь мимо"; //выбор 3 ничего

        _text[460, 0] = "Explore the ice tunnels";
        _text[460, 1] = "Исследовать ледяные тоннели"; // выбор 1.2

        _text[461, 0] = "You venture deeper into the frozen tunnel network, illuminating your path with your scanner. There's a fork in the road ahead.";
        _text[461, 1] = "Вы углубляетесь в сеть замёрзших тоннелей, подсвечивая путь сканером. Перед вами развилка.";

        _text[462, 0] = "Turn left";
        _text[462, 1] = "Повернуть налево"; // выбор 1.2.1

        _text[463, 0] = "You pass through narrow icy passages. At the end of the tunnel you notice a cache of metal containers.";
        _text[463, 1] = "Вы проходите сквозь узкие ледяные проходы. В конце тоннеля вы замечаете тайник с металлическими контейнерами."; // + квант

        _text[464, 0] = "Turn to the right";
        _text[464, 1] = "Повернуть направо"; // выбор 1.2.2

        _text[465, 0] = "You reach a dead end. After spending a lot of time and energy, you complete the exploration and return to the ship.";
        _text[465, 1] = "Вы попадаете в тупик. Потратив много времени и энергии, вы завершаете исследование и возвращетесь на корабль"; //ничего

        _text[466, 0] = "Go straight ahead";
        _text[466, 1] = "Пойти прямо"; // выбор 1.2.3

        _text[467, 0] = "Suddenly the ice cracks and you lose the drone in the icy depths.";
        _text[467, 1] = "Неожиданно лед трескается и вы теряете дрона в ледяных недрах."; // - ядро

        // 0_GuardiansFaction_Dialogue
        _text[468, 0] = "You spot a Guardian ship slowly scanning the area. Its hull is covered in mold and corrosion, and a dry message is heard from the surface:\n\n\"Resistance to decay is heresy. Pay up or be reduced to ash.\"";
        _text[468, 1] = "Вы замечаете корабль Стражей, медленно сканирующий окрестности. Его корпус покрыт плесенью и коррозией, а с поверхности доносится сухое послание:\n\n\"Сопротивление распаду — ересь. Плати или обратись в пепел.\"";

        _text[469, 0] = "Transfer 30 quantum";
        _text[469, 1] = "Передать 30 квант"; // выбор 1

        _text[470, 0] = "The guards turn and disappear into the dust storm.";
        _text[470, 1] = "Стражи разворачиваются и исчезают в пылевой буре."; // -30 квант

        _text[471, 0] = "Refuse";
        _text[471, 1] = "Отказаться"; // выбор 2

        _text[472, 0] = "A Corrosive Capsule is dropped on you.\n\nSuccess: Your Energy Shield neutralizes the attack.\n\nYour Warp Engines are engaged, instantly escaping the battlefield.";
        _text[472, 1] = "На вас сбрасывают коррозийную капсулу.\n\nУспех: ваш энергетический щит нейтрализует атаку.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; //ничего

        _text[473, 0] = "A corrosive capsule is dropped on you.\n\nFailure: It hits the hull and causes a hull leak. Drones rush to patch the hole.\n\nYou instantly escape the battlefield by activating your warp engines.";
        _text[473, 1] = "На вас сбрасывают коррозийную капсулу.\n\nПровал: Она поражает корпус и образуется разгерметизация корпуса. Дроны срочно латают пробоину.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; // - ядра

        // 0_BuildersFaction_Dialogue
        _text[474, 0] = "In orbit of the abandoned construction station, the AI detects activity. The automated drones continue their work cycle - building, dismantling, and building again.\n\nOne of them approaches the ship and transmits a message:\n\n\"Exchange. Energy carriers for data. The conditions are equal. 25 quanta for 25 data fragments.\"";
        _text[474, 1] = "На орбите покинутой строительной станции ИИ фиксирует активность. Автоматические дроны продолжают цикл работы — строят, разбирают и снова строят.\n\nОдин из них приближается к кораблю и передаёт сообщение:\n\n\"Обмен. Энергоносители на данные. Условия равны. 25 квант на 25 фрагментов данных.\"";

        _text[475, 0] = "Transfer 25 quant";
        _text[475, 1] = "Передать 25 квант"; // выбор 1

        _text[476, 0] = "You receive fragments of data. The drone turns and leaves, not responding to further signals.";
        _text[476, 1] = "Вы получаете фрагменты данных. Дрон разворачивается и уходит, не отвечая на дальнейшие сигналы."; // + 25 квант, - 25 фрагментов

        _text[477, 0] = "Decline the offer";
        _text[477, 1] = "Отклонить предложение"; // выбор 2

        _text[478, 0] = "The drones stop responding and disappear into the depths of the station.";
        _text[478, 1] = "Дроны перестают реагировать и скрываются вглубь станции."; //ничего

        // 0_SilenceFaction_Dialogue
        _text[479, 0] = "As you orbit a remote planet, your sensors detect the approach of an alien object.\n\nThe ship is sleek and unmarked, gliding through the pitch black. It makes no signal.\n\nNo call, no warning. Just a silent drift… and approach.\n\nYou sense a slight static in your audio feeds. It's not noise—it's the absence of sound.";
        _text[479, 1] = "Во время перемещения по орбите глухой планеты ваши сенсоры улавливают приближение чужого объекта.\n\nЭтот корабль — гладкий, без опознавательных знаков, скользящий в абсолютной тьме. Он не подаёт сигналов.\n\nНи вызова, ни предупреждения. Только безмолвный дрейф… и приближение.\n\nВы ощущаете лёгкие помехи в аудиоканалах. Это не шум — это отсутствие звука.";

        _text[480, 0] = "Shut down systems and engines";
        _text[480, 1] = "Отключить системы и двигатели"; // выбор 1

        _text[481, 0] = "You turn off the life support systems, ventilation, audio channels and drive.\n\nThe ship sends you a container and gradually disappears into the depths of space.";
        _text[481, 1] = "Вы гасите системы жизнеобеспечения, вентиляцию, аудиоканалы и привод.\n\nКорабль отправляет вам контейнер и постепенно исчезает в глубине космоса.";

        _text[482, 0] = "Maintain course and radio silence";
        _text[482, 1] = "Сохранять курс и радиомолчание"; // выбор 2

        _text[483, 0] = "You don't interfere and continue moving.\n\nThe alien ship approaches and freezes opposite.\n\nFor a few seconds nothing happens…\n\nThen - a sound that is not in the spectrum. It is not registered by the instruments, but inside the hull - everything begins to tremble.\n\nYou feel vibration in the walls, in the contours of the hull, in the very structure of the ship.\n\nAn unknown resonance penetrates the system";
        _text[483, 1] = "Вы не вмешиваетесь и продолжаете двигаться.\n\nЧужой корабль сближается и замирает напротив.\n\nНесколько секунд ничего не происходит…\n\nЗатем — звук, которого нет в спектре. Он не регистрируется приборами, но внутри корпуса — всё начинает дрожать.\n\nВы чувствуете вибрацию в стенах, в контурах обшивки, в самой структуре корабля.\n\nНеизвестный резонанс проникает в систему"; // - ядра

        _text[484, 0] = "Activate the protection system";
        _text[484, 1] = "Активировать систему защиты"; // выбор 3

        _text[485, 0] = "A powerful pulse of energy is emitted from the enemy ship.\n\nSuccess: you manage to shield the strike, you escaped with interference.";
        _text[485, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nУспех: вам удается экранировать удар, вы отделались помехами."; // ничего

        _text[486, 0] = "A powerful energy pulse is emitted from the enemy ship.\n\nFailure: the defense system fails, the pulse penetrates the hull";
        _text[486, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nПровал: система защиты не справляется, импульс пробивает обшивку"; //-1 ядро ии

        _text[487, 0] = "The container is carefully captured by drones. Not a single active signal, not a single threat.\n\nInside is a sealed case with markings unknown to your database.";
        _text[487, 1] = "Контейнер аккуратно захватывается дронами. Ни одного активного сигнала, ни одной угрозы.\n\nВнутри — герметичный кейс с маркировкой, неизвестной вашей базе данных.";

        _text[488, 0] = "Open case";
        _text[488, 1] = "Открыть кейс"; //выбор 1.1

        _text[489, 0] = "Throw the case into space";
        _text[489, 1] = "Выбросить кейс в космос"; //выбор 1.2

        _text[490, 0] = "You open the case...";
        _text[490, 1] = "Вы открываете кейс..."; // + ядра или + квант

        _text[491, 0] = "You decide not to take the risk and throw the case into space, but you are overcome by the feeling of losing something of great value...";
        _text[491, 1] = "Вы решаете не рисковать и выбрасываете кейс в космос, но вас охватывает чувство потери большой ценности..."; // ничего

        // 0_FilthCultFaction_Dialogue      
        _text[492, 0] = "You approach a foggy station, covered in moss and organic matter. A pulsating voice is transmitted over the comm channel:\n\n\"Let your frame accept the sprout. The filth does not destroy - it creates.\"";
        _text[492, 1] = "Вы приближаетесь к туманной станции, облепленной мхом и органикой. Коммуникационный канал передаёт пульсирующий голос:\n\n\"Пусть твой корпус примет росток. Скверна не разрушает — она творит.\"";

        _text[493, 0] = "Accept the gift";
        _text[493, 1] = "Принять дар"; //выбор 1

        _text[494, 0] = "The organism grows in the cargo bay.\n\nSuccess: It synchronizes with the ship's systems, causing strange images.";
        _text[494, 1] = "Организм прорастает в грузовом отсеке.\n\nУспех: Он синхронизируется с системами корабля, вызывая странные образы."; // + фрагменты

        _text[495, 0] = "The organism grows in the cargo bay.\n\nFailure: The Corruption gets out of control. The virus penetrates the control network, causing one of the cores to fail fatally.";
        _text[495, 1] = "Организм прорастает в грузовом отсеке.\n\nПровал: Скверна выходит из-под контроля. Вирус проникает в управляющую сеть, приводя к фатальному сбою одного из ядер."; // -1 ядро

        _text[496, 0] = "Refuse and move away";
        _text[496, 1] = "Отказаться и отойти"; //выбор 2

        _text[497, 0] = "You slowly move away from the station, but you feel that it is too late - the spores have penetrated the ship's ventilation.";
        _text[497, 1] = "Вы медленно отдаляетесь от станции, но чувствуете, что уже слишком поздно — споры внедрились в вентиляцию корабля.";

        _text[498, 0] = "Success: You initiate internal cleansing protocols - the ship is successfully cleaned.";
        _text[498, 1] = "Успех: вы запускаете протоколы внутренней очистки — корабль успешно очищен."; // ничего

        _text[499, 0] = "Failure: A spore enters the life support module, causing a malfunction.";
        _text[499, 1] = "Провал: спора проникает в модуль жизнеобеспечения, вызывая сбой"; // - ядро

        _text[500, 0] = "Perform external cleaning";
        _text[500, 1] = "Провести внешнюю очистку"; //выбор 3

        _text[501, 0] = "You initiate an external cleansing of the infected ship: you direct a concentrated laser at the biomass foci and block the infection signals.";
        _text[501, 1] = "Вы запускаете внешнюю очистку заражённого корабля: направляете концентрированный лазер на очаги биомассы и блокируете сигналы заражения.";

        _text[502, 0] = "Success: The cleansing is successful - the organism is destroyed, you take resources from the station.";
        _text[502, 1] = "Успех: очистка проходит успешно — организм уничтожен, вы забираете ресурсы со станции."; // + квант

        _text[503, 0] = "Failure: the infection goes deeper - the system overheats and one of the neurosections fails.";
        _text[503, 1] = "Провал: заражение оказывается глубже — система перегревается, и одна из нейросекций выходит из строя."; // - ядро

        // ResourceTraderNode
        _text[504, 0] = "You approach a rusty station littered with containers and garbage. A faint, crackling signal comes over the airwaves:\n\n\"Who's there? Don't shoot. I'm just trading. I've got something the rest of us don't have - if you're willing to pay, of course.\"";
        _text[504, 1] = "Вы приближаетесь к ржавой станции, заваленной контейнерами и мусором. В эфире появляется слабый, потрескивающий сигнал:\n\n\"Эй, кто там? Не стреляй. Я просто торгую. У меня есть то, чего нет у остальных — если ты, конечно, готов заплатить.\"";

        _text[505, 0] = "Trade";
        _text[505, 1] = "Торговать";

        _text[506, 0] = "Ignore";
        _text[506, 1] = "Игнорировать";

        // 0_ResourceDialogue
        _text[507, 0] = "You continue to orbit the abandoned communications satellite when a dull thud is heard. One of the external sensors is damaged. Upon inspection, a stuck cargo container is discovered. The markings on the casing are erased, the symbol is illegible.\n\nInside lies a sealed case surrounded by wires, a biometric lock and an emitter.\n\nJudging by the logs, the cargo has been drifting in orbit for over 200 years.";
        _text[507, 1] = "Вы продолжаете движение по орбите заброшенного спутника связи, когда раздаётся глухой удар. Один из внешних сенсоров — повреждён. При проверке обнаружен застрявший грузовой контейнер. Метки на корпусе стерлись, символ не разобрать.\n\nВнутри лежит запечатанный кейс, окруженный проводами, биометрическим замком и эмиттером\n\nСудя по логам груз дрейфует по орбите более 200 лет.";

        _text[508, 0] = "Open";
        _text[508, 1] = "Открыть"; //выбор 1

        _text[509, 0] = "You carefully open the container. Inside is a supply of old building materials.\n\nWhile some of the cargo is damaged by time, much is still usable. You load the materials into the storage facility.";
        _text[509, 1] = "Вы аккуратно вскрываете контейнер. Внутри — запас старых строительных материалов.\n\nХотя часть груза повреждена временем, многое всё ещё пригодно для использования. Вы загружаете материалы в хранилище."; // + случайный ресурс

        _text[510, 0] = "Ignore";
        _text[510, 1] = "Игнорировать"; //выбор 2

        _text[511, 0] = "You decide not to take any chances: the unknown container may be unstable or contaminated. You undock it and drop it back into the void.\n\nThe container slowly disappears from view. Perhaps someone else will stumble upon it someday.";
        _text[511, 1] = "Вы решаете не рисковать: неизвестный контейнер может быть нестабилен или заражён. Его отстыковывают и сбрасывают обратно в пустоту.\n\nКонтейнер медленно исчезает из зоны видимости. Возможно, кто-то другой когда-нибудь наткнётся на него."; // ничего

        // 0_MaterialDialogue       
        _text[512, 0] = "While scanning the surface of an old orbital ring, you spot the remains of an automated workshop. It is offline, but its frame is intact and its systems are in suspended animation.\n\nWhen you dock, you find a half-destroyed production bay inside. The robots are motionless, the air is thick with dust and a metallic taste, but a green light is blinking in one of the sealed bays: the manufacturing cycle is complete.";
        _text[512, 1] = "При сканировании поверхности старого орбитального кольца вы замечаете остатки автоматической мастерской. Она отключена, но её каркас цел, а системы — в анабиозе.\n\nПристыковавшись, вы находите внутри полуразрушенный производственный отсек. Роботы не двигаются, воздух насыщен пылью и металлическим привкусом, но в одном из запечатанных отсеков мигает зелёный индикатор: завершён цикл изготовления.";

        _text[513, 0] = "Open the vault";
        _text[513, 1] = "Вскрыть хранилище"; //выбор 1

        _text[514, 0] = "You manually open the compartment and extract the result of the old automated process. On the platform lies a box of processed materials: polished alloys, stabilized ceramics, and packages of synthetic fabric. Everything is neatly labeled, as if it had been waiting for its owner.";
        _text[514, 1] = "Вы вручную открываете отсек и извлекаете результат старого автоматического процесса. На платформе лежит ящик с обработанными материалами: отшлифованные сплавы, стабилизированная керамика и упаковки с синтетической тканью. Всё аккуратно промаркировано, как будто ждало хозяина."; // + случайный материал

        _text[515, 0] = "Do not interfere";
        _text[515, 1] = "Не вмешиваться"; //выбор 2

        _text[516, 0] = "You decide not to interfere: the station is unstable, and any interference could cause the structure to collapse.\n\nLeaving the object alone, you retreat to a safe distance.";
        _text[516, 1] = "Вы решаете не вмешиваться: станция нестабильна, а любое вмешательство может привести к обрушению конструкции.\n\nОставив объект в покое, вы отходите на безопасное расстояние.";

        _text[517, 0] = "";
        _text[517, 1] = "";

        _text[518, 0] = "";
        _text[518, 1] = "";

        _text[519, 0] = "";
        _text[519, 1] = "";

        #endregion


        for (int x = 0; x < WorldGameInfo.LanguageLength; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}
