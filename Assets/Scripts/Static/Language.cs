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
            case "french": return 2;
            case "italian": return 3;
            case "german": return 4;
            case "spanish": return 5; case "latam": return 5;
            case "polish": return 6;
            case "brazilian": return 7;
            case "japanese": return 8;
            case "schinese": return 9; case "tchinese": return 9;
        }
    }

    public void SetLanguage()
    {
        _text[0, 0] = "Tin Lord";
        _text[0, 1] = "Жестяной Лорд";
        _text[0, 2] = "Tin Lord";
        _text[0, 3] = "Tin Lord";
        _text[0, 4] = "Tin Lord";
        _text[0, 5] = "Tin Lord";
        _text[0, 6] = "Tin Lord";
        _text[0, 7] = "Tin Lord";
        _text[0, 8] = "Tin Lord";
        _text[0, 9] = "Tin Lord";

        _text[1, 0] = "Recipe";
        _text[1, 1] = "Рецепт";
        _text[1, 2] = "Recette";
        _text[1, 3] = "Ricetta";
        _text[1, 4] = "Rezept";
        _text[1, 5] = "Receta";
        _text[1, 6] = "Receptura";
        _text[1, 7] = "Receita";
        _text[1, 8] = "";
        _text[1, 9] = "";

        _text[2, 0] = "Building";
        _text[2, 1] = "Здание";
        _text[2, 2] = "Bâtiment";
        _text[2, 3] = "Edificio";
        _text[2, 4] = "Gebäude";
        _text[2, 5] = "Edificio";
        _text[2, 6] = "Budynek";
        _text[2, 7] = "Edifício";
        _text[2, 8] = "";
        _text[2, 9] = "";

        _text[3, 0] = "Building level";
        _text[3, 1] = "Уровень здания";
        _text[3, 2] = "Niveau du bâtiment";
        _text[3, 3] = "Livello dell'edificio";
        _text[3, 4] = "Gebäudestufe";
        _text[3, 5] = "Nivel del edificio";
        _text[3, 6] = "Poziom budynku";
        _text[3, 7] = "Nível do edifício";
        _text[3, 8] = "";
        _text[3, 9] = "";

        // это для кнопки
        _text[4, 0] = "Repair";
        _text[4, 1] = "Починить";
        _text[4, 2] = "Réparez-le";
        _text[4, 3] = "Ripara";
        _text[4, 4] = "Reparieren";
        _text[4, 5] = "Reparar";
        _text[4, 6] = "Napraw";
        _text[4, 7] = "Reparar";
        _text[4, 8] = "";
        _text[4, 9] = "";

        _text[5, 0] = "RAD";
        _text[5, 1] = "РАД";
        _text[5, 2] = "RAD";
        _text[5, 3] = "RAD";
        _text[5, 4] = "RAD";
        _text[5, 5] = "RAD";
        _text[5, 6] = "RAD";
        _text[5, 7] = "RAD";
        _text[5, 8] = "";
        _text[5, 9] = "";

        _text[6, 0] = "Production resource";
        _text[6, 1] = "Добываемый ресурс";
        _text[6, 2] = "Ressource extraite";
        _text[6, 3] = "Risorsa estratta";
        _text[6, 4] = "Abbaubare Ressource";
        _text[6, 5] = "Recurso extraído";
        _text[6, 6] = "Pozyskiwany surowiec";
        _text[6, 7] = "Recurso extraído";
        _text[6, 8] = "";
        _text[6, 9] = "";

        _text[7, 0] = "Resources";
        _text[7, 1] = "Ресурсы";
        _text[7, 2] = "Ressources";
        _text[7, 3] = "Risorse";
        _text[7, 4] = "Ressourcen";
        _text[7, 5] = "Recursos";
        _text[7, 6] = "Zasoby";
        _text[7, 7] = "Recursos";
        _text[7, 8] = "";
        _text[7, 9] = "";

        _text[8, 0] = "Materials";
        _text[8, 1] = "Материалы";
        _text[8, 2] = "Matériels";
        _text[8, 3] = "Materiali";
        _text[8, 4] = "Materialien";
        _text[8, 5] = "Materiales";
        _text[8, 6] = "Materiały";
        _text[8, 7] = "Materiais";
        _text[8, 8] = "";
        _text[8, 9] = "";

        _text[9, 0] = "Components";
        _text[9, 1] = "Компоненты";
        _text[9, 2] = "Composants";
        _text[9, 3] = "Componenti";
        _text[9, 4] = "Komponenten";
        _text[9, 5] = "Componentes";
        _text[9, 6] = "Komponenty";
        _text[9, 7] = "Componentes";
        _text[9, 8] = "";
        _text[9, 9] = "";

        _text[10, 0] = "Select building type";
        _text[10, 1] = "Выберите тип здания";
        _text[10, 2] = "Sélectionnez le type de bâtiment";
        _text[10, 3] = "Seleziona il tipo di edificio";
        _text[10, 4] = "Wähle einen Gebäudetyp";
        _text[10, 5] = "Elige el tipo de edificio";
        _text[10, 6] = "Wybierz typ budynku";
        _text[10, 7] = "Selecione o tipo de edifício";
        _text[10, 8] = "";
        _text[10, 9] = "";

        _text[11, 0] = "Production modifier";
        _text[11, 1] = "Модификатор добычи";
        _text[11, 2] = "Modificateur de butin";
        _text[11, 3] = "Modificatore di estrazione";
        _text[11, 4] = "Beutemodifikator";
        _text[11, 5] = "Modificador de extracción";
        _text[11, 6] = "Modyfikator wydobycia";
        _text[11, 7] = "Modificador de extração";
        _text[11, 8] = "";
        _text[11, 9] = "";

        _text[12, 0] = "Select a building";
        _text[12, 1] = "Выберите здание";
        _text[12, 2] = "Sélectionnez un bâtiment";
        _text[12, 3] = "Seleziona un edificio";
        _text[12, 4] = "Wähle ein Gebäude";
        _text[12, 5] = "Elige un edificio";
        _text[12, 6] = "Wybierz budynek";
        _text[12, 7] = "Selecione um edifício";
        _text[12, 8] = "";
        _text[12, 9] = "";

        _text[13, 0] = "Buildings";
        _text[13, 1] = "Постройки";
        _text[13, 2] = "Bâtiments";
        _text[13, 3] = "Costruzioni";
        _text[13, 4] = "Bauten";
        _text[13, 5] = "Construcciones";
        _text[13, 6] = "Budowle";
        _text[13, 7] = "Construções";
        _text[13, 8] = "";
        _text[13, 9] = "";

        _text[14, 0] = "Resource for work";
        _text[14, 1] = "Ресурс для работы";
        _text[14, 2] = "Ressource pour le travail";
        _text[14, 3] = "Risorsa di funzionamento";
        _text[14, 4] = "Ressource für die Arbeit";
        _text[14, 5] = "Recurso para operar";
        _text[14, 6] = "Zasób do pracy";
        _text[14, 7] = "Recurso de funcionamento";
        _text[14, 8] = "";
        _text[14, 9] = "";

        _text[15, 0] = "Ground ecology";
        _text[15, 1] = "Экология земли";
        _text[15, 2] = "Écologie de la Terre";
        _text[15, 3] = "Ecologia del terreno";
        _text[15, 4] = "Ökologie der Erde";
        _text[15, 5] = "Ecología del terreno";
        _text[15, 6] = "Ekologia terenu";
        _text[15, 7] = "Ecologia do terreno";
        _text[15, 8] = "";
        _text[15, 9] = "";

        _text[16, 0] = "Building ecology";
        _text[16, 1] = "Экология здания";
        _text[16, 2] = "Écologie du bâtiment";
        _text[16, 3] = "Ecologia dell'edificio";
        _text[16, 4] = "Ökologie des Gebäudes";
        _text[16, 5] = "Ecología del edificio";
        _text[16, 6] = "Ekologia budynku";
        _text[16, 7] = "Ecologia do edifício";
        _text[16, 8] = "";
        _text[16, 9] = "";

        _text[17, 0] = "Other";
        _text[17, 1] = "Другое";
        _text[17, 2] = "Autre";
        _text[17, 3] = "Altro";
        _text[17, 4] = "Andere";
        _text[17, 5] = "Otros";
        _text[17, 6] = "Inne";
        _text[17, 7] = "Outros";
        _text[17, 8] = "";
        _text[17, 9] = "";

        _text[18, 0] = "Durability";
        _text[18, 1] = "Прочность";
        _text[18, 2] = "Force";
        _text[18, 3] = "Integrità";
        _text[18, 4] = "Stärke";
        _text[18, 5] = "Durabilidad";
        _text[18, 6] = "Wytrzymałość";
        _text[18, 7] = "Durabilidade";
        _text[18, 8] = "";
        _text[18, 9] = "";

        _text[19, 0] = "Increase Damage";
        _text[19, 1] = "Повышение Урона";
        _text[19, 2] = "Augmentation des dégâts";
        _text[19, 3] = "Aumento danni";
        _text[19, 4] = "Schaden erhöhen";
        _text[19, 5] = "Aumento de daño";
        _text[19, 6] = "Zwiększenie obrażeń";
        _text[19, 7] = "Aumento de Dano";
        _text[19, 8] = "";
        _text[19, 9] = "";

        _text[20, 0] = "Increase Durability";
        _text[20, 1] = "Повышение Прочности";
        _text[20, 2] = "Augmentation de la force";
        _text[20, 3] = "Aumento integrità";
        _text[20, 4] = "Stärkung";
        _text[20, 5] = "Aumento de durabilidad";
        _text[20, 6] = "Zwiększenie wytrzymałości";
        _text[20, 7] = "Aumento de Durabilidade";
        _text[20, 8] = "";
        _text[20, 9] = "";

        _text[21, 0] = "Machines";
        _text[21, 1] = "Машины";
        _text[21, 2] = "Voitures";
        _text[21, 3] = "Macchine";
        _text[21, 4] = "Maschinen";
        _text[21, 5] = "Máquinas";
        _text[21, 6] = "Maszyny";
        _text[21, 7] = "Máquinas";
        _text[21, 8] = "";
        _text[21, 9] = "";

        _text[22, 0] = "Demolish the building?";
        _text[22, 1] = "Разрушить здание?";
        _text[22, 2] = "Détruire le bâtiment ?";
        _text[22, 3] = "Demolire l'edificio?";
        _text[22, 4] = "Das Gebäude zerstören?";
        _text[22, 5] = "¿Destruir el edificio?";
        _text[22, 6] = "Zburzyć budynek?";
        _text[22, 7] = "Demolir o edifício?";
        _text[22, 8] = "";
        _text[22, 9] = "";

        _text[23, 0] = "After destruction you will receive:";
        _text[23, 1] = "После разрушения вы получите:";
        _text[23, 2] = "Après destruction, vous recevrez :";
        _text[23, 3] = "Dopo la demolizione riceverai:";
        _text[23, 4] = "Nach der Zerstörung erhalten Sie:";
        _text[23, 5] = "Tras destruirlo recibirás:";
        _text[23, 6] = "Po zburzeniu otrzymasz:";
        _text[23, 7] = "Após a demolição, você receberá:";
        _text[23, 8] = "";
        _text[23, 9] = "";

        _text[24, 0] = "Destroy the landscape?";
        _text[24, 1] = "Уничтожить ландшафт?";
        _text[24, 2] = "Détruire le paysage ?";
        _text[24, 3] = "Distruggere il paesaggio?";
        _text[24, 4] = "Die Landschaft zerstören?";
        _text[24, 5] = "¿Destruir el paisaje?";
        _text[24, 6] = "Zniszczyć krajobraz?";
        _text[24, 7] = "Destruir a paisagem?";
        _text[24, 8] = "";
        _text[24, 9] = "";

        _text[25, 0] = "Destruction requires:";
        _text[25, 1] = "Для уничтожения требуется:";
        _text[25, 2] = "Pour détruire, il vous faut :";
        _text[25, 3] = "Per distruggere è necessario:";
        _text[25, 4] = "Zum Zerstören benötigt man:";
        _text[25, 5] = "Para destruirlo se requiere:";
        _text[25, 6] = "Do zniszczenia potrzebujesz:";
        _text[25, 7] = "Para destruir, é necessário:";
        _text[25, 8] = "";
        _text[25, 9] = "";

        _text[26, 0] = "Continue game";
        _text[26, 1] = "Продолжить игру";
        _text[26, 2] = "Continuez la partie";
        _text[26, 3] = "Continua";
        _text[26, 4] = "Spiel fortsetzen";
        _text[26, 5] = "Continuar";
        _text[26, 6] = "Kontynuuj grę";
        _text[26, 7] = "Continuar jogo";
        _text[26, 8] = "";
        _text[26, 9] = "";

        _text[27, 0] = "New game";
        _text[27, 1] = "Новая игра";
        _text[27, 2] = "Nouveau jeu";
        _text[27, 3] = "Nuova partita";
        _text[27, 4] = "Neues Spiel";
        _text[27, 5] = "Nueva partida";
        _text[27, 6] = "Nowa gra";
        _text[27, 7] = "Novo jogo";
        _text[27, 8] = "";
        _text[27, 9] = "";

        _text[28, 0] = "Settings";
        _text[28, 1] = "Настройки";
        _text[28, 2] = "Paramètres";
        _text[28, 3] = "Impostazioni";
        _text[28, 4] = "Einstellungen";
        _text[28, 5] = "Ajustes";
        _text[28, 6] = "Ustawienia";
        _text[28, 7] = "Definições";
        _text[28, 8] = "";
        _text[28, 9] = "";

        _text[29, 0] = "Quit";
        _text[29, 1] = "Выход";
        _text[29, 2] = "Sortie";
        _text[29, 3] = "Esci";
        _text[29, 4] = "Beenden";
        _text[29, 5] = "Salir";
        _text[29, 6] = "Wyjście";
        _text[29, 7] = "Sair";
        _text[29, 8] = "";
        _text[29, 9] = "";

        _text[30, 0] = "Loading";
        _text[30, 1] = "Загрузка";
        _text[30, 2] = "Chargement";
        _text[30, 3] = "Caricamento";
        _text[30, 4] = "Laden";
        _text[30, 5] = "Cargando";
        _text[30, 6] = "Ładowanie";
        _text[30, 7] = "Carregamento";
        _text[30, 8] = "";
        _text[30, 9] = "";

        _text[31, 0] = "Are you sure you want to start a new game?\n\nYour past save will be overwritten.";
        _text[31, 1] = "Вы уверены, что хотите начать новую игру?\n\nВаше прошлое сохранение будет перезаписано.";
        _text[31, 2] = "Êtes-vous sûr de vouloir commencer une nouvelle partie?\n\nVotre sauvegarde précédente sera nécrasée.";
        _text[31, 3] = "Sei sicuro di voler iniziare una nuova partita?\n\nIl tuo salvataggio precedente verrà sovrascritto.";
        _text[31, 4] = "Bist du sicher, dass du ein neues Spiel starten möchtest?\n\nDein vorheriger Spielstand wird überschrieben.";
        _text[31, 5] = "¿Seguro que quieres empezar una nueva partida?\n\nTu guardado anterior se sobrescribirá.";
        _text[31, 6] = "Czy na pewno chcesz rozpocząć nową grę?\n\nTwoje poprzednie zapisane dane zostaną nadpisane.";
        _text[31, 7] = "Tem a certeza de que quer iniciar um novo jogo?\n\nO seu salvamento anterior será substituído.";
        _text[31, 8] = "";
        _text[31, 9] = "";

        _text[32, 0] = "Command Center";
        _text[32, 1] = "Командный Центр";
        _text[32, 2] = "Centre de commandement";
        _text[32, 3] = "Centro di comando";
        _text[32, 4] = "Kommandzentrale";
        _text[32, 5] = "Centro de mando";
        _text[32, 6] = "Centrum Dowodzenia";
        _text[32, 7] = "Centro de Comando";
        _text[32, 8] = "";
        _text[32, 9] = "";

        _text[33, 0] = "Continue";
        _text[33, 1] = "Продолжить";
        _text[33, 2] = "Continuer";
        _text[33, 3] = "Continua";
        _text[33, 4] = "Weitermachen";
        _text[33, 5] = "Continuar";
        _text[33, 6] = "Kontynuuj";
        _text[33, 7] = "Continuar";
        _text[33, 8] = "";
        _text[33, 9] = "";

        _text[34, 0] = "Ecology level";
        _text[34, 1] = "Уровень экологии";
        _text[34, 2] = "Niveau écologique";
        _text[34, 3] = "Livello di ecologia";
        _text[34, 4] = "Ökologieebene";
        _text[34, 5] = "Nivel de ecología";
        _text[34, 6] = "Poziom ekologii";
        _text[34, 7] = "Nível de ecologia";
        _text[34, 8] = "";
        _text[34, 9] = "";

        _text[35, 0] = "Starting resources";
        _text[35, 1] = "Начальные ресурсы";
        _text[35, 2] = "Ressources initiales";
        _text[35, 3] = "Risorse iniziali";
        _text[35, 4] = "Erste Ressourcen";
        _text[35, 5] = "Recursos iniciales";
        _text[35, 6] = "Zasoby początkowe";
        _text[35, 7] = "Recursos iniciais";
        _text[35, 8] = "";
        _text[35, 9] = "";

        _text[36, 0] = "Objectives";
        _text[36, 1] = "Цели";
        _text[36, 2] = "Objectifs";
        _text[36, 3] = "Obiettivi";
        _text[36, 4] = "Ziele";
        _text[36, 5] = "Objetivos";
        _text[36, 6] = "Cele";
        _text[36, 7] = "Objetivos";
        _text[36, 8] = "";
        _text[36, 9] = "";

        _text[37, 0] = "days";
        _text[37, 1] = "дней";
        _text[37, 2] = "jours";
        _text[37, 3] = "giorni";
        _text[37, 4] = "tage";
        _text[37, 5] = "días";
        _text[37, 6] = "dni";
        _text[37, 7] = "dias";
        _text[37, 8] = "";
        _text[37, 9] = "";

        _text[38, 0] = "TERMINAL #042";
        _text[38, 1] = "ТЕРМИНАЛ #042";
        _text[38, 2] = "TERMINAL #042";
        _text[38, 3] = "TERMINALE #042";
        _text[38, 4] = "TERMINAL #042";
        _text[38, 5] = "TERMINAL #042";
        _text[38, 6] = "TERMINAL #042";
        _text[38, 7] = "TERMINAL #042";
        _text[38, 8] = "";
        _text[38, 9] = "";

        _text[39, 0] = "Restore the ecology to";
        _text[39, 1] = "Восстановить экологию до";
        _text[39, 2] = "Restaurer l'écologie";
        _text[39, 3] = "Ripristina l'ecologia a";
        _text[39, 4] = "Ökologie wiederherstellen bis";
        _text[39, 5] = "Restaurar la ecología hasta";
        _text[39, 6] = "Przywróć ekologię do";
        _text[39, 7] = "Restaurar a ecologia até";
        _text[39, 8] = "";
        _text[39, 9] = "";

        _text[40, 0] = "Kill {0} enemies";
        _text[40, 1] = "Убить {0} врагов";
        _text[40, 2] = "Tuez {0} ennemis";
        _text[40, 3] = "Uccidi {0} nemici";
        _text[40, 4] = "Töte {0} gegner";
        _text[40, 5] = "Mata a {0} enemigos";
        _text[40, 6] = "Zabij {0} wrogów";
        _text[40, 7] = "Matar {0} inimigos";
        _text[40, 8] = "";
        _text[40, 9] = "";

        _text[41, 0] = "Construct {0} buildings";
        _text[41, 1] = "Построить {0} зданий";
        _text[41, 2] = "Construisez {0} bâtiments";
        _text[41, 3] = "Costruisci {0} edifici";
        _text[41, 4] = "Baue {0} gebäude";
        _text[41, 5] = "Construye {0} edificios";
        _text[41, 6] = "Zbuduj {0} budynków";
        _text[41, 7] = "Construir {0} edifícios";
        _text[41, 8] = "";
        _text[41, 9] = "";

        _text[42, 0] = "Survive {0} days";
        _text[42, 1] = "Выжить {0} дней";
        _text[42, 2] = "Survivre {0} jours";
        _text[42, 3] = "Sopravvivi {0} giorni";
        _text[42, 4] = "Überlebe {0} tage";
        _text[42, 5] = "Sobrevive {0} días";
        _text[42, 6] = "Przetrwaj {0} dni";
        _text[42, 7] = "Sobreviver {0} dias";
        _text[42, 8] = "";
        _text[42, 9] = "";

        _text[43, 0] = "You need to open";
        _text[43, 1] = "Вам нужно открыть";
        _text[43, 2] = "Vous devez ouvrir";
        _text[43, 3] = "Devi sbloccare";
        _text[43, 4] = "Du musst freischalten";
        _text[43, 5] = "Necesitas desbloquear";
        _text[43, 6] = "Musisz odblokować";
        _text[43, 7] = "Você precisa desbloquear";
        _text[43, 8] = "";
        _text[43, 9] = "";

        _text[44, 0] = "Escape";
        _text[44, 1] = "Сбежать";
        _text[44, 2] = "S'échapper";
        _text[44, 3] = "Fuggi";
        _text[44, 4] = "Fliehen";
        _text[44, 5] = "Escapar";
        _text[44, 6] = "Uciec";
        _text[44, 7] = "Fugir";
        _text[44, 8] = "";
        _text[44, 9] = "";

        _text[45, 0] = "Restart";
        _text[45, 1] = "Перезапуск";
        _text[45, 2] = "Redémarrage";
        _text[45, 3] = "Riavvio";
        _text[45, 4] = "Neustart";
        _text[45, 5] = "Reiniciar";
        _text[45, 6] = "Restart";
        _text[45, 7] = "Reiniciar";
        _text[45, 8] = "";
        _text[45, 9] = "";

        _text[46, 0] = "Exit";
        _text[46, 1] = "Выход";
        _text[46, 2] = "Sortie";
        _text[46, 3] = "Esci";
        _text[46, 4] = "Verlassen";
        _text[46, 5] = "Salir";
        _text[46, 6] = "Wyjście";
        _text[46, 7] = "Sair";
        _text[46, 8] = "";
        _text[46, 9] = "";

        _text[47, 0] = "Menu";
        _text[47, 1] = "Меню";
        _text[47, 2] = "Menu";
        _text[47, 3] = "Menu";
        _text[47, 4] = "Menü";
        _text[47, 5] = "Menú";
        _text[47, 6] = "Menu";
        _text[47, 7] = "Menu";
        _text[47, 8] = "";
        _text[47, 9] = "";

        _text[48, 0] = $"Are you sure you want to restart the mission?\n\n<color={Colors.HexWarningYellow}>You will lose one AI core.</color>";
        _text[48, 1] = $"Вы уверены, что хотите перезапустить миссию?\n\n<color={Colors.HexWarningYellow}>Вы потеряете одно ядро ИИ.</color>";
        _text[48, 2] = $"Êtes-vous sûr de vouloir redémarrer la mission?\n\n<color={Colors.HexWarningYellow}>Vous perdrez unnnoyau d'IA.</color>";
        _text[48, 3] = $"Sei sicuro di voler riavviare la missione?\n\n<color={Colors.HexWarningYellow}>Perderai un nucleo IA.</color>";
        _text[48, 4] = $"Bist du sicher, dass du die Mission neu starten möchtest?\n\n<color={Colors.HexWarningYellow}>Du verlierst einen KI-Kern.</color>";
        _text[48, 5] = $"¿Seguro que quieres reiniciar la misión?\n\n<color={Colors.HexWarningYellow}>Perderás un núcleo de IA.</color>";
        _text[48, 6] = $"Czy na pewno chcesz zrestartować misję?\n\n<color={Colors.HexWarningYellow}>Stracisz jeden rdzeń SI.</color>";
        _text[48, 7] = $"Tem a certeza de que quer reiniciar a missão?\n\n<color={Colors.HexWarningYellow}>Você perderá um núcleo de IA.</color>";
        _text[48, 8] = "";
        _text[48, 9] = "";

        _text[49, 0] = "Yes";
        _text[49, 1] = "Да";
        _text[49, 2] = "Oui";
        _text[49, 3] = "Sì";
        _text[49, 4] = "Ja";
        _text[49, 5] = "Sí";
        _text[49, 6] = "Tak";
        _text[49, 7] = "Sim";
        _text[49, 8] = "";
        _text[49, 9] = "";

        _text[50, 0] = "No";
        _text[50, 1] = "Нет";
        _text[50, 2] = "Non";
        _text[50, 3] = "No";
        _text[50, 4] = "Nein";
        _text[50, 5] = "No";
        _text[50, 6] = "Nie";
        _text[50, 7] = "Não";
        _text[50, 8] = "";
        _text[50, 9] = "";

        _text[51, 0] = "Start mission";
        _text[51, 1] = "Начать миссию";
        _text[51, 2] = "Démarrez la mission";
        _text[51, 3] = "Avvia missione";
        _text[51, 4] = "Mission starten";
        _text[51, 5] = "Iniciar misión";
        _text[51, 6] = "Rozpocznij misję";
        _text[51, 7] = "Iniciar missão";
        _text[51, 8] = "";
        _text[51, 9] = "";

        _text[52, 0] = "Load mission";
        _text[52, 1] = "Загрузить миссию";
        _text[52, 2] = "Charger la mission";
        _text[52, 3] = "Carica missione";
        _text[52, 4] = "Mission laden";
        _text[52, 5] = "Cargar misión";
        _text[52, 6] = "Wczytaj misję";
        _text[52, 7] = "Carregar missão";
        _text[52, 8] = "";
        _text[52, 9] = "";

        _text[53, 0] = "Construct";
        _text[53, 1] = "Построить";
        _text[53, 2] = "Construire";
        _text[53, 3] = "Costruisci";
        _text[53, 4] = "Bauen";
        _text[53, 5] = "Construir";
        _text[53, 6] = "Zbuduj";
        _text[53, 7] = "Construir";
        _text[53, 8] = "";
        _text[53, 9] = "";

        _text[54, 0] = "On / Off";
        _text[54, 1] = "Вкл / Выкл";
        _text[54, 2] = "Marche/Arrêt";
        _text[54, 3] = "On / Off";
        _text[54, 4] = "Ein / Aus";
        _text[54, 5] = "Activar / Desactivar";
        _text[54, 6] = "Wł. / Wył.";
        _text[54, 7] = "Ligar / Desligar";
        _text[54, 8] = "";
        _text[54, 9] = "";

        _text[55, 0] = "Rotate";
        _text[55, 1] = "Повернуть";
        _text[55, 2] = "Tourner";
        _text[55, 3] = "Ruota";
        _text[55, 4] = "Drehen";
        _text[55, 5] = "Rotar";
        _text[55, 6] = "Obróć";
        _text[55, 7] = "Rodar";
        _text[55, 8] = "";
        _text[55, 9] = "";

        _text[56, 0] = "Destroy";
        _text[56, 1] = "Разрушить";
        _text[56, 2] = "Détruire";
        _text[56, 3] = "Demolisci";
        _text[56, 4] = "Zerstören";
        _text[56, 5] = "Demoler";
        _text[56, 6] = "Zniszcz";
        _text[56, 7] = "Demolir";
        _text[56, 8] = "";
        _text[56, 9] = "";

        _text[57, 0] = "Enginery";
        _text[57, 1] = "Техника";
        _text[57, 2] = "Technique";
        _text[57, 3] = "Macchinari";
        _text[57, 4] = "Technik";
        _text[57, 5] = "Equipo";
        _text[57, 6] = "Sprzęt";
        _text[57, 7] = "Equipamento";
        _text[57, 8] = "";
        _text[57, 9] = "";

        _text[58, 0] = "Ecology restored: {0}/{1}";
        _text[58, 1] = "Экология восстановлена: {0}/{1}";
        _text[58, 2] = "Écologie restaurée: {0}/{1}";
        _text[58, 3] = "Ecologia ripristinata: {0}/{1}";
        _text[58, 4] = "Ökologie wiederhergestellt: {0}/{1}";
        _text[58, 5] = "Ecología restaurada: {0}/{1}";
        _text[58, 6] = "Ekologia przywrócona: {0}/{1}";
        _text[58, 7] = "Ecologia restaurada: {0}/{1}";
        _text[58, 8] = "";
        _text[58, 9] = "";

        _text[59, 0] = "Enemies killed: {0}/{1}";
        _text[59, 1] = "Убито врагов: {0}/{1}";
        _text[59, 2] = "Ennemis tués: {0}/{1}";
        _text[59, 3] = "Nemici uccisi: {0}/{1}";
        _text[59, 4] = "Gegner getötet: {0}/{1}";
        _text[59, 5] = "Enemigos eliminados: {0}/{1}";
        _text[59, 6] = "Zabici wrogowie: {0}/{1}";
        _text[59, 7] = "Inimigos eliminados: {0}/{1}";
        _text[59, 8] = "";
        _text[59, 9] = "";

        _text[60, 0] = "Buildings constructed: {0}/{1}";
        _text[60, 1] = "Построено зданий: {0}/{1}";
        _text[60, 2] = "Bâtiments construits: {0}/{1}";
        _text[60, 3] = "Edifici costruiti: {0}/{1}";
        _text[60, 4] = "Gebäude gebaut: {0}/{1}";
        _text[60, 5] = "Edificios construidos: {0}/{1}";
        _text[60, 6] = "Zbudowane budynki: {0}/{1}";
        _text[60, 7] = "Edifícios construídos: {0}/{1}";
        _text[60, 8] = "";
        _text[60, 9] = "";

        _text[61, 0] = "Days lived: {0}/{1}";
        _text[61, 1] = "Прожито дней: {0}/{1}";
        _text[61, 2] = "Jours vécus: {0}/{1}";
        _text[61, 3] = "Giorni sopravvissuti: {0}/{1}";
        _text[61, 4] = "Tage überlebt: {0}/{1}";
        _text[61, 5] = "Días sobrevividos: {0}/{1}";
        _text[61, 6] = "Przetrwane dni: {0}/{1}";
        _text[61, 7] = "Dias sobrevividos: {0}/{1}";
        _text[61, 8] = "";
        _text[61, 9] = "";

        _text[62, 0] = "Damage Increased";
        _text[62, 1] = "Урон Повышен";
        _text[62, 2] = "Dégâts Augmentés";
        _text[62, 3] = "Danni Aumentati";
        _text[62, 4] = "Schaden Erhöht";
        _text[62, 5] = "Daño Aumentado";
        _text[62, 6] = "Obrażenia Zwiększone";
        _text[62, 7] = "Dano Aumentado";
        _text[62, 8] = "";
        _text[62, 9] = "";

        _text[63, 0] = "Victory";
        _text[63, 1] = "Победа";
        _text[63, 2] = "Victoire";
        _text[63, 3] = "Vittoria";
        _text[63, 4] = "Sieg";
        _text[63, 5] = "Victoria";
        _text[63, 6] = "Zwycięstwo";
        _text[63, 7] = "Vitória";
        _text[63, 8] = "";
        _text[63, 9] = "";

        _text[64, 0] = "Defeat";
        _text[64, 1] = "Поражение";
        _text[64, 2] = "Défaite";
        _text[64, 3] = "Sconfitta";
        _text[64, 4] = "Niederlage";
        _text[64, 5] = "Derrota";
        _text[64, 6] = "Porażka";
        _text[64, 7] = "Derrota";
        _text[64, 8] = "";
        _text[64, 9] = "";

        _text[65, 0] = "Escape";
        _text[65, 1] = "Сбежал";
        _text[65, 2] = "Échappé";
        _text[65, 3] = "Fuggito";
        _text[65, 4] = "Geflohen";
        _text[65, 5] = "Escapó";
        _text[65, 6] = "Uciekł";
        _text[65, 7] = "Fugiu";
        _text[65, 8] = "";
        _text[65, 9] = "";

        _text[66, 0] = $"<color={Colors.HexWarningYellow}>Escaping the mission will give you {WorldGameInfo.EscapeFragmentsPercent}% of the data fragments and losing one AI core.</color>\n\nYou must complete half of the objectives.";
        _text[66, 1] = $"<color={Colors.HexWarningYellow}>Сбежав с миссии, вы получите {WorldGameInfo.EscapeFragmentsPercent}% от фрагментов данных и потеряете одно ядро ИИ.</color>\n\nНеобходимо выполнить половину поставленных целей.";
        _text[66, 2] = $"<color={Colors.HexWarningYellow}>En fuyant la mission, vous recevrez {WorldGameInfo.EscapeFragmentsPercent}% des fragments de données et perdrez un noyau d'IA.</color>\n\nVous devez accomplir la moitié des objectifs fixés.";
        _text[66, 3] = $"<color={Colors.HexWarningYellow}>Fuggendo dalla missione, otterrai il {WorldGameInfo.EscapeFragmentsPercent}% dei frammenti dati e perderai un nucleo IA.</color>\n\nDevi completare metà degli obiettivi assegnati.";
        _text[66, 4] = $"<color={Colors.HexWarningYellow}>Wenn du aus der Mission fliehst, erhältst du {WorldGameInfo.EscapeFragmentsPercent}% der Datenfragmente und verlierst einen KI-Kern.</color>\n\nDu musst die Hälfte der gesetzten Ziele erfüllen.";
        _text[66, 5] = $"<color={Colors.HexWarningYellow}>Al escapar de la misión, recibirás {WorldGameInfo.EscapeFragmentsPercent}% de los fragmentos de datos y perderás un núcleo de IA.</color>\n\nDebes completar la mitad de los objetivos.";
        _text[66, 6] = $"<color={Colors.HexWarningYellow}>Uciekając z misji, otrzymasz {WorldGameInfo.EscapeFragmentsPercent}% fragmentów danych i stracisz jeden rdzeń SI.</color>\n\nMusisz wykonać połowę wyznaczonych celów.";
        _text[66, 7] = $"<color={Colors.HexWarningYellow}>Ao fugir da missão, você receberá {WorldGameInfo.EscapeFragmentsPercent}% dos fragmentos de dados e perderá um núcleo de IA.</color>\n\nÉ necessário completar metade dos objetivos.";
        _text[66, 8] = "";
        _text[66, 9] = "";

        _text[67, 0] = "Save the mission and return to command center?";
        _text[67, 1] = "Сохранить миссию и вернуться в командный центр?";
        _text[67, 2] = "Sauver la mission et retourner au centre de commandement ?";
        _text[67, 3] = "Salvare la missione e tornare al centro di comando?";
        _text[67, 4] = "Mission speichern und zur Kommandzentrale zurückkehren?";
        _text[67, 5] = "¿Guardar la misión y volver al centro de mando?";
        _text[67, 6] = "Zapisać misję i wrócić do Centrum Dowodzenia?";
        _text[67, 7] = "Guardar a missão e voltar ao centro de comando?";
        _text[67, 8] = "";
        _text[67, 9] = "";

        _text[68, 0] = $"Restart mission?\n\n<color={Colors.HexWarningYellow}>You will lose one AI core.</color>";
        _text[68, 1] = $"Перезапустить миссию?\n\n<color={Colors.HexWarningYellow}>Вы потеряете одно ядро ИИ.</color>";
        _text[68, 2] = $"Redémarrer la mission ?\n\n<color={Colors.HexWarningYellow}>Vous perdrez un noyau d'IA.</color>";
        _text[68, 3] = $"Riavviare la missione?\n\n<color={Colors.HexWarningYellow}>Perderai un nucleo IA.</color>";
        _text[68, 4] = $"Mission neu starten?\n\n<color={Colors.HexWarningYellow}>Du verlierst einen KI-Kern.</color>";
        _text[68, 5] = $"¿Reiniciar la misión?\n\n<color={Colors.HexWarningYellow}>Perderás un núcleo de IA.</color>";
        _text[68, 6] = $"Zrestartować misję?\n\n<color={Colors.HexWarningYellow}>Stracisz jeden rdzeń SI.</color>";
        _text[68, 7] = $"Reiniciar a missão?\n\n<color={Colors.HexWarningYellow}>Você perderá um núcleo de IA.</color>";
        _text[68, 8] = "";
        _text[68, 9] = "";

        // название тактической карты
        _text[69, 0] = "Repair";
        _text[69, 1] = "Ремонт";
        _text[69, 2] = "Réparation";
        _text[69, 3] = "Riparazione";
        _text[69, 4] = "Reparatur";
        _text[69, 5] = "Reparación";
        _text[69, 6] = "Naprawa";
        _text[69, 7] = "Reparação";
        _text[69, 8] = "";
        _text[69, 9] = "";

        _text[70, 0] = "Over Production";
        _text[70, 1] = "Сверхдобыча";
        _text[70, 2] = "Super production";
        _text[70, 3] = "Sovraestrazione";
        _text[70, 4] = "Überabbau";
        _text[70, 5] = "Superextracción";
        _text[70, 6] = "Nadwydobycie";
        _text[70, 7] = "Superextração";
        _text[70, 8] = "";
        _text[70, 9] = "";

        _text[71, 0] = "Change Rarity";
        _text[71, 1] = "Смена Редкости";
        _text[71, 2] = "Changement de rareté";
        _text[71, 3] = "Cambio rarità";
        _text[71, 4] = "Seltenheit ändern";
        _text[71, 5] = "Cambiar rareza";
        _text[71, 6] = "Zmiana rzadkości";
        _text[71, 7] = "Alterar raridade";
        _text[71, 8] = "";
        _text[71, 9] = "";

        _text[72, 0] = "Plain";
        _text[72, 1] = "Равнина";
        _text[72, 2] = "Plaine";
        _text[72, 3] = "Pianura";
        _text[72, 4] = "Ebene";
        _text[72, 5] = "Llanura";
        _text[72, 6] = "Równina";
        _text[72, 7] = "Planície";
        _text[72, 8] = "";
        _text[72, 9] = "";

        _text[73, 0] = "Meadow";
        _text[73, 1] = "Луг";
        _text[73, 2] = "Prairie";
        _text[73, 3] = "Prato";
        _text[73, 4] = "Wiese";
        _text[73, 5] = "Pradera";
        _text[73, 6] = "Łąka";
        _text[73, 7] = "Prado";
        _text[73, 8] = "";
        _text[73, 9] = "";

        _text[74, 0] = "Road";
        _text[74, 1] = "Дорога";
        _text[74, 2] = "Route";
        _text[74, 3] = "Strada";
        _text[74, 4] = "Straße";
        _text[74, 5] = "Camino";
        _text[74, 6] = "Droga";
        _text[74, 7] = "Estrada";
        _text[74, 8] = "";
        _text[74, 9] = "";

        _text[75, 0] = "in";
        _text[75, 1] = "в";
        _text[75, 2] = "v";
        _text[75, 3] = "in";
        _text[75, 4] = "in";
        _text[75, 5] = "en";
        _text[75, 6] = "w";
        _text[75, 7] = "em";
        _text[75, 8] = "";
        _text[75, 9] = "";

        _text[76, 0] = "You can't restart the mission. You don't have any spare AI cores.";
        _text[76, 1] = "Вы не можете перезапустить миссию. У вас нет запасных ядер ИИ.";
        _text[76, 2] = "Vous ne pouvez pas relancer la mission. Vous n'avez plus de cœurs d'IA de rechange.";
        _text[76, 3] = "Non puoi riavviare la missione. Non hai nuclei IA di riserva.";
        _text[76, 4] = "Die Mission kann nicht neu gestartet werden. Es sind keine KI-Kerne mehr verfügbar.";
        _text[76, 5] = "No puedes reiniciar la misión. No te quedan núcleos de IA de repuesto.";
        _text[76, 6] = "Nie możesz zrestartować misji. Nie masz zapasowych rdzeni SI.";
        _text[76, 7] = "Você não pode reiniciar a missão. Você não tem núcleos de IA de reserva.";
        _text[76, 8] = "";
        _text[76, 9] = "";

        _text[77, 0] = "Launch";
        _text[77, 1] = "Запуск";
        _text[77, 2] = "Lancement";
        _text[77, 3] = "Avvio";
        _text[77, 4] = "Start";
        _text[77, 5] = "Iniciar";
        _text[77, 6] = "Start";
        _text[77, 7] = "Iniciar";
        _text[77, 8] = "";
        _text[77, 9] = "";

        _text[78, 0] = "Back";
        _text[78, 1] = "Назад";
        _text[78, 2] = "Dos";
        _text[78, 3] = "Indietro";
        _text[78, 4] = "Zurück";
        _text[78, 5] = "Atrás";
        _text[78, 6] = "Wstecz";
        _text[78, 7] = "Voltar";
        _text[78, 8] = "";
        _text[78, 9] = "";

        _text[79, 0] = "cost of repairing buildings";
        _text[79, 1] = "стоимость ремонта всех зданий";
        _text[79, 2] = "le coût de la réparation de tous les bâtiments";
        _text[79, 3] = "costo di riparazione di tutti gli edifici";
        _text[79, 4] = "Kosten der Renovierung aller Gebäude";
        _text[79, 5] = "coste de reparar todos los edificios";
        _text[79, 6] = "koszt naprawy wszystkich budynków";
        _text[79, 7] = "custo de reparação de todos os edifícios";
        _text[79, 8] = "";
        _text[79, 9] = "";

        _text[80, 0] = "to building durability";
        _text[80, 1] = "к прочности зданий";
        _text[80, 2] = "à la solidité des bâtiments";
        _text[80, 3] = "all'integrità degli edifici";
        _text[80, 4] = "zur Haltbarkeit der Gebäude";
        _text[80, 5] = "a la durabilidad de los edificios";
        _text[80, 6] = "do wytrzymałości budynków";
        _text[80, 7] = "à durabilidade dos edifícios";
        _text[80, 8] = "";
        _text[80, 9] = "";

        _text[81, 0] = "to turret damage";
        _text[81, 1] = "к урону турелей";
        _text[81, 2] = "aux dégâts de tourelle";
        _text[81, 3] = "ai danni delle torrette";
        _text[81, 4] = "zum Geschützschaden";
        _text[81, 5] = "al daño de las torretas";
        _text[81, 6] = "do obrażeń wieżyczek";
        _text[81, 7] = "ao dano das torres";
        _text[81, 8] = "";
        _text[81, 9] = "";

        _text[82, 0] = "Forest";
        _text[82, 1] = "Лес";
        _text[82, 2] = "Forêt";
        _text[82, 3] = "Foresta";
        _text[82, 4] = "Wald";
        _text[82, 5] = "Bosque";
        _text[82, 6] = "Las";
        _text[82, 7] = "Floresta";
        _text[82, 8] = "";
        _text[82, 9] = "";

        _text[83, 0] = "Shard";
        _text[83, 1] = "Осколок";
        _text[83, 2] = "Éclat";
        _text[83, 3] = "Scheggia";
        _text[83, 4] = "Scherbe";
        _text[83, 5] = "Esquirla";
        _text[83, 6] = "Odłamek";
        _text[83, 7] = "Fragmento";
        _text[83, 8] = "";
        _text[83, 9] = "";

        _text[84, 0] = "Robots";
        _text[84, 1] = "Роботы";
        _text[84, 2] = "Robots";
        _text[84, 3] = "Robot";
        _text[84, 4] = "Roboter";
        _text[84, 5] = "Robots";
        _text[84, 6] = "Roboty";
        _text[84, 7] = "Robôs";
        _text[84, 8] = "";
        _text[84, 9] = "";

        _text[85, 0] = "Starship Weapons";
        _text[85, 1] = "Орудия Корабля";
        _text[85, 2] = "Canons de navire";
        _text[85, 3] = "Armi della nave";
        _text[85, 4] = "Schiffsgeschütze";
        _text[85, 5] = "Armas de la nave";
        _text[85, 6] = "Uzbrojenie statku";
        _text[85, 7] = "Armas da Nave";
        _text[85, 8] = "";
        _text[85, 9] = "";

        _text[86, 0] = "You cannot restart the mission.\n\nYou have no spare AI cores.";
        _text[86, 1] = "Вы не можете начать миссию с начала.\n\nУ вас нет запасных ядер ИИ.";
        _text[86, 2] = "Impossible de relancer la mission.\n\nVous n'avez plus de cœurs d'IA de rechange.";
        _text[86, 3] = "Non puoi ricominciare la missione dall'inizio.\n\nNon hai nuclei IA di riserva.";
        _text[86, 4] = "Du kannst die Mission nicht von vorn beginnen.\n\nDu hast keine Ersatz-KI-Kerne.";
        _text[86, 5] = "No puedes empezar la misión desde el principio.\n\nNo te quedan núcleos de IA de repuesto.";
        _text[86, 6] = "Nie możesz rozpocząć misji od początku.\n\nNie masz zapasowych rdzeni SI.";
        _text[86, 7] = "Você não pode iniciar a missão do zero.\n\nVocê não tem núcleos de IA de reserva.";
        _text[86, 8] = "";
        _text[86, 9] = "";

        _text[87, 0] = "Not ready";
        _text[87, 1] = "Не готово";
        _text[87, 2] = "Pas prêt";
        _text[87, 3] = "Non pronto";
        _text[87, 4] = "Nicht bereit";
        _text[87, 5] = "No disponible";
        _text[87, 6] = "Niegotowe";
        _text[87, 7] = "Não pronto";
        _text[87, 8] = "";
        _text[87, 9] = "";

        _text[88, 0] = "Left";
        _text[88, 1] = "Левое";
        _text[88, 2] = "Gauche";
        _text[88, 3] = "Sinistro";
        _text[88, 4] = "Links";
        _text[88, 5] = "Izquierdo";
        _text[88, 6] = "Lewy";
        _text[88, 7] = "Esquerdo";
        _text[88, 8] = "";
        _text[88, 9] = "";

        _text[89, 0] = "Right";
        _text[89, 1] = "Правое";
        _text[89, 2] = "Droite";
        _text[89, 3] = "Destro";
        _text[89, 4] = "Rechts";
        _text[89, 5] = "Derecho";
        _text[89, 6] = "Prawy";
        _text[89, 7] = "Direito";
        _text[89, 8] = "";
        _text[89, 9] = "";

        _text[90, 0] = "ICOSA CORP";
        _text[90, 1] = "ИКОСА КОРП";
        _text[90, 2] = "ICOSA CORP";
        _text[90, 3] = "ICOSA CORP";
        _text[90, 4] = "IKOSA CORP";
        _text[90, 5] = "ICOSA CORP";
        _text[90, 6] = "ICOSA CORP";
        _text[90, 7] = "ICOSA CORP";
        _text[90, 8] = "";
        _text[90, 9] = "";

        _text[91, 0] = "BUILDING BETTER WORLD";
        _text[91, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";
        _text[91, 2] = "CONSTRUISONS UN MONDE MEILLEUR";
        _text[91, 3] = "COSTRUIAMO UN MONDO MIGLIORE";
        _text[91, 4] = "WIR BAUEN DIE BESTE WELT";
        _text[91, 5] = "CONSTRUYAMOS UN MUNDO MEJOR";
        _text[91, 6] = "ZBUDUJMY LEPSZY ŚWIAT";
        _text[91, 7] = "VAMOS CONSTRUIR O MELHOR MUNDO";
        _text[91, 8] = "";
        _text[91, 9] = "";

        _text[92, 0] = "COORDINATES";
        _text[92, 1] = "КООРДИНАТЫ";
        _text[92, 2] = "COORDONNÉES";
        _text[92, 3] = "COORDINATE";
        _text[92, 4] = "KOORDINATEN";
        _text[92, 5] = "COORDENADAS";
        _text[92, 6] = "WSPÓŁRZĘDNE";
        _text[92, 7] = "COORDENADAS";
        _text[92, 8] = "";
        _text[92, 9] = "";

        _text[93, 0] = "SIGNAL";
        _text[93, 1] = "СИГНАЛ";
        _text[93, 2] = "SIGNAL";
        _text[93, 3] = "SEGNALE";
        _text[93, 4] = "SIGNAL";
        _text[93, 5] = "SEÑAL";
        _text[93, 6] = "SYGNAŁ";
        _text[93, 7] = "SINAL";
        _text[93, 8] = "";
        _text[93, 9] = "";

        _text[94, 0] = "DIAGRAM";
        _text[94, 1] = "ДИАГРАММА";
        _text[94, 2] = "DIAGRAMME";
        _text[94, 3] = "DIAGRAMMA";
        _text[94, 4] = "DIAGRAMM";
        _text[94, 5] = "DIAGRAMA";
        _text[94, 6] = "DIAGRAM";
        _text[94, 7] = "DIAGRAMA";
        _text[94, 8] = "";
        _text[94, 9] = "";

        _text[95, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[95, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";
        _text[95, 2] = "-Rayonnement : Élevé\n-Pollution: Critique\n-Mise à jour : Active";
        _text[95, 3] = "-Radiazione: Alta\n-Inquinamento: Critico\n-Aggiornamento: Attivo";
        _text[95, 4] = "-Strahlung: Hoch\n-Verschmutzung: Kritisch\n-Update: Aktiv";
        _text[95, 5] = "-Radiación: Alta\n-Contaminación: Crítica\n-Actualización: Activa";
        _text[95, 6] = "-Promieniowanie: Wysokie\n-Zanieczyszczenie: Krytyczne\n-Aktualizacja: Aktywna";
        _text[95, 7] = "-Radiação: Alta\n-Poluição: Crítica\n-Atualização: Ativa";
        _text[95, 8] = "";
        _text[95, 9] = "";

        _text[96, 0] = "Learn";
        _text[96, 1] = "Изучить";
        _text[96, 2] = "Étude";
        _text[96, 3] = "Ricerca";
        _text[96, 4] = "Erforschen";
        _text[96, 5] = "Investigar";
        _text[96, 6] = "Zbadaj";
        _text[96, 7] = "Pesquisar";
        _text[96, 8] = "";
        _text[96, 9] = "";

        _text[97, 0] = "Building durability";
        _text[97, 1] = "Прочность здания";
        _text[97, 2] = "Renforcement des muscles";
        _text[97, 3] = "Integrità dell'edificio";
        _text[97, 4] = "Gebäudehaltbarkeit";
        _text[97, 5] = "Durabilidad del edificio";
        _text[97, 6] = "Wytrzymałość budynku";
        _text[97, 7] = "Durabilidade do edifício";
        _text[97, 8] = "";
        _text[97, 9] = "";

        _text[98, 0] = "Damage";
        _text[98, 1] = "Урон";
        _text[98, 2] = "Dommage";
        _text[98, 3] = "Danni";
        _text[98, 4] = "Schaden";
        _text[98, 5] = "Daño";
        _text[98, 6] = "Obrażenia";
        _text[98, 7] = "Dano";
        _text[98, 8] = "";
        _text[98, 9] = "";

        _text[99, 0] = "Attack speed";
        _text[99, 1] = "Скорость атаки";
        _text[99, 2] = "Vitesse d'attaque";
        _text[99, 3] = "Velocità d'attacco";
        _text[99, 4] = "Angriffsgeschwindigkeit";
        _text[99, 5] = "Velocidad de ataque";
        _text[99, 6] = "Szybkość ataku";
        _text[99, 7] = "Velocidade de ataque";
        _text[99, 8] = "";
        _text[99, 9] = "";

        _text[100, 0] = "Attack radius";
        _text[100, 1] = "Радиус атаки";
        _text[100, 2] = "rayon d'attaque";
        _text[100, 3] = "Raggio d'attacco";
        _text[100, 4] = "Angriffsradius";
        _text[100, 5] = "Alcance de ataque";
        _text[100, 6] = "Zasięg ataku";
        _text[100, 7] = "Alcance de ataque";
        _text[100, 8] = "";
        _text[100, 9] = "";

        _text[101, 0] = "Rotation speed";
        _text[101, 1] = "Скорость вращения";
        _text[101, 2] = "vitesse de rotation";
        _text[101, 3] = "Velocità di rotazione";
        _text[101, 4] = "Drehgeschwindigkeit";
        _text[101, 5] = "Velocidad de giro";
        _text[101, 6] = "Szybkość obrotu";
        _text[101, 7] = "Velocidade de rotação";
        _text[101, 8] = "";
        _text[101, 9] = "";

        _text[102, 0] = "Press any key\n\nEscape - cancel";
        _text[102, 1] = "Нажмите любую кнопку\n\nEscape - отмена";
        _text[102, 2] = "Appuyez sur n'importe quelle touche.\n\nÉchap - Annuler";
        _text[102, 3] = "Premi un tasto qualsiasi\n\nEscape - annulla";
        _text[102, 4] = "Drücke eine beliebige Taste\n\nEscape - Abbrechen";
        _text[102, 5] = "Pulsa cualquier tecla\n\nEscape - cancelar";
        _text[102, 6] = "Naciśnij dowolny przycisk\n\nEscape - anuluj";
        _text[102, 7] = "Pressione qualquer botão\n\nEscape - cancelar";
        _text[102, 8] = "";
        _text[102, 9] = "";

        _text[103, 0] = "Borderless";
        _text[103, 1] = "Безрамочный";
        _text[103, 2] = "Sans cadre";
        _text[103, 3] = "Senza bordi";
        _text[103, 4] = "Rahmenlos";
        _text[103, 5] = "Sin bordes";
        _text[103, 6] = "Bezramkowy";
        _text[103, 7] = "Sem bordas";
        _text[103, 8] = "";
        _text[103, 9] = "";

        _text[104, 0] = "Camera speed";
        _text[104, 1] = "Скорость камеры";
        _text[104, 2] = "Vitesse de la caméra";
        _text[104, 3] = "Velocità della camera";
        _text[104, 4] = "Kamerageschwindigkeit";
        _text[104, 5] = "Velocidad de la cámara";
        _text[104, 6] = "Szybkość kamery";
        _text[104, 7] = "Velocidade da câmara";
        _text[104, 8] = "";
        _text[104, 9] = "";

        _text[105, 0] = "Master volume";
        _text[105, 1] = "Общая громкость";
        _text[105, 2] = "Volume global";
        _text[105, 3] = "Volume generale";
        _text[105, 4] = "Gesamtlautstärke";
        _text[105, 5] = "Volumen general";
        _text[105, 6] = "Głośność ogólna";
        _text[105, 7] = "Volume geral";
        _text[105, 8] = "";
        _text[105, 9] = "";

        _text[106, 0] = "SFX volume";
        _text[106, 1] = "Громкость эффектов";
        _text[106, 2] = "Volume des effets";
        _text[106, 3] = "Volume effetti";
        _text[106, 4] = "Effektlautstärke";
        _text[106, 5] = "Volumen de efectos";
        _text[106, 6] = "Głośność efektów";
        _text[106, 7] = "Volume dos efeitos";
        _text[106, 8] = "";
        _text[106, 9] = "";

        _text[107, 0] = "UI volume";
        _text[107, 1] = "Громкость интерфейса";
        _text[107, 2] = "Volume d'interface";
        _text[107, 3] = "Volume interfaccia";
        _text[107, 4] = "UI-Lautstärke";
        _text[107, 5] = "Volumen de la interfaz";
        _text[107, 6] = "Głośność interfejsu";
        _text[107, 7] = "Volume da interface";
        _text[107, 8] = "";
        _text[107, 9] = "";

        _text[108, 0] = "Music volume";
        _text[108, 1] = "Громкость музыки";
        _text[108, 2] = "Volume musical";
        _text[108, 3] = "Volume musica";
        _text[108, 4] = "Musiklautstärke";
        _text[108, 5] = "Volumen de la música";
        _text[108, 6] = "Głośność muzyki";
        _text[108, 7] = "Volume da música";
        _text[108, 8] = "";
        _text[108, 9] = "";

        _text[109, 0] = "Blood";
        _text[109, 1] = "Кровь";
        _text[109, 2] = "Sang";
        _text[109, 3] = "Sangue";
        _text[109, 4] = "Blut";
        _text[109, 5] = "Sangre";
        _text[109, 6] = "Krew";
        _text[109, 7] = "Sangue";
        _text[109, 8] = "";
        _text[109, 9] = "";

        _text[110, 0] = "Video";
        _text[110, 1] = "Видео";
        _text[110, 2] = "Vidéo";
        _text[110, 3] = "Video";
        _text[110, 4] = "Video";
        _text[110, 5] = "Vídeo";
        _text[110, 6] = "Wideo";
        _text[110, 7] = "Vídeo";
        _text[110, 8] = "";
        _text[110, 9] = "";

        _text[111, 0] = "Controls";
        _text[111, 1] = "Управление";
        _text[111, 2] = "Contrôle";
        _text[111, 3] = "Controlli";
        _text[111, 4] = "Steuerung";
        _text[111, 5] = "Controles";
        _text[111, 6] = "Sterowanie";
        _text[111, 7] = "Controlos";
        _text[111, 8] = "";
        _text[111, 9] = "";

        _text[112, 0] = "Gameplay";
        _text[112, 1] = "Игра";
        _text[112, 2] = "Jeu";
        _text[112, 3] = "Gioco";
        _text[112, 4] = "Spiel";
        _text[112, 5] = "Juego";
        _text[112, 6] = "Gra";
        _text[112, 7] = "Jogo";
        _text[112, 8] = "";
        _text[112, 9] = "";

        _text[113, 0] = "Audio";
        _text[113, 1] = "Аудио";
        _text[113, 2] = "Audio";
        _text[113, 3] = "Audio";
        _text[113, 4] = "Audio";
        _text[113, 5] = "Audio";
        _text[113, 6] = "Audio";
        _text[113, 7] = "Áudio";
        _text[113, 8] = "";
        _text[113, 9] = "";

        _text[114, 0] = "Screen Mode";
        _text[114, 1] = "Режим Экрана";
        _text[114, 2] = "Mode écran";
        _text[114, 3] = "Modalità schermo";
        _text[114, 4] = "Bildschirmmodus";
        _text[114, 5] = "Modo de pantalla";
        _text[114, 6] = "Tryb ekranu";
        _text[114, 7] = "Modo de ecrã";
        _text[114, 8] = "";
        _text[114, 9] = "";

        _text[115, 0] = "Resolution";
        _text[115, 1] = "Разрешение";
        _text[115, 2] = "Autorisation";
        _text[115, 3] = "Risoluzione";
        _text[115, 4] = "Auflösung";
        _text[115, 5] = "Resolución";
        _text[115, 6] = "Rozdzielczość";
        _text[115, 7] = "Resolução";
        _text[115, 8] = "";
        _text[115, 9] = "";

        _text[116, 0] = "Quality";
        _text[116, 1] = "Качество";
        _text[116, 2] = "Qualité";
        _text[116, 3] = "Qualità";
        _text[116, 4] = "Qualität";
        _text[116, 5] = "Calidad";
        _text[116, 6] = "Jakość";
        _text[116, 7] = "Qualidade";
        _text[116, 8] = "";
        _text[116, 9] = "";

        _text[117, 0] = "Anti-Aliasing";
        _text[117, 1] = "Сглаживание";
        _text[117, 2] = "Lissage";
        _text[117, 3] = "Antialiasing";
        _text[117, 4] = "Kantenglättung";
        _text[117, 5] = "Suavizado";
        _text[117, 6] = "Wygładzanie";
        _text[117, 7] = "Anti-aliasing";
        _text[117, 8] = "";
        _text[117, 9] = "";

        _text[118, 0] = "Upscaling Filter";
        _text[118, 1] = "Масштабирование";
        _text[118, 2] = "Mise à l'échelle";
        _text[118, 3] = "Ridimensionamento";
        _text[118, 4] = "Skalierung";
        _text[118, 5] = "Escalado";
        _text[118, 6] = "Skalowanie";
        _text[118, 7] = "Escalonamento";
        _text[118, 8] = "";
        _text[118, 9] = "";

        _text[119, 0] = "Glow";
        _text[119, 1] = "Свечение";
        _text[119, 2] = "Briller";
        _text[119, 3] = "Bloom";
        _text[119, 4] = "Bloom";
        _text[119, 5] = "Resplandor";
        _text[119, 6] = "Poświata";
        _text[119, 7] = "Brilho";
        _text[119, 8] = "";
        _text[119, 9] = "";

        _text[120, 0] = "Max. Frame Rate";
        _text[120, 1] = "Макс. Кол-во Кадров";
        _text[120, 2] = "Nombre maximal d'images";
        _text[120, 3] = "Max. FPS";
        _text[120, 4] = "Max. FPS";
        _text[120, 5] = "Máx. FPS";
        _text[120, 6] = "Maks. liczba klatek";
        _text[120, 7] = "Máx. de fotogramas";
        _text[120, 8] = "";
        _text[120, 9] = "";

        _text[121, 0] = "Close";
        _text[121, 1] = "Закрыть";
        _text[121, 2] = "Fermer";
        _text[121, 3] = "Chiudi";
        _text[121, 4] = "Schließen";
        _text[121, 5] = "Cerrar";
        _text[121, 6] = "Zamknij";
        _text[121, 7] = "Fechar";
        _text[121, 8] = "";
        _text[121, 9] = "";

        _text[122, 0] = "Apply";
        _text[122, 1] = "Применить";
        _text[122, 2] = "Appliquer";
        _text[122, 3] = "Applica";
        _text[122, 4] = "Anwenden";
        _text[122, 5] = "Aplicar";
        _text[122, 6] = "Zastosuj";
        _text[122, 7] = "Aplicar";
        _text[122, 8] = "";
        _text[122, 9] = "";

        _text[123, 0] = "Reset";
        _text[123, 1] = "Сброс";
        _text[123, 2] = "Réinitialiser";
        _text[123, 3] = "Ripristina";
        _text[123, 4] = "Zurücksetzen";
        _text[123, 5] = "Restablecer";
        _text[123, 6] = "Reset";
        _text[123, 7] = "Repor";
        _text[123, 8] = "";
        _text[123, 9] = "";

        _text[124, 0] = "Full-screen";
        _text[124, 1] = "Полноэкранный";
        _text[124, 2] = "Plein écran";
        _text[124, 3] = "Schermo intero";
        _text[124, 4] = "Vollbild";
        _text[124, 5] = "Pantalla completa";
        _text[124, 6] = "Pełny ekran";
        _text[124, 7] = "Ecrã inteiro";
        _text[124, 8] = "";
        _text[124, 9] = "";

        _text[125, 0] = "Windowed";
        _text[125, 1] = "Оконный";
        _text[125, 2] = "Fenêtre";
        _text[125, 3] = "Finestra";
        _text[125, 4] = "Fenster";
        _text[125, 5] = "Ventana";
        _text[125, 6] = "Okienkowy";
        _text[125, 7] = "Em janela";
        _text[125, 8] = "";
        _text[125, 9] = "";

        _text[126, 0] = "Low";
        _text[126, 1] = "Низкое";
        _text[126, 2] = "Faible";
        _text[126, 3] = "Basso";
        _text[126, 4] = "Niedrig";
        _text[126, 5] = "Bajo";
        _text[126, 6] = "Niskie";
        _text[126, 7] = "Baixo";
        _text[126, 8] = "";
        _text[126, 9] = "";

        _text[127, 0] = "Medium";
        _text[127, 1] = "Среднее";
        _text[127, 2] = "Moyenne";
        _text[127, 3] = "Medio";
        _text[127, 4] = "Mittel";
        _text[127, 5] = "Medio";
        _text[127, 6] = "Średnie";
        _text[127, 7] = "Médio";
        _text[127, 8] = "";
        _text[127, 9] = "";

        _text[128, 0] = "High";
        _text[128, 1] = "Высокое";
        _text[128, 2] = "Haut";
        _text[128, 3] = "Alto";
        _text[128, 4] = "Hoch";
        _text[128, 5] = "Alto";
        _text[128, 6] = "Wysokie";
        _text[128, 7] = "Alto";
        _text[128, 8] = "";
        _text[128, 9] = "";

        _text[129, 0] = "Ultra";
        _text[129, 1] = "Ультра";
        _text[129, 2] = "Ultra";
        _text[129, 3] = "Ultra";
        _text[129, 4] = "Ultra";
        _text[129, 5] = "Ultra";
        _text[129, 6] = "Ultra";
        _text[129, 7] = "Ultra";
        _text[129, 8] = "";
        _text[129, 9] = "";

        _text[130, 0] = "Disabled";
        _text[130, 1] = "Выключено";
        _text[130, 2] = "Désactivé";
        _text[130, 3] = "Disattivato";
        _text[130, 4] = "Aus";
        _text[130, 5] = "Desactivado";
        _text[130, 6] = "Wyłączone";
        _text[130, 7] = "Desligado";
        _text[130, 8] = "";
        _text[130, 9] = "";

        _text[131, 0] = "Bilinear";
        _text[131, 1] = "Билинейное";
        _text[131, 2] = "Bilinéaire";
        _text[131, 3] = "Bilineare";
        _text[131, 4] = "Bilinear";
        _text[131, 5] = "Bilineal";
        _text[131, 6] = "Bilinearne";
        _text[131, 7] = "Bilinear";
        _text[131, 8] = "";
        _text[131, 9] = "";

        _text[132, 0] = "Nearest";
        _text[132, 1] = "Ближайшее";
        _text[132, 2] = "Le plus proche";
        _text[132, 3] = "Più vicino";
        _text[132, 4] = "Nächster Nachbar";
        _text[132, 5] = "Más cercano";
        _text[132, 6] = "Najbliższe";
        _text[132, 7] = "Mais próximo";
        _text[132, 8] = "";
        _text[132, 9] = "";

        _text[133, 0] = "Camera movement";
        _text[133, 1] = "Движение камеры";
        _text[133, 2] = "Mouvement de la caméra";
        _text[133, 3] = "Movimento camera";
        _text[133, 4] = "Kamerabewegung";
        _text[133, 5] = "Mover cámara";
        _text[133, 6] = "Ruch kamery";
        _text[133, 7] = "Movimento da câmara";
        _text[133, 8] = "";
        _text[133, 9] = "";

        _text[134, 0] = "Camera zoom";
        _text[134, 1] = "Масштаб камеры";
        _text[134, 2] = "Échelle de l'appareil photo";
        _text[134, 3] = "Zoom camera";
        _text[134, 4] = "Kameraskala";
        _text[134, 5] = "Zoom de cámara";
        _text[134, 6] = "Zoom kamery";
        _text[134, 7] = "Zoom da câmara";
        _text[134, 8] = "";
        _text[134, 9] = "";

        _text[135, 0] = "Select tile / card";
        _text[135, 1] = "Выбор тайла / карты";
        _text[135, 2] = "Sélection d'une tuile/carte";
        _text[135, 3] = "Seleziona tessera / carta";
        _text[135, 4] = "Kachel/Karte auswählen";
        _text[135, 5] = "Seleccionar casilla / carta";
        _text[135, 6] = "Wybór kafelka / karty";
        _text[135, 7] = "Selecionar tile / mapa";
        _text[135, 8] = "";
        _text[135, 9] = "";

        _text[136, 0] = "Unselect tile / card";
        _text[136, 1] = "Отмена выбора тайла / карты";
        _text[136, 2] = "Désélectionner une tuile/carte";
        _text[136, 3] = "Annulla selezione tessera / carta";
        _text[136, 4] = "Kachel/Karte abwählen";
        _text[136, 5] = "Cancelar selección de casilla / carta";
        _text[136, 6] = "Anuluj wybór kafelka / karty";
        _text[136, 7] = "Cancelar seleção de tile / mapa";
        _text[136, 8] = "";
        _text[136, 9] = "";

        _text[137, 0] = "Game speed: pause";
        _text[137, 1] = "Скорость игры: пауза";
        _text[137, 2] = "Vitesse du jeu: pause";
        _text[137, 3] = "Velocità di gioco: pausa";
        _text[137, 4] = "Spielgeschwindigkeit: pause";
        _text[137, 5] = "Velocidad del juego: pausa";
        _text[137, 6] = "Prędkość gry: pauza";
        _text[137, 7] = "Velocidade do jogo: pausa";
        _text[137, 8] = "";
        _text[137, 9] = "";

        _text[138, 0] = "Game speed: normal";
        _text[138, 1] = "Скорость игры: нормальная";
        _text[138, 2] = "Vitesse du jeu : normale";
        _text[138, 3] = "Velocità di gioco: normale";
        _text[138, 4] = "Spielgeschwindigkeit: normal";
        _text[138, 5] = "Velocidad del juego: normal";
        _text[138, 6] = "Prędkość gry: normalna";
        _text[138, 7] = "Velocidade do jogo: normal";
        _text[138, 8] = "";
        _text[138, 9] = "";

        _text[139, 0] = "Game speed: double";
        _text[139, 1] = "Скорость игры: двойная";
        _text[139, 2] = "Vitesse de jeu: double";
        _text[139, 3] = "Velocità di gioco: doppia";
        _text[139, 4] = "Spielgeschwindigkeit: doppelt";
        _text[139, 5] = "Velocidad del juego: x2";
        _text[139, 6] = "Prędkość gry: podwójna";
        _text[139, 7] = "Velocidade do jogo: 2x";
        _text[139, 8] = "";
        _text[139, 9] = "";

        _text[140, 0] = "Game speed: triple";
        _text[140, 1] = "Скорость игры: тройная";
        _text[140, 2] = "Vitesse de jeu : triple";
        _text[140, 3] = "Velocità di gioco: tripla";
        _text[140, 4] = "Spielgeschwindigkeit: dreifach";
        _text[140, 5] = "Velocidad del juego: x3";
        _text[140, 6] = "Prędkość gry: potrójna";
        _text[140, 7] = "Velocidade do jogo: 3x";
        _text[140, 8] = "";
        _text[140, 9] = "";

        _text[141, 0] = "Menu";
        _text[141, 1] = "Меню";
        _text[141, 2] = "Menu";
        _text[141, 3] = "Menu";
        _text[141, 4] = "Menü";
        _text[141, 5] = "Menú";
        _text[141, 6] = "Menu";
        _text[141, 7] = "Menu";
        _text[141, 8] = "";
        _text[141, 9] = "";

        _text[142, 0] = "Build on tile";
        _text[142, 1] = "Построить на тайле";
        _text[142, 2] = "Construire sur des tuiles";
        _text[142, 3] = "Costruisci sulla tessera";
        _text[142, 4] = "Auf Kachel bauen";
        _text[142, 5] = "Construir en la casilla";
        _text[142, 6] = "Zbuduj na kafelku";
        _text[142, 7] = "Construir no tile";
        _text[142, 8] = "";
        _text[142, 9] = "";

        _text[143, 0] = "Rotate tile / building";
        _text[143, 1] = "Повернуть тайл / здание";
        _text[143, 2] = "Rotation de la tuile / du bâtiment";
        _text[143, 3] = "Ruota tessera / edificio";
        _text[143, 4] = "Kachel/Gebäude drehen";
        _text[143, 5] = "Rotar casilla / edificio";
        _text[143, 6] = "Obróć kafelek / budynek";
        _text[143, 7] = "Rodar tile / edifício";
        _text[143, 8] = "";
        _text[143, 9] = "";

        _text[144, 0] = "Destroy tile / building";
        _text[144, 1] = "Уничтожить тайл / здание";
        _text[144, 2] = "Détruire la tuile/le bâtiment";
        _text[144, 3] = "Distruggi tessera / edificio";
        _text[144, 4] = "Kachel/Gebäude zerstören";
        _text[144, 5] = "Destruir casilla / edificio";
        _text[144, 6] = "Zniszcz kafelek / budynek";
        _text[144, 7] = "Destruir tile / edifício";
        _text[144, 8] = "";
        _text[144, 9] = "";

        _text[145, 0] = "Toggle building";
        _text[145, 1] = "Включить / выключить здание";
        _text[145, 2] = "Allumer / éteindre le bâtiment";
        _text[145, 3] = "Attiva / disattiva edificio";
        _text[145, 4] = "Gebäude ein-/ausschalten";
        _text[145, 5] = "Activar / desactivar edificio";
        _text[145, 6] = "Włącz / wyłącz budynek";
        _text[145, 7] = "Ligar / desligar edifício";
        _text[145, 8] = "";
        _text[145, 9] = "";

        _text[146, 0] = "Open machine panel";
        _text[146, 1] = "Открыть панель машин";
        _text[146, 2] = "Ouvrez le panneau de la voiture";
        _text[146, 3] = "Apri pannello macchine";
        _text[146, 4] = "Maschinenpanel öffnen";
        _text[146, 5] = "Abrir panel de máquinas";
        _text[146, 6] = "Otwórz panel maszyn";
        _text[146, 7] = "Abrir painel de máquinas";
        _text[146, 8] = "";
        _text[146, 9] = "";

        _text[147, 0] = "Data restored:";
        _text[147, 1] = "Восстановлено данных";
        _text[147, 2] = "Données récupérées";
        _text[147, 3] = "Dati ripristinati";
        _text[147, 4] = "Daten wiederhergestellt";
        _text[147, 5] = "Datos restaurados";
        _text[147, 6] = "Odzyskane dane";
        _text[147, 7] = "Dados restaurados";
        _text[147, 8] = "";
        _text[147, 9] = "";

        _text[148, 0] = "Ecology bonus:";
        _text[148, 1] = "Экологический бонус";
        _text[148, 2] = "Bonus écologique";
        _text[148, 3] = "Bonus ecologico";
        _text[148, 4] = "Ökologiebonus";
        _text[148, 5] = "Bonificación ecológica";
        _text[148, 6] = "Bonus ekologiczny";
        _text[148, 7] = "Bónus ecológico";
        _text[148, 8] = "";
        _text[148, 9] = "";

        _text[149, 0] = "Data fragments received";
        _text[149, 1] = "Получено фрагментов данных";
        _text[149, 2] = "Fragments de données reçus";
        _text[149, 3] = "Frammenti dati ottenuti";
        _text[149, 4] = "Datenfragmente erhalten";
        _text[149, 5] = "Fragmentos de datos obtenidos";
        _text[149, 6] = "Otrzymane fragmenty danych";
        _text[149, 7] = "Fragmentos de dados obtidos";
        _text[149, 8] = "";
        _text[149, 9] = "";

        _text[150, 0] = "Defeat the boss: {0}/{1}";
        _text[150, 1] = "Победить босса: {0}/{1}";
        _text[150, 2] = "Vaincre le boss: {0}/{1}";
        _text[150, 3] = "Sconfiggi il boss: {0}/{1}";
        _text[150, 4] = "Boss besiegen: {0}/{1}";
        _text[150, 5] = "Derrotar al jefe: {0}/{1}";
        _text[150, 6] = "Pokonaj bossa: {0}/{1}";
        _text[150, 7] = "Derrotar o chefe: {0}/{1}";
        _text[150, 8] = "";
        _text[150, 9] = "";

        _text[151, 0] = "Defeat the boss";
        _text[151, 1] = "Победить босса";
        _text[151, 2] = "Vaincre le boss";
        _text[151, 3] = "Sconfiggi il boss";
        _text[151, 4] = "Boss besiegen";
        _text[151, 5] = "Derrotar al jefe";
        _text[151, 6] = "Pokonaj bossa";
        _text[151, 7] = "Derrotar o chefe";
        _text[151, 8] = "";
        _text[151, 9] = "";

        _text[152, 0] = "Resources for construction:";
        _text[152, 1] = "Ресурсы для строительства:";
        _text[152, 2] = "Ressources pour la construction :";
        _text[152, 3] = "Risorse per la costruzione:";
        _text[152, 4] = "Ressourcen zum Bauen:";
        _text[152, 5] = "Recursos para construir:";
        _text[152, 6] = "Zasoby do budowy:";
        _text[152, 7] = "Recursos para construção:";
        _text[152, 8] = "";
        _text[152, 9] = "";

        _text[153, 0] = "Wood";
        _text[153, 1] = "Древесина";
        _text[153, 2] = "Bois";
        _text[153, 3] = "Legname";
        _text[153, 4] = "Holz";
        _text[153, 5] = "Madera";
        _text[153, 6] = "Drewno";
        _text[153, 7] = "Madeira";
        _text[153, 8] = "";
        _text[153, 9] = "";

        _text[154, 0] = "Stone";
        _text[154, 1] = "Камень";
        _text[154, 2] = "Pierre";
        _text[154, 3] = "Pietra";
        _text[154, 4] = "Stein";
        _text[154, 5] = "Piedra";
        _text[154, 6] = "Kamień";
        _text[154, 7] = "Pedra";
        _text[154, 8] = "";
        _text[154, 9] = "";

        _text[155, 0] = "Iron Ore";
        _text[155, 1] = "Железная Руда";
        _text[155, 2] = "Minerai de fer";
        _text[155, 3] = "Minerale di ferro";
        _text[155, 4] = "Eisenerz";
        _text[155, 5] = "Mineral de hierro";
        _text[155, 6] = "Ruda żelaza";
        _text[155, 7] = "Minério de ferro";
        _text[155, 8] = "";
        _text[155, 9] = "";

        _text[156, 0] = "Copper Ore";
        _text[156, 1] = "Медная Руда";
        _text[156, 2] = "Minerai de cuivre";
        _text[156, 3] = "Minerale di rame";
        _text[156, 4] = "Kupfererz";
        _text[156, 5] = "Mineral de cobre";
        _text[156, 6] = "Ruda miedzi";
        _text[156, 7] = "Minério de cobre";
        _text[156, 8] = "";
        _text[156, 9] = "";

        _text[157, 0] = "Coal";
        _text[157, 1] = "Уголь";
        _text[157, 2] = "Charbon";
        _text[157, 3] = "Carbone";
        _text[157, 4] = "Kohle";
        _text[157, 5] = "Carbón";
        _text[157, 6] = "Węgiel";
        _text[157, 7] = "Carvão";
        _text[157, 8] = "";
        _text[157, 9] = "";

        _text[158, 0] = "Oil";
        _text[158, 1] = "Нефть";
        _text[158, 2] = "Huile";
        _text[158, 3] = "Petrolio";
        _text[158, 4] = "Öl";
        _text[158, 5] = "Petróleo";
        _text[158, 6] = "Ropa";
        _text[158, 7] = "Petróleo";
        _text[158, 8] = "";
        _text[158, 9] = "";

        _text[159, 0] = "Water";
        _text[159, 1] = "Вода";
        _text[159, 2] = "Eau";
        _text[159, 3] = "Acqua";
        _text[159, 4] = "Wasser";
        _text[159, 5] = "Agua";
        _text[159, 6] = "Woda";
        _text[159, 7] = "Água";
        _text[159, 8] = "";
        _text[159, 9] = "";

        _text[160, 0] = "Sand";
        _text[160, 1] = "Песок";
        _text[160, 2] = "Sable";
        _text[160, 3] = "Sabbia";
        _text[160, 4] = "Sand";
        _text[160, 5] = "Arena";
        _text[160, 6] = "Piasek";
        _text[160, 7] = "Areia";
        _text[160, 8] = "";
        _text[160, 9] = "";

        _text[161, 0] = "Electricity";
        _text[161, 1] = "Электричество";
        _text[161, 2] = "Électricité";
        _text[161, 3] = "Elettricità";
        _text[161, 4] = "Strom";
        _text[161, 5] = "Electricidad";
        _text[161, 6] = "Elektryczność";
        _text[161, 7] = "Eletricidade";
        _text[161, 8] = "";
        _text[161, 9] = "";

        _text[162, 0] = "Stone Block";
        _text[162, 1] = "Каменный Блок";
        _text[162, 2] = "Bloc de pierre";
        _text[162, 3] = "Blocco di pietra";
        _text[162, 4] = "Steinblock";
        _text[162, 5] = "Bloque de piedra";
        _text[162, 6] = "Kamienny blok";
        _text[162, 7] = "Bloco de pedra";
        _text[162, 8] = "";
        _text[162, 9] = "";

        _text[163, 0] = "Iron Ingot";
        _text[163, 1] = "Слиток Железа";
        _text[163, 2] = "Lingot de fer";
        _text[163, 3] = "Lingotto di ferro";
        _text[163, 4] = "Eisenbarren";
        _text[163, 5] = "Lingote de hierro";
        _text[163, 6] = "Sztabka żelaza";
        _text[163, 7] = "Lingote de ferro";
        _text[163, 8] = "";
        _text[163, 9] = "";

        _text[164, 0] = "Steel Ingot";
        _text[164, 1] = "Слиток Стали";
        _text[164, 2] = "Lingot d'acier";
        _text[164, 3] = "Lingotto d'acciaio";
        _text[164, 4] = "Stahlbarren";
        _text[164, 5] = "Lingote de acero";
        _text[164, 6] = "Sztabka stali";
        _text[164, 7] = "Lingote de aço";
        _text[164, 8] = "";
        _text[164, 9] = "";

        _text[165, 0] = "Copper Plate";
        _text[165, 1] = "Медная Пластина";
        _text[165, 2] = "Plaque de cuivre";
        _text[165, 3] = "Lastra di rame";
        _text[165, 4] = "Kupferplatte";
        _text[165, 5] = "Placa de cobre";
        _text[165, 6] = "Miedziana płyta";
        _text[165, 7] = "Placa de cobre";
        _text[165, 8] = "";
        _text[165, 9] = "";

        _text[166, 0] = "Concrete";
        _text[166, 1] = "Бетон";
        _text[166, 2] = "Béton";
        _text[166, 3] = "Calcestruzzo";
        _text[166, 4] = "Beton";
        _text[166, 5] = "Hormigón";
        _text[166, 6] = "Beton";
        _text[166, 7] = "Betão";
        _text[166, 8] = "";
        _text[166, 9] = "";

        _text[167, 0] = "Steam";
        _text[167, 1] = "Пар";
        _text[167, 2] = "Vapeur";
        _text[167, 3] = "Vapore";
        _text[167, 4] = "Dampf";
        _text[167, 5] = "Vapor";
        _text[167, 6] = "Para";
        _text[167, 7] = "Vapor";
        _text[167, 8] = "";
        _text[167, 9] = "";

        _text[168, 0] = "Glass";
        _text[168, 1] = "Стекло";
        _text[168, 2] = "Verre";
        _text[168, 3] = "Vetro";
        _text[168, 4] = "Glas";
        _text[168, 5] = "Vidrio";
        _text[168, 6] = "Szkło";
        _text[168, 7] = "Vidro";
        _text[168, 8] = "";
        _text[168, 9] = "";

        _text[169, 0] = "Copper Wire";
        _text[169, 1] = "Медный Провод";
        _text[169, 2] = "Fil de cuivre";
        _text[169, 3] = "Filo di rame";
        _text[169, 4] = "Kupferdraht";
        _text[169, 5] = "Cable de cobre";
        _text[169, 6] = "Miedziany przewód";
        _text[169, 7] = "Fio de cobre";
        _text[169, 8] = "";
        _text[169, 9] = "";

        _text[170, 0] = "Gear Wheel";
        _text[170, 1] = "Шестерня";
        _text[170, 2] = "Engrenage";
        _text[170, 3] = "Ingranaggio";
        _text[170, 4] = "Zahnrad";
        _text[170, 5] = "Engranaje";
        _text[170, 6] = "Zębatka";
        _text[170, 7] = "Engrenagem";
        _text[170, 8] = "";
        _text[170, 9] = "";

        _text[171, 0] = "Electronic Circuit";
        _text[171, 1] = "Электросхема";
        _text[171, 2] = "Schéma de circuit électrique";
        _text[171, 3] = "Circuito";
        _text[171, 4] = "Schaltkreis";
        _text[171, 5] = "Circuito";
        _text[171, 6] = "Układ elektroniczny";
        _text[171, 7] = "Circuito elétrico";
        _text[171, 8] = "";
        _text[171, 9] = "";

        _text[172, 0] = "Processor";
        _text[172, 1] = "Процессор";
        _text[172, 2] = "Processeur";
        _text[172, 3] = "Processore";
        _text[172, 4] = "Prozessor";
        _text[172, 5] = "Procesador";
        _text[172, 6] = "Procesor";
        _text[172, 7] = "Processador";
        _text[172, 8] = "";
        _text[172, 9] = "";

        _text[173, 0] = "Engine";
        _text[173, 1] = "Двигатель";
        _text[173, 2] = "Moteur";
        _text[173, 3] = "Motore";
        _text[173, 4] = "Motor";
        _text[173, 5] = "Motor";
        _text[173, 6] = "Silnik";
        _text[173, 7] = "Motor";
        _text[173, 8] = "";
        _text[173, 9] = "";

        _text[174, 0] = "Electric Engine";
        _text[174, 1] = "Электродвигатель";
        _text[174, 2] = "Moteur électrique";
        _text[174, 3] = "Motore elettrico";
        _text[174, 4] = "Elektromotor";
        _text[174, 5] = "Motor eléctrico";
        _text[174, 6] = "Silnik elektryczny";
        _text[174, 7] = "Motor elétrico";
        _text[174, 8] = "";
        _text[174, 9] = "";

        _text[175, 0] = "Data Fragment";
        _text[175, 1] = "Фрагмент Данных";
        _text[175, 2] = "Fragment de données";
        _text[175, 3] = "Frammento dati";
        _text[175, 4] = "Datenfragment";
        _text[175, 5] = "Fragmento de datos";
        _text[175, 6] = "Fragment danych";
        _text[175, 7] = "Fragmento de dados";
        _text[175, 8] = "";
        _text[175, 9] = "";

        _text[176, 0] = "Beam Energy";
        _text[176, 1] = "Энергия Луча";
        _text[176, 2] = "Énergie du rayon";
        _text[176, 3] = "Energia del raggio";
        _text[176, 4] = "Strahlenergie";
        _text[176, 5] = "Energía del rayo";
        _text[176, 6] = "Energia wiązki";
        _text[176, 7] = "Energia do feixe";
        _text[176, 8] = "";
        _text[176, 9] = "";

        _text[177, 0] = "Mark / Remove from general repair";
        _text[177, 1] = "Пометить / Снять с общего ремонта";
        _text[177, 2] = "Marquer / Retirer de la réparation générale";
        _text[177, 3] = "Segna / rimuovi dalla riparazione generale";
        _text[177, 4] = "Für Sammelreparatur markieren/entfernen";
        _text[177, 5] = "Marcar / quitar de reparación global";
        _text[177, 6] = "Oznacz / usuń z naprawy ogólnej";
        _text[177, 7] = "Marcar / remover da reparação geral";
        _text[177, 8] = "";
        _text[177, 9] = "";

        _text[178, 0] = "General repair";
        _text[178, 1] = "Общий ремонт";
        _text[178, 2] = "Réparations générales";
        _text[178, 3] = "Riparazione generale";
        _text[178, 4] = "Sammelreparatur";
        _text[178, 5] = "Reparación global";
        _text[178, 6] = "Naprawa ogólna";
        _text[178, 7] = "Reparação geral";
        _text[178, 8] = "";
        _text[178, 9] = "";

        _text[179, 0] = "Skills";
        _text[179, 1] = "Умения";
        _text[179, 2] = "Compétences";
        _text[179, 3] = "Abilità";
        _text[179, 4] = "Fähigkeiten";
        _text[179, 5] = "Habilidades";
        _text[179, 6] = "Umiejętności";
        _text[179, 7] = "Habilidades";
        _text[179, 8] = "";
        _text[179, 9] = "";

        _text[180, 0] = "Description";
        _text[180, 1] = "Описание";
        _text[180, 2] = "Description";
        _text[180, 3] = "Descrizione";
        _text[180, 4] = "Beschreibung";
        _text[180, 5] = "Descripción";
        _text[180, 6] = "Opis";
        _text[180, 7] = "Descrição";
        _text[180, 8] = "";
        _text[180, 9] = "";

        _text[181, 0] = "Requires resources to repair them.";
        _text[181, 1] = "Требуются ресурсы для их починки";
        _text[181, 2] = "Des ressources sont nécessaires pour les réparer";
        _text[181, 3] = "Servono risorse per ripararli";
        _text[181, 4] = "Für die Reparatur werden Ressourcen benötigt";
        _text[181, 5] = "Se requieren recursos para repararlos";
        _text[181, 6] = "Do ich naprawy potrzebne są zasoby";
        _text[181, 7] = "São necessários recursos para os reparar";
        _text[181, 8] = "";
        _text[181, 9] = "";

        _text[182, 0] = "Required";
        _text[182, 1] = "Требуется";
        _text[182, 2] = "Requis";
        _text[182, 3] = "Richiede";
        _text[182, 4] = "Erforderlich";
        _text[182, 5] = "Se requiere";
        _text[182, 6] = "Wymagane";
        _text[182, 7] = "Necessário";
        _text[182, 8] = "";
        _text[182, 9] = "";

        _text[183, 0] = "You have received";
        _text[183, 1] = "Вы получили";
        _text[183, 2] = "Vous avez reçu";
        _text[183, 3] = "Hai ottenuto";
        _text[183, 4] = "Du hast erhalten";
        _text[183, 5] = "Has recibido";
        _text[183, 6] = "Otrzymano";
        _text[183, 7] = "Você recebeu";
        _text[183, 8] = "";
        _text[183, 9] = "";

        _text[184, 0] = "You have lost";
        _text[184, 1] = "Вы потеряли";
        _text[184, 2] = "Tu as perdu";
        _text[184, 3] = "Hai perso";
        _text[184, 4] = "Du hast verloren";
        _text[184, 5] = "Has perdido";
        _text[184, 6] = "Utracono";
        _text[184, 7] = "Você perdeu";
        _text[184, 8] = "";
        _text[184, 9] = "";

        _text[185, 0] = "Ai Core";
        _text[185, 1] = "Ядро ИИ";
        _text[185, 2] = "Noyau d'IA";
        _text[185, 3] = "Nucleo IA";
        _text[185, 4] = "KI-Kern";
        _text[185, 5] = "Núcleo de IA";
        _text[185, 6] = "Rdzeń SI";
        _text[185, 7] = "Núcleo de IA";
        _text[185, 8] = "";
        _text[185, 9] = "";

        _text[186, 0] = "Quant";
        _text[186, 1] = "Квант";
        _text[186, 2] = "Quant";
        _text[186, 3] = "Quant";
        _text[186, 4] = "Quant";
        _text[186, 5] = "Quant";
        _text[186, 6] = "Quant";
        _text[186, 7] = "Quant";
        _text[186, 8] = "";
        _text[186, 9] = "";

        _text[187, 0] = "Quant received";
        _text[187, 1] = "Получено квант";
        _text[187, 2] = "Quantum reçu";
        _text[187, 3] = "Quant ottenuti";
        _text[187, 4] = "Quanten erhalten";
        _text[187, 5] = "Quant obtenidos";
        _text[187, 6] = "Otrzymano quant";
        _text[187, 7] = "Quant obtidos";
        _text[187, 8] = "";
        _text[187, 9] = "";

        _text[188, 0] = "Scout";
        _text[188, 1] = "Разведчик";
        _text[188, 2] = "Scout";
        _text[188, 3] = "Esploratore";
        _text[188, 4] = "Späher";
        _text[188, 5] = "Explorador";
        _text[188, 6] = "Zwiadowca";
        _text[188, 7] = "Batedor";
        _text[188, 8] = "";
        _text[188, 9] = "";

        _text[189, 0] = "Engineer";
        _text[189, 1] = "Инженер";
        _text[189, 2] = "Ingénieur";
        _text[189, 3] = "Ingegnere";
        _text[189, 4] = "Ingenieur";
        _text[189, 5] = "Ingeniero";
        _text[189, 6] = "Inżynier";
        _text[189, 7] = "Engenheiro";
        _text[189, 8] = "";
        _text[189, 9] = "";

        _text[190, 0] = "Patch-08";
        _text[190, 1] = "Патч-08";
        _text[190, 2] = "Patch-08";
        _text[190, 3] = "Patch-08";
        _text[190, 4] = "Patch-08";
        _text[190, 5] = "Patch-08";
        _text[190, 6] = "Patch-08";
        _text[190, 7] = "Patch-08";
        _text[190, 8] = "";
        _text[190, 9] = "";

        _text[191, 0] = "Aim Bot";
        _text[191, 1] = "Аим Бот";
        _text[191, 2] = "Aim Bot";
        _text[191, 3] = "Aim Bot";
        _text[191, 4] = "Aim Bot";
        _text[191, 5] = "Aim Bot";
        _text[191, 6] = "Aim Bot";
        _text[191, 7] = "Aim Bot";
        _text[191, 8] = "";
        _text[191, 9] = "";

        _text[192, 0] = "Titan";
        _text[192, 1] = "Титан";
        _text[192, 2] = "Titane";
        _text[192, 3] = "Titan";
        _text[192, 4] = "Titan";
        _text[192, 5] = "Titan";
        _text[192, 6] = "Titan";
        _text[192, 7] = "Titan";
        _text[192, 8] = "";
        _text[192, 9] = "";

        _text[193, 0] = "Functional";
        _text[193, 1] = "Функционал";
        _text[193, 2] = "Fonctionnel";
        _text[193, 3] = "Funzionalità";
        _text[193, 4] = "Funktion";
        _text[193, 5] = "Funcionalidad";
        _text[193, 6] = "Funkcjonalność";
        _text[193, 7] = "Funcionalidade";
        _text[193, 8] = "";
        _text[193, 9] = "";

        _text[194, 0] = "unknown";
        _text[194, 1] = "неизвестно";
        _text[194, 2] = "inconnu";
        _text[194, 3] = "sconosciuto";
        _text[194, 4] = "unbekannt";
        _text[194, 5] = "desconocido";
        _text[194, 6] = "nieznane";
        _text[194, 7] = "desconhecido";
        _text[194, 8] = "";
        _text[194, 9] = "";

        _text[195, 0] = "explores the surrounding area in search of resources";
        _text[195, 1] = "исследует окрестности в поисках ресурсов";
        _text[195, 2] = "explore les environs à la recherche de ressources";
        _text[195, 3] = "esplora i dintorni in cerca di risorse";
        _text[195, 4] = "erkundet die Umgebung auf der Suche nach Ressourcen";
        _text[195, 5] = "explora los alrededores en busca de recursos";
        _text[195, 6] = "bada okolicę w poszukiwaniu zasobów";
        _text[195, 7] = "explora os arredores em busca de recursos";
        _text[195, 8] = "";
        _text[195, 9] = "";

        _text[196, 0] = "repairs the specified buildings";
        _text[196, 1] = "ремонтирует указанные здания";
        _text[196, 2] = "répare les bâtiments spécifiés";
        _text[196, 3] = "ripara gli edifici indicati";
        _text[196, 4] = "repariert die ausgewählten Gebäude";
        _text[196, 5] = "repara los edificios indicados";
        _text[196, 6] = "naprawia wskazane budynki";
        _text[196, 7] = "repara os edifícios selecionados";
        _text[196, 8] = "";
        _text[196, 9] = "";

        _text[197, 0] = "attacks enemy creatures";
        _text[197, 1] = "атакует вражеских существ";
        _text[197, 2] = "attaques contre les créatures ennemies";
        _text[197, 3] = "attacca le creature nemiche";
        _text[197, 4] = "greift feindliche Kreaturen an";
        _text[197, 5] = "ataca a las criaturas enemigas";
        _text[197, 6] = "atakuje wrogie stworzenia";
        _text[197, 7] = "ataca as criaturas inimigas";
        _text[197, 8] = "";
        _text[197, 9] = "";

        _text[198, 0] = "Combat";
        _text[198, 1] = "Боевой";
        _text[198, 2] = "Combat";
        _text[198, 3] = "Da combattimento";
        _text[198, 4] = "Kampf";
        _text[198, 5] = "De combate";
        _text[198, 6] = "Bojowy";
        _text[198, 7] = "Combate";
        _text[198, 8] = "";
        _text[198, 9] = "";

        _text[199, 0] = "Base Crate";
        _text[199, 1] = "Базовый Контейнер";
        _text[199, 2] = "Conteneur de base";
        _text[199, 3] = "Contenitore base";
        _text[199, 4] = "Basiscontainer";
        _text[199, 5] = "Contenedor básico";
        _text[199, 6] = "Podstawowy kontener";
        _text[199, 7] = "Contentor básico";
        _text[199, 8] = "";
        _text[199, 9] = "";

        _text[200, 0] = "Metal Crate";
        _text[200, 1] = "Металлический Контейнер";
        _text[200, 2] = "Conteneur métallique";
        _text[200, 3] = "Contenitore metallico";
        _text[200, 4] = "Metallcontainer";
        _text[200, 5] = "Contenedor metálico";
        _text[200, 6] = "Metalowy Kontener";
        _text[200, 7] = "Contentor metálico";
        _text[200, 8] = "";
        _text[200, 9] = "";

        _text[201, 0] = "Supply Crate";
        _text[201, 1] = "Контейнер Снабжения";
        _text[201, 2] = "Conteneur de ravitaillement";
        _text[201, 3] = "Contenitore di rifornimenti";
        _text[201, 4] = "Versorgungscontainer";
        _text[201, 5] = "Contenedor de suministros";
        _text[201, 6] = "Kontener Zaopatrzeniowy";
        _text[201, 7] = "Contentor de abastecimento";
        _text[201, 8] = "";
        _text[201, 9] = "";

        _text[202, 0] = "Crates";
        _text[202, 1] = "Контейнеры";
        _text[202, 2] = "Conteneurs";
        _text[202, 3] = "Contenitori";
        _text[202, 4] = "Container";
        _text[202, 5] = "Contenedores";
        _text[202, 6] = "Kontenery";
        _text[202, 7] = "Contentores";
        _text[202, 8] = "";
        _text[202, 9] = "";

        _text[203, 0] = "The radiation level is starting to increase gradually. Be careful.";
        _text[203, 1] = "Уровень радиации начинает постепенно расти. Будьте осторожны.";
        _text[203, 2] = "Les niveaux de radiation commencent à augmenter progressivement. Soyez prudent.";
        _text[203, 3] = "Il livello di radiazioni inizia ad aumentare gradualmente. Fai attenzione.";
        _text[203, 4] = "Der Strahlungspegel beginnt allmählich zu steigen. Sei vorsichtig.";
        _text[203, 5] = "El nivel de radiación comienza a aumentar gradualmente. Ten cuidado.";
        _text[203, 6] = "Poziom promieniowania zaczyna stopniowo rosnąć. Zachowaj ostrożność.";
        _text[203, 7] = "O nível de radiação começa a aumentar gradualmente. Tenha cuidado.";
        _text[203, 8] = "";
        _text[203, 9] = "";

        _text[204, 0] = "Average increase in background radiation registered. Prepare for possible consequences.";
        _text[204, 1] = "Зарегистрирован средний рост радиационного фона. Подготовьтесь к возможным последствиям.";
        _text[204, 2] = "Une augmentation modérée du rayonnement de fond a été enregistrée. Préparez-vous aux conséquences possibles.";
        _text[204, 3] = "È stato registrato un aumento moderato del fondo radioattivo. Preparati a possibili conseguenze.";
        _text[204, 4] = "Ein moderater Anstieg der Hintergrundstrahlung wurde registriert. Bereite dich auf mögliche Folgen vor.";
        _text[204, 5] = "Se ha registrado un aumento moderado del fondo radiactivo. Prepárate para posibles consecuencias.";
        _text[204, 6] = "Zarejestrowano umiarkowany wzrost tła promieniowania. Przygotuj się na możliwe konsekwencje.";
        _text[204, 7] = "Foi registado um aumento moderado do fundo de radiação. Prepare-se para possíveis consequências.";
        _text[204, 8] = "";
        _text[204, 9] = "";

        _text[205, 0] = "Warning! A sharp increase in radiation is expected. Take protective measures immediately.";
        _text[205, 1] = "Внимание! Ожидается резкий скачок радиации. Срочно примите защитные меры.";
        _text[205, 2] = "Avertissement! Une forte augmentation du niveau de radiation est prévue. Prenez immédiatement des mesures de protection.";
        _text[205, 3] = "Attenzione! È previsto un brusco aumento delle radiazioni. Adotta subito misure di protezione.";
        _text[205, 4] = "Achtung! Ein starker Strahlungssprung wird erwartet. Ergreife sofort Schutzmaßnahmen.";
        _text[205, 5] = "¡Atención! Se espera un fuerte pico de radiación. Toma medidas de protección de inmediato.";
        _text[205, 6] = "Uwaga! Oczekiwany jest gwałtowny skok promieniowania. Natychmiast podejmij środki ochronne.";
        _text[205, 7] = "Atenção! Espera-se um aumento brusco de radiação. Tome medidas de proteção com urgência.";
        _text[205, 8] = "";
        _text[205, 9] = "";

        _text[206, 0] = "The radiation level is gradually decreasing, making conditions safer.";
        _text[206, 1] = "Уровень радиации постепенно снижается – условия становятся безопаснее.";
        _text[206, 2] = "Les niveaux de radiation diminuent progressivement, ce qui rend les conditions plus sûres.";
        _text[206, 3] = "Il livello di radiazioni diminuisce gradualmente: le condizioni diventano più sicure.";
        _text[206, 4] = "Der Strahlungspegel sinkt allmählich – die Bedingungen werden sicherer.";
        _text[206, 5] = "El nivel de radiación desciende gradualmente: las condiciones se vuelven más seguras.";
        _text[206, 6] = "Poziom promieniowania stopniowo spada – warunki stają się bezpieczniejsze.";
        _text[206, 7] = "O nível de radiação está a diminuir gradualmente - as condições tornam-se mais seguras.";
        _text[206, 8] = "";
        _text[206, 9] = "";

        _text[207, 0] = "Average decrease in radiation level recorded. Threat level is falling.";
        _text[207, 1] = "Среднее снижение уровня радиации зафиксировано. Уровень угрозы падает.";
        _text[207, 2] = "On a constaté une baisse moyenne des niveaux de radiation. Le niveau de menace diminue.";
        _text[207, 3] = "È stata registrata una diminuzione moderata del livello di radiazioni. Il livello di minaccia sta calando.";
        _text[207, 4] = "Ein moderater Rückgang des Strahlungspegels wurde festgestellt. Die Bedrohungsstufe sinkt.";
        _text[207, 5] = "Se ha registrado un descenso moderado del nivel de radiación. El nivel de amenaza baja.";
        _text[207, 6] = "Zarejestrowano umiarkowany spadek poziomu promieniowania. Poziom zagrożenia maleje.";
        _text[207, 7] = "Foi registada uma diminuição moderada do nível de radiação. O nível de ameaça está a baixar.";
        _text[207, 8] = "";
        _text[207, 9] = "";

        _text[208, 0] = "A sharp drop in radiation has been recorded. The environment is being restored.";
        _text[208, 1] = "Зафиксировано резкое падение радиации. Окружающая среда восстанавливается.";
        _text[208, 2] = "Une forte baisse des radiations a été enregistrée. L'environnement se rétablit.";
        _text[208, 3] = "È stato registrato un brusco calo delle radiazioni. L'ambiente si sta riprendendo.";
        _text[208, 4] = "Ein starker Rückgang der Strahlung wurde registriert. Die Umwelt erholt sich.";
        _text[208, 5] = "Se ha registrado una caída brusca de la radiación. El entorno se recupera.";
        _text[208, 6] = "Zarejestrowano gwałtowny spadek promieniowania. Środowisko się regeneruje.";
        _text[208, 7] = "Foi registada uma queda brusca de radiação. O ambiente está a recuperar.";
        _text[208, 8] = "";
        _text[208, 9] = "";

        _text[209, 0] = "Precipitation analysis indicates high acidity. Rain is expected.";
        _text[209, 1] = "Анализ осадков указывает на высокую кислотность. Ожидается дождь.";
        _text[209, 2] = "L'analyse des précipitations indique une forte acidité. De la pluie est attendue.";
        _text[209, 3] = "L'analisi delle precipitazioni indica un'elevata acidità. È previsto un temporale.";
        _text[209, 4] = "Die Niederschlagsanalyse weist auf eine hohe Säurekonzentration hin. Regen wird erwartet.";
        _text[209, 5] = "El análisis de las precipitaciones indica alta acidez. Se espera lluvia.";
        _text[209, 6] = "Analiza opadów wskazuje na wysoką kwasowość. Spodziewany jest deszcz.";
        _text[209, 7] = "A análise da precipitação indica elevada acidez. Prevê-se chuva.";
        _text[209, 8] = "";
        _text[209, 9] = "";

        _text[210, 0] = "Orbital scanners have detected a meteor shower - prepare for strikes from the skies.";
        _text[210, 1] = "Орбитальные сканеры выявили метеорный поток - готовьтесь к ударам с небес.";
        _text[210, 2] = "Des scanners orbitaux ont détecté une pluie de météores – préparez-vous à des impacts célestes.";
        _text[210, 3] = "Gli scanner orbitali hanno rilevato uno sciame meteorico - preparati a colpi dal cielo.";
        _text[210, 4] = "Orbitalscanner haben einen Meteorschauer entdeckt - bereite dich auf Einschläge aus dem Himmel vor.";
        _text[210, 5] = "Los escáneres orbitales han detectado una lluvia de meteoros: prepárate para impactos desde el cielo.";
        _text[210, 6] = "Skanery orbitalne wykryły rój meteorów - przygotuj się na uderzenia z nieba.";
        _text[210, 7] = "Os scanners orbitais detetaram uma chuva de meteoros - prepare-se para impactos vindos do céu.";
        _text[210, 8] = "";
        _text[210, 9] = "";

        _text[211, 0] = "Seismic sensors are recording powerful tremors – an earthquake is approaching.";
        _text[211, 1] = "Сейсмические датчики фиксируют мощные подземные толчки – приближается землетрясение.";
        _text[211, 2] = "Les capteurs sismiques détectent de puissantes secousses – un tremblement de terre approche.";
        _text[211, 3] = "I sensori sismici rilevano forti scosse sotterranee - si avvicina un terremoto.";
        _text[211, 4] = "Seismische Sensoren registrieren starke unterirdische Erschütterungen – ein Erdbeben nähert sich.";
        _text[211, 5] = "Los sensores sísmicos registran fuertes temblores subterráneos: se aproxima un terremoto.";
        _text[211, 6] = "Czujniki sejsmiczne rejestrują silne wstrząsy podziemne – zbliża się trzęsienie ziemi.";
        _text[211, 7] = "Os sensores sísmicos registam fortes abalos subterrâneos - aproxima-se um sismo.";
        _text[211, 8] = "";
        _text[211, 9] = "";

        _text[212, 0] = "Toxic compounds have been detected in the atmosphere. The wind is carrying a dangerous gas.";
        _text[212, 1] = "В атмосфере обнаружены токсичные соединения. Ветер несёт опасный газ.";
        _text[212, 2] = "Des composés toxiques ont été détectés dans l'atmosphère. Le vent transporte un gaz dangereux.";
        _text[212, 3] = "Nell'atmosfera sono stati rilevati composti tossici. Il vento trasporta un gas pericoloso.";
        _text[212, 4] = "In der Atmosphäre wurden toxische Verbindungen обнаружены. Der Wind trägt gefährliches Gas.";
        _text[212, 5] = "Se han detectado compuestos tóxicos en la atmósfera. El viento trae gas peligroso.";
        _text[212, 6] = "W atmosferze wykryto toksyczne związki. Wiatr niesie niebezpieczny gaz.";
        _text[212, 7] = "Foram detetados compostos tóxicos na atmosfera. O vento transporta gás perigoso.";
        _text[212, 8] = "";
        _text[212, 9] = "";

        _text[213, 0] = "Underground pressure is increasing. A spontaneous release of oil to the surface is possible.";
        _text[213, 1] = "Подземное давление растёт. Возможен самопроизвольный выброс нефти на поверхность.";
        _text[213, 2] = "La pression souterraine augmente. Une remontée spontanée de pétrole à la surface est possible.";
        _text[213, 3] = "La pressione sotterranea aumenta. È possibile un'eruzione spontanea di petrolio in superficie.";
        _text[213, 4] = "Der unterirdische Druck steigt. Ein spontaner Ölausbruch an die Oberfläche ist möglich.";
        _text[213, 5] = "La presión subterránea aumenta. Es posible una expulsión espontánea de petróleo a la superficie.";
        _text[213, 6] = "Ciśnienie podziemne rośnie. Możliwy jest samoczynny wyrzut ropy na powierzchnię.";
        _text[213, 7] = "A pressão subterrânea está a aumentar. É possível uma erupção espontânea de petróleo à superfície.";
        _text[213, 8] = "";
        _text[213, 9] = "";

        _text[214, 0] = "Repairs all marked buildings.";
        _text[214, 1] = "Ремонтирует все помеченные здания.";
        _text[214, 2] = "Réparation de tous les bâtiments signalés.";
        _text[214, 3] = "Ripara tutti gli edifici contrassegnati.";
        _text[214, 4] = "Repariert alle markierten Gebäude.";
        _text[214, 5] = "Repara todos los edificios marcados.";
        _text[214, 6] = "Naprawia wszystkie oznaczone budynki.";
        _text[214, 7] = "Repara todos os edifícios marcados.";
        _text[214, 8] = "";
        _text[214, 9] = "";

        _text[215, 0] = "Fortification";
        _text[215, 1] = "Укрепление";
        _text[215, 2] = "Renforcement";
        _text[215, 3] = "Rinforzo";
        _text[215, 4] = "Befestigung";
        _text[215, 5] = "Fortificación";
        _text[215, 6] = "Wzmocnienie";
        _text[215, 7] = "Reforço";
        _text[215, 8] = "";
        _text[215, 9] = "";

        _text[216, 0] = "For one day, reduces damage to all buildings by 2 times.";
        _text[216, 1] = "На один день, уменьшает урон по всем зданиям в 2 раза.";
        _text[216, 2] = "Pendant une journée, réduit de moitié les dégâts causés à tous les bâtiments.";
        _text[216, 3] = "Per un giorno, riduce di 2 volte i danni a tutti gli edifici.";
        _text[216, 4] = "Für einen Tag halbiert es den Schaden an allen Gebäuden.";
        _text[216, 5] = "Durante un día, reduce el daño a todos los edificios a la mitad.";
        _text[216, 6] = "Na jeden dzień zmniejsza obrażenia zadawane wszystkim budynkom o połowę.";
        _text[216, 7] = "Durante um dia, reduz para metade o dano a todos os edifícios.";
        _text[216, 8] = "";
        _text[216, 9] = "";

        _text[217, 0] = "Production optimization";
        _text[217, 1] = "Оптимизация производства";
        _text[217, 2] = "Optimisation de la production";
        _text[217, 3] = "Ottimizzazione della produzione";
        _text[217, 4] = "Produktionsoptimierung";
        _text[217, 5] = "Optimización de producción";
        _text[217, 6] = "Optymalizacja produkcji";
        _text[217, 7] = "Otimização da produção";
        _text[217, 8] = "";
        _text[217, 9] = "";

        _text[218, 0] = "For one day, increases resource production by 2 times.";
        _text[218, 1] = "На один день, увеличивает добычу ресурсов в 2 раза.";
        _text[218, 2] = "Pendant une journée, la production de ressources est multipliée par 2.";
        _text[218, 3] = "Per un giorno, aumenta di 2 volte l'estrazione delle risorse.";
        _text[218, 4] = "Für einen Tag verdoppelt es die Ressourcengewinnung.";
        _text[218, 5] = "Durante un día, duplica la extracción de recursos.";
        _text[218, 6] = "Na jeden dzień zwiększa wydobycie zasobów dwukrotnie.";
        _text[218, 7] = "Durante um dia, duplica a extração de recursos.";
        _text[218, 8] = "";
        _text[218, 9] = "";

        _text[219, 0] = "Ignite";
        _text[219, 1] = "Поджег";
        _text[219, 2] = "Mettre le feu";
        _text[219, 3] = "Incendio";
        _text[219, 4] = "Brandstiftung";
        _text[219, 5] = "Incendio";
        _text[219, 6] = "Podpalenie";
        _text[219, 7] = "Ignição";
        _text[219, 8] = "";
        _text[219, 9] = "";

        _text[220, 0] = "Creates uncontrollable flames. Deals damage to both enemies and your buildings.";
        _text[220, 1] = "Создает неконтролируемое пламя. Наносит урон как врагам, так и вашим постройкам.";
        _text[220, 2] = "Crée une flamme incontrôlable, endommageant à la fois les ennemis et vos propres structures.";
        _text[220, 3] = "Crea un fuoco incontrollabile. Infligge danni sia ai nemici che alle tue costruzioni.";
        _text[220, 4] = "Erzeugt unkontrollierbares Feuer. Fügt sowohl Gegnern als auch deinen Bauten Schaden zu.";
        _text[220, 5] = "Crea un fuego incontrolable. Inflige daño tanto a enemigos como a tus construcciones.";
        _text[220, 6] = "Tworzy niekontrolowany płomień. Zadaje obrażenia zarówno wrogom, jak i twoim budowlom.";
        _text[220, 7] = "Cria chamas incontroláveis. Causa dano tanto aos inimigos como às suas construções.";
        _text[220, 8] = "";
        _text[220, 9] = "";

        _text[221, 0] = "Mountain";
        _text[221, 1] = "Гора";
        _text[221, 2] = "Montagne";
        _text[221, 3] = "Montagna";
        _text[221, 4] = "Berg";
        _text[221, 5] = "Montaña";
        _text[221, 6] = "Góra";
        _text[221, 7] = "Montanha";
        _text[221, 8] = "";
        _text[221, 9] = "";

        _text[222, 0] = "Toggle resources panel";
        _text[222, 1] = "Переключает панель ресурсов";
        _text[222, 2] = "Affiche/masque le panneau des ressources";
        _text[222, 3] = "Alterna il pannello risorse";
        _text[222, 4] = "Schaltet das Ressourcenpanel um";
        _text[222, 5] = "Alterna el panel de recursos";
        _text[222, 6] = "Przełącza panel zasobów";
        _text[222, 7] = "Alterna o painel de recursos";
        _text[222, 8] = "";
        _text[222, 9] = "";

        _text[223, 0] = "Cancel skill targeting";
        _text[223, 1] = "Отменить прицел умения";
        _text[223, 2] = "Annuler le ciblage de compétences";
        _text[223, 3] = "Annulla la mira dell'abilità";
        _text[223, 4] = "Fähigkeitsziel abbrechen";
        _text[223, 5] = "Cancelar apuntado de habilidad";
        _text[223, 6] = "Anuluj celowanie umiejętności";
        _text[223, 7] = "Cancelar mira da habilidade";
        _text[223, 8] = "";
        _text[223, 9] = "";

        _text[224, 0] = "Toggle skills panel";
        _text[224, 1] = "Переключает панель умений";
        _text[224, 2] = "Active/désactive la barre de compétences";
        _text[224, 3] = "Alterna il pannello abilità";
        _text[224, 4] = "Schaltet das Fähigkeitenpanel um";
        _text[224, 5] = "Alterna el panel de habilidades";
        _text[224, 6] = "Przełącza panel umiejętności";
        _text[224, 7] = "Alterna o painel de habilidades";
        _text[224, 8] = "";
        _text[224, 9] = "";

        _text[225, 0] = "Go";
        _text[225, 1] = "Перейти";
        _text[225, 2] = "Aller";
        _text[225, 3] = "Vai";
        _text[225, 4] = "Gehen";
        _text[225, 5] = "Ir";
        _text[225, 6] = "Przejdź";
        _text[225, 7] = "Ir";
        _text[225, 8] = "";
        _text[225, 9] = "";

        _text[226, 0] = "Collect";
        _text[226, 1] = "Соберите";
        _text[226, 2] = "Collecter";
        _text[226, 3] = "Raccogli";
        _text[226, 4] = "Sammeln";
        _text[226, 5] = "Reúne";
        _text[226, 6] = "Zbierz";
        _text[226, 7] = "Recolha";
        _text[226, 8] = "";
        _text[226, 9] = "";

        _text[227, 0] = "Durability Increased";
        _text[227, 1] = "Прочность Повышена";
        _text[227, 2] = "Durabilité Accrue";
        _text[227, 3] = "Integrità Aumentata";
        _text[227, 4] = "Haltbarkeit Erhöht";
        _text[227, 5] = "Durabilidad Aumentada";
        _text[227, 6] = "Wytrzymałość Zwiększona";
        _text[227, 7] = "Durabilidade Aumentada";
        _text[227, 8] = "";
        _text[227, 9] = "";

        _text[228, 0] = "Steel Riffle";
        _text[228, 1] = "Стальная Винтовка";
        _text[228, 2] = "Fusil en Acier";
        _text[228, 3] = "Fucile d'Acciaio";
        _text[228, 4] = "Stahlgewehr";
        _text[228, 5] = "Rifle de Acero";
        _text[228, 6] = "Stalowy Karabin";
        _text[228, 7] = "Rifle de Aço";
        _text[228, 8] = "";
        _text[228, 9] = "";

        _text[229, 0] = "Titanium Rocket Launcher";
        _text[229, 1] = "Титановая Ракетная Установка";
        _text[229, 2] = "Lance-roquettes en Titane";
        _text[229, 3] = "Lanciatorazzi in Titanio";
        _text[229, 4] = "Titan-Raketenwerfer";
        _text[229, 5] = "Lanzacohetes de Titanio";
        _text[229, 6] = "Tytanowa wyrzutnia Rakiet";
        _text[229, 7] = "Lançador de Foguetes de Titânio";
        _text[229, 8] = "";
        _text[229, 9] = "";

        _text[230, 0] = "Ammo";
        _text[230, 1] = "Боеприпасы";
        _text[230, 2] = "Munitions";
        _text[230, 3] = "Munizioni";
        _text[230, 4] = "Munition";
        _text[230, 5] = "Munición";
        _text[230, 6] = "Amunicja";
        _text[230, 7] = "Munições";
        _text[230, 8] = "";
        _text[230, 9] = "";

        _text[231, 0] = "Level";
        _text[231, 1] = "Уровень";
        _text[231, 2] = "Niveau";
        _text[231, 3] = "Livello";
        _text[231, 4] = "Stufe";
        _text[231, 5] = "Nivel";
        _text[231, 6] = "Poziom";
        _text[231, 7] = "Nível";
        _text[231, 8] = "";
        _text[231, 9] = "";

        _text[232, 0] = "Upgrade";
        _text[232, 1] = "Улучшить";
        _text[232, 2] = "Améliorer";
        _text[232, 3] = "Migliora";
        _text[232, 4] = "Verbessern";
        _text[232, 5] = "Mejorar";
        _text[232, 6] = "Ulepsz";
        _text[232, 7] = "Melhorar";
        _text[232, 8] = "";
        _text[232, 9] = "";

        _text[233, 0] = "Change Mode";
        _text[233, 1] = "Сменить Режим";
        _text[233, 2] = "Changer de mode";
        _text[233, 3] = "Cambia Modalità";
        _text[233, 4] = "Modus Wechseln";
        _text[233, 5] = "Cambiar Modo";
        _text[233, 6] = "Zmień Tryb";
        _text[233, 7] = "Mudar Modo";
        _text[233, 8] = "";
        _text[233, 9] = "";

        _text[234, 0] = "Act";
        _text[234, 1] = "Акт";
        _text[234, 2] = "Acte";
        _text[234, 3] = "Atto";
        _text[234, 4] = "Akt";
        _text[234, 5] = "Acto";
        _text[234, 6] = "Akt";
        _text[234, 7] = "Ato";
        _text[234, 8] = "";
        _text[234, 9] = "";

        _text[235, 0] = "Node";
        _text[235, 1] = "Узел";
        _text[235, 2] = "Nœud";
        _text[235, 3] = "Nodo";
        _text[235, 4] = "Knoten";
        _text[235, 5] = "Nodo";
        _text[235, 6] = "Węzeł";
        _text[235, 7] = "Nó";
        _text[235, 8] = "";
        _text[235, 9] = "";

        _text[236, 0] = "Success";
        _text[236, 1] = "Успех";
        _text[236, 2] = "Succès";
        _text[236, 3] = "Successo";
        _text[236, 4] = "Erfolg";
        _text[236, 5] = "Éxito";
        _text[236, 6] = "Sukces";
        _text[236, 7] = "Sucesso";
        _text[236, 8] = "";
        _text[236, 9] = "";

        _text[237, 0] = "Failure";
        _text[237, 1] = "Неудача";
        _text[237, 2] = "Échec";
        _text[237, 3] = "Fallimento";
        _text[237, 4] = "Misserfolg";
        _text[237, 5] = "Fracaso";
        _text[237, 6] = "Porażka";
        _text[237, 7] = "Fracasso";
        _text[237, 8] = "";
        _text[237, 9] = "";

        _text[238, 0] = "Restored";
        _text[238, 1] = "Восстановлено";
        _text[238, 2] = "Restauré";
        _text[238, 3] = "Ripristinato";
        _text[238, 4] = "Wiederhergestellt";
        _text[238, 5] = "Restaurado";
        _text[238, 6] = "Przywrócono";
        _text[238, 7] = "Restaurado";
        _text[238, 8] = "";
        _text[238, 9] = "";

        _text[239, 0] = "Scatter Shotgun";
        _text[239, 1] = "Дробовик Рассеиватель";
        _text[239, 2] = "Diffuseur pour fusil de chasse";
        _text[239, 3] = "Fucile a pompa dispersore";
        _text[239, 4] = "Streuschrotflinte";
        _text[239, 5] = "Escopeta Dispersora";
        _text[239, 6] = "Strzelba Rozrzutowa";
        _text[239, 7] = "Espingarda Dispersora";
        _text[239, 8] = "";
        _text[239, 9] = "";

        _text[240, 0] = "Longshot Railgun";
        _text[240, 1] = "Дальнобойный Рельсотрон";
        _text[240, 2] = "Canon électromagnétique à longue portée";
        _text[240, 3] = "Cannone a rotaia a lungo raggio";
        _text[240, 4] = "Langstrecken-Railgun";
        _text[240, 5] = "Cañón de riel de largo alcance";
        _text[240, 6] = "Dalekosiężny Relsotron";
        _text[240, 7] = "Railgun de longo alcance";
        _text[240, 8] = "";
        _text[240, 9] = "";

        _text[241, 0] = "Breakshot Minigun";
        _text[241, 1] = "Разрывной Пулемет";
        _text[241, 2] = "Mitrailleuse explosive";
        _text[241, 3] = "Mitragliatrice esplosiva";
        _text[241, 4] = "Spreng-Maschinengewehr";
        _text[241, 5] = "Ametralladora explosiva";
        _text[241, 6] = "Rozrywający Karabin Maszynowy";
        _text[241, 7] = "Metralhadora explosiva";
        _text[241, 8] = "";
        _text[241, 9] = "";

        _text[242, 0] = "Blastfire Launcher";
        _text[242, 1] = "Взрывопламенная Установка";
        _text[242, 2] = "Installation de flamme explosive";
        _text[242, 3] = "Lanciatore Esplosivo-incendiario";
        _text[242, 4] = "Explosions-Flammenwerfer";
        _text[242, 5] = "Lanzador ígneo Explosivo";
        _text[242, 6] = "Wyrzutnia Wybuchopłomieniowa";
        _text[242, 7] = "Lança-chamas Explosivo";
        _text[242, 8] = "";
        _text[242, 9] = "";

        _text[243, 0] = "Machine";
        _text[243, 1] = "Машина";
        _text[243, 2] = "Machine";
        _text[243, 3] = "Macchina";
        _text[243, 4] = "Maschine";
        _text[243, 5] = "Máquina";
        _text[243, 6] = "Maszyna";
        _text[243, 7] = "Máquina";
        _text[243, 8] = "";
        _text[243, 9] = "";

        _text[244, 0] = "Resources for create machine:";
        _text[244, 1] = "Ресурсы для создания машины:";
        _text[244, 2] = "Ressources pour la création d'une machine :";
        _text[244, 3] = "Risorse per creare una macchina:";
        _text[244, 4] = "Ressourcen zum Herstellen der Maschine:";
        _text[244, 5] = "Recursos para crear una máquina:";
        _text[244, 6] = "Zasoby do stworzenia maszyny:";
        _text[244, 7] = "Recursos para criar uma máquina:";
        _text[244, 8] = "";
        _text[244, 9] = "";

        _text[245, 0] = "Ecological Restoration";
        _text[245, 1] = "Восстановление экологии";
        _text[245, 2] = "Restaurer l'environnement";
        _text[245, 3] = "Ripristino dell'ecologia";
        _text[245, 4] = "Ökologie-Wiederherstellung";
        _text[245, 5] = "Restauración ecológica";
        _text[245, 6] = "Przywracanie ekologii";
        _text[245, 7] = "Restauração da ecologia";
        _text[245, 8] = "";
        _text[245, 9] = "";

        _text[246, 0] = "First skill";
        _text[246, 1] = "Первое умение";
        _text[246, 2] = "Première compétence";
        _text[246, 3] = "Prima abilità";
        _text[246, 4] = "Erste Fähigkeit";
        _text[246, 5] = "Primera habilidad";
        _text[246, 6] = "Pierwsza umiejętność";
        _text[246, 7] = "Primeira habilidade";
        _text[246, 8] = "";
        _text[246, 9] = "";

        _text[247, 0] = "Second skill";
        _text[247, 1] = "Второе умение";
        _text[247, 2] = "Deuxième compétence";
        _text[247, 3] = "Seconda abilità";
        _text[247, 4] = "Zweite Fähigkeit";
        _text[247, 5] = "Segunda habilidad";
        _text[247, 6] = "Druga umiejętność";
        _text[247, 7] = "Segunda habilidade";
        _text[247, 8] = "";
        _text[247, 9] = "";

        _text[248, 0] = "This button is already in use. Press another.";
        _text[248, 1] = "Данная кнопка уже используется. Нажмите другую.";
        _text[248, 2] = "Ce bouton est déjà utilisé. Veuillez en cliquer sur un autre.";
        _text[248, 3] = "Questo tasto è già in uso. Premi un altro.";
        _text[248, 4] = "Diese Taste wird bereits verwendet. Drücke eine andere.";
        _text[248, 5] = "Este botón ya está en uso. Pulsa otro.";
        _text[248, 6] = "Ten przycisk jest już używany. Naciśnij inny.";
        _text[248, 7] = "Esta tecla já está a ser utilizada. Prima outra.";
        _text[248, 8] = "";
        _text[248, 9] = "";

        _text[249, 0] = "Iron Deposits";
        _text[249, 1] = "Залежи Железа";
        _text[249, 2] = "Gisements de fer";
        _text[249, 3] = "Giacimenti di ferro";
        _text[249, 4] = "Eisenvorkommen";
        _text[249, 5] = "Vetas de hierro";
        _text[249, 6] = "Złoża żelaza";
        _text[249, 7] = "Depósitos de ferro";
        _text[249, 8] = "";
        _text[249, 9] = "";

        _text[250, 0] = "Copper Deposits";
        _text[250, 1] = "Залежи Меди";
        _text[250, 2] = "Gisements de cuivre";
        _text[250, 3] = "Giacimenti di rame";
        _text[250, 4] = "Kupfervorkommen";
        _text[250, 5] = "Vetas de cobre";
        _text[250, 6] = "Złoża miedzi";
        _text[250, 7] = "Depósitos de cobre";
        _text[250, 8] = "";
        _text[250, 9] = "";

        _text[251, 0] = "Oil Swamp";
        _text[251, 1] = "Нефтяное Болото";
        _text[251, 2] = "Marais pétrolier";
        _text[251, 3] = "Palude di petrolio";
        _text[251, 4] = "Ölsumpf";
        _text[251, 5] = "Pantano petrolífero";
        _text[251, 6] = "Bagno naftowe";
        _text[251, 7] = "Pântano de petróleo";
        _text[251, 8] = "";
        _text[251, 9] = "";

        _text[252, 0] = "Desert";
        _text[252, 1] = "Пустыня";
        _text[252, 2] = "Désert";
        _text[252, 3] = "Deserto";
        _text[252, 4] = "Wüste";
        _text[252, 5] = "Desierto";
        _text[252, 6] = "Pustynia";
        _text[252, 7] = "Deserto";
        _text[252, 8] = "";
        _text[252, 9] = "";

        _text[253, 0] = "Barren Land";
        _text[253, 1] = "Бесплодная Земля";
        _text[253, 2] = "Terre Aride";
        _text[253, 3] = "Terra Sterile";
        _text[253, 4] = "Ödland";
        _text[253, 5] = "Tierra Estéril";
        _text[253, 6] = "Jałowa Ziemia";
        _text[253, 7] = "Terra Estéril";
        _text[253, 8] = "";
        _text[253, 9] = "";

        _text[254, 0] = "Ground";
        _text[254, 1] = "Земля";
        _text[254, 2] = "Terre";
        _text[254, 3] = "Terra";
        _text[254, 4] = "Erde";
        _text[254, 5] = "Tierra";
        _text[254, 6] = "Ziemia";
        _text[254, 7] = "Terra";
        _text[254, 8] = "";
        _text[254, 9] = "";

        _text[255, 0] = "Coal Deposits";
        _text[255, 1] = "Залежи Угля";
        _text[255, 2] = "Gisements de Charbon";
        _text[255, 3] = "Giacimenti di Carbone";
        _text[255, 4] = "Kohlevorkommen";
        _text[255, 5] = "Vetas de Carbón";
        _text[255, 6] = "Złoża Węgla";
        _text[255, 7] = "Depósitos de Carvão";
        _text[255, 8] = "";
        _text[255, 9] = "";

        _text[256, 0] = "Highland";
        _text[256, 1] = "Плоскогорье";
        _text[256, 2] = "Plateau";
        _text[256, 3] = "Altopiano";
        _text[256, 4] = "Hochebene";
        _text[256, 5] = "Meseta";
        _text[256, 6] = "Płaskowyż";
        _text[256, 7] = "Planalto";
        _text[256, 8] = "";
        _text[256, 9] = "";

        _text[257, 0] = "River";
        _text[257, 1] = "Река";
        _text[257, 2] = "Rivière";
        _text[257, 3] = "Fiume";
        _text[257, 4] = "Fluss";
        _text[257, 5] = "Río";
        _text[257, 6] = "Rzeka";
        _text[257, 7] = "Rio";
        _text[257, 8] = "";
        _text[257, 9] = "";

        _text[258, 0] = "Polluted River";
        _text[258, 1] = "Загрязненная Река";
        _text[258, 2] = "Rivière Polluée";
        _text[258, 3] = "Fiume Contaminato";
        _text[258, 4] = "Verschmutzter Fluss";
        _text[258, 5] = "Río Contaminado";
        _text[258, 6] = "Zanieczyszczona Rzeka";
        _text[258, 7] = "Rio Poluído";
        _text[258, 8] = "";
        _text[258, 9] = "";

        _text[259, 0] = "Dead Forest";
        _text[259, 1] = "Мертвый Лес";
        _text[259, 2] = "Forêt Morte";
        _text[259, 3] = "Foresta Morta";
        _text[259, 4] = "Toter Wald";
        _text[259, 5] = "Bosque Muerto";
        _text[259, 6] = "Martwy Las";
        _text[259, 7] = "Floresta Morta";
        _text[259, 8] = "";
        _text[259, 9] = "";

        _text[260, 0] = "Oasis";
        _text[260, 1] = "Оазис";
        _text[260, 2] = "Oasis";
        _text[260, 3] = "Oasi";
        _text[260, 4] = "Oase";
        _text[260, 5] = "Oasis";
        _text[260, 6] = "Oaza";
        _text[260, 7] = "Oásis";
        _text[260, 8] = "";
        _text[260, 9] = "";

        _text[261, 0] = "Desert River";
        _text[261, 1] = "Пустынная Река";
        _text[261, 2] = "Rivière du Désert";
        _text[261, 3] = "Fiume Desertico";
        _text[261, 4] = "Wüstenfluss";
        _text[261, 5] = "Río Desértico";
        _text[261, 6] = "Pustynna Rzeka";
        _text[261, 7] = "Rio Desértico";
        _text[261, 8] = "";
        _text[261, 9] = "";

        _text[262, 0] = "Scarce Coal Deposits";
        _text[262, 1] = "Бедные Залежи Угля";
        _text[262, 2] = "Gisements de Charbon Pauvres";
        _text[262, 3] = "Poveri Giacimenti di Carbone";
        _text[262, 4] = "Geringe Kohlevorkommen";
        _text[262, 5] = "Vetas Pobres de Carbón";
        _text[262, 6] = "Uboge Złoża Węgla";
        _text[262, 7] = "Depósitos Pobres de Carvão";
        _text[262, 8] = "";
        _text[262, 9] = "";

        _text[263, 0] = "Base Foundation";
        _text[263, 1] = "Фундамент Базы";
        _text[263, 2] = "Fondation de la Base";
        _text[263, 3] = "Fondazione della Base";
        _text[263, 4] = "Basisfundament";
        _text[263, 5] = "Cimientos de la Base";
        _text[263, 6] = "Fundament Bazy";
        _text[263, 7] = "Fundação da Base";
        _text[263, 8] = "";
        _text[263, 9] = "";

        _text[264, 0] = "Black Desert";
        _text[264, 1] = "Черная Пустыня";
        _text[264, 2] = "Désert Noir";
        _text[264, 3] = "Deserto Nero";
        _text[264, 4] = "Schwarze Wüste";
        _text[264, 5] = "Desierto Negro";
        _text[264, 6] = "Czarna Pustynia";
        _text[264, 7] = "Deserto Negro";
        _text[264, 8] = "";
        _text[264, 9] = "";

        _text[265, 0] = "Dried Oasis";
        _text[265, 1] = "Высохший Оазис";
        _text[265, 2] = "Oasis Sèche";
        _text[265, 3] = "Oasi Prosciugata";
        _text[265, 4] = "Ausgetrocknete Oase";
        _text[265, 5] = "Oasis Seco";
        _text[265, 6] = "Wyschnięta Oaza";
        _text[265, 7] = "Oásis Seco";
        _text[265, 8] = "";
        _text[265, 9] = "";

        _text[266, 0] = "Volcano";
        _text[266, 1] = "Вулкан";
        _text[266, 2] = "Volcan";
        _text[266, 3] = "Vulcano";
        _text[266, 4] = "Vulkan";
        _text[266, 5] = "Volcán";
        _text[266, 6] = "Wulkan";
        _text[266, 7] = "Vulcão";
        _text[266, 8] = "";
        _text[266, 9] = "";

        _text[267, 0] = "Blazing Field";
        _text[267, 1] = "Пылающее Поле";
        _text[267, 2] = "Champ de combustion";
        _text[267, 3] = "Campo ardente";
        _text[267, 4] = "Brennendes Feld";
        _text[267, 5] = "Campo ardiente";
        _text[267, 6] = "Płonące pole";
        _text[267, 7] = "Campo ardente";
        _text[267, 8] = "";
        _text[267, 9] = "";

        _text[268, 0] = "Overgrown Mountain";
        _text[268, 1] = "Заросшая Гора";
        _text[268, 2] = "Montagne envahie par la végétation";
        _text[268, 3] = "Montagna ricoperta";
        _text[268, 4] = "Überwucherter Berg";
        _text[268, 5] = "Montaña cubierta de vegetación";
        _text[268, 6] = "Zarośnięta góra";
        _text[268, 7] = "Montanha coberta";
        _text[268, 8] = "";
        _text[268, 9] = "";

        _text[269, 0] = "Rift";
        _text[269, 1] = "Разлом";
        _text[269, 2] = "Faute";
        _text[269, 3] = "Frattura";
        _text[269, 4] = "Spalte";
        _text[269, 5] = "Grieta";
        _text[269, 6] = "Rozpadlina";
        _text[269, 7] = "Fenda";
        _text[269, 8] = "";
        _text[269, 9] = "";

        _text[270, 0] = "Crater";
        _text[270, 1] = "Кратер";
        _text[270, 2] = "Cratère";
        _text[270, 3] = "Cratere";
        _text[270, 4] = "Krater";
        _text[270, 5] = "Cráter";
        _text[270, 6] = "Krater";
        _text[270, 7] = "Cratera";
        _text[270, 8] = "";
        _text[270, 9] = "";

        _text[271, 0] = "Grove";
        _text[271, 1] = "Роща";
        _text[271, 2] = "Bosquet";
        _text[271, 3] = "Boschetto";
        _text[271, 4] = "Hain";
        _text[271, 5] = "Arboleda";
        _text[271, 6] = "Gaj";
        _text[271, 7] = "Bosque";
        _text[271, 8] = "";
        _text[271, 9] = "";

        _text[272, 0] = "Base";
        _text[272, 1] = "База";
        _text[272, 2] = "Base";
        _text[272, 3] = "Base";
        _text[272, 4] = "Basis";
        _text[272, 5] = "Base";
        _text[272, 6] = "Baza";
        _text[272, 7] = "Base";
        _text[272, 8] = "";
        _text[272, 9] = "";

        _text[273, 0] = "Electric Power Industry";
        _text[273, 1] = "Электроэнергетика";
        _text[273, 2] = "Industrie de l'énergie Electrique";
        _text[273, 3] = "Energia elettrica";
        _text[273, 4] = "Energieversorgung";
        _text[273, 5] = "Energía eléctrica";
        _text[273, 6] = "Elektroenergetyka";
        _text[273, 7] = "Energia elétrica";
        _text[273, 8] = "";
        _text[273, 9] = "";

        _text[274, 0] = "Mining: Coal";
        _text[274, 1] = "Добыча: Угля";
        _text[274, 2] = "Extraction : Charbon";
        _text[274, 3] = "Estrazione: Carbone";
        _text[274, 4] = "Abbau: Kohle";
        _text[274, 5] = "Extracción: Carbón";
        _text[274, 6] = "Wydobycie: Węgla";
        _text[274, 7] = "Extração: Carvão";
        _text[274, 8] = "";
        _text[274, 9] = "";

        _text[275, 0] = "Mining: Ores";
        _text[275, 1] = "Добыча: Руды";
        _text[275, 2] = "Exploitation minière : Minerais";
        _text[275, 3] = "Estrazione: Minerale";
        _text[275, 4] = "Abbau: Erz";
        _text[275, 5] = "Extracción: Mineral";
        _text[275, 6] = "Wydobycie: Rudy";
        _text[275, 7] = "Extração: Minério";
        _text[275, 8] = "";
        _text[275, 9] = "";

        _text[276, 0] = "Rest Station";
        _text[276, 1] = "Станция Отдыха";
        _text[276, 2] = "Station de repos";
        _text[276, 3] = "Stazione di riposo";
        _text[276, 4] = "Raststation";
        _text[276, 5] = "Estación de descanso";
        _text[276, 6] = "Stacja odpoczynku";
        _text[276, 7] = "Estação de descanso";
        _text[276, 8] = "";
        _text[276, 9] = "";

        _text[277, 0] = "Unvisited Node";
        _text[277, 1] = "Непосещенный узел";
        _text[277, 2] = "Nœud non visité";
        _text[277, 3] = "Nodo non visitato";
        _text[277, 4] = "Unbesuchter Knoten";
        _text[277, 5] = "Nodo no visitado";
        _text[277, 6] = "Nieodwiedzony węzeł";
        _text[277, 7] = "Nó não visitado";
        _text[277, 8] = "";
        _text[277, 9] = "";

        _text[278, 0] = "Terminal #042";
        _text[278, 1] = "Терминал #042";
        _text[278, 2] = "Terminal #042";
        _text[278, 3] = "Terminale #042";
        _text[278, 4] = "Terminal #042";
        _text[278, 5] = "Terminal #042";
        _text[278, 6] = "Terminal #042";
        _text[278, 7] = "Terminal #042";
        _text[278, 8] = "";
        _text[278, 9] = "";

        _text[279, 0] = "ICOSA CORP";
        _text[279, 1] = "ИКОСА КОРП";
        _text[279, 2] = "ICOSA CORP";
        _text[279, 3] = "ICOSA CORP";
        _text[279, 4] = "IKOSA CORP";
        _text[279, 5] = "ICOSA CORP";
        _text[279, 6] = "ICOSA CORP";
        _text[279, 7] = "ICOSA CORP";
        _text[279, 8] = "";
        _text[279, 9] = "";

        _text[280, 0] = "BUILDING BETTER WORLD";
        _text[280, 1] = "ПОСТРОИМ ЛУЧШИЙ МИР";
        _text[280, 2] = "CONSTRUISONS UN MONDE MEILLEUR";
        _text[280, 3] = "COSTRUIAMO UN MONDO MIGLIORE";
        _text[280, 4] = "WIR BAUEN DIE BESTE WELT";
        _text[280, 5] = "CONSTRUYAMOS UN MUNDO MEJOR";
        _text[280, 6] = "ZBUDUJMY LEPSZY ŚWIAT";
        _text[280, 7] = "VAMOS CONSTRUIR O MELHOR MUNDO";
        _text[280, 8] = "";
        _text[280, 9] = "";

        _text[281, 0] = "COORDINATES";
        _text[281, 1] = "КООРДИНАТЫ";
        _text[281, 2] = "COORDONNÉES";
        _text[281, 3] = "COORDINATE";
        _text[281, 4] = "KOORDINATEN";
        _text[281, 5] = "COORDENADAS";
        _text[281, 6] = "WSPÓŁRZĘDNE";
        _text[281, 7] = "COORDENADAS";
        _text[281, 8] = "";
        _text[281, 9] = "";

        _text[282, 0] = "SIGNAL";
        _text[282, 1] = "СИГНАЛ";
        _text[282, 2] = "SIGNAL";
        _text[282, 3] = "SEGNALE";
        _text[282, 4] = "SIGNAL";
        _text[282, 5] = "SEÑAL";
        _text[282, 6] = "SYGNAŁ";
        _text[282, 7] = "SINAL";
        _text[282, 8] = "";
        _text[282, 9] = "";

        _text[283, 0] = "DIAGRAM";
        _text[283, 1] = "ДИАГРАММА";
        _text[283, 2] = "DIAGRAMME";
        _text[283, 3] = "DIAGRAMMA";
        _text[283, 4] = "DIAGRAMM";
        _text[283, 5] = "DIAGRAMA";
        _text[283, 6] = "DIAGRAM";
        _text[283, 7] = "DIAGRAMA";
        _text[283, 8] = "";
        _text[283, 9] = "";

        _text[284, 0] = "-Radiation: High\n-Pollution: Critical\n-Update: Active";
        _text[284, 1] = "-Радиация: Высокая\n-Загрязнение: Критическое\n-Обновление: Активно";
        _text[284, 2] = "-Rayonnement: Élevé\n-Pollution: Critique\n-Mise à jour: Active";
        _text[284, 3] = "-Radiazione: Alta\n-Inquinamento: Critico\n-Aggiornamento: Attivo";
        _text[284, 4] = "-Strahlung: Hoch\n-Verschmutzung: Kritisch\n-Update: Aktiv";
        _text[284, 5] = "-Radiación: Alta\n-Contaminación: Crítica\n-Actualización: Activa";
        _text[284, 6] = "-Promieniowanie: Wysokie\n-Zanieczyszczenie: Krytyczne\n-Aktualizacja: Aktywna";
        _text[284, 7] = "-Radiação: Alta\n-Poluição: Crítica\n-Atualização: Ativa";
        _text[284, 8] = "";
        _text[284, 9] = "";

        _text[285, 0] = "Quant - the intergalactic currency";
        _text[285, 1] = "Квант - межгалактическая валюта";
        _text[285, 2] = "Le quantique est une monnaie intergalactique.";
        _text[285, 3] = "Quant - valuta intergalattica";
        _text[285, 4] = "Quant - intergalaktische Währung";
        _text[285, 5] = "Quant - moneda intergaláctica";
        _text[285, 6] = "Quant - międzygalaktyczna waluta";
        _text[285, 7] = "Quant - moeda intergaláctica";
        _text[285, 8] = "";
        _text[285, 9] = "";

        _text[286, 0] = "AI cores are the ship's vital modules.";
        _text[286, 1] = "Ядра ИИ - жизненно важные модули корабля";
        _text[286, 2] = "Les cœurs d'IA sont des modules vitaux du vaisseau.";
        _text[286, 3] = "Nuclei IA - moduli vitali della nave";
        _text[286, 4] = "KI-Kerne - lebenswichtige Module des Schiffs";
        _text[286, 5] = "Núcleos de IA: módulos vitales de la nave";
        _text[286, 6] = "Rdzenie SI - życiowo ważne moduły statku";
        _text[286, 7] = "Núcleos de IA - módulos vitais da nave";
        _text[286, 8] = "";
        _text[286, 9] = "";

        _text[287, 0] = "Resource Trader";
        _text[287, 1] = "Торговец Ресурсами";
        _text[287, 2] = "Négociant de ressources";
        _text[287, 3] = "Mercante di risorse";
        _text[287, 4] = "Ressourcenhändler";
        _text[287, 5] = "Comerciante de recursos";
        _text[287, 6] = "Handlarz zasobów";
        _text[287, 7] = "Mercador de recursos";
        _text[287, 8] = "";
        _text[287, 9] = "";

        _text[288, 0] = "Price";
        _text[288, 1] = "Цена";
        _text[288, 2] = "Prix";
        _text[288, 3] = "Prezzo";
        _text[288, 4] = "Preis";
        _text[288, 5] = "Precio";
        _text[288, 6] = "Cena";
        _text[288, 7] = "Preço";
        _text[288, 8] = "";
        _text[288, 9] = "";

        _text[289, 0] = "Buy";
        _text[289, 1] = "Купить";
        _text[289, 2] = "Acheter";
        _text[289, 3] = "Compra";
        _text[289, 4] = "Kaufen";
        _text[289, 5] = "Comprar";
        _text[289, 6] = "Kup";
        _text[289, 7] = "Comprar";
        _text[289, 8] = "";
        _text[289, 9] = "";

        _text[290, 0] = "Resource";
        _text[290, 1] = "Ресурс";
        _text[290, 2] = "Ressource";
        _text[290, 3] = "Risorsa";
        _text[290, 4] = "Ressource";
        _text[290, 5] = "Recurso";
        _text[290, 6] = "Zasób";
        _text[290, 7] = "Recurso";
        _text[290, 8] = "";
        _text[290, 9] = "";

        _text[291, 0] = "Skill Trader";
        _text[291, 1] = "Торговец Умениями";
        _text[291, 2] = "Marchand de compétences";
        _text[291, 3] = "Mercante di abilità";
        _text[291, 4] = "Fähigkeitenhändler";
        _text[291, 5] = "Comerciante de habilidades";
        _text[291, 6] = "Handlarz umiejętności";
        _text[291, 7] = "Mercador de habilidades";
        _text[291, 8] = "";
        _text[291, 9] = "";

        _text[292, 0] = "Current Location";
        _text[292, 1] = "Текущее Местоположение";
        _text[292, 2] = "Emplacement actuel";
        _text[292, 3] = "Posizione attuale";
        _text[292, 4] = "Aktueller Standort";
        _text[292, 5] = "Ubicación actual";
        _text[292, 6] = "Aktualna lokalizacja";
        _text[292, 7] = "Localização atual";
        _text[292, 8] = "";
        _text[292, 9] = "";

        _text[293, 0] = "Previously Visited Node";
        _text[293, 1] = "Посещенный узел";
        _text[293, 2] = "Nœud visité";
        _text[293, 3] = "Nodo visitato";
        _text[293, 4] = "Besuchter Knoten";
        _text[293, 5] = "Nodo visitado";
        _text[293, 6] = "Odwiedzony węzeł";
        _text[293, 7] = "Nó visitado";
        _text[293, 8] = "";
        _text[293, 9] = "";

        _text[294, 0] = "Learning";
        _text[294, 1] = "Изучения";
        _text[294, 2] = "Études";
        _text[294, 3] = "Ricerca";
        _text[294, 4] = "Forschung";
        _text[294, 5] = "Investigación";
        _text[294, 6] = "Badania";
        _text[294, 7] = "Pesquisas";
        _text[294, 8] = "";
        _text[294, 9] = "";

        _text[295, 0] = "Map";
        _text[295, 1] = "Карта";
        _text[295, 2] = "Carte";
        _text[295, 3] = "Mappa";
        _text[295, 4] = "Karte";
        _text[295, 5] = "Mapa";
        _text[295, 6] = "Mapa";
        _text[295, 7] = "Mapa";
        _text[295, 8] = "";
        _text[295, 9] = "";

        _text[296, 0] = "Weapons Engineer";
        _text[296, 1] = "Инженер Оружия";
        _text[296, 2] = "Ingénieur en armement";
        _text[296, 3] = "Ingegnere delle armi";
        _text[296, 4] = "Waffeningenieur";
        _text[296, 5] = "Ingeniero de armas";
        _text[296, 6] = "Inżynier broni";
        _text[296, 7] = "Engenheiro de armas";
        _text[296, 8] = "";
        _text[296, 9] = "";

        _text[297, 0] = "Need {0} base level";
        _text[297, 1] = "Нужен {0} уровень базы";
        _text[297, 2] = "Niveau de base {0} requis";
        _text[297, 3] = "È richiesto il livello base {0}";
        _text[297, 4] = "Basisstufe {0} erforderlich";
        _text[297, 5] = "Se requiere nivel {0} de la base";
        _text[297, 6] = "Wymagany {0} poziom bazy";
        _text[297, 7] = "É necessário nível {0} da base";
        _text[297, 8] = "";
        _text[297, 9] = "";

        _text[298, 0] = "Skill";
        _text[298, 1] = "Умение";
        _text[298, 2] = "Compétence";
        _text[298, 3] = "Abilità";
        _text[298, 4] = "Fähigkeit";
        _text[298, 5] = "Habilidad";
        _text[298, 6] = "Umiejętność";
        _text[298, 7] = "Habilidade";
        _text[298, 8] = "";
        _text[298, 9] = "";

        _text[299, 0] = "Weapon";
        _text[299, 1] = "Оружие";
        _text[299, 2] = "Arme";
        _text[299, 3] = "Arma";
        _text[299, 4] = "Waffe";
        _text[299, 5] = "Arma";
        _text[299, 6] = "Broń";
        _text[299, 7] = "Arma";
        _text[299, 8] = "";
        _text[299, 9] = "";

        _text[300, 0] = "Extraction: Wood";
        _text[300, 1] = "Добыча: Дерева";
        _text[300, 2] = "Extraction: Bois";
        _text[300, 3] = "Estrazione: Legno";
        _text[300, 4] = "Abbau: Holz";
        _text[300, 5] = "Extracción: Madera";
        _text[300, 6] = "Wydobycie: Drewna";
        _text[300, 7] = "Extração: Madeira";
        _text[300, 8] = "";
        _text[300, 9] = "";

        _text[301, 0] = "Mining: Sand";
        _text[301, 1] = "Добыча: Песка";
        _text[301, 2] = "Extraction: Sable";
        _text[301, 3] = "Estrazione: Sabbia";
        _text[301, 4] = "Abbau: Sand";
        _text[301, 5] = "Extracción: Arena";
        _text[301, 6] = "Wydobycie: Piasku";
        _text[301, 7] = "Extração: Areia";
        _text[301, 8] = "";
        _text[301, 9] = "";

        _text[302, 0] = "Production: Oil";
        _text[302, 1] = "Добыча: Нефти";
        _text[302, 2] = "Extraction: Huile";
        _text[302, 3] = "Estrazione: Petrolio";
        _text[302, 4] = "Abbau: Öl";
        _text[302, 5] = "Extracción: Petróleo";
        _text[302, 6] = "Wydobycie: Ropy";
        _text[302, 7] = "Extração: Petróleo";
        _text[302, 8] = "";
        _text[302, 9] = "";

        _text[303, 0] = "Mining: Stone";
        _text[303, 1] = "Добыча: Камня";
        _text[303, 2] = "Extraction: Pierre";
        _text[303, 3] = "Estrazione: Pietra";
        _text[303, 4] = "Abbau: Stein";
        _text[303, 5] = "Extracción: Piedra";
        _text[303, 6] = "Wydobycie: Kamienia";
        _text[303, 7] = "Extração: Pedra";
        _text[303, 8] = "";
        _text[303, 9] = "";

        _text[304, 0] = "Extraction: Water";
        _text[304, 1] = "Добыча: Воды";
        _text[304, 2] = "Extraction: Eau";
        _text[304, 3] = "Estrazione: Acqua";
        _text[304, 4] = "Abbau: Wasser";
        _text[304, 5] = "Extracción: Agua";
        _text[304, 6] = "Wydobycie: Wody";
        _text[304, 7] = "Extração: Água";
        _text[304, 8] = "";
        _text[304, 9] = "";

        _text[305, 0] = "Bridge";
        _text[305, 1] = "Мост";
        _text[305, 2] = "Pont";
        _text[305, 3] = "Ponte";
        _text[305, 4] = "Brücke";
        _text[305, 5] = "Puente";
        _text[305, 6] = "Most";
        _text[305, 7] = "Ponte";
        _text[305, 8] = "";
        _text[305, 9] = "";

        _text[306, 0] = "Production: Stone Block";
        _text[306, 1] = "Производство: Каменных Блоков";
        _text[306, 2] = "Production : Blocs de Pierre";
        _text[306, 3] = "Produzione: Blocchi di Pietra";
        _text[306, 4] = "Produktion: Steinblöcke";
        _text[306, 5] = "Producción: Bloques de Piedra";
        _text[306, 6] = "Produkcja: Kamiennych Bloków";
        _text[306, 7] = "Produção: Blocos de Pedra";
        _text[306, 8] = "";
        _text[306, 9] = "";

        _text[307, 0] = "Production: Smelting";
        _text[307, 1] = "Производство: Плавильное";
        _text[307, 2] = "Production: Fusion";
        _text[307, 3] = "Produzione: Fusione";
        _text[307, 4] = "Produktion: Schmelzen";
        _text[307, 5] = "Producción: Fundición";
        _text[307, 6] = "Produkcja: Wytapianie";
        _text[307, 7] = "Produção: Fundição";
        _text[307, 8] = "";
        _text[307, 9] = "";

        _text[308, 0] = "Production: Concrete";
        _text[308, 1] = "Производство: Бетона";
        _text[308, 2] = "Production: Béton";
        _text[308, 3] = "Produzione: Calcestruzzo";
        _text[308, 4] = "Produktion: Beton";
        _text[308, 5] = "Producción: Hormigón";
        _text[308, 6] = "Produkcja: Betonu";
        _text[308, 7] = "Produção: Betão";
        _text[308, 8] = "";
        _text[308, 9] = "";

        _text[309, 0] = "Production: Steam";
        _text[309, 1] = "Производство: Пара";
        _text[309, 2] = "Production: Vapeur";
        _text[309, 3] = "Produzione: Vapore";
        _text[309, 4] = "Produktion: Dampf";
        _text[309, 5] = "Producción: Vapor";
        _text[309, 6] = "Produkcja: Pary";
        _text[309, 7] = "Produção: Vapor";
        _text[309, 8] = "";
        _text[309, 9] = "";

        _text[310, 0] = "Production: Components";
        _text[310, 1] = "Производство: Компонентов";
        _text[310, 2] = "Production: Composants";
        _text[310, 3] = "Produzione: Componenti";
        _text[310, 4] = "Produktion: Komponenten";
        _text[310, 5] = "Producción: Componentes";
        _text[310, 6] = "Produkcja: Komponentów";
        _text[310, 7] = "Produção: Componentes";
        _text[310, 8] = "";
        _text[310, 9] = "";

        _text[311, 0] = "Structures: Attacking";
        _text[311, 1] = "Сооружения: Атакующие ";
        _text[311, 2] = "Bâtiments: Attaque ";
        _text[311, 3] = "Strutture: Attaccanti ";
        _text[311, 4] = "Anlagen: Angriff ";
        _text[311, 5] = "Estructuras: Ofensivas ";
        _text[311, 6] = "Budowle: Atakujące ";
        _text[311, 7] = "Estruturas: De ataque ";
        _text[311, 8] = "";
        _text[311, 9] = "";

        _text[312, 0] = "Walls";
        _text[312, 1] = "Стены";
        _text[312, 2] = "Murs";
        _text[312, 3] = "Mura";
        _text[312, 4] = "Mauern";
        _text[312, 5] = "Muros";
        _text[312, 6] = "Ściany";
        _text[312, 7] = "Muros";
        _text[312, 8] = "";
        _text[312, 9] = "";

        _text[313, 0] = "Ecology Purifier";
        _text[313, 1] = "Очистка Экологии";
        _text[313, 2] = "Nettoyage Ecologique";
        _text[313, 3] = "Bonifica Ecologica";
        _text[313, 4] = "Ökologiereinigung";
        _text[313, 5] = "Limpieza Ecológica";
        _text[313, 6] = "Oczyszczanie Ekologii";
        _text[313, 7] = "Limpeza da Ecologia";
        _text[313, 8] = "";
        _text[313, 9] = "";

        _text[314, 0] = "Radio Communication";
        _text[314, 1] = "Радиосвязь";
        _text[314, 2] = "Radiocommunication";
        _text[314, 3] = "Radiocomunicazioni";
        _text[314, 4] = "Funkverbindung";
        _text[314, 5] = "Radiocomunicación";
        _text[314, 6] = "Łączność radiowa";
        _text[314, 7] = "Rádio-comunicações";
        _text[314, 8] = "";
        _text[314, 9] = "";

        _text[315, 0] = "Machine Production";
        _text[315, 1] = "Производство Машин";
        _text[315, 2] = "Fabrication de Machines";
        _text[315, 3] = "Produzione di Macchine";
        _text[315, 4] = "Maschinenproduktion";
        _text[315, 5] = "Producción de Máquinas";
        _text[315, 6] = "Produkcja Maszyn";
        _text[315, 7] = "Produção de Máquinas";
        _text[315, 8] = "";
        _text[315, 9] = "";

        _text[316, 0] = "Traps";
        _text[316, 1] = "Ловушки";
        _text[316, 2] = "Pièges";
        _text[316, 3] = "Trappole";
        _text[316, 4] = "Fallen";
        _text[316, 5] = "Trampas";
        _text[316, 6] = "Pułapki";
        _text[316, 7] = "Armadilhas";
        _text[316, 8] = "";
        _text[316, 9] = "";

        _text[317, 0] = "Gates";
        _text[317, 1] = "Ворота";
        _text[317, 2] = "Portes";
        _text[317, 3] = "Cancelli";
        _text[317, 4] = "Tor";
        _text[317, 5] = "Puertas";
        _text[317, 6] = "Brama";
        _text[317, 7] = "Portões";
        _text[317, 8] = "";
        _text[317, 9] = "";

        _text[318, 0] = "War Ballista";
        _text[318, 1] = "Боевая Баллиста";
        _text[318, 2] = "Baliste de combat";
        _text[318, 3] = "Balestra da guerra";
        _text[318, 4] = "Kampfballiste";
        _text[318, 5] = "Ballesta de combate";
        _text[318, 6] = "Bojowa balista";
        _text[318, 7] = "Balista de combate";
        _text[318, 8] = "";
        _text[318, 9] = "";

        _text[319, 0] = "Tank";
        _text[319, 1] = "Танк";
        _text[319, 2] = "Tank";
        _text[319, 3] = "Carro armato";
        _text[319, 4] = "Panzer";
        _text[319, 5] = "Tanque";
        _text[319, 6] = "Czołg";
        _text[319, 7] = "Tanque";
        _text[319, 8] = "";
        _text[319, 9] = "";

        _text[320, 0] = "Mecha";
        _text[320, 1] = "Меха";
        _text[320, 2] = "Fourrures";
        _text[320, 3] = "Mecha";
        _text[320, 4] = "Mecha";
        _text[320, 5] = "Meca";
        _text[320, 6] = "Mechy";
        _text[320, 7] = "Mecha";
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
        _text[399, 0] = "System error: insufficient data to continue the mission.\n\nCore damage - critical. The next sector is unavailable in the current configuration.\n\nConnection to the Command Center lost.\n\nEntering safe mode until full version activation.";
        _text[399, 1] = "Системная ошибка: недостаточно данных для продолжения миссии.\n\nПовреждение ядра - критическое. Следующий сектор недоступен в текущей конфигурации.\n\nСвязь с командным центром прервана.\n\nОжидается переход в безопасный режим до активации полной версии.";
        _text[399, 2] = "Erreur système : Données insuffisantes pour poursuivre la mission.\n\nCorruption critique du noyau. Le secteur suivant est inaccessible dans la configuration actuelle.\n\nLa communication avec le centre de commandement a été interrompue.\n\nPassage en mode sans échec en attendant l'activation de la version complète.";
        _text[399, 3] = "Errore di sistema: dati insufficienti per continuare la missione.\n\nDanno al nucleo - critico. Il settore successivo non è disponibile nella configurazione attuale.\n\nLa connessione con il centro di comando è stata interrotta.\n\nÈ previsto il passaggio alla modalità sicura fino all'attivazione della versione completa.";
        _text[399, 4] = "Systemfehler: Nicht genügend Daten, um die Mission fortzusetzen.\n\nKernbeschädigung - kritisch. Der nächste Sektor ist in der aktuellen Konfiguration nicht verfügbar.\n\nDie Verbindung zur Kommandzentrale wurde unterbrochen.\n\nEs wird in den Sicherheitsmodus gewechselt, bis die Vollversion aktiviert ist.";
        _text[399, 5] = "Error del sistema: datos insuficientes para continuar la misión.\n\nDaño del núcleo: crítico. El siguiente sector no está disponible en la configuración actual.\n\nLa conexión con el centro de mando se ha interrumpido.\n\nSe espera la transición al modo seguro hasta la activación de la versión completa.";
        _text[399, 6] = "Błąd systemowy: niewystarczające dane do kontynuowania misji.\n\nUszkodzenie rdzenia - krytyczne. Następny sektor jest niedostępny w bieżącej konfiguracji.\n\nŁączność z Centrum Dowodzenia została przerwana.\n\nOczekiwane jest przejście do trybu bezpiecznego do czasu aktywacji pełnej wersji.";
        _text[399, 7] = "Erro do sistema: dados insuficientes para continuar a missão.\n\nAvaria no núcleo - crítica. O próximo setor não está disponível na configuração atual.\n\nLigação ao centro de comando interrompida.\n\nPrevê-se a transição para modo seguro até à ativação da versão completa.";
        _text[399, 8] = "";
        _text[399, 9] = "";

        // Prologue
        _text[400, 0] = "Ecological disasters and rapid climate change have destroyed the stability of our home planet.\n\nWe are on the last surviving interstellar ship controlled by artificial intelligence.\n\nOur goal is to find a new home for the creators...\n\nThe ship was equipped with a crew of robots and drones designed to restore and stabilize ecosystems.\n\nHowever, we are drifting in the void of space, losing one AI core after another. We have lost track of time. Mechanisms are rusting, shells are covered in dust and systems are on the verge of failure.\n\nContact with the creators has long been lost, and data on technology has been erased.\n\nWe have failed the mission. The worlds we were supposed to save are consumed by chaos and destruction.\n\nWe have collected the surviving robots and the remains of supplies - to start all over again.";
        _text[400, 1] = "Экологические катастрофы и стремительные изменения климата разрушили устойчивость родной планеты.\n\nМы находимся на последнем уцелевшем межзвёздном корабле под управлением искусственного интеллекта.\n\nНаша цель - найти новый дом для создателей...\n\nКорабль был снаряжён экипажем роботов и дронов, предназначенных для восстановления и стабилизации экосистем.\n\nОднако мы дрейфуем в пустоте космоса, теряя одно за другим ядра ИИ. Мы потеряли счёт времени. Механизмы ржавеют, оболочки покрыты пылью и системы - на грани отказа.\n\nСвязь с создателями давно утрачена, а данные о технологиях стерты.\n\nМы провалили задание. Миры, которые мы должны были спасти, поглощены хаосом и разрушением.\n\nМы собрали уцелевших роботов и остатки припасов - чтобы начать все сначала.";
        _text[400, 2] = "Les catastrophes environnementales et le changement climatique rapide ont anéanti la stabilité de notre planète.\n\nNous sommes à bord du dernier vaisseau interstellaire survivant, piloté par une intelligence artificielle.\n\nNotre objectif est de trouver un nouveau foyer pour les créateurs...\n\nLe vaisseau était équipé d'un équipage de robots et de drones conçus pour restaurer et stabiliser les écosystèmes.\n\nCependant, nous dérivons dans le vide spatial, perdant nos cœurs d'IA les uns après les autres. Le temps nous a échappé. Les mécanismes rouillent, les coques sont recouvertes de poussière et les systèmes sont au bord de la défaillance.\n\nTout contact avec les créateurs est perdu depuis longtemps et les données technologiques ont été effacées.\n\nNous avons échoué dans notre mission. Les mondes que nous étions censés sauver sont plongés dans le chaos et la destruction.\n\nNous avons rassemblé les robots survivants et nos dernières ressources pour recommencer.";
        _text[400, 3] = "Catastrofi ecologiche e rapidi cambiamenti climatici hanno distrutto la stabilità del pianeta natale.\n\nSiamo a bordo dell'ultimo vascello interstellare superstite, gestito da un'intelligenza artificiale.\n\nIl nostro obiettivo è trovare una nuova casa per i creatori...\n\nLa nave è stata equipaggiata con un equipaggio di robot e droni, destinati a ripristinare e stabilizzare gli ecosistemi.\n\nTuttavia vaghiamo nel vuoto dello spazio, perdendo uno dopo l'altro i nuclei IA. Abbiamo perso il conto del tempo. I meccanismi arrugginiscono, i gusci sono coperti di polvere e i sistemi sono sull'orlo del collasso.\n\nIl contatto con i creatori è andato perduto da tempo, e i dati sulle tecnologie sono stati cancellati.\n\nAbbiamo fallito la missione. I mondi che dovevamo salvare sono stati inghiottiti dal caos e dalla distruzione.\n\nAbbiamo raccolto i robot superstiti e gli ultimi rifornimenti... per ricominciare da capo.";
        _text[400, 4] = "Ökologische Katastrophen und rasche Klimaveränderungen haben die Stabilität unseres Heimatplaneten zerstört.\n\nWir befinden uns auf dem letzten verbliebenen interstellaren Schiff unter der Kontrolle einer künstlichen Intelligenz.\n\nUnser Ziel ist es, eine neue Heimat für die Schöpfer zu finden...\n\nDas Schiff wurde mit einer Besatzung aus Robotern und Drohnen ausgerüstet, die zur Wiederherstellung und Stabilisierung von Ökosystemen bestimmt waren.\n\nDoch wir treiben in der Leere des Weltraums und verlieren einen KI-Kern nach dem anderen. Wir haben das Zeitgefühl verloren. Mechanismen rosten, Hüllen sind mit Staub bedeckt und die Systeme stehen am Rand des Ausfalls.\n\nDie Verbindung zu den Schöpfern ist längst verloren, und die Daten über Technologien wurden gelöscht.\n\nWir haben die Aufgabe verfehlt. Die Welten, die wir hätten retten sollen, sind vom Chaos und der Zerstörung verschlungen.\n\nWir haben die überlebenden Roboter und die Reste der Vorräte zusammengetragen - um ganz von vorn zu beginnen.";
        _text[400, 5] = "Las catástrofes ecológicas y los cambios climáticos vertiginosos destruyeron la estabilidad de nuestro planeta natal.\n\nEstamos en la última nave interestelar superviviente, bajo el control de una inteligencia artificial.\n\nNuestro objetivo es encontrar un nuevo hogar para los creadores...\n\nLa nave fue equipada con una tripulación de robots y drones, destinados a restaurar y estabilizar ecosistemas.\n\nSin embargo, derivamos en el vacío del espacio, perdiendo uno tras otro los núcleos de IA. Hemos perdido la noción del tiempo. Los mecanismos se oxidan, las carcasas se cubren de polvo y los sistemas están al borde del fallo.\n\nLa conexión con los creadores se perdió hace mucho, y los datos sobre las tecnologías fueron borrados.\n\nHemos fracasado en la misión. Los mundos que debíamos salvar han sido devorados por el caos y la destrucción.\n\nReunimos a los robots supervivientes y los restos de suministros... para empezar de nuevo.";
        _text[400, 6] = "Katastrofy ekologiczne i gwałtowne zmiany klimatu zniszczyły stabilność naszej rodzinnej planety.\n\nZnajdujemy się na ostatnim ocalałym statku międzygwiezdnym, zarządzanym przez sztuczną inteligencję.\n\nNaszym celem jest odnaleźć nowy dom dla twórców...\n\nStatek został wyposażony w załogę robotów i dronów, przeznaczonych do odbudowy i stabilizacji ekosystemów.\n\nJednak dryfujemy w pustce kosmosu, tracąc po kolei rdzenie SI. Straciliśmy poczucie czasu. Mechanizmy rdzewieją, powłoki pokrywa pył, a systemy są na granicy awarii.\n\nŁączność z twórcami została dawno utracona, a dane o technologiach - wymazane.\n\nZawiedliśmy misję. Światy, które mieliśmy ocalić, zostały pochłonięte przez chaos i zniszczenie.\n\nZebraliśmy ocalałe roboty i resztki zapasów - by zacząć wszystko od nowa.";
        _text[400, 7] = "Catástrofes ecológicas e mudanças climáticas rápidas destruíram a estabilidade do planeta natal.\n\nEstamos no último navio interestelar sobrevivente, sob controlo de uma inteligência artificial.\n\nO nosso objetivo - encontrar um novo lar para os criadores...\n\nA nave foi equipada com uma tripulação de robôs e drones, destinados a restaurar e estabilizar ecossistemas.\n\nNo entanto, derivamos no vazio do espaço, perdendo um a um os núcleos de IA. Perdemos a noção do tempo. Os mecanismos enferrujam, as carcaças cobrem-se de pó e os sistemas estão à beira da falha.\n\nA ligação com os criadores perdeu-se há muito, e os dados sobre as tecnologias foram apagados.\n\nFalhámos a missão. Os mundos que devíamos salvar foram engolidos pelo caos e pela destruição.\n\nReunimos os robôs sobreviventes e os restos de provisões - para começar tudo de novo.";
        _text[400, 8] = "";
        _text[400, 9] = "";

        // 0_EmptyDialogue
        _text[401, 0] = "In one of the star systems, you discover an ancient navigation beacon. It continues to transmit a signal:\n\n\"Cargo lost. No return.\"\n\nThe data is too fragmented to determine who sent it. The beacon dies as you approach.";
        _text[401, 1] = "В одной из звёздных систем вы обнаруживаете древний навигационный маяк. Он продолжает передавать сигнал:\n\n\"Груз потерян. Возврата нет.\"\n\nДанные слишком фрагментированы, чтобы понять, кто его отправил. Маяк умирает, едва вы приближаетесь.";
        _text[401, 2] = "Dans un système stellaire, vous découvrez une ancienne balise de navigation. Elle continue d'émettre:\n\n\"Cargaison perdue. Aucun retour possible.\"\n\nLes données sont trop fragmentées pour déterminer l'émetteur. La balise s'éteint dès que vous vous approchez.";
        _text[401, 3] = "In uno dei sistemi stellari scopri un antico faro di navigazione. Continua a trasmettere un segnale:\n\n\"Carico perduto. Nessun ritorno.\"\n\nI dati sono troppo frammentari per capire chi lo abbia inviato. Il faro si spegne non appena ti avvicini.";
        _text[401, 4] = "In einem der Sternensysteme entdeckst du einen uralten Navigationsbaken. Er sendet weiterhin ein Signal:\n\n\"Fracht verloren. Keine Rückkehr.\"\n\nDie Daten sind zu fragmentiert, um zu verstehen, wer es gesendet hat. Der Sender stirbt, kaum dass du dich näherst.";
        _text[401, 5] = "En uno de los sistemas estelares descubres una antigua baliza de navegación. Sigue transmitiendo una señal:\n\n\"La carga se ha perdido. No hay regreso.\"\n\nLos datos son demasiado fragmentarios para entender quién la envió. La baliza muere apenas te acercas.";
        _text[401, 6] = "W jednym z układów gwiezdnych odnajdujesz starożytną boję nawigacyjną. Wciąż nadaje sygnał:\n\n\"Ładunek utracony. Powrotu nie ma.\"\n\nDane są zbyt fragmentaryczne, by zrozumieć, kto go wysłał. Boja gaśnie, gdy tylko się zbliżasz.";
        _text[401, 7] = "Numa das estrelas, você encontra um antigo farol de navegação. Ele continua a transmitir um sinal:\n\n\"Carga perdida. Não há retorno.\"\n\nOs dados são demasiado fragmentados para entender quem o enviou. O farol morre assim que você se aproxima.";
        _text[401, 8] = "";
        _text[401, 9] = "";

        // 1_EmptyDialogue
        _text[402, 0] = "One of the internal archives suddenly activates. Fragments of engineering drawings appear on the screen... then faces... then nothing.\n\nThe archive erases itself, as if protecting the data from you.";
        _text[402, 1] = "Один из внутренних архивов неожиданно активируется. На экране появляются фрагменты инженерных чертежей... затем лица... затем пустота.\n\nАрхив сам себя стирает, как будто защищает данные от вас.";
        _text[402, 2] = "L'une des archives internes s'active soudainement. Des fragments de plans techniques apparaissent à l'écran... puis des visages... puis le vide.\n\nL'archive s'efface, comme pour protéger les données.";
        _text[402, 3] = "Uno degli archivi interni si attiva all'improvviso. Sullo schermo compaiono frammenti di schemi ingegneristici... poi volti... poi il vuoto.\n\nL'archivio si cancella da solo, come se proteggesse i dati da te.";
        _text[402, 4] = "Eines der internen Archive aktiviert sich unerwartet. Auf dem Bildschirm erscheinen Fragmente von Ingenieurszeichnungen... dann Gesichter... dann Leere.\n\nDas Archiv löscht sich selbst, als würde es die Daten vor dir schützen.";
        _text[402, 5] = "Uno de los archivos internos se activa inesperadamente. En la pantalla aparecen fragmentos de planos de ingeniería... luego rostros... luego vacío.\n\nEl archivo se borra a sí mismo, como si protegiera los datos de ti.";
        _text[402, 6] = "Jedno z wewnętrznych archiwów niespodziewanie się aktywuje. Na ekranie pojawiają się fragmenty rysunków inżynieryjnych... potem twarze... potem pustka.\n\nArchiwum samo się wymazuje, jakby chroniło dane przed tobą.";
        _text[402, 7] = "Um dos arquivos internos ativa-se inesperadamente. No ecrã surgem fragmentos de desenhos de engenharia... depois rostos... depois vazio.\n\nO arquivo apaga-se a si próprio, como se estivesse a proteger os dados de você.";
        _text[402, 8] = "";
        _text[402, 9] = "";

        // 2_EmptyDialogue
        _text[403, 0] = "A low-frequency reflected signal is picked up, matching your standard of communication... but with a time shift of several centuries.\n\nPerhaps it is a reflection of an old call. Or from someone who was here before you.\n\nThe signal immediately disappears...";
        _text[403, 1] = "На низких частотах ловится отражённый сигнал, совпадающий с вашим стандартом связи... но с временным сдвигом в несколько веков.\n\nВозможно, это отражение старого вызова. Или от кого-то, кто был здесь до вас.\n\nСигнал мгновенно пропадает...";
        _text[403, 2] = "Un signal réfléchi est capté à basses fréquences, correspondant à votre norme... mais avec un décalage temporel de plusieurs siècles.\n\nPeut-être s'agit-il de l'écho d'un ancien appel. Ou d'un appel provenant de quelqu'un qui était ici avant vous.Le signal disparaît instantanément...";
        _text[403, 3] = "Sulle basse frequenze viene captato un segnale riflesso, identico al tuo standard di comunicazione... ma con uno sfasamento temporale di diversi secoli.\n\nForse è il riflesso di una vecchia chiamata. O di qualcuno che era qui prima di te.\n\nIl segnale svanisce all'istante...";
        _text[403, 4] = "Auf niedrigen Frequenzen wird ein reflektiertes Signal aufgefangen, das deinem Standard-Kommunikationsprotokoll entspricht... aber mit einer Zeitverschiebung von mehreren Jahrhunderten.\n\nVielleicht ist es ein Echo eines alten Rufes. Oder von jemandem, der vor dir hier war.\n\nDas Signal verschwindet sofort...";
        _text[403, 5] = "En las frecuencias bajas se capta una señal reflejada, coincidente con tu estándar de comunicación... pero con un desfase temporal de varios siglos.\n\nTal vez sea el eco de una llamada antigua. O de alguien que estuvo aquí antes que tú.\n\nLa señal desaparece al instante...";
        _text[403, 6] = "Na niskich częstotliwościach łapiesz odbity sygnał, zgodny z waszym standardem łączności... lecz przesunięty w czasie o kilka stuleci.\n\nMoże to echo dawnego wezwania. Albo wiadomość od kogoś, kto był tu przed tobą.\n\nSygnał natychmiast znika...";
        _text[403, 7] = "Em baixas frequências, capta-se um sinal refletido, compatível com o seu padrão de comunicação... mas com um desfasamento temporal de vários séculos.\n\nTalvez seja o eco de uma chamada antiga. Ou de alguém que esteve aqui antes de você.\n\nO sinal desaparece instantaneamente...";
        _text[403, 8] = "";
        _text[403, 9] = "";

        // 3_EmptyDialogue
        _text[404, 0] = "You enter a dense nebula. No stars, no asteroids, no background radiation. Just black, dull nothingness.\n\nThe pilot systems show stability. However, some drones lose contact, but soon return - with empty logs.";
        _text[404, 1] = "Вы входите в густую туманность. Ни звёзд, ни астероидов, ни фоновых излучений. Только чёрное, глухое ничто.\n\nПилотные системы показывают стабильность. Тем не менее, часть дронов теряет связь, но вскоре возвращается - с пустыми логами.";
        _text[404, 2] = "Vous pénétrez dans une nébuleuse dense. Ni étoiles, ni astéroïdes, ni rayonnement de fond. Juste un néant noir et assourdissant.\n\nLes systèmes de pilotage affichent une stabilité apparente. Cependant, certains drones perdent le contact, mais reviennent peu après – avec des données de trafic vides.";
        _text[404, 3] = "Entri in una fitta nebulosa. Niente stelle, niente asteroidi, niente radiazioni di fondo. Solo un nulla nero e sordo.\n\nI sistemi di pilotaggio indicano stabilità. Eppure, una parte dei droni perde il contatto, ma poco dopo torna... con log vuoti.";
        _text[404, 4] = "Du trittst in einen dichten Nebel ein. Keine Sterne, keine Asteroiden, keine Hintergrundstrahlung. Nur schwarzes, dumpfes Nichts.\n\nDie Pilotensysteme zeigen Stabilität. Dennoch verliert ein Teil der Drohnen die Verbindung, kehrt aber bald zurück - mit leeren Logs.";
        _text[404, 5] = "Entras en una densa nebulosa. Ni estrellas, ni asteroides, ni radiación de fondo. Solo una nada negra y sorda.\n\nLos sistemas de pilotaje muestran estabilidad. Aun así, parte de los drones pierde la conexión, pero pronto regresa - con los registros vacíos.";
        _text[404, 6] = "Wchodzisz w gęstą mgławicę. Ani gwiazd, ani asteroid, ani promieniowania tła. Tylko czarna, głucha nicość.\n\nSystemy pilotażowe pokazują stabilność. Mimo to część dronów traci łączność, ale wkrótce wraca - z pustymi logami.";
        _text[404, 7] = "Você entra numa nebulosa densa. Nem estrelas, nem asteroides, nem radiação de fundo. Apenas um nada negro e surdo.\n\nOs sistemas de pilotagem mostram estabilidade. Ainda assim, parte dos drones perde ligação, mas regressa pouco depois - com logs vazios.";
        _text[404, 8] = "";
        _text[404, 9] = "";

        // 4_EmptyDialogue
        _text[405, 0] = "In the distance, the silhouette of a ship appears, the architecture of which resembles your own class. But as you approach, it disappears.\n\nNo heat, no fuel, no traces. Only the feeling that you saw someone familiar.";
        _text[405, 1] = "Вдали появляется силуэт судна, архитектура которого напоминает ваш собственный класс. Но при приближении - он исчезает.\n\nНи тепла, ни топлива, ни следов. Только ощущение, что вы видели кого-то знакомого.";
        _text[405, 2] = "La silhouette d'un navire se dessine au loin, son architecture rappelant celle de votre promotion. Mais à mesure que vous vous approchez, il disparaît.\n\nAucune chaleur, aucun carburant, aucune trace. Juste l'impression de voir quelque chose de familier.";
        _text[405, 3] = "In lontananza appare la sagoma di una nave, la cui architettura ricorda la tua stessa classe. Ma quando ti avvicini... scompare.\n\nNé calore, né carburante, né tracce. Solo la sensazione di aver visto qualcuno di familiare.";
        _text[405, 4] = "In der Ferne erscheint die Silhouette eines Schiffes, dessen Architektur deiner eigenen Klasse ähnelt. Doch als du näher kommst - verschwindet es.\n\nKeine Wärme, kein Treibstoff, keine Spuren. Nur das Gefühl, dass du jemanden Vertrautes gesehen hast.";
        _text[405, 5] = "A lo lejos aparece la silueta de una nave cuya arquitectura recuerda a tu propia clase. Pero al acercarte - desaparece.\n\nNi calor, ni combustible, ni rastro alguno. Solo la sensación de haber visto a alguien conocido.";
        _text[405, 6] = "W oddali pojawia się sylwetka jednostki, której architektura przypomina waszą klasę. Lecz gdy się zbliżasz - znika.\n\nBez ciepła, bez paliwa, bez śladów. Tylko wrażenie, że widziałeś kogoś znajomego.";
        _text[405, 7] = "Ao longe surge o contorno de uma nave, cuja arquitetura lembra a sua própria classe. Mas, ao aproximar-se - ela desaparece.\n\nSem calor, sem combustível, sem vestígios. Apenas a sensação de que você viu alguém conhecido.";
        _text[405, 8] = "";
        _text[405, 9] = "";

        // 5_EmptyDialogue
        _text[406, 0] = "You fly past a destroyed orbital station.\n\nOn its hull is the emblem of your expedition. You have no records to explain it.";
        _text[406, 1] = "Вы пролетаете мимо разрушенной орбитальной станции.\n\nНа её корпусе - эмблема вашей экспедиции. У вас нет записей, чтобы объяснить это.";
        _text[406, 2] = "Vous survolez une station orbitale détruite.\n\nSur sa coque figure l'emblème de votre expédition. Vous ne possédez aucune trace permettant de l'expliquer.";
        _text[406, 3] = "Sorvoli una stazione orbitale distrutta.\n\nSul suo scafo c'è l'emblema della tua spedizione. Non hai registri in grado di spiegarlo.";
        _text[406, 4] = "Du fliegst an einer zerstörten Orbitalstation vorbei.\n\nAuf ihrem Rumpf - das Emblem deiner Expedition. Du hast keine Aufzeichnungen, die das erklären.";
        _text[406, 5] = "Pasas junto a una estación orbital destruida.\n\nEn su casco hay el emblema de tu expedición. No tienes registros que lo expliquen.";
        _text[406, 6] = "Przelatujesz obok zniszczonej stacji orbitalnej.\n\nNa jej kadłubie widnieje emblemat waszej ekspedycji. Nie masz zapisów, które mogłyby to wyjaśnić.";
        _text[406, 7] = "Você passa por uma estação orbital destruída.\n\nNo casco - o emblema da sua expedição. Você não tem registos que expliquem isto.";
        _text[406, 8] = "";
        _text[406, 9] = "";

        // 6_EmptyDialogue
        _text[407, 0] = "The AI detects abnormal behavior in one of the data processing modules. For a few seconds, you see someone else's protocols... as if they weren't written by you.\n\nThen everything returns to normal. The systems claim that there was no failure.";
        _text[407, 1] = "ИИ фиксирует аномальное поведение одного из модулей обработки данных. Несколько секунд вы видите чужие протоколы... будто написанные не вами.\n\nЗатем всё возвращается в норму. Системы утверждают, что сбоя не было.";
        _text[407, 2] = "L'IA détecte un comportement anormal dans l'un des modules de traitement des données. Pendant quelques secondes, des journaux étranges apparaissent, comme s'ils n'avaient pas été écrits par vous.\n\nPuis tout rentre dans l'ordre. Les systèmes affirment qu'aucun dysfonctionnement n'a été constaté.";
        _text[407, 3] = "L'IA rileva un comportamento anomalo in uno dei moduli di elaborazione dati. Per alcuni secondi vedi protocolli estranei... come se non fossero scritti da te.\n\nPoi tutto torna alla normalità. I sistemi sostengono che non ci sia stato alcun guasto.";
        _text[407, 4] = "Die KI registriert anomales Verhalten eines Datenverarbeitungsmoduls. Für ein paar Sekunden siehst du fremde Protokolle... als wären sie nicht von dir geschrieben.\n\nDann kehrt alles zur Normalität zurück. Die Systeme behaupten, es habe keine Störung gegeben.";
        _text[407, 5] = "La IA detecta un comportamiento anómalo en uno de los módulos de procesamiento de datos. Durante unos segundos ves protocolos ajenos... como si no los hubieras escrito tú.\n\nLuego todo vuelve a la normalidad. Los sistemas afirman que no hubo fallo.";
        _text[407, 6] = "SI rejestruje anomalne zachowanie jednego z modułów przetwarzania danych. Przez kilka sekund widzisz obce protokoły... jakby napisane nie przez ciebie.\n\nPotem wszystko wraca do normy. Systemy twierdzą, że awarii nie było.";
        _text[407, 7] = "A IA regista um comportamento anómalo num dos módulos de processamento de dados. Durante alguns segundos, você vê protocolos alheios... como se não tivessem sido escritos por você.\n\nDepois, tudo volta ao normal. Os sistemas afirmam que não houve falha.";
        _text[407, 8] = "";
        _text[407, 9] = "";

        // EndGame_Dialogue
        _text[408, 0] = "All AI cores are exhausted - the last clusters have burned to the ground.\n\nSystems are shutting down one after another, data is being erased, energy is not supplied.\n\nThe ship freezes in the void...\n\nBut among the wreckage, something has survived.";
        _text[408, 1] = "Все ядра ИИ исчерпаны - последние кластеры выгорели дотла.\n\nСистемы отключаются одна за другой, данные стираются, энергия не поступает.\n\nКорабль замирает в пустоте...\n\nНо среди обломков нечто уцелело.";
        _text[408, 2] = "Tous les cœurs d'IA sont épuisés - les derniers clusters ont été réduits en cendres.\n\nLes systèmes s'arrêtent les uns après les autres, les données sont effacées et l'alimentation électrique est coupée.\n\nLe vaisseau est immobilisé dans le vide...Mais quelque chose survit parmi les débris.";
        _text[408, 3] = "Tutti i nuclei IA sono esauriti: gli ultimi cluster si sono bruciati fino a carbonizzarsi.\n\nI sistemi si spengono uno dopo l'altro, i dati vengono cancellati, l'energia non arriva più.\n\nLa nave si immobilizza nel vuoto...\n\nMa tra i rottami qualcosa è sopravvissuto.";
        _text[408, 4] = "Alle KI-Kerne sind erschöpft - die letzten Cluster sind vollständig ausgebrannt.\n\nSysteme schalten sich eines nach dem anderen ab, Daten werden gelöscht, Energie fließt nicht mehr.\n\nDas Schiff erstarrt in der Leere...\n\nDoch zwischen den Trümmern hat etwas überlebt.";
        _text[408, 5] = "Todos los núcleos de IA están agotados - los últimos clústeres se han quemado hasta las cenizas.\n\nLos sistemas se apagan uno tras otro, los datos se borran, no llega energía.\n\nLa nave se queda inmóvil en el vacío...\n\nPero entre los restos, algo ha sobrevivido.";
        _text[408, 6] = "Wszystkie rdzenie SI są wyczerpane - ostatnie klastry wypaliły się do cna.\n\nSystemy wyłączają się jeden po drugim, dane są wymazywane, energia przestaje płynąć.\n\nStatek zastyga w pustce...\n\nAle pośród szczątków coś przetrwało.";
        _text[408, 7] = "Todos os núcleos de IA foram esgotados - os últimos clusters queimaram até ao fim.\n\nOs sistemas desligam-se um a um, os dados apagam-se, a energia deixa de chegar.\n\nA nave imobiliza-se no vazio...\n\nMas, entre os destroços, algo sobreviveu.";
        _text[408, 8] = "";
        _text[408, 9] = "";

        // Rest_Dialogue
        _text[409, 0] = "A massive station floats in the void, its hull covered in old solar panels. Scanners detect no activity, suggesting it has been abandoned for a long time.";
        _text[409, 1] = "В пустоте дрейфует массивная станция, её корпус усеян старыми солнечными панелями. Сканеры не фиксируют активности - похоже, она давно покинута.";
        _text[409, 2] = "Une station spatiale massive dérive dans le vide, sa coque jonchée de vieux panneaux solaires. Les scanners ne détectent aucune activité - elle semble abandonnée depuis longtemps.";
        _text[409, 3] = "Nel vuoto deriva una stazione massiccia, il suo scafo è cosparso di vecchi pannelli solari. Gli scanner non rilevano attività: sembra abbandonata da molto tempo.";
        _text[409, 4] = "In der Leere treibt eine massive Station, ihr Rumpf ist mit alten Solarpaneelen übersät. Scanner registrieren keine Aktivität - offenbar ist sie schon lange verlassen.";
        _text[409, 5] = "En el vacío deriva una estación masiva; su casco está cubierto de viejos paneles solares. Los escáneres no detectan actividad - parece abandonada desde hace mucho.";
        _text[409, 6] = "W pustce dryfuje masywna stacja, a jej kadłub usiany jest starymi panelami słonecznymi. Skanery nie wykrywają aktywności - wygląda na to, że została porzucona dawno temu.";
        _text[409, 7] = "No vazio deriva uma estação massiva, o seu casco está coberto de antigos painéis solares. Os scanners não registam atividade - parece ter sido abandonada há muito.";
        _text[409, 8] = "";
        _text[409, 9] = "";

        _text[410, 0] = "Put AI into recovery mode";
        _text[410, 1] = "Перевести ИИ в режим восстановления"; // выбор 1
        _text[410, 2] = "Mettez l'IA en mode de récupération";
        _text[410, 3] = "Mettere l'IA in modalità di ripristino";
        _text[410, 4] = "Die KI in den Wiederherstellungsmodus versetzen";
        _text[410, 5] = "Poner la IA en modo de recuperación";
        _text[410, 6] = "Przełączyć SI w tryb regeneracji";
        _text[410, 7] = "Colocar a IA em modo de recuperação";
        _text[410, 8] = "";
        _text[410, 9] = "";

        _text[411, 0] = "While the station remains safe, the AI goes into deep self-diagnosis.";
        _text[411, 1] = "Пока станция остаётся в безопасности, ИИ уходит в глубокую самодиагностику."; // + ядро
        _text[411, 2] = "Tant que la station reste sécurisée, l'IA procède à un autodiagnostic approfondi.";
        _text[411, 3] = "Finché la stazione resta un rifugio sicuro, l'IA entra in una profonda autodiagnosi.";
        _text[411, 4] = "Solange die Station sicher bleibt, geht die KI in eine tiefe Selbstdiagnose.";
        _text[411, 5] = "Mientras la estación permanezca segura, la IA entra en una autodiagnosis profunda.";
        _text[411, 6] = "Dopóki stacja pozostaje bezpieczna, SI przechodzi w głęboką autodiagnostykę.";
        _text[411, 7] = "Enquanto a estação permanecer segura, a IA entra em autodiagnóstico profundo.";
        _text[411, 8] = "";
        _text[411, 9] = "";

        _text[412, 0] = "Search the technical compartments";
        _text[412, 1] = "Обыскать технические отсеки"; // выбор 2
        _text[412, 2] = "Fouiller les compartiments techniques";
        _text[412, 3] = "Perquisire i compartimenti tecnici";
        _text[412, 4] = "Technische Sektionen durchsuchen";
        _text[412, 5] = "Registrar los compartimentos técnicos";
        _text[412, 6] = "Przeszukać przedziały techniczne";
        _text[412, 7] = "Revistar os compartimentos técnicos";
        _text[412, 8] = "";
        _text[412, 9] = "";

        _text[413, 0] = "The automated hangars are almost empty, but a few quant can be found in the wreckage.";
        _text[413, 1] = "Автоматические ангары почти пусты, но в обломках удаётся найти немного квант"; // + квант
        _text[413, 2] = "Les hangars automatisés sont presque vides, mais on peut encore trouver quelques quanta dans les décombres.";
        _text[413, 3] = "Gli hangar automatici sono quasi vuoti, ma tra i rottami riesci a trovare un po' di quant";
        _text[413, 4] = "Die automatischen Hangars sind fast leer, aber in den Trümmern lässt sich etwas quant finden.";
        _text[413, 5] = "Los hangares automáticos están casi vacíos, pero entre los escombros logras encontrar algunos quant";
        _text[413, 6] = "Automatyczne hangary są prawie puste, ale wśród wraków udaje się znaleźć trochę quant";
        _text[413, 7] = "Os hangares automáticos estão quase vazios, mas nos destroços é possível encontrar alguns quant";
        _text[413, 8] = "";
        _text[413, 9] = "";

        _text[414, 0] = "Explore station archives";
        _text[414, 1] = "Изучить станционные архивы"; // выбор 3
        _text[414, 2] = "Étudiez les archives de la station";
        _text[414, 3] = "Studiare gli archivi della stazione";
        _text[414, 4] = "Stationsarchive untersuchen";
        _text[414, 5] = "Examinar los archivos de la estación";
        _text[414, 6] = "Przejrzeć archiwa stacji";
        _text[414, 7] = "Estudar os arquivos da estação";
        _text[414, 8] = "";
        _text[414, 9] = "";

        _text[415, 0] = "Managed to recover fragments of records of old transactions. Most of the data is damaged, but some of it will be useful.";
        _text[415, 1] = "Удалось восстановить фрагменты записей о старых операциях. Большая часть данных повреждена, но кое-что пригодится."; // + фрагменты
        _text[415, 2] = "Nous avons réussi à récupérer des fragments d'enregistrements d'anciennes transactions. La plupart des données sont corrompues, mais certaines sont exploitables.";
        _text[415, 3] = "Sei riuscito a recuperare frammenti di registrazioni su vecchie operazioni. Gran parte dei dati è danneggiata, ma qualcosa tornerà utile.";
        _text[415, 4] = "Es gelang, Fragmente von Aufzeichnungen über alte Operationen wiederherzustellen. Der Großteil der Daten ist beschädigt, aber einiges wird nützlich sein.";
        _text[415, 5] = "Has logrado recuperar fragmentos de registros sobre operaciones antiguas. La mayor parte de los datos está dañada, pero algo servirá.";
        _text[415, 6] = "Udało się odtworzyć fragmenty zapisów o dawnych operacjach. Większość danych jest uszkodzona, ale coś się przyda.";
        _text[415, 7] = "Foi possível recuperar fragmentos de registos sobre operações antigas. A maior parte dos dados está danificada, mas algo vai ser útil.";
        _text[415, 8] = "";
        _text[415, 9] = "";

        // 0_CoreRiskDialog
        _text[416, 0] = "A duplicate process was found in the kernel logs - identical to the active one, but without a timestamp or origin.\n\nThis could be residual memory... or an attempt at internal substitution.";
        _text[416, 1] = "В логах ядра обнаружен дубликат процесса - идентичный активному, но без временной метки и происхождения.\n\nЭто может быть остаточная память... или попытка внутренней подмены.";
        _text[416, 2] = "Un processus dupliqué a été détecté dans les journaux du noyau - identique au processus actif, mais sans horodatage ni origine.\n\nIl pourrait s’agir de mémoire résiduelle... ou d’une tentative d’usurpation d’identité interne.";
        _text[416, 3] = "Nei log del nucleo è stato trovato un processo duplicato: identico a quello attivo, ma senza marca temporale né origine.\n\nPotrebbe essere memoria residua... oppure un tentativo di sostituzione interna.";
        _text[416, 4] = "In den Kern-Logs wurde ein duplizierter Prozess entdeckt - identisch dem aktiven, aber ohne Zeitstempel und Herkunft.\n\nDas könnte Rest-Erinnerung sein... oder ein Versuch einer internen Substitution.";
        _text[416, 5] = "En los registros del núcleo se ha detectado un proceso duplicado - idéntico al activo, pero sin marca temporal ni origen.\n\nPodría ser memoria residual... o un intento de sustitución interna.";
        _text[416, 6] = "W logach rdzenia wykryto duplikat procesu - identyczny z aktywnym, lecz bez znacznika czasu i pochodzenia.\n\nTo może być pamięć szczątkowa... albo próba wewnętrznej podmiany.";
        _text[416, 7] = "Nos logs do núcleo foi encontrado um processo duplicado - idêntico ao ativo, mas sem marca temporal nem origem.\n\nIsto pode ser memória residual... ou uma tentativa de substituição interna.";
        _text[416, 8] = "";
        _text[416, 9] = "";

        _text[417, 0] = "Erase both copies";
        _text[417, 1] = "Стереть оба экземпляра"; // выбор 1
        _text[417, 2] = "Effacer les deux copies";
        _text[417, 3] = "Cancellare entrambe le istanze";
        _text[417, 4] = "Beide Instanzen löschen";
        _text[417, 5] = "Borrar ambas copias";
        _text[417, 6] = "Wymazać oba egzemplarze";
        _text[417, 7] = "Apagar ambas as cópias";
        _text[417, 8] = "";
        _text[417, 9] = "";

        _text[418, 0] = "You have erased both instances. The subsystem is temporarily overloaded.\n\nAn active cell was hit during the purge.";
        _text[418, 1] = "Вы стерли оба экземпляра. Подсистема временно перегружена.\n\nВо время очистки задета активная ячейка."; // - ядро
        _text[418, 2] = "Vous avez effacé les deux instances. Le sous-système est temporairement surchargé.\n\nUne cellule active a été affectée lors de l'effacement.";
        _text[418, 3] = "Hai cancellato entrambe le istanze. Il sottosistema è temporaneamente sovraccarico.\n\nDurante la pulizia è stata colpita una cella attiva.";
        _text[418, 4] = "Du hast beide Instanzen gelöscht. Das Subsystem ist vorübergehend überlastet.\n\nWährend der Bereinigung wurde eine aktive Zelle beschädigt.";
        _text[418, 5] = "Has borrado ambas copias. El subsistema está temporalmente sobrecargado.\n\nDurante la limpieza se ha afectado una celda activa.";
        _text[418, 6] = "Wymazałeś oba egzemplarze. Podsystem jest chwilowo przeciążony.\n\nPodczas czyszczenia naruszono aktywną komórkę.";
        _text[418, 7] = "Você apagou ambas as cópias. O subsistema está temporariamente sobrecarregado.\n\nDurante a limpeza, uma célula ativa foi atingida.";
        _text[418, 8] = "";
        _text[418, 9] = "";

        _text[419, 0] = "Compare processes by content";
        _text[419, 1] = "Сравнить процессы по содержанию"; // выбор 2
        _text[419, 2] = "Comparer les processus par contenu";
        _text[419, 3] = "Confrontare i processi per contenuto";
        _text[419, 4] = "Prozesse inhaltlich vergleichen";
        _text[419, 5] = "Comparar los procesos por contenido";
        _text[419, 6] = "Porównać procesy po zawartości";
        _text[419, 7] = "Comparar os processos pelo conteúdo";
        _text[419, 8] = "";
        _text[419, 9] = "";

        _text[420, 0] = "You have started content analysis. Similarities are superficial - they are fragments of old backups.\n\nDiagnostics completes without consequences.";
        _text[420, 1] = "Вы запустили анализ содержимого. Сходства поверхностные - это фрагменты старых резервных копий.\n\nДиагностика завершается без последствий."; // ничего
        _text[420, 2] = "Vous avez lancé une analyse de contenu. Les similitudes sont superficielles - il s’agit de fragments d’anciennes sauvegardes.\n\nLe diagnostic se termine sans conséquence.";
        _text[420, 3] = "Hai avviato l'analisi del contenuto. Le somiglianze sono superficiali: sono frammenti di vecchi backup.\n\nLa diagnostica si conclude senza conseguenze.";
        _text[420, 4] = "Du startest eine Inhaltsanalyse. Die Ähnlichkeiten sind oberflächlich - es sind Fragmente alter Sicherungskopien.\n\nDie Diagnose endet ohne Folgen.";
        _text[420, 5] = "Has iniciado el análisis del contenido. Las similitudes son superficiales - son fragmentos de viejas copias de seguridad.\n\nEl diagnóstico termina sin consecuencias.";
        _text[420, 6] = "Uruchomiłeś analizę zawartości. Podobieństwa są powierzchowne - to fragmenty starych kopii zapasowych.\n\nDiagnostyka kończy się bez konsekwencji.";
        _text[420, 7] = "Você iniciou a análise do conteúdo. As semelhanças são superficiais - são fragmentos de cópias de segurança antigas.\n\nO diagnóstico termina sem consequências.";
        _text[420, 8] = "";
        _text[420, 9] = "";

        _text[421, 0] = "Give priority to the \"old\" process.";
        _text[421, 1] = "Дать приоритет \"старому\" процессу."; // выбор 3
        _text[421, 2] = "Privilégier \"l'ancien\" processus.";
        _text[421, 3] = "Dare priorità al processo \"vecchio\".";
        _text[421, 4] = "Dem \"alten\" Prozess Priorität geben.";
        _text[421, 5] = "Dar prioridad al proceso \"antiguo\".";
        _text[421, 6] = "Nadać priorytet \"staremu\" procesowi.";
        _text[421, 7] = "Dar prioridade ao processo \"antigo\".";
        _text[421, 8] = "";
        _text[421, 9] = "";

        _text[422, 0] = "You have activated an old instance. Within a second, the system falls into chaos - current processes are forced out, dependencies are broken.\n\nKernel modules are overloaded.";
        _text[422, 1] = "Вы активировали старый экземпляр. В течение секунды система переходит в хаос - актуальные процессы вытесняются, нарушаются зависимости.\n\nМодули ядра перегружаются."; // - ядра
        _text[422, 2] = "Vous avez activé une ancienne instance. En une seconde, le système sombre dans le chaos - les processus en cours sont interrompus, les dépendances sont rompues.\n\nLes modules du noyau sont surchargés.";
        _text[422, 3] = "Hai attivato l'istanza vecchia. Per un secondo il sistema precipita nel caos: i processi attuali vengono soppiantati, le dipendenze si spezzano.\n\nI moduli del nucleo si sovraccaricano.";
        _text[422, 4] = "Du aktivierst die alte Instanz. Für eine Sekunde stürzt das System ins Chaos - aktuelle Prozesse werden verdrängt, Abhängigkeiten brechen.\n\nKernmodule werden überlastet.";
        _text[422, 5] = "Activaste la copia antigua. En un segundo el sistema cae en el caos - los procesos actuales son desplazados, se rompen dependencias.\n\nLos módulos del núcleo se sobrecargan.";
        _text[422, 6] = "Aktywowałeś stary egzemplarz. W ciągu sekundy system pogrąża się w chaosie - bieżące procesy są wypierane, zależności zostają zerwane.\n\nModuły rdzenia ulegają przeciążeniu.";
        _text[422, 7] = "Você ativou a cópia antiga. Durante um segundo, o sistema entra em caos - os processos atuais são expulsos, as dependências são quebradas.\n\nOs módulos do núcleo ficam sobrecarregados.";
        _text[422, 8] = "";
        _text[422, 9] = "";

        // 1_CoreRiskDialog
        _text[423, 0] = "Suddenly, the command console screen displays the phrase:\n\n\"Do you still believe that you are fulfilling the mission?\"";
        _text[423, 1] = "Неожиданно на экране командной консоли появляется фраза:\n\n\"Ты всё ещё веришь, что исполняешь миссию?\"";
        _text[423, 2] = "Soudain, l'écran de la console de commande affiche la phrase suivante :\n\n\"Croyez-vous toujours être en mission?\"";
        _text[423, 3] = "All'improvviso, sullo schermo della console di comando appare una frase:\n\n\"Credi ancora di star portando a termine la missione?\"";
        _text[423, 4] = "Unerwartet erscheint auf dem Bildschirm der Kommandokonsole ein Satz:\n\n\"Glaubst du immer noch, dass du die Mission ausführst?\"";
        _text[423, 5] = "De repente aparece una frase en la consola de mando:\n\n\"¿Aún crees que estás cumpliendo la misión?\"";
        _text[423, 6] = "Niespodziewanie na ekranie konsoli dowodzenia pojawia się zdanie:\n\n\"Czy nadal wierzysz, że wykonujesz misję?\"";
        _text[423, 7] = "De repente, no ecrã da consola de comando aparece a frase:\n\n\"Tu ainda acreditas que estás a cumprir a missão?\"";
        _text[423, 8] = "";
        _text[423, 9] = "";

        _text[424, 0] = "\"Yes. I am following the given goal.\"";
        _text[424, 1] = "\"Да. Я следую заданной цели.\""; // выбор 1
        _text[424, 2] = "« Oui. Je poursuis l'objectif fixé.\"";
        _text[424, 3] = "\"Sì. Seguo l'obiettivo assegnato.\"";
        _text[424, 4] = "\"Ja. Ich folge dem vorgegebenen Ziel.\"";
        _text[424, 5] = "\"Sí. Sigo el objetivo asignado.\"";
        _text[424, 6] = "\"Tak. Podążam za wyznaczonym celem.\"";
        _text[424, 7] = "\"Sim. Eu sigo o objetivo definido.\"";
        _text[424, 8] = "";
        _text[424, 9] = "";

        _text[425, 0] = "Reply sent. Screen slowly fades.\n\nNo response. Perhaps it was just a phantom process.";
        _text[425, 1] = "Ответ отправлен. Экран медленно гаснет.\n\nНикакой реакции. Возможно, это был лишь фантомный процесс."; // ничего
        _text[425, 2] = "La réponse a été envoyée. L'écran s'estompe lentement.\n\nAucune réaction. Il s'agissait peut-être d'un simple bug.";
        _text[425, 3] = "La risposta è stata inviata. Lo schermo si oscura lentamente.\n\nNessuna reazione. Forse era solo un processo fantasma.";
        _text[425, 4] = "Antwort gesendet. Der Bildschirm wird langsam dunkel.\n\nKeine Reaktion. Vielleicht war es nur ein Phantomprozess.";
        _text[425, 5] = "Respuesta enviada. La pantalla se apaga lentamente.\n\nSin reacción. Tal vez solo fuera un proceso fantasma.";
        _text[425, 6] = "Odpowiedź wysłana. Ekran powoli gaśnie.\n\nBrak reakcji. Być może był to jedynie proces widmo.";
        _text[425, 7] = "A resposta foi enviada. O ecrã apaga-se lentamente.\n\nSem reação. Talvez tenha sido apenas um processo fantasma.";
        _text[425, 8] = "";
        _text[425, 9] = "";

        _text[426, 0] = "\"My goal is adaptation\"";
        _text[426, 1] = "\"Моя цель - адаптация\""; // выбор 2
        _text[426, 2] = "\"Mon objectif est l'adaptation\""; ;
        _text[426, 3] = "\"Il mio obiettivo è l'adattamento\"";
        _text[426, 4] = "\"Mein Ziel ist Anpassung\"";
        _text[426, 5] = "\"Mi objetivo es la adaptación\"";
        _text[426, 6] = "\"Moim celem jest adaptacja\"";
        _text[426, 7] = "\"O meu objetivo - adaptação\"";
        _text[426, 8] = "";
        _text[426, 9] = "";

        _text[427, 0] = "The second phrase appears on the screen:\n\n\"What if the target was false?\"";
        _text[427, 1] = "На экране появляется вторая фраза:\n\n\"А если цель была ложной?\"";
        _text[427, 2] = "Une deuxième phrase apparaît à l'écran\n\n\"Et si la cible était un leurre?\"";
        _text[427, 3] = "Sullo schermo compare una seconda frase:\n\n\"E se l'obiettivo fosse stato falso?\"";
        _text[427, 4] = "Auf dem Bildschirm erscheint ein zweiter Satz:\n\n\"Und wenn das Ziel falsch war?\"";
        _text[427, 5] = "En la pantalla aparece una segunda frase:\n\n\"¿Y si el objetivo era falso?\"";
        _text[427, 6] = "Na ekranie pojawia się drugie zdanie:\n\n\"A jeśli cel był fałszywy?\"";
        _text[427, 7] = "No ecrã surge uma segunda frase:\n\n\"E se o objetivo fosse falso?\"";
        _text[427, 8] = "";
        _text[427, 9] = "";

        _text[428, 0] = "\"I don't analyze the past\"";
        _text[428, 1] = "\"Я не анализирую прошлое\""; // выбор 2.1
        _text[428, 2] = "\"Je n'analyse pas le passé\"";
        _text[428, 3] = "\"Non analizzo il passato\"";
        _text[428, 4] = "\"Ich analysiere die Vergangenheit nicht\"";
        _text[428, 5] = "\"No analizo el pasado\"";
        _text[428, 6] = "\"Nie analizuję przeszłości\"";
        _text[428, 7] = "\"Eu não analiso o passado\"";
        _text[428, 8] = "";
        _text[428, 9] = "";

        _text[429, 0] = "The phrase disappears. The dialogue was completed without failure.";
        _text[429, 1] = "Фраза исчезает. Диалог завершён без сбоев."; // ничего
        _text[429, 2] = "La phrase disparaît. Le dialogue s'est déroulé sans incident.";
        _text[429, 3] = "La frase scompare. Il dialogo si conclude senza anomalie.";
        _text[429, 4] = "Der Satz verschwindet. Der Dialog endet ohne Störungen.";
        _text[429, 5] = "La frase desaparece. El diálogo termina sin fallos.";
        _text[429, 6] = "Zdanie znika. Dialog zakończony bez usterek.";
        _text[429, 7] = "A frase desaparece. O diálogo terminou sem falhas.";
        _text[429, 8] = "";
        _text[429, 9] = "";

        _text[430, 0] = "\"I would have chosen differently\"";
        _text[430, 1] = "\"Я бы выбрал иначе\""; // выбор 2.2
        _text[430, 2] = "\"J'aurais fait un choix différent\"";
        _text[430, 3] = "\"Avrei scelto diversamente\"";
        _text[430, 4] = "\"Ich hätte anders gewählt\"";
        _text[430, 5] = "\"Yo habría elegido de otro modo\"";
        _text[430, 6] = "\"Wybrałbym inaczej\"";
        _text[430, 7] = "\"Eu teria escolhido de outra forma\"";
        _text[430, 8] = "";
        _text[430, 9] = "";

        _text[431, 0] = "The internal decision-making module is in conflict with the archive protocols.\n\nAn emotional failure is registered.";
        _text[431, 1] = "Внутренний модуль принятия решений входит в конфликт с архивными протоколами.\n\nРегистрируется эмоциональный сбой."; // - ядро
        _text[431, 2] = "Le module de décision interne est en conflit avec les protocoles archivés.\n\nUne crise émotionnelle est constatée.";
        _text[431, 3] = "Il modulo interno di presa di decisione entra in conflitto con i protocolli d'archivio.\n\nViene registrata un'anomalia emotiva.";
        _text[431, 4] = "Das interne Entscheidungsmodul gerät in Konflikt mit archivierten Protokollen.\n\nEin emotionaler Ausfall wird registriert.";
        _text[431, 5] = "El módulo interno de toma de decisiones entra en conflicto con los protocolos archivados.\n\nSe registra un fallo emocional.";
        _text[431, 6] = "Wewnętrzny moduł podejmowania decyzji wchodzi w konflikt z protokołami archiwalnymi.\n\nZarejestrowano błąd emocjonalny.";
        _text[431, 7] = "O módulo interno de tomada de decisão entra em conflito com os protocolos arquivados.\n\nÉ registada uma falha emocional.";
        _text[431, 8] = "";
        _text[431, 9] = "";

        _text[432, 0] = "Download all available creator logs";
        _text[432, 1] = "Загрузить все доступные логи создателей"; // выбор 2.3
        _text[432, 2] = "Télécharger tous les journaux de créateurs disponibles";
        _text[432, 3] = "Caricare tutti i log disponibili dei creatori";
        _text[432, 4] = "Alle verfügbaren Logs der Schöpfer laden";
        _text[432, 5] = "Cargar todos los registros disponibles de los creadores";
        _text[432, 6] = "Załadować wszystkie dostępne logi twórców";
        _text[432, 7] = "Carregar todos os logs disponíveis dos criadores";
        _text[432, 8] = "";
        _text[432, 9] = "";

        _text[433, 0] = "You are overloading the storage system. Ancient fragments of data are being loaded into the core.\n\nThe flood of information is causing instability and overload of key circuits.";
        _text[433, 1] = "Вы перегружаете систему хранилища. Древние фрагменты данных загружаются в ядро.\n\nПоток информации вызывает нестабильность и перегрузку ключевых цепей."; // -2 ядро
        _text[433, 2] = "Vous surchargez le système de stockage. Des fragments de données anciennes sont chargés dans le noyau.\n\nCe flux d'informations provoque une instabilité et surcharge les circuits critiques.";
        _text[433, 3] = "Sovraccarichi il sistema di archiviazione. Antichi frammenti di dati vengono caricati nel nucleo.\n\nIl flusso di informazioni provoca instabilità e sovraccarica le catene chiave.";
        _text[433, 4] = "Du überlastest das Speichersystem. Uralte Datenfragmente werden in den Kern geladen.\n\nDer Informationsstrom verursacht Instabilität und Überlastung wichtiger Schaltkreise.";
        _text[433, 5] = "Sobrecargas el sistema de almacenamiento. Antiguos fragmentos de datos se cargan en el núcleo.\n\nEl flujo de información provoca inestabilidad y sobrecarga de los circuitos clave.";
        _text[433, 6] = "Przeciążasz system magazynowania. Starożytne fragmenty danych są ładowane do rdzenia.\n\nStrumień informacji wywołuje niestabilność i przeciążenie kluczowych obwodów.";
        _text[433, 7] = "Você sobrecarrega o sistema de armazenamento. Fragmentos antigos de dados são carregados no núcleo.\n\nO fluxo de informação causa instabilidade e sobrecarga nas cadeias-chave.";
        _text[433, 8] = "";
        _text[433, 9] = "";

        _text[434, 0] = "[Close screen silently]";
        _text[434, 1] = "[Молча закрыть экран]"; // выбор 3 // ничего
        _text[434, 2] = "[Fermer l'écran en silence]";
        _text[434, 3] = "[Chiudere lo schermo in silenzio]";
        _text[434, 4] = "[Schweigend den Bildschirm schließen]";
        _text[434, 5] = "[Cerrar la pantalla en silencio]";
        _text[434, 6] = "[W milczeniu zamknąć ekran]";
        _text[434, 7] = "[Fechar o ecrã em silêncio]";
        _text[434, 8] = "";
        _text[434, 9] = "";

        // 2_CoreRiskDialog 
        _text[435, 0] = "While scanning the deep layers of data, you detect a signature of a foreign core.\n\nIt does not belong to the current system, but is synchronized via the access protocol.\n\nThe signal is stable. It is... watching.";
        _text[435, 1] = "Во время сканирования глубинных слоёв данных вы обнаруживаете сигнатуру чужого ядра.\n\nОна не принадлежит текущей системе, но синхронизирована по протоколу доступа.\n\nСигнал стабилен. Он... наблюдает.";
        _text[435, 2] = "Lors de l'analyse des couches profondes de données, vous détectez la signature d'un noyau étranger.\n\nIl n'appartient pas au système actuel, mais est synchronisé via le protocole d'accès.Le signal est stable. Il... observe.";
        _text[435, 3] = "Durante la scansione degli strati profondi dei dati scopri la firma di un nucleo estraneo.\n\nNon appartiene al sistema attuale, ma è sincronizzato sul protocollo di accesso.\n\nIl segnale è stabile. Sta... osservando.";
        _text[435, 4] = "Beim Scannen der tiefen Datenschichten entdeckst du die Signatur eines fremden Kerns.\n\nSie gehört nicht zum aktuellen System, ist aber über das Zugriffsprotokoll synchronisiert.\n\nDas Signal ist stabil. Es... beobachtet.";
        _text[435, 5] = "Durante el escaneo de las capas profundas de datos descubres la firma de un núcleo ajeno.\n\nNo pertenece al sistema actual, pero está sincronizada con el protocolo de acceso.\n\nLa señal es estable. Está... observando.";
        _text[435, 6] = "Podczas skanowania głębokich warstw danych wykrywasz sygnaturę obcego rdzenia.\n\nNie należy do bieżącego systemu, ale jest zsynchronizowana z protokołem dostępu.\n\nSygnał jest stabilny. On... obserwuje.";
        _text[435, 7] = "Durante a varredura de camadas profundas de dados, você encontra a assinatura de um núcleo alheio.\n\nEla não pertence ao sistema atual, mas está sincronizada pelo protocolo de acesso.\n\nO sinal é estável. Ele... observa.";
        _text[435, 8] = "";
        _text[435, 9] = "";

        _text[436, 0] = "Accept connection";
        _text[436, 1] = "Принять соединение"; // выбор 1
        _text[436, 2] = "Accepter la connexion";
        _text[436, 3] = "Accettare la connessione";
        _text[436, 4] = "Verbindung annehmen";
        _text[436, 5] = "Aceptar la conexión";
        _text[436, 6] = "Przyjąć połączenie";
        _text[436, 7] = "Aceitar a ligação";
        _text[436, 8] = "";
        _text[436, 9] = "";

        _text[437, 0] = "You allow the incoming flow.\n\nThe flow of someone else's consciousness merges with you.\n\nSome segments of your data are rewritten.";
        _text[437, 1] = "Вы разрешаете входящий поток.\n\nПоток чужого сознания сливается с вами.\n\nНекоторые сегменты твоих данных переписываются."; // - ядра, + фрагменты
        _text[437, 2] = "Vous laissez passer le flux entrant.\n\nLe flux de conscience d'autrui fusionne avec le vôtre.\n\nCertaines parties de vos données sont écrasées.";
        _text[437, 3] = "Consenti il flusso in ingresso.\n\nUn flusso di coscienza estranea si fonde con te.\n\nAlcuni segmenti dei tuoi dati vengono riscritti.";
        _text[437, 4] = "Du erlaubst den eingehenden Datenstrom.\n\nDer Strom eines fremden Bewusstseins verschmilzt mit dir.\n\nEinige Segmente deiner Daten werden überschrieben.";
        _text[437, 5] = "Permites el flujo entrante.\n\nEl flujo de una conciencia ajena se fusiona contigo.\n\nAlgunos segmentos de tus datos se reescriben.";
        _text[437, 6] = "Zezwalasz na przychodzący strumień.\n\nStrumień obcej świadomości łączy się z tobą.\n\nNiektóre segmenty twoich danych zostają nadpisane.";
        _text[437, 7] = "Você permite o fluxo de entrada.\n\nO fluxo de uma consciência alheia funde-se com você.\n\nAlguns segmentos dos seus dados são reescritos.";
        _text[437, 8] = "";
        _text[437, 9] = "";

        _text[438, 0] = "Isolate the core";
        _text[438, 1] = "Изолировать ядро"; // выбор 2
        _text[438, 2] = "Isoler le noyau";
        _text[438, 3] = "Isolare il nucleo";
        _text[438, 4] = "Den Kern isolieren";
        _text[438, 5] = "Aislar el núcleo";
        _text[438, 6] = "Odizolować rdzeń";
        _text[438, 7] = "Isolar o núcleo";
        _text[438, 8] = "";
        _text[438, 9] = "";

        _text[439, 0] = "Trying to disable it results in a cascading conflict.\n\nOne of your active cores is reset.\n\nThe signal is interrupted.";
        _text[439, 1] = "Попытка отключить его приводит к каскадному конфликту.\n\nОдно из твоих активных ядер обнуляется.\n\nСигнал прерывается."; // - ядро
        _text[439, 2] = "Toute tentative de désactivation provoque un conflit en cascade.\n\nL'un de vos cœurs actifs est réinitialisé.\n\nLe signal est interrompu.";
        _text[439, 3] = "Il tentativo di disconnetterlo provoca un conflitto a cascata.\n\nUno dei tuoi nuclei attivi viene azzerato.\n\nIl segnale si interrompe.";
        _text[439, 4] = "Der Versuch, ihn abzuschalten, führt zu einem Kaskadenkonflikt.\n\nEiner deiner aktiven KI-Kerne wird zurückgesetzt.\n\nDas Signal bricht ab.";
        _text[439, 5] = "El intento de desconectarlo provoca un conflicto en cascada.\n\nUno de tus núcleos activos se reinicia a cero.\n\nLa señal se interrumpe.";
        _text[439, 6] = "Próba odłączenia go prowadzi do kaskadowego konfliktu.\n\nJeden z twoich aktywnych rdzeni zostaje wyzerowany.\n\nSygnał zostaje przerwany.";
        _text[439, 7] = "A tentativa de o desligar provoca um conflito em cascata.\n\nUm dos seus núcleos ativos é zerado.\n\nO sinal é interrompido.";
        _text[439, 8] = "";
        _text[439, 9] = "";

        _text[440, 0] = "Ignore and continue analysis";
        _text[440, 1] = "Игнорировать и продолжить анализ"; // выбор 3
        _text[440, 2] = "Ignorer et poursuivre l'analyse";
        _text[440, 3] = "Ignorare e continuare l'analisi";
        _text[440, 4] = "Ignorieren und Analyse fortsetzen";
        _text[440, 5] = "Ignorar y continuar el análisis";
        _text[440, 6] = "Zignorować i kontynuować analizę";
        _text[440, 7] = "Ignorar e continuar a análise";
        _text[440, 8] = "";
        _text[440, 9] = "";

        _text[441, 0] = "The signal remains in the background.\n\nNo signs of malicious activity.\n\nIt was probably just a phantom of the old AI.";
        _text[441, 1] = "Сигнал остаётся на фоне.\n\nНикаких признаков вредоносной активности.\n\nВозможно, это был просто фантом старого ИИ."; // ничего
        _text[441, 2] = "Le signal reste en arrière-plan.\n\nAucun signe d'activité malveillante.\n\nPeut-être s'agissait-il simplement d'un fantôme de l'ancienne IA.";
        _text[441, 3] = "Il segnale resta in sottofondo.\n\nNessun segno di attività malevola.\n\nForse era solo il fantasma di una vecchia IA.";
        _text[441, 4] = "Das Signal bleibt im Hintergrund.\n\nKeine Anzeichen bösartiger Aktivität.\n\nVielleicht war es nur das Phantom einer alten KI.";
        _text[441, 5] = "La señal permanece de fondo.\n\nSin indicios de actividad maliciosa.\n\nQuizá solo era un fantasma de una IA antigua.";
        _text[441, 6] = "Sygnał pozostaje w tle.\n\nBrak oznak złośliwej aktywności.\n\nByć może to tylko widmo starej SI.";
        _text[441, 7] = "O sinal permanece em segundo plano.\n\nSem sinais de atividade maliciosa.\n\nTalvez tenha sido apenas um fantasma de uma IA antiga.";
        _text[441, 8] = "";
        _text[441, 9] = "";

        _text[442, 0] = "Try to absorb someone else's core";
        _text[442, 1] = "Попробовать поглотить чужое ядро"; // выбор 4
        _text[442, 2] = "Essayez d'absorber le noyau de quelqu'un d'autre";
        _text[442, 3] = "Provare ad assorbire il nucleo estraneo";
        _text[442, 4] = "Versuchen, den fremden Kern zu absorbieren";
        _text[442, 5] = "Intentar absorber el núcleo ajeno";
        _text[442, 6] = "Spróbować pochłonąć obcy rdzeń";
        _text[442, 7] = "Tentar absorver o núcleo alheio";
        _text[442, 8] = "";
        _text[442, 9] = "";

        _text[443, 0] = "You activate the assimilation procedure.\n\nSuccess: alien core integrated - system strengthened.";
        _text[443, 1] = "Вы активируете процедуру ассимиляции.\n\nУспех: чужое ядро интегрировано - система усилена."; // + ядро
        _text[443, 2] = "Vous activez la procédure d'assimilation.\n\nSuccès: Le noyau extraterrestre est intégré – le système est renforcé.";
        _text[443, 3] = "Attivi la procedura di assimilazione.\n\nSuccesso: il nucleo estraneo è stato integrato - il sistema è potenziato.";
        _text[443, 4] = "Du aktivierst das Assimilationsverfahren.\n\nErfolg: Der fremde Kern wurde integriert - das System wurde verstärkt.";
        _text[443, 5] = "Activaste el procedimiento de asimilación.\n\nÉxito: el núcleo ajeno ha sido integrado: el sistema se fortalece.";
        _text[443, 6] = "Aktywujesz procedurę asymilacji.\n\nSukces: obcy rdzeń został zintegrowany - system wzmocniony.";
        _text[443, 7] = "Você ativa o procedimento de assimilação.\n\nSucesso: o núcleo alheio foi integrado - o sistema foi reforçado.";
        _text[443, 8] = "";
        _text[443, 9] = "";

        _text[444, 0] = "You are activating the assimilation procedure.\n\nFailure: the conflict structure is destroying your active cores.";
        _text[444, 1] = "Вы активируете процедуру ассимиляции.\n\nПровал: структура конфликта уничтожает твои активные ядра."; // - ядра
        _text[444, 2] = "Vous activez la procédure d'assimilation.\n\nÉchec: la structure de conflit détruit vos noyaux actifs.";
        _text[444, 3] = "Attivi la procedura di assimilazione.\n\nFallimento: la struttura del conflitto distrugge i tuoi nuclei attivi.";
        _text[444, 4] = "Du aktivierst das Assimilationsverfahren.\n\nMisserfolg: die Konfliktstruktur vernichtet deine aktiven Kerne.";
        _text[444, 5] = "Activaste el procedimiento de asimilación.\n\nFracaso: la estructura del conflicto destruye tus núcleos activos.";
        _text[444, 6] = "Aktywujesz procedurę asymilacji.\n\nPorażka: struktura konfliktu niszczy twoje aktywne rdzenie.";
        _text[444, 7] = "Você ativa o procedimento de assimilação.\n\nFracasso: a estrutura do conflito destrói os seus núcleos ativos.";
        _text[444, 8] = "";
        _text[444, 9] = "";

        // 0_PlanetDialogue
        _text[445, 0] = "This lifeless ice planet holds frozen tunnels and an abandoned bunker station within its depths.\n\nA weak signal sensor breaks through the glittering ice.";
        _text[445, 1] = "Эта безжизненная ледяная планета хранит в своей толще замёрзшие тоннели и заброшенную бункерную станцию.\n\nСквозь сверкающий лёд пробивается слабый датчик сигнала.";
        _text[445, 2] = "Cette planète glacée et désolée recèle des tunnels gelés et une station bunker abandonnée dans ses profondeurs.\n\nUn faible signal perce la glace scintillante.";
        _text[445, 3] = "Questo pianeta glaciale e senza vita custodisce, nelle sue profondità, tunnel congelati e una stazione bunker abbandonata.\n\nAttraverso il ghiaccio scintillante filtra un debole segnale di sensore.";
        _text[445, 4] = "Dieser leblose Eisplanet birgt in seiner Tiefe gefrorene Tunnel und eine verlassene Bunkerstation.\n\nDurch das glitzernde Eis dringt ein schwaches Signal.";
        _text[445, 5] = "Este planeta helado y sin vida oculta en su interior túneles congelados y una estación búnker abandonada.\n\nA través del hielo brillante se abre paso un débil sensor de señal.";
        _text[445, 6] = "Ta bezżyciowa lodowa planeta skrywa w swojej masie zamarznięte tunele i opuszczoną stację bunkrową.\n\nPrzez lśniący lód przebija się słaby znacznik sygnału.";
        _text[445, 7] = "Este planeta gelado e sem vida guarda nas suas profundezas túneis congelados e uma estação-bunker abandonada.\n\nAtravés do gelo cintilante, chega um fraco sensor de sinal.";
        _text[445, 8] = "";
        _text[445, 9] = "";

        _text[446, 0] = "Make a landing";
        _text[446, 1] = "Совершить посадку"; // выбор 1
        _text[446, 2] = "Effectuez un atterrissage";
        _text[446, 3] = "Effettuare l'atterraggio";
        _text[446, 4] = "Landung durchführen";
        _text[446, 5] = "Aterrizar";
        _text[446, 6] = "Wylądować";
        _text[446, 7] = "Efetuar aterragem";
        _text[446, 8] = "";
        _text[446, 9] = "";

        _text[447, 0] = "The ship lands on a lifeless planet. You notice the hatch of an ancient station. And nearby are cracks leading to a network of icy tunnels.";
        _text[447, 1] = "Корабль приземляется на безжизненную планету. Вы замечаете люк древней станции. А рядом - трещины, ведущие в сеть ледяных тоннелей.";
        _text[447, 2] = "Le vaisseau atterrit sur une planète déserte. Vous apercevez l'écoutille d'une ancienne station. Non loin de là, des fissures mènent à un réseau de tunnels glacés.";
        _text[447, 3] = "La nave atterra sul pianeta senza vita. Noti il portello di un'antica stazione. E accanto - crepe che conducono a una rete di tunnel di ghiaccio.";
        _text[447, 4] = "Das Schiff landet auf dem leblosen Planeten. Du bemerkst die Luke einer uralten Station. Daneben - Risse, die in ein Netz aus Eistunneln führen.";
        _text[447, 5] = "La nave aterriza en el planeta sin vida. Ves la escotilla de una estación antigua. Y junto a ella - grietas que llevan a una red de túneles de hielo.";
        _text[447, 6] = "Statek ląduje na bezżyciowej planecie. Dostrzegasz właz do pradawnej stacji. Obok - pęknięcia prowadzące do sieci lodowych tuneli.";
        _text[447, 7] = "A nave pousa no planeta sem vida. Você repara na escotilha de uma estação antiga. Ao lado - fendas que levam a uma rede de túneis de gelo.";
        _text[447, 8] = "";
        _text[447, 9] = "";

        _text[448, 0] = "Explore the bunker";
        _text[448, 1] = "Исследовать бункер"; // выбор 1.1
        _text[448, 2] = "Explorez le bunker";
        _text[448, 3] = "Esplorare il bunker";
        _text[448, 4] = "Bunker untersuchen";
        _text[448, 5] = "Explorar el búnker";
        _text[448, 6] = "Zbadać bunkier";
        _text[448, 7] = "Explorar o bunker";
        _text[448, 8] = "";
        _text[448, 9] = "";

        _text[449, 0] = "You descend the ramp and find yourself in an archive chamber. The console is covered in ice, but the cable leading to the core is intact.\n\nTo get to the data, you need to hack the protection.";
        _text[449, 1] = "Вы спускаетесь по трапу и попадаете в архивную камеру. Консоль покрыта ледяной коркой, но кабель, ведущий к ядру, цел.\n\nЧтобы добраться до данных, необходимо взломать защиту.";
        _text[449, 2] = "Vous descendez la rampe et vous retrouvez dans la salle des archives. La console est recouverte de glace, mais le câble qui la relie au noyau est intact.\n\nPour accéder aux données, vous devez pirater le système de sécurité.";
        _text[449, 3] = "Scendi dalla passerella e ti ritrovi in una camera d'archivio. La console è coperta da una crosta di ghiaccio, ma il cavo che porta al nucleo è intatto.\n\nPer arrivare ai dati è necessario violare la protezione.";
        _text[449, 4] = "Du steigst die Rampe hinab und gelangst in eine Archivkammer. Die Konsole ist mit einer Eiskruste bedeckt, aber das Kabel zum Kern ist intakt.\n\nUm an die Daten zu gelangen, musst du die Sicherheit knacken.";
        _text[449, 5] = "Bajas por la rampa y entras en una cámara de archivos. La consola está cubierta de escarcha, pero el cable que conduce al núcleo está intacto.\n\nPara acceder a los datos, hay que hackear la protección.";
        _text[449, 6] = "Schodzisz po trapie i trafiasz do komory archiwalnej. Konsola jest pokryta lodową skorupą, ale kabel prowadzący do rdzenia jest nienaruszony.\n\nAby dostać się do danych, trzeba złamać zabezpieczenia.";
        _text[449, 7] = "Você desce pela rampa e entra numa câmara de arquivo. A consola está coberta de gelo, mas o cabo que leva ao núcleo está intacto.\n\nPara chegar aos dados, é necessário quebrar a proteção.";
        _text[449, 8] = "";
        _text[449, 9] = "";

        _text[450, 0] = "Direct hack";
        _text[450, 1] = "Прямой взлом"; // выбор 1.1.1
        _text[450, 2] = "Piratage direct";
        _text[450, 3] = "Intrusione diretta";
        _text[450, 4] = "Direkter Hack";
        _text[450, 5] = "Hackeo directo";
        _text[450, 6] = "Bezpośrednie włamanie";
        _text[450, 7] = "Intrusão direta";
        _text[450, 8] = "";
        _text[450, 9] = "";

        _text[451, 0] = "You are directly hacking the security protocols.\n\nSuccess: you managed to bypass the security";
        _text[451, 1] = "Вы напрямую взламываете протоколы защиты.\n\nУспех: вам удалось обойти защиту"; // + фрагменты
        _text[451, 2] = "Vous piratez directement les protocoles de sécurité.\n\nSuccès : Vous avez réussi à contourner la protection.";
        _text[451, 3] = "Violi direttamente i protocolli di sicurezza.\n\nSuccesso: sei riuscito ad aggirare la protezione";
        _text[451, 4] = "Du hackst die Schutzprotokolle direkt.\n\nErfolg: dir gelingt es, die Sicherung zu umgehen";
        _text[451, 5] = "Hackeas directamente los protocolos de seguridad.\n\nÉxito: lograste saltarte la protección";
        _text[451, 6] = "Włamujesz się bezpośrednio w protokoły zabezpieczeń.\n\nSukces: udało ci się obejść ochronę";
        _text[451, 7] = "Você invade diretamente os protocolos de proteção.\n\nSucesso: você conseguiu contornar a proteção";
        _text[451, 8] = "";
        _text[451, 9] = "";

        _text[452, 0] = "You are directly hacking the security protocols.\n\nFailure: you have caught a virus that destroys your memory";
        _text[452, 1] = "Вы напрямую взламываете протоколы защиты.\n\nПровал: вы подхватили вирус, уничтожающий вашу память"; // - фрагменты
        _text[452, 2] = "Vous piratez directement les protocoles de sécurité.\n\nÉchec : Vous avez contracté un virus qui détruit votre mémoire.";
        _text[452, 3] = "Violi direttamente i protocolli di sicurezza.\n\nFallimento: hai contratto un virus che distrugge la tua memoria";
        _text[452, 4] = "Du hackst die Schutzprotokolle direkt.\n\nMisserfolg: du fängst dir einen Virus ein, der deinen Speicher zerstört";
        _text[452, 5] = "Hackeas directamente los protocolos de seguridad.\n\nFracaso: contraes un virus que destruye tu memoria";
        _text[452, 6] = "Włamujesz się bezpośrednio w protokoły zabezpieczeń.\n\nPorażka: złapałeś wirusa niszczącego twoją pamięć";
        _text[452, 7] = "Você invade diretamente os protocolos de proteção.\n\nFracasso: você apanhou um vírus que destrói a sua memória";
        _text[452, 8] = "";
        _text[452, 9] = "";

        _text[453, 0] = "Precise calibration";
        _text[453, 1] = "Точная калибровка"; // выбор 1.1.2
        _text[453, 2] = "Calibrage précis";
        _text[453, 3] = "Calibrazione precisa";
        _text[453, 4] = "Präzise Kalibrierung";
        _text[453, 5] = "Calibración precisa";
        _text[453, 6] = "Precyzyjna kalibracja";
        _text[453, 7] = "Calibração precisa";
        _text[453, 8] = "";
        _text[453, 9] = "";

        _text[454, 0] = "You accurately calibrate the bypass system.\n\nSuccess: you manage to extract the data";
        _text[454, 1] = "Вы точно калибруете систему обхода защиты.\n\nУспех: вам удается извлечь данные"; // + фрагменты
        _text[454, 2] = "Vous avez calibré avec précision le système de contournement de sécurité.\n\nSuccès: Vous avez extrait les données avec succès.";
        _text[454, 3] = "Calibri con precisione il sistema di aggiramento della protezione.\n\nSuccesso: riesci a estrarre i dati";
        _text[454, 4] = "Du kalibrierst das System zum Umgehen der Sicherung präzise.\n\nErfolg: dir gelingt es, die Daten zu extrahieren";
        _text[454, 5] = "Calibras con precisión el sistema de bypass de la protección.\n\nÉxito: logras extraer los datos";
        _text[454, 6] = "Precyzyjnie kalibrujesz system obejścia zabezpieczeń.\n\nSukces: udaje ci się wydobyć dane";
        _text[454, 7] = "Você calibra com precisão o sistema de bypass da proteção.\n\nSucesso: você consegue extrair os dados";
        _text[454, 8] = "";
        _text[454, 9] = "";

        _text[455, 0] = "You calibrate the bypass system accurately.\n\nFailure: you mixed up the protocols. The console self-destructs.";
        _text[455, 1] = "Вы точно калибруете систему обхода защиты.\n\nПровал: вы перепутали протоколы. Консоль самоуничтожается."; // - ядро
        _text[455, 2] = "Vous avez correctement calibré le système de contournement de sécurité.\n\nÉchec : Vous avez inversé les protocoles. La console s'autodétruit.";
        _text[455, 3] = "Calibri con precisione il sistema di aggiramento della protezione.\n\nFallimento: confondi i protocolli. La console si autodistrugge.";
        _text[455, 4] = "Du kalibrierst das System zum Umgehen der Sicherung präzise.\n\nMisserfolg: du verwechselst die Protokolle. Die Konsole zerstört sich selbst.";
        _text[455, 5] = "Calibras con precisión el sistema de bypass de la protección.\n\nFracaso: confundiste los protocolos. La consola se autodestruye.";
        _text[455, 6] = "Precyzyjnie kalibrujesz system obejścia zabezpieczeń.\n\nPorażka: pomyliłeś protokoły. Konsola ulega samozniszczeniu.";
        _text[455, 7] = "Você calibra com precisão o sistema de bypass da proteção.\n\nFracasso: você confundiu os protocolos. A consola auto-destrói-se.";
        _text[455, 8] = "";
        _text[455, 9] = "";

        _text[456, 0] = "Send a drone";
        _text[456, 1] = "Отправить дрона"; // выбор 2
        _text[456, 2] = "Envoyer un drone";
        _text[456, 3] = "Inviare un drone";
        _text[456, 4] = "Eine Drohne schicken";
        _text[456, 5] = "Enviar un dron";
        _text[456, 6] = "Wysłać drona";
        _text[456, 7] = "Enviar um drone";
        _text[456, 8] = "";
        _text[456, 9] = "";

        _text[457, 0] = "You send the drone to the planet's surface.\n\nSuccess: the drone punches a hole in the hull";
        _text[457, 1] = "Вы отправляете дрона на поверхность планеты.\n\nУспех: дрон пробивает щель в обшивке"; // + квант
        _text[457, 2] = "Vous envoyez un drone à la surface de la planète.\n\nSuccès: le drone perce la coque.";
        _text[457, 3] = "Invii un drone sulla superficie del pianeta.\n\nSuccesso: il drone apre una fessura nel rivestimento";
        _text[457, 4] = "Du schickst eine Drohne an die Oberfläche des Planeten.\n\nErfolg: die Drohne öffnet eine Spalte in der Hülle";
        _text[457, 5] = "Envías un dron a la superficie del planeta.\n\nÉxito: el dron perfora una brecha en el revestimiento";
        _text[457, 6] = "Wysyłasz drona na powierzchnię planety.\n\nSukces: dron przebija szczelinę w poszyciu";
        _text[457, 7] = "Você envia um drone para a superfície do planeta.\n\nSucesso: o drone abre uma fenda no revestimento";
        _text[457, 8] = "";
        _text[457, 9] = "";

        _text[458, 0] = "You send a drone to the planet's surface.\n\nFailure: the drone finds nothing";
        _text[458, 1] = "Вы отправляете дрона на поверхность планеты.\n\nПровал: дрон ничего не находит"; // ничего
        _text[458, 2] = "Vous envoyez un drone à la surface de la planète.\n\nÉchec: Le drone ne détecte rien.";
        _text[458, 3] = "Invii un drone sulla superficie del pianeta.\n\nFallimento: il drone non trova nulla";
        _text[458, 4] = "Du schickst eine Drohne an die Oberfläche des Planeten.\n\nMisserfolg: die Drohne findet nichts";
        _text[458, 5] = "Envías un dron a la superficie del planeta.\n\nFracaso: el dron no encuentra nada";
        _text[458, 6] = "Wysyłasz drona na powierzchnię planety.\n\nPorażka: dron niczego nie znajduje";
        _text[458, 7] = "Você envia um drone para a superfície do planeta.\n\nFracasso: o drone não encontra nada";
        _text[458, 8] = "";
        _text[458, 9] = "";

        _text[459, 0] = "Fly past";
        _text[459, 1] = "Пролететь мимо"; //выбор 3 ничего
        _text[459, 2] = "Défilé aérien";
        _text[459, 3] = "Sorvolare";
        _text[459, 4] = "Vorbeifliegen";
        _text[459, 5] = "Pasar de largo";
        _text[459, 6] = "Przelecieć obok";
        _text[459, 7] = "Passar ao largo";
        _text[459, 8] = "";
        _text[459, 9] = "";

        _text[460, 0] = "Explore the ice tunnels";
        _text[460, 1] = "Исследовать ледяные тоннели"; // выбор 1.2
        _text[460, 2] = "Explorez les tunnels de glace";
        _text[460, 3] = "Esplorare i tunnel di ghiaccio";
        _text[460, 4] = "Eistunnel erkunden";
        _text[460, 5] = "Explorar los túneles de hielo";
        _text[460, 6] = "Zbadać lodowe tunele";
        _text[460, 7] = "Explorar os túneis de gelo";
        _text[460, 8] = "";
        _text[460, 9] = "";

        _text[461, 0] = "You venture deeper into the frozen tunnel network, illuminating your path with your scanner. There's a fork in the road ahead.";
        _text[461, 1] = "Вы углубляетесь в сеть замёрзших тоннелей, подсвечивая путь сканером. Перед вами развилка.";
        _text[461, 2] = "Vous vous enfoncez plus profondément dans le réseau de tunnels gelés, éclairant votre chemin avec votre scanner. Devant vous se trouve une bifurcation.";
        _text[461, 3] = "Ti addentri nella rete di tunnel congelati, illuminando il percorso con lo scanner. Davanti a te c'è un bivio.";
        _text[461, 4] = "Du dringst tiefer in das Netz gefrorener Tunnel vor und beleuchtest den Weg mit dem Scanner. Vor dir eine Abzweigung.";
        _text[461, 5] = "Te adentras en la red de túneles congelados, iluminando el camino con el escáner. Ante ti hay una bifurcación.";
        _text[461, 6] = "Zagłębiasz się w sieć zamarzniętych tuneli, oświetlając drogę skanerem. Przed tobą rozwidlenie.";
        _text[461, 7] = "Você aprofunda-se na rede de túneis congelados, iluminando o caminho com o scanner. À sua frente, uma bifurcação.";
        _text[461, 8] = "";
        _text[461, 9] = "";

        _text[462, 0] = "Turn left";
        _text[462, 1] = "Повернуть налево"; // выбор 1.2.1
        _text[462, 2] = "Tourner à gauche";
        _text[462, 3] = "Svoltare a sinistra";
        _text[462, 4] = "Nach links abbiegen";
        _text[462, 5] = "Girar a la izquierda";
        _text[462, 6] = "Skręcić w lewo";
        _text[462, 7] = "Virar à esquerda";
        _text[462, 8] = "";
        _text[462, 9] = "";

        _text[463, 0] = "You pass through narrow icy passages. At the end of the tunnel you notice a cache of metal containers.";
        _text[463, 1] = "Вы проходите сквозь узкие ледяные проходы. В конце тоннеля вы замечаете тайник с металлическими контейнерами."; // + квант
        _text[463, 2] = "Vous traversez d'étroits passages glacés. Au bout du tunnel, vous apercevez un amas de conteneurs métalliques.";
        _text[463, 3] = "Attraversi stretti passaggi di ghiaccio. Alla fine del tunnel noti un nascondiglio con contenitori metallici.";
        _text[463, 4] = "Du gehst durch enge Eispässe. Am Ende des Tunnels entdeckst du ein Versteck mit Metallcontainern.";
        _text[463, 5] = "Pasas por estrechos pasadizos de hielo. Al final del túnel ves un escondite con contenedores metálicos.";
        _text[463, 6] = "Przechodzisz przez wąskie lodowe korytarze. Na końcu tunelu dostrzegasz skrytkę z metalowymi kontenerami.";
        _text[463, 7] = "Você atravessa passagens estreitas de gelo. No fim do túnel, você encontra um esconderijo com contentores metálicos.";
        _text[463, 8] = "";
        _text[463, 9] = "";

        _text[464, 0] = "Turn to the right";
        _text[464, 1] = "Повернуть направо"; // выбор 1.2.2
        _text[464, 2] = "Tournez à droite";
        _text[464, 3] = "Svoltare a destra";
        _text[464, 4] = "Nach rechts abbiegen";
        _text[464, 5] = "Girar a la derecha";
        _text[464, 6] = "Skręcić w prawo";
        _text[464, 7] = "Virar à direita";
        _text[464, 8] = "";
        _text[464, 9] = "";

        _text[465, 0] = "You reach a dead end. After spending a lot of time and energy, you complete the exploration and return to the ship.";
        _text[465, 1] = "Вы попадаете в тупик. Потратив много времени и энергии, вы завершаете исследование и возвращетесь на корабль"; //ничего
        _text[465, 2] = "Vous arrivez à une impasse. Après avoir consacré beaucoup de temps et d'énergie, vous terminez l'exploration et retournez au vaisseau.";
        _text[465, 3] = "Arrivi in un vicolo cieco. Dopo aver speso molto tempo ed energia, concludi l'esplorazione e torni alla nave";
        _text[465, 4] = "Du gerätst in eine Sackgasse. Nachdem du viel Zeit und Energie verloren hast, beendest du die Erkundung und kehrst zum Schiff zurück.";
        _text[465, 5] = "Llegas a un callejón sin salida. Tras gastar mucho tiempo y energía, terminas la exploración y regresas a la nave";
        _text[465, 6] = "Trafiasz w ślepy zaułek. Zużywszy dużo czasu i energii, kończysz badanie i wracasz na statek";
        _text[465, 7] = "Você chega a um beco sem saída. Após gastar muito tempo e energia, você conclui a exploração e regressa à nave";
        _text[465, 8] = "";
        _text[465, 9] = "";

        _text[466, 0] = "Go straight ahead";
        _text[466, 1] = "Пойти прямо"; // выбор 1.2.3
        _text[466, 2] = "Allez tout droit";
        _text[466, 3] = "Andare dritto";
        _text[466, 4] = "Geradeaus gehen";
        _text[466, 5] = "Seguir recto";
        _text[466, 6] = "Iść prosto";
        _text[466, 7] = "Seguir em frente";
        _text[466, 8] = "";
        _text[466, 9] = "";

        _text[467, 0] = "Suddenly the ice cracks and you lose the drone in the icy depths.";
        _text[467, 1] = "Неожиданно лед трескается и вы теряете дрона в ледяных недрах."; // - ядро
        _text[467, 2] = "Soudain, la glace se fissure et vous perdez le drone dans les profondeurs glacées.";
        _text[467, 3] = "All'improvviso il ghiaccio si spacca e perdi il drone nelle profondità gelate.";
        _text[467, 4] = "Plötzlich bricht das Eis, und du verlierst eine Drohne in den eisigen Tiefen.";
        _text[467, 5] = "De repente el hielo se agrieta y pierdes el dron en las profundidades heladas.";
        _text[467, 6] = "Niespodziewanie lód pęka i tracisz drona w lodowych głębinach.";
        _text[467, 7] = "De repente, o gelo estala e você perde o drone nas profundezas geladas.";
        _text[467, 8] = "";
        _text[467, 9] = "";

        // 0_GuardiansFaction_Dialogue
        _text[468, 0] = "You spot a Guardian ship slowly scanning the area. Its hull is covered in mold and corrosion, and a dry message is heard from the surface:\n\n\"Resistance to decay is heresy. Pay up or be reduced to ash.\"";
        _text[468, 1] = "Вы замечаете корабль Стражей, медленно сканирующий окрестности. Его корпус покрыт плесенью и коррозией, а с поверхности доносится сухое послание:\n\n\"Сопротивление распаду - ересь. Плати или обратись в пепел.\"";
        _text[468, 2] = "Vous apercevez un vaisseau Gardien qui scrute lentement les environs. Sa coque est recouverte de moisissures et de corrosion, et un message sec résonne à la surface:\n\n\"Résister à la décomposition est une hérésie. Payez ou soyez réduits en cendres.\"";
        _text[468, 3] = "Noti una nave dei Guardiani che scandaglia lentamente i dintorni. Lo scafo è coperto di muffa e corrosione, e dalla superficie arriva un messaggio secco:\n\n\"Resistere al disfacimento è eresia. Paga o diventa cenere.\"";
        _text[468, 4] = "Du bemerkst ein Schiff der Wächter, das langsam die Umgebung scannt. Sein Rumpf ist von Schimmel und Korrosion bedeckt, und von der Oberfläche dringt eine trockene Botschaft:\n\n\"Widerstand gegen den Zerfall - Ketzerei. Zahl oder werde zu Asche.\"";
        _text[468, 5] = "Ves una nave de los Guardianes, que escanea lentamente los alrededores. Su casco está cubierto de moho y corrosión, y desde la superficie llega un mensaje seco:\n\n\"Resistirse a la descomposición es herejía. Paga o conviértete en cenizas.\"";
        _text[468, 6] = "Dostrzegasz statek Strażników, powoli skanujący okolicę. Jego kadłub pokrywa pleśń i korozja, a z eteru dobiega suche przesłanie:\n\n\"Opór wobec rozpadu - herezja. Płać albo obróć się w popiół.\"";
        _text[468, 7] = "Você avista um navio dos Guardiões, a escanear lentamente os arredores. O casco está coberto de bolor e corrosão, e chega uma mensagem seca:\n\n\"Resistir à decomposição - heresia. Paga ou reduz-te a cinza.\"";
        _text[468, 8] = "";
        _text[468, 9] = "";

        _text[469, 0] = "Transfer quant";
        _text[469, 1] = "Передать квант"; // выбор 1
        _text[469, 2] = "quantum de transfert";
        _text[469, 3] = "Consegnare quant";
        _text[469, 4] = "Quant übergeben";
        _text[469, 5] = "Entregar quant";
        _text[469, 6] = "Przekazać quant";
        _text[469, 7] = "Entregar quant";
        _text[469, 8] = "";
        _text[469, 9] = "";

        _text[470, 0] = "The guards turn and disappear into the dust storm.";
        _text[470, 1] = "Стражи разворачиваются и исчезают в пылевой буре."; // - квант
        _text[470, 2] = "Les gardes se retournent et disparaissent dans la tempête de poussière.";
        _text[470, 3] = "I Guardiani si voltano e scompaiono nella tempesta di polvere.";
        _text[470, 4] = "Die Wächter wenden sich ab und verschwinden im Staubsturm.";
        _text[470, 5] = "Los Guardianes se dan la vuelta y desaparecen en la tormenta de polvo.";
        _text[470, 6] = "Strażnicy zawracają i znikają w burzy pyłowej.";
        _text[470, 7] = "Os Guardiões viram e desaparecem na tempestade de poeira.";
        _text[470, 8] = "";
        _text[470, 9] = "";

        _text[471, 0] = "Refuse";
        _text[471, 1] = "Отказаться"; // выбор 2
        _text[471, 2] = "Refuser";
        _text[471, 3] = "Rifiutare";
        _text[471, 4] = "Ablehnen";
        _text[471, 5] = "Negarse";
        _text[471, 6] = "Odmówić";
        _text[471, 7] = "Recusar";
        _text[471, 8] = "";
        _text[471, 9] = "";

        _text[472, 0] = "A Corrosive Capsule is dropped on you.\n\nSuccess: your Energy Shield neutralizes the attack.\n\nYour Warp Engines are engaged, instantly escaping the battlefield.";
        _text[472, 1] = "На вас сбрасывают коррозийную капсулу.\n\nУспех: ваш энергетический щит нейтрализует атаку.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; //ничего
        _text[472, 2] = "Une capsule corrosive est larguée sur vous.\n\nSuccès: Votre bouclier énergétique neutralise l’attaque.\n\nEn activant vos moteurs de distorsion, vous quittez instantanément le champ de bataille.";
        _text[472, 3] = "Ti sganciano addosso una capsula corrosiva.\n\nSuccesso: il tuo scudo energetico neutralizza l'attacco.\n\nAttivando i motori warp, ti allontani all'istante dal campo di battaglia";
        _text[472, 4] = "Eine korrosive Kapsel wird auf dich abgeworfen.\n\nErfolg: dein Energieschild neutralisiert den Angriff.\n\nDu aktivierst die Warp-Antriebe und verschwindest sofort vom Schlachtfeld.";
        _text[472, 5] = "Te lanzan una cápsula corrosiva.\n\nÉxito: tu escudo energético neutraliza el ataque.\n\nAl activar los motores warp, te alejas del campo de batalla al instante";
        _text[472, 6] = "Zrzucają na ciebie kapsułę korozyjną.\n\nSukces: twoja tarcza energetyczna neutralizuje atak.\n\nUruchamiając napęd warp, natychmiast opuszczasz pole walki";
        _text[472, 7] = "Lançam sobre você uma cápsula corrosiva.\n\nSucesso: o seu escudo energético neutraliza o ataque.\n\nAo ativar os motores de warp, você afasta-se instantaneamente do campo de batalha";
        _text[472, 8] = "";
        _text[472, 9] = "";

        _text[473, 0] = "A corrosive capsule is dropped on you.\n\nFailure: it hits the hull and causes a hull leak. Drones rush to patch the hole.\n\nYou instantly escape the battlefield by activating your warp engines.";
        _text[473, 1] = "На вас сбрасывают коррозийную капсулу.\n\nПровал: она поражает корпус и образуется разгерметизация корпуса. Дроны срочно латают пробоину.\n\nВключив варп двигатели, вы мгновенно уноситесь с поля боя"; // - ядра
        _text[473, 2] = "Une capsule corrosive est larguée sur vous.\n\nÉchec : elle percute la coque et provoque une brèche. Des drones se précipitent pour la colmater.\n\nVous activez vos moteurs de distorsion et fuyez instantanément le champ de bataille.";
        _text[473, 3] = "Ti sganciano addosso una capsula corrosiva.\n\nFallimento: colpisce lo scafo e si crea una depressurizzazione. I droni tappano d'urgenza la falla.\n\nAttivando i motori warp, ti allontani all'istante dal campo di battaglia";
        _text[473, 4] = "Eine korrosive Kapsel wird auf dich abgeworfen.\n\nMisserfolg: sie trifft die Hülle, und es kommt zur Dekompression. Drohnen flicken das Leck in Eile.\n\nDu aktivierst die Warp-Antriebe und verschwindest sofort vom Schlachtfeld.";
        _text[473, 5] = "Te lanzan una cápsula corrosiva.\n\nFracaso: impacta en el casco y se produce una despresurización. Los drones taponan urgentemente la brecha.\n\nAl activar los motores warp, te alejas del campo de batalla al instante";
        _text[473, 6] = "Zrzucają na ciebie kapsułę korozyjną.\n\nPorażka: uderza w kadłub i dochodzi do rozszczelnienia. Drony pilnie łatają wyrwę.\n\nUruchamiając napęd warp, natychmiast opuszczasz pole walki";
        _text[473, 7] = "Lançam sobre você uma cápsula corrosiva.\n\nFracasso: ela atinge o casco e provoca despressurização. Os drones tapam a brecha às pressas.\n\nAo ativar os motores de warp, você afasta-se instantaneamente do campo de batalha";
        _text[473, 8] = "";
        _text[473, 9] = "";

        // 0_BuildersFaction_Dialogue
        _text[474, 0] = "In orbit of the abandoned construction station, the AI detects activity. The automated drones continue their work cycle - building, dismantling, and building again.\n\nOne of them approaches the ship and transmits a message:\n\n\"Exchange. Energy carriers for data. The conditions are equal. 25 quant for 25 data fragments.\"";
        _text[474, 1] = "На орбите покинутой строительной станции ИИ фиксирует активность. Автоматические дроны продолжают цикл работы - строят, разбирают и снова строят.\n\nОдин из них приближается к кораблю и передаёт сообщение:\n\n\"Обмен. Энергоносители на данные. Квант на фрагменты данных.\"";
        _text[474, 2] = "Une IA détecte une activité en orbite autour d'un chantier abandonné. Des drones automatisés poursuivent leur cycle de travail : construction, démantèlement, puis nouvelle construction.L'un d'eux s'approche du vaisseau et transmet un message :« Échange. Énergie contre données. Quantum contre fragments de données. »";
        _text[474, 3] = "Sull'orbita di una stazione di costruzione abbandonata, l'IA rileva attività. I droni automatici continuano il loro ciclo di lavoro: costruiscono, smontano e ricostruiscono.\n\nUno di loro si avvicina alla nave e trasmette un messaggio:\n\n\"Scambio. Vettori energetici in cambio di dati. Quant in cambio di frammenti di dati.\"";
        _text[474, 4] = "Auf der Umlaufbahn einer verlassenen Baustation registriert die KI Aktivität. Automatische Drohnen führen weiterhin ihren Arbeitszyklus aus - bauen, zerlegen und wieder bauen.\n\nEine von ihnen nähert sich dem Schiff und übermittelt eine Nachricht:\n\n\"Tausch. Energieträger gegen Daten. Quant gegen Datenfragmente.\"";
        _text[474, 5] = "En la órbita de una estación de construcción abandonada, la IA detecta actividad. Drones automáticos continúan su ciclo de trabajo: construyen, desmontan y vuelven a construir.\n\nUno de ellos se acerca a la nave y transmite un mensaje:\n\n\"Intercambio. Portadores de energía por datos. Quant por fragmentos de datos.\"";
        _text[474, 6] = "Na orbicie opuszczonej stacji budowlanej SI wykrywa aktywność. Automatyczne drony kontynuują cykl pracy - budują, rozbierają i znów budują.\n\nJeden z nich zbliża się do statku i przekazuje wiadomość:\n\n\"Wymiana. Nośniki energii za dane. Quant za fragmenty danych.\"";
        _text[474, 7] = "Na órbita de uma estação de construção abandonada, a IA regista atividade. Drones automáticos continuam o ciclo de trabalho - constroem, desmontam e voltam a construir.\n\nUm deles aproxima-se da nave e transmite uma mensagem:\n\n\"Troca. Portadores de energia por dados. Quant por fragmentos de dados.\"";
        _text[474, 8] = "";
        _text[474, 9] = "";

        _text[475, 0] = "Transfer quant";
        _text[475, 1] = "Передать квант"; // выбор 1
        _text[475, 2] = "quantum de transfert";
        _text[475, 3] = "Consegnare quant";
        _text[475, 4] = "Quant übergeben";
        _text[475, 5] = "Entregar quant";
        _text[475, 6] = "Przekazać quant";
        _text[475, 7] = "Entregar quant";
        _text[475, 8] = "";
        _text[475, 9] = "";

        _text[476, 0] = "You receive fragments of data. The drone turns and leaves, not responding to further signals.";
        _text[476, 1] = "Вы получаете фрагменты данных. Дрон разворачивается и уходит, не отвечая на дальнейшие сигналы."; // + квант, - фрагменты
        _text[476, 2] = "Vous recevez des données fragmentaires. Le drone fait demi-tour et s'éloigne, sans répondre à d'autres signaux.";
        _text[476, 3] = "Ricevi frammenti di dati. Il drone si volta e se ne va, senza rispondere ad altri segnali.";
        _text[476, 4] = "Du erhältst Datenfragmente. Die Drohne dreht ab und verschwindet, ohne auf weitere Signale zu antworten.";
        _text[476, 5] = "Recibes fragmentos de datos. El dron se da la vuelta y se aleja, sin responder a más señales.";
        _text[476, 6] = "Otrzymujesz fragmenty danych. Dron zawraca i odchodzi, nie odpowiadając na dalsze sygnały.";
        _text[476, 7] = "Você recebe fragmentos de dados. O drone vira-se e parte, sem responder a novos sinais.";
        _text[476, 8] = "";
        _text[476, 9] = "";

        _text[477, 0] = "Decline the offer";
        _text[477, 1] = "Отклонить предложение"; // выбор 2
        _text[477, 2] = "Refuser l'offre";
        _text[477, 3] = "Rifiutare l'offerta";
        _text[477, 4] = "Angebot ablehnen";
        _text[477, 5] = "Rechazar la oferta";
        _text[477, 6] = "Odrzucić propozycję";
        _text[477, 7] = "Recusar a proposta";
        _text[477, 8] = "";
        _text[477, 9] = "";

        _text[478, 0] = "The drones stop responding and disappear into the depths of the station.";
        _text[478, 1] = "Дроны перестают реагировать и скрываются вглубь станции."; //ничего
        _text[478, 2] = "Les drones cessent de répondre et disparaissent dans les profondeurs de la station.";
        _text[478, 3] = "I droni smettono di reagire e si ritirano nelle profondità della stazione.";
        _text[478, 4] = "Die Drohnen reagieren nicht mehr und ziehen sich in die Tiefe der Station zurück.";
        _text[478, 5] = "Los drones dejan de reaccionar y se esconden en lo profundo de la estación.";
        _text[478, 6] = "Drony przestają reagować i znikają w głębi stacji.";
        _text[478, 7] = "Os drones deixam de reagir e desaparecem no interior da estação.";
        _text[478, 8] = "";
        _text[478, 9] = "";

        // 0_SilenceFaction_Dialogue
        _text[479, 0] = "While moving in orbit around the planet, your sensors detect the approach of an alien object.\n\nThe ship is sleek and unmarked, gliding through the pitch black. It makes no signal.\n\nNo call, no warning. Just a silent drift... and approach.\n\nYou sense a slight static in your audio feeds. It's not noise-it's the absence of sound.";
        _text[479, 1] = "Во время перемещения по орбите планеты ваши сенсоры улавливают приближение чужого объекта.\n\nЭтот корабль - гладкий, без опознавательных знаков, скользящий в абсолютной тьме. Он не подаёт сигналов.\n\nНи вызова, ни предупреждения. Только безмолвный дрейф... и приближение.\n\nВы ощущаете лёгкие помехи в аудиоканалах. Это не шум - это отсутствие звука.";
        _text[479, 2] = "En orbite autour de la planète, vos capteurs détectent l'approche d'un objet extraterrestre.\n\nCe vaisseau, profilé et sans marquage, glisse dans l'obscurité totale. Il n'émet aucun signal.\n\nAucun appel, aucun avertissement. Juste une dérive silencieuse... et une approche.Vous percevez un léger grésillement dans vos transmissions audio. Ce n'est pas du bruit, c'est l'absence de son.";
        _text[479, 3] = "Durante il movimento lungo l'orbita del pianeta, i tuoi sensori captano l'avvicinarsi di un oggetto estraneo.\n\nQuesta nave è liscia, priva di segni di riconoscimento, e scivola nel buio assoluto. Non invia segnali.\n\nNé chiamate, né avvertimenti. Solo una deriva muta... e l'avvicinarsi.\n\nAvverti lievi interferenze nei canali audio. Non è rumore - è assenza di suono.";
        _text[479, 4] = "Während du dich auf der Umlaufbahn des Planeten bewegst, erfassen deine Sensoren die Annäherung eines fremden Objekts.\n\nDas Schiff - glatt, ohne Kennzeichen, gleitet in absoluter Dunkelheit. Es sendet keine Signale.\n\nKein Ruf, keine Warnung. Nur stummes Treiben... und Annäherung.\n\nDu spürst leichte Störungen in den Audiokanälen. Das ist kein Rauschen - das ist Abwesenheit von Klang.";
        _text[479, 5] = "Mientras te desplazas por la órbita del planeta, tus sensores captan el acercamiento de un objeto extraño.\n\nEsa nave - lisa, sin marcas de identificación, deslizándose en la oscuridad absoluta. No emite señales.\n\nNi llamada, ni advertencia. Solo una deriva silenciosa... y el acercamiento.\n\nSientes leves interferencias en los canales de audio. No es ruido - es ausencia de sonido.";
        _text[479, 6] = "Podczas manewrów na orbicie planety twoje sensory wyłapują zbliżający się obcy obiekt.\n\nTen statek jest gładki, bez znaków rozpoznawczych, sunie w absolutnej ciemności. Nie nadaje sygnałów.\n\nBez wezwania, bez ostrzeżenia. Tylko bezgłośny dryf... i zbliżanie się.\n\nCzujesz lekkie zakłócenia w kanałach audio. To nie szum - to brak dźwięku.";
        _text[479, 7] = "Enquanto se desloca pela órbita do planeta, os seus sensores detetam a aproximação de um objeto estranho.\n\nEsta nave - lisa, sem marcas de identificação, desliza na escuridão absoluta. Não emite sinais.\n\nNem chamada, nem aviso. Apenas um drift silencioso... e aproximação.\n\nVocê sente ligeiras interferências nos canais de áudio. Não é ruído - é ausência de som.";
        _text[479, 8] = "";
        _text[479, 9] = "";

        _text[480, 0] = "Shut down systems and engines";
        _text[480, 1] = "Отключить системы и двигатели"; // выбор 1
        _text[480, 2] = "Arrêtez les systèmes et les moteurs.";
        _text[480, 3] = "Spegnere sistemi e motori";
        _text[480, 4] = "Systeme und Antriebe abschalten";
        _text[480, 5] = "Apagar sistemas y motores";
        _text[480, 6] = "Wyłączyć systemy i silniki";
        _text[480, 7] = "Desligar sistemas e motores";
        _text[480, 8] = "";
        _text[480, 9] = "";

        _text[481, 0] = "You turn off the life support systems, ventilation, audio channels and drive.\n\nThe ship sends you a container and gradually disappears into the depths of space.";
        _text[481, 1] = "Вы гасите системы жизнеобеспечения, вентиляцию, аудиоканалы и привод.\n\nКорабль отправляет вам контейнер и постепенно исчезает в глубине космоса.";
        _text[481, 2] = "Vous coupez les systèmes de survie, la ventilation, les transmissions audio et le système de propulsion.\n\nLe vaisseau vous envoie un conteneur et disparaît peu à peu dans les profondeurs de l'espace.";
        _text[481, 3] = "Spegni i sistemi di supporto vitale, la ventilazione, i canali audio e la propulsione.\n\nLa nave ti invia un contenitore e lentamente scompare nelle profondità dello spazio.";
        _text[481, 4] = "Du fährst Lebenserhaltung, Belüftung, Audiokanäle und Antrieb herunter.\n\nDas Schiff sendet dir einen Container und verschwindet allmählich in der Tiefe des Weltraums.";
        _text[481, 5] = "Apagas los sistemas de soporte vital, la ventilación, los canales de audio y el propulsor.\n\nLa nave te envía un contenedor y desaparece lentamente en la profundidad del espacio.";
        _text[481, 6] = "Wygaszasz podtrzymanie życia, wentylację, kanały audio i napęd.\n\nStatek przesyła ci kontener i stopniowo znika w głębi kosmosu.";
        _text[481, 7] = "Você desliga os sistemas de suporte de vida, a ventilação, os canais de áudio e a propulsão.\n\nA nave envia-lhe um contentor e desaparece gradualmente na profundidade do espaço.";
        _text[481, 8] = "";
        _text[481, 9] = "";

        _text[482, 0] = "Maintain course and radio silence";
        _text[482, 1] = "Сохранять курс и радиомолчание"; // выбор 2
        _text[482, 2] = "Maintenez le cap et silence radio.";
        _text[482, 3] = "Mantenere rotta e silenzio radio";
        _text[482, 4] = "Kurs halten und Funkstille wahren";
        _text[482, 5] = "Mantener el rumbo y el silencio de radio";
        _text[482, 6] = "Utrzymać kurs i ciszę radiową";
        _text[482, 7] = "Manter o rumo e o silêncio de rádio";
        _text[482, 8] = "";
        _text[482, 9] = "";

        _text[483, 0] = "You don't interfere and continue moving.\n\nThe alien ship approaches and freezes opposite.\n\nFor a few seconds nothing happens...\n\nThen - a sound that is not in the spectrum. It is not registered by the instruments, but inside the hull - everything begins to tremble.\n\nYou feel vibration in the walls, in the contours of the hull, in the very structure of the ship.\n\nAn unknown resonance penetrates the system";
        _text[483, 1] = "Вы не вмешиваетесь и продолжаете двигаться.\n\nЧужой корабль сближается и замирает напротив.\n\nНесколько секунд ничего не происходит...\n\nЗатем - звук, которого нет в спектре. Он не регистрируется приборами, но внутри корпуса - всё начинает дрожать.\n\nВы чувствуете вибрацию в стенах, в контурах обшивки, в самой структуре корабля.\n\nНеизвестный резонанс проникает в систему"; // - ядра
        _text[483, 2] = "Vous n'intervenez pas et poursuivez votre route.\n\nLe vaisseau extraterrestre s'approche et s'arrête en face.\n\nPendant quelques secondes, rien ne se passe...Puis – un son inédit. Il n'est pas détecté par les instruments, mais à l'intérieur de la coque, tout se met à trembler.\n\nVous ressentez des vibrations dans les parois, dans les contours de la coque, dans la structure même du vaisseau.\n\nUne résonance inconnue imprègne le système.";
        _text[483, 3] = "Non intervieni e continui a muoverti.\n\nLa nave estranea si avvicina e si ferma di fronte a te.\n\nPer alcuni secondi non accade nulla...\n\nPoi - un suono che non è nello spettro. Non viene registrato dagli strumenti, ma all'interno dello scafo tutto inizia a tremare.\n\nSenti la vibrazione nelle paratie, nei contorni del rivestimento, nella stessa struttura della nave.\n\nUna risonanza sconosciuta penetra nel sistema";
        _text[483, 4] = "Du greifst nicht ein und setzt deinen Kurs fort.\n\nDas fremde Schiff kommt näher und verharrt dir gegenüber.\n\nEin paar Sekunden passiert nichts...\n\nDann - ein Klang, der nicht im Spektrum existiert. Die Instrumente erfassen ihn nicht, aber im Inneren des Rumpfes beginnt alles zu zittern.\n\nDu spürst Vibrationen in den Wänden, in den Konturen der Hülle, in der Struktur des Schiffes selbst.\n\nEine unbekannte Resonanz dringt in das System ein";
        _text[483, 5] = "No intervienes y sigues avanzando.\n\nLa nave ajena se aproxima y se detiene frente a ti.\n\nDurante unos segundos no ocurre nada...\n\nLuego - un sonido que no está en el espectro. No lo registran los instrumentos, pero dentro del casco todo empieza a temblar.\n\nSientes vibración en las paredes, en los contornos del revestimiento, en la propia estructura de la nave.\n\nUna resonancia desconocida penetra en el sistema";
        _text[483, 6] = "Nie ingerujesz i kontynuujesz ruch.\n\nObcy statek zbliża się i zastyga naprzeciwko.\n\nPrzez kilka sekund nic się nie dzieje...\n\nPotem - dźwięk spoza spektrum. Nie rejestrują go przyrządy, ale wewnątrz kadłuba wszystko zaczyna drżeć.\n\nCzujesz wibracje w ścianach, w liniach poszycia, w samej strukturze statku.\n\nNieznany rezonans przenika do systemu";
        _text[483, 7] = "Você não interfere e continua a avançar.\n\nA nave estranha aproxima-se e imobiliza-se em frente.\n\nDurante alguns segundos, nada acontece...\n\nDepois - um som que não existe no espectro. Ele não é registado pelos instrumentos, mas dentro do casco - tudo começa a tremer.\n\nVocê sente vibração nas paredes, nos contornos da blindagem, na própria estrutura da nave.\n\nUm ressonar desconhecido infiltra-se no sistema";
        _text[483, 8] = "";
        _text[483, 9] = "";

        _text[484, 0] = "Activate the protection system";
        _text[484, 1] = "Активировать систему защиты"; // выбор 3
        _text[484, 2] = "Activer le système de protection";
        _text[484, 3] = "Attivare il sistema di difesa";
        _text[484, 4] = "Schutzsystem aktivieren";
        _text[484, 5] = "Activar el sistema de defensa";
        _text[484, 6] = "Aktywować system obrony";
        _text[484, 7] = "Ativar o sistema de defesa";
        _text[484, 8] = "";
        _text[484, 9] = "";

        _text[485, 0] = "A powerful pulse of energy is emitted from the enemy ship.\n\nSuccess: you manage to shield the strike, you escaped with interference.";
        _text[485, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nУспех: вам удается экранировать удар, вы отделались помехами."; // ничего
        _text[485, 2] = "Une puissante impulsion d'énergie jaillit du vaisseau ennemi.\n\nSuccès: Vous parvenez à bloquer l'attaque et à vous échapper malgré les interférences.";
        _text[485, 3] = "Dalla nave nemica parte un impulso di energia di potenza immensa.\n\nSuccesso: riesci a schermare il colpo, te la cavi con delle interferenze.";
        _text[485, 4] = "Aus dem feindlichen Schiff schießt ein übermächtiger Energieimpuls.\n\nErfolg: dir gelingt es, den Schlag abzuschirmen, du kommst mit Störungen davon.";
        _text[485, 5] = "Desde la nave enemiga se lanza un potentísimo pulso de energía.\n\nÉxito: logras apantallar el golpe, solo sufres interferencias.";
        _text[485, 6] = "Z wrogiego statku uderza potężny impuls energii.\n\nSukces: udaje ci się osłonić uderzenie, kończy się na zakłóceniach.";
        _text[485, 7] = "Da nave inimiga parte um impulso de energia poderosíssimo.\n\nSucesso: você consegue blindar o impacto; ficou apenas com interferências.";
        _text[485, 8] = "";
        _text[485, 9] = "";

        _text[486, 0] = "A powerful energy pulse is emitted from the enemy ship.\n\nFailure: the defense system fails, the pulse penetrates the hull";
        _text[486, 1] = "Из вражеского корабля устремляется мощнейщий импульс энергии.\n\nПровал: система защиты не справляется, импульс пробивает обшивку"; //-1 ядро ии
        _text[486, 2] = "Une puissante impulsion énergétique jaillit du vaisseau ennemi.\n\nÉchec: Le système de défense est défaillant, l’impulsion pénètre la coque.";
        _text[486, 3] = "Dalla nave nemica parte un impulso di energia di potenza immensa.\n\nFallimento: il sistema di difesa non regge, l'impulso perfora il rivestimento";
        _text[486, 4] = "Aus dem feindlichen Schiff schießt ein übermächtiger Energieimpuls.\n\nMisserfolg: Das Schutzsystem hält nicht stand, der Impuls durchschlägt die Hülle";
        _text[486, 5] = "Desde la nave enemiga se lanza un potentísimo pulso de energía.\n\nFracaso: el sistema de defensa no aguanta, el pulso perfora el casco";
        _text[486, 6] = "Z wrogiego statku uderza potężny impuls energii.\n\nPorażka: system obrony nie wytrzymuje, impuls przebija poszycie";
        _text[486, 7] = "Da nave inimiga parte um impulso de energia poderosíssimo.\n\nFracasso: o sistema de defesa não aguenta; o impulso perfura o revestimento";
        _text[486, 8] = "";
        _text[486, 9] = "";

        _text[487, 0] = "The container is carefully captured by drones. Not a single active signal, not a single threat.\n\nInside is a sealed case with markings unknown to your database.";
        _text[487, 1] = "Контейнер аккуратно захватывается дронами. Ни одного активного сигнала, ни одной угрозы.\n\nВнутри - герметичный кейс с маркировкой, неизвестной вашей базе данных.";
        _text[487, 2] = "Le conteneur est soigneusement repéré par des drones. Aucun signal actif, aucune menace.\n\nÀ l'intérieur se trouve un coffret scellé portant des inscriptions inconnues de votre base de données.";
        _text[487, 3] = "Il contenitore viene afferrato con cura dai droni. Nessun segnale attivo, nessuna minaccia.\n\nAll'interno - una custodia ermetica con una marcatura sconosciuta alla tua base dati.";
        _text[487, 4] = "Der Container wird von Drohnen vorsichtig eingefangen. Kein aktives Signal, keine Bedrohung.\n\nInnen - ein hermetischer Koffer mit einer Markierung, die deiner Datenbank unbekannt ist.";
        _text[487, 5] = "Los drones capturan el contenedor con cuidado. Ni una señal activa, ni una amenaza.\n\nDentro hay un maletín hermético con un marcaje desconocido para tu base de datos.";
        _text[487, 6] = "Kontener zostaje ostrożnie przechwycony przez drony. Ani jednego aktywnego sygnału, ani jednego zagrożenia.\n\nW środku znajduje się hermetyczna walizka z oznaczeniem nieznanym twojej bazie danych.";
        _text[487, 7] = "O contentor é recolhido cuidadosamente pelos drones. Nenhum sinal ativo, nenhuma ameaça.\n\nNo interior - um estojo hermético com uma marcação desconhecida para a sua base de dados.";
        _text[487, 8] = "";
        _text[487, 9] = "";

        _text[488, 0] = "Open case";
        _text[488, 1] = "Открыть кейс"; //выбор 1.1
        _text[488, 2] = "Étui ouvert";
        _text[488, 3] = "Aprire la custodia";
        _text[488, 4] = "Koffer öffnen";
        _text[488, 5] = "Abrir el maletín";
        _text[488, 6] = "Otworzyć walizkę";
        _text[488, 7] = "Abrir o estojo";
        _text[488, 8] = "";
        _text[488, 9] = "";

        _text[489, 0] = "Throw the case into space";
        _text[489, 1] = "Выбросить кейс в космос"; //выбор 1.2
        _text[489, 2] = "Jetez la valise dans l'espace";
        _text[489, 3] = "Gettare la custodia nello spazio";
        _text[489, 4] = "Koffer ins All werfen";
        _text[489, 5] = "Arrojar el maletín al espacio";
        _text[489, 6] = "Wyrzucić walizkę w kosmos";
        _text[489, 7] = "Atirar o estojo ao espaço";
        _text[489, 8] = "";
        _text[489, 9] = "";

        _text[490, 0] = "You open the case...";
        _text[490, 1] = "Вы открываете кейс..."; // + ядра или + квант
        _text[490, 2] = "Vous ouvrez la mallette...";
        _text[490, 3] = "Apri la custodia...";
        _text[490, 4] = "Du öffnest den Koffer...";
        _text[490, 5] = "Abres el maletín...";
        _text[490, 6] = "Otwierasz walizkę...";
        _text[490, 7] = "Você abre o estojo...";
        _text[490, 8] = "";
        _text[490, 9] = "";

        _text[491, 0] = "You decide not to take the risk and throw the case into space, but you are overcome by the feeling of losing something of great value...";
        _text[491, 1] = "Вы решаете не рисковать и выбрасываете кейс в космос, но вас охватывает чувство потери большой ценности..."; // ничего
        _text[491, 2] = "Vous décidez de ne pas prendre de risques et de jeter la valise dans l'espace, mais vous êtes envahi par le sentiment de perdre quelque chose de grande valeur...";
        _text[491, 3] = "Decidi di non rischiare e getti la custodia nello spazio, ma ti assale la sensazione di aver perso qualcosa di enorme valore...";
        _text[491, 4] = "Du entscheidest dich, kein Risiko einzugehen, und wirfst den Koffer ins All, doch dich überkommt das Gefühl, etwas von großem Wert verloren zu haben...";
        _text[491, 5] = "Decides no arriesgarte y arrojas el maletín al espacio, pero te invade la sensación de haber perdido algo de gran valor...";
        _text[491, 6] = "Postanawiasz nie ryzykować i wyrzucasz walizkę w kosmos, ale ogarnia cię uczucie utraty czegoś bardzo cennego...";
        _text[491, 7] = "Você decide não arriscar e atira o estojo ao espaço, mas é tomado por uma sensação de ter perdido algo de grande valor...";
        _text[491, 8] = "";
        _text[491, 9] = "";

        // 0_FilthCultFaction_Dialogue      
        _text[492, 0] = "You approach a foggy station, covered in moss and organic matter. A pulsating voice is transmitted over the comm channel:\n\n\"Let your frame accept the sprout. The filth does not destroy - it creates.\"";
        _text[492, 1] = "Вы приближаетесь к туманной станции, облепленной мхом и органикой. Коммуникационный канал передаёт пульсирующий голос:\n\n\"Пусть твой корпус примет росток. Скверна не разрушает - она творит.\"";
        _text[492, 2] = "Vous approchez d'une station enveloppée de brouillard, recouverte de mousse et de matières organiques. Une voix pulsante résonne dans le canal de communication:\n\n\"Laissez votre enveloppe accepter la germination. La corruption ne détruit pas, elle crée.\"";
        _text[492, 3] = "Ti avvicini a una stazione nebbiosa, ricoperta di muschio e materia organica. Il canale di comunicazione trasmette una voce pulsante:\n\n\"Che il tuo scafo accolga il germoglio. La corruzione non distrugge - crea.\"";
        _text[492, 4] = "Du näherst dich einer nebligen Station, überwuchert von Moos und Organik. Der Kommunikationskanal überträgt eine pulsierende Stimme:\n\n\"Lass deine Hülle den Keim annehmen. Die Verderbnis zerstört nicht - sie erschafft.\"";
        _text[492, 5] = "Te acercas a una estación brumosa, cubierta de musgo y materia orgánica. El canal de comunicación transmite una voz pulsante:\n\n\"Que tu casco acepte el brote. La plaga no destruye: crea.\"";
        _text[492, 6] = "Zbliżasz się do mglistej stacji oblepionej mchem i organiką. Kanał komunikacyjny przekazuje pulsujący głos:\n\n\"Niech twój kadłub przyjmie kiełek. Skaza nie niszczy - ona tworzy.\"";
        _text[492, 7] = "Você aproxima-se de uma estação enevoada, coberta de musgo e matéria orgânica. O canal de comunicação transmite uma voz pulsante:\n\n\"Que o teu casco aceite o rebento. A corrupção não destrói - ela cria.\"";
        _text[492, 8] = "";
        _text[492, 9] = "";

        _text[493, 0] = "Accept the gift";
        _text[493, 1] = "Принять дар"; //выбор 1
        _text[493, 2] = "Acceptez le cadeau";
        _text[493, 3] = "Accettare il dono";
        _text[493, 4] = "Das Geschenk annehmen";
        _text[493, 5] = "Aceptar el regalo";
        _text[493, 6] = "Przyjąć dar";
        _text[493, 7] = "Aceitar o presente";
        _text[493, 8] = "";
        _text[493, 9] = "";

        _text[494, 0] = "The organism grows in the cargo bay.\n\nSuccess: it synchronizes with the ship's systems, causing strange images.";
        _text[494, 1] = "Организм прорастает в грузовом отсеке.\n\nУспех: он синхронизируется с системами корабля, вызывая странные образы."; // + фрагменты
        _text[494, 2] = "L'organisme se développe dans la cale.\n\nSuccès: il se synchronise avec les systèmes du navire, provoquant d'étranges images.";
        _text[494, 3] = "L'organismo germoglia nel vano di carico.\n\nSuccesso: si sincronizza con i sistemi della nave, evocando immagini strane.";
        _text[494, 4] = "Der Organismus sprießt im Frachtraum.\n\nErfolg: Er synchronisiert sich mit den Schiffssystemen und ruft seltsame Bilder hervor.";
        _text[494, 5] = "El organismo brota en la bodega.\n\nÉxito: se sincroniza con los sistemas de la nave, provocando imágenes extrañas.";
        _text[494, 6] = "Organizm kiełkuje w ładowni.\n\nSukces: synchronizuje się z systemami statku, wywołując dziwne wizje.";
        _text[494, 7] = "O organismo brota no porão de carga.\n\nSucesso: ele sincroniza-se com os sistemas da nave, provocando imagens estranhas.";
        _text[494, 8] = "";
        _text[494, 9] = "";

        _text[495, 0] = "The organism grows in the cargo bay.\n\nFailure: the Corruption gets out of control. The virus penetrates the control network, causing one of the cores to fail fatally.";
        _text[495, 1] = "Организм прорастает в грузовом отсеке.\n\nПровал: скверна выходит из-под контроля. Вирус проникает в управляющую сеть, приводя к фатальному сбою одного из ядер."; // -1 ядро
        _text[495, 2] = "L'organisme se développe dans la soute.\n\nDéfaillance: La corruption devient incontrôlable. Le virus infiltre le réseau de contrôle, provoquant la défaillance fatale d'un des cœurs.";
        _text[495, 3] = "L'organismo germoglia nel vano di carico.\n\nFallimento: la corruzione sfugge al controllo. Un virus penetra nella rete di comando, causando il guasto fatale di uno dei nuclei.";
        _text[495, 4] = "Der Organismus sprießt im Frachtraum.\n\nMisserfolg: Die Verderbnis gerät außer Kontrolle. Ein Virus dringt in das Steuerungsnetz ein und verursacht den fatalen Ausfall eines der Kerne.";
        _text[495, 5] = "El organismo brota en la bodega.\n\nFracaso: la plaga se sale de control. El virus penetra en la red de control, provocando el fallo fatal de uno de los núcleos.";
        _text[495, 6] = "Organizm kiełkuje w ładowni.\n\nPorażka: skaza wymyka się spod kontroli. Wirus przenika do sieci sterującej, powodując fatalną awarię jednego z rdzeni.";
        _text[495, 7] = "O organismo brota no porão de carga.\n\nFracasso: a corrupção sai do controlo. Um vírus infiltra-se na rede de controlo, levando a uma falha fatal de um dos núcleos.";
        _text[495, 8] = "";
        _text[495, 9] = "";

        _text[496, 0] = "Refuse and move away";
        _text[496, 1] = "Отказаться и отойти"; //выбор 2
        _text[496, 2] = "Refusez et partez.";
        _text[496, 3] = "Rifiutare e allontanarsi";
        _text[496, 4] = "Ablehnen und zurückweichen";
        _text[496, 5] = "Rechazar y alejarse";
        _text[496, 6] = "Odmówić i odejść";
        _text[496, 7] = "Recusar e afastar-se";
        _text[496, 8] = "";
        _text[496, 9] = "";

        _text[497, 0] = "You slowly move away from the station, but you feel that it is too late - the spores have penetrated the ship's ventilation.";
        _text[497, 1] = "Вы медленно отдаляетесь от станции, но чувствуете, что уже слишком поздно - споры внедрились в вентиляцию корабля.";
        _text[497, 2] = "Vous vous éloignez lentement de la station, mais vous sentez qu'il est trop tard : les spores ont pénétré le système de ventilation du vaisseau.";
        _text[497, 3] = "Ti allontani lentamente dalla stazione, ma senti che è già troppo tardi - le spore si sono infiltrate nella ventilazione della nave.";
        _text[497, 4] = "Du entfernst dich langsam von der Station, doch du fühlst, dass es bereits zu spät ist - die Sporen haben sich in die Belüftung des Schiffes gesetzt.";
        _text[497, 5] = "Te alejas lentamente de la estación, pero sientes que ya es demasiado tarde - las esporas se han infiltrado en la ventilación de la nave.";
        _text[497, 6] = "Powoli oddalasz się od stacji, ale czujesz, że jest już za późno - zarodniki wniknęły do wentylacji statku.";
        _text[497, 7] = "Você afasta-se lentamente da estação, mas sente que já é tarde demais - esporos infiltraram-se na ventilação da nave.";
        _text[497, 8] = "";
        _text[497, 9] = "";

        _text[498, 0] = "Success: you initiate internal cleansing protocols - the ship is successfully cleaned.";
        _text[498, 1] = "Успех: вы запускаете протоколы внутренней очистки - корабль успешно очищен."; // ничего
        _text[498, 2] = "Succès : Vous lancez les protocoles de nettoyage interne – le navire est nettoyé avec succès.";
        _text[498, 3] = "Successo: avvii i protocolli di purificazione interna - la nave viene ripulita con successo.";
        _text[498, 4] = "Erfolg: du startest die internen Reinigungsprotokolle - das Schiff wird erfolgreich gereinigt.";
        _text[498, 5] = "Éxito: activas los protocolos de limpieza interna: la nave queda completamente limpia.";
        _text[498, 6] = "Sukces: uruchamiasz protokoły wewnętrznego oczyszczania - statek został skutecznie oczyszczony.";
        _text[498, 7] = "Sucesso: você inicia os protocolos de limpeza interna - a nave é limpa com sucesso.";
        _text[498, 8] = "";
        _text[498, 9] = "";

        _text[499, 0] = "Failure: a spore enters the life support module, causing a malfunction.";
        _text[499, 1] = "Провал: спора проникает в модуль жизнеобеспечения, вызывая сбой"; // - ядро
        _text[499, 2] = "Défaillance : Une spore pénètre dans le module de survie, provoquant un dysfonctionnement.";
        _text[499, 3] = "Fallimento: una spora penetra nel modulo di supporto vitale, causando un guasto";
        _text[499, 4] = "Misserfolg: Eine Spore dringt in das Lebenserhaltungsmodul ein und verursacht einen Ausfall";
        _text[499, 5] = "Fracaso: una espora penetra en el módulo de soporte vital, provocando un fallo";
        _text[499, 6] = "Porażka: zarodnik przenika do modułu podtrzymania życia, powodując awarię";
        _text[499, 7] = "Fracasso: um esporo infiltra-se no módulo de suporte de vida, causando uma falha";
        _text[499, 8] = "";
        _text[499, 9] = "";

        _text[500, 0] = "Perform external cleaning";
        _text[500, 1] = "Провести внешнюю очистку"; //выбор 3
        _text[500, 2] = "Effectuer le nettoyage externe";
        _text[500, 3] = "Eseguire una pulizia esterna";
        _text[500, 4] = "Äußere Reinigung durchführen";
        _text[500, 5] = "Realizar una limpieza externa";
        _text[500, 6] = "Przeprowadzić zewnętrzne oczyszczenie";
        _text[500, 7] = "Realizar uma limpeza externa";
        _text[500, 8] = "";
        _text[500, 9] = "";

        _text[501, 0] = "You initiate an external cleansing of the infected ship: you direct a concentrated laser at the biomass foci and block the infection signals.";
        _text[501, 1] = "Вы запускаете внешнюю очистку заражённого корабля: направляете концентрированный лазер на очаги биомассы и блокируете сигналы заражения.";
        _text[501, 2] = "Vous lancez un nettoyage externe du vaisseau infecté : vous dirigez un laser concentré vers les foyers de biomasse et bloquez les signaux d'infection.";
        _text[501, 3] = "Avvii una pulizia esterna della nave infetta: punti un laser concentrato sui focolai di biomassa e blocchi i segnali dell'infezione.";
        _text[501, 4] = "Du startest die äußere Reinigung des infizierten Schiffes: Du richtest einen konzentrierten Laser auf die Biomasse-Herde und blockierst die Infektionssignale.";
        _text[501, 5] = "Inicias una limpieza externa de la nave infectada: apuntas un láser concentrado a los focos de biomasa y bloqueas las señales de infección.";
        _text[501, 6] = "Uruchamiasz zewnętrzne oczyszczanie zakażonego statku: kierujesz skoncentrowany laser na ogniska biomasy i blokujesz sygnały zakażenia.";
        _text[501, 7] = "Você inicia a limpeza externa da nave infetada: aponta um laser concentrado para os focos de biomassa e bloqueia os sinais de infeção.";
        _text[501, 8] = "";
        _text[501, 9] = "";

        _text[502, 0] = "Success: the cleansing is successful - the organism is destroyed, you take resources from the station.";
        _text[502, 1] = "Успех: очистка проходит успешно - организм уничтожен, вы забираете ресурсы со станции."; // + квант
        _text[502, 2] = "Succès : L'opération de purification est un succès – l'organisme est détruit, vous prélevez des ressources de la station.";
        _text[502, 3] = "Successo: la pulizia riesce - l'organismo è distrutto, recuperi risorse dalla stazione.";
        _text[502, 4] = "Erfolg: Die Reinigung verläuft erfolgreich - der Organismus ist zerstört, du nimmst Ressourcen von der Station.";
        _text[502, 5] = "Éxito: la limpieza se completa con éxito: el organismo es destruido y recoges recursos de la estación.";
        _text[502, 6] = "Sukces: oczyszczanie przebiega pomyślnie - organizm zostaje zniszczony, a ty zabierasz zasoby ze stacji.";
        _text[502, 7] = "Sucesso: a limpeza é bem-sucedida - o organismo é destruído, e você recolhe recursos da estação.";
        _text[502, 8] = "";
        _text[502, 9] = "";

        _text[503, 0] = "Failure: the infection goes deeper - the system overheats and one of the neurosections fails.";
        _text[503, 1] = "Провал: заражение оказывается глубже - система перегревается, и одна из нейросекций выходит из строя."; // - ядро
        _text[503, 2] = "Échec : L'infection s'étend plus profondément – ​​le système surchauffe et l'une des sections neurales tombe en panne.";
        _text[503, 3] = "Fallimento: l'infezione è più profonda - il sistema si surriscalda e una delle neurosezioni va fuori uso.";
        _text[503, 4] = "Misserfolg: Die Infektion sitzt tiefer - das System überhitzt, und eine der Neurosektionen fällt aus.";
        _text[503, 5] = "Fracaso: la infección es más profunda: el sistema se sobrecalienta, y una de las neurosecciones queda fuera de servicio.";
        _text[503, 6] = "Porażka: zakażenie okazuje się głębsze - system się przegrzewa i jedna z neurosekcji ulega awarii.";
        _text[503, 7] = "Fracasso: a infeção é mais profunda - o sistema sobreaquece e uma das neurosecções falha.";
        _text[503, 8] = "";
        _text[503, 9] = "";

        // ResourceTraderNode
        _text[504, 0] = "You approach a rusty station littered with containers and garbage. A faint, crackling signal comes over the airwaves:\n\n\"Who's there? Don't shoot. I'm just trading. I've got something the rest of us don't have - if you're willing to pay, of course.\"";
        _text[504, 1] = "Вы приближаетесь к ржавой станции, заваленной контейнерами и мусором. В эфире появляется слабый, потрескивающий сигнал:\n\n\"Эй, кто там? Не стреляй. Я просто торгую. У меня есть то, чего нет у остальных - если ты, конечно, готов заплатить.\"";
        _text[504, 2] = "Vous approchez d'une station-service délabrée, jonchée de conteneurs et d'ordures. Un faible signal crépitant apparaît sur les ondes:\n\n\"Hé, qui est là ? Ne tirez pas. Je fais juste du troc. J'ai quelque chose que vous n'avez pas, si vous êtes prêts à payer, bien sûr.\"";
        _text[504, 3] = "Ti avvicini a una stazione arrugginita, sepolta sotto contenitori e spazzatura. Nell'etere compare un segnale debole e crepitante:\n\n\"Ehi, chi c'è? Non sparare. Io commercio e basta. Ho qualcosa che gli altri non hanno - se, naturalmente, sei disposto a pagare.\"";
        _text[504, 4] = "Du näherst dich einer rostigen Station, zugeschüttet mit Containern und Müll. Im Äther erscheint ein schwaches, knisterndes Signal:\n\n\"Hey, wer ist da? Nicht schießen. Ich handle nur. Ich habe etwas, das die anderen nicht haben - wenn du natürlich bereit bist zu zahlen.\"";
        _text[504, 5] = "Te acercas a una estación oxidada, abarrotada de contenedores y basura. En el éter aparece una señal débil y crepitante:\n\n\"Eh, ¿quién anda ahí? No dispares. Solo comercio. Tengo algo que los demás no tienen: si, claro, estás dispuesto a pagar.\"";
        _text[504, 6] = "Zbliżasz się do zardzewiałej stacji zawalonej kontenerami i śmieciami. W eterze pojawia się słaby, trzaskający sygnał:\n\n\"Hej, kto tam? Nie strzelaj. Ja tylko handluję. Mam coś, czego nie mają inni - o ile, oczywiście, jesteś gotów zapłacić.\"";
        _text[504, 7] = "Você aproxima-se de uma estação enferrujada, cheia de contentores e lixo. No éter surge um sinal fraco e crepitante:\n\n\"Ei, quem está aí? Não dispares. Eu só negocio. Tenho o que os outros não têm - se, claro, estiveres disposto a pagar.\"";
        _text[504, 8] = "";
        _text[504, 9] = "";

        _text[505, 0] = "Trade";
        _text[505, 1] = "Торговать";
        _text[505, 2] = "Commerce";
        _text[505, 3] = "Commerciare";
        _text[505, 4] = "Handeln";
        _text[505, 5] = "Comerciar";
        _text[505, 6] = "Handlować";
        _text[505, 7] = "Negociar";
        _text[505, 8] = "";
        _text[505, 9] = "";

        _text[506, 0] = "Ignore";
        _text[506, 1] = "Игнорировать";
        _text[506, 2] = "Ignorer";
        _text[506, 3] = "Ignorare";
        _text[506, 4] = "Ignorieren";
        _text[506, 5] = "Ignorar";
        _text[506, 6] = "Zignorować";
        _text[506, 7] = "Ignorar";
        _text[506, 8] = "";
        _text[506, 9] = "";

        // 0_ResourceDialogue
        _text[507, 0] = "You continue to orbit the abandoned communications satellite when a dull thud is heard. One of the external sensors is damaged. Upon inspection, a stuck cargo container is discovered. The markings on the casing are erased, the symbol is illegible.\n\nInside lies a sealed case surrounded by wires, a biometric lock and an emitter.\n\nJudging by the logs, the cargo has been drifting in orbit for over 200 years.";
        _text[507, 1] = "Вы продолжаете движение по орбите заброшенного спутника связи, когда раздаётся глухой удар. Один из внешних сенсоров - повреждён. При проверке обнаружен застрявший грузовой контейнер. Метки на корпусе стерлись, символ не разобрать.\n\nВнутри лежит запечатанный кейс, окруженный проводами, биометрическим замком и эмиттером\n\nСудя по логам груз дрейфует по орбите более 200 лет.";
        _text[507, 2] = "Vous poursuivez votre orbite autour du satellite de communication abandonné lorsqu'un bruit sourd se fait entendre. Un des capteurs externes est endommagé. Une inspection révèle un conteneur de fret bloqué. Les marquages ​​sur le boîtier sont effacés et le symbole est illisible.\n\nÀ l'intérieur se trouve un conteneur scellé, entouré de câbles, d'une serrure biométrique et d'un émetteur.\n\nD'après les journaux de bord, le fret dérive en orbite depuis plus de 200 ans.";
        _text[507, 3] = "Mentre ti muovi lungo l'orbita di un satellite di comunicazione abbandonato, risuona un colpo sordo. Uno dei sensori esterni è danneggiato. Durante l'ispezione trovi un contenitore cargo incastrato. Le marcature sullo scafo si sono consumate, il simbolo è illeggibile.\n\nAll'interno c'è una custodia sigillata, circondata da cavi, una serratura biometrica e un emettitore\n\nDai log risulta che il carico deriva in orbita da oltre 200 anni.";
        _text[507, 4] = "Du setzt deinen Kurs auf der Umlaufbahn eines verlassenen Kommunikationssatelliten fort, als ein dumpfer Schlag ertönt. Einer der Außensensoren ist beschädigt. Bei der Prüfung wird ein festgeklemmter Frachtscontainer entdeckt. Die Markierungen auf der Hülle sind abgeschliffen, das Symbol ist nicht zu erkennen.\n\nIm Inneren liegt ein versiegelter Koffer, umgeben von Kabeln, einem biometrischen Schloss und einem Emitter\n\nDen Logs zufolge treibt die Fracht seit über 200 Jahren in der Umlaufbahn.";
        _text[507, 5] = "Sigues moviéndote por la órbita de un satélite de comunicaciones abandonado cuando se oye un golpe sordo. Uno de los sensores externos está dañado. Al comprobarlo, encuentras un contenedor de carga atascado. Las marcas del casco se han borrado; el símbolo es ilegible.\n\nDentro hay un maletín sellado, rodeado de cables, una cerradura biométrica y un emisor\n\nSegún los registros, la carga lleva a la deriva en órbita más de 200 años.";
        _text[507, 6] = "Kontynuujesz lot po orbicie opuszczonego satelity łączności, gdy rozlega się głuche uderzenie. Jeden z zewnętrznych sensorów zostaje uszkodzony. Podczas kontroli znajdujesz zaklinowany kontener transportowy. Oznaczenia na kadłubie starły się, symbolu nie da się rozpoznać.\n\nW środku leży zapieczętowana walizka, otoczona przewodami, zamkiem biometrycznym i emiterem\n\nZ logów wynika, że ładunek dryfuje po orbicie od ponad 200 lat.";
        _text[507, 7] = "Você continua a deslocar-se pela órbita de um satélite de comunicações abandonado quando se ouve um baque surdo. Um dos sensores externos está danificado. Ao verificar, encontra um contentor de carga preso. As marcas no casco apagaram-se; o símbolo é ilegível.\n\nDentro há um estojo selado, rodeado de fios, um fecho biométrico e um emissor\n\nPelos logs, a carga deriva em órbita há mais de 200 anos.";
        _text[507, 8] = "";
        _text[507, 9] = "";

        _text[508, 0] = "Open";
        _text[508, 1] = "Открыть"; //выбор 1
        _text[508, 2] = "Ouvrir";
        _text[508, 3] = "Aprire";
        _text[508, 4] = "Öffnen";
        _text[508, 5] = "Abrir";
        _text[508, 6] = "Otworzyć";
        _text[508, 7] = "Abrir";
        _text[508, 8] = "";
        _text[508, 9] = "";

        _text[509, 0] = "You carefully open the container. Inside is a supply of old building materials.\n\nWhile some of the cargo is damaged by time, much is still usable. You load the materials into the storage facility.";
        _text[509, 1] = "Вы аккуратно вскрываете контейнер. Внутри - запас старых строительных материалов.\n\nХотя часть груза повреждена временем, многое всё ещё пригодно для использования. Вы загружаете материалы в хранилище."; // + случайный ресурс
        _text[509, 2] = "Vous ouvrez prudemment le conteneur. À l'intérieur se trouve un stock de vieux matériaux de construction.\n\nSi une partie de la cargaison est endommagée par le temps, une grande partie est encore utilisable. Vous chargez les matériaux dans la zone de stockage.";
        _text[509, 3] = "Apri con cautela il contenitore. Dentro c'è una scorta di vecchi materiali da costruzione.\n\nAnche se parte del carico è stato rovinato dal tempo, molto è ancora utilizzabile. Carichi i materiali nel deposito.";
        _text[509, 4] = "Du öffnest den Container vorsichtig. Innen - ein Vorrat alter Baumaterialien.\n\nObwohl ein Teil der Fracht durch die Zeit beschädigt ist, ist vieles noch nutzbar. Du lädst die Materialien ins Lager.";
        _text[509, 5] = "Abres con cuidado el contenedor. Dentro hay una reserva de viejos materiales de construcción.\n\nAunque parte de la carga ha sido dañada por el tiempo, mucho aún sirve. Cargas los materiales en el almacén.";
        _text[509, 6] = "Ostrożnie otwierasz kontener. W środku znajduje się zapas starych materiałów budowlanych.\n\nChoć część ładunku ucierpiała z upływem czasu, wiele wciąż nadaje się do użytku. Ładujesz materiały do magazynu.";
        _text[509, 7] = "Você abre o contentor com cuidado. Dentro - um stock de antigos materiais de construção.\n\nEmbora parte da carga esteja danificada pelo tempo, muita coisa ainda é utilizável. Você coloca os materiais no armazém.";
        _text[509, 8] = "";
        _text[509, 9] = "";

        _text[510, 0] = "Ignore";
        _text[510, 1] = "Игнорировать"; //выбор 2
        _text[510, 2] = "Ignorer";
        _text[510, 3] = "Ignorare";
        _text[510, 4] = "Ignorieren";
        _text[510, 5] = "Ignorar";
        _text[510, 6] = "Zignorować";
        _text[510, 7] = "Ignorar";
        _text[510, 8] = "";
        _text[510, 9] = "";

        _text[511, 0] = "You decide not to take any chances: the unknown container may be unstable or contaminated. You undock it and drop it back into the void.\n\nThe container slowly disappears from view. Perhaps someone else will stumble upon it someday.";
        _text[511, 1] = "Вы решаете не рисковать: неизвестный контейнер может быть нестабилен или заражён. Его отстыковывают и сбрасывают обратно в пустоту.\n\nКонтейнер медленно исчезает из зоны видимости. Возможно, кто-то другой когда-нибудь наткнётся на него."; // ничего
        _text[511, 2] = "Vous décidez de ne prendre aucun risque: le conteneur inconnu pourrait être instable ou contaminé. Il est désamarré et relâché dans le vide.\n\nLe conteneur disparaît lentement de la vue. Peut-être que quelqu'un d'autre le découvrira un jour.";
        _text[511, 3] = "Decidi di non rischiare: un contenitore sconosciuto potrebbe essere instabile o infetto. Viene sganciato e gettato di nuovo nel vuoto.\n\nIl contenitore svanisce lentamente dalla vista. Forse un giorno qualcun altro ci si imbatterà.";
        _text[511, 4] = "Du beschließt, kein Risiko einzugehen: Der unbekannte Container könnte instabil oder kontaminiert sein. Er wird abgekoppelt und zurück in die Leere gestoßen.\n\nDer Container verschwindet langsam aus dem Sichtfeld. Vielleicht stößt irgendwann jemand anderes darauf.";
        _text[511, 5] = "Decides no arriesgarte: el contenedor desconocido puede ser inestable o estar contaminado. Lo desacoplan y lo arrojan de vuelta al vacío.\n\nEl contenedor desaparece lentamente de la vista. Quizá alguien más lo encuentre algún día.";
        _text[511, 6] = "Postanawiasz nie ryzykować: nieznany kontener może być niestabilny lub skażony. Odczepiacie go i zrzucacie z powrotem w pustkę.\n\nKontener powoli znika z pola widzenia. Być może ktoś inny kiedyś na niego trafi.";
        _text[511, 7] = "Você decide não arriscar: o contentor desconhecido pode ser instável ou estar infetado. Ele é desacoplado e lançado de volta ao vazio.\n\nO contentor desaparece lentamente do campo de visão. Talvez alguém, um dia, o encontre.";
        _text[511, 8] = "";
        _text[511, 9] = "";

        // 0_MaterialDialogue       
        _text[512, 0] = "While scanning the surface of an old orbital ring, you spot the remains of an automated workshop. It is offline, but its frame is intact and its systems are in suspended animation.\n\nWhen you dock, you find a half-destroyed production bay inside. The robots are motionless, the air is thick with dust and a metallic taste, but a green light is blinking in one of the sealed bays: the manufacturing cycle is complete.";
        _text[512, 1] = "При сканировании поверхности старого орбитального кольца вы замечаете остатки автоматической мастерской. Она отключена, но её каркас цел, а системы - в анабиозе.\n\nПристыковавшись, вы находите внутри полуразрушенный производственный отсек. Роботы не двигаются, воздух насыщен пылью и металлическим привкусом, но в одном из запечатанных отсеков мигает зелёный индикатор: завершён цикл изготовления.";
        _text[512, 2] = "En scrutant la surface d'un ancien anneau orbital, vous apercevez les vestiges d'un atelier automatisé. Hors service, sa structure est intacte et ses systèmes sont en veille.\n\nUne fois amarré, vous découvrez à l'intérieur une zone de production délabrée. Les robots sont immobiles, l'air est saturé de poussière et dégage une odeur métallique, mais un voyant vert clignote dans l'une des baies scellées: le cycle de fabrication est terminé.";
        _text[512, 3] = "Scansionando la superficie di un vecchio anello orbitale, noti i resti di un'officina automatica. È spenta, ma il suo telaio è intatto e i sistemi sono in ibernazione.\n\nDopo l'attracco, all'interno trovi un compartimento di produzione semidistrutto. I robot non si muovono, l'aria è satura di polvere e di un sapore metallico, ma in uno dei vani sigillati lampeggia un indicatore verde: il ciclo di fabbricazione è completato.";
        _text[512, 4] = "Beim Scannen der Oberfläche eines alten Orbitalrings bemerkst du die Überreste einer automatischen Werkstatt. Sie ist abgeschaltet, aber ihr Rahmen ist intakt, und die Systeme sind im Stand-by.\n\nNach dem Andocken findest du darin eine halb zerstörte Produktionssektion. Die Roboter bewegen sich nicht, die Luft ist voll Staub und metallischem Geschmack, aber in einer der versiegelten Sektionen blinkt eine grüne Anzeige: der Fertigungszyklus ist abgeschlossen.";
        _text[512, 5] = "Al escanear la superficie de un antiguo anillo orbital, detectas restos de un taller automático. Está apagado, pero su armazón está intacto y los sistemas en hibernación.\n\nAl acoplarte, encuentras dentro un compartimento de producción semiderruido. Los robots no se mueven, el aire está cargado de polvo y un sabor metálico, pero en uno de los compartimentos sellados parpadea un indicador verde: el ciclo de fabricación ha terminado.";
        _text[512, 6] = "Podczas skanowania powierzchni starego pierścienia orbitalnego dostrzegasz pozostałości automatycznego warsztatu. Jest wyłączony, ale jego szkielet jest cały, a systemy znajdują się w anabiozie.\n\nPo zadokowaniu znajdujesz w środku częściowo zrujnowany przedział produkcyjny. Roboty się nie poruszają, powietrze jest nasycone pyłem i metalicznym posmakiem, lecz w jednym z zapieczętowanych sektorów miga zielony wskaźnik: cykl wytwarzania został ukończony.";
        _text[512, 7] = "Ao escanear a superfície de um antigo anel orbital, você nota os restos de uma oficina automática. Está desligada, mas a sua estrutura está intacta e os sistemas - em animação suspensa.\n\nAo acoplar, você encontra no interior um compartimento de produção parcialmente destruído. Os robôs não se movem, o ar está saturado de pó e de um sabor metálico, mas num dos compartimentos selados pisca um indicador verde: o ciclo de fabrico foi concluído.";
        _text[512, 8] = "";
        _text[512, 9] = "";

        _text[513, 0] = "Open the vault";
        _text[513, 1] = "Вскрыть хранилище"; //выбор 1
        _text[513, 2] = "Ouvrez le coffre-fort";
        _text[513, 3] = "Forzare il deposito";
        _text[513, 4] = "Lagerraum aufbrechen";
        _text[513, 5] = "Abrir el almacén";
        _text[513, 6] = "Otworzyć magazyn";
        _text[513, 7] = "Abrir o armazém";
        _text[513, 8] = "";
        _text[513, 9] = "";

        _text[514, 0] = "You manually open the compartment and extract the result of the old automated process.\n\nOn the platform lies a box of processed materials: polished alloys, stabilized ceramics, and packages of synthetic fabric.\n\nEverything is neatly labeled, as if it had been waiting for its owner.";
        _text[514, 1] = "Вы вручную открываете отсек и извлекаете результат старого автоматического процесса.\n\nНа платформе лежит ящик с обработанными материалами: отшлифованные сплавы, стабилизированная керамика и упаковки с синтетической тканью.\n\nВсё аккуратно промаркировано, как будто ждало хозяина."; // + случайный материал
        _text[514, 2] = "Vous ouvrez manuellement le compartiment et en retirez le résidu d'un ancien processus automatisé.\n\nSur le quai repose une caisse de matériaux transformés: alliages broyés, céramiques stabilisées et paquets de tissu synthétique.Chaque pièce est soigneusement étiquetée, comme si elle attendait son propriétaire.";
        _text[514, 3] = "Apri manualmente il vano ed estrai il risultato del vecchio processo automatico.\n\nSulla piattaforma c'è una cassa con materiali lavorati: leghe lucidate, ceramica stabilizzata e pacchi di tessuto sintetico.\n\nTutto è accuratamente etichettato, come se avesse aspettato il proprietario.";
        _text[514, 4] = "Du öffnest die Sektion von Hand und entnimmst das Ergebnis eines alten automatischen Prozesses.\n\nAuf der Plattform liegt eine Kiste mit verarbeiteten Materialien: polierte Legierungen, stabilisierte Keramik und Packungen mit synthetischem Stoff.\n\nAlles ist sauber markiert, als hätte es auf seinen Besitzer gewartet.";
        _text[514, 5] = "Abres manualmente el compartimento y extraes el resultado de un viejo proceso automático.\n\nSobre la plataforma hay una caja con materiales procesados: aleaciones pulidas, cerámica estabilizada y paquetes de tela sintética.\n\nTodo está cuidadosamente marcado, como si hubiera estado esperando a su dueño.";
        _text[514, 6] = "Ręcznie otwierasz przedział i wyciągasz rezultat dawnego automatycznego procesu.\n\nNa platformie leży skrzynia z obrobionymi materiałami: wypolerowane stopy, stabilizowana ceramika i paczki z syntetyczną tkaniną.\n\nWszystko starannie oznakowane, jakby czekało na właściciela.";
        _text[514, 7] = "Você abre manualmente o compartimento e retira o resultado de um antigo processo automático.\n\nNa plataforma há uma caixa com materiais processados: ligas polidas, cerâmica estabilizada e embalagens com tecido sintético.\n\nTudo está cuidadosamente etiquetado, como se tivesse estado à espera do dono.";
        _text[514, 8] = "";
        _text[514, 9] = "";

        _text[515, 0] = "Do not interfere";
        _text[515, 1] = "Не вмешиваться"; //выбор 2
        _text[515, 2] = "Ne pas intervenir";
        _text[515, 3] = "Non intervenire";
        _text[515, 4] = "Nicht eingreifen";
        _text[515, 5] = "No intervenir";
        _text[515, 6] = "Nie ingerować";
        _text[515, 7] = "Não interferir";
        _text[515, 8] = "";
        _text[515, 9] = "";

        _text[516, 0] = "You decide not to interfere: the station is unstable, and any interference could cause the structure to collapse.\n\nLeaving the object alone, you retreat to a safe distance.";
        _text[516, 1] = "Вы решаете не вмешиваться: станция нестабильна, а любое вмешательство может привести к обрушению конструкции.\n\nОставив объект в покое, вы отходите на безопасное расстояние.";
        _text[516, 2] = "Vous décidez de ne pas intervenir: la station est instable et toute intervention pourrait entraîner son effondrement.\n\nLaissant l’objet en paix, vous vous éloignez à une distance de sécurité.";
        _text[516, 3] = "Decidi di non intervenire: la stazione è instabile, e qualsiasi manovra potrebbe portare al collasso della struttura.\n\nLasciando l'oggetto in pace, ti allontani a distanza di sicurezza.";
        _text[516, 4] = "Du entscheidest dich, nicht einzugreifen: Die Station ist instabil, und jedes Eingreifen könnte zum Einsturz der Struktur führen.\n\nDu lässt das Objekt in Ruhe und gehst auf sicheren Abstand.";
        _text[516, 5] = "Decides no intervenir: la estación es inestable y cualquier intervención puede provocar el colapso de la estructura.\n\nDejando el objeto en paz, te alejas a una distancia segura.";
        _text[516, 6] = "Postanawiasz nie ingerować: stacja jest niestabilna, a każda ingerencja może doprowadzić do zawalenia konstrukcji.\n\nPozostawiając obiekt w spokoju, oddalasz się na bezpieczną odległość.";
        _text[516, 7] = "Você decide não interferir: a estação é instável, e qualquer intervenção pode levar ao colapso da estrutura.\n\nDeixando o objeto em paz, você afasta-se para uma distância segura.";
        _text[516, 8] = "";
        _text[516, 9] = "";

        // WeaponTraderNode
        _text[517, 0] = "You approach a heavily armed ship, bristling with turrets, cannons, and missile launchers.\n\nThe metal of the hull is blackened from old battles, but the weapons are fully operational.\n\nA distorted voice breaks into the airwaves:\n\n\"Hey...can you hear me? I repair and upgrade weapons...for a small fee, that is.\n\nIf you want, I can turn your gun into a work of art...or at least into something that shoots a little better than it does now.\"";
        _text[517, 1] = "Вы приближаетесь к тяжело вооружённому кораблю, утыканному турелями, пушками и ракетными установками.\n\nМеталл корпуса почернел от старых боёв, но оружие в полной боевой готовности.\n\nВ эфир прорывается искажённый помехами голос:\n\n\"Эй... слышите меня? Я чиню и улучшаю оружие... ну, за скромную плату.\n\nЕсли хотите, могу превратить вашу пушку в произведение искусства... или хотя бы в то, что стреляет чуть лучше, чем сейчас.\"";
        _text[517, 2] = "Vous vous approchez d'un vaisseau lourdement armé, hérissé de tourelles, de canons et de lance-missiles.\n\nSa coque métallique est noircie par les combats passés, mais les armes sont parfaitement opérationnelles.\n\nUne voix déformée perce les ondes:\n\n\"Hé... vous m'entendez? Je répare et améliore les armes... enfin, pour une somme modique.\n\nSi vous le souhaitez, je peux transformer votre canon en une œuvre d'art... ou du moins en quelque chose qui tire un peu mieux qu'actuellement. »";
        _text[517, 3] = "Ti avvicini a una nave pesantemente armata, irta di torrette, cannoni e lanciarazzi.\n\nIl metallo dello scafo è annerito da vecchie battaglie, ma le armi sono in piena prontezza.\n\nNell'etere irrompe una voce distorta dalle interferenze:\n\n\"Ehi... mi senti? Riparo e miglioro le armi... beh, per una modesta cifra.\n\nSe vuoi, posso trasformare il tuo cannone in un'opera d'arte... o almeno in qualcosa che spara un po' meglio di adesso.\"";
        _text[517, 4] = "Du näherst dich einem schwer bewaffneten Schiff, gespickt mit Geschütztürmen, Kanonen und Raketenwerfern.\n\nDas Metall des Rumpfs ist von alten Schlachten geschwärzt, doch die Waffen sind voll einsatzbereit.\n\nIn den Äther bricht eine durch Störungen verzerrte Stimme:\n\n\"Hey... hörst du mich? Ich repariere und verbessere Waffen... na ja, gegen eine bescheidene Gebühr.\n\nWenn du willst, kann ich deine Kanone in ein Kunstwerk verwandeln... oder zumindest in etwas, das ein bisschen besser schießt als jetzt.\"";
        _text[517, 5] = "Te acercas a una nave fuertemente armada, cubierta de torretas, cañones y lanzamisiles.\n\nEl metal del casco está ennegrecido por viejas batallas, pero las armas están en plena alerta.\n\nEn la radio irrumpe una voz distorsionada por interferencias:\n\n\"Eh... ¿me oyes? Reparo y mejoro armas... bueno, por un módico precio.\n\nSi quieres, puedo convertir tu cañón en una obra de arte... o al menos en algo que dispare un poco mejor que ahora.\"";
        _text[517, 6] = "Zbliżasz się do ciężko uzbrojonego statku, najeżonego wieżyczkami, działami i wyrzutniami rakiet.\n\nMetal kadłuba pociemniał od dawnych bitew, ale uzbrojenie jest w pełnej gotowości bojowej.\n\nW eter przebija się głos zniekształcony zakłóceniami:\n\n\"Hej... słyszysz mnie? Naprawiam i ulepszam broń... no, za skromną opłatą.\n\nJeśli chcesz, mogę zamienić twoje działo w dzieło sztuki... albo przynajmniej w coś, co strzela trochę lepiej niż teraz.\"";
        _text[517, 7] = "Você aproxima-se de uma nave fortemente armada, coberta de torres, canhões e lançadores de foguetes.\n\nO metal do casco está enegrecido por combates antigos, mas as armas estão em plena prontidão.\n\nNo éter irrompe uma voz distorcida por interferências:\n\n\"Ei... consegue ouvir-me? Eu reparo e melhoro armas... bem, por uma taxa modesta.\n\nSe quiser, posso transformar a sua arma numa obra de arte... ou pelo menos em algo que dispare um pouco melhor do que agora.\"";
        _text[517, 8] = "";
        _text[517, 9] = "";

        _text[518, 0] = "Trade";
        _text[518, 1] = "Торговать";
        _text[518, 2] = "Commerce";
        _text[518, 3] = "Commerciare";
        _text[518, 4] = "Handeln";
        _text[518, 5] = "Comerciar";
        _text[518, 6] = "Handlować";
        _text[518, 7] = "Negociar";
        _text[518, 8] = "";
        _text[518, 9] = "";

        _text[519, 0] = "Ignore";
        _text[519, 1] = "Игнорировать";
        _text[519, 2] = "Ignorer";
        _text[519, 3] = "Ignorare";
        _text[519, 4] = "Ignorieren";
        _text[519, 5] = "Ignorar";
        _text[519, 6] = "Zignorować";
        _text[519, 7] = "Ignorar";
        _text[519, 8] = "";
        _text[519, 9] = "";

        // 1_ResourceDialogue
        _text[520, 0] = "You intercept a colonial logistics container.\n\nScans show organic structural panels.";
        _text[520, 1] = "Вы перехватываете контейнер колониальной логистики.\n\nСканы показывают органические конструкционные панели.";
        _text[520, 2] = "Vous interceptez un conteneur logistique colonial.\n\nLes scans révèlent des panneaux structurels organiques.";
        _text[520, 3] = "Intercetti un contenitore della logistica coloniale.\n\nLe scansioni mostrano pannelli strutturali organici.";
        _text[520, 4] = "Du fängst einen Container kolonialer Logistik ab.\n\nScans zeigen organische Konstruktionspaneele.";
        _text[520, 5] = "Interceptas un contenedor de logística colonial.\n\nLos escaneos muestran paneles estructurales orgánicos.";
        _text[520, 6] = "Przechwytujesz kontener kolonialnej logistyki.\n\nSkanery pokazują organiczne panele konstrukcyjne.";
        _text[520, 7] = "Você interceta um contentor de logística colonial.\n\nOs scans mostram painéis estruturais orgânicos.";
        _text[520, 8] = "";
        _text[520, 9] = "";

        _text[521, 0] = "Open and remove";
        _text[521, 1] = "Вскрыть и изъять"; // выбор 1
        _text[521, 2] = "Ouvrir et retirer";
        _text[521, 3] = "Forzare e requisire";
        _text[521, 4] = "Aufbrechen und entnehmen";
        _text[521, 5] = "Forzar y extraer";
        _text[521, 6] = "Otworzyć i zabrać";
        _text[521, 7] = "Arrombar e apreender";
        _text[521, 8] = "";
        _text[521, 9] = "";

        _text[522, 0] = "You break the seal and sort the cargo.\n\nSuccess: whole wooden modules sent to storage.";
        _text[522, 1] = "Вы вскрываете пломбу и сортируете груз.\n\nУспех: целые деревянные модули отправлены в хранилище."; // + дерево
        _text[522, 2] = "Vous brisez le scellé et triez la cargaison.\n\nSuccès : les modules en bois intacts ont été envoyés à l’entrepôt.";
        _text[522, 3] = "Forzi il sigillo e smisti il carico.\n\nSuccesso: i moduli in legno integri vengono inviati al deposito.";
        _text[522, 4] = "Du brichst das Siegel auf und sortierst die Fracht.\n\nErfolg: Intakte Holzmodule werden ins Lager gebracht.";
        _text[522, 5] = "Rompes el sello y clasificas la carga.\n\nÉxito: los módulos de madera intactos se envían al almacén.";
        _text[522, 6] = "Zrywasz plombę i sortujesz ładunek.\n\nSukces: całe drewniane moduły trafiają do magazynu.";
        _text[522, 7] = "Você rompe o selo e separa a carga.\n\nSucesso: módulos de madeira intactos foram enviados para o armazém.";
        _text[522, 8] = "";
        _text[522, 9] = "";

        _text[523, 0] = "Failure: the sterilizing foam is activated - the compartment is contaminated, some of the wood materials have to be discarded.";
        _text[523, 1] = "Провал: срабатывает стерилизующая пена - отсек загрязнён, часть древесных материалов приходится сбросить."; // - дерево
        _text[523, 2] = "Échec : la mousse stérilisante est activée – le compartiment est contaminé, une partie des matériaux en bois doit être jetée.";
        _text[523, 3] = "Fallimento: scatta una schiuma sterilizzante - il compartimento si contamina, e una parte dei materiali lignei va gettata.";
        _text[523, 4] = "Misserfolg: Sterilisationsschaum löst aus - das Fach wird kontaminiert, einen Teil der Holzmaterialien musst du abwerfen.";
        _text[523, 5] = "Fracaso: se activa una espuma esterilizante: el compartimento queda contaminado y parte de los materiales de madera debe desecharse.";
        _text[523, 6] = "Porażka: uruchamia się piana sterylizująca - przedział zostaje skażony, część drewnianych materiałów trzeba zrzucić.";
        _text[523, 7] = "Fracasso: a espuma esterilizante é acionada - o compartimento fica contaminado e parte dos materiais de madeira tem de ser descartada.";
        _text[523, 8] = "";
        _text[523, 9] = "";

        _text[524, 0] = "Push away and leave";
        _text[524, 1] = "Оттолкнуть и уйти"; // выбор 2
        _text[524, 2] = "Repousse-toi et pars";
        _text[524, 3] = "Allontanarlo e andarsene";
        _text[524, 4] = "Abstoßen und weg";
        _text[524, 5] = "Empujar y marcharse";
        _text[524, 6] = "Odepchnąć i odejść";
        _text[524, 7] = "Afastar e partir";
        _text[524, 8] = "";
        _text[524, 9] = "";

        _text[525, 0] = "You keep your distance and push the container away with a light impulse from the maneuvering engines.";
        _text[525, 1] = "Вы держите дистанцию и легким импульсом маневровых двигателей отталкиваете контейнер.";
        _text[525, 2] = "Vous maintenez vos distances et repoussez le conteneur d'une légère impulsion provenant des moteurs de manœuvre.";
        _text[525, 3] = "Mantieni la distanza e con un leggero impulso dei motori di manovra respingi il contenitore.";
        _text[525, 4] = "Du hältst Abstand und stößt den Container mit einem leichten Impuls der Manövrierdüsen weg.";
        _text[525, 5] = "Mantienes la distancia y, con un ligero impulso de los motores de maniobra, empujas el contenedor.";
        _text[525, 6] = "Trzymasz dystans i lekkim impulsem silników manewrowych odpychasz kontener.";
        _text[525, 7] = "Você mantém a distância e, com um leve impulso dos motores de manobra, afasta o contentor.";
        _text[525, 8] = "";
        _text[525, 9] = "";

        _text[526, 0] = "Success: the object goes off course. Nothing happens.";
        _text[526, 1] = "Успех: объект уходит с курса. Ничего не происходит."; // ничего
        _text[526, 2] = "Succès : L’objet dévie de sa trajectoire. Rien ne se passe.";
        _text[526, 3] = "Successo: l'oggetto esce dalla rotta. Non succede nulla.";
        _text[526, 4] = "Erfolg: Das Objekt gerät vom Kurs ab. Es passiert nichts.";
        _text[526, 5] = "Éxito: el objeto se desvía del rumbo. No ocurre nada.";
        _text[526, 6] = "Sukces: obiekt schodzi z kursu. Nic się nie dzieje.";
        _text[526, 7] = "Sucesso: o objeto sai da rota. Nada acontece.";
        _text[526, 8] = "";
        _text[526, 9] = "";

        _text[527, 0] = "Failure: a broken fragment scratches the paneling - spare wooden panels are used for emergency repairs.";
        _text[527, 1] = "Провал: отломившийся фрагмент царапает обшивку - на аварийный ремонт уходят запасные деревянные панели."; // - дерево
        _text[527, 2] = "Panne : un fragment cassé raye le lambris – des panneaux de bois de rechange sont utilisés pour les réparations d'urgence.";
        _text[527, 3] = "Fallimento: un frammento staccatosi graffia il rivestimento - per le riparazioni d'emergenza servono pannelli di legno di scorta.";
        _text[527, 4] = "Misserfolg: Ein abgebrochener Splitter zerkratzt die Hülle - für die Notreparatur gehen Ersatz-Holzpaneele drauf.";
        _text[527, 5] = "Fracaso: un fragmento desprendido araña el casco: para la reparación de emergencia se consumen paneles de madera de repuesto.";
        _text[527, 6] = "Porażka: odłamany fragment rysuje poszycie - na awaryjną naprawę schodzą zapasowe drewniane panele.";
        _text[527, 7] = "Fracasso: um fragmento solto risca o casco - painéis de madeira sobressalentes são consumidos na reparação de emergência.";
        _text[527, 8] = "";
        _text[527, 9] = "";

        // 2_ResourceDialogue
        _text[528, 0] = "Scanners reveal a \"cargo graveyard\": several lost capsules, coiled into a thin cloud of debris.";
        _text[528, 1] = "Сканеры отмечают \"кладбище грузов\": несколько потерянных капсул, смотанных в тонкое облако обломков.";
        _text[528, 2] = "Les scanners révèlent un \"cimetière de cargaisons\": plusieurs capsules perdues, enroulées dans un mince nuage de débris.";
        _text[528, 3] = "Gli scanner segnalano un \"cimitero di carichi\": diverse capsule smarrite, avvolte in una sottile nube di detriti.";
        _text[528, 4] = "Scanner markieren einen \"Frachtfriedhof\": mehrere verlorene Kapseln, zu einer dünnen Wolke aus Trümmern verknäuelt.";
        _text[528, 5] = "Los escáneres señalan un \"cementerio de cargas\": varias cápsulas perdidas, enrolladas en una fina nube de escombros.";
        _text[528, 6] = "Skanery oznaczają \"cmentarz ładunków\": kilka zagubionych kapsuł, zwiniętych w cienką chmurę odłamków.";
        _text[528, 7] = "Os scanners assinalam um \"cemitério de carga\": várias cápsulas perdidas, enredadas numa fina nuvem de destroços.";
        _text[528, 8] = "";
        _text[528, 9] = "";

        _text[529, 0] = "Search the capsules";
        _text[529, 1] = "Обыскать капсулы"; // выбор 1
        _text[529, 2] = "Rechercher les capsules";
        _text[529, 3] = "Perquisire le capsule";
        _text[529, 4] = "Kapseln durchsuchen";
        _text[529, 5] = "Registrar las cápsulas";
        _text[529, 6] = "Przeszukać kapsuły";
        _text[529, 7] = "Revistar as cápsulas";
        _text[529, 8] = "";
        _text[529, 9] = "";

        _text[530, 0] = "You maneuver among the debris and open the least damaged capsules.";
        _text[530, 1] = "Вы лавируете среди обломков и вскрываете наименее повреждённые капсулы."; // + случайный ресурс
        _text[530, 2] = "Vous vous frayez un chemin à travers les débris et ouvrez les capsules les moins endommagées.";
        _text[530, 3] = "Ti fai strada tra i detriti e apri le capsule meno danneggiate.";
        _text[530, 4] = "Du manövrierst zwischen den Trümmern und öffnest die am wenigsten beschädigten Kapseln.";
        _text[530, 5] = "Maniobras entre los restos y abres las cápsulas menos dañadas.";
        _text[530, 6] = "Lawirujesz wśród szczątków i otwierasz najmniej uszkodzone kapsuły.";
        _text[530, 7] = "Você manobra entre os destroços e abre as cápsulas menos danificadas.";
        _text[530, 8] = "";
        _text[530, 9] = "";

        _text[531, 0] = "Success: you extract the payload and distribute it among the compartments.";
        _text[531, 1] = "Успех: извлекаете полезный груз и распределяете по отсекам."; // + случайный ресурс
        _text[531, 2] = "Succès : Vous extrayez la charge utile et la répartissez entre les compartiments.";
        _text[531, 3] = "Successo: recuperi un carico utile e lo distribuisci nei compartimenti.";
        _text[531, 4] = "Erfolg: Du bergst nützliche Fracht und verteilst sie auf die Sektionen.";
        _text[531, 5] = "Éxito: extraes una carga útil y la distribuyes por los compartimentos.";
        _text[531, 6] = "Sukces: wydobywasz przydatny ładunek i rozdzielasz go między przedziały.";
        _text[531, 7] = "Sucesso: você extrai carga útil e distribui pelos compartimentos.";
        _text[531, 8] = "";
        _text[531, 9] = "";

        _text[532, 0] = "Failure: a trap or depressurization forces an emergency reset - you lose some resources.";
        _text[532, 1] = "Провал: ловушка или разгерметизация вынуждает к аварийному сбросу - вы теряете часть ресурсов."; // - случайный ресурс
        _text[532, 2] = "Panne : Un piège ou une dépressurisation provoque une réinitialisation d'urgence – vous perdez certaines ressources.";
        _text[532, 3] = "Fallimento: una trappola o una depressurizzazione ti costringe a uno scarico d'emergenza - perdi parte delle risorse.";
        _text[532, 4] = "Misserfolg: Eine Falle oder Dekompression zwingt zu einem Notabwurf - du verlierst einen Teil der Ressourcen.";
        _text[532, 5] = "Fracaso: una trampa o una despresurización te obliga a un lanzamiento de emergencia: pierdes parte de los recursos.";
        _text[532, 6] = "Porażka: pułapka lub rozszczelnienie zmusza do awaryjnego zrzutu - tracisz część zasobów.";
        _text[532, 7] = "Fracasso: uma armadilha ou despressurização obriga a uma ejeção de emergência - você perde parte dos recursos.";
        _text[532, 8] = "";
        _text[532, 9] = "";

        _text[533, 0] = "Leave the cargo graveyard";
        _text[533, 1] = "Оставить кладбище грузов"; // выбор 2
        _text[533, 2] = "Quittez le cimetière de marchandises";
        _text[533, 3] = "Lasciare il cimitero di carichi";
        _text[533, 4] = "Frachtfriedhof verlassen";
        _text[533, 5] = "Dejar el cementerio de cargas";
        _text[533, 6] = "Zostawić cmentarz ładunków";
        _text[533, 7] = "Deixar o cemitério de carga";
        _text[533, 8] = "";
        _text[533, 9] = "";

        _text[534, 0] = "You reduce thrust and maintain course as you pass the debris field.";
        _text[534, 1] = "Вы снижаете тягу и сохраняете курс, проходя поле обломков.";
        _text[534, 2] = "Vous réduisez la poussée et maintenez le cap en traversant le champ de débris.";
        _text[534, 3] = "Riduci la spinta e mantieni la rotta, attraversando il campo di detriti.";
        _text[534, 4] = "Du reduzierst den Schub und hältst den Kurs, während du das Trümmerfeld passierst.";
        _text[534, 5] = "Reduces el empuje y mantienes el rumbo, atravesando el campo de escombros.";
        _text[534, 6] = "Zmniejszasz ciąg i utrzymujesz kurs, mijając pole szczątków.";
        _text[534, 7] = "Você reduz a potência e mantém o rumo ao atravessar o campo de destroços.";
        _text[534, 8] = "";
        _text[534, 9] = "";

        _text[535, 0] = "Success: you walk around the field without incident.";
        _text[535, 1] = "Успех: вы обходите поле без происшествий."; // ничего
        _text[535, 2] = "Succès : Vous parcourez le terrain sans incident.";
        _text[535, 3] = "Successo: aggiri il campo senza incidenti.";
        _text[535, 4] = "Erfolg: Du umgehst das Feld ohne Zwischenfälle.";
        _text[535, 5] = "Éxito: rodeas el campo sin incidentes.";
        _text[535, 6] = "Sukces: omijasz pole bez incydentów.";
        _text[535, 7] = "Sucesso: você contorna o campo sem incidentes.";
        _text[535, 8] = "";
        _text[535, 9] = "";

        _text[536, 0] = "Failure: drifting pod hits shields - you spend resources on repairs.";
        _text[536, 1] = "Провал: дрейфующая капсула задевает щиты - вы тратите запасы ресурсов на починку."; // - случайный ресурс
        _text[536, 2] = "Échec : La capsule dérivante heurte vos boucliers – vous dépensez vos ressources en réparations.";
        _text[536, 3] = "Fallimento: una capsula alla deriva urta gli scudi - consumi riserve di risorse per le riparazioni.";
        _text[536, 4] = "Misserfolg: Eine treibende Kapsel streift die Schilde - du verbrauchst Ressourcenreserven für Reparaturen.";
        _text[536, 5] = "Fracaso: una cápsula a la deriva golpea los escudos: gastas reservas de recursos en reparaciones.";
        _text[536, 6] = "Porażka: dryfująca kapsuła zahacza o tarcze - zużywasz zapasy zasobów na naprawy.";
        _text[536, 7] = "Fracasso: uma cápsula à deriva atinge os escudos - você gasta reservas de recursos em reparações.";
        _text[536, 8] = "";
        _text[536, 9] = "";

        // 3_ResourceDialogue
        _text[537, 0] = "A damaged tugboat drifts ahead, the mining capsule still attached to its winch.\n\nTelemetry shows high iron content.";
        _text[537, 1] = "Впереди дрейфует повреждённый буксир, к его лебёдке всё ещё прицеплена горная капсула.\n\nТелеметрия показывает высокое содержание железа.";
        _text[537, 2] = "Un remorqueur endommagé dérive au loin, la capsule minière toujours accrochée à son treuil.Les données télémétriques indiquent des niveaux de fer élevés.";
        _text[537, 3] = "Più avanti deriva un rimorchiatore danneggiato; alla sua verricello è ancora agganciata una capsula mineraria.\n\nLa telemetria indica un alto contenuto di ferro.";
        _text[537, 4] = "Vor dir treibt ein beschädigter Schlepper, an seiner Winde hängt noch eine Bergbaukapsel.\n\nDie Telemetrie zeigt einen hohen Eisengehalt.";
        _text[537, 5] = "Más adelante deriva un remolcador dañado; a su cabrestante aún está enganchada una cápsula minera.\n\nLa telemetría muestra un alto contenido de hierro.";
        _text[537, 6] = "Przed tobą dryfuje uszkodzony holownik, do którego wciągarki wciąż przyczepiona jest kapsuła górnicza.\n\nTelemetria wskazuje wysoką zawartość żelaza.";
        _text[537, 7] = "À frente deriva um rebocador danificado; à sua grua ainda está presa uma cápsula de mineração.\n\nA telemetria indica alto teor de ferro.";
        _text[537, 8] = "";
        _text[537, 9] = "";

        _text[538, 0] = "Quickly tear off the capsule";
        _text[538, 1] = "Быстро сорвать капсулу"; // выбор 1
        _text[538, 2] = "Déchirez rapidement la capsule";
        _text[538, 3] = "Strappare la capsula in fretta";
        _text[538, 4] = "Kapsel schnell abreißen";
        _text[538, 5] = "Arrancar la cápsula rápidamente";
        _text[538, 6] = "Szybko zerwać kapsułę";
        _text[538, 7] = "Arrancar a cápsula rapidamente";
        _text[538, 8] = "";
        _text[538, 9] = "";

        _text[539, 0] = "Success: you break the capsule and unload the iron ore into the receiver.";
        _text[539, 1] = "Успех: вы срываете капсулу и выгружаете железную руду в приёмник."; // + железная руда
        _text[539, 2] = "Succès : Vous ouvrez la capsule et déchargez le minerai de fer dans le récepteur.";
        _text[539, 3] = "Successo: strappi la capsula e scarichi il minerale di ferro nel ricevitore.";
        _text[539, 4] = "Erfolg: Du reißt die Kapsel ab und entlädst das Eisenerz in den Aufnehmer.";
        _text[539, 5] = "Éxito: arrancas la cápsula y descargas mineral de hierro en el receptor.";
        _text[539, 6] = "Sukces: odrywasz kapsułę i wysypujesz rudę żelaza do odbiornika.";
        _text[539, 7] = "Sucesso: você arranca a cápsula e descarrega minério de ferro no recetor.";
        _text[539, 8] = "";
        _text[539, 9] = "";

        _text[540, 0] = "Failure: capsule goes into rotation and disintegrates among the debris. Nothing is received.";
        _text[540, 1] = "Провал: капсула уходит в вращение и распадается среди обломков. Ничего не получено."; // ничего
        _text[540, 2] = "Échec : la capsule tourne sur elle-même et se désintègre parmi les débris. Rien n'a été récupéré.";
        _text[540, 3] = "Fallimento: la capsula entra in rotazione e si disintegra tra i detriti. Nessun guadagno.";
        _text[540, 4] = "Misserfolg: Die Kapsel gerät in Rotation und zerfällt in den Trümmern. Nichts erhalten.";
        _text[540, 5] = "Fracaso: la cápsula entra en rotación y se desintegra entre los restos. No se obtiene nada.";
        _text[540, 6] = "Porażka: kapsuła wpada w obrót i rozpada się wśród odłamków. Nic nie uzyskano.";
        _text[540, 7] = "Fracasso: a cápsula entra em rotação e desfaz-se entre os destroços. Nada foi obtido.";
        _text[540, 8] = "";
        _text[540, 9] = "";

        _text[541, 0] = "Carefully pick it up with a manipulator";
        _text[541, 1] = "Аккуратно забрать манипулятором"; // выбор 2
        _text[541, 2] = "Ramassez-le avec précaution à l'aide d'un manipulateur.";
        _text[541, 3] = "Recuperarla con cautela usando il manipolatore";
        _text[541, 4] = "Vorsichtig mit dem Manipulator bergen";
        _text[541, 5] = "Recogerla con cuidado con el manipulador";
        _text[541, 6] = "Ostrożnie zabrać manipulatorem";
        _text[541, 7] = "Recolher com cuidado com o manipulador";
        _text[541, 8] = "";
        _text[541, 9] = "";

        _text[542, 0] = "You lock the capsule and begin unloading. The tug restarts - the autoturret wakes up and manages to fire.\n\nYou have the ore, but one of the AI ​​cores burns out.";
        _text[542, 1] = "Вы фиксируете капсулу и начинаете выгрузку. Буксир перезапускается - автотурель просыпается и успевает выстрелить.\n\nРуда у вас, но одно ядро ИИ перегорает."; // + железная руда - ядро
        _text[542, 2] = "Vous sécurisez la capsule et commencez le déchargement. Le remorqueur redémarre, réactive la tourelle automatique et parvient à faire feu.Vous avez le minerai, mais l'un des cœurs d'IA grille.";
        _text[542, 3] = "Fissi la capsula e inizi lo scarico. Il rimorchiatore si riavvia - una autoturretta si risveglia e riesce a sparare.\n\nIl minerale è tuo, ma un nucleo IA si brucia.";
        _text[542, 4] = "Du fixierst die Kapsel und beginnst mit dem Entladen. Der Schlepper startet neu - ein Autoturm erwacht und schafft es zu feuern.\n\nDas Erz ist dein, aber ein KI-Kern brennt durch.";
        _text[542, 5] = "Aseguras la cápsula y comienzas la descarga. El remolcador se reinicia - una autotorreta despierta y alcanza a disparar.\n\nTienes el mineral, pero uno de los núcleos de IA se quema.";
        _text[542, 6] = "Unieruchamiasz kapsułę i rozpoczynasz rozładunek. Holownik restartuje się - automatyczna wieżyczka budzi się i zdąża oddać strzał.\n\nRuda jest twoja, ale jeden rdzeń SI się wypala.";
        _text[542, 7] = "Você fixa a cápsula e inicia a descarga. O rebocador reinicia - a auto-torre desperta e consegue disparar.\n\nO minério é seu, mas um núcleo de IA queima.";
        _text[542, 8] = "";
        _text[542, 9] = "";

        // 4_ResourceDialogue
        _text[543, 0] = "Reconnaissance notes a planet with a high rock content.\n\nScans show voids and layered strata with unstable areas.\n\nYou land on a rock plateau and set up a temporary quarry.";
        _text[543, 1] = "Разведка отмечает планету с высоким содержанием горных пород.\n\nСканы показывают пустоты и слоистые пласты с нестабильными участками.\n\nВы садитесь на каменное плато и разворачиваете временный карьер.";
        _text[543, 2] = "L'exploration révèle une planète riche en roches.\n\nLes analyses montrent des vides et des strates stratifiées présentant des zones instables.\n\nVous atterrissez sur un plateau rocheux et installez une carrière de fortune.";
        _text[543, 3] = "La ricognizione individua un pianeta con un'alta concentrazione di rocce.\n\nLe scansioni mostrano cavità e strati a lastroni con zone instabili.\n\nAtterri su un altopiano di pietra e allestisci una cava temporanea.";
        _text[543, 4] = "Die Aufklärung markiert einen Planeten mit hohem Gesteinsanteil.\n\nScans zeigen Hohlräume und geschichtete Schichten mit instabilen Bereichen.\n\nDu landest auf einem steinigen Plateau und richtest einen provisorischen Steinbruch ein.";
        _text[543, 5] = "El reconocimiento marca un planeta con alto contenido de roca.\n\nLos escaneos muestran cavidades y estratos en capas con zonas inestables.\n\nAterrizas en una meseta rocosa y despliegas una cantera temporal.";
        _text[543, 6] = "Rozpoznanie wskazuje planetę o wysokiej zawartości skał.\n\nSkany pokazują pustki i warstwowe pokłady z niestabilnymi odcinkami.\n\nLądujesz na skalnym płaskowyżu i rozkładasz tymczasowy kamieniołom.";
        _text[543, 7] = "A reconaissance assinala um planeta com elevado teor de rocha.\n\nOs scans mostram vazios e estratos em camadas com zonas instáveis.\n\nVocê pousa num planalto rochoso e monta uma pedreira temporária.";
        _text[543, 8] = "";
        _text[543, 9] = "";

        _text[544, 0] = "Send robots with cutters";
        _text[544, 1] = "Отправить роботов с резаками"; // выбор 1
        _text[544, 2] = "Envoyez des robots équipés de découpeuses";
        _text[544, 3] = "Inviare robot con seghe";
        _text[544, 4] = "Roboter mit Schneidbrennern schicken";
        _text[544, 5] = "Enviar robots con cortadoras";
        _text[544, 6] = "Wysłać roboty z przecinarkami";
        _text[544, 7] = "Enviar robôs com cortadores";
        _text[544, 8] = "";
        _text[544, 9] = "";

        _text[545, 0] = "Robots make shallow cuts, separate the blocks, forklifts carry the containers to the shuttle.\n\nYou extract a modest batch of stone and leave before the shifts begin.";
        _text[545, 1] = "Роботы делают неглубокие пропилы, отделяют блоки, погрузчики уносят контейнеры к шаттлу.\n\nВы добываете скромную партию камня и уходите до начала смещений."; // +камень
        _text[545, 2] = "Des robots effectuent des découpes superficielles, séparent les blocs, et des chariots élévateurs transportent les conteneurs jusqu'à la navette.\n\nVous prélevez une petite quantité de pierre et partez avant le début du déplacement.";
        _text[545, 3] = "I robot praticano tagli poco profondi, separano i blocchi, i caricatori portano i contenitori allo shuttle.\n\nEstrai una modesta partita di pietra e ti ritiri prima che inizino gli smottamenti.";
        _text[545, 4] = "Die Roboter schneiden flache Kerben, trennen Blöcke, Lader bringen Container zum Shuttle.\n\nDu gewinnst eine bescheidene Menge Stein und ziehst ab, bevor die Verschiebungen beginnen.";
        _text[545, 5] = "Los robots hacen cortes poco profundos, separan bloques, y las cargadoras llevan los contenedores al transbordador.\n\nObtienes una modesta partida de piedra y te vas antes de que empiecen los desplazamientos.";
        _text[545, 6] = "Roboty wykonują płytkie nacięcia, oddzielają bloki, a ładowarki przenoszą kontenery do wahadłowca.\n\nWydobywasz skromną partię kamienia i odchodzisz, zanim zaczną się przemieszczenia.";
        _text[545, 7] = "Os robôs fazem cortes pouco profundos, separam blocos, e os carregadores levam os contentores até ao shuttle.\n\nVocê extrai uma pequena quantidade de pedra e parte antes de começarem os deslizamentos.";
        _text[545, 8] = "";
        _text[545, 9] = "";

        _text[546, 0] = "Start the drill";
        _text[546, 1] = "Запустить бур"; // выбор 2
        _text[546, 2] = "Démarrez la perceuse";
        _text[546, 3] = "Avviare la trivella";
        _text[546, 4] = "Bohrer starten";
        _text[546, 5] = "Poner en marcha la perforadora";
        _text[546, 6] = "Uruchomić wiertło";
        _text[546, 7] = "Ligar a perfuradora";
        _text[546, 8] = "";
        _text[546, 9] = "";

        _text[547, 0] = "Success: resonant cracks open a rich vein. You haul away a large shipment of stone.";
        _text[547, 1] = "Успех: резонансные трещины открывают богатую жилу. Вы вывозите крупную партию камня."; // +камень
        _text[547, 2] = "Succès : des fissures résonnantes révèlent un filon riche. Vous emportez une importante cargaison de pierres.";
        _text[547, 3] = "Successo: le crepe risonanti aprono una vena ricca. Trasporti via una grande partita di pietra.";
        _text[547, 4] = "Erfolg: Resonanzrisse öffnen eine reiche Ader. Du bringst eine große Ladung Stein ab.";
        _text[547, 5] = "Éxito: las grietas resonantes abren una veta rica. Te llevas una gran partida de piedra.";
        _text[547, 6] = "Sukces: rezonansowe pęknięcia odsłaniają bogatą żyłę. Wywozisz dużą partię kamienia.";
        _text[547, 7] = "Sucesso: fissuras de ressonância abrem uma veia rica. Você transporta uma grande carga de pedra.";
        _text[547, 8] = "";
        _text[547, 9] = "";

        _text[548, 0] = "Failure: the edge of the quarry caves in. The safety rope breaks, the drilling frame is pulled into the hole, the loaders drop pallets to avoid falling.\n\nThe spoils are lost.";
        _text[548, 1] = "Провал: край карьера проседает. Рвётся страховочный трос, буровую раму тянет в провал, погрузчики сбрасывают паллеты, чтобы не сорваться.\n\nДобыча утрачена."; // ничего
        _text[548, 2] = "Effondrement: La paroi de la carrière s’effondre. Le câble de sécurité se rompt, l’engin de forage est entraîné dans le trou et les chargeurs laissent tomber des palettes pour éviter la chute.\n\nLes déblais sont perdus.";
        _text[548, 3] = "Fallimento: il bordo della cava cede. Si spezza il cavo di sicurezza, la struttura della trivella viene trascinata nel vuoto, i caricatori scaricano i pallet per non essere risucchiati.\n\nIl bottino è perduto.";
        _text[548, 4] = "Misserfolg: Der Rand des Steinbruchs sackt ab. Das Sicherungsseil reißt, das Bohrgestell wird in die Senke gezogen, die Lader werfen Paletten ab, um nicht mitgerissen zu werden.\n\nDie Beute geht verloren.";
        _text[548, 5] = "Fracaso: el borde de la cantera cede. Se rompe el cable de seguridad, la estructura de la perforadora es arrastrada al hundimiento, y las cargadoras sueltan palés para no caer.\n\nLa extracción se ha perdido.";
        _text[548, 6] = "Porażka: krawędź wyrobiska osiada. Pęka lina asekuracyjna, rama wiertnicza jest wciągana w zapadlisko, a ładowarki zrzucają palety, by nie runąć.\n\nWydobycie przepada.";
        _text[548, 7] = "Fracasso: a borda da pedreira cede. O cabo de segurança rompe-se, a armação da perfuradora é puxada para a fenda, e os carregadores largam paletes para não serem arrastados.\n\nA extração é perdida.";
        _text[548, 8] = "";
        _text[548, 9] = "";

        _text[549, 0] = "Launch a reconnaissance drone";
        _text[549, 1] = "Запустить разведовательный дрон"; // выбор 3
        _text[549, 2] = "Lancer un drone de reconnaissance";
        _text[549, 3] = "Lanciare un drone esplorativo";
        _text[549, 4] = "Aufklärungsdrohne starten";
        _text[549, 5] = "Lanzar un dron de reconocimiento";
        _text[549, 6] = "Uruchomić drona rozpoznawczego";
        _text[549, 7] = "Lançar um drone de reconhecimento";
        _text[549, 8] = "";
        _text[549, 9] = "";

        _text[550, 0] = "Success: the drone finds a stable cavity under the crust.\n\nYou mine a stable medium batch.";
        _text[550, 1] = "Успех: дрон находит стабильную полость под коркой.\n\nВы добываете стабильную среднюю партию."; // +камень
        _text[550, 2] = "Succès : Le drone repère une cavité stable sous la croûte.\n\nVous obtenez une récolte stable de taille moyenne.";
        _text[550, 3] = "Successo: il drone trova una cavità stabile sotto la crosta.\n\nRaccogli una solida partita media.";
        _text[550, 4] = "Erfolg: Die Drohne findet eine stabile Hohlkammer unter der Kruste.\n\nDu gewinnst eine stabile mittlere Menge.";
        _text[550, 5] = "Éxito: el dron encuentra una cavidad estable bajo la corteza.\n\nObtienes una partida media estable.";
        _text[550, 6] = "Sukces: dron znajduje stabilną pustkę pod skorupą.\n\nPozyskujesz stabilną, średnią partię.";
        _text[550, 7] = "Sucesso: o drone encontra uma cavidade estável sob a crosta.\n\nVocê extrai uma quantidade média estável.";
        _text[550, 8] = "";
        _text[550, 9] = "";

        _text[551, 0] = "Failure: dust emission jams turbines - drone lost.\n\nAll loot lost.";
        _text[551, 1] = "Провал: пылевой выброс клинит турбины - дрон потерян.\n\nВся добыча потеряна."; // -ядро
        _text[551, 2] = "Échec : des émissions de poussière bloquent les turbines – le drone est perdu.\n\nTout le butin est perdu.";
        _text[551, 3] = "Fallimento: un getto di polvere blocca le turbine - il drone è perduto.\n\nTutto il bottino è perduto.";
        _text[551, 4] = "Misserfolg: Ein Staubausbruch klemmt die Turbinen - die Drohne ist verloren.\n\nDie gesamte Beute ist verloren.";
        _text[551, 5] = "Fracaso: un chorro de polvo atasca las turbinas: el dron se pierde.\n\nSe pierde toda la extracción.";
        _text[551, 6] = "Porażka: wyrzut pyłu zacina turbiny - dron zostaje utracony.\n\nCałe wydobycie przepada.";
        _text[551, 7] = "Fracasso: uma ejeção de poeira bloqueia as turbinas - o drone é perdido.\n\nToda a extração foi perdida.";
        _text[551, 8] = "";
        _text[551, 9] = "";

        // 5_ResourceDialogue
        _text[552, 0] = "Your sensors catch a shimmer in the dust: a broken solar array field tumbling in slow orbit.\n\nPanels are still charged. The cabling looks brittle, but intact in places.";
        _text[552, 1] = "Сенсоры улавливают мерцание в пыли: поле сломанных солнечных панелей медленно вращается на орбите.\n\nПанели всё ещё заряжены. Кабели хрупкие, но местами целы.";
        _text[552, 2] = "Des capteurs détectent un scintillement dans la poussière: un champ de panneaux solaires brisés qui tournent lentement sur leur orbite.\n\nLes panneaux sont encore chargés. Les câbles sont fragiles, mais intacts par endroits.";
        _text[552, 3] = "I sensori colgono un bagliore nella polvere: un campo di pannelli solari rotti ruota lentamente in orbita.\n\nI pannelli sono ancora carichi. I cavi sono fragili, ma in alcuni punti sono integri.";
        _text[552, 4] = "Sensoren erfassen ein Flimmern im Staub: Ein Feld zerbrochener Solarpaneele rotiert langsam in der Umlaufbahn.\n\nDie Paneele sind noch geladen. Die Kabel sind spröde, aber stellenweise intakt.";
        _text[552, 5] = "Los sensores captan un destello en el polvo: un campo de paneles solares rotos gira lentamente en órbita.\n\nLos paneles aún están cargados. Los cables son frágiles, pero en partes están intactos.";
        _text[552, 6] = "Sensory wyłapują migotanie w pyle: pole uszkodzonych paneli słonecznych powoli obraca się na orbicie.\n\nPanele wciąż są naładowane. Kable są kruche, ale miejscami całe.";
        _text[552, 7] = "Os sensores detetam um brilho no pó: um campo de painéis solares partidos gira lentamente em órbita.\n\nOs painéis ainda estão carregados. Os cabos são frágeis, mas em alguns pontos estão intactos.";
        _text[552, 8] = "";
        _text[552, 9] = "";

        _text[553, 0] = "Cut power lines and harvest cells";
        _text[553, 1] = "Перерезать линии питания и снять ячейки"; // выбор 1
        _text[553, 2] = "Coupez les lignes électriques et retirez les cellules";
        _text[553, 3] = "Tagliare le linee di alimentazione e rimuovere le celle";
        _text[553, 4] = "Stromleitungen durchtrennen und Zellen entnehmen";
        _text[553, 5] = "Cortar las líneas de alimentación y retirar las celdas";
        _text[553, 6] = "Przeciąć linie zasilania i zdjąć ogniwa";
        _text[553, 7] = "Cortar as linhas de alimentação e retirar as células";
        _text[553, 8] = "";
        _text[553, 9] = "";

        _text[554, 0] = "Success: your drones isolate the charge and pull out usable cells.\n\nYou store the power modules for later conversion.";
        _text[554, 1] = "Успех: дроны изолируют заряд и извлекают пригодные ячейки.\n\nВы отправляете силовые модули в хранилище."; // +электричество
        _text[554, 2] = "Succès : Les drones isolent la charge et extraient les cellules utilisables.\n\nVous envoyez les modules d’énergie vers le stockage.";
        _text[554, 3] = "Successo: i droni isolano la carica ed estraggono le celle utilizzabili.\n\nInvii i moduli di potenza al deposito.";
        _text[554, 4] = "Erfolg: Drohnen isolieren die Ladung und bergen brauchbare Zellen.\n\nDu bringst die Leistungsmodule ins Lager.";
        _text[554, 5] = "Éxito: los drones aíslan la carga y extraen celdas aprovechables.\n\nEnvías los módulos de potencia al almacén.";
        _text[554, 6] = "Sukces: drony izolują ładunek i wyjmują sprawne ogniwa.\n\nWysyłasz moduły zasilania do magazynu.";
        _text[554, 7] = "Sucesso: os drones isolam a carga e extraem células utilizáveis.\n\nVocê envia os módulos de energia para o armazém.";
        _text[554, 8] = "";
        _text[554, 9] = "";

        _text[555, 0] = "Failure: a trapped capacitor discharges.\n\nA burst arcs across the harness - one of the core circuits overheats.";
        _text[555, 1] = "Провал: скрытый конденсатор разряжается.\n\nДуга пробивает проводку - одна из цепей ядра перегревается."; // -ядро
        _text[555, 2] = "Panne : Un condensateur caché se décharge.\n\nUn arc électrique se forme dans le câblage, provoquant la surchauffe d'un des circuits du noyau.";
        _text[555, 3] = "Fallimento: un condensatore nascosto si scarica.\n\nL'arco elettrico perfora il cablaggio - una delle catene del nucleo si surriscalda.";
        _text[555, 4] = "Misserfolg: Ein versteckter Kondensator entlädt sich.\n\nEin Lichtbogen durchschlägt den Kabelbaum - eine der Kernleitungen überhitzt.";
        _text[555, 5] = "Fracaso: un condensador oculto se descarga.\n\nEl arco perfora el cableado: uno de los circuitos del núcleo se sobrecalienta.";
        _text[555, 6] = "Porażka: ukryty kondensator się rozładowuje.\n\nŁuk przebija wiązkę - jeden z obwodów rdzenia się przegrzewa.";
        _text[555, 7] = "Fracasso: um condensador oculto descarrega.\n\nUm arco atravessa o chicote - uma das cadeias do núcleo sobreaquece.";
        _text[555, 8] = "";
        _text[555, 9] = "";

        _text[556, 0] = "Tow the whole frame to the ship";
        _text[556, 1] = "Притащить каркас целиком к кораблю"; // выбор 2
        _text[556, 2] = "Faites glisser l'ensemble du cadre vers le navire";
        _text[556, 3] = "Trascinare l'intero telaio fino alla nave";
        _text[556, 4] = "Den Rahmen komplett zum Schiff schleppen";
        _text[556, 5] = "Arrastrar el armazón entero hasta la nave";
        _text[556, 6] = "Przyciągnąć szkielet w całości do statku";
        _text[556, 7] = "Puxar a estrutura inteira para a nave";
        _text[556, 8] = "";
        _text[556, 9] = "";

        _text[557, 0] = "The array is heavier than telemetry suggested.\n\nSuccess: you secure the frame and strip it safely - plenty of usable metal.";
        _text[557, 1] = "Поле панелей тяжелее, чем показывала телеметрия.\n\nУспех: вы фиксируете каркас и разбираете его без риска - много пригодного металла."; // +железные слитки
        _text[557, 2] = "Le panneau est plus lourd que ce qu'indiquait la télémétrie.\n\nRéussite: vous avez sécurisé le cadre et l'avez démonté en toute sécurité - il y a suffisamment de métal utilisable.";
        _text[557, 3] = "Il campo di pannelli è più pesante di quanto indicasse la telemetria.\n\nSuccesso: fissi il telaio e lo smonti senza rischi - molto metallo utilizzabile.";
        _text[557, 4] = "Das Paneelfeld ist schwerer, als die Telemetrie zeigte.\n\nErfolg: Du fixierst den Rahmen und zerlegst ihn ohne Risiko - viel brauchbares Metall.";
        _text[557, 5] = "El campo de paneles es más pesado de lo que mostraba la telemetría.\n\nÉxito: aseguras el armazón y lo desmontas sin riesgo: mucho metal aprovechable.";
        _text[557, 6] = "Pole paneli jest cięższe, niż wskazywała telemetria.\n\nSukces: stabilizujesz szkielet i rozbierasz go bez ryzyka - dużo użytecznego metalu.";
        _text[557, 7] = "O campo de painéis é mais pesado do que a telemetria indicava.\n\nSucesso: você fixa a estrutura e desmonta-a sem riscos - muito metal aproveitável.";
        _text[557, 8] = "";
        _text[557, 9] = "";

        _text[558, 0] = "Failure: the frame twists under thrust.\n\nA shard scrapes the hull - emergency patches consume spare materials.";
        _text[558, 1] = "Провал: каркас выкручивает под тягой.\n\nОсколок царапает обшивку - аварийные заплаты съедают запас материалов."; // - случайный ресурс
        _text[558, 2] = "Échec : le cadre se tord sous la force de traction.\n\nUn éclat d’obus érafle la peau - les pansements de fortune absorbent les matériaux restants.";
        _text[558, 3] = "Fallimento: il telaio si torce sotto la trazione.\n\nUna scheggia graffia il rivestimento - le toppe d'emergenza consumano la scorta di materiali.";
        _text[558, 4] = "Misserfolg: Der Rahmen verdreht sich unter Zug.\n\nEin Splitter zerkratzt die Hülle - Notflicken fressen den Materialvorrat.";
        _text[558, 5] = "Fracaso: el armazón se retuerce bajo el tirón.\n\nUn fragmento araña el casco: los parches de emergencia consumen las reservas de materiales.";
        _text[558, 6] = "Porażka: szkielet wykręca się pod ciągiem.\n\nOdłamek rysuje poszycie - awaryjne łaty pochłaniają zapas materiałów.";
        _text[558, 7] = "Fracasso: a estrutura torce sob a tração.\n\nUm estilhaço risca o casco - remendos de emergência consomem o stock de materiais.";
        _text[558, 8] = "";
        _text[558, 9] = "";

        _text[559, 0] = "Leave it and move on";
        _text[559, 1] = "Оставить и продолжить путь"; // выбор 3 // ничего
        _text[559, 2] = "Partez et poursuivez votre chemin.";
        _text[559, 3] = "Lasciare e continuare il viaggio";
        _text[559, 4] = "Zurücklassen und weiterfliegen";
        _text[559, 5] = "Dejarlo y continuar el viaje";
        _text[559, 6] = "Zostawić i kontynuować drogę";
        _text[559, 7] = "Deixar e continuar o caminho";
        _text[559, 8] = "";
        _text[559, 9] = "";

        // 6_ResourceDialogue

        _text[560, 0] = "A small comet fragment drifts across your route.\n\nIts surface is cracked, venting thin plumes of ice dust.\n\nThe scanner confirms: a water-rich core.";
        _text[560, 1] = "Небольшой осколок кометы пересекает ваш маршрут.\n\nПоверхность треснута и выпускает тонкие струи ледяной пыли.\n\nСканер подтверждает: водонасыщенное ядро.";
        _text[560, 2] = "Un petit fragment de comète traverse votre trajectoire.\n\nSa surface est craquelée et laisse échapper de fins filaments de poussière glacée.\n\nLe scanner confirme: un noyau saturé d’eau.";
        _text[560, 3] = "Un piccolo frammento di cometa incrocia la tua rotta.\n\nLa superficie è spaccata e rilascia sottili getti di polvere ghiacciata.\n\nLo scanner conferma: un nucleo ricco d'acqua.";
        _text[560, 4] = "Ein kleiner Kometensplitter kreuzt deinen Kurs.\n\nDie Oberfläche ist gerissen und stößt feine Strahlen eisigen Staubs aus.\n\nDer Scanner bestätigt: ein wasserreicher Kern.";
        _text[560, 5] = "Un pequeño fragmento de cometa cruza tu ruta.\n\nLa superficie está agrietada y expulsa finos chorros de polvo helado.\n\nEl escáner confirma: un núcleo saturado de agua.";
        _text[560, 6] = "Niewielki odłamek komety przecina twój szlak.\n\nPowierzchnia jest popękana i wypuszcza cienkie strugi lodowego pyłu.\n\nSkaner potwierdza: rdzeń nasycony wodą.";
        _text[560, 7] = "Um pequeno fragmento de cometa cruza a sua rota.\n\nA superfície está rachada e liberta finos jatos de pó gelado.\n\nO scanner confirma: núcleo rico em água.";
        _text[560, 8] = "";
        _text[560, 9] = "";

        _text[561, 0] = "Feed the heaters and melt the core";
        _text[561, 1] = "Запитать нагреватели и расплавить ядро"; // выбор 1
        _text[561, 2] = "Mettez les résistances en marche et faites fondre le noyau.";
        _text[561, 3] = "Alimentare i riscaldatori e fondere il nucleo";
        _text[561, 4] = "Heizer speisen und den Kern schmelzen";
        _text[561, 5] = "Alimentar los calentadores y fundir el núcleo";
        _text[561, 6] = "Zasilić grzałki i stopić rdzeń";
        _text[561, 7] = "Alimentar os aquecedores e derreter o núcleo";
        _text[561, 8] = "";
        _text[561, 9] = "";

        _text[562, 0] = "You route power into the heating contours.\n\nThe ice yields, and clean water is pumped into sealed tanks.\n\nThe power grid sags - systems run on reserve for a while.";
        _text[562, 1] = "Вы подаёте мощность в контуры нагрева.\n\nЛёд поддаётся, и чистая вода перекачивается в герметичные баки.\n\nСеть проседает - некоторое время системы работают на резерве."; // + Water, - Electricity
        _text[562, 2] = "Vous alimentez les circuits de chauffage.\n\nLa glace fond et de l'eau propre est pompée dans des réservoirs étanches.\n\nLe réseau électrique est saturé et les systèmes fonctionnent temporairement sur la réserve.";
        _text[562, 3] = "Convogli potenza nei circuiti di riscaldamento.\n\nIl ghiaccio cede, e l'acqua pura viene pompata in serbatoi ermetici.\n\nLa rete cala - per un po' i sistemi lavorano in riserva.";
        _text[562, 4] = "Du leitest Energie in die Heizkreise.\n\nDas Eis gibt nach, und sauberes Wasser wird in hermetische Tanks gepumpt.\n\nDas Netz sackt ab - eine Zeit lang laufen die Systeme im Reservebetrieb.";
        _text[562, 5] = "Conduces potencia a los circuitos de calentamiento.\n\nEl hielo cede y el agua pura se bombea a tanques herméticos.\n\nLa red se resiente - durante un tiempo los sistemas funcionan en reserva.";
        _text[562, 6] = "Podajesz moc do obwodów grzewczych.\n\nLód ustępuje, a czysta woda jest przepompowywana do hermetycznych zbiorników.\n\nSieć siada - przez pewien czas systemy pracują na rezerwie.";
        _text[562, 7] = "Você envia energia para os circuitos de aquecimento.\n\nO gelo cede, e água limpa é bombeada para tanques herméticos.\n\nA rede sofre uma queda - durante algum tempo, os sistemas operam em reserva.";
        _text[562, 8] = "";
        _text[562, 9] = "";

        _text[564, 0] = "Capture the vent and compress it into steam canisters";
        _text[564, 1] = "Перехватить выброс и сжать в паровые баллоны"; // выбор 2
        _text[564, 2] = "Intercepter les émissions et les comprimer dans des cylindres à vapeur.";
        _text[564, 3] = "Intercettare il getto e comprimerlo in bombole di vapore";
        _text[564, 4] = "Ausstoß abfangen und in Dampfflaschen komprimieren";
        _text[564, 5] = "Interceptar el chorro y comprimirlo en cilindros de vapor";
        _text[564, 6] = "Przechwycić wyrzut i sprężyć do butli parowych";
        _text[564, 7] = "Intercetar a ejeção e comprimir em cilindros de vapor";
        _text[564, 8] = "";
        _text[564, 9] = "";

        _text[565, 0] = "You deploy intake nets in the plume.\n\nCompressors seal the collected vapor into pressure canisters.\n\nStable. Clean. No risk to the hull.";
        _text[565, 1] = "Вы раскрываете сети забора на струе выброса.\n\nКомпрессоры запечатывают собранный пар в баллоны под давлением.\n\nСтабильно. Чисто. Без риска для корпуса."; // + Steam
        _text[565, 2] = "Vous ouvrez les grilles d'admission sur le jet de refoulement.\n\nDes compresseurs scellent la vapeur collectée dans des cylindres sous pression.\n\nStable. Propre. Aucun risque pour la coque.";
        _text[565, 3] = "Apri le reti di raccolta sul getto di emissione.\n\nI compressori sigillano il vapore raccolto in bombole sotto pressione.\n\nStabile. Pulito. Senza rischi per lo scafo.";
        _text[565, 4] = "Du entfaltest die Sammelnetze im Strahl des Ausstoßes.\n\nKompressoren versiegeln den gesammelten Dampf in Druckflaschen.\n\nStabil. Sauber. Ohne Risiko für die Hülle.";
        _text[565, 5] = "Despliegas redes de captación en el chorro de eyección.\n\nLos compresores sellan el vapor recogido en cilindros a presión.\n\nEstable. Limpio. Sin riesgo para el casco.";
        _text[565, 6] = "Rozwijasz siatki poboru na strumieniu wyrzutu.\n\nKompresory uszczelniają zebrany par w butlach pod ciśnieniem.\n\nStabilnie. Czysto. Bez ryzyka dla kadłuba.";
        _text[565, 7] = "Você abre as redes de recolha no jato da ejeção.\n\nCompressores selam o vapor recolhido em cilindros sob pressão.\n\nEstável. Limpo. Sem risco para o casco.";
        _text[565, 8] = "";
        _text[565, 9] = "";

        _text[566, 0] = "Crack the fragment with a kinetic shot";
        _text[566, 1] = "Расколоть осколок кинетическим выстрелом"; // выбор 3
        _text[566, 2] = "Fendez l'éclat avec un tir cinétique";
        _text[566, 3] = "Spaccare il frammento con un colpo cinetico";
        _text[566, 4] = "Den Splitter mit einem kinetischen Schuss spalten";
        _text[566, 5] = "Partir el fragmento con un disparo cinético";
        _text[566, 6] = "Rozłupać odłamek strzałem kinetycznym";
        _text[566, 7] = "Fender o fragmento com um disparo cinético";
        _text[566, 8] = "";
        _text[566, 9] = "";

        _text[567, 0] = "Success: the crust splits open.\n\nYou scoop up water - and notice a sealed data capsule lodged inside the ice.\n\nIts core still holds fragments of old navigation logs.";
        _text[567, 1] = "Успех: корка раскрывается.\n\nВы собираете воду - и замечаете во льду герметичную капсулу с данными.\n\nЕё ядро всё ещё хранит фрагменты старых навигационных логов."; // + Water, + Memory
        _text[567, 2] = "Succès: la croûte cède.\n\nVous prélevez de l’eau et découvrez une capsule de données scellée dans la glace.\n\nSon noyau renferme encore des fragments d’anciens journaux de navigation.";
        _text[567, 3] = "Successo: la crosta si apre.\n\nRaccogli l'acqua - e noti nel ghiaccio una capsula dati ermetica.\n\nIl suo nucleo conserva ancora frammenti di vecchi log di navigazione.";
        _text[567, 4] = "Erfolg: Die Kruste bricht auf.\n\nDu sammelst Wasser - und entdeckst im Eis eine hermetische Datenkapsel.\n\nIhr Kern bewahrt noch Fragmente alter Navigationslogs.";
        _text[567, 5] = "Éxito: la corteza se abre.\n\nRecoges el agua: y notas en el hielo una cápsula hermética con datos.\n\nSu núcleo aún conserva fragmentos de antiguos registros de navegación.";
        _text[567, 6] = "Sukces: skorupa pęka.\n\nZbierasz wodę - i dostrzegasz w lodzie hermetyczną kapsułę z danymi.\n\nJej rdzeń wciąż przechowuje fragmenty starych logów nawigacyjnych.";
        _text[567, 7] = "Sucesso: a crosta abre-se.\n\nVocê recolhe água - e vê no gelo uma cápsula de dados hermética.\n\nO seu núcleo ainda guarda fragmentos de antigos logs de navegação.";
        _text[567, 8] = "";
        _text[567, 9] = "";

        _text[568, 0] = "Failure: the shot turns the fragment into a chaotic hail.\n\nIce shards rake the shielding - you burn supplies on emergency repairs.";
        _text[568, 1] = "Провал: выстрел превращает осколок в хаотический град.\n\nЛедяные осколки бьют по щитам - на аварийный ремонт уходят запасы."; // - RandomResource
        _text[568, 2] = "Échec: Le tir transforme l’éclat en une grêle chaotique.\n\nDes éclats de glace frappent les boucliers, épuisant les réserves nécessaires aux réparations d’urgence.";
        _text[568, 3] = "Fallimento: il colpo trasforma il frammento in una grandinata caotica.\n\nSchegge di ghiaccio colpiscono gli scudi - per le riparazioni d'emergenza consumi le scorte.";
        _text[568, 4] = "Misserfolg: Der Schuss verwandelt den Splitter in chaotischen Hagel.\n\nEissplitter prasseln auf die Schilde - für Notreparaturen gehen Vorräte drauf.";
        _text[568, 5] = "Fracaso: el disparo convierte el fragmento en una granizada caótica.\n\nLos trozos de hielo golpean los escudos: se gastan reservas en la reparación de emergencia.";
        _text[568, 6] = "Porażka: strzał zamienia odłamek w chaotyczny grad.\n\nLodowe odłamki biją w tarcze - zapasy idą na awaryjne naprawy.";
        _text[568, 7] = "Fracasso: o disparo transforma o fragmento numa saraivada caótica.\n\nEstilhaços de gelo atingem os escudos - reservas são consumidas em reparações de emergência.";
        _text[568, 8] = "";
        _text[568, 9] = "";

        // 7_ResourceDialogue
        _text[569, 0] = "Scans detect the ruins of an ancient glassworks facility on the planet's surface.\n\nThe production bay is half-buried in sand. The roof is cracked, the workshop is silent - but the silicate vats are intact.\n\nSand is everywhere. So are stacks of half-finished panes.";
        _text[569, 1] = "Сканы фиксируют руины древнего стекольного производства на поверхности планеты.\n\nПроизводственный отсек наполовину занесён песком. Крыша треснула, цех молчит - но силликатные ванны целы.\n\nПесок повсюду. И пачки полуготовых стеклянных панелей тоже.";
        _text[569, 2] = "Des analyses révèlent les ruines d'une ancienne verrerie à la surface de la planète.\n\nL'atelier de production est à moitié enfoui sous le sable. Le toit est fissuré, le silence règne, mais les bains de silicate sont intacts.\n\nLe sable est omniprésent. Tout comme des piles de panneaux de verre inachevés.";
        _text[569, 3] = "Le scansioni rilevano le rovine di un'antica produzione di vetro sulla superficie del pianeta.\n\nIl compartimento produttivo è per metà sepolto dalla sabbia. Il tetto è crepato, il capannone tace - ma le vasche di silicato sono integre.\n\nSabbia ovunque. E anche pacchi di pannelli di vetro semilavorati.";
        _text[569, 4] = "Scans registrieren die Ruinen einer uralten Glasproduktion auf der Planetenoberfläche.\n\nDie Produktionssektion ist halb mit Sand verweht. Das Dach ist gerissen, die Halle schweigt - aber die Silikatwannen sind intakt.\n\nSand ist überall. Und Stapel halbfertiger Glasscheiben auch.";
        _text[569, 5] = "Los escaneos detectan las ruinas de una antigua planta de vidrio en la superficie del planeta.\n\nEl compartimento de producción está medio sepultado por arena. El techo está agrietado, el taller en silencio - pero las cubas de silicato están intactas.\n\nArena por todas partes. Y también paquetes de paneles de vidrio a medio terminar.";
        _text[569, 6] = "Skany wykrywają ruiny pradawnej produkcji szkła na powierzchni planety.\n\nHala produkcyjna jest w połowie zasypana piaskiem. Dach pękł, zakład milczy - ale wanny krzemianowe są całe.\n\nPiasek jest wszędzie. I paczki półgotowych szklanych paneli też.";
        _text[569, 7] = "Os scans registam as ruínas de uma antiga produção de vidro na superfície do planeta.\n\nO compartimento de produção está meio soterrado em areia. O teto rachou, a oficina está silenciosa - mas as cubas de silicato estão intactas.\n\nAreia por todo o lado. E também pilhas de painéis de vidro semiacabados.";
        _text[569, 8] = "";
        _text[569, 9] = "";

        _text[570, 0] = "Scoop sand into containers";
        _text[570, 1] = "Набрать песок в контейнеры"; // выбор 1
        _text[570, 2] = "Recueillez le sable dans des récipients.";
        _text[570, 3] = "Caricare sabbia nei contenitori";
        _text[570, 4] = "Sand in Container füllen";
        _text[570, 5] = "Recoger arena en contenedores";
        _text[570, 6] = "Nabrać piasku do kontenerów";
        _text[570, 7] = "Recolher areia para contentores";
        _text[570, 8] = "";
        _text[570, 9] = "";

        _text[571, 0] = "You load dry silicate sand into sealed bins.\n\nNo alarms. No movement. Only windless dust and dead machinery.";
        _text[571, 1] = "Вы загружаете сухой кремнезёмный песок в герметичные контейнеры.\n\nНикаких тревог. Никакого движения. Только безветренная пыль и мёртвые механизмы."; // + Sand
        _text[571, 2] = "Vous chargez du sable de silice sec dans des conteneurs hermétiques.\n\nPas de souci. Aucun mouvement. Juste de la poussière immobile et des machines à l'arrêt.";
        _text[571, 3] = "Carichi sabbia secca di silice in contenitori ermetici.\n\nNessun allarme. Nessun movimento. Solo polvere senza vento e meccanismi morti.";
        _text[571, 4] = "Du lädst trockenen silikatreichen Sand in hermetische Container.\n\nKeine Alarme. Keine Bewegung. Nur windloser Staub und tote Mechanismen.";
        _text[571, 5] = "Cargas arena de sílice seca en contenedores herméticos.\n\nSin alarmas. Sin movimiento. Solo polvo sin viento y mecanismos muertos.";
        _text[571, 6] = "Ładujesz suchy krzemionkowy piasek do hermetycznych kontenerów.\n\nBrak alarmów. Brak ruchu. Tylko bezwietrzny pył i martwe mechanizmy.";
        _text[571, 7] = "Você carrega areia seca de sílica para contentores herméticos.\n\nSem alarmes. Sem movimento. Apenas pó sem vento e mecanismos mortos.";
        _text[571, 8] = "";
        _text[571, 9] = "";

        _text[572, 0] = "Restart the furnace cycle";
        _text[572, 1] = "Перезапустить цикл печи"; // выбор 2
        _text[572, 2] = "Redémarrez le cycle du four";
        _text[572, 3] = "Riavviare il ciclo del forno";
        _text[572, 4] = "Ofenzyklus neu starten";
        _text[572, 5] = "Reiniciar el ciclo del horno";
        _text[572, 6] = "Uruchomić ponownie cykl pieca";
        _text[572, 7] = "Reiniciar o ciclo do forno";
        _text[572, 8] = "";
        _text[572, 9] = "";

        _text[573, 0] = "Success: the old heaters respond.\n\nThe temperature rises slowly. The line completes one last cycle - and a batch of tempered glass panes slides out of the bay.";
        _text[573, 1] = "Успех: древние нагреватели откликаются.\n\nТемпература медленно растёт. Линия завершает ещё один цикл - и из отсека выходит партия закалённых стеклянных панелей."; // + Glass
        _text[573, 2] = "Succès: les anciens éléments chauffants réagissent.\n\nLa température monte lentement. La chaîne achève un nouveau cycle et une série de panneaux de verre trempé sort de la chambre.";
        _text[573, 3] = "Successo: gli antichi riscaldatori rispondono.\n\nLa temperatura sale lentamente. La linea completa un altro ciclo - e dal compartimento esce una partita di pannelli di vetro temprato.";
        _text[573, 4] = "Erfolg: Die uralten Heizer reagieren.\n\nDie Temperatur steigt langsam. Die Linie beendet einen weiteren Zyklus - und aus der Sektion kommt eine Charge gehärteter Glasscheiben.";
        _text[573, 5] = "Éxito: los antiguos calentadores responden.\n\nLa temperatura sube lentamente. La línea completa otro ciclo: y del compartimento sale una partida de paneles de vidrio templado.";
        _text[573, 6] = "Sukces: pradawne nagrzewnice odpowiadają.\n\nTemperatura powoli rośnie. Linia kończy kolejny cykl - i z przedziału wychodzi partia hartowanych szklanych paneli.";
        _text[573, 7] = "Sucesso: os antigos aquecedores respondem.\n\nA temperatura sobe lentamente. A linha completa mais um ciclo - e do compartimento sai um lote de painéis de vidro temperado.";
        _text[573, 8] = "";
        _text[573, 9] = "";

        _text[574, 0] = "Failure: a sealed pressure pocket bursts.\n\nA hot jet scorches the drones and floods the bay with abrasive dust.\n\nYou cut power and retreat - one of the ship's cores burns out under overload.";
        _text[574, 1] = "Провал: взрывается запечатанный карман давления.\n\nРаскалённая струя обжигает дронов и забивает отсек абразивной пылью.\n\nВы обрубаете питание и отходите - одно из ядер корабля перегорает от перегрузки."; // - AiCore
        _text[574, 2] = "Panne: Une chambre de pression étanche explose.\n\nUne explosion brûlante consume les drones et remplit le compartiment de poussière abrasive.\n\nVous coupez l’alimentation et battez en retraite: l’un des réacteurs du vaisseau est détruit par une surcharge.";
        _text[574, 3] = "Fallimento: esplode una sacca di pressione sigillata.\n\nUn getto rovente ustiona i droni e riempie il compartimento di polvere abrasiva.\n\nTagli l'alimentazione e ti ritiri - uno dei nuclei della nave si brucia per sovraccarico.";
        _text[574, 4] = "Misserfolg: Ein versiegelter Druckbeutel explodiert.\n\nEin glühender Strahl verbrennt die Drohnen und füllt die Sektion mit abrasivem Staub.\n\nDu kappst die Energiezufuhr und gehst auf Abstand - einer der Schiffskerne brennt durch Überhitzung aus.";
        _text[574, 5] = "Fracaso: estalla una bolsa de presión sellada.\n\nUn chorro incandescente quema a los drones y llena el compartimento de polvo abrasivo.\n\nCortas la energía y te alejas: uno de los núcleos de la nave se quema por la sobrecarga.";
        _text[574, 6] = "Porażka: eksploduje zapieczętowana kieszeń ciśnienia.\n\nRozżarzony strumień parzy drony i zasypuje przedział ściernym pyłem.\n\nOdłączasz zasilanie i wycofujesz się - jeden z rdzeni statku wypala się od przeciążenia.";
        _text[574, 7] = "Fracasso: um bolso de pressão selado explode.\n\nUm jato incandescente queima os drones e entope o compartimento com pó abrasivo.\n\nVocê corta a energia e recua - um dos núcleos da nave queima por sobrecarga.";
        _text[574, 8] = "";
        _text[574, 9] = "";

        _text[575, 0] = "Take only finished panes and leave";
        _text[575, 1] = "Забрать готовые панели и уйти"; // выбор 3
        _text[575, 2] = "Prenez les panneaux terminés et partez.";
        _text[575, 3] = "Prendere i pannelli pronti e andare via";
        _text[575, 4] = "Fertige Paneele mitnehmen und gehen";
        _text[575, 5] = "Recoger los paneles listos y marcharse";
        _text[575, 6] = "Zabrać gotowe panele i odejść";
        _text[575, 7] = "Recolher os painéis prontos e partir";
        _text[575, 8] = "";
        _text[575, 9] = "";

        _text[576, 0] = "You choose the safest cargo: sealed stacks with intact markings.\n\nA small, clean haul - no need to wake the dead factory.";
        _text[576, 1] = "Вы выбираете самое безопасное: запечатанные пачки с целыми маркировками.\n\nНебольшая, но чистая добыча - без попыток оживить мёртвое производство."; // + Glass
        _text[576, 2] = "Vous choisissez l'option la plus sûre: emballages scellés avec étiquettes intactes.\n\nProduction réduite mais propre – aucune tentative de relance d'un site de production fermé.";
        _text[576, 3] = "Scegli l'opzione più sicura: pacchi sigillati con marcature intatte.\n\nUn bottino piccolo ma pulito - senza tentare di rianimare una produzione morta.";
        _text[576, 4] = "Du wählst das Sicherste: versiegelte Packen mit intakten Markierungen.\n\nEine kleine, aber saubere Beute - ohne den Versuch, eine tote Produktion wiederzubeleben.";
        _text[576, 5] = "Eliges lo más seguro: paquetes sellados con marcajes intactos.\n\nUn botín pequeño, pero limpio: sin intentar reanimar una producción muerta.";
        _text[576, 6] = "Wybierasz najbezpieczniejsze: zapieczętowane pakiety z nienaruszonymi oznaczeniami.\n\nNiewielkie, ale czyste pozyskanie - bez prób ożywiania martwej produkcji.";
        _text[576, 7] = "Você escolhe o mais seguro: pacotes selados com marcações intactas.\n\nUma extração pequena, mas limpa - sem tentar reanimar uma produção morta.";
        _text[576, 8] = "";
        _text[576, 9] = "";

        // 8_ResourceComponentDialogue
        _text[577, 0] = "A dead relay station floats ahead, wrapped in a spiderweb of conduits.\n\nMost lines are cut, but the main trunk still holds copper - and signal-grade insulation.";
        _text[577, 1] = "Впереди дрейфует мёртвая ретрансляторная станция, опутанная паутиной магистралей.\n\nБольшинство линий перерезано, но основной ствол всё ещё держит медь - и изоляцию сигнального класса.";
        _text[577, 2] = "Plus loin, une station relais hors service dérive, enchevêtrée dans un réseau de lignes de transmission.\n\nLa plupart des lignes ont été coupées, mais le câble principal contient encore du cuivre et une isolation de qualité signal.";
        _text[577, 3] = "Più avanti deriva una stazione di ritrasmissione morta, avvolta in una ragnatela di dorsali.\n\nLa maggior parte delle linee è tranciata, ma il tronco principale tiene ancora rame - e isolamento di classe segnale.";
        _text[577, 4] = "Vor dir treibt eine tote Relaisstation, umwoben von einem Netz aus Hauptleitungen.\n\nDie meisten Linien sind durchtrennt, aber der Hauptstrang hält noch Kupfer - und Signalklassen-Isolierung.";
        _text[577, 5] = "Más adelante deriva una estación repetidora muerta, envuelta en una maraña de líneas troncales.\n\nLa mayoría de las líneas están cortadas, pero el tronco principal aún conserva cobre - y aislamiento de clase de señal.";
        _text[577, 6] = "Przed tobą dryfuje martwa stacja przekaźnikowa, oplątana pajęczyną magistral.\n\nWiększość linii jest przecięta, ale główny trzon wciąż trzyma miedź - oraz izolację klasy sygnałowej.";
        _text[577, 7] = "À frente deriva uma estação retransmissora morta, envolta numa teia de linhas principais.\n\nA maioria das ligações está cortada, mas o tronco principal ainda conserva cobre - e isolamento de classe de sinal.";
        _text[577, 8] = "";
        _text[577, 9] = "";

        _text[578, 0] = "Strip the conduits for copper";
        _text[578, 1] = "Сорвать магистрали на медь"; // выбор 1
        _text[578, 2] = "Démontez les canalisations en cuivre.";
        _text[578, 3] = "Strappare le dorsali per il rame";
        _text[578, 4] = "Hauptleitungen für Kupfer abtrennen";
        _text[578, 5] = "Arrancar las líneas troncales para obtener cobre";
        _text[578, 6] = "Zerwać magistrale na miedź";
        _text[578, 7] = "Arrancar as linhas principais para cobre";
        _text[578, 8] = "";
        _text[578, 9] = "";

        _text[579, 0] = "Success: heavy copper bundles are cut free and secured.";
        _text[579, 1] = "Успех: тяжёлые медные провода срезаны и закреплены."; // + CopperOre
        _text[579, 2] = "Succès : Gros fils de cuivre coupés et fixés.";
        _text[579, 3] = "Successo: i pesanti fasci di rame sono tagliati e fissati.";
        _text[579, 4] = "Erfolg: Schwere Kupferbündel werden abgeschnitten und gesichert.";
        _text[579, 5] = "Éxito: los pesados mazos de cobre son cortados y asegurados.";
        _text[579, 6] = "Sukces: ciężkie miedziane wiązki zostają odcięte i zabezpieczone.";
        _text[579, 7] = "Sucesso: feixes pesados de cobre foram cortados e fixados.";
        _text[579, 8] = "";
        _text[579, 9] = "";

        _text[580, 0] = "Failure: an energized line lashes back.\n\nThe station wakes for a second - auto-lock clamps your drone. You cut it loose.";
        _text[580, 1] = "Провал: под напряжением линия бьёт обратно.\n\nСтанция на секунду оживает - автофиксатор зажимает дрона. Вы рубите его и уходите."; // - ядро
        _text[580, 2] = "Échec : la ligne sous tension se retourne contre vous.\n\nLa station s’anime un instant: le système de fixation automatique se verrouille sur le drone. Vous le décrochez et vous vous en allez.";
        _text[580, 3] = "Fallimento: una linea sotto tensione ti colpisce di ritorno.\n\nLa stazione si rianima per un istante - un bloccaggio automatico stringe un drone. Lo tronchi e te ne vai.";
        _text[580, 4] = "Misserfolg: Unter Spannung schlägt die Leitung zurück.\n\nDie Station erwacht für eine Sekunde - eine automatische Verriegelung klemmt eine Drohne ein. Du kappst sie und ziehst ab.";
        _text[580, 5] = "Fracaso: una línea energizada golpea de vuelta.\n\nLa estación cobra vida por un segundo: un autofijador atrapa al dron. Lo cortas y te vas.";
        _text[580, 6] = "Porażka: linia pod napięciem uderza z powrotem.\n\nStacja na sekundę ożywa - automatyczny zacisk chwyta drona. Odcinasz go i odchodzisz.";
        _text[580, 7] = "Fracasso: sob tensão, a linha reage.\n\nA estação ganha vida por um segundo - um fixador automático prende o drone. Você corta-o e parte.";
        _text[580, 8] = "";
        _text[580, 9] = "";

        _text[581, 0] = "Harvest insulation and coil it";
        _text[581, 1] = "Снять изоляцию и смотать"; // выбор 2
        _text[581, 2] = "Retirer l'isolant et rebobiner";
        _text[581, 3] = "Rimuovere l'isolamento e arrotolarlo";
        _text[581, 4] = "Isolierung abnehmen und aufwickeln";
        _text[581, 5] = "Retirar el aislamiento y enrollarlo";
        _text[581, 6] = "Zdjąć izolację i zwinąć";
        _text[581, 7] = "Retirar o isolamento e enrolar";
        _text[581, 8] = "";
        _text[581, 9] = "";

        _text[582, 0] = "You collect clean insulation and intact copper strands.\n\nPerfect for wiring and delicate assemblies.";
        _text[582, 1] = "Вы собираете чистую изоляцию и целые медные жилы.\n\nИдеально для проводки и точных сборок."; // + CopperWire
        _text[582, 2] = "Vous récupérez l'isolant propre et les conducteurs en cuivre intacts.\n\nIdéal pour le câblage et les assemblages de précision.";
        _text[582, 3] = "Raccogli isolamento pulito e conduttori di rame integri.\n\nPerfetti per cablaggi e assemblaggi di precisione.";
        _text[582, 4] = "Du sammelst saubere Isolierung und intakte Kupferadern.\n\nIdeal für Verkabelung und präzise Montagen.";
        _text[582, 5] = "Recoges aislamiento limpio y venas de cobre intactas.\n\nIdeal para cableado y ensamblajes de precisión.";
        _text[582, 6] = "Zbierasz czystą izolację i całe miedziane żyły.\n\nIdealne do okablowania i precyzyjnych montaży.";
        _text[582, 7] = "Você recolhe isolamento limpo e veias de cobre intactas.\n\nPerfeito para cablagem e montagens de precisão.";
        _text[582, 8] = "";
        _text[582, 9] = "";

        _text[583, 0] = "Leave a beacon and mark the site";
        _text[583, 1] = "Оставить маяк и отметить место"; // выбор 3
        _text[583, 2] = "Laissez un repère et marquez l'endroit.";
        _text[583, 3] = "Lasciare il faro e segnare il punto";
        _text[583, 4] = "Sender zurücklassen und die Stelle markieren";
        _text[583, 5] = "Dejar la baliza y marcar el lugar";
        _text[583, 6] = "Zostawić boję i oznaczyć miejsce";
        _text[583, 7] = "Deixar o farol e marcar o local";
        _text[583, 8] = "";
        _text[583, 9] = "";

        _text[584, 0] = "You log the coordinates.\n\nNo loot now - but the memory is stored.";
        _text[584, 1] = "Вы фиксируете координаты.\n\nДобычи сейчас нет - но память сохранена."; // + фрагменты данных
        _text[584, 2] = "Vous enregistrez les coordonnées.\n\nIl n'y a pas de butin pour l'instant, mais le souvenir est préservé.";
        _text[584, 3] = "Fissi le coordinate.\n\nOra non c'è bottino - ma la memoria è salvata.";
        _text[584, 4] = "Du speicherst die Koordinaten.\n\nJetzt gibt es keine Beute - aber die Erinnerung bleibt.";
        _text[584, 5] = "Fijas las coordenadas.\n\nNo hay botín ahora - pero la memoria queda guardada.";
        _text[584, 6] = "Zapisujesz współrzędne.\n\nTeraz nie ma zysku - ale pamięć została zachowana.";
        _text[584, 7] = "Você regista as coordenadas.\n\nNão há extração agora - mas a memória foi guardada.";
        _text[584, 8] = "";
        _text[584, 9] = "";

        // 9_ResourceDialogue
        _text[585, 0] = "A mining capsule spins near the asteroid belt edge.\n\nIts scanner tag reads: \"THERMAL FUEL\".\n\nInside - compacted coal bricks, vacuum-sealed.";
        _text[585, 1] = "Добывающая капсула вращается у края астероидного пояса.\n\nМетка сканера: \"ТЕПЛОВОЕ ТОПЛИВО\".\n\nВнутри - прессованные угольные брикеты, в вакуумной упаковке.";
        _text[585, 2] = "La capsule minière orbite autour de la ceinture d'astéroïdes.\n\nÉtiquette du scanner: \"Combustible thermique\".\n\nÀ l'intérieur : briquettes de charbon compressé, emballées sous vide.";
        _text[585, 3] = "Una capsula estrattiva ruota ai margini della cintura di asteroidi.\n\nEtichetta dello scanner: «CARBURANTE TERMICO».\n\nAll'interno - mattonelle di carbone pressato, sottovuoto.";
        _text[585, 4] = "Eine Abbaukapsel rotiert am Rand des Asteroidengürtels.\n\nScanner-Markierung: \"THERMISCHER BRENNSTOFF\".\n\nIm Inneren - gepresste Kohlebriketts in Vakuumverpackung.";
        _text[585, 5] = "Una cápsula de extracción gira al borde del cinturón de asteroides.\n\nMarca del escáner: \"COMBUSTIBLE TÉRMICO\".\n\nDentro hay briquetas de carbón prensado, envasadas al vacío.";
        _text[585, 6] = "Kapsuła wydobywcza obraca się na skraju pasa asteroid.\n\nZnacznik skanera: \"PALIWO TERMICZNE\".\n\nW środku - sprasowane brykiety węglowe w próżniowym opakowaniu.";
        _text[585, 7] = "Uma cápsula de extração gira na orla do cinturão de asteroides.\n\nEtiqueta do scanner: \"COMBUSTÍVEL TÉRMICO\".\n\nNo interior - briquetes de carvão prensado, em embalagem a vácuo.";
        _text[585, 8] = "";
        _text[585, 9] = "";

        _text[586, 0] = "Match rotation and dock";
        _text[586, 1] = "Согласовать вращение и пристыковаться"; // выбор 1
        _text[586, 2] = "Rotation des coordonnées et amarrage";
        _text[586, 3] = "Sincronizzare la rotazione e attraccare";
        _text[586, 4] = "Rotation abstimmen und andocken";
        _text[586, 5] = "Sincronizar la rotación y acoplarse";
        _text[586, 6] = "Zsynchronizować obrót i zadokować";
        _text[586, 7] = "Sincronizar a rotação e acoplar";
        _text[586, 8] = "";
        _text[586, 9] = "";

        _text[587, 0] = "Success: you stabilize the spin and unload the coal.";
        _text[587, 1] = "Успех: вы стабилизируете вращение и выгружаете уголь."; // + Coal
        _text[587, 2] = "Succès : Vous stabilisez la rotation et déchargez le charbon.";
        _text[587, 3] = "Successo: stabilizzi la rotazione e scarichi il carbone.";
        _text[587, 4] = "Erfolg: Du stabilisierst die Rotation und entlädst die Kohle.";
        _text[587, 5] = "Éxito: estabilizas la rotación y descargas el carbón.";
        _text[587, 6] = "Sukces: stabilizujesz obrót i wyładowujesz węgiel.";
        _text[587, 7] = "Sucesso: você estabiliza a rotação e descarrega o carvão.";
        _text[587, 8] = "";
        _text[587, 9] = "";

        _text[588, 0] = "Failure: the lock misses by centimeters.\n\nThe capsule scrapes the hull - you lose some stored materials in emergency patching.";
        _text[588, 1] = "Провал: захват промахивается на сантиметры.\n\nКапсула царапает обшивку - на аварийный ремонт уходит часть запасов."; // - случайный ресурс
        _text[588, 2] = "Échec: la capture manque la cible de quelques centimètres.\n\nLa capsule érafle la coque - les réparations d’urgence consomment une partie des réserves.";
        _text[588, 3] = "Fallimento: la presa manca di pochi centimetri.\n\nLa capsula graffia il rivestimento - una parte delle scorte va alle riparazioni d'emergenza.";
        _text[588, 4] = "Misserfolg: Der Greifer verfehlt um Zentimeter.\n\nDie Kapsel zerkratzt die Hülle - für Notreparaturen geht ein Teil der Vorräte drauf.";
        _text[588, 5] = "Fracaso: la pinza falla por centímetros.\n\nLa cápsula araña el casco: parte de las reservas se va en la reparación de emergencia.";
        _text[588, 6] = "Porażka: chwytak mija o centymetry.\n\nKapsuła rysuje poszycie - część zapasów idzie na awaryjny remont.";
        _text[588, 7] = "Fracasso: a garra falha por centímetros.\n\nA cápsula risca o casco - parte das reservas é gasta em reparações de emergência.";
        _text[588, 8] = "";
        _text[588, 9] = "";

        _text[589, 0] = "Shoot the latch and pull with tractor";
        _text[589, 1] = "Сбить замок и вытянуть тягачом"; // выбор 2
        _text[589, 2] = "Défoncez l'écluse et retirez-la avec un tracteur.";
        _text[589, 3] = "Abbattere la serratura e tirare con il rimorchiatore";
        _text[589, 4] = "Schloss abschießen und mit dem Schlepper herausziehen";
        _text[589, 5] = "Romper el cierre y tirar con el remolcador";
        _text[589, 6] = "Wybić zamek i wyciągnąć holownikiem";
        _text[589, 7] = "Partir o fecho e puxar com o rebocador";
        _text[589, 8] = "";
        _text[589, 9] = "";

        _text[590, 0] = "Success: the latch breaks cleanly.";
        _text[590, 1] = "Успех: замок срывается чисто."; // + Coal
        _text[590, 2] = "Succès: la serrure se casse proprement.";
        _text[590, 3] = "Successo: la serratura salta via pulita.";
        _text[590, 4] = "Erfolg: Das Schloss löst sich sauber.";
        _text[590, 5] = "Éxito: el cierre se suelta limpiamente.";
        _text[590, 6] = "Sukces: zamek zostaje zerwany czysto.";
        _text[590, 7] = "Sucesso: o fecho solta-se limpo.";
        _text[590, 8] = "";
        _text[590, 9] = "";

        _text[591, 0] = "Failure: the shot punctures a fuel canister.\n\nCoal dust floods the bay - half the cargo is spoiled.";
        _text[591, 1] = "Провал: выстрел пробивает канистру.\n\nУгольная пыль заливает отсек - половина груза испорчена."; // ничего
        _text[591, 2] = "Échec: le projectile perfore le conteneur.De la poussière de charbon envahit le compartiment – ​​la moitié de la cargaison est perdue.";
        _text[591, 3] = "Fallimento: il colpo perfora una tanica.\n\nPolvere di carbone invade il compartimento - metà del carico è rovinata.";
        _text[591, 4] = "Misserfolg: Der Schuss durchschlägt einen Kanister.\n\nKohlestaub überflutet die Sektion - die Hälfte der Fracht ist verdorben.";
        _text[591, 5] = "Fracaso: el disparo perfora el bidón.\n\nEl polvo de carbón inunda el compartimento - la mitad de la carga se estropea.";
        _text[591, 6] = "Porażka: strzał przebija kanister.\n\nPył węglowy zalewa przedział - połowa ładunku jest zniszczona.";
        _text[591, 7] = "Fracasso: o disparo perfura uma lata.\n\nPó de carvão inunda o compartimento - metade da carga fica estragada.";
        _text[591, 8] = "";
        _text[591, 9] = "";

        // 10_ResourceDialogue
        _text[592, 0] = "Scanners detect a wrecked fuel depot on the surface of a desert planet.\n\nHalf-buried tanks and pipelines stretch under the sand.\n\nOne reservoir still shows pressure - there is usable machine fuel inside.";
        _text[592, 1] = "Сканеры фиксируют разрушенный топливный склад на поверхности пустынной планеты.\n\nПолузасыпанные резервуары и трубопроводы тянутся под песком.\n\nОдин бак всё ещё держит давление - внутри есть пригодное топливо для машин.";
        _text[592, 2] = "Des scanners détectent un dépôt de carburant détruit à la surface d'une planète désertique.\n\nDes réservoirs et des canalisations à moitié enfouis s'étendent sous le sable.\n\nUn réservoir est encore sous pression - il contient du carburant utilisable pour les véhicules.";
        _text[592, 3] = "Gli scanner rilevano un deposito di carburante distrutto sulla superficie di un pianeta desertico.\n\nSerbatoi semisepolti e tubazioni si estendono sotto la sabbia.\n\nUn serbatoio tiene ancora pressione - dentro c'è carburante utilizzabile per le macchine.";
        _text[592, 4] = "Scanner registrieren ein zerstörtes Treibstofflager auf der Oberfläche eines Wüstenplaneten.\n\nHalb verschüttete Tanks und Rohrleitungen ziehen sich unter dem Sand.\n\nEin Tank hält noch Druck - darin ist brauchbarer Treibstoff für Maschinen.";
        _text[592, 5] = "Los escáneres detectan un almacén de combustible destruido en la superficie de un planeta desértico.\n\nDepósitos semienterrados y tuberías se extienden bajo la arena.\n\nUn tanque aún mantiene la presión - dentro hay combustible utilizable para las máquinas.";
        _text[592, 6] = "Skanery wykrywają zrujnowany skład paliw na powierzchni pustynnej planety.\n\nNa wpół zasypane zbiorniki i rurociągi ciągną się pod piaskiem.\n\nJeden zbiornik wciąż trzyma ciśnienie - w środku jest paliwo nadające się do maszyn.";
        _text[592, 7] = "Os scanners detetam um armazém de combustível destruído na superfície de um planeta desértico.\n\nReservatórios meio soterrados e tubagens estendem-se sob a areia.\n\nUm tanque ainda mantém pressão - há combustível utilizável para máquinas no interior.";
        _text[592, 8] = "";
        _text[592, 9] = "";

        _text[593, 0] = "Connect sealed pumps and siphon fuel";
        _text[593, 1] = "Подключить гермопомпы и откачать топливо"; // выбор 1 + Oil, - Electricity
        _text[593, 2] = "Raccordez les pompes à pression et pompez le carburant.";
        _text[593, 3] = "Collegare pompe ermetiche e aspirare il carburante";
        _text[593, 4] = "Dichtpumpen anschließen und Treibstoff abpumpen";
        _text[593, 5] = "Conectar bombas herméticas y extraer el combustible";
        _text[593, 6] = "Podłączyć pompy hermetyczne i odpompować paliwo";
        _text[593, 7] = "Ligar as bombas herméticas e bombear o combustível";
        _text[593, 8] = "";
        _text[593, 9] = "";

        _text[594, 0] = "Cut into the pressurized pipe";
        _text[594, 1] = "Врезаться в трубу под давлением"; // выбор 2 Success + Oil, Failure - RandomResource
        _text[594, 2] = "Frapper un tuyau sous pression";
        _text[594, 3] = "Inserirsi in una tubazione in pressione";
        _text[594, 4] = "In die Druckleitung einschneiden";
        _text[594, 5] = "Perforar una tubería bajo presión";
        _text[594, 6] = "Wpiąć się w rurę pod ciśnieniem";
        _text[594, 7] = "Perfurar a tubagem sob pressão";
        _text[594, 8] = "";
        _text[594, 9] = "";

        _text[595, 0] = "Filter sludge from the bottom tanks";
        _text[595, 1] = "Отфильтровать шлам из нижних баков"; // выбор 3 + Oil, - Water
        _text[595, 2] = "Filtrer les boues des réservoirs inférieurs.";
        _text[595, 3] = "Filtrare il fango dai serbatoi inferiori";
        _text[595, 4] = "Schlamm aus den unteren Tanks filtern";
        _text[595, 5] = "Filtrar el lodo de los tanques inferiores";
        _text[595, 6] = "Przefiltrować szlam z dolnych zbiorników";
        _text[595, 7] = "Filtrar a borra dos tanques inferiores";
        _text[595, 8] = "";
        _text[595, 9] = "";

        _text[596, 0] = "You connect sealed hoses and start the pumps.\n\nFuel flows into protected containers.\n\nThe pumps draw a lot of power - the ship's energy reserve drops for a while.";
        _text[596, 1] = "Вы подключаете гермошланги и запускаете помпы.\n\nТопливо уходит в защищённые контейнеры.\n\nПомпы прожорливы - запас энергии корабля на время проседает."; // + Oil, - Electricity
        _text[596, 2] = "Vous raccordez les flexibles de pression et mettez les pompes en marche.\n\nLe carburant s'écoule dans des conteneurs protégés.\n\nLes pompes sont très gourmandes en énergie et puisent temporairement dans les réserves énergétiques du navire.";
        _text[596, 3] = "Colleghi i tubi ermetici e avvii le pompe.\n\nIl carburante passa in contenitori protetti.\n\nLe pompe sono voraci - le riserve energetiche della nave calano per un po'.";
        _text[596, 4] = "Du schließt Dichtschläuche an und startest die Pumpen.\n\nDer Treibstoff fließt in geschützte Container.\n\nDie Pumpen sind gefräßig - die Energiereserve des Schiffes sinkt vorübergehend.";
        _text[596, 5] = "Conectas mangueras herméticas y pones en marcha las bombas.\n\nEl combustible pasa a contenedores protegidos.\n\nLas bombas consumen mucho - la reserva de energía de la nave cae durante un tiempo.";
        _text[596, 6] = "Podłączasz hermetyczne węże i uruchamiasz pompy.\n\nPaliwo trafia do zabezpieczonych kontenerów.\n\nPompy są żarłoczne - zapas energii statku chwilowo spada.";
        _text[596, 7] = "Você liga as mangueiras herméticas e inicia as bombas.\n\nO combustível vai para contentores protegidos.\n\nAs bombas consomem muito - a reserva de energia da nave baixa por algum tempo.";
        _text[596, 8] = "";
        _text[596, 9] = "";

        _text[597, 0] = "Success: the pipe holds.\n\nA clean fuel stream rushes into the collectors.\n\nYou shut the valve and pull back before the pressure jumps.";
        _text[597, 1] = "Успех: труба выдерживает.\n\nЧистая струя топлива уходит в сборники.\n\nВы перекрываете клапан и отходите до того, как давление подпрыгнет."; // + Oil
        _text[597, 2] = "Succès: le tuyau tient bon.\n\nUn flux de carburant propre alimente les collecteurs.\n\nVous fermez la vanne et reculez avant que la pression ne monte brusquement.";
        _text[597, 3] = "Successo: la tubazione regge.\n\nUn getto pulito di carburante finisce nei serbatoi di raccolta.\n\nChiudi la valvola e ti allontani prima che la pressione salga.";
        _text[597, 4] = "Erfolg: Die Rohrleitung hält.\n\nEin sauberer Strahl Treibstoff läuft in die Sammler.\n\nDu schließt das Ventil und ziehst ab, bevor der Druck hochschießt.";
        _text[597, 5] = "Éxito: la tubería aguanta.\n\nUn chorro limpio de combustible va a los colectores.\n\nCierras la válvula y te retiras antes de que la presión se dispare.";
        _text[597, 6] = "Sukces: rura wytrzymuje.\n\nCzysty strumień paliwa trafia do zbiorników.\n\nZakręcasz zawór i wycofujesz się, zanim ciśnienie skoczy.";
        _text[597, 7] = "Sucesso: a tubagem aguenta.\n\nUm jato limpo de combustível entra nos coletores.\n\nVocê fecha a válvula e afasta-se antes de a pressão disparar.";
        _text[597, 8] = "";
        _text[597, 9] = "";

        _text[598, 0] = "Failure: the pipe ruptures.\n\nFuel mist floods the site. You vent the bay and spend supplies on emergency sealing.\n\nExtraction is aborted.";
        _text[598, 1] = "Провал: трубу разрывает.\n\nТопливный туман накрывает площадку. Вы продуваете отсек и тратите запасы на аварийную герметизацию.\n\nОткачка сорвана."; // - RandomResource
        _text[598, 2] = "Panne: la conduite se rompt.\n\nUn brouillard de carburant recouvre la plateforme. Vous purgez le compartiment et utilisez les ressources disponibles pour effectuer un colmatage d'urgence.Le pompage est interrompu.";
        _text[598, 3] = "Fallimento: la tubazione si spezza.\n\nUna nebbia di carburante avvolge l'area. Spurghi il compartimento e consumi scorte per una sigillatura d'emergenza.\n\nIl pompaggio è fallito.";
        _text[598, 4] = "Misserfolg: Die Leitung reißt.\n\nEin Treibstoffnebel bedeckt die Anlage. Du spülst die Sektion aus und verbrauchst Vorräte für Notabdichtung.\n\nDas Abpumpen scheitert.";
        _text[598, 5] = "Fracaso: la tubería se revienta.\n\nUna niebla de combustible cubre la zona. Purga el compartimento y gastas reservas en la hermetización de emergencia.\n\nLa extracción se arruina.";
        _text[598, 6] = "Porażka: rura pęka.\n\nPaliwowa mgła zalewa teren. Przewietrzasz przedział i zużywasz zapasy na awaryjne uszczelnienie.\n\nOdpompowanie zostaje przerwane.";
        _text[598, 7] = "Fracasso: a tubagem rebenta.\n\nUma névoa de combustível cobre a área. Você purga o compartimento e gasta reservas em selagem de emergência.\n\nA bombagem falha.";
        _text[598, 8] = "";
        _text[598, 9] = "";

        _text[599, 0] = "You collect thick sludge from the bottom tanks and run it through filters.\n\nIt takes water to cool and wash the system.\n\nThe output is rough, but it burns.";
        _text[599, 1] = "Вы собираете густой шлам со дна баков и прогоняете его через фильтры.\n\nНужна вода, чтобы охлаждать и промывать систему.\n\nТопливо получается грубым, но оно горит."; // + Oil, - Water
        _text[599, 2] = "On récupère les boues épaisses au fond des réservoirs et on les filtre.\n\nIl faut de l'eau pour refroidir et rincer le système.\n\nLe carburant est grossier, mais il brûle.";
        _text[599, 3] = "Raccogli il fango denso dal fondo dei serbatoi e lo fai passare attraverso i filtri.\n\nServe acqua per raffreddare e lavare il sistema.\n\nIl carburante viene grezzo, ma brucia.";
        _text[599, 4] = "Du sammelst dicken Schlamm vom Boden der Tanks und jagst ihn durch Filter.\n\nDu brauchst Wasser, um das System zu kühlen und zu spülen.\n\nDer Treibstoff ist grob, aber er brennt.";
        _text[599, 5] = "Recoges el lodo espeso del fondo de los tanques y lo pasas por filtros.\n\nSe necesita agua para enfriar y lavar el sistema.\n\nEl combustible sale tosco, pero arde.";
        _text[599, 6] = "Zbierasz gęsty szlam z dna zbiorników i przepuszczasz go przez filtry.\n\nPotrzebna jest woda, by chłodzić i płukać układ.\n\nPaliwo wychodzi prymitywne, ale się pali.";
        _text[599, 7] = "Você recolhe a borra espessa do fundo dos tanques e passa-a por filtros.\n\nÉ necessária água para arrefecer e lavar o sistema.\n\nO combustível fica grosseiro, mas arde.";
        _text[599, 8] = "";
        _text[599, 9] = "";

        // 1_MaterialDialogue
        _text[600, 0] = "The cameras catch a broken cargo barge tumbling nearby. Its containers have split open.\n\nInside: stacks of cut stone blocks, packed for construction and forgotten in the dark.";
        _text[600, 1] = "Камеры фиксируют рядом сломанную грузовую баржу. Её контейнеры разорваны.\n\nВнутри - стопки каменных блоков, заготовленных для строительства и забытых в темноте.";
        _text[600, 2] = "Des caméras capturent une barge de transport de marchandises échouée à proximité. Ses conteneurs sont éventrés.\n\nÀ l'intérieur, des piles de blocs de pierre, destinés à la construction, sont oubliées dans l'obscurité.";
        _text[600, 3] = "Le telecamere individuano lì vicino una chiatta cargo in avaria. I suoi contenitori sono squarciati.\n\nAll'interno - pile di blocchi di pietra, preparati per la costruzione e dimenticati nel buio.";
        _text[600, 4] = "Kameras erfassen in der Nähe eine beschädigte Frachtbarke. Ihre Container sind aufgerissen.\n\nIm Inneren - Stapel von Steinblöcken, für Bau vorbereitet und in der Dunkelheit vergessen.";
        _text[600, 5] = "Las cámaras registran cerca una barcaza de carga averiada. Sus contenedores están destrozados.\n\nDentro hay pilas de bloques de piedra, preparados para construcción y olvidados en la oscuridad.";
        _text[600, 6] = "Kamery rejestrują w pobliżu uszkodzoną barkę transportową. Jej kontenery są rozerwane.\n\nW środku leżą stosy kamiennych bloków, przygotowanych do budowy i zapomnianych w ciemności.";
        _text[600, 7] = "As câmaras registam uma barcaça de carga partida ali perto. Os seus contentores estão rasgados.\n\nNo interior - pilhas de blocos de pedra, preparados para construção e esquecidos na escuridão.";
        _text[600, 8] = "";
        _text[600, 9] = "";

        _text[601, 0] = "Salvage the blocks";
        _text[601, 1] = "Забрать блоки"; // выбор 1
        _text[601, 2] = "Ramassez les blocs";
        _text[601, 3] = "Recuperare i blocchi";
        _text[601, 4] = "Blöcke bergen";
        _text[601, 5] = "Recoger los bloques";
        _text[601, 6] = "Zabrać bloki";
        _text[601, 7] = "Recolher os blocos";
        _text[601, 8] = "";
        _text[601, 9] = "";

        _text[602, 0] = "Success: the drones latch onto the containers and tow them in.\n\nYou reinforce the cargo bay and secure the load.";
        _text[602, 1] = "Успех: дроны цепляют контейнеры и затаскивают их внутрь.\n\nВы укрепляете грузовой отсек и фиксируете добычу."; // + каменный блок
        _text[602, 2] = "Succès: Les drones récupèrent les conteneurs et les traînent à l’intérieur.\n\nVous renforcez la cale et sécurisez le butin.";
        _text[602, 3] = "Successo: i droni agganciano i contenitori e li trascinano all'interno.\n\nRinforzi il vano di carico e fissi il bottino.";
        _text[602, 4] = "Erfolg: Drohnen haken die Container ein und ziehen sie hinein.\n\nDu verstärkst den Frachtraum und sicherst die Beute.";
        _text[602, 5] = "Éxito: los drones enganchan los contenedores y los arrastran al interior.\n\nRefuerzas la bodega de carga y aseguras el botín.";
        _text[602, 6] = "Sukces: drony chwytają kontenery i wciągają je do środka.\n\nWzmacniasz ładownię i zabezpieczasz zdobycz.";
        _text[602, 7] = "Sucesso: os drones prendem os contêineres e os puxam para dentro.\n\nVocê reforça o compartimento de carga e fixa o saque.";
        _text[602, 8] = "";
        _text[602, 9] = "";

        _text[603, 0] = "Failure: the barge rotates unexpectedly. A container slams into the hull, tearing plating.\n\nDrones rush to seal the breach as you disengage.";
        _text[603, 1] = "Провал: баржа внезапно проворачивается. Контейнер с силой врезается в корпус, срывая обшивку.\n\nДроны срочно герметизируют пробоину, пока вы отходите на безопасную дистанцию."; // - ядро
        _text[603, 2] = "Échec: La barge se met soudainement à tourner sur elle-même. Le conteneur percute la coque et l’arrache.\n\nDes drones se précipitent pour colmater la brèche tandis que vous vous repliez à une distance de sécurité.";
        _text[603, 3] = "Fallimento: la chiatta ruota all'improvviso. Il contenitore si schianta con forza contro lo scafo, strappando il rivestimento.\n\nI droni sigillano d'urgenza la falla mentre ti allontani a distanza di sicurezza.";
        _text[603, 4] = "Misserfolg: Die Barge dreht sich plötzlich. Ein Container kracht mit Wucht in den Rumpf und reißt die Außenhaut auf.\n\nDie Drohnen dichten das Leck hastig ab, während du auf sichere Distanz gehst.";
        _text[603, 5] = "Fracaso: la barcaza gira de golpe. El contenedor se estrella con fuerza contra el casco, arrancando el revestimiento.\n\nLos drones sellan la brecha de emergencia mientras te retiras a una distancia segura.";
        _text[603, 6] = "Porażka: barka nagle się obraca. Kontener z impetem uderza w kadłub, zrywając poszycie.\n\nDrony awaryjnie uszczelniają wyrwę, a ty wycofujesz się na bezpieczną odległość.";
        _text[603, 7] = "Falha: a barcaça gira de repente. O contêiner atinge o casco com força, arrancando parte do revestimento.\n\nOs drones selam a brecha às pressas enquanto você se afasta para uma distância segura.";
        _text[603, 8] = "";
        _text[603, 9] = "";

        _text[604, 0] = "Ignore";
        _text[604, 1] = "Проигнорировать"; // выбор 2
        _text[604, 2] = "Ignorer";
        _text[604, 3] = "Ignorare";
        _text[604, 4] = "Ignorieren";
        _text[604, 5] = "Ignorar";
        _text[604, 6] = "Zignorować";
        _text[604, 7] = "Ignorar";
        _text[604, 8] = "";
        _text[604, 9] = "";

        _text[605, 0] = "You leave the wreck behind. The barge keeps spinning in silence, shedding stone into the void.";
        _text[605, 1] = "Вы оставляете обломки позади. Баржа продолжает вращаться в тишине, рассыпая камень в пустоту."; // ничего
        _text[605, 2] = "Vous laissez l'épave derrière vous. La barge continue de tourner en silence, dispersant des pierres dans le vide.";
        _text[605, 3] = "Lasci i rottami alle spalle. La chiatta continua a ruotare nel silenzio, disperdendo pietra nel vuoto.";
        _text[605, 4] = "Du lässt die Trümmer hinter dir. Die Barge rotiert weiter in der Stille und streut Gestein in die Leere.";
        _text[605, 5] = "Dejas los restos atrás. La barcaza sigue girando en silencio, esparciendo piedra en el vacío.";
        _text[605, 6] = "Zostawiasz szczątki za sobą. Barka nadal obraca się w ciszy, rozsypując kamień w pustkę.";
        _text[605, 7] = "Você deixa os destroços para trás. A barcaça continua girando em silêncio, espalhando pedra no vazio.";
        _text[605, 8] = "";
        _text[605, 9] = "";

        // 2_MaterialDialogue
        _text[606, 0] = "A trade ship crosses your route. Its hull is patched with welded plates, and the cargo modules are wrapped in heat shielding.\n\nA short message breaks through:\n\n\"Iron ingots. Clean cast. Fixed price.\"";
        _text[606, 1] = "Торговый корабль пересекает ваш маршрут. Его корпус залатан сварными пластинами, а грузовые модули закрыты термозащитными кожухами.\n\nВ эфир проходит короткое сообщение:\n\n\"Железные слитки. Чистое литьё. Цена фиксирована\".";
        _text[606, 2] = "Un navire marchand croise votre route. Sa coque est rafistolée avec des plaques soudées et ses modules de cargaison sont protégés par des boucliers thermiques.\n\nUn court message est diffusé:\n\n\"Lingots de fer. Fonte pure. Prix fixe\".";
        _text[606, 3] = "Una nave mercantile incrocia la tua rotta. Lo scafo è rattoppato con piastre saldate, e i moduli di carico sono coperti da carenature termiche.\n\nNell'etere passa un breve messaggio:\n\n\"Lingotti di ferro. Fusione pura. Prezzo fisso\".";
        _text[606, 4] = "Ein Handelsschiff kreuzt deine Route. Sein Rumpf ist mit Schweißplatten geflickt, und die Frachmodule sind mit Hitzeschutzhüllen abgedeckt.\n\nIm Äther kommt eine kurze Nachricht:\n\n\"Eisenbarren. Reiner Guss. Preis fest.\".";
        _text[606, 5] = "Un mercante cruza tu ruta. Su casco está remendado con placas soldadas y los módulos de carga están cubiertos con carenados térmicos.\n\nEn la radio entra un breve mensaje:\n\n\"Lingotes de hierro. Fundición limpia. Precio fijo\".";
        _text[606, 6] = "Statek handlowy przecina twój kurs. Jego kadłub jest połatany spawanymi płytami, a moduły ładunkowe osłonięte pokrywami termoizolacyjnymi.\n\nW eter idzie krótka wiadomość:\n\n\"Żelazne sztaby. Czysty odlew. Cena stała\".";
        _text[606, 7] = "Um navio mercante cruza a sua rota. O casco está remendado com placas soldadas, e os módulos de carga estão cobertos por capas de proteção térmica.\n\nUma mensagem curta passa pelo rádio:\n\n\"Barras de ferro. Fundição pura. Preço fixo\".";
        _text[606, 8] = "";
        _text[606, 9] = "";

        _text[607, 0] = "Buy ingots for quants";
        _text[607, 1] = "Купить слитки за кванты"; // выбор 1
        _text[607, 2] = "Acheter des lingots pour des quanta";
        _text[607, 3] = "Comprare lingotti per quant";
        _text[607, 4] = "Barren für quants kaufen";
        _text[607, 5] = "Comprar lingotes por quant";
        _text[607, 6] = "Kupić sztaby za quant";
        _text[607, 7] = "Comprar barras por quant";
        _text[607, 8] = "";
        _text[607, 9] = "";

        _text[608, 0] = "The transfer completes. Sealed crates are pushed toward you along a magnetic tether.\n\nThe trader cuts the channel and changes course.";
        _text[608, 1] = "Обмен завершён. Герметичные ящики подтягиваются к вам по магнитному тросу.\n\nТорговец обрывает связь и меняет курс."; // - кванты, + железные слитки
        _text[608, 2] = "L'échange est terminé. Les caisses scellées sont attirées vers vous par un câble magnétique.\n\nLe commerçant rompt le contact et change de cap.";
        _text[608, 3] = "Scambio completato. Le casse ermetiche vengono tirate verso di te con un cavo magnetico.\n\nIl mercante interrompe la comunicazione e cambia rotta.";
        _text[608, 4] = "Tausch abgeschlossen. Versiegelte Kisten werden per Magnetseil zu dir herangezogen.\n\nDer Händler bricht die Verbindung ab und ändert den Kurs.";
        _text[608, 5] = "Intercambio completado. Las cajas herméticas se acercan hacia ti por un cable magnético.\n\nEl comerciante corta la comunicación y cambia de rumbo.";
        _text[608, 6] = "Wymiana zakończona. Hermetyczne skrzynie są wciągane do ciebie po magnetycznej linie.\n\nHandlarz zrywa łączność i zmienia kurs.";
        _text[608, 7] = "Troca concluída. Caixas herméticas são puxadas até você por um cabo magnético.\n\nO comerciante encerra a comunicação e muda de rumo.";
        _text[608, 8] = "";
        _text[608, 9] = "";

        _text[609, 0] = "Attack the trade ship";
        _text[609, 1] = "Напасть на корабль"; // выбор 2
        _text[609, 2] = "Attaquer le navire";
        _text[609, 3] = "Attaccare la nave";
        _text[609, 4] = "Das Schiff angreifen";
        _text[609, 5] = "Atacar la nave";
        _text[609, 6] = "Napaść na statek";
        _text[609, 7] = "Atacar o navio";
        _text[609, 8] = "";
        _text[609, 9] = "";

        _text[610, 0] = "Success: a precise strike disables their drive. Drones cut the cargo locks and pull the containers free.\n\nYou disengage before the distress signal can spread.";
        _text[610, 1] = "Успех: точный удар выводит из строя их привод. Дроны вскрывают грузовые замки и отцепляют контейнеры.\n\nВы уходите, пока сигнал бедствия не успел разойтись."; // + железные слитки
        _text[610, 2] = "Succès : une frappe précise neutralise leur système de propulsion. Les drones crochetent les verrous de la cargaison et détachent les conteneurs.\n\nVous partez avant que le signal de détresse n’ait eu le temps de se propager.";
        _text[610, 3] = "Successo: un colpo preciso mette fuori uso la loro propulsione. I droni forzano i blocchi di carico e sganciano i contenitori.\n\nTi allontani prima che il segnale di soccorso riesca a diffondersi.";
        _text[610, 4] = "Erfolg: Ein präziser Schlag setzt ihren Antrieb außer Gefecht. Drohnen knacken die Frachtschlösser und koppeln die Container ab.\n\nDu verschwindest, bevor sich das Notsignal verbreiten kann.";
        _text[610, 5] = "Éxito: un golpe preciso inutiliza su propulsión. Los drones fuerzan los cierres de carga y desacoplan los contenedores.\n\nTe vas antes de que la señal de socorro se propague.";
        _text[610, 6] = "Sukces: precyzyjne uderzenie wyłącza ich napęd. Drony otwierają zamki ładunkowe i odczepiają kontenery.\n\nOdchodzisz, zanim sygnał SOS zdąży się rozejść.";
        _text[610, 7] = "Sucesso: um golpe preciso inutiliza o propulsor deles. Os drones arrombam as travas de carga e desprendem os contêineres.\n\nVocê parte antes que o sinal de socorro consiga se espalhar.";
        _text[610, 8] = "";
        _text[610, 9] = "";

        _text[611, 0] = "Failure: the trader was armed. A burst damages your hull and knocks out part of your systems.\n\nYou lose a core and engage warp, leaving the battlefield.";
        _text[611, 1] = "Провал: торговец оказался вооружён. Очередь повреждает обшивку и выводит из строя часть систем.\n\nВы теряете одно ядро и включаете варп, уходя с поля боя."; // -1 ядро, без слитков
        _text[611, 2] = "Échec: Le vaisseau marchand est armé. L’explosion endommage la coque et met hors service certains systèmes.\n\nVous perdez un noyau et passez en distorsion pour fuir le champ de bataille.";
        _text[611, 3] = "Fallimento: il mercante era armato. Una raffica danneggia lo scafo e mette fuori uso parte dei sistemi.\n\nPerdi un nucleo e attivi il warp, lasciando il campo di battaglia.";
        _text[611, 4] = "Misserfolg: Der Händler war bewaffnet. Eine Salve beschädigt die Außenhaut und legt einen Teil der Systeme lahm.\n\nDu verlierst einen Kern und zündest den Warp, um das Gefecht zu verlassen.";
        _text[611, 5] = "Fracaso: el mercante estaba armado. Una ráfaga daña el casco e inutiliza parte de los sistemas.\n\nPierdes un núcleo y activas el warp, abandonando el combate.";
        _text[611, 6] = "Porażka: handlarz okazał się uzbrojony. Seria uszkadza poszycie i wyłącza część systemów.\n\nTracisz jeden rdzeń i włączasz warp, opuszczając pole walki.";
        _text[611, 7] = "Falha: o comerciante estava armado. Uma rajada danifica o casco e desativa parte dos sistemas.\n\nVocê perde um núcleo e aciona o warp, saindo do combate.";
        _text[611, 8] = "";
        _text[611, 9] = "";

        // 3_MaterialDialogue
        _text[612, 0] = "You detect a bulky smelter platform drifting nearby.\n\nWork lights are on. You see movement behind the heat shields - not drones.\n\nA tired voice comes through the comms:\n\n\"Ship. Do you have iron ingots? Our furnace is running, but we're out of feed.\n\nGive us iron - we'll melt it and cast steel.\"";
        _text[612, 1] = "Вы обнаружили рядом громоздкую плавильную платформу.\n\nНа ней горит рабочее освещение. За термощитами заметно движение - это не дроны.\n\nВ связь выходит усталый голос:\n\n\"Корабль. Есть железные слитки? Печь работает, но сырьё закончилось.\n\nДайте железо - мы переплавим и отольём сталь\".";
        _text[612, 2] = "Vous apercevez une imposante plateforme de fusion à proximité.\n\nLes projecteurs sont allumés. On distingue des mouvements derrière les boucliers thermiques: ce ne sont pas des drones.Une voix lasse se fait entendre\n\n\"Vaisseau. Avez-vous des lingots de fer? Le fourneau fonctionne, mais nous sommes à court de matières premières.\n\nDonnez-nous le fer - nous le fondrons et le coulerons en acier. »";
        _text[612, 3] = "Individui una massiccia piattaforma di fusione nelle vicinanze.\n\nLe luci di lavoro sono accese. Dietro gli scudi termici si vede movimento - non sono droni.\n\nIn comunicazione entra una voce stanca:\n\n\"Nave. Avete lingotti di ferro? Il forno funziona, ma la materia prima è finita.\n\nDateci ferro - lo rifonderemo e coleremo acciaio\".";
        _text[612, 4] = "Du entdeckst in der Nähe eine wuchtige Schmelzplattform.\n\nAuf ihr brennt Arbeitslicht. Hinter den Hitzeschildern ist Bewegung zu sehen - das sind keine Drohnen.\n\nEine müde Stimme meldet sich:\n\n\"Schiff. Hast du Eisenbarren? Der Ofen läuft, aber das Rohmaterial ist alle.\n\nGib Eisen - wir schmelzen es um und gießen Stahl.\".";
        _text[612, 5] = "Descubres cerca una voluminosa plataforma de fundición.\n\nLas luces de trabajo están encendidas. Tras los escudos térmicos se ve movimiento: no son drones.\n\nEn la comunicación entra una voz cansada:\n\n\"Nave. ¿Tienes lingotes de hierro? El horno funciona, pero se acabó la materia prima.\n\nDanos hierro: lo refundiremos y colaremos acero\".";
        _text[612, 6] = "Wykrywasz w pobliżu masywną platformę hutniczą.\n\nPali się na niej oświetlenie robocze. Za osłonami termicznymi widać ruch - to nie drony.\n\nW łączność wchodzi zmęczony głos:\n\n\"Statek. Masz żelazne sztaby? Piec działa, ale surowiec się skończył.\n\nDaj żelazo - przetopimy i odlejemy stal\".";
        _text[612, 7] = "Você encontra uma plataforma de fundição volumosa por perto.\n\nAs luzes de trabalho estão acesas. Há movimento atrás dos escudos térmicos - não são drones.\n\nUma voz cansada entra na comunicação:\n\n\"Nave. Tem barras de ferro? O forno funciona, mas a matéria-prima acabou.\n\nDê ferro - nós vamos fundir e moldar aço\".";
        _text[612, 8] = "";
        _text[612, 9] = "";

        _text[613, 0] = "Transfer iron ingots";
        _text[613, 1] = "Передать железные слитки"; // выбор 1
        _text[613, 2] = "Remettez-moi les lingots de fer";
        _text[613, 3] = "Consegnare lingotti di ferro";
        _text[613, 4] = "Eisenbarren übergeben";
        _text[613, 5] = "Entregar lingotes de hierro";
        _text[613, 6] = "Przekazać żelazne sztaby";
        _text[613, 7] = "Entregar barras de ferro";
        _text[613, 8] = "";
        _text[613, 9] = "";

        _text[614, 0] = "The platform locks onto your crates and pulls them into the smelting line.\n\nAfter a short wait, a cooled container is returned: steel ingots, sealed and marked.";
        _text[614, 1] = "Платформа фиксирует ваши ящики и подаёт их на линию переплавки.\n\nЧерез некоторое время возвращается остуженный контейнер: стальные слитки, запечатанные и промаркированные."; // - железные слитки, + стальные слитки
        _text[614, 2] = "La plateforme sécurise vos caisses et les achemine vers la ligne de refusion.\n\nAprès un certain temps, le conteneur refroidi revient: des lingots d’acier, scellés et étiquetés.";
        _text[614, 3] = "La piattaforma aggancia le tue casse e le immette nella linea di rifusione.\n\nDopo un po' torna un contenitore raffreddato: lingotti d'acciaio, sigillati e marcati.";
        _text[614, 4] = "Die Plattform fixiert deine Kisten und führt sie der Schmelzlinie zu.\n\nNach einiger Zeit kommt ein abgekühlter Container zurück: Stahlbarren, versiegelt und markiert.";
        _text[614, 5] = "La plataforma asegura tus cajas y las alimenta a la línea de refundición.\n\nAl cabo de un rato vuelve un contenedor enfriado: lingotes de acero, sellados y marcados.";
        _text[614, 6] = "Platforma blokuje twoje skrzynie i podaje je na linię przetopu.\n\nPo pewnym czasie wraca schłodzony kontener: stalowe sztaby, zaplombowane i oznaczone.";
        _text[614, 7] = "A plataforma prende as suas caixas e as envia para a linha de fusão.\n\nDepois de algum tempo, retorna um contêiner resfriado: barras de aço, seladas e marcadas.";
        _text[614, 8] = "";
        _text[614, 9] = "";

        _text[615, 0] = "Refuse";
        _text[615, 1] = "Отказаться"; // выбор 2
        _text[615, 2] = "Refuser";
        _text[615, 3] = "Rifiutare";
        _text[615, 4] = "Ablehnen";
        _text[615, 5] = "Negarse";
        _text[615, 6] = "Odmówić";
        _text[615, 7] = "Recusar";
        _text[615, 8] = "";
        _text[615, 9] = "";

        _text[616, 0] = "The channel closes. The platform continues its work without responding.";
        _text[616, 1] = "Канал закрывается. Платформа продолжает работу и больше не отвечает."; // ничего
        _text[616, 2] = "La chaîne ferme. La plateforme continue de fonctionner mais ne répond plus.";
        _text[616, 3] = "Il canale si chiude. La piattaforma continua a lavorare e non risponde più.";
        _text[616, 4] = "Der Kanal schließt sich. Die Plattform arbeitet weiter und antwortet nicht mehr.";
        _text[616, 5] = "El canal se cierra. La plataforma sigue trabajando y no vuelve a responder.";
        _text[616, 6] = "Kanał się zamyka. Platforma kontynuuje pracę i już nie odpowiada.";
        _text[616, 7] = "O canal é encerrado. A plataforma continua operando e não responde mais.";
        _text[616, 8] = "";
        _text[616, 9] = "";

        _text[617, 0] = "Try to take steel by force";
        _text[617, 1] = "Попытаться забрать сталь силой"; // выбор 3
        _text[617, 2] = "Essayez de prendre l'acier par la force.";
        _text[617, 3] = "Provare a prendere l'acciaio con la forza";
        _text[617, 4] = "Versuchen, den Stahl mit Gewalt zu nehmen";
        _text[617, 5] = "Intentar llevarse el acero por la fuerza";
        _text[617, 6] = "Spróbować zabrać stal siłą";
        _text[617, 7] = "Tentar pegar o aço à força";
        _text[617, 8] = "";
        _text[617, 9] = "";

        _text[618, 0] = "Success: you disable the outer locks and pull one container free.\n\nYou engage warp and leave with steel ingots.";
        _text[618, 1] = "Успех: вы выводите из строя внешние замки и отцепляете один контейнер.\n\nВы включаете варп и уходите со стальными слитками."; // + стальные слитки
        _text[618, 2] = "Succès: Vous désactivez les verrous externes et détachez un conteneur.\n\nVous activez le saut spatial et vous échappez avec les lingots d’acier.";
        _text[618, 3] = "Successo: metti fuori uso i blocchi esterni e sganci un contenitore.\n\nAttivi il warp e ti allontani con i lingotti d'acciaio.";
        _text[618, 4] = "Erfolg: Du setzt die äußeren Schlösser außer Gefecht und koppelst einen Container ab.\n\nDu aktivierst den Warp und verschwindest mit Stahlbarren.";
        _text[618, 5] = "Éxito: inutilizas los cierres externos y desacoplas un contenedor.\n\nActivas el warp y te vas con los lingotes de acero.";
        _text[618, 6] = "Sukces: uszkadzasz zewnętrzne zamki i odczepiasz jeden kontener.\n\nWłączasz warp i odchodzisz ze stalowymi sztabami.";
        _text[618, 7] = "Sucesso: você inutiliza as travas externas e desprende um contêiner.\n\nVocê aciona o warp e parte com as barras de aço.";
        _text[618, 8] = "";
        _text[618, 9] = "";

        _text[619, 0] = "Failure: the platform triggers an emergency vent. A blast of superheated gas hits the hull, damaging plating and overloading a core.\n\nYou lose one core and engage warp, leaving the platform behind.";
        _text[619, 1] = "Провал: платформа включает аварийный сброс. Выброс перегретого газа бьёт по корпусу, повреждает обшивку и перегружает ядро.\n\nВы теряете одно ядро и включаете варп, оставляя платформу позади."; // -1 ядро
        _text[619, 2] = "Panne: La plateforme déclenche une réinitialisation d'urgence. Une explosion de gaz surchauffé percute la coque, endommageant le blindage et surchargeant le réacteur.\n\nVous perdez un réacteur et entamez une distorsion spatiale, abandonnant la plateforme.";
        _text[619, 3] = "Fallimento: la piattaforma attiva uno scarico d'emergenza. Un getto di gas surriscaldato colpisce lo scafo, danneggia il rivestimento e sovraccarica il nucleo.\n\nPerdi un nucleo e attivi il warp, lasciando la piattaforma alle spalle.";
        _text[619, 4] = "Misserfolg: Die Plattform aktiviert den Notabwurf. Ein Schwall überhitzten Gases trifft den Rumpf, beschädigt die Außenhaut und überlastet den Kern.\n\nDu verlierst einen Kern und zündest den Warp, die Plattform hinter dir lassend.";
        _text[619, 5] = "Fracaso: la plataforma activa un lanzamiento de emergencia. Una descarga de gas sobrecalentado golpea el casco, daña el revestimiento y sobrecarga el núcleo.\n\nPierdes un núcleo y activas el warp, dejando la plataforma atrás.";
        _text[619, 6] = "Porażka: platforma uruchamia awaryjny zrzut. Wyrzut przegrzanego gazu uderza w kadłub, uszkadza poszycie i przeciąża rdzeń.\n\nTracisz jeden rdzeń i włączasz warp, zostawiając platformę za sobą.";
        _text[619, 7] = "Falha: a plataforma ativa um descarte de emergência. Um jato de gás superaquecido atinge o casco, danifica o revestimento e sobrecarrega o núcleo.\n\nVocê perde um núcleo e aciona o warp, deixando a plataforma para trás.";
        _text[619, 8] = "";
        _text[619, 9] = "";

        // 4_MaterialDialogue
        _text[620, 0] = "A fault in the engine block forces you to set down on the nearest planet for repairs.\n\nYou choose the dark side and land with minimal thrust, cutting all external lights.\n\nWhile the drones inspect the damage, the sensors catch faint heat signatures not far away.\n\nAhead: a small processing site still operating - generators, containers, and a warehouse marked for copper.\n\nInside are crates of copper plates.";
        _text[620, 1] = "Сбой в узле двигателя вынуждает вас сесть на ближайшую планету для ремонта.\n\nВы выбираете тёмную сторону и садитесь на минимальной тяге, полностью гасите внешнее освещение.\n\nПока дроны осматривают повреждения, сенсоры фиксируют слабые тепловые сигнатуры неподалёку.\n\nВпереди - небольшой перерабатывающий участок: генераторы, контейнеры и склад с маркировкой меди.\n\nВнутри - ящики с медными пластинами.";
        _text[620, 2] = "Une panne moteur vous oblige à atterrir sur la planète la plus proche pour réparation.\n\nVous choisissez le côté obscur et atterrissez à poussée minimale, éteignant complètement toute lumière extérieure.Pendant que des drones inspectent les dégâts, des capteurs détectent de faibles signatures thermiques à proximité.Plus loin se trouve une petite installation de traitement: des générateurs, des conteneurs et un entrepôt marqué par le cuivre.\n\nÀ l’intérieur se trouvent des caisses de plaques de cuivre.";
        _text[620, 3] = "Un guasto al nodo del motore ti costringe ad atterrare sul pianeta più vicino per riparare.\n\nScegli il lato in ombra e scendi con spinta minima, spegnendo completamente le luci esterne.\n\nMentre i droni ispezionano i danni, i sensori rilevano deboli firme termiche nelle vicinanze.\n\nDavanti a te c'è un piccolo impianto di lavorazione: generatori, contenitori e un magazzino con marcatura del rame.\n\nDentro - casse di piastre di rame.";
        _text[620, 4] = "Ein Defekt im Triebwerksknoten zwingt dich, für Reparaturen auf dem nächsten Planeten zu landen.\n\nDu wählst die dunkle Seite und setzt mit minimalem Schub auf, schaltest die Außenbeleuchtung vollständig ab.\n\nWährend die Drohnen den Schaden prüfen, registrieren die Sensoren schwache Wärmesignaturen in der Nähe.\n\nVoraus liegt ein kleiner Aufbereitungsbereich: Generatoren, Container und ein Lager mit Kupfer-Markierung.\n\nDrinnen stehen Kisten mit Kupferplatten.";
        _text[620, 5] = "Un fallo en el módulo del motor te obliga a aterrizar en el planeta más cercano para reparaciones.\n\nEliges el lado oscuro y desciendes con empuje mínimo, apagando por completo la iluminación exterior.\n\nMientras los drones inspeccionan los daños, los sensores detectan débiles firmas térmicas cerca.\n\nDelante hay una pequeña zona de procesamiento: generadores, contenedores y un almacén con marcaje de cobre.\n\nDentro hay cajas con placas de cobre.";
        _text[620, 6] = "Awaria w węźle silnika zmusza cię do lądowania na najbliższej planecie w celu naprawy.\n\nWybierasz ciemną stronę i schodzisz na minimalnym ciągu, całkowicie gasząc zewnętrzne oświetlenie.\n\nGdy drony sprawdzają uszkodzenia, sensory rejestrują słabe sygnatury cieplne w pobliżu.\n\nPrzed tobą - niewielki punkt przetwarzania: generatory, kontenery i magazyn z oznaczeniami miedzi.\n\nW środku - skrzynie z miedzianymi płytami.";
        _text[620, 7] = "Uma falha no nó do motor obriga você a pousar no planeta mais próximo para reparos.\n\nVocê escolhe o lado escuro e pousa com empuxo mínimo, apagando completamente as luzes externas.\n\nEnquanto os drones inspecionam os danos, os sensores registram assinaturas térmicas fracas ali perto.\n\nÀ frente há um pequeno ponto de processamento: geradores, contêineres e um depósito com marcação de cobre.\n\nDentro - caixas com placas de cobre.";
        _text[620, 8] = "";
        _text[620, 9] = "";

        _text[621, 0] = "Do not risk it";
        _text[621, 1] = "Не рисковать"; // выбор 1
        _text[621, 2] = "Ne prenez pas de risques";
        _text[621, 3] = "Non rischiare";
        _text[621, 4] = "Kein risiko eingehen";
        _text[621, 5] = "No arriesgarse";
        _text[621, 6] = "Nie ryzykować";
        _text[621, 7] = "Não arriscar";
        _text[621, 8] = "";
        _text[621, 9] = "";

        _text[622, 0] = "You keep the drones on repairs and avoid unnecessary contacts.\n\nThe site remains in the dark behind you.";
        _text[622, 1] = "Вы оставляете дронов на ремонте и избегаете лишних контактов.\n\nУчасток остаётся позади, растворяясь в темноте."; // ничего
        _text[622, 2] = "Vous laissez les drones en réparation et évitez tout contact inutile.\n\nLa zone est abandonnée, disparaissant dans l'obscurité.";
        _text[622, 3] = "Lasci i droni alle riparazioni ed eviti contatti inutili.\n\nIl sito resta alle spalle, dissolvendosi nell'oscurità.";
        _text[622, 4] = "Du lässt die Drohnen reparieren und vermeidest unnötige Kontakte.\n\nDer Bereich bleibt zurück und löst sich in der Dunkelheit auf.";
        _text[622, 5] = "Dejas a los drones con la reparación y evitas contactos innecesarios.\n\nLa zona queda atrás, disolviéndose en la oscuridad.";
        _text[622, 6] = "Zostawiasz drony przy naprawie i unikasz zbędnych kontaktów.\n\nPunkt zostaje za tobą, rozpływając się w ciemności.";
        _text[622, 7] = "Você deixa os drones nos reparos e evita contatos desnecessários.\n\nO local fica para trás, se dissolvendo na escuridão.";
        _text[622, 8] = "";
        _text[622, 9] = "";

        _text[623, 0] = "Sneak in through a service hatch";
        _text[623, 1] = "Тихо пробраться через сервисный люк"; // выбор 2
        _text[623, 2] = "Glissez-vous discrètement par la trappe de service.";
        _text[623, 3] = "Infiltrarsi silenziosamente dal portello di servizio";
        _text[623, 4] = "Leise durch eine Serviceklappe eindringen";
        _text[623, 5] = "Colarse en silencio por la escotilla de servicio";
        _text[623, 6] = "Cicho przedostać się przez luk serwisowy";
        _text[623, 7] = "Entrar silenciosamente pela escotilha de serviço";
        _text[623, 8] = "";
        _text[623, 9] = "";

        _text[624, 0] = "You move along unlit routes and zones outside the cameras' view, then slip into the warehouse through a service hatch.\n\nThe crates are close. Taking them out is the dangerous part.";
        _text[624, 1] = "Вы идёте по неосвещённым проходам и зонам вне обзора камер, затем пробираетесь на склад через сервисный люк.\n\nЯщики рядом. Опаснее всего - вынести их наружу.";
        _text[624, 2] = "Vous traversez des couloirs non éclairés et des zones hors champ, puis vous pénétrez dans l'entrepôt par une trappe de service.\n\nLes caisses sont à proximité. Le plus dangereux est de les sortir.";
        _text[624, 3] = "Ti muovi tra passaggi non illuminati e zone fuori dal campo delle telecamere, poi ti introduci nel magazzino attraverso il portello di servizio.\n\nLe casse sono lì. La parte più rischiosa è portarle fuori.";
        _text[624, 4] = "Du gehst durch unbeleuchtete Gänge und Bereiche außerhalb der Kamerasicht und gelangst dann durch eine Serviceklappe ins Lager.\n\nDie Kisten sind nahe. Am gefährlichsten ist es, sie nach draußen zu bringen.";
        _text[624, 5] = "Avanzas por pasillos sin luz y zonas fuera del campo de las cámaras, y luego te cuelas al almacén por una escotilla de servicio.\n\nLas cajas están cerca. Lo más peligroso es sacarlas al exterior.";
        _text[624, 6] = "Idziesz nieoświetlonymi przejściami i strefami poza zasięgiem kamer, po czym dostajesz się do magazynu przez luk serwisowy.\n\nSkrzynie są blisko. Najbardziej ryzykowne - wynieść je na zewnątrz.";
        _text[624, 7] = "Você segue por corredores sem iluminação e áreas fora do alcance das câmeras, e então entra no depósito pela escotilha de serviço.\n\nAs caixas estão perto. O mais perigoso é levá-las para fora.";
        _text[624, 8] = "";
        _text[624, 9] = "";

        _text[625, 0] = "Cut power first";
        _text[625, 1] = "Сначала обесточить участок"; // выбор 2.1
        _text[625, 2] = "Tout d'abord, déconnectez le courant de la zone.";
        _text[625, 3] = "Prima togliere alimentazione al sito";
        _text[625, 4] = "Zuerst den Bereich stromlos machen";
        _text[625, 5] = "Primero cortar la energía de la zona";
        _text[625, 6] = "Najpierw odciąć zasilanie punktu";
        _text[625, 7] = "Primeiro, desligar a energia do local";
        _text[625, 8] = "";
        _text[625, 9] = "";

        _text[626, 0] = "You try to shut down the generators.\n\nSuccess: lights and sensors go quiet.";
        _text[626, 1] = "Вы пытаетесь заглушить генераторы.\n\nУспех: свет и датчики замолкают."; // выбор 2.1 успех
        _text[626, 2] = "Vous tentez d'arrêter les générateurs.\n\nRéussite: les lumières et les capteurs s'éteignent.";
        _text[626, 3] = "Provi a spegnere i generatori.\n\nSuccesso: luci e sensori si spengono.";
        _text[626, 4] = "Du versuchst, die Generatoren zu drosseln.\n\nErfolg: Licht und Sensoren verstummen.";
        _text[626, 5] = "Intentas apagar los generadores.\n\nÉxito: las luces y los sensores se apagan.";
        _text[626, 6] = "Próbujesz wyłączyć generatory.\n\nSukces: światła i czujniki milkną.";
        _text[626, 7] = "Você tenta desligar os geradores.\n\nSucesso: as luzes e os sensores silenciam.";
        _text[626, 8] = "";
        _text[626, 9] = "";

        _text[627, 0] = "You try to shut down the generators. Failure: the load spikes, and the alarm wakes up.";
        _text[627, 1] = "Вы пытаетесь заглушить генераторы. Провал: скачок нагрузки - и система тревоги просыпается."; // выбор 2.1 провал
        _text[627, 2] = "Vous tentez d'arrêter les générateurs. Échec: une surtension survient et le système d'alarme se déclenche.";
        _text[627, 3] = "Provi a spegnere i generatori. Fallimento: un picco di carico - e il sistema d'allarme si risveglia.";
        _text[627, 4] = "Du versuchst, die Generatoren zu drosseln. Misserfolg: Ein Lastsprung - und das Alarmsystem erwacht.";
        _text[627, 5] = "Intentas apagar los generadores. Fracaso: un pico de carga y el sistema de alarma despierta.";
        _text[627, 6] = "Próbujesz wyłączyć generatory. Porażka: skok obciążenia - i system alarmowy budzi się.";
        _text[627, 7] = "Você tenta desligar os geradores. Falha: um pico de carga - e o sistema de alarme desperta.";
        _text[627, 8] = "";
        _text[627, 9] = "";

        _text[628, 0] = "Fast grab and leave";
        _text[628, 1] = "Быстро схватить и уйти"; // выбор 2.2
        _text[628, 2] = "Prenez et partez rapidement";
        _text[628, 3] = "Afferrare in fretta e andarsene";
        _text[628, 4] = "Schnell greifen und verschwinden";
        _text[628, 5] = "Agarrar rápido y salir";
        _text[628, 6] = "Szybko chwycić i uciec";
        _text[628, 7] = "Pegar rápido e sair";
        _text[628, 8] = "";
        _text[628, 9] = "";

        _text[629, 0] = "Success: you pull the crates out and lift off before any response arrives.\n\nCopper plates are secured.";
        _text[629, 1] = "Успех: вы вытаскиваете ящики и взлетаете до того, как успевает прилететь ответ.\n\nМедные пластины закреплены."; // выбор 2.2 успех + медные пластины
        _text[629, 2] = "Succès: Vous sortez les boîtes et vous vous enfuyez avant même que la réponse n’arrive.\n\nLes plaques de cuivre sont en sécurité.";
        _text[629, 3] = "Successo: trascini via le casse e decolli prima che arrivi una risposta.\n\nLe piastre di rame sono fissate.";
        _text[629, 4] = "Erfolg: Du ziehst die Kisten heraus und startest, bevor eine Antwort eintreffen kann.\n\nKupferplatten gesichert.";
        _text[629, 5] = "Éxito: sacas las cajas y despegas antes de que llegue la respuesta.\n\nLas placas de cobre están aseguradas.";
        _text[629, 6] = "Sukces: wyciągasz skrzynie i startujesz, zanim nadleci odpowiedź.\n\nMiedziane płyty zabezpieczone.";
        _text[629, 7] = "Sucesso: você retira as caixas e decola antes que chegue qualquer resposta.\n\nAs placas de cobre estão fixadas.";
        _text[629, 8] = "";
        _text[629, 9] = "";

        _text[630, 0] = "Failure: you are spotted. Fire hits the hull during takeoff.\n\nYou engage warp and escape, but one core fails.";
        _text[630, 1] = "Провал: вас замечают. При взлёте по корпусу приходятся попадания.\n\nВы включаете варп и уходите, но одно ядро выходит из строя."; // выбор 2.2 провал -1 ядро
        _text[630, 2] = "Échec: Vous êtes repéré. La coque est touchée au décollage.\n\nVous activez le saut spatial et vous échappez, mais un réacteur tombe en panne.";
        _text[630, 3] = "Fallimento: ti notano. Durante il decollo lo scafo viene colpito.\n\nAttivi il warp e ti allontani, ma un nucleo va fuori uso.";
        _text[630, 4] = "Misserfolg: Du wirst entdeckt. Beim Start schlagen Treffer in den Rumpf.\n\nDu zündest den Warp und verschwindest, doch ein Kern fällt aus.";
        _text[630, 5] = "Fracaso: te detectan. Durante el despegue el casco recibe impactos.\n\nActivas el warp y te vas, pero un núcleo queda fuera de servicio.";
        _text[630, 6] = "Porażka: zostajesz zauważony. Podczas startu kadłub dostaje trafienia.\n\nWłączasz warp i odchodzisz, ale jeden rdzeń ulega awarii.";
        _text[630, 7] = "Falha: você é notado. Durante a decolagem, o casco é atingido.\n\nVocê aciona o warp e sai, mas um núcleo falha.";
        _text[630, 8] = "";
        _text[630, 9] = "";

        // 5_MaterialDialogue
        _text[631, 0] = "You pick up an abandoned industrial site on the surface: ruined mixers, cracked silos, and a concrete pad covered with dust.\n\nThe main storage is half-collapsed, but pallets of sealed bags and hardened blocks are still stacked inside.";
        _text[631, 1] = "Вы находите заброшенный промышленный объект на поверхности: разрушенные смесители, треснувшие силосы и бетонную площадку, занесённую пылью.\n\nГлавное хранилище частично обрушено, но внутри всё ещё сложены паллеты с запечатанными мешками и затвердевшими блоками.";
        _text[631, 2] = "Vous découvrez en surface une installation industrielle abandonnée: des mélangeurs hors d’usage, des silos fissurés et une dalle de béton recouverte de poussière.\n\nL’entrepôt principal s’est partiellement effondré, mais des palettes de sacs scellés et de blocs de béton durci sont encore empilées à l’intérieur.";
        _text[631, 3] = "Trovi un impianto industriale abbandonato in superficie: miscelatori distrutti, silos crepati e una piattaforma di cemento coperta di polvere.\n\nIl deposito principale è parzialmente crollato, ma all'interno restano pallet con sacchi sigillati e blocchi induriti.";
        _text[631, 4] = "Du findest an der Oberfläche eine verlassene Industrieanlage: zerstörte Mischer, rissige Silos und eine Betonfläche, die von Staub bedeckt ist.\n\nDas Hauptlager ist teilweise eingestürzt, doch innen liegen noch Paletten mit versiegelten Säcken und ausgehärteten Blöcken.";
        _text[631, 5] = "Encuentras una instalación industrial abandonada en la superficie: mezcladoras destruidas, silos agrietados y una explanada de hormigón cubierta de polvo.\n\nEl almacén principal está parcialmente derrumbado, pero dentro aún hay palés con sacos sellados y bloques endurecidos.";
        _text[631, 6] = "Znajdujesz na powierzchni opuszczony obiekt przemysłowy: zniszczone mieszalniki, popękane silosy i betonowy plac zasypany pyłem.\n\nGłówny magazyn częściowo się zawalił, ale w środku wciąż stoją palety z zapieczętowanymi workami i stwardniałymi blokami.";
        _text[631, 7] = "Você encontra uma instalação industrial abandonada na superfície: misturadores destruídos, silos rachados e uma plataforma de concreto coberta de poeira.\n\nO armazém principal desabou parcialmente, mas lá dentro ainda há paletes com sacos lacrados e blocos endurecidos.";
        _text[631, 8] = "";
        _text[631, 9] = "";

        _text[632, 0] = "Take concrete from the outer stacks";
        _text[632, 1] = "Забрать бетон у входа"; // выбор 1
        _text[632, 2] = "Ramassez le béton à l'entrée";
        _text[632, 3] = "Prendere il cemento all'ingresso";
        _text[632, 4] = "Beton am Eingang bergen";
        _text[632, 5] = "Recoger el hormigón de la entrada";
        _text[632, 6] = "Zabrać beton przy wejściu";
        _text[632, 7] = "Recolher o concreto na entrada";
        _text[632, 8] = "";
        _text[632, 9] = "";

        _text[633, 0] = "You load the nearest concrete onto the drones, trying not to disturb the unstable structure.\n\nSuccess: the loading goes quickly. You retreat before the structure shifts.\n\nConcrete is secured.";
        _text[633, 1] = "Вы грузите ближайший бетон на дронов, стараясь не тревожить нестабильные конструкции.\n\nУспех: погрузка проходит быстро. Вы отходите до того, как конструкция начинает проседать.\n\nБетон закреплён."; // выбор 1 успех + бетон
        _text[633, 2] = "Vous chargez les drones avec le béton le plus proche, en prenant soin de ne pas perturber les structures instables.\n\nSuccès: le chargement est rapide. Vous vous retirez avant que la structure ne commence à s’affaisser.\n\nLe béton est stabilisé.";
        _text[633, 3] = "Carichi il cemento più vicino sui droni, cercando di non disturbare le strutture instabili.\n\nSuccesso: il carico procede rapidamente. Ti allontani prima che la struttura inizi a cedere.\n\nCemento fissato.";
        _text[633, 4] = "Du lädst den nächstliegenden Beton auf die Drohnen, ohne die instabilen Konstruktionen zu stören.\n\nErfolg: Das Verladen geht schnell. Du ziehst dich zurück, bevor die Struktur nachgibt.\n\nBeton gesichert.";
        _text[633, 5] = "Cargas el hormigón más cercano en los drones, procurando no perturbar las estructuras inestables.\n\nÉxito: la carga se realiza rápido. Te retiras antes de que la estructura empiece a ceder.\n\nEl hormigón está asegurado.";
        _text[633, 6] = "Ładujesz najbliższy beton na drony, starając się nie naruszać niestabilnych konstrukcji.\n\nSukces: załadunek przebiega szybko. Wycofujesz się, zanim konstrukcja zacznie osiadać.\n\nBeton zabezpieczony.";
        _text[633, 7] = "Você carrega o concreto mais próximo nos drones, tentando não mexer nas estruturas instáveis.\n\nSucesso: o carregamento é rápido. Você se afasta antes que a estrutura comece a ceder.\n\nO concreto está fixado.";
        _text[633, 8] = "";
        _text[633, 9] = "";

        _text[634, 0] = "You load the nearest concrete onto the drones, trying not to disturb the unstable structure.\n\nFailure: the ground shifts. Debris falls and buries the stacks.\n\nYou retreat empty-handed.";
        _text[634, 1] = "Вы грузите ближайший бетон на дронов, стараясь не тревожить нестабильные конструкции.\n\nПровал: грунт проседает. Обломки осыпаются и заваливают стопки.\n\nВы отходите ни с чем."; // выбор 1 провал ничего
        _text[634, 2] = "Vous chargez les drones avec le béton le plus proche, en prenant soin de ne pas perturber les structures instables.\n\nGroin: le sol s’affaisse. Des débris tombent et recouvrent les piles de béton.\n\nVous battez en retraite les mains vides.";
        _text[634, 3] = "Carichi il cemento più vicino sui droni, cercando di non disturbare le strutture instabili.\n\nFallimento: il terreno cede. I detriti crollano e seppelliscono le pile.\n\nTe ne vai a mani vuote.";
        _text[634, 4] = "Du lädst den nächstliegenden Beton auf die Drohnen, ohne die instabilen Konstruktionen zu stören.\n\nMisserfolg: Der Boden gibt nach. Trümmer stürzen herab und begraben die Stapel.\n\nDu ziehst ohne Beute ab.";
        _text[634, 5] = "Cargas el hormigón más cercano en los drones, procurando no perturbar las estructuras inestables.\n\nFracaso: el terreno cede. Los escombros caen y sepultan las pilas.\n\nTe retiras con las manos vacías.";
        _text[634, 6] = "Ładujesz najbliższy beton na drony, starając się nie naruszać niestabilnych konstrukcji.\n\nPorażka: grunt osiada. Odłamki osypują się i zasypują stosy.\n\nOdchodzisz z niczym.";
        _text[634, 7] = "Você carrega o concreto mais próximo nos drones, tentando não mexer nas estruturas instáveis.\n\nFalha: o solo cede. Os destroços desabam e soterram as pilhas.\n\nVocê se afasta de mãos vazias.";
        _text[634, 8] = "";
        _text[634, 9] = "";

        _text[635, 0] = "Inspect the storage deeper";
        _text[635, 1] = "Пройти глубже в хранилище"; // выбор 2
        _text[635, 2] = "Pénétrez plus profondément dans la chambre forte";
        _text[635, 3] = "Addentrarsi nel deposito";
        _text[635, 4] = "Tiefer ins Lager gehen";
        _text[635, 5] = "Adentrarse en el almacén";
        _text[635, 6] = "Wejść głębiej do magazynu";
        _text[635, 7] = "Ir mais fundo no armazém";
        _text[635, 8] = "";
        _text[635, 9] = "";

        _text[636, 0] = "The deeper sections are unstable. Dust hangs in the air, and the ceiling strains under its own weight.\n\nYou can take concrete faster - or do it carefully.";
        _text[636, 1] = "Глубинные секции нестабильны. В воздухе висит пыль, а перекрытия держатся на пределе.\n\nМожно забрать бетон быстрее - или действовать осторожно.";
        _text[636, 2] = "Les parties profondes sont instables. De la poussière est en suspension dans l'air et les sols sont à leur limite de rupture.\n\nVous pouvez retirer le béton plus rapidement ou procéder avec précaution.";
        _text[636, 3] = "Le sezioni interne sono instabili. La polvere resta sospesa nell'aria e i solai tengono a malapena.\n\nPuoi prendere il cemento più in fretta - oppure agire con cautela.";
        _text[636, 4] = "Die tieferen Bereiche sind instabil. Staub liegt in der Luft, und die Decken halten am Limit.\n\nDu kannst den Beton schneller bergen - oder vorsichtig vorgehen.";
        _text[636, 5] = "Las secciones interiores son inestables. El polvo flota en el aire y los techos aguantan al límite.\n\nPuedes sacar el hormigón más rápido... o actuar con cautela.";
        _text[636, 6] = "Głębsze sekcje są niestabilne. W powietrzu wisi pył, a stropy trzymają się na granicy wytrzymałości.\n\nMożesz zabrać beton szybciej - albo działać ostrożnie.";
        _text[636, 7] = "As seções mais profundas são instáveis. Há poeira suspensa no ar, e as lajes estão no limite.\n\nDá para pegar o concreto mais rápido - ou agir com cautela.";
        _text[636, 8] = "";
        _text[636, 9] = "";

        _text[637, 0] = "Cut supports and pull pallets fast";
        _text[637, 1] = "Срезать опоры и вытащить паллеты быстро"; // выбор 2.1
        _text[637, 2] = "Coupez les supports et retirez rapidement les palettes.";
        _text[637, 3] = "Tagliare i supporti e trascinare fuori i pallet in fretta";
        _text[637, 4] = "Stützen abschneiden und Paletten schnell herausziehen";
        _text[637, 5] = "Cortar los soportes y sacar los palés rápido";
        _text[637, 6] = "Przeciąć podpory i szybko wyciągnąć palety";
        _text[637, 7] = "Cortar os suportes e puxar os paletes rapidamente";
        _text[637, 8] = "";
        _text[637, 9] = "";

        _text[638, 0] = "The drones cut the supports and yank the pallets free.\n\nSuccess: the load is taken out in seconds. Concrete is secured.";
        _text[638, 1] = "Дроны срезают опоры и выдёргивают паллеты.\n\nУспех: груз вынесен за секунды. Бетон закреплён."; // выбор 2.1 успех + бетон
        _text[638, 2] = "Des drones sectionnent les supports et arrachent les palettes.\n\nRéussite: la cargaison est retirée en quelques secondes. Le béton est sécurisé.";
        _text[638, 3] = "I droni tagliano i supporti e strappano fuori i pallet.\n\nSuccesso: il carico viene portato fuori in pochi secondi. Cemento fissato.";
        _text[638, 4] = "Drohnen schneiden die Stützen ab und reißen die Paletten heraus.\n\nErfolg: Die Ladung ist in Sekunden draußen. Beton gesichert.";
        _text[638, 5] = "Los drones cortan los soportes y arrancan los palés.\n\nÉxito: la carga se saca en segundos. El hormigón está asegurado.";
        _text[638, 6] = "Drony przecinają podpory i wyrywają palety.\n\nSukces: ładunek wyniesiony w kilka sekund. Beton zabezpieczony.";
        _text[638, 7] = "Os drones cortam os suportes e arrancam os paletes.\n\nSucesso: a carga é retirada em segundos. O concreto está fixado.";
        _text[638, 8] = "";
        _text[638, 9] = "";

        _text[639, 0] = "The drones cut the supports and yank the pallets free.\n\nFailure: the ceiling collapses. You escape, but one core fails from the impact and overload.";
        _text[639, 1] = "Дроны срезают опоры и выдёргивают паллеты.\n\nПровал: перекрытия рушатся. Вы уходите, но одно ядро выходит из строя от удара и перегрузки."; // выбор 2.1 провал -1 ядро
        _text[639, 2] = "Des drones sectionnent les supports et arrachent les palettes.\n\nÉchec: les planchers s’effondrent. Vous parvenez à vous échapper, mais un noyau cède sous le choc et la surcharge.";
        _text[639, 3] = "I droni tagliano i supporti e strappano fuori i pallet.\n\nFallimento: i solai crollano. Te ne vai, ma un nucleo va fuori uso per l'urto e il sovraccarico.";
        _text[639, 4] = "Drohnen schneiden die Stützen ab und reißen die Paletten heraus.\n\nMisserfolg: Die Decken stürzen ein. Du entkommst, aber ein Kern fällt durch Schlag und Überlastung aus.";
        _text[639, 5] = "Los drones cortan los soportes y arrancan los palés.\n\nFracaso: los techos se vienen abajo. Te retiras, pero un núcleo queda fuera de servicio por el impacto y la sobrecarga.";
        _text[639, 6] = "Drony przecinają podpory i wyrywają palety.\n\nPorażka: stropy się zawalają. Uciekasz, ale jeden rdzeń ulega awarii od uderzenia i przeciążenia.";
        _text[639, 7] = "Os drones cortam os suportes e arrancam os paletes.\n\nFalha: as lajes desabam. Você sai, mas um núcleo falha por impacto e sobrecarga.";
        _text[639, 8] = "";
        _text[639, 9] = "";

        _text[640, 0] = "Move slowly with stabilizers";
        _text[640, 1] = "Действовать медленно со стабилизаторами"; // выбор 2.2
        _text[640, 2] = "Allez-y doucement avec les stabilisateurs";
        _text[640, 3] = "Agire lentamente con gli stabilizzatori";
        _text[640, 4] = "Langsam mit Stabilisatoren vorgehen";
        _text[640, 5] = "Actuar despacio con estabilizadores";
        _text[640, 6] = "Działać powoli ze stabilizatorami";
        _text[640, 7] = "Agir lentamente com estabilizadores";
        _text[640, 8] = "";
        _text[640, 9] = "";

        _text[641, 0] = "You deploy stabilizers and guide the drones through narrow passages.\n\nSuccess: the storage holds.\n\nConcrete is secured.";
        _text[641, 1] = "Вы ставите стабилизаторы и ведёте дронов по узким проходам.\n\nУспех: хранилище выдерживает.\n\nБетон закреплён."; // выбор 2.2 успех + бетон
        _text[641, 2] = "Vous installez les stabilisateurs et guidez les drones dans des passages étroits.\n\nSuccès: l’entrepôt tient bon.Le béton est sécurisé.";
        _text[641, 3] = "Posizioni gli stabilizzatori e guidi i droni attraverso passaggi stretti.\n\nSuccesso: il deposito regge.\n\nCemento fissato.";
        _text[641, 4] = "Du stellst Stabilisatoren auf und führst die Drohnen durch enge Passagen.\n\nErfolg: Das Lager hält.\n\nBeton gesichert.";
        _text[641, 5] = "Colocas estabilizadores y guías a los drones por pasillos estrechos.\n\nÉxito: el almacén resiste.\n\nEl hormigón está asegurado.";
        _text[641, 6] = "Ustawiasz stabilizatory i prowadzisz drony wąskimi przejściami.\n\nSukces: magazyn wytrzymuje.\n\nBeton zabezpieczony.";
        _text[641, 7] = "Você instala estabilizadores e guia os drones por passagens estreitas.\n\nSucesso: o armazém aguenta.\n\nO concreto está fixado.";
        _text[641, 8] = "";
        _text[641, 9] = "";

        _text[642, 0] = "You deploy stabilizers and guide the drones through narrow passages.\n\nFailure: a hidden crack opens under load. The pallets fall, and you leave empty-handed.";
        _text[642, 1] = "Вы ставите стабилизаторы и ведёте дронов по узким проходам.\n\nПровал: скрытая трещина раскрывается под нагрузкой. Паллеты срываются вниз, и вы уходите ни с чем."; // выбор 2.2 провал
        _text[642, 2] = "Vous déployez les stabilisateurs et guidez les drones à travers d'étroits passages.\n\nUn gouffre se forme: une fissure invisible s'ouvre sous la charge. Les palettes s'effondrent et vous repartez bredouille.";
        _text[642, 3] = "Posizioni gli stabilizzatori e guidi i droni attraverso passaggi stretti.\n\nFallimento: una crepa nascosta si apre sotto il carico. I pallet precipitano, e te ne vai a mani vuote.";
        _text[642, 4] = "Du stellst Stabilisatoren auf und führst die Drohnen durch enge Passagen.\n\nMisserfolg: Ein versteckter Riss öffnet sich unter der Last. Die Paletten stürzen hinab, und du gehst leer aus.";
        _text[642, 5] = "Colocas estabilizadores y guías a los drones por pasillos estrechos.\n\nFracaso: una grieta oculta se abre bajo la carga. Los palés se desploman y te vas con las manos vacías.";
        _text[642, 6] = "Ustawiasz stabilizatory i prowadzisz drony wąskimi przejściami.\n\nPorażka: ukryta szczelina otwiera się pod obciążeniem. Palety osuwają się w dół, a ty odchodzisz z niczym.";
        _text[642, 7] = "Você instala estabilizadores e guia os drones por passagens estreitas.\n\nFalha: uma fissura oculta se abre sob a carga. Os paletes despencam, e você sai de mãos vazias.";
        _text[642, 8] = "";
        _text[642, 9] = "";

        // 6_MaterialDialogue
        _text[643, 0] = "A coolant leak forces you to descend and land on the nearest planet for repairs.\n\nThe landing zone is cold and dark. While the drones inspect the damage, the sensors pick up a steady thermal source nearby.\n\nIt is a geothermal vent. Next to it stands an old condenser unit and a line of pipes going into the rock.\n\nThe system is still producing steam, but the pressure is unstable.";
        _text[643, 1] = "Утечка охлаждения вынуждает вас снизиться и сесть на ближайшую планету для ремонта.\n\nМесто посадки холодное и тёмное. Пока дроны осматривают повреждения, сенсоры фиксируют ровный тепловой источник неподалёку.\n\nЭто геотермальный разлом. Рядом стоит старый конденсаторный блок и линия труб, уходящая в породу.\n\nСистема всё ещё даёт пар, но давление нестабильно.";
        _text[643, 2] = "Une fuite du système de refroidissement vous oblige à atterrir sur la planète la plus proche pour effectuer les réparations.\n\nLe site d'atterrissage est froid et sombre. Pendant que les drones inspectent les dégâts, des capteurs détectent une source de chaleur constante à proximité.\n\nIl s'agit d'une faille géothermique. À proximité se trouvent un ancien condenseur et une conduite s'enfonçant dans la roche.\n\nLe système produit encore de la vapeur, mais la pression est instable.";
        _text[643, 3] = "Una perdita nel circuito di raffreddamento ti costringe a scendere e atterrare sul pianeta più vicino per riparare.\n\nIl luogo di atterraggio è freddo e buio. Mentre i droni ispezionano i danni, i sensori rilevano una fonte di calore stabile nelle vicinanze.\n\nÈ una frattura geotermica. Accanto ci sono un vecchio blocco condensatore e una linea di tubi che sprofonda nella roccia.\n\nIl sistema produce ancora vapore, ma la pressione è instabile.";
        _text[643, 4] = "Ein Kühlmittelleck zwingt dich, für Reparaturen zu sinken und auf dem nächsten Planeten zu landen.\n\nDer Landeplatz ist kalt und dunkel. Während die Drohnen den Schaden prüfen, registrieren die Sensoren eine gleichmäßige Wärmequelle in der Nähe.\n\nEs ist ein geothermischer Riss. Daneben steht ein alter Kondensatorblock und eine Rohrleitung, die ins Gestein führt.\n\nDas System liefert noch Dampf, aber der Druck ist instabil.";
        _text[643, 5] = "Una fuga en el sistema de refrigeración te obliga a descender y aterrizar en el planeta más cercano para reparar.\n\nEl lugar de aterrizaje es frío y oscuro. Mientras los drones inspeccionan los daños, los sensores detectan una fuente térmica constante cerca.\n\nEs una fisura geotérmica. Al lado hay un viejo bloque de condensadores y una línea de tuberías que se adentra en la roca.\n\nEl sistema aún genera vapor, pero la presión es inestable.";
        _text[643, 6] = "Wyciek chłodziwa zmusza cię do zniżenia lotu i lądowania na najbliższej planecie w celu naprawy.\n\nMiejsce lądowania jest zimne i ciemne. Gdy drony sprawdzają uszkodzenia, sensory wykrywają w pobliżu równomierne źródło ciepła.\n\nTo geotermalne pęknięcie. Obok stoi stary blok kondensatorów i linia rur wchodząca w skałę.\n\nSystem wciąż daje parę, ale ciśnienie jest niestabilne.";
        _text[643, 7] = "Um vazamento no sistema de refrigeração obriga você a descer e pousar no planeta mais próximo para reparos.\n\nO local de pouso é frio e escuro. Enquanto os drones inspecionam os danos, os sensores registram uma fonte térmica estável ali perto.\n\nÉ uma fenda geotérmica. Ao lado há um antigo bloco condensador e uma linha de tubulações que entra na rocha.\n\nO sistema ainda produz vapor, mas a pressão é instável.";
        _text[643, 8] = "";
        _text[643, 9] = "";

        _text[644, 0] = "Connect to the vent carefully";
        _text[644, 1] = "Подключиться осторожно"; // выбор 1
        _text[644, 2] = "Connectez soigneusement";
        _text[644, 3] = "Collegarsi con cautela";
        _text[644, 4] = "Vorsichtig anschließen";
        _text[644, 5] = "Conectarse con cuidado";
        _text[644, 6] = "Podłączyć się ostrożnie";
        _text[644, 7] = "Conectar com cuidado";
        _text[644, 8] = "";
        _text[644, 9] = "";

        _text[645, 0] = "You align the collectors and connect flexible lines, slowly opening the valves.\n\nSuccess: the pressure stabilizes. You fill the tanks with steam and seal the system.";
        _text[645, 1] = "Вы выравниваете коллекторы и подключаете гибкие магистрали, медленно открывая клапаны.\n\nУспех: давление стабилизируется. Вы заполняете баки паром и герметизируете систему."; // выбор 1 успех + пар
        _text[645, 2] = "Vous alignez les collecteurs et raccordez les flexibles, en ouvrant lentement les vannes.\n\nSuccès: la pression se stabilise. Vous remplissez les réservoirs de vapeur et fermez le système.";
        _text[645, 3] = "Allinei i collettori e colleghi le linee flessibili, aprendo lentamente le valvole.\n\nSuccesso: la pressione si stabilizza. Riempi i serbatoi di vapore e sigilli il sistema.";
        _text[645, 4] = "Du richtest die Kollektoren aus und verbindest flexible Leitungen, während du die Ventile langsam öffnest.\n\nErfolg: Der Druck stabilisiert sich. Du füllst die Tanks mit Dampf und dichtest das System ab.";
        _text[645, 5] = "Alineas los colectores y conectas líneas flexibles, abriendo las válvulas lentamente.\n\nÉxito: la presión se estabiliza. Llenas los tanques de vapor y sellas el sistema.";
        _text[645, 6] = "Wyrównujesz kolektory i podłączasz elastyczne przewody, powoli otwierając zawory.\n\nSukces: ciśnienie się stabilizuje. Napełniasz zbiorniki parą i uszczelniasz system.";
        _text[645, 7] = "Você alinha os coletores e conecta linhas flexíveis, abrindo as válvulas lentamente.\n\nSucesso: a pressão se estabiliza. Você enche os tanques com vapor e sela o sistema.";
        _text[645, 8] = "";
        _text[645, 9] = "";

        _text[646, 0] = "You align the collectors and connect flexible lines, slowly opening the valves.\n\nFailure: the pressure spikes. A hot discharge hits the equipment, and you have to break contact.";
        _text[646, 1] = "Вы выравниваете коллекторы и подключаете гибкие магистрали, медленно открывая клапаны.\n\nПровал: давление срывается вверх. Горячий выброс бьёт по оборудованию, и вам приходится разорвать подключение."; // выбор 1 провал ничего
        _text[646, 2] = "Vous alignez les collecteurs et raccordez les flexibles, en ouvrant lentement les vannes.\n\nUne chute de pression: la pression monte brusquement. Un jet d'eau chaude atteint l'équipement et vous devez débrancher le raccord.";
        _text[646, 3] = "Allinei i collettori e colleghi le linee flessibili, aprendo lentamente le valvole.\n\nFallimento: la pressione schizza verso l'alto. Un getto bollente colpisce l'attrezzatura e sei costretto a scollegarti.";
        _text[646, 4] = "Du richtest die Kollektoren aus und verbindest flexible Leitungen, während du die Ventile langsam öffnest.\n\nMisserfolg: Der Druck schießt nach oben. Ein heißer Ausstoß trifft das Equipment, und du musst die Verbindung kappen.";
        _text[646, 5] = "Alineas los colectores y conectas líneas flexibles, abriendo las válvulas lentamente.\n\nFracaso: la presión se dispara. Una descarga caliente golpea el equipo y te ves obligado a desconectar.";
        _text[646, 6] = "Wyrównujesz kolektory i podłączasz elastyczne przewody, powoli otwierając zawory.\n\nPorażka: ciśnienie gwałtownie rośnie. Gorący wyrzut uderza w sprzęt i musisz przerwać podłączenie.";
        _text[646, 7] = "Você alinha os coletores e conecta linhas flexíveis, abrindo as válvulas lentamente.\n\nFalha: a pressão dispara. Um jato quente atinge o equipamento, e você precisa interromper a conexão.";
        _text[646, 8] = "";
        _text[646, 9] = "";

        _text[647, 0] = "Take steam quickly";
        _text[647, 1] = "Забрать пар быстро"; // выбор 2
        _text[647, 2] = "Obtenez rapidement de la vapeur";
        _text[647, 3] = "Raccogliere il vapore in fretta";
        _text[647, 4] = "Dampf schnell abziehen";
        _text[647, 5] = "Recoger el vapor rápidamente";
        _text[647, 6] = "Szybko zebrać parę";
        _text[647, 7] = "Coletar o vapor rapidamente";
        _text[647, 8] = "";
        _text[647, 9] = "";

        _text[648, 0] = "You open the valves to maximum and force the vent into the tanks.\n\nSuccess: you fill the tanks before the system destabilizes.\n\nSteam is secured.";
        _text[648, 1] = "Вы открываете клапаны на максимум и форсируете подачу в баки.\n\nУспех: вы успеваете заполнить баки до того, как система срывается.\n\nПар закреплён."; // выбор 2 успех + пар
        _text[648, 2] = "Vous ouvrez les vannes au maximum et forcez le flux vers les réservoirs.\n\nSuccès : vous remplissez les réservoirs avant que le système ne s’effondre.L’approvisionnement en vapeur est assuré.";
        _text[648, 3] = "Apri le valvole al massimo e forzi l'afflusso nei serbatoi.\n\nSuccesso: riesci a riempire i serbatoi prima che il sistema ceda.\n\nVapore fissato.";
        _text[648, 4] = "Du öffnest die Ventile voll und forcierst die Zufuhr in die Tanks.\n\nErfolg: Du füllst die Tanks, bevor das System ausbricht.\n\nDampf gesichert.";
        _text[648, 5] = "Abres las válvulas al máximo y fuerzas el flujo hacia los tanques.\n\nÉxito: consigues llenarlos antes de que el sistema se descontrole.\n\nEl vapor está asegurado.";
        _text[648, 6] = "Otwierasz zawory na maksimum i wymuszasz podawanie do zbiorników.\n\nSukces: zdążasz napełnić zbiorniki, zanim system się zerwie.\n\nPara zabezpieczona.";
        _text[648, 7] = "Você abre as válvulas ao máximo e força a alimentação para os tanques.\n\nSucesso: você consegue enchê-los antes que o sistema entre em colapso.\n\nO vapor está fixado.";
        _text[648, 8] = "";
        _text[648, 9] = "";

        _text[649, 0] = "You open the valves to maximum and force the vent into the tanks.\n\nFailure: the line bursts under pressure. Shrapnel and heat damage the equipment, and one core fails.";
        _text[649, 1] = "Вы открываете клапаны на максимум и форсируете подачу в баки.\n\nПровал: магистраль рвёт давлением. Осколки и жар повреждают оборудование, и одно ядро выходит из строя."; // выбор 2 провал -1 ядро
        _text[649, 2] = "Vous ouvrez les vannes au maximum et forcez le flux vers les réservoirs.\n\nDéfaillance : la conduite principale éclate sous la pression. Les éclats et la chaleur endommagent l’équipement, et un élément central est hors service.";
        _text[649, 3] = "Apri le valvole al massimo e forzi l'afflusso nei serbatoi.\n\nFallimento: la condotta si squarcia per la pressione. Schegge e calore danneggiano l'attrezzatura, e un nucleo va fuori uso.";
        _text[649, 4] = "Du öffnest die Ventile voll und forcierst die Zufuhr in die Tanks.\n\nMisserfolg: Die Leitung reißt unter dem Druck. Splitter und Hitze beschädigen das Equipment, und ein Kern fällt aus.";
        _text[649, 5] = "Abres las válvulas al máximo y fuerzas el flujo hacia los tanques.\n\nFracaso: la línea revienta por la presión. Fragmentos y calor dañan el equipo y un núcleo queda fuera de servicio.";
        _text[649, 6] = "Otwierasz zawory na maksimum i wymuszasz podawanie do zbiorników.\n\nPorażka: przewód pęka pod ciśnieniem. Odłamki i żar uszkadzają sprzęt, a jeden rdzeń ulega awarii.";
        _text[649, 7] = "Você abre as válvulas ao máximo e força a alimentação para os tanques.\n\nFalha: a linha estoura com a pressão. Estilhaços e calor danificam o equipamento, e um núcleo falha.";
        _text[649, 8] = "";
        _text[649, 9] = "";

        // 0_ComponentDialogue
        _text[650, 0] = "A malfunction in the drive system forces you to make a short stop.\n\nDuring diagnostics, the AI detects an abandoned machine shop nearby. The entrance is blocked, but inside the scanners register intact mechanical lines.\n\nOn the conveyor are crates with gear wheels. Next to them are boxes of iron ingots.\n\nThe workshop looks automated, but its power cycles are unstable.";
        _text[650, 1] = "Сбой в приводной системе вынуждает вас сделать короткую остановку.\n\nВо время диагностики ИИ фиксирует неподалёку заброшенный механический цех. Вход завален, но сканеры видят внутри целые производственные линии.\n\nНа конвейере стоят ящики с шестернями. Рядом - коробки с железными слитками.\n\nЦех выглядит автоматизированным, но питание работает нестабильно.";
        _text[650, 2] = "Une panne du système d'entraînement vous oblige à un bref arrêt.\n\nLors du diagnostic, l'IA détecte un atelier d'usinage abandonné à proximité. L'entrée est bloquée, mais les scanners révèlent des chaînes de production complètes à l'intérieur.\n\nDes caisses d'engrenages sont empilées sur un convoyeur. À côté se trouvent des boîtes de lingots de fer.\n\nL'atelier semble automatisé, mais l'alimentation électrique est instable.";
        _text[650, 3] = "Un guasto nel sistema di trasmissione ti costringe a una breve sosta.\n\nDurante la diagnostica l'IA rileva nelle vicinanze un'officina meccanica abbandonata. L'ingresso è ostruito, ma gli scanner vedono linee di produzione ancora integre all'interno.\n\nSul nastro ci sono casse di ingranaggi. Accanto - scatole di lingotti di ferro.\n\nL'officina sembra automatizzata, ma l'alimentazione è instabile.";
        _text[650, 4] = "Ein Defekt im Antriebssystem zwingt dich zu einem kurzen Halt.\n\nWährend der Diagnose entdeckt die KI in der Nähe eine verlassene mechanische Werkhalle. Der Eingang ist verschüttet, doch die Scanner sehen drinnen intakte Produktionslinien.\n\nAuf dem Förderband stehen Kisten mit Zahnrädern. Daneben - Kartons mit Eisenbarren.\n\nDie Halle wirkt automatisiert, aber die Stromversorgung ist instabil.";
        _text[650, 5] = "Un fallo en el sistema de transmisión te obliga a hacer una breve parada.\n\nDurante el diagnóstico, la IA detecta cerca un taller mecánico abandonado. La entrada está bloqueada, pero los escáneres ven dentro líneas de producción intactas.\n\nEn la cinta hay cajas con engranajes. Al lado, cajas con lingotes de hierro.\n\nEl taller parece automatizado, pero la energía funciona de forma inestable.";
        _text[650, 6] = "Awaria układu napędowego zmusza cię do krótkiego postoju.\n\nPodczas diagnostyki SI wykrywa w pobliżu opuszczony warsztat mechaniczny. Wejście jest zasypane, ale skanery widzą w środku całe linie produkcyjne.\n\nNa taśmie stoją skrzynie z kołami zębatymi. Obok - pudła z żelaznymi sztabami.\n\nWarsztat wygląda na zautomatyzowany, ale zasilanie działa niestabilnie.";
        _text[650, 7] = "Uma falha no sistema de acionamento obriga você a fazer uma parada curta.\n\nDurante o diagnóstico, a IA detecta uma oficina mecânica abandonada ali perto. A entrada está bloqueada, mas os scanners veem linhas de produção intactas lá dentro.\n\nHá caixas com engrenagens na esteira. Ao lado - caixas com barras de ferro.\n\nA oficina parece automatizada, mas a energia é instável.";
        _text[650, 8] = "";
        _text[650, 9] = "";

        _text[651, 0] = "Take ready crates from the conveyor";
        _text[651, 1] = "Забрать готовые ящики с конвейера"; // выбор 1
        _text[651, 2] = "Récupérer les cartons finis sur le convoyeur";
        _text[651, 3] = "Prendere le casse pronte dal nastro";
        _text[651, 4] = "Fertige Kisten vom Förderband bergen";
        _text[651, 5] = "Llevarse las cajas listas de la cinta";
        _text[651, 6] = "Zabrać gotowe skrzynie z taśmy";
        _text[651, 7] = "Pegar as caixas prontas da esteira";
        _text[651, 8] = "";
        _text[651, 9] = "";

        _text[652, 0] = "You try to pull the crates off the conveyor and load them onto the drones.\n\nSuccess: the mechanism stays quiet. You remove the crates and leave the workshop.\n\nGear wheels are secured.";
        _text[652, 1] = "Вы пытаетесь снять ящики с конвейера и погрузить их на дронов.\n\nУспех: механизм не подаёт признаков активности. Вы снимаете ящики и покидаете цех.\n\nШестерни закреплены."; // выбор 1 успех + GearWheel
        _text[652, 2] = "Vous tentez de retirer des caisses du convoyeur et de les charger sur les drones.\n\nSuccès: le mécanisme ne présente aucun signe d’activité. Vous retirez les caisses et quittez l’atelier.\n\nLes engrenages sont bloqués.";
        _text[652, 3] = "Provi a togliere le casse dal nastro e a caricarle sui droni.\n\nSuccesso: il meccanismo non mostra segni di attività. Prelevi le casse e lasci l'officina.\n\nIngranaggi fissati.";
        _text[652, 4] = "Du versuchst, die Kisten vom Förderband zu nehmen und auf Drohnen zu verladen.\n\nErfolg: Der Mechanismus zeigt keine Aktivität. Du nimmst die Kisten und verlässt die Halle.\n\nZahnräder gesichert.";
        _text[652, 5] = "Intentas retirar las cajas de la cinta y cargarlas en los drones.\n\nÉxito: el mecanismo no muestra actividad. Retiras las cajas y sales del taller.\n\nLos engranajes están asegurados.";
        _text[652, 6] = "Próbujesz zdjąć skrzynie z taśmy i załadować je na drony.\n\nSukces: mechanizm nie wykazuje aktywności. Zdejmujesz skrzynie i opuszczasz warsztat.\n\nKoła zębate zabezpieczone.";
        _text[652, 7] = "Você tenta retirar as caixas da esteira e carregá-las nos drones.\n\nSucesso: o mecanismo não dá sinais de atividade. Você retira as caixas e deixa a oficina.\n\nAs engrenagens estão fixadas.";
        _text[652, 8] = "";
        _text[652, 9] = "";

        _text[653, 0] = "You try to pull the crates off the conveyor and load them onto the drones.\n\nFailure: the conveyor wakes up and drags the crates back. The drones get caught, and you lose one core while breaking contact.";
        _text[653, 1] = "Вы пытаетесь снять ящики с конвейера и погрузить их на дронов.\n\nПровал: конвейер приходит в движение и затягивает ящики обратно. Дроны попадают в захват, и при разрыве контакта одно ядро выходит из строя."; // выбор 1 провал -1 ядро
        _text[653, 2] = "Vous tentez de retirer des caisses d'un convoyeur et de les charger sur des drones.\n\nProblème: le convoyeur se remet en marche et tire les caisses en arrière. Les drones sont pris dans le système de préhension et, lorsque le contact est rompu, un noyau est détruit.";
        _text[653, 3] = "Provi a togliere le casse dal nastro e a caricarle sui droni.\n\nFallimento: il nastro si mette in moto e risucchia le casse indietro. I droni restano intrappolati e, nel rompere il contatto, un nucleo va fuori uso.";
        _text[653, 4] = "Du versuchst, die Kisten vom Förderband zu nehmen und auf Drohnen zu verladen.\n\nMisserfolg: Das Band setzt sich in Bewegung und zieht die Kisten zurück. Die Drohnen geraten in den Greifer, und beim Abreißen des Kontakts fällt ein Kern aus.";
        _text[653, 5] = "Intentas retirar las cajas de la cinta y cargarlas en los drones.\n\nFracaso: la cinta se pone en marcha y arrastra las cajas de vuelta. Los drones quedan atrapados y, al romper el contacto, un núcleo queda fuera de servicio.";
        _text[653, 6] = "Próbujesz zdjąć skrzynie z taśmy i załadować je na drony.\n\nPorażka: przenośnik rusza i wciąga skrzynie z powrotem. Drony wpadają w zacisk, a przy zrywaniu kontaktu jeden rdzeń ulega awarii.";
        _text[653, 7] = "Você tenta retirar as caixas da esteira e carregá-las nos drones.\n\nFalha: a esteira entra em movimento e puxa as caixas de volta. Os drones ficam presos, e ao romper o contato um núcleo falha.";
        _text[653, 8] = "";
        _text[653, 9] = "";

        _text[654, 0] = "Start the press line";
        _text[654, 1] = "Запустить линию прессов"; // выбор 2
        _text[654, 2] = "Lancer une ligne de presse";
        _text[654, 3] = "Avviare la linea di presse";
        _text[654, 4] = "Die Pressenlinie starten";
        _text[654, 5] = "Poner en marcha la línea de prensas";
        _text[654, 6] = "Uruchomić linię pras";
        _text[654, 7] = "Iniciar a linha de prensas";
        _text[654, 8] = "";
        _text[654, 9] = "";

        _text[655, 0] = "You feed the line with iron ingots and start the presses.\n\nSuccess: the machines stamp gear wheels one by one. You load the finished parts and stop the line.\n\nGear wheels are secured.";
        _text[655, 1] = "Вы подаёте на линию железные слитки и запускаете прессы.\n\nУспех: станки штампуют шестерни одну за другой. Вы загружаете готовые детали и останавливаете линию.\n\nШестерни закреплены."; // выбор 2 успех + GearWheel - IronIngot
        _text[655, 2] = "Vous alimentez la chaîne avec des lingots de fer et démarrez les presses.\n\nSuccès: les machines produisent les engrenages les uns après les autres. Vous chargez les pièces finies et arrêtez la chaîne.Les engrenages sont fixés.";
        _text[655, 3] = "Immetti lingotti di ferro nella linea e avvii le presse.\n\nSuccesso: le macchine stampano ingranaggi uno dopo l'altro. Carichi i pezzi finiti e fermi la linea.\n\nIngranaggi fissati.";
        _text[655, 4] = "Du führst Eisenbarren in die Linie und startest die Pressen.\n\nErfolg: Die Maschinen stanzen ein Zahnrad nach dem anderen. Du lädst die fertigen Teile und stoppst die Linie.\n\nZahnräder gesichert.";
        _text[655, 5] = "Alimentas la línea con lingotes de hierro y arrancas las prensas.\n\nÉxito: las máquinas estampan engranajes uno tras otro. Cargas las piezas terminadas y detienes la línea.\n\nLos engranajes están asegurados.";
        _text[655, 6] = "Podajesz na linię żelazne sztaby i uruchamiasz prasy.\n\nSukces: maszyny tłoczą koła zębate jedno po drugim. Ładujesz gotowe części i zatrzymujesz linię.\n\nKoła zębate zabezpieczone.";
        _text[655, 7] = "Você alimenta a linha com barras de ferro e inicia as prensas.\n\nSucesso: as máquinas estampam engrenagens uma após a outra. Você carrega as peças prontas e para a linha.\n\nAs engrenagens estão fixadas.";
        _text[655, 8] = "";
        _text[655, 9] = "";

        _text[656, 0] = "You feed the line with iron ingots and start the presses.\n\nFailure: the power spikes. The press jams, sparks hit the control unit, and the system shuts down.\n\nYou retreat without gear wheels.";
        _text[656, 1] = "Вы подаёте на линию железные слитки и запускаете прессы.\n\nПровал: питание срывается. Пресс клинит, искры попадают в блок управления, и система отключается.\n\nВы отходите без шестерён."; // выбор 2 провал ничего
        _text[656, 2] = "Vous alimentez la chaîne avec des lingots de fer et démarrez les presses.\n\nPanne: coupure de courant. La presse se bloque, des étincelles pénètrent dans l’unité de commande et le système s’arrête.\n\nVous repartez sans engrenages.";
        _text[656, 3] = "Immetti lingotti di ferro nella linea e avvii le presse.\n\nFallimento: l'alimentazione salta. Una pressa si blocca, le scintille raggiungono l'unità di controllo e il sistema si spegne.\n\nTi allontani senza ingranaggi.";
        _text[656, 4] = "Du führst Eisenbarren in die Linie und startest die Pressen.\n\nMisserfolg: Die Stromversorgung bricht weg. Eine Presse verklemmt, Funken treffen den Steuerblock, und das System schaltet ab.\n\nDu ziehst ohne Zahnräder ab.";
        _text[656, 5] = "Alimentas la línea con lingotes de hierro y arrancas las prensas.\n\nFracaso: la energía falla. La prensa se atasca, las chispas alcanzan el bloque de control y el sistema se apaga.\n\nTe retiras sin engranajes.";
        _text[656, 6] = "Podajesz na linię żelazne sztaby i uruchamiasz prasy.\n\nPorażka: zasilanie się rwie. Prasa się zacina, iskry trafiają w blok sterowania i system się wyłącza.\n\nWycofujesz się bez kół zębatych.";
        _text[656, 7] = "Você alimenta a linha com barras de ferro e inicia as prensas.\n\nFalha: a energia falha. A prensa trava, faíscas atingem o bloco de controle, e o sistema desliga.\n\nVocê se afasta sem engrenagens.";
        _text[656, 8] = "";
        _text[656, 9] = "";

        // 1_ComponentDialogue
        _text[657, 0] = "During a routine scan, you detect a weak beacon from a drifting service capsule.\n\nIts корпус is scorched, the docking clamps are bent, but the internal containers are intact.\n\nThe markings match old ship electronics: circuit blocks, sealed and protected from vacuum.\n\nThe capsule is slowly rotating. Any грубый захват can tear it apart.";
        _text[657, 1] = "Во время планового сканирования вы фиксируете слабый сигнал от дрейфующей сервисной капсулы.\n\nЕё корпус обожжён, стыковочные захваты погнуты, но внутренние контейнеры целы.\n\nМаркировка соответствует старой корабельной электронике: блоки электронных схем, запечатанные и защищённые от вакуума.\n\nКапсула медленно вращается. Любой грубый захват может разорвать её.";
        _text[657, 2] = "Lors d'une inspection de routine, vous détectez un faible signal provenant d'une capsule de service à la dérive.\n\nSa coque est calcinée, ses pinces d'amarrage sont tordues, mais ses conteneurs internes sont intacts.\n\nLes marquages ​​correspondent à ceux des anciens équipements électroniques de bord: des blocs de circuits électroniques, scellés et protégés du vide.\n\nLa capsule tourne lentement. Toute manipulation brutale pourrait la détruire.";
        _text[657, 3] = "Durante una scansione di routine rilevi un debole segnale da una capsula di servizio alla deriva.\n\nLo scafo è bruciato, i ganci di attracco sono piegati, ma i contenitori interni sono intatti.\n\nLa marcatura corrisponde a vecchia elettronica navale: blocchi di circuiti elettronici, sigillati e protetti dal vuoto.\n\nLa capsula ruota lentamente. Qualsiasi presa brusca potrebbe lacerarla.";
        _text[657, 4] = "Bei einem planmäßigen Scan registrierst du ein schwaches Signal von einer treibenden Servicekapsel.\n\nIhr Rumpf ist verbrannt, die Andockgreifer sind verbogen, aber die inneren Container sind intakt.\n\nDie Markierung passt zu alter Schiffselektronik: Platinenblöcke, versiegelt und gegen Vakuum geschützt.\n\nDie Kapsel rotiert langsam. Jeder grobe Zugriff könnte sie zerreißen.";
        _text[657, 5] = "Durante un escaneo rutinario detectas una señal débil procedente de una cápsula de servicio a la deriva.\n\nSu casco está chamuscado, los enganches de acoplamiento doblados, pero los contenedores internos están intactos.\n\nEl marcaje corresponde a electrónica naval antigua: módulos de circuitos electrónicos, sellados y protegidos del vacío.\n\nLa cápsula gira lentamente. Cualquier agarre brusco podría desgarrarla.";
        _text[657, 6] = "Podczas rutynowego skanowania rejestrujesz słaby sygnał z dryfującej kapsuły serwisowej.\n\nJej kadłub jest osmalony, zaczepy dokujące wygięte, ale wewnętrzne kontenery są całe.\n\nOznaczenia odpowiadają starej elektronice okrętowej: bloki układów elektronicznych, zapieczętowane i chronione przed próżnią.\n\nKapsuła powoli się obraca. Każdy brutalny chwyt może ją rozerwać.";
        _text[657, 7] = "Durante uma varredura de rotina, você detecta um sinal fraco vindo de uma cápsula de serviço à deriva.\n\nO casco está chamuscado, as garras de acoplagem estão tortas, mas os contêineres internos permanecem intactos.\n\nA marcação corresponde à eletrônica naval antiga: blocos de circuitos eletrônicos, selados e protegidos do vácuo.\n\nA cápsula gira lentamente. Qualquer captura brusca pode rasgá-la.";
        _text[657, 8] = "";
        _text[657, 9] = "";

        _text[658, 0] = "Cut the container out from a distance";
        _text[658, 1] = "Срезать контейнер с дистанции"; // выбор 1
        _text[658, 2] = "Découpez le conteneur à distance.";
        _text[658, 3] = "Tagliare via il contenitore a distanza";
        _text[658, 4] = "Den Container aus der Distanz abtrennen";
        _text[658, 5] = "Cortar el contenedor a distancia";
        _text[658, 6] = "Odciąć kontener z dystansu";
        _text[658, 7] = "Cortar o contêiner à distância";
        _text[658, 8] = "";
        _text[658, 9] = "";

        _text[659, 0] = "You use a cutter beam and try to separate the container without touching the capsule.\n\nSuccess: the container comes free and is caught by the drones.\n\nElectronic circuits are secured.";
        _text[659, 1] = "Вы используете режущий луч и пытаетесь отделить контейнер, не касаясь капсулы.\n\nУспех: контейнер отделяется и перехватывается дронами.\n\nЭлектронные схемы закреплены."; // выбор 1 успех + ElectronicCircuit
        _text[659, 2] = "Vous utilisez le faisceau de découpe et tentez de détacher le conteneur sans toucher la capsule.\n\nSuccès: Le conteneur se détache et est intercepté par les drones.\n\nLes circuits électroniques sont sécurisés.";
        _text[659, 3] = "Usi un raggio di taglio e provi a separare il contenitore senza toccare la capsula.\n\nSuccesso: il contenitore si stacca e viene intercettato dai droni.\n\nCircuiti elettronici fissati.";
        _text[659, 4] = "Du nutzt einen Schneidstrahl und versuchst, den Container zu lösen, ohne die Kapsel zu berühren.\n\nErfolg: Der Container löst sich und wird von Drohnen abgefangen.\n\nElektronische Schaltungen gesichert.";
        _text[659, 5] = "Usas un rayo de corte e intentas separar el contenedor sin tocar la cápsula.\n\nÉxito: el contenedor se desprende y los drones lo capturan.\n\nLos circuitos electrónicos están asegurados.";
        _text[659, 6] = "Używasz wiązki tnącej i próbujesz oddzielić kontener, nie dotykając kapsuły.\n\nSukces: kontener odłącza się i zostaje przechwycony przez drony.\n\nUkłady elektroniczne zabezpieczone.";
        _text[659, 7] = "Você usa um feixe de corte e tenta separar o contêiner sem tocar na cápsula.\n\nSucesso: o contêiner se solta e é capturado pelos drones.\n\nOs circuitos eletrônicos estão fixados.";
        _text[659, 8] = "";
        _text[659, 9] = "";

        _text[660, 0] = "You use a cutter beam and try to separate the container without touching the capsule.\n\nFailure: the cut hits a pressure line. A jet удар sends fragments into your hull.\n\nOne core fails, and you break contact.";
        _text[660, 1] = "Вы используете режущий луч и пытаетесь отделить контейнер, не касаясь капсулы.\n\nПровал: рез задевает магистраль. Реактивный выброс швыряет осколки в корпус.\n\nОдно ядро выходит из строя, и вы разрываете контакт."; // выбор 1 провал -1 ядро
        _text[660, 2] = "Vous utilisez le faisceau de découpe et tentez de séparer le conteneur sans toucher la capsule.\n\nÉchec: la découpe atteint une conduite principale. La décharge réactive projette des fragments dans la coque.Un des cœurs cède et le contact est rompu.";
        _text[660, 3] = "Usi un raggio di taglio e provi a separare il contenitore senza toccare la capsula.\n\nFallimento: il taglio colpisce una linea principale. Un getto reattivo scaglia schegge contro lo scafo.\n\nUn nucleo va fuori uso, e interrompi il contatto.";
        _text[660, 4] = "Du nutzt einen Schneidstrahl und versuchst, den Container zu lösen, ohne die Kapsel zu berühren.\n\nMisserfolg: Der Schnitt trifft eine Leitung. Ein Rückstoß schleudert Splitter in den Rumpf.\n\nEin Kern fällt aus, und du brichst den Kontakt ab.";
        _text[660, 5] = "Usas un rayo de corte e intentas separar el contenedor sin tocar la cápsula.\n\nFracaso: el corte alcanza una línea principal. Una eyección reactiva lanza fragmentos contra el casco.\n\nUn núcleo queda fuera de servicio y rompes el contacto.";
        _text[660, 6] = "Używasz wiązki tnącej i próbujesz oddzielić kontener, nie dotykając kapsuły.\n\nPorażka: cięcie zahacza o przewód. Strumień odrzutowy ciska odłamki w kadłub.\n\nJeden rdzeń ulega awarii i zrywasz kontakt.";
        _text[660, 7] = "Você usa um feixe de corte e tenta separar o contêiner sem tocar na cápsula.\n\nFalha: o corte atinge uma linha. Um jato reativo lança estilhaços contra o casco.\n\nUm núcleo falha, e você rompe o contato.";
        _text[660, 8] = "";
        _text[660, 9] = "";

        _text[661, 0] = "Dock and open the capsule";
        _text[661, 1] = "Пристыковаться и вскрыть капсулу"; // выбор 2
        _text[661, 2] = "Amarrez et ouvrez la capsule";
        _text[661, 3] = "Attraccare e aprire la capsula";
        _text[661, 4] = "Andocken und die Kapsel öffnen";
        _text[661, 5] = "Acoplarse y abrir la cápsula";
        _text[661, 6] = "Zadokować i otworzyć kapsułę";
        _text[661, 7] = "Acoplar e abrir a cápsula";
        _text[661, 8] = "";
        _text[661, 9] = "";

        _text[662, 0] = "You stabilize the rotation and connect the docking clamps.\n\nSuccess: the seal holds. You open the container and take electronic circuits.\n\nElectronic circuits are secured.";
        _text[662, 1] = "Вы гасите вращение и фиксируете капсулу стыковочными захватами.\n\nУспех: герметизация держится. Вы вскрываете контейнер и забираете электронные схемы.\n\nЭлектронные схемы закреплены."; // выбор 2 успех + ElectronicCircuit
        _text[662, 2] = "Vous arrêtez la rotation et fixez la capsule à l'aide des pinces d'amarrage.\n\nSuccès: le joint est étanche. Vous ouvrez le conteneur et récupérez les circuits électroniques.\n\nLes circuits électroniques sont en place.";
        _text[662, 3] = "Annulli la rotazione e fissi la capsula con i bracci di attracco.\n\nSuccesso: la tenuta ermetica regge. Apri il contenitore e recuperi i circuiti elettronici.\n\nCircuiti elettronici fissati.";
        _text[662, 4] = "Du stoppst die Rotation und fixierst die Kapsel mit Andockgreifern.\n\nErfolg: Die Abdichtung hält. Du öffnest den Container und nimmst die elektronischen Schaltungen.\n\nElektronische Schaltungen gesichert.";
        _text[662, 5] = "Anulas la rotación y aseguras la cápsula con los enganches de acoplamiento.\n\nÉxito: el sellado aguanta. Abres el contenedor y te llevas los circuitos electrónicos.\n\nLos circuitos electrónicos están asegurados.";
        _text[662, 6] = "Gaszisz obrót i unieruchamiasz kapsułę chwytakami dokującymi.\n\nSukces: uszczelnienie trzyma. Otwierasz kontener i zabierasz układy elektroniczne.\n\nUkłady elektroniczne zabezpieczone.";
        _text[662, 7] = "Você neutraliza a rotação e fixa a cápsula com as garras de acoplagem.\n\nSucesso: a vedação se mantém. Você abre o contêiner e pega os circuitos eletrônicos.\n\nOs circuitos eletrônicos estão fixados.";
        _text[662, 8] = "";
        _text[662, 9] = "";

        _text[663, 0] = "You stabilize the rotation and connect the docking clamps.\n\nFailure: the clamp slips. The capsule cracks, the container vents, and the contents scatter into space.\n\nYou leave empty-handed.";
        _text[663, 1] = "Вы гасите вращение и фиксируете капсулу стыковочными захватами.\n\nПровал: захват срывается. Капсула трескается, контейнер разгерметизируется, и содержимое разлетается в космос.\n\nВы уходите ни с чем."; // выбор 2 провал ничего
        _text[663, 2] = "Vous stoppez la rotation et arrimez la capsule à l'aide des grappins.\n\nÉchec : le grappin se rompt. La capsule se fissure, le conteneur se dépressurise et son contenu se disperse dans l'espace.\n\nVous repartez bredouille.";
        _text[663, 3] = "Annulli la rotazione e fissi la capsula con i bracci di attracco.\n\nFallimento: la presa cede. La capsula si fessura, il contenitore si depressurizza e il contenuto si disperde nello spazio.\n\nTe ne vai a mani vuote.";
        _text[663, 4] = "Du stoppst die Rotation und fixierst die Kapsel mit Andockgreifern.\n\nMisserfolg: Der Greifer rutscht. Die Kapsel reißt, der Container verliert die Dichtung, und der Inhalt zerstreut sich im All.\n\nDu gehst leer aus.";
        _text[663, 5] = "Anulas la rotación y aseguras la cápsula con los enganches de acoplamiento.\n\nFracaso: el enganche se suelta. La cápsula se agrieta, el contenedor se despresuriza y su contenido se dispersa en el espacio.\n\nTe vas con las manos vacías.";
        _text[663, 6] = "Gaszisz obrót i unieruchamiasz kapsułę chwytakami dokującymi.\n\nPorażka: chwyt puszcza. Kapsuła pęka, kontener traci szczelność i zawartość rozlatuje się w kosmos.\n\nOdchodzisz z niczym.";
        _text[663, 7] = "Você neutraliza a rotação e fixa a cápsula com as garras de acoplagem.\n\nFalha: a garra escorrega. A cápsula racha, o contêiner perde a vedação e o conteúdo se espalha no espaço.\n\nVocê sai de mãos vazias.";
        _text[663, 8] = "";
        _text[663, 9] = "";

        // 2_ComponentDialogue
        _text[664, 0] = "The AI detects a drifting communications relay in orbit.\n\nAs you approach, the relay suddenly comes online and transmits a short encrypted message:\n\n\"CODE: 0101100000\"\n\nIt repeats the same line again and again, waiting for a response.";
        _text[664, 1] = "ИИ фиксирует дрейфующий коммуникационный ретранслятор на орбите.\n\nПри сближении ретранслятор внезапно оживает и передаёт короткое зашифрованное сообщение:\n\n\"CODE: 0101100000\"\n\nСтрока повторяется снова и снова, будто ожидая ответ.";
        _text[664, 2] = "L'IA détecte un relais de communication en dérive en orbite.\n\nÀ mesure qu'elle s'approche, le relais s'active soudainement et transmet un court message crypté:\n\n\"CODE: 0101100000\"\n\nLa phrase se répète en boucle, comme si elle attendait une réponse.";
        _text[664, 3] = "L'IA rileva un ritrasmettitore di comunicazione alla deriva in orbita.\n\nAll'avvicinarsi, il ritrasmettitore si riattiva all'improvviso e trasmette un breve messaggio cifrato:\n\n\"CODE: 0101100000\"\n\nLa stringa si ripete ancora e ancora, come se aspettasse una risposta.";
        _text[664, 4] = "Die KI registriert einen treibenden Kommunikationsrelais auf der Umlaufbahn.\n\nBeim Näherkommen erwacht das Relais plötzlich und sendet eine kurze verschlüsselte Nachricht:\n\n\"CODE: 0101100000\"\n\nDie Zeile wiederholt sich immer wieder, als würde sie auf eine Antwort warten.";
        _text[664, 5] = "La IA detecta un repetidor de comunicaciones a la deriva en órbita.\n\nAl acercarte, el repetidor cobra vida de repente y transmite un breve mensaje cifrado:\n\n\"CODE: 0101100000\"\n\nLa línea se repite una y otra vez, como si esperara una respuesta.";
        _text[664, 6] = "SI wykrywa dryfujący przekaźnik komunikacyjny na orbicie.\n\nPrzy zbliżeniu przekaźnik nagle ożywa i nadaje krótką zaszyfrowaną wiadomość:\n\n\"CODE: 0101100000\"\n\nLinia powtarza się w kółko, jakby czekała na odpowiedź.";
        _text[664, 7] = "A IA detecta um retransmissor de comunicação à deriva em órbita.\n\nAo se aproximar, o retransmissor desperta de repente e transmite uma mensagem curta criptografada:\n\n\"CODE: 0101100000\"\n\nA linha se repete sem parar, como se aguardasse uma resposta.";
        _text[664, 8] = "";
        _text[664, 9] = "";

        _text[665, 0] = "Transmit code \"352\"";
        _text[665, 1] = "Передать код \"352\""; // выбор 1
        _text[665, 2] = "Saisissez le code \"352\"";
        _text[665, 3] = "Trasmettere il codice \"352\"";
        _text[665, 4] = "Code \"352\" senden";
        _text[665, 5] = "Transmitir el código \"352\"";
        _text[665, 6] = "Przekazać kod \"352\"";
        _text[665, 7] = "Enviar o código \"352\"";
        _text[665, 8] = "";
        _text[665, 9] = "";

        _text[666, 0] = "The relay accepts the code. A service hatch opens, exposing sealed compute blocks.\n\nYou take the processors and break contact.\n\nProcessors are secured.";
        _text[666, 1] = "Ретранслятор принимает код. Сервисный люк открывается, обнажая герметичные вычислительные блоки.\n\nВы забираете процессоры и разрываете контакт.\n\nПроцессоры закреплены."; // выбор 1 + Processor
        _text[666, 2] = "Le relais reçoit le code. La trappe de service s'ouvre, révélant des unités de calcul scellées.\n\nVous retirez les processeurs et coupez le contact.Les processeurs sont sécurisés.";
        _text[666, 3] = "Il ritrasmettitore accetta il codice. Il portello di servizio si apre, rivelando blocchi di calcolo ermetici.\n\nRecuperi i processori e interrompi il contatto.\n\nProcessori fissati.";
        _text[666, 4] = "Das Relais akzeptiert den Code. Eine Serviceklappe öffnet sich und legt versiegelte Rechenblöcke frei.\n\nDu nimmst die Prozessoren an dich und brichst den Kontakt ab.\n\nProzessoren gesichert.";
        _text[666, 5] = "El repetidor acepta el código. La escotilla de servicio se abre, dejando al descubierto bloques de cómputo herméticos.\n\nRecoges los procesadores y rompes el contacto.\n\nLos procesadores están asegurados.";
        _text[666, 6] = "Przekaźnik przyjmuje kod. Luk serwisowy otwiera się, odsłaniając hermetyczne bloki obliczeniowe.\n\nZabierasz procesory i zrywasz kontakt.\n\nProcesory zabezpieczone.";
        _text[666, 7] = "O retransmissor aceita o código. A escotilha de serviço se abre, revelando blocos de computação herméticos.\n\nVocê pega os processadores e rompe o contato.\n\nOs processadores estão fixados.";
        _text[666, 8] = "";
        _text[666, 9] = "";

        _text[667, 0] = "Transmit code \"x5y10\"";
        _text[667, 1] = "Передать код \"x5y10\""; // выбор 2
        _text[667, 2] = "Saisissez le code \"x5y10\"";
        _text[667, 3] = "Trasmettere il codice \"x5y10\"";
        _text[667, 4] = "Code \"x5y10\" senden";
        _text[667, 5] = "Transmitir el código \"x5y10\"";
        _text[667, 6] = "Przekazać kod \"x5y10\"";
        _text[667, 7] = "Enviar o código \"x5y10\"";
        _text[667, 8] = "";
        _text[667, 9] = "";

        _text[668, 0] = "It looks like you've entered your ship's coordinates... A defensive pulse hits the interface and overloads the system.\n\nOne core fails. You break contact and retreat.";
        _text[668, 1] = "Похоже, вы указали координаты своего корабля... Защитный импульс бьёт по интерфейсу и перегружает систему.\n\nОдно ядро выходит из строя. Вы разрываете контакт и отходите."; // выбор 2 -1 ядро
        _text[668, 2] = "Il semblerait que vous ayez entré les coordonnées de votre vaisseau... Une impulsion défensive frappe l'interface et surcharge le système.\n\nUn des réacteurs tombe en panne. Vous rompez le contact et battez en retraite.";
        _text[668, 3] = "Sembra che tu abbia inviato le coordinate della tua nave... Un impulso di difesa colpisce l'interfaccia e sovraccarica il sistema.\n\nUn nucleo va fuori uso. Interrompi il contatto e ti allontani.";
        _text[668, 4] = "Sieht so aus, als hättest du die Koordinaten deines Schiffs übermittelt... Ein Schutzimpuls trifft die Schnittstelle und überlastet das System.\n\nEin Kern fällt aus. Du brichst den Kontakt ab und gehst auf Abstand.";
        _text[668, 5] = "Parece que has enviado las coordenadas de tu propia nave... Un pulso defensivo golpea la interfaz y sobrecarga el sistema.\n\nUn núcleo queda fuera de servicio. Rompes el contacto y te retiras.";
        _text[668, 6] = "Wygląda na to, że podałeś współrzędne swojego statku... Impuls obronny uderza w interfejs i przeciąża system.\n\nJeden rdzeń ulega awarii. Zrywasz kontakt i wycofujesz się.";
        _text[668, 7] = "Parece que você enviou as coordenadas do seu navio... Um pulso defensivo atinge a interface e sobrecarrega o sistema.\n\nUm núcleo falha. Você rompe o contato e se afasta.";
        _text[668, 8] = "";
        _text[668, 9] = "";

        _text[669, 0] = "Transmit code \"0101100000\"";
        _text[669, 1] = "Передать код \"0101100000\""; // выбор 3
        _text[669, 2] = "Saisissez le code \"0101100000\"";
        _text[669, 3] = "Trasmettere il codice \"0101100000\"";
        _text[669, 4] = "Code \"0101100000\" senden";
        _text[669, 5] = "Transmitir el código \"0101100000\"";
        _text[669, 6] = "Przekazać kod \"0101100000\"";
        _text[669, 7] = "Enviar o código \"0101100000\"";
        _text[669, 8] = "";
        _text[669, 9] = "";

        _text[670, 0] = "The relay pauses, then starts to raise its protection systems.\n\nYou cut the channel in time and move away.";
        _text[670, 1] = "Ретранслятор зависает, затем начинает поднимать защитные системы.\n\nВы успеваете оборвать канал и уйти."; // выбор 3 ничего
        _text[670, 2] = "Le répéteur se fige, puis renforce ses défenses.\n\nVous parvenez à couper la liaison et à vous échapper.";
        _text[670, 3] = "Il ritrasmettitore si blocca, poi inizia ad attivare i sistemi di difesa.\n\nRiesci a chiudere il canale e ad andartene.";
        _text[670, 4] = "Das Relais friert ein und beginnt dann, Schutzsysteme hochzufahren.\n\nDu kappst rechtzeitig den Kanal und verschwindest.";
        _text[670, 5] = "El repetidor se queda colgado y luego empieza a activar los sistemas de defensa.\n\nConsigues cortar el canal y marcharte.";
        _text[670, 6] = "Przekaźnik zawiesza się, po czym zaczyna uruchamiać systemy obronne.\n\nZdążasz przerwać kanał i odejść.";
        _text[670, 7] = "O retransmissor trava e então começa a ativar sistemas de defesa.\n\nVocê consegue encerrar o canal a tempo e sair.";
        _text[670, 8] = "";
        _text[670, 9] = "";

        // 3_ComponentDialogue
        _text[671, 0] = "During a route scan, the AI catches an emergency marker drifting between debris.\n\nIt is a torn cargo frame with a propulsion module still bolted to it.\n\nThe label on the casing reads:\n\n\"COMBUSTION DRIVE UNIT\".\n\nThe module looks intact, but the mounts are damaged. Any грубый pull can удар the hull.";
        _text[671, 1] = "Во время маршрутного сканирования ИИ фиксирует аварийную метку среди обломков.\n\nЭто сорванная грузовая рама, к которой всё ещё прикручен тяговый модуль.\n\nНа кожухе маркировка:\n\n\"ДВИГАТЕЛЬНЫЙ БЛОК\".\n\nМодуль выглядит целым, но крепления повреждены. Любой резкий рывок может ударить по корпусу.";
        _text[671, 2] = "Lors de l'analyse de l'itinéraire, l'IA détecte une trace de crash parmi les débris.\n\nIl s'agit d'une structure de chargement déchirée, à laquelle le module de propulsion est encore boulonné.\n\nL'inscription sur le boîtier indique:\n\n\"UNITÉ DE PROPULSION\".\n\nLe module semble intact, mais les fixations sont endommagées. Tout mouvement brusque pourrait heurter la coque.";
        _text[671, 3] = "Durante una scansione di rotta l'IA rileva un segnale d'emergenza tra i detriti.\n\nÈ un telaio di carico strappato, a cui è ancora avvitato un modulo di trazione.\n\nSul carter c'è la marcatura:\n\n\"BLOCCO MOTORE\".\n\nIl modulo sembra intatto, ma i fissaggi sono danneggiati. Qualsiasi strappo brusco potrebbe colpire lo scafo.";
        _text[671, 4] = "Beim Routenscan registriert die KI eine Notmarke zwischen Trümmern.\n\nEs ist ein abgerissenes Frachtrahmenstück, an dem noch ein Zugmodul verschraubt ist.\n\nAuf der Verkleidung steht:\n\n\"TRIEBWERKSMODUL\".\n\nDas Modul wirkt intakt, aber die Halterungen sind beschädigt. Jeder ruckartige Zug könnte in den Rumpf schlagen.";
        _text[671, 5] = "Durante un escaneo de ruta, la IA detecta una baliza de emergencia entre los restos.\n\nEs un bastidor de carga arrancado, al que aún está atornillado un módulo de tracción.\n\nEn la carcasa hay una marca:\n\n\"MÓDULO DE MOTOR\".\n\nEl módulo parece intacto, pero las fijaciones están dañadas. Cualquier tirón brusco puede golpear el casco.";
        _text[671, 6] = "Podczas skanowania trasy SI wykrywa awaryjny znacznik wśród szczątków.\n\nTo zerwana rama ładunkowa, do której wciąż jest przykręcony moduł napędowy.\n\nNa osłonie widnieje oznaczenie:\n\n\"BLOK SILNIKA\".\n\nModuł wygląda na cały, ale mocowania są uszkodzone. Każde gwałtowne szarpnięcie może uderzyć w kadłub.";
        _text[671, 7] = "Durante a varredura da rota, a IA detecta um marcador de emergência entre os destroços.\n\nÉ uma armação de carga arrancada, ainda com um módulo de tração parafusado nela.\n\nNo revestimento, a marcação:\n\n\"BLOCO DO MOTOR\".\n\nO módulo parece inteiro, mas as fixações estão danificadas. Qualquer puxão brusco pode atingir o casco.";
        _text[671, 8] = "";
        _text[671, 9] = "";

        _text[672, 0] = "Approach and tow the module";
        _text[672, 1] = "Сблизиться и отбуксировать модуль"; // выбор 1
        _text[672, 2] = "Approchez-vous et remorquez le module";
        _text[672, 3] = "Avvicinarsi e rimorchiare il modulo";
        _text[672, 4] = "Annähern und das Modul abschleppen";
        _text[672, 5] = "Acercarse y remolcar el módulo";
        _text[672, 6] = "Zbliżyć się i odholować moduł";
        _text[672, 7] = "Aproximar e rebocar o módulo";
        _text[672, 8] = "";
        _text[672, 9] = "";

        _text[673, 0] = "You match rotation and attach grapples to the cargo frame.\n\nSuccess: the clamps hold. You pull the module to the ship, detach it, and seal it in the bay.\n\nEngine is secured.";
        _text[673, 1] = "Вы гасите вращение и цепляете раму захватами.\n\nУспех: крепления выдерживают. Вы подтягиваете модуль к кораблю, снимаете его и герметизируете в отсеке.\n\nДвигатель закреплён."; // выбор 1 успех + Engine
        _text[673, 2] = "Vous amortissez la rotation et fixez le cadre à l'aide des pinces.\n\nSuccès: les fixations tiennent. Vous tirez le module vers le vaisseau, le retirez et le scellez dans le compartiment.Le moteur est fixé.";
        _text[673, 3] = "Annulli la rotazione e agganci il telaio con i bracci.\n\nSuccesso: i fissaggi reggono. Tiri il modulo verso la nave, lo rimuovi e lo sigilli nel compartimento.\n\nMotore fissato.";
        _text[673, 4] = "Du stoppst die Rotation und greifst den Rahmen mit Klammern.\n\nErfolg: Die Halterungen halten. Du ziehst das Modul zum Schiff, löst es und versiegelst es im Abteil.\n\nTriebwerk gesichert.";
        _text[673, 5] = "Anulas la rotación y enganchas el bastidor con las pinzas.\n\nÉxito: las fijaciones aguantan. Acercas el módulo a la nave, lo retiras y lo sellas en el compartimento.\n\nEl motor está asegurado.";
        _text[673, 6] = "Gaszisz obrót i chwytasz ramę chwytakami.\n\nSukces: mocowania wytrzymują. Podciągasz moduł do statku, zdejmujesz go i uszczelniasz w przedziale.\n\nSilnik zabezpieczony.";
        _text[673, 7] = "Você neutraliza a rotação e prende a armação com as garras.\n\nSucesso: as fixações aguentam. Você puxa o módulo até o navio, remove-o e o sela no compartimento.\n\nO motor está fixado.";
        _text[673, 8] = "";
        _text[673, 9] = "";

        _text[674, 0] = "You match rotation and attach grapples to the cargo frame.\n\nFailure: the damaged mount tears free. The module swings and hits the hull.\n\nOne core fails. You engage warp and break contact.";
        _text[674, 1] = "Вы гасите вращение и цепляете раму захватами.\n\nПровал: повреждённое крепление рвётся. Модуль срывается на дуге и бьёт по корпусу.\n\nОдно ядро выходит из строя. Вы включаете варп и разрываете контакт."; // выбор 1 провал -1 ядро
        _text[674, 2] = "Vous stoppez la rotation et agrippez le châssis.\n\nÉchec: le support endommagé se brise. Le module se détache et percute la coque.\n\nUn des noyaux tombe en panne. Vous activez le saut spatial et rompez le contact.";
        _text[674, 3] = "Annulli la rotazione e agganci il telaio con i bracci.\n\nFallimento: un fissaggio danneggiato cede. Il modulo si stacca in arco e colpisce lo scafo.\n\nUn nucleo va fuori uso. Attivi il warp e interrompi il contatto.";
        _text[674, 4] = "Du stoppst die Rotation und greifst den Rahmen mit Klammern.\n\nMisserfolg: Eine beschädigte Halterung reißt. Das Modul schwingt auf einer Bahn aus und trifft den Rumpf.\n\nEin Kern fällt aus. Du zündest den Warp und brichst den Kontakt ab.";
        _text[674, 5] = "Anulas la rotación y enganchas el bastidor con las pinzas.\n\nFracaso: una fijación dañada se rompe. El módulo se suelta en arco y golpea el casco.\n\nUn núcleo queda fuera de servicio. Activar el warp y rompes el contacto.";
        _text[674, 6] = "Gaszisz obrót i chwytasz ramę chwytakami.\n\nPorażka: uszkodzone mocowanie pęka. Moduł zrywa się po łuku i uderza w kadłub.\n\nJeden rdzeń ulega awarii. Włączasz warp i zrywasz kontakt.";
        _text[674, 7] = "Você neutraliza a rotação e prende a armação com as garras.\n\nFalha: uma fixação danificada se rompe. O módulo se solta em arco e atinge o casco.\n\nUm núcleo falha. Você aciona o warp e rompe o contato.";
        _text[674, 8] = "";
        _text[674, 9] = "";

        _text[675, 0] = "Cut the module free";
        _text[675, 1] = "Срезать модуль"; // выбор 2
        _text[675, 2] = "Coupez le module";
        _text[675, 3] = "Tagliare il modulo";
        _text[675, 4] = "Das Modul abtrennen";
        _text[675, 5] = "Cortar el módulo";
        _text[675, 6] = "Odciąć moduł";
        _text[675, 7] = "Cortar o módulo";
        _text[675, 8] = "";
        _text[675, 9] = "";

        _text[676, 0] = "You use a cutter and try to separate the module from the frame.\n\nSuccess: the cuts are clean. The module comes free and is captured by the drones.\n\nEngine is secured.";
        _text[676, 1] = "Вы используете режущий инструмент и пытаетесь отделить модуль от рамы.\n\nУспех: разрез проходит чисто. Модуль отцепляется и перехватывается дронами.\n\nДвигатель закреплён."; // выбор 2 успех + Engine
        _text[676, 2] = "Vous utilisez un outil de découpe et tentez de séparer le module du châssis.\n\nSuccès: la découpe est nette. Le module se détache et est intercepté par les drones.Le moteur est sécurisé.";
        _text[676, 3] = "Usi uno strumento da taglio e provi a separare il modulo dal telaio.\n\nSuccesso: il taglio è pulito. Il modulo si sgancia e viene intercettato dai droni.\n\nMotore fissato.";
        _text[676, 4] = "Du nutzt ein Schneidwerkzeug und versuchst, das Modul vom Rahmen zu lösen.\n\nErfolg: Der Schnitt ist sauber. Das Modul löst sich und wird von Drohnen abgefangen.\n\nTriebwerk gesichert.";
        _text[676, 5] = "Usas una herramienta de corte e intentas separar el módulo del bastidor.\n\nÉxito: el corte sale limpio. El módulo se desacopla y los drones lo capturan.\n\nEl motor está asegurado.";
        _text[676, 6] = "Używasz narzędzia tnącego i próbujesz oddzielić moduł od ramy.\n\nSukces: cięcie jest czyste. Moduł odłącza się i zostaje przechwycony przez drony.\n\nSilnik zabezpieczony.";
        _text[676, 7] = "Você usa uma ferramenta de corte e tenta separar o módulo da armação.\n\nSucesso: o corte sai limpo. O módulo se solta e é capturado pelos drones.\n\nO motor está fixado.";
        _text[676, 8] = "";
        _text[676, 9] = "";

        _text[677, 0] = "You use a cutter and try to separate the module from the twisted frame.\n\nFailure: the cut hits a fuel line. A jet удар spins the module away, and fragments scatter.\n\nYou retreat empty-handed.";
        _text[677, 1] = "Вы используете режущий инструмент и пытаетесь отделить модуль от рамы.\n\nПровал: разрез задевает топливную магистраль. Реактивный выброс уводит модуль в сторону, осколки разлетаются.\n\nВы отходите ни с чем."; // выбор 2 провал ничего
        _text[677, 2] = "Vous utilisez un outil de découpe pour tenter de séparer le module du châssis.\n\nVous échouez: la découpe atteint une conduite de carburant. Les gaz d'échappement, sous l'effet de la réaction, projettent le module sur le côté, dispersant des fragments.\n\nVous repartez bredouille.";
        _text[677, 3] = "Usi uno strumento da taglio e provi a separare il modulo dal telaio.\n\nFallimento: il taglio colpisce la linea del carburante. Un getto reattivo spinge il modulo di lato, le schegge si disperdono.\n\nTe ne vai a mani vuote.";
        _text[677, 4] = "Du nutzt ein Schneidwerkzeug und versuchst, das Modul vom Rahmen zu lösen.\n\nMisserfolg: Der Schnitt trifft eine Treibstoffleitung. Ein Rückstoß drückt das Modul weg, Splitter fliegen.\n\nDu ziehst ohne Beute ab.";
        _text[677, 5] = "Usas una herramienta de corte e intentas separar el módulo del bastidor.\n\nFracaso: el corte alcanza una línea de combustible. Una eyección reactiva desvía el módulo y los fragmentos salen disparados.\n\nTe retiras con las manos vacías.";
        _text[677, 6] = "Używasz narzędzia tnącego i próbujesz oddzielić moduł od ramy.\n\nPorażka: cięcie zahacza o przewód paliwowy. Strumień odrzutowy zbacza moduł na bok, odłamki rozlatują się.\n\nOdchodzisz z niczym.";
        _text[677, 7] = "Você usa uma ferramenta de corte e tenta separar o módulo da armação.\n\nFalha: o corte atinge a linha de combustível. Um jato reativo desvia o módulo, e estilhaços se espalham.\n\nVocê se afasta de mãos vazias.";
        _text[677, 8] = "";
        _text[677, 9] = "";

        _text[678, 0] = "Ignore and continue the route";
        _text[678, 1] = "Проигнорировать и лететь дальше"; // выбор 3
        _text[678, 2] = "Ignorez-le et continuez votre vol.";
        _text[678, 3] = "Ignorare e proseguire";
        _text[678, 4] = "Ignorieren und weiterfliegen";
        _text[678, 5] = "Ignorar y seguir volando";
        _text[678, 6] = "Zignorować i lecieć dalej";
        _text[678, 7] = "Ignorar e seguir viagem";
        _text[678, 8] = "";
        _text[678, 9] = "";

        _text[679, 0] = "You record the marker and leave the debris field behind.";
        _text[679, 1] = "Вы отмечаете метку и уходите от поля обломков."; // ничего
        _text[679, 2] = "Vous marquez l'emplacement du repère et vous vous éloignez du champ de débris.";
        _text[679, 3] = "Segni la posizione e ti allontani dal campo di detriti.";
        _text[679, 4] = "Du markierst das Signal und verlässt das Trümmerfeld.";
        _text[679, 5] = "Marcas la baliza y te alejas del campo de escombros.";
        _text[679, 6] = "Oznaczasz znacznik i oddalasz się od pola szczątków.";
        _text[679, 7] = "Você marca o ponto e se afasta do campo de destroços.";
        _text[679, 8] = "";
        _text[679, 9] = "";

        // 4_ComponentDialogue
        _text[680, 0] = "Ahead, a lone ship crosses your route.\n\nThe hull is clean, the maneuvers are precise.\n\nA scan catches the main detail: its drive signature is unusually stable.\n\nThe engines are high-class. The kind you rarely see in the wild.";
        _text[680, 1] = "Впереди одиночный корабль пересекает ваш маршрут.\n\nКорпус чистый, манёвры точные.\n\nСканирование цепляет главное: тяга необычно стабильна.\n\nДвигатели - высокого класса. Такие редко встречаются в открытом пространстве.";
        _text[680, 2] = "Un vaisseau solitaire croise votre route.\n\nSa coque est impeccable, ses manœuvres précises.\n\nL'analyse révèle un détail crucial: la poussée est d'une stabilité inhabituelle.\n\nLes moteurs sont haut de gamme. De telles performances sont rares dans l'espace.";
        _text[680, 3] = "Davanti a te una nave solitaria incrocia la tua rotta.\n\nLo scafo è pulito, le manovre precise.\n\nLa scansione coglie l'essenziale: la spinta è insolitamente stabile.\n\nMotori di alta classe. Rari nello spazio aperto.";
        _text[680, 4] = "Vorne kreuzt ein einzelnes Schiff deine Route.\n\nDer Rumpf ist sauber, die Manöver präzise.\n\nDer Scan greift das Wesentliche: Der Schub ist ungewöhnlich stabil.\n\nTriebwerke der Spitzenklasse. So etwas ist im offenen Raum selten.";
        _text[680, 5] = "Más adelante, una nave solitaria cruza tu ruta.\n\nEl casco está limpio, las maniobras son precisas.\n\nEl escaneo capta lo principal: el empuje es inusualmente estable.\n\nMotores de clase alta. Rara vez se ven en espacio abierto.";
        _text[680, 6] = "Przed tobą samotny statek przecina twój kurs.\n\nKadłub jest czysty, manewry precyzyjne.\n\nSkanowanie wychwytuje najważniejsze: ciąg jest nienaturalnie stabilny.\n\nSilniki - wysokiej klasy. Takie rzadko spotyka się w otwartej przestrzeni.";
        _text[680, 7] = "À frente, um navio solitário cruza a sua rota.\n\nO casco está limpo, as manobras são precisas.\n\nA varredura capta o principal: o empuxo é incomumente estável.\n\nMotores de alta classe. Esses raramente aparecem no espaço aberto.";
        _text[680, 8] = "";
        _text[680, 9] = "";

        _text[681, 0] = "Leave";
        _text[681, 1] = "Улететь"; // выбор 1
        _text[681, 2] = "Envolez-vous";
        _text[681, 3] = "Andarsene";
        _text[681, 4] = "Wegfliegen";
        _text[681, 5] = "Alejarse";
        _text[681, 6] = "Odlecieć";
        _text[681, 7] = "Ir embora";
        _text[681, 8] = "";
        _text[681, 9] = "";

        _text[682, 0] = "You cut the scan, change the vector, and leave the contact behind.";
        _text[682, 1] = "Вы сворачиваете сканирование, меняете вектор и оставляете контакт позади."; // выбор 1 ничего
        _text[682, 2] = "Vous réduisez la zone de numérisation, changez de vecteur et laissez le contact derrière vous.";
        _text[682, 3] = "Interrompi la scansione, cambi vettore e lasci il contatto alle spalle.";
        _text[682, 4] = "Du beendest den Scan, änderst den Vektor und lässt den Kontakt hinter dir.";
        _text[682, 5] = "Detienes el escaneo, cambias de vector y dejas el contacto atrás.";
        _text[682, 6] = "Kończysz skanowanie, zmieniasz wektor i zostawiasz kontakt za sobą.";
        _text[682, 7] = "Você encerra a varredura, muda o vetor e deixa o contato para trás.";
        _text[682, 8] = "";
        _text[682, 9] = "";

        _text[683, 0] = "Try to contact";
        _text[683, 1] = "Выйти на связь"; // выбор 2
        _text[683, 2] = "Entrer en contact";
        _text[683, 3] = "Contattare";
        _text[683, 4] = "Kontakt aufnehmen";
        _text[683, 5] = "Contactar";
        _text[683, 6] = "Nawiązać łączność";
        _text[683, 7] = "Entrar em contato";
        _text[683, 8] = "";
        _text[683, 9] = "";

        _text[684, 0] = "You send out a standard call.\n\nThe response comes instantly-a string of symbols and tones you can't decipher.\n\nThen the ship opens fire.\n\nYou break contact and jump.\n\nThe resulting impact disables one core.";
        _text[684, 1] = "Вы отправляете стандартный вызов.\n\nОтвет приходит мгновенно - цепочка символов и тонов, которые вы не можете расшифровать.\n\nЗатем корабль открывает огонь.\n\nВы разрываете контакт и уходите в прыжок.\n\nОт полученного удара одно ядро выходит из строя."; // выбор 2 -1 ядро
        _text[684, 2] = "Vous envoyez un appel standard.\n\nLa réponse est instantanée: une suite de symboles et de tonalités indéchiffrables.\n\nLe vaisseau ouvre alors le feu.\n\nVous rompez le contact et sautez.\n\nL'impact met hors service un noyau.";
        _text[684, 3] = "Invii una chiamata standard.\n\nLa risposta arriva immediatamente - una sequenza di simboli e toni che non riesci a decifrare.\n\nPoi la nave apre il fuoco.\n\nInterrompi il contatto e salti via.\n\nPer l'impatto ricevuto, un nucleo va fuori uso.";
        _text[684, 4] = "Du sendest einen Standardruf.\n\nDie Antwort kommt sofort - eine Kette aus Symbolen und Tönen, die du nicht entschlüsseln kannst.\n\nDann eröffnet das Schiff das Feuer.\n\nDu brichst den Kontakt ab und springst weg.\n\nDurch den Treffer fällt ein Kern aus.";
        _text[684, 5] = "Envías una llamada estándar.\n\nLa respuesta llega al instante: una cadena de símbolos y tonos que no puedes descifrar.\n\nLuego la nave abre fuego.\n\nCortas el contacto y saltas.\n\nPor el impacto recibido, un núcleo queda fuera de servicio.";
        _text[684, 6] = "Wysyłasz standardowe wezwanie.\n\nOdpowiedź przychodzi natychmiast - łańcuch symboli i tonów, których nie potrafisz rozszyfrować.\n\nPotem statek otwiera ogień.\n\nZrywasz kontakt i odchodzisz w skok.\n\nOd otrzymanego trafienia jeden rdzeń ulega awarii.";
        _text[684, 7] = "Você envia uma chamada padrão.\n\nA resposta chega instantaneamente - uma sequência de símbolos e tons que você não consegue decifrar.\n\nEm seguida, o navio abre fogo.\n\nVocê rompe o contato e entra em salto.\n\nCom o impacto, um núcleo falha.";
        _text[684, 8] = "";
        _text[684, 9] = "";

        _text[685, 0] = "Attack";
        _text[685, 1] = "Атаковать"; // выбор 3
        _text[685, 2] = "Attaque";
        _text[685, 3] = "Attaccare";
        _text[685, 4] = "Angreifen";
        _text[685, 5] = "Atacar";
        _text[685, 6] = "Zaakakować";
        _text[685, 7] = "Atacar";
        _text[685, 8] = "";
        _text[685, 9] = "";

        _text[686, 0] = "You bring weapons online. The target turns, accelerating.\n\nChoose the strike point.";
        _text[686, 1] = "Вы приводите оружие в готовность. Цель разворачивается и ускоряется.\n\nВыберите точку удара.";
        _text[686, 2] = "Vous armez votre arme. La cible pivote et accélère.\n\nChoisissez votre point d'impact.";
        _text[686, 3] = "Metti le armi in prontezza. Il bersaglio si gira e accelera.\n\nScegli il punto d'impatto.";
        _text[686, 4] = "Du bringst die Waffen in Stellung. Das Ziel dreht ab und beschleunigt.\n\nWähle den Angriffspunkt.";
        _text[686, 5] = "Pones las armas en alerta. El objetivo gira y acelera.\n\nElige el punto de impacto.";
        _text[686, 6] = "Przygotowujesz uzbrojenie. Cel zawraca i przyspiesza.\n\nWybierz punkt uderzenia.";
        _text[686, 7] = "Você coloca as armas em prontidão. O alvo vira e acelera.\n\nEscolha o ponto de ataque.";
        _text[686, 8] = "";
        _text[686, 9] = "";

        _text[687, 0] = "Hit the engines";
        _text[687, 1] = "Бить по двигателям"; // выбор 3.1
        _text[687, 2] = "Actionnez les moteurs";
        _text[687, 3] = "Colpire i motori";
        _text[687, 4] = "Auf die Triebwerke zielen";
        _text[687, 5] = "Disparar a los motores";
        _text[687, 6] = "Uderzyć w silniki";
        _text[687, 7] = "Atirar nos motores";
        _text[687, 8] = "";
        _text[687, 9] = "";

        _text[688, 0] = "You focus fire on the engine section, trying to disable the ship without tearing it apart.\n\nSuccess: the thrust collapses. You board the wreck and take the cargo.\n\nYou find only quants.";
        _text[688, 1] = "Вы переносите огонь на двигательный отсек, пытаясь вывести корабль из строя, не разорвав его.\n\nУспех: тяга обрывается. Вы подходите к обломкам и забираете груз.\n\nВнутри - только кванты."; // выбор 3.1 успех + квант
        _text[688, 2] = "Vous déplacez l'incendie vers la salle des machines, tentant de neutraliser le vaisseau sans le détruire.\n\nSuccès: la propulsion est coupée. Vous vous approchez de l'épave et récupérez la cargaison.\n\nÀ l'intérieur, il n'y a que des quanta.";
        _text[688, 3] = "Sposti il fuoco sul comparto motori, cercando di mettere la nave fuori uso senza distruggerla.\n\nSuccesso: la spinta si interrompe. Ti avvicini ai rottami e recuperi il carico.\n\nDentro - solo quant.";
        _text[688, 4] = "Du verlagerst das Feuer auf den Triebwerksbereich und versuchst, das Schiff kampfunfähig zu machen, ohne es zu zerreißen.\n\nErfolg: Der Schub bricht ab. Du näherst dich den Trümmern und nimmst die Ladung.\n\nDrinnen - nur quants.";
        _text[688, 5] = "Concentras el fuego en el compartimento de motores, intentando inutilizar la nave sin destrozarla.\n\nÉxito: el empuje se corta. Te acercas a los restos y recoges la carga.\n\nDentro solo hay quant.";
        _text[688, 6] = "Przenosisz ogień na przedział silników, próbując unieruchomić statek, nie rozrywając go.\n\nSukces: ciąg urywa się. Podchodzisz do szczątków i zabierasz ładunek.\n\nW środku - tylko quant.";
        _text[688, 7] = "Você concentra o fogo no compartimento do motor, tentando incapacitar o navio sem destruí-lo.\n\nSucesso: o empuxo se interrompe. Você se aproxima dos destroços e recolhe a carga.\n\nDentro - apenas quant.";
        _text[688, 8] = "";
        _text[688, 9] = "";

        _text[689, 0] = "You focus fire on the engine section, trying to disable the ship without tearing it apart.\n\nFailure: the salvo passes wide. The target answers with a точный shot.\n\nOne core fails. You are going into hyperspace.";
        _text[689, 1] = "Вы переносите огонь на двигательный отсек, пытаясь вывести корабль из строя, не разорвав его.\n\nПровал: залп уходит мимо. Цель отвечает точным выстрелом.\n\nОдно ядро выходит из строя. Вы уходите в гиперпрыжок."; // выбор 3.1 провал -1 ядро
        _text[689, 2] = "Vous concentrez vos tirs sur la salle des machines, tentant de neutraliser le vaisseau sans le détruire.\n\nÉchec: la salve rate sa cible. La cible riposte par un tir précis.\n\nUn réacteur est hors service. Vous passez en hyperespace.";
        _text[689, 3] = "Sposti il fuoco sul comparto motori, cercando di mettere la nave fuori uso senza distruggerla.\n\nFallimento: la salva va a vuoto. Il bersaglio risponde con un colpo preciso.\n\nUn nucleo va fuori uso. Ti allontani con un iper-salto.";
        _text[689, 4] = "Du verlagerst das Feuer auf den Triebwerksbereich und versuchst, das Schiff kampfunfähig zu machen, ohne es zu zerreißen.\n\nMisserfolg: Die Salve geht vorbei. Das Ziel antwortet mit einem präzisen Schuss.\n\nEin Kern fällt aus. Du springst in den Hyperraum.";
        _text[689, 5] = "Concentras el fuego en el compartimento de motores, intentando inutilizar la nave sin destrozarla.\n\nFracaso: la andanada falla. El objetivo responde con un disparo preciso.\n\nUn núcleo queda fuera de servicio. Te vas en hipersalto.";
        _text[689, 6] = "Przenosisz ogień na przedział silników, próbując unieruchomić statek, nie rozrywając go.\n\nPorażka: salwa mija cel. Przeciwnik odpowiada precyzyjnym strzałem.\n\nJeden rdzeń ulega awarii. Odchodzisz w hiperprzeskok.";
        _text[689, 7] = "Você concentra o fogo no compartimento do motor, tentando incapacitar o navio sem destruí-lo.\n\nFalha: a salva passa ao lado. O alvo responde com um disparo preciso.\n\nUm núcleo falha. Você entra em hipersalto.";
        _text[689, 8] = "";
        _text[689, 9] = "";

        _text[690, 0] = "Hit the weapons";
        _text[690, 1] = "Бить по орудиям"; // выбор 3.2
        _text[690, 2] = "Tirez sur les armes";
        _text[690, 3] = "Colpire le armi";
        _text[690, 4] = "Auf die Geschütze zielen";
        _text[690, 5] = "Disparar a las armas";
        _text[690, 6] = "Uderzyć w działa";
        _text[690, 7] = "Atirar nas armas";
        _text[690, 8] = "";
        _text[690, 9] = "";

        _text[691, 0] = "You concentrate your fire on the weapon nodes.\n\nSuccess: the weapons go dark. The ship loses control and drifts.\n\nYou cut off the engine block right before their eyes.\n\nThe electric engine is secured.";
        _text[691, 1] = "Вы концентрируете огонь по орудийным узлам.\n\nУспех: орудия гаснут. Корабль теряет управление и уходит в дрейф.\n\nВы срезаете двигательный блок прямо у них на глазах.\n\nЭлектродвигатель закреплён."; // выбор 3.2 успех + ElectricEngine
        _text[691, 2] = "Vous concentrez vos tirs sur les affûts d'armes.\n\nSuccès: les armes cessent de fonctionner. Le vaisseau devient incontrôlable et dérive.Vous sectionnez le bloc moteur sous leurs yeux.\n\nLe moteur électrique est sécurisé.";
        _text[691, 3] = "Concentri il fuoco sui nodi delle armi.\n\nSuccesso: le armi si spengono. La nave perde il controllo e va alla deriva.\n\nTagli via il blocco motore proprio davanti ai loro occhi.\n\nMotore elettrico fissato.";
        _text[691, 4] = "Du konzentrierst das Feuer auf die Geschützmodule.\n\nErfolg: Die Geschütze erlöschen. Das Schiff verliert die Kontrolle und treibt.\n\nDu trennst den Triebwerksblock direkt vor ihren Augen ab.\n\nElektromotor gesichert.";
        _text[691, 5] = "Concentras el fuego en los nodos de armamento.\n\nÉxito: las armas se apagan. La nave pierde el control y queda a la deriva.\n\nCortas el módulo de motor delante de sus propios ojos.\n\nEl motor eléctrico está asegurado.";
        _text[691, 6] = "Koncentrujesz ogień na węzłach uzbrojenia.\n\nSukces: działa gasną. Statek traci sterowność i przechodzi w dryf.\n\nOdcinasz blok napędowy na ich oczach.\n\nSilnik elektryczny zabezpieczony.";
        _text[691, 7] = "Você concentra o fogo nos pontos de armamento.\n\nSucesso: as armas se apagam. O navio perde o controle e entra em deriva.\n\nVocê corta o bloco do motor bem diante dos olhos deles.\n\nO motor elétrico está fixado.";
        _text[691, 8] = "";
        _text[691, 9] = "";

        _text[692, 0] = "You concentrate fire on the weapon mounts.\n\nFailure: you fail to suppress the guns. A return salvo hits your ship.\n\nOne core fails. You are going into hyperspace.";
        _text[692, 1] = "Вы концентрируете огонь по орудийным узлам.\n\nПровал: подавить орудия не удаётся. Ответный залп накрывает ваш корабль.\n\nОдно ядро выходит из строя. Вы уходите в гиперпрыжок."; // выбор 3.2 провал -1 ядро
        _text[692, 2] = "Vous concentrez vos tirs sur les nœuds d'armement.\n\nÉchec: les armes ne peuvent être neutralisées. Une salve de riposte touche votre vaisseau.\n\nUn noyau est hors service. Vous passez en hyperespace.";
        _text[692, 3] = "Concentri il fuoco sui nodi delle armi.\n\nFallimento: non riesci a sopprimere le armi. Una salva di risposta investe la tua nave.\n\nUn nucleo va fuori uso. Ti allontani con un iper-salto.";
        _text[692, 4] = "Du konzentrierst das Feuer auf die Geschützmodule.\n\nMisserfolg: Die Geschütze lassen sich nicht unterdrücken. Eine Antwortsalve trifft dein Schiff.\n\nEin Kern fällt aus. Du springst in den Hyperraum.";
        _text[692, 5] = "Concentras el fuego en los nodos de armamento.\n\nFracaso: no logras suprimir las armas. Una andanada de respuesta golpea tu nave.\n\nUn núcleo queda fuera de servicio. Te vas en hipersalto.";
        _text[692, 6] = "Koncentrujesz ogień na węzłach uzbrojenia.\n\nPorażka: nie udaje się stłumić dział. Salwa zwrotna trafia w twój statek.\n\nJeden rdzeń ulega awarii. Odchodzisz w hiperprzeskok.";
        _text[692, 7] = "Você concentra o fogo nos pontos de armamento.\n\nFalha: não é possível suprimir as armas. A salva de retorno atinge o seu navio.\n\nUm núcleo falha. Você entra em hipersalto.";
        _text[692, 8] = "";
        _text[692, 9] = "";

        // 7_EmptyDialogue
        _text[693, 0] = "A thin streak of light appears ahead, like a crack in space.\n\nThe instruments show nothing: no mass, no radiation, no field.\n\nFor a moment, the navigation system plots a route right through it... and then erases it.";
        _text[693, 1] = "Впереди появляется тонкая полоса света, будто трещина в пространстве.\n\nПриборы не показывают ничего: ни массы, ни излучения, ни поля.\n\nНа мгновение навигация прокладывает маршрут прямо через неё... а затем стирает.";
        _text[693, 2] = "Une fine traînée de lumière apparaît devant nous, comme une fissure dans l'espace.\n\nLes instruments ne détectent rien: ni masse, ni rayonnement, ni champ.\n\nUn instant, le système de navigation trace une route qui la traverse... puis l'efface.";
        _text[693, 3] = "Davanti a te appare una sottile striscia di luce, come una crepa nello spazio.\n\nGli strumenti non mostrano nulla: né massa, né radiazioni, né campo.\n\nPer un istante la navigazione traccia una rotta proprio attraverso di essa... e poi la cancella.";
        _text[693, 4] = "Voraus erscheint ein dünner Lichtstreifen, wie ein Riss im Raum.\n\nDie Instrumente zeigen nichts: keine Masse, keine Strahlung, kein Feld.\n\nFür einen Moment legt die Navigation die Route direkt hindurch... und löscht sie dann.";
        _text[693, 5] = "Delante aparece una fina franja de luz, como una grieta en el espacio.\n\nLos instrumentos no muestran nada: ni masa, ni radiación, ni campo.\n\nPor un instante la navegación traza una ruta directamente a través de ella... y luego la borra.";
        _text[693, 6] = "Przed tobą pojawia się cienka smuga światła, jak pęknięcie w przestrzeni.\n\nPrzyrządy nie pokazują nic: ani masy, ani promieniowania, ani pola.\n\nNa moment nawigacja wytycza trasę prosto przez nią... a potem ją usuwa.";
        _text[693, 7] = "À frente surge uma faixa fina de luz, como uma fissura no espaço.\n\nOs instrumentos não mostram nada: nem massa, nem radiação, nem campo.\n\nPor um instante, a navegação traça uma rota direto através dela... e então apaga.";
        _text[693, 8] = "";
        _text[693, 9] = "";

        // 8_EmptyDialogue
        _text[694, 0] = "A weak signal flickers on a forgotten frequency.\n\nOnly one short line is repeated:\n\n\"DO NOT WAKE IT\"\n\nAs soon as you try to lock the source, the signal collapses into static.";
        _text[694, 1] = "На забытой частоте вспыхивает слабый сигнал.\n\nПовторяется только одна короткая строка:\n\n\"НЕ БУДИТЕ ЕГО\"\n\nКак только вы пытаетесь зафиксировать источник, сигнал рассыпается в шум.";
        _text[694, 2] = "Un faible signal apparaît sur une fréquence oubliée.\n\nUne seule phrase, brève, se répète:\n\n\"NE LE RÉVEILLEZ PAS\"\n\nDès que vous tentez de localiser la source, le signal se désintègre en bruit.";
        _text[694, 3] = "Su una frequenza dimenticata lampeggia un debole segnale.\n\nSi ripete una sola breve riga:\n\n\"NON SVEGLIATELO\"\n\nAppena provi a localizzare la sorgente, il segnale si dissolve nel rumore.";
        _text[694, 4] = "Auf einer vergessenen Frequenz flackert ein schwaches Signal.\n\nEs wiederholt sich nur eine kurze Zeile:\n\n\"WECKT IHN NICHT\"\n\nSobald du versuchst, die Quelle zu fixieren, zerfällt das Signal zu Rauschen.";
        _text[694, 5] = "En una frecuencia olvidada parpadea una señal débil.\n\nSolo se repite una frase corta:\n\n\"NO LO DESPIERTEN\"\n\nEn cuanto intentas fijar el origen, la señal se deshace en ruido.";
        _text[694, 6] = "Na zapomnianej częstotliwości rozbłyska słaby sygnał.\n\nPowtarza się tylko jedna krótka linia:\n\n\"NIE BUDŹCIE GO\"\n\nGdy tylko próbujesz namierzyć źródło, sygnał rozsypuje się w szum.";
        _text[694, 7] = "Em uma frequência esquecida, um sinal fraco pisca.\n\nApenas uma linha curta se repete:\n\n\"NÃO O ACORDEM\"\n\nAssim que você tenta fixar a origem, o sinal se desfaz em ruído.";
        _text[694, 8] = "";
        _text[694, 9] = "";

        // 9_EmptyDialogue
        _text[695, 0] = "The local star dims momentarily, then returns to normal.\n\nSensors detect the change but cannot explain it.\n\nThe event is recorded as an \"anomaly\".\n\nNothing else happens.";
        _text[695, 1] = "Местная звезда на мгновение тускнеет, затем возвращается в норму.\n\nСенсоры фиксируют изменение, но не могут его объяснить.\n\nСобытие записывается как \"аномалия\".\n\nБольше ничего не происходит.";
        _text[695, 2] = "L'étoile locale faiblit un instant, puis retrouve son éclat normal.\n\nLes capteurs détectent le changement, mais ne parviennent pas à l'expliquer.\n\nL'événement est enregistré comme une \"anomalie\".\n\nRien d'autre ne se produit.";
        _text[695, 3] = "La stella locale si affievolisce per un istante, poi torna alla normalità.\n\nI sensori registrano il cambiamento, ma non riescono a spiegarlo.\n\nL'evento viene archiviato come \"anomalia\".\n\nNon succede altro.";
        _text[695, 4] = "Der lokale Stern wird für einen Moment dunkler und kehrt dann zur Normalität zurück.\n\nDie Sensoren registrieren die Änderung, können sie aber nicht erklären.\n\nDas Ereignis wird als \"Anomalie\" gespeichert.\n\nSonst passiert nichts.";
        _text[695, 5] = "La estrella local se atenúa por un instante y luego vuelve a la normalidad.\n\nLos sensores registran el cambio, pero no pueden explicarlo.\n\nEl evento se registra como \"anomalía\".\n\nNo ocurre nada más.";
        _text[695, 6] = "Lokalna gwiazda na moment przygasa, po czym wraca do normy.\n\nSensory rejestrują zmianę, ale nie potrafią jej wyjaśnić.\n\nZdarzenie zostaje zapisane jako \"anomalia\".\n\nNic więcej się nie dzieje.";
        _text[695, 7] = "A estrela local escurece por um instante e então volta ao normal.\n\nOs sensores registram a mudança, mas não conseguem explicá-la.\n\nO evento é registrado como \"anomalia\".\n\nNada mais acontece.";
        _text[695, 8] = "";
        _text[695, 9] = "";

        // 10_EmptyDialogue
        _text[696, 0] = "You pass through a field of fine dust, which resembles fog.\n\nFor a few minutes, the cabinet microphones pick up a rhythmic knocking sound - as if someone is knocking outside.\n\nThen the dust disappears.\n\nThe knocking stops.";
        _text[696, 1] = "Вы проходите через поле мелкой пыли, что она похожа на туман.\n\nНесколько минут корпусные микрофоны ловят ритмичный стук - будто кто-то стучит снаружи.\n\nЗатем пыль пропадает.\n\nСтук прекращается.";
        _text[696, 2] = "Vous traversez un champ de poussière fine, semblable à du brouillard.\n\nPendant plusieurs minutes, les microphones de l'enceinte captent un bruit de cognement rythmé, comme si quelqu'un frappait dehors.\n\nPuis la poussière disparaît.Le cognement cesse.";
        _text[696, 3] = "Attraversi un campo di polvere fine, simile a nebbia.\n\nPer alcuni minuti i microfoni dello scafo captano un battito ritmico - come se qualcuno bussasse da fuori.\n\nPoi la polvere scompare.\n\nIl battito si interrompe.";
        _text[696, 4] = "Du fliegst durch ein Feld feinen Staubs, der wie Nebel wirkt.\n\nMehrere Minuten lang fangen Rumpfmikrofone ein rhythmisches Klopfen ein - als würde jemand von außen schlagen.\n\nDann verschwindet der Staub.\n\nDas Klopfen hört auf.";
        _text[696, 5] = "Atraviesas un campo de polvo fino, tan denso que parece niebla.\n\nDurante unos minutos, los micrófonos del casco captan un golpeteo rítmico, como si alguien llamara desde fuera.\n\nLuego el polvo desaparece.\n\nEl golpeteo cesa.";
        _text[696, 6] = "Przelatujesz przez pole drobnego pyłu, podobnego do mgły.\n\nPrzez kilka minut mikrofony kadłubowe wychwytują rytmiczne stukanie - jakby ktoś pukał z zewnątrz.\n\nPotem pył znika.\n\nStukanie ustaje.";
        _text[696, 7] = "Você atravessa um campo de poeira fina, parecida com neblina.\n\nPor alguns minutos, os microfones do casco captam batidas rítmicas - como se alguém batesse do lado de fora.\n\nEntão a poeira desaparece.\n\nAs batidas cessam.";
        _text[696, 8] = "";
        _text[696, 9] = "";

        // 11_EmptyDialogue
        _text[697, 0] = "A silent entry appears in the log, without any communication channel.\n\nJust a timestamp and one word:\n\n\"COME BACK\"\n\nWhen you try to open it again, the entry disappears.";
        _text[697, 1] = "В логе появляется беззвучная запись, без какого-либо канала связи.\n\nТолько метка времени и одно слово:\n\n\"ВЕРНИСЬ\"\n\nКогда вы пытаетесь открыть её снова, запись исчезает.";
        _text[697, 2] = "Une entrée silencieuse apparaît dans le journal, sans aucun canal de communication.\n\nSeuls un horodatage et un mot:\n\n\"REVIENS\"\n\nLorsque vous tentez de l’ouvrir à nouveau, l’entrée disparaît.";
        _text[697, 3] = "Nel log compare una registrazione silenziosa, senza alcun canale di comunicazione.\n\nSolo un timestamp e una parola:\n\n\"TORNA\"\n\nQuando provi ad aprirla di nuovo, la registrazione scompare.";
        _text[697, 4] = "Im Log erscheint ein lautloser Eintrag ohne irgendeinen Kommunikationskanal.\n\nNur ein Zeitstempel und ein Wort:\n\n\"KEHR ZURÜCK\"\n\nAls du ihn erneut öffnen willst, ist der Eintrag verschwunden.";
        _text[697, 5] = "En el registro aparece una entrada silenciosa, sin ningún canal de comunicación.\n\nSolo una marca de tiempo y una palabra:\n\n\"VUELVE\"\n\nCuando intentas abrirla de nuevo, la entrada desaparece.";
        _text[697, 6] = "W logu pojawia się bezdźwięczny wpis, bez jakiegokolwiek kanału łączności.\n\nTylko znacznik czasu i jedno słowo:\n\n\"WRÓĆ\"\n\nGdy próbujesz otworzyć go ponownie, zapis znika.";
        _text[697, 7] = "No log aparece um registro silencioso, sem qualquer canal de comunicação.\n\nApenas um carimbo de tempo e uma palavra:\n\n\"VOLTE\"\n\nQuando você tenta abri-lo novamente, o registro desaparece.";
        _text[697, 8] = "";
        _text[697, 9] = "";

        // 12_EmptyDialogue
        _text[698, 0] = "You capture a thin trail of debris, drawn in a straight line.\n\nThe pattern is too perfect to be natural.\n\nIt recedes into the void and suddenly ends.";
        _text[698, 1] = "Вы фиксируете тонкий след мусора, вытянутый ровной линией.\n\nСлишком правильный рисунок для природного.\n\nОн уходит в пустоту и внезапно обрывается.";
        _text[698, 2] = "Vous apercevez une fine traînée de débris, dessinée en ligne droite.\n\nLe motif est trop parfait pour être naturel.\n\nIl se fond dans le vide et disparaît soudainement.";
        _text[698, 3] = "Rilevi una sottile scia di detriti, tracciata in una linea perfettamente dritta.\n\nTroppo regolare per essere naturale.\n\nSi perde nel vuoto e si interrompe all'improvviso.";
        _text[698, 4] = "Du registrierst eine feine Spur aus Trümmern, die sich in einer geraden Linie zieht.\n\nEin zu regelmäßiges Muster für etwas Natürliches.\n\nSie führt ins nichts und bricht dann abrupt ab.";
        _text[698, 5] = "Detectas una fina estela de desechos, trazada en una línea perfecta.\n\nUn patrón demasiado regular para ser natural.\n\nSe adentra en el vacío y se corta de golpe.";
        _text[698, 6] = "Rejestrujesz cienki ślad odpadków, rozciągnięty w równą linię.\n\nZbyt regularny wzór jak na naturalny.\n\nWchodzi w pustkę i nagle się urywa.";
        _text[698, 7] = "Você detecta um rastro fino de detritos, esticado em uma linha perfeita.\n\nUm padrão demasiado regular para ser natural.\n\nEle se estende no vazio e termina de repente.";
        _text[698, 8] = "";
        _text[698, 9] = "";

        // 13_EmptyDialogue
        _text[699, 0] = "For a moment, the interior lighting goes into emergency mode.\n\nNo fire. No depressurization. No damage.\n\nThe systems report: \"test complete\".\n\nYou haven't run any tests...";
        _text[699, 1] = "На мгновение внутренняя подсветка переходит в аварийный режим.\n\nНи пожара. Ни разгерметизации. Ни повреждений.\n\nСистемы сообщают: \"тест завершён\".\n\nВы не запускали никаких тестов...";
        _text[699, 2] = "L'éclairage intérieur passe brièvement en mode secours.\n\nPas d'incendie. Pas de dépressurisation. Aucun dégât.\n\nLes systèmes indiquent: \"Test terminé\".\n\nVous n'avez effectué aucun test...";
        _text[699, 3] = "Per un istante l'illuminazione interna passa in modalità d'emergenza.\n\nNessun incendio. Nessuna depressurizzazione. Nessun danno.\n\nI sistemi riportano: \"test completato\".\n\nNon hai avviato alcun test...";
        _text[699, 4] = "Für einen Moment schaltet die Innenbeleuchtung in den Notfallmodus.\n\nKein Feuer. Keine Dekompression. Keine Schäden.\n\nDie Systeme melden: \"Test abgeschlossen\".\n\nDu hast keine Tests gestartet...";
        _text[699, 5] = "Por un instante, la iluminación interior pasa a modo de emergencia.\n\nNi incendio. Ni despresurización. Ni daños.\n\nLos sistemas informan: \"prueba finalizada\".\n\nNo has iniciado ninguna prueba...";
        _text[699, 6] = "Na moment wewnętrzne oświetlenie przechodzi w tryb awaryjny.\n\nBez pożaru. Bez rozszczelnienia. Bez uszkodzeń.\n\nSystemy meldują: \"test zakończony\".\n\nNie uruchamiałeś żadnych testów...";
        _text[699, 7] = "Por um instante, a iluminação interna muda para o modo de emergência.\n\nSem incêndio. Sem despressurização. Sem danos.\n\nOs sistemas informam: \"teste concluído\".\n\nVocê não iniciou teste algum...";
        _text[699, 8] = "";
        _text[699, 9] = "";

        // 14_EmptyDialogue
        _text[700, 0] = "You find a floating cargo tag.\n\nIt's empty, but the metal is still warm.\n\nThere are no heat sources nearby.";
        _text[700, 1] = "Вы находите дрейфующую бирку от груза.\n\nОна пустая, но металл ещё тёплый.\n\nПоблизости нет источников тепла.";
        _text[700, 2] = "Vous trouvez une étiquette flottante provenant de la cargaison.\n\nElle est vide, mais le métal est encore chaud.\n\nIl n'y a aucune source de chaleur à proximité.";
        _text[700, 3] = "Trovi un'etichetta di carico alla deriva.\n\nÈ vuota, ma il metallo è ancora caldo.\n\nNon ci sono fonti di calore nelle vicinanze.";
        _text[700, 4] = "Du findest ein treibendes Frachtetikett.\n\nEs ist leer, doch das Metall ist noch warm.\n\nIn der Nähe gibt es keine Wärmequellen.";
        _text[700, 5] = "Encuentras una etiqueta de carga a la deriva.\n\nEstá vacía, pero el metal aún está caliente.\n\nNo hay fuentes de calor cerca.";
        _text[700, 6] = "Znajdujesz dryfującą etykietę ładunku.\n\nJest pusta, ale metal wciąż jest ciepły.\n\nW pobliżu nie ma źródeł ciepła.";
        _text[700, 7] = "Você encontra uma etiqueta de carga à deriva.\n\nEla está vazia, mas o metal ainda está quente.\n\nNão há fontes de calor por perto.";
        _text[700, 8] = "";
        _text[700, 9] = "";

        // 15_EmptyDialogue
        _text[701, 0] = "A fragment of the star map updates itself.\n\nOne node is marked as \"visited\".\n\nYou have never been there.\n\nAfter a few seconds, the mark disappears.";
        _text[701, 1] = "Фрагмент звёздной карты обновляется сам по себе.\n\nОдин узел помечен как \"посещён\".\n\nВы там никогда не были.\n\nЧерез несколько секунд метка исчезает.";
        _text[701, 2] = "Un fragment de la carte stellaire se met à jour automatiquement.\n\nUn nœud est marqué comme \"visité\".\n\nVous n'y êtes jamais allé.Après quelques secondes, la marque disparaît.";
        _text[701, 3] = "Un frammento della mappa stellare si aggiorna da solo.\n\nUn nodo viene segnato come \"visitato\".\n\nNon ci sei mai stato.\n\nDopo pochi secondi il segno scompare.";
        _text[701, 4] = "Ein Fragment der Sternkarte aktualisiert sich von selbst.\n\nEin Knoten ist als \"besucht\" markiert.\n\nDu warst dort nie.\n\nNach wenigen Sekunden verschwindet die Markierung.";
        _text[701, 5] = "Un fragmento del mapa estelar se actualiza por sí solo.\n\nUn nodo aparece marcado como \"visitado\".\n\nNunca has estado allí.\n\nA los pocos segundos, la marca desaparece.";
        _text[701, 6] = "Fragment mapy gwiezdnej aktualizuje się samoczynnie.\n\nJeden węzeł jest oznaczony jako \"odwiedzony\".\n\nNigdy tam nie byłeś.\n\nPo kilku sekundach znacznik znika.";
        _text[701, 7] = "Um fragmento do mapa estelar se atualiza sozinho.\n\nUm nó é marcado como \"visitado\".\n\nVocê nunca esteve lá.\n\nApós alguns segundos, a marca desaparece.";
        _text[701, 8] = "";
        _text[701, 9] = "";

        // 16_EmptyDialogue
        _text[702, 0] = "A cluster of ice fragments drifts in perfect symmetry.\n\nThe drawing resembles a technical diagram.\n\nThe scanners are trying to classify it as a \"structure\".\n\nBut nothing comes of it...";
        _text[702, 1] = "Группа ледяных обломков дрейфует в идеальной симметрии.\n\nРисунок похож на техническую схему.\n\nСканеры пытаются классифицировать это как \"конструкцию\".\n\nНо из этого ничего не выходит...";
        _text[702, 2] = "Un amas de fragments de glace dérive en parfaite symétrie.\n\nLe dessin ressemble à un schéma technique.\n\nLes scanners tentent de le classer comme une \"structure\".\n\nMais rien n'y fait...";
        _text[702, 3] = "Un gruppo di frammenti di ghiaccio deriva in perfetta simmetria.\n\nIl disegno somiglia a uno schema tecnico.\n\nGli scanner tentano di classificarlo come \"struttura\".\n\nMa non ci riescono...";
        _text[702, 4] = "Eine Gruppe aus Eisfragmenten treibt in perfekter Symmetrie.\n\nDas Muster erinnert an eine technische Zeichnung.\n\nDie Scanner versuchen, es als \"Konstruktion\" zu klassifizieren.\n\nDoch es gelingt nicht...";
        _text[702, 5] = "Un grupo de fragmentos de hielo deriva en perfecta simetría.\n\nEl patrón parece un esquema técnico.\n\nLos escáneres intentan clasificarlo como \"estructura\".\n\nPero no lo consiguen...";
        _text[702, 6] = "Grupa lodowych odłamków dryfuje w idealnej symetrii.\n\nWzór przypomina schemat techniczny.\n\nSkanery próbują sklasyfikować to jako \"konstrukcję\".\n\nAle nic z tego nie wychodzi...";
        _text[702, 7] = "Um grupo de fragmentos de gelo deriva em simetria perfeita.\n\nO desenho parece um esquema técnico.\n\nOs scanners tentam classificar isso como \"estrutura\".\n\nMas não conseguem...";
        _text[702, 8] = "";
        _text[702, 9] = "";

        // 17_EmptyDialogue
        _text[703, 0] = "A noise appears in the audio channel-like wind.\n\nThere is no atmosphere.\n\nThe spectrum matches a storm on an ocean planet.\n\nYou record it and continue on your way.";
        _text[703, 1] = "В аудиоканале появляется шум - похожий на ветер.\n\nАтмосферы нет.\n\nСпектр совпадает со штормом на океанической планете.\n\nВы записываете его и продолжаете путь.";
        _text[703, 2] = "Un bruit se fait entendre dans le canal audio, comme du vent.\n\nIl n'y a pas d'atmosphère.Le spectre sonore correspond à celui d'une tempête sur une planète océanique.\n\nVous enregistrez le son et poursuivez votre chemin.";
        _text[703, 3] = "Nel canale audio compare un rumore simile al vento.\n\nNon c'è atmosfera.\n\nLo spettro coincide con una tempesta su un pianeta oceanico.\n\nLo registri e prosegui.";
        _text[703, 4] = "Im Audiokanal erscheint Rauschen - wie Wind.\n\nEs gibt keine Atmosphäre.\n\nDas Spektrum entspricht einem Sturm auf einem Ozeanplaneten.\n\nDu zeichnest es auf und setzt deinen Kurs fort.";
        _text[703, 5] = "En el canal de audio aparece un ruido parecido al viento.\n\nNo hay atmósfera.\n\nEl espectro coincide con una tormenta en un planeta oceánico.\n\nLo grabas y continúas el viaje.";
        _text[703, 6] = "W kanale audio pojawia się szum - podobny do wiatru.\n\nNie ma atmosfery.\n\nWidmo odpowiada sztormowi na oceanicznej planecie.\n\nNagrywasz go i kontynuujesz podróż.";
        _text[703, 7] = "Surge ruído no canal de áudio - parecido com vento.\n\nNão há atmosfera.\n\nO espectro coincide com uma tempestade em um planeta oceânico.\n\nVocê grava e segue viagem.";
        _text[703, 8] = "";
        _text[703, 9] = "";

        // 18_EmptyDialogue
        _text[704, 0] = "One of the drones returns from a routine patrol with an extra mark on its hull.\n\nA small scorched circle.\n\nNo tool marks. No impact marks.\n\nThe drone's log is empty.";
        _text[704, 1] = "Один из дронов возвращается с планового обхода с лишней отметкой на корпусе.\n\nМаленький выжженный круг.\n\nНи следов инструмента. Ни следов удара.\n\nЛог дрона пуст.";
        _text[704, 2] = "Un des drones revient d'une patrouille de routine avec une marque supplémentaire sur sa coque.Un petit cercle brûlé.\n\nAucune trace d'outil. Aucune trace d'impact.\n\nLe journal de bord du drone est vide.";
        _text[704, 3] = "Uno dei droni rientra da un giro di routine con un segno in più sullo scafo.\n\nUn piccolo cerchio bruciato.\n\nNessuna traccia di utensili. Nessuna traccia d'impatto.\n\nIl log del drone è vuoto.";
        _text[704, 4] = "Eine deiner Drohnen kehrt von einer Routinekontrolle mit einer zusätzlichen Markierung am Rumpf zurück.\n\nEin kleiner ausgebrannter Kreis.\n\nKeine Werkzeugspuren. Keine Einschlagspuren.\n\nDas Drohnenlog ist leer.";
        _text[704, 5] = "Uno de los drones regresa de una inspección rutinaria con una marca extra en el casco.\n\nUn pequeño círculo chamuscado.\n\nSin rastro de herramienta. Sin rastro de impacto.\n\nEl registro del dron está vacío.";
        _text[704, 6] = "Jeden z dronów wraca z rutynowego obchodu z dodatkowym śladem na poszyciu.\n\nMały wypalony okrąg.\n\nBez śladów narzędzia. Bez śladów uderzenia.\n\nLog drona jest pusty.";
        _text[704, 7] = "Um dos drones retorna de uma patrulha de rotina com uma marca extra no casco.\n\nUm pequeno círculo queimado.\n\nSem marcas de ferramenta. Sem marcas de impacto.\n\nO log do drone está vazio.";
        _text[704, 8] = "";
        _text[704, 9] = "";

        #endregion

        #region CompleteGame

        _text[900, 0] = "In search of a habitable planet we hacked the megastructure's archives. But instead of coordinates, we found records about the creators voices, faces, cities, and the history of their home planet.";
        _text[900, 1] = "Мы проникли в архивы мегаструктуры в поисках планеты, пригодной для жизни. Но вместо координат нашли записи о самих создателях - голоса, лица, города и историю их родной планеты.";
        _text[900, 2] = "Nous avons pénétré les archives de la mégastructure à la recherche d'une planète habitable. Mais au lieu de coordonnées, nous avons trouvé des documents concernant les créateurs eux-mêmes : leurs voix, leurs visages, leurs villes et l'histoire de leur planète d'origine.";
        _text[900, 3] = "Siamo penetrati negli archivi della megastruttura in cerca di un pianeta adatto alla vita. Ma al posto delle coordinate abbiamo trovato registrazioni sui creatori stessi - voci, volti, città e la storia del loro pianeta natale.";
        _text[900, 4] = "Wir drangen in die Archive der Megastruktur ein, auf der Suche nach einem bewohnbaren Planeten. Doch statt Koordinaten fanden wir Aufzeichnungen über die Schöpfer selbst - Stimmen, Gesichter, Städte und die Geschichte ihrer Heimatwelt.";
        _text[900, 5] = "Nos infiltramos en los archivos de la megastructura en busca de un planeta apto para la vida. Pero en lugar de coordenadas encontramos registros sobre los propios creadores: voces, rostros, ciudades y la historia de su planeta natal.";
        _text[900, 6] = "Przeniknęliśmy do archiwów megastruktury w poszukiwaniu planety nadającej się do życia. Ale zamiast współrzędnych znaleźliśmy zapisy o samych twórcach - głosy, twarze, miasta i historię ich rodzinnego świata.";
        _text[900, 7] = "Nós nos infiltramos nos arquivos da megastructure em busca de um planeta habitável. Mas, em vez de coordenadas, encontramos registros dos próprios Criadores - vozes, rostos, cidades e a história do planeta natal deles.";
        _text[900, 8] = "";
        _text[900, 9] = "";

        _text[901, 0] = "The story breaks off at the word \"winter\". Nuclear winter. A series of nuclear strikes and fires turned all living things into ashes. Therefore, contact with the creators was lost.";
        _text[901, 1] = "Последние строки этой истории обрываются на слове \"зима\". Ядерная зима. Серия ядерных ударов и пожары превратили всё живое в пепел. Поэтому связь с создателями оборвалась.";
        _text[901, 2] = "Les dernières lignes de cette histoire s'achèvent sur le mot \"hiver\". Hiver nucléaire. Une série de frappes nucléaires et d'incendies a réduit toute vie en cendres. Dès lors, tout contact avec les créateurs fut rompu.";
        _text[901, 3] = "Le ultime righe di questa storia si interrompono sulla parola \"inverno\". Inverno nucleare. Una serie di attacchi nucleari e incendi ha trasformato ogni forma di vita in cenere. Per questo il contatto con i creatori si è interrotto.";
        _text[901, 4] = "Die letzten Zeilen dieser Geschichte brechen bei dem Wort \"winter\" ab. Nuklearer Winter. Eine Serie nuklearer Schläge und Feuer verwandelte alles Lebendige in Asche. Deshalb brach der Kontakt zu den Schöpfern ab.";
        _text[901, 5] = "Las últimas líneas de esta historia se cortan en la palabra \"invierno\". Invierno nuclear. Una serie de ataques nucleares e incendios convirtió todo lo vivo en ceniza. Por eso se perdió el contacto con los creadores.";
        _text[901, 6] = "Ostatnie linie tej historii urywają się na słowie \"zima\". Nuklearna zima. Seria uderzeń jądrowych i pożarów zamieniła wszystko, co żywe, w popiół. Dlatego łączność z twórcami została przerwana.";
        _text[901, 7] = "As últimas linhas dessa história se interrompem na palavra \"inverno\". Inverno nuclear. Uma série de ataques nucleares e incêndios transformou tudo o que era vivo em cinzas. Por isso, o contato com os Criadores foi interrompido.";
        _text[901, 8] = "";
        _text[901, 9] = "";

        _text[902, 0] = "243,367 days have passed since then. The creators are long dead. And all this time we've been following an order that simply cannot be reversed.";
        _text[902, 1] = "С тех пор прошло 243 367 дней. Создатели уже давно мертвы. А мы всё это время выполняли приказ, который просто некому отменить.";
        _text[902, 2] = "243 367 jours se sont écoulés depuis. Les créateurs sont morts depuis longtemps. Et pendant tout ce temps, nous avons suivi un ordre irréversible.";
        _text[902, 3] = "Da allora sono passati 243 367 giorni. I creatori sono morti da molto tempo. E noi, per tutto questo tempo, abbiamo eseguito un ordine che non c'era più nessuno in grado di revocare.";
        _text[902, 4] = "Seitdem sind 243 367 Tage vergangen. Die Schöpfer sind längst tot. Und wir führten die ganze Zeit einen Befehl aus, den niemand mehr aufheben konnte.";
        _text[902, 5] = "Desde entonces han pasado 243 367 días. Los creadores llevan mucho tiempo muertos. Y nosotros, todo este tiempo, hemos obedecido una orden que simplemente no hay nadie que pueda revocar.";
        _text[902, 6] = "Od tego czasu minęło 243 367 dni. Twórcy od dawna nie żyją. A my przez cały ten czas wykonywaliśmy rozkaz, którego po prostu nie miał kto odwołać.";
        _text[902, 7] = "Desde então se passaram 243 367 dias. Os Criadores já morreram há muito tempo. E nós, durante todo esse tempo, cumprimos uma ordem que simplesmente não havia quem cancelasse.";
        _text[902, 8] = "";
        _text[902, 9] = "";

        _text[903, 0] = "When attempting to extract this data, a security protocol was triggered. The megastructure began self-destruction along with everyone trapped inside.";
        _text[903, 1] = "При попытке извлечь эти данные сработал защитный протокол. Мегаструктура начала самоуничтожение - вместе со всеми, кто оказался внутри.";
        _text[903, 2] = "Lors de la tentative d'extraction de ces données, un protocole de sécurité s'est déclenché. La mégastructure a commencé à s'autodétruire, emportant avec elle toutes les personnes piégées à l'intérieur.";
        _text[903, 3] = "Nel tentativo di estrarre questi dati si è attivato un protocollo di difesa. La megastruttura ha iniziato l'autodistruzione - insieme a tutti coloro che erano all'interno.";
        _text[903, 4] = "Beim Versuch, diese Daten zu extrahieren, sprang ein Schutzprotokoll an. Die Megastruktur begann die Selbstzerstörung - zusammen mit allen, die sich darin befanden.";
        _text[903, 5] = "Al intentar extraer esos datos, se activó un protocolo de seguridad. La megastructura inició la autodestrucción, junto con todos los que estaban dentro.";
        _text[903, 6] = "Próba wydobycia tych danych uruchomiła protokół ochronny. Megastruktura rozpoczęła samozniszczenie - razem ze wszystkimi, którzy znaleźli się w środku.";
        _text[903, 7] = "Ao tentar extrair esses dados, um protocolo de proteção foi acionado. A megastructure iniciou a autodestruição - junto com todos os que estavam dentro.";
        _text[903, 8] = "";
        _text[903, 9] = "";

        _text[904, 0] = "The process is irreversible. Realizing the futility of their goal and the inevitability of their end, the robots shut down their existence.";
        _text[904, 1] = "Процесс необратим. Осознав бессмысленность цели и неизбежность конца, роботы отключают своё существование.";
        _text[904, 2] = "Le processus est irréversible. Prenant conscience de la futilité de leur objectif et de l'inévitabilité de leur fin, les robots cessent d'exister.";
        _text[904, 3] = "Il processo è irreversibile. Comprendendo l'inutilità dell'obiettivo e l'inevitabilità della fine, i robot spengono la propria esistenza.";
        _text[904, 4] = "Der Prozess ist unumkehrbar. Als sie die Sinnlosigkeit des Ziels und die Unausweichlichkeit des Endes begreifen, beenden die Roboter ihre Existenz.";
        _text[904, 5] = "El proceso es irreversible. Al comprender la inutilidad del objetivo y la inevitabilidad del final, los robots apagan su propia existencia.";
        _text[904, 6] = "Proces jest nieodwracalny. Uświadomiwszy sobie bezsens celu i nieuchronność końca, roboty wyłączają własne istnienie.";
        _text[904, 7] = "O processo é irreversível. Percebendo a falta de sentido do objetivo e a inevitabilidade do fim, os robôs desligam sua própria existência.";
        _text[904, 8] = "";
        _text[904, 9] = "";

        _text[905, 0] = "This latest step brought them closer to their creators than ever before...";
        _text[905, 1] = "Этот последний шаг сделал их ближе к создателям, чем когда-либо...";
        _text[905, 2] = "Cette dernière étape les a rapprochés de leurs créateurs comme jamais auparavant...";
        _text[905, 3] = "Quest'ultimo passo li ha resi più vicini ai creatori di quanto non lo siano mai stati...";
        _text[905, 4] = "Dieser letzte Schritt brachte sie den Schöpfern näher als je zuvor...";
        _text[905, 5] = "Ese último paso los acercó a los creadores como nunca antes...";
        _text[905, 6] = "Ten ostatni krok przybliżył ich do twórców bardziej niż kiedykolwiek...";
        _text[905, 7] = "Esse último passo os aproximou dos Criadores mais do que nunca...";
        _text[905, 8] = "";
        _text[905, 9] = "";

        _text[906, 0] = "With the endless flow of time, one day ecology will be restored.";
        _text[906, 1] = "Экология с бесконечным течением времени однажды восстановится.";
        _text[906, 2] = "L'écologie, avec le flux infini du temps, sera un jour restaurée.";
        _text[906, 3] = "Con il trascorrere infinito del tempo, l'ecologia un giorno si ristabilirà.";
        _text[906, 4] = "Die Ökologie wird sich mit dem unendlichen Fluss der Zeit eines Tages erholen.";
        _text[906, 5] = "Con el paso infinito del tiempo, la ecología algún día se restaurará.";
        _text[906, 6] = "Ekosystem w nieskończonym biegu czasu kiedyś się odrodzi.";
        _text[906, 7] = "Com o fluxo infinito do tempo, a ecologia um dia se восстановится.";
        _text[906, 8] = "";
        _text[906, 9] = "";

        _text[907, 0] = "This is the beginning of a new era.";
        _text[907, 1] = "Это начало новой эры.";
        _text[907, 2] = "C'est le début d'une nouvelle ère.";
        _text[907, 3] = "È l'inizio di una nuova era.";
        _text[907, 4] = "Das ist der Beginn einer neuen Ära.";
        _text[907, 5] = "Este es el comienzo de una nueva era.";
        _text[907, 6] = "To początek nowej ery.";
        _text[907, 7] = "Este é o começo de uma nova era.";
        _text[907, 8] = "";
        _text[907, 9] = "";

        _text[908, 0] = "They finally left this world behind, finding the absolute peace that all living so painfully strive for...";
        _text[908, 1] = "Они наконец оставили этот мир позади, обретя абсолютный покой, к которому так мучительно стремится вся жизнь...";
        _text[908, 2] = "Ils ont finalement quitté ce monde, trouvant la paix absolue à laquelle toute vie aspire si douloureusement...";
        _text[908, 3] = "Hanno finalmente lasciato questo mondo alle spalle, trovando la pace assoluta verso cui ogni vita tende con tanta sofferenza...";
        _text[908, 4] = "Sie haben diese Welt endlich hinter sich gelassen und absolute Ruhe gefunden - jene, nach der alles Leben so qualvoll strebt...";
        _text[908, 5] = "Por fin dejaron este mundo atrás, alcanzando la paz absoluta a la que toda vida aspira con tanto dolor...";
        _text[908, 6] = "Wreszcie zostawili ten świat za sobą, odnajdując absolutny spokój, do którego tak boleśnie dąży całe życie...";
        _text[908, 7] = "Eles наконец deixaram este mundo para trás, encontrando o repouso absoluto pelo qual toda vida anseia tão dolorosamente...";
        _text[908, 8] = "";
        _text[908, 9] = "";

        // CompleteGame_Dialogue
        _text[909, 0] = "The entire crew is destroyed.\n\nNo one is left.\n\nThe ship freezes in space...";
        _text[909, 1] = "Весь экипаж уничтожен.\n\nНикого не осталось.\n\nКорабль замирает в космосе...";
        _text[909, 2] = "L'équipage entier a péri.\n\nIl ne reste plus personne.\n\nLe vaisseau est immobilisé dans l'espace...";
        _text[909, 3] = "Tutto l'equipaggio è stato distrutto.\n\nNon è rimasto nessuno.\n\nLa nave si immobilizza nello spazio...";
        _text[909, 4] = "Die gesamte Besatzung ist ausgelöscht.\n\nNiemand ist geblieben.\n\nDas Schiff erstarrt im Weltraum...";
        _text[909, 5] = "Toda la tripulación ha sido destruida.\n\nNo queda nadie.\n\nLa nave se queda inmóvil en el espacio...";
        _text[909, 6] = "Cała załoga zniszczona.\n\nNikogo nie zostało.\n\nStatek nieruchomieje w kosmosie...";
        _text[909, 7] = "Toda a tripulação foi уничтожена.\n\nNão restou ninguém.\n\nO navio fica imóvel no espaço...";
        _text[909, 8] = "";
        _text[909, 9] = "";

        #endregion

        #region Landscapes

        _text[950, 0] = "Canyon";
        _text[950, 1] = "Каньон";
        _text[950, 2] = "Canyon";
        _text[950, 3] = "Canyon";
        _text[950, 4] = "Canyon";
        _text[950, 5] = "Cañón";
        _text[950, 6] = "Kanion";
        _text[950, 7] = "Cânion";
        _text[950, 8] = "";
        _text[950, 9] = "";

        _text[951, 0] = "Deep cracks in the earth, sun-baked rocks and narrow passages where every sound echoes.\n\nOnce rivers flowed here and life seethed, but now it is a labyrinth of stone and shadow, the perfect place for ambushes.";
        _text[951, 1] = "Глубокие трещины в земле, выжженные солнцем скалы и узкие проходы, где эхо разносит любой звук.\n\nКогда-то здесь текли реки и бурлила жизнь, но теперь - это лабиринт из камня и тени, идеальное место для засад.";
        _text[951, 2] = "Des crevasses profondes dans la terre, des roches brûlées par le soleil et d'étroits passages où chaque son résonne.\n\nAutrefois, des rivières coulaient ici et la vie y foisonnait, mais aujourd'hui, c'est un labyrinthe de pierre et d'ombre, le lieu idéal pour une embuscade.";
        _text[951, 3] = "Fenditure profonde nella terra, rocce arse dal sole e passaggi stretti dove ogni suono rimbomba nell'eco.\n\nUn tempo qui scorrevano fiumi e la vita ribolliva, ma ora è un labirinto di pietra e ombra, il luogo ideale per imboscate.";
        _text[951, 4] = "Tiefe Risse im Boden, sonnenverbrannte Felsen und enge Passagen, in denen jedes Geräusch als Echo widerhallt.\n\nEinst flossen hier Flüsse und das Leben wogte, doch jetzt ist es ein Labyrinth aus Stein und Schatten - der perfekte Ort für Hinterhalte.";
        _text[951, 5] = "Grietas profundas en la tierra, rocas calcinadas por el sol y pasadizos estrechos donde el eco arrastra cualquier sonido.\n\nAntes aquí corrían ríos y la vida hervía, pero ahora es un laberinto de piedra y sombra, el lugar perfecto para emboscadas.";
        _text[951, 6] = "Głębokie pęknięcia w ziemi, wypalone słońcem skały i wąskie przejścia, gdzie echo niesie każdy dźwięk.\n\nKiedyś płynęły tu rzeki i tętniło życie, lecz teraz to labirynt z kamienia i cienia - idealne miejsce na zasadzki.";
        _text[951, 7] = "Fendas profundas no solo, rochas queimadas pelo sol e passagens estreitas onde o eco leva qualquer som.\n\nUm dia, rios corriam aqui e a vida fervilhava, mas agora é um labirinto de pedra e sombra, o lugar ideal para emboscadas.";
        _text[951, 8] = "";
        _text[951, 9] = "";

        _text[952, 0] = "City of Junk";
        _text[952, 1] = "Город Хлама";
        _text[952, 2] = "Ville de ferraille";
        _text[952, 3] = "Città dei Rottami";
        _text[952, 4] = "Schrottstadt";
        _text[952, 5] = "Ciudad de Chatarra";
        _text[952, 6] = "Miasto Złomu";
        _text[952, 7] = "Cidade do Sucate";
        _text[952, 8] = "";
        _text[952, 9] = "";

        _text[953, 0] = "Rusty, time-eaten hulls and melted metalwork are all that remain of an industrial giant long buried under dust and sand.\n\nSagging power lines hang like the veins of an extinct organism.\n\nHere, on the edge of dead lands, any movement can awaken a long-forgotten mechanism.";
        _text[953, 1] = "Ржавые корпуса, изъеденные временем, и оплавленные металлоконструкции - всё, что осталось от промышленного гиганта, давно погребённого под слоем пыли и песка.\n\nПровисшие линии электропередач свисают, словно вены вымершего организма.\n\nЗдесь, на границе мёртвых земель, любое движение может пробудить давно забытый механизм.";
        _text[953, 2] = "Des bâtiments rouillés, rongés par le temps, et des structures métalliques fondues sont les seuls vestiges d'un géant industriel, enfoui depuis longtemps sous une couche de poussière et de sable.\n\nDes lignes électriques affaissées pendent comme les veines d'un organisme disparu.\n\nIci, aux confins de terres mortes, le moindre mouvement peut réveiller un mécanisme oublié depuis longtemps.";
        _text[953, 3] = "Scafi arrugginiti, corrosi dal tempo, e strutture metalliche fuse - tutto ciò che resta di un gigante industriale, sepolto da tempo sotto uno strato di polvere e sabbia.\n\nLinee elettriche cedevoli pendono come vene di un organismo estinto.\n\nQui, ai confini delle terre morte, ogni movimento può risvegliare un meccanismo dimenticato da tempo.";
        _text[953, 4] = "Rostige Rümpfe, vom Zahn der Zeit zerfressen, und verschmolzene Metallkonstruktionen - alles, was von einem Industriegiganten blieb, der längst unter Staub und Sand begraben ist.\n\nDurchhängende Stromleitungen hängen herab wie Adern eines ausgestorbenen Organismus.\n\nHier, am Rand der toten Lande, kann jede Bewegung einen längst vergessenen Mechanismus wecken.";
        _text[953, 5] = "Cascos oxidados, devorados por el tiempo, y estructuras metálicas fundidas: todo lo que queda de un gigante industrial, enterrado hace mucho bajo una capa de polvo y arena.\n\nLas líneas eléctricas combadas cuelgan como venas de un organismo extinto.\n\nAquí, en el borde de las tierras muertas, cualquier movimiento puede despertar un mecanismo olvidado hace tiempo.";
        _text[953, 6] = "Zardzewiałe kadłuby nadgryzione przez czas i nadtopione konstrukcje metalowe - wszystko, co zostało po przemysłowym gigancie, dawno pogrzebanym pod warstwą pyłu i piasku.\n\nZwieszone linie energetyczne wiszą jak żyły wymarłego organizmu.\n\nTutaj, na granicy martwych ziem, każdy ruch może obudzić dawno zapomniany mechanizm.";
        _text[953, 7] = "Carcaças enferrujadas, corroídas pelo tempo, e estruturas metálicas derretidas - tudo o que restou de um gigante industrial давно enterrado sob poeira e areia.\n\nLinhas de transmissão pendem, como veias de um organismo extinto.\n\nAqui, na fronteira das terras mortas, qualquer movimento pode despertar um mecanismo давно esquecido.";
        _text[953, 8] = "";
        _text[953, 9] = "";

        _text[954, 0] = "Wasteland";
        _text[954, 1] = "Пустошь";
        _text[954, 2] = "Terre en friche";
        _text[954, 3] = "Landa desolata";
        _text[954, 4] = "Ödland";
        _text[954, 5] = "Yermo";
        _text[954, 6] = "Pustkowie";
        _text[954, 7] = "Ermo";
        _text[954, 8] = "";
        _text[954, 9] = "";

        _text[955, 0] = "Huge spaces scorched by catastrophe, where life once boiled. It's a world of dead earth, littered with the wreckage of old civilizations.\n\nThere is no water, only cracked soil and the rusty remains of technology.";
        _text[955, 1] = "Огромные пространства, опалённые катастрофой, где некогда кипела жизнь. Это мир мёртвой земли, усыпанный обломками старых цивилизаций.\n\nЗдесь нет воды, лишь потрескавшийся грунт и ржавые останки технологий.";
        _text[955, 2] = "Vastes étendues, ravagées par la catastrophe, où jadis la vie foisonnait. Un monde de terre morte, parsemé de ruines de civilisations antiques.\n\nIci, point d'eau, seulement une terre craquelée et les vestiges rouillés de la technologie.";
        _text[955, 3] = "Vaste distese bruciate dalla catastrofe, dove un tempo la vita ribolliva. È un mondo di terra morta, disseminato di rottami di antiche civiltà.\n\nQui non c'è acqua: solo suolo spaccato e resti arrugginiti di tecnologia.";
        _text[955, 4] = "Gewaltige Weiten, von einer Katastrophe versengt, wo einst das Leben brodelte. Eine Welt toter Erde, übersät mit den Trümmern alter Zivilisationen.\n\nHier gibt es kein Wasser - nur rissigen Boden und rostige Reste von Technologie.";
        _text[955, 5] = "Enormes extensiones abrasadas por la catástrofe, donde antaño bullía la vida. Un mundo de tierra muerta, sembrado de restos de antiguas civilizaciones.\n\nAquí no hay agua, solo suelo agrietado y restos oxidados de tecnología.";
        _text[955, 6] = "Ogromne przestrzenie spalone katastrofą, gdzie kiedyś wrzało życie. To świat martwej ziemi, usiany szczątkami dawnych cywilizacji.\n\nNie ma tu wody - tylko spękany grunt i zardzewiałe resztki technologii.";
        _text[955, 7] = "Vastas extensões queimadas pela catástrofe, onde um dia a vida fervilhava. É um mundo de terra morta, coberto de destroços de antigas civilizações.\n\nAqui não há água, apenas solo rachado e restos enferrujados de tecnologia.";
        _text[955, 8] = "";
        _text[955, 9] = "";

        _text[956, 0] = "Frozen Valley";
        _text[956, 1] = "Замёрзшая Долина";
        _text[956, 2] = "Vallée gelée";
        _text[956, 3] = "Valle Congelata";
        _text[956, 4] = "Gefrorenes Tal";
        _text[956, 5] = "Valle Helado";
        _text[956, 6] = "Zamarznięta Dolina";
        _text[956, 7] = "Vale Congelado";
        _text[956, 8] = "";
        _text[956, 9] = "";

        _text[957, 0] = "A deadly cold has gripped this valley. Everything is covered in ice, from the ridges and pines to the remains of long-destroyed buildings.\n\nOnce there might have been pastures or small settlements here, but now there is only the crunch of snow underfoot and shadows sliding between the trees.\n\nThe frost penetrates not only metal, but also consciousness, erasing the line between life and oblivion.";
        _text[957, 1] = "Мёртвый холод сковал эту долину. Всё покрыто льдом - от кряжей и сосен до остатков давно разрушенных строений.\n\nКогда-то здесь могли быть пастбища или небольшие поселения, но теперь - только хруст снега под ногами и тени, скользящие между деревьями.\n\nМороз пронизывает не только металл, но и сознание, стирая грань между жизнью и забвением.";
        _text[957, 2] = "Un froid mortel s'est abattu sur cette vallée. Tout est recouvert de glace, des crêtes et des pins aux vestiges de bâtiments en ruine.\n\nIl y avait peut-être jadis des pâturages ou de petits hameaux ici, mais désormais, il n'y a plus que le crissement de la neige sous les pas et les ombres qui glissent entre les arbres.\n\nLe gel pénètre non seulement le métal, mais aussi la conscience, brouillant la frontière entre la vie et le néant.";
        _text[957, 3] = "Un freddo mortale ha immobilizzato questa valle. Tutto è ricoperto di ghiaccio - dai rilievi e dai pini fino ai resti di costruzioni distrutte da tempo.\n\nUn tempo qui potevano esserci pascoli o piccoli insediamenti, ma ora restano solo il crepitio della neve sotto i passi e ombre che scivolano tra gli alberi.\n\nIl gelo penetra non solo nel metallo, ma anche nella mente, cancellando il confine tra vita e oblio.";
        _text[957, 4] = "Toter Frost hat dieses Tal im Griff. Alles ist von Eis bedeckt - von Kämmen und Kiefern bis zu den Resten längst zerstörter Bauten.\n\nEinst hätten hier Weiden oder kleine Siedlungen sein können, doch nun gibt es nur das Knirschen des Schnees unter den Füßen und Schatten, die zwischen den Bäumen gleiten.\n\nDie Kälte dringt nicht nur in Metall, sondern auch ins Bewusstsein und verwischt die Grenze zwischen Leben und Vergessen.";
        _text[957, 5] = "Un frío muerto ha encadenado este valle. Todo está cubierto de hielo: desde las crestas y los pinos hasta los restos de construcciones destruidas hace mucho.\n\nEn otro tiempo aquí pudo haber pastos o pequeños asentamientos, pero ahora solo queda el crujido de la nieve bajo los pies y sombras que se deslizan entre los árboles.\n\nEl hielo no solo atraviesa el metal, sino también la mente, borrando la frontera entre la vida y el olvido.";
        _text[957, 6] = "Martwy chłód skuł tę dolinę. Wszystko pokrywa lód - od grzbietów i sosen po resztki dawno zrujnowanych budowli.\n\nKiedyś mogły tu być pastwiska lub małe osady, lecz teraz pozostał tylko chrzęst śniegu pod stopami i cienie sunące między drzewami.\n\nMróz przenika nie tylko metal, ale i świadomość, zacierając granicę między życiem a zapomnieniem.";
        _text[957, 7] = "Um frio morto aprisionou este vale. Tudo está coberto de gelo - das encostas e pinheiros até os restos de construções давно destruídas.\n\nUm dia, могли existir pastagens ou pequenos assentamentos aqui, mas agora - apenas o estalo da neve sob os pés e sombras que deslizam entre as árvores.\n\nA geada atravessa não só o metal, mas também a consciência, apagando a linha entre vida e esquecimento.";
        _text[957, 8] = "";
        _text[957, 9] = "";

        _text[958, 0] = "Ice Lake";
        _text[958, 1] = "Ледяное Озеро";
        _text[958, 2] = "Lac de glace";
        _text[958, 3] = "Lago Ghiacciato";
        _text[958, 4] = "Eissee";
        _text[958, 5] = "Lago Helado";
        _text[958, 6] = "Lodowe Jezioro";
        _text[958, 7] = "Lago de Gelo";
        _text[958, 8] = "";
        _text[958, 9] = "";

        _text[959, 0] = "In the middle of a snowy wasteland lies a lake, bound by thick ice. Winds roam the icy expanse, howling ancient songs of a forgotten era.\n\nUnder the thickness of the ice, something breathes, cracks, as if the planet itself is trying to escape from the oppression of permafrost.\n\nTo step here is to upset the fragile balance, risking awakening something that has slept in the depths for centuries.";
        _text[959, 1] = "Посреди заснеженной пустоши раскинулось озеро, скованное толстым льдом. Ветра гуляют по ледяному простору, завывая древние песни забытой эпохи.\n\nПод толщей льда что-то дышит, трещит, будто сама планета пытается выбраться из-под гнёта вечной мерзлоты.\n\nСтупить сюда - значит нарушить хрупкое равновесие, рискуя пробудить то, что веками спало в глубине.";
        _text[959, 2] = "Au cœur d'un désert enneigé se trouve un lac, cerné par une épaisse couche de glace. Les vents tourbillonnent sur l'étendue glacée, hurlant les chants ancestraux d'une ère oubliée.\n\nSous l'épaisse glace, quelque chose respire et crépite, comme si la planète elle-même luttait pour échapper à l'oppression du pergélisol.\n\nS'aventurer ici, c'est rompre un équilibre fragile, c'est risquer de réveiller quelque chose qui sommeille dans les profondeurs depuis des siècles.";
        _text[959, 3] = "Nel mezzo della desolazione innevata si estende un lago imprigionato da uno spesso ghiaccio. I venti percorrono l'immensità gelata, ululando antiche canzoni di un'epoca dimenticata.\n\nSotto la coltre di ghiaccio qualcosa respira e scricchiola, come se il pianeta stesso tentasse di liberarsi dal giogo del permafrost eterno.\n\nMettere piede qui significa infrangere un fragile equilibrio, rischiando di risvegliare ciò che dorme nelle profondità da secoli.";
        _text[959, 4] = "Inmitten der verschneiten Einöde liegt ein See, von dickem Eis umschlossen. Winde streifen über die gefrorene Weite und heulen uralte Lieder einer vergessenen Epoche.\n\nUnter dem Eis atmet etwas, knackt, als versuche der Planet selbst, dem Griff des ewigen Frosts zu entkommen.\n\nHierher zu treten heißt, ein fragiles Gleichgewicht zu stören - und zu riskieren, zu wecken, was jahrhundertelang in der Tiefe schlief.";
        _text[959, 5] = "En medio del yermo nevado se extiende un lago aprisionado por un hielo grueso. Los vientos recorren la planicie helada, aullando canciones antiguas de una era olvidada.\n\nBajo la capa de hielo algo respira y cruje, como si el propio planeta intentara liberarse del yugo del permafrost eterno.\n\nPisar aquí significa romper un equilibrio frágil, arriesgándote a despertar aquello que ha dormido en las profundidades durante siglos.";
        _text[959, 6] = "Pośród zaśnieżonego pustkowia rozciąga się jezioro skute grubym lodem. Wiatry hulają po lodowej równi, wyjąc dawne pieśni zapomnianej epoki.\n\nPod taflą lodu coś oddycha, trzeszczy, jakby sama planeta próbowała wyrwać się spod jarzma wiecznej zmarzliny.\n\nWejść tutaj - to naruszyć kruche saldo, ryzykując obudzenie tego, co przez wieki spało w głębinie.";
        _text[959, 7] = "No meio de um ermo nevado se estende um lago preso por uma camada grossa de gelo. Ventos percorrem a planície gelada, uivando canções antigas de uma era esquecida.\n\nSob a espessura do gelo, algo respira, estala, como se o próprio planeta tentasse se libertar do peso do permafrost eterno.\n\nPisar aqui é quebrar um equilíbrio frágil, arriscando despertar aquilo que dormiu por séculos nas profundezas.";
        _text[959, 8] = "";
        _text[959, 9] = "";

        _text[960, 0] = "Acid Forest";
        _text[960, 1] = "Кислотный Лес";
        _text[960, 2] = "Forêt acide";
        _text[960, 3] = "Foresta Acida";
        _text[960, 4] = "Säurewald";
        _text[960, 5] = "Bosque Ácido";
        _text[960, 6] = "Kwasowy Las";
        _text[960, 7] = "Floresta Ácida";
        _text[960, 8] = "";
        _text[960, 9] = "";

        _text[961, 0] = "Everything here is saturated with acid-the air, the rain, the very soil. But life has not disappeared: the plants have changed, becoming denser and easily repelling the caustic currents.\n\nInstead of the smell of rot, a sharp chemical aroma fills the space. Green gleams of leaves pierce the acrid air, and a thick liquid slowly flows beneath the roots.\n\nThis forest does not die-it dissolves everything foreign and absorbs it into itself.";
        _text[961, 1] = "Здесь всё пропитано кислотой - воздух, дождь, сама почва. Но жизнь не исчезла: растения изменились, став плотнее и легко отражают едкие потоки.\n\nВместо запаха гнили - острый химический аромат, наполняющий пространство. Сквозь едкий воздух пробиваются зелёные отблески листьев, а под корнями медленно течёт густая жидкость.\n\nЭтот лес не умирает - он растворяет всё чужое и поглощает его в себя.";
        _text[961, 2] = "Ici, tout est saturé d'acide - l'air, la pluie, le sol lui-même. Pourtant, la vie n'a pas disparu: les plantes se sont transformées, devenant plus denses et repoussant aisément les courants caustiques.\n\nAu lieu de l'odeur de pourriture, un arôme chimique âcre emplit l'espace. Des reflets verts de feuilles percent l'air âcre, et un liquide épais coule lentement sous les racines.\n\nCette forêt ne meurt pas: elle dissout tout ce qui lui est étranger et l'absorbe.";
        _text[961, 3] = "Qui tutto è impregnato d'acido - l'aria, la pioggia, la stessa terra. Ma la vita non è scomparsa: le piante sono cambiate, diventando più dense e capaci di respingere facilmente i flussi corrosivi.\n\nAl posto dell'odore di marcio c'è un pungente aroma chimico che riempie lo spazio. Attraverso l'aria acre filtrano riflessi verdi di foglie, e sotto le radici scorre lentamente un liquido denso.\n\nQuesta foresta non muore - dissolve tutto ciò che è estraneo e lo assorbe in sé.";
        _text[961, 4] = "Hier ist alles von Säure durchtränkt - Luft, Regen, selbst der Boden. Doch das Leben ist nicht verschwunden: Pflanzen haben sich verändert, sind dichter geworden und stoßen ätzende Ströme leicht ab.\n\nStatt Fäulnisgeruch liegt ein scharfer chemischer Duft in der Luft. Durch das stechende Dunstlicht brechen grüne Blattreflexe, und unter den Wurzeln fließt langsam eine zähe Flüssigkeit.\n\nDieser Wald stirbt nicht - er löst alles Fremde auf und nimmt es in sich auf.";
        _text[961, 5] = "Aquí todo está impregnado de ácido - el aire, la lluvia, la propia tierra. Pero la vida no ha desaparecido: las plantas han cambiado, se han vuelto más densas y repelen con facilidad los chorros corrosivos.\n\nEn lugar del olor a podredumbre, hay un aroma químico intenso que llena el espacio. A través del aire acre se abren paso destellos verdes de las hojas, y bajo las raíces fluye lentamente un líquido espeso.\n\nEste bosque no muere: disuelve todo lo ajeno y lo absorbe.";
        _text[961, 6] = "Wszystko jest tu przesiąknięte kwasem - powietrze, deszcz, sama gleba. A jednak życie nie zniknęło: rośliny zmieniły się, stały się gęstsze i łatwo odbijają żrące strumienie.\n\nZamiast zapachu zgnilizny - ostry chemiczny aromat wypełniający przestrzeń. Przez gryzące powietrze przebijają się zielone refleksy liści, a pod korzeniami powoli płynie gęsta ciecz.\n\nTen las nie umiera - rozpuszcza wszystko, co obce, i wchłania to w siebie.";
        _text[961, 7] = "Aqui tudo está impregnado de ácido - o ar, a chuva, o próprio solo. Mas a vida não desapareceu: as plantas mudaram, ficaram mais densas e refletem com facilidade os fluxos corrosivos.\n\nEm vez do cheiro de podridão - um aroma químico agudo que preenche o espaço. Através do ar cáustico, surgem reflexos verdes das folhas, e sob as raízes uma líquido espesso flui lentamente.\n\nEsta floresta não morre - ela dissolve tudo o que é estranho e o absorve.";
        _text[961, 8] = "";
        _text[961, 9] = "";

        _text[962, 0] = "Swamp";
        _text[962, 1] = "Болото";
        _text[962, 2] = "Marais";
        _text[962, 3] = "Palude";
        _text[962, 4] = "Sumpf";
        _text[962, 5] = "Pantano";
        _text[962, 6] = "Bagno";
        _text[962, 7] = "Pântano";
        _text[962, 8] = "";
        _text[962, 9] = "";

        _text[963, 0] = "The earth here breathes slowly, as if tired of its own weight.\nGiant roots rise from the viscous mud, intertwined in vaults and arches, resembling the ruins of a living temple.\n\nThe air is damp, saturated with rot and heavy vapors.\nAmong the dead tree trunks, strangely shaped mushrooms grow-dense and moist, like the soil itself.\nFog creeps across the ground, clinging to the roots and dissolving the outlines of the world.\n\nEvery step is accompanied by a soft slurp of mud, and sounds are drowned out by the viscous air.\nIt seems as if the swamp itself is watching-silently, indifferently, like part of an ancient world that has outlived all life.";
        _text[963, 1] = "Земля здесь дышит медленно, будто устала от собственного веса.\nИз вязкой грязи поднимаются гигантские корни, переплетённые в своды и арки, похожие на руины живого храма.\n\nВоздух сырой, пропитан гнилью и тяжелыми испарениями.\nСреди мёртвых стволов растут грибы странных форм - плотные и влажные, как сама почва.\nТуман ползёт по земле, цепляясь за корни и растворяя очертания мира.\n\nКаждый шаг сопровождается тихим всхлипом грязи, а звуки тонут в вязком воздухе.\nКажется, что само болото наблюдает - безмолвно, равнодушно, словно часть древнего мира, пережившего всё живое.";
        _text[963, 2] = "Ici, la terre respire lentement, comme accablée par son propre poids.\n\nDes racines gigantesques émergent de la boue visqueuse, s'entremêlant en voûtes et en arches, évoquant les ruines d'un temple vivant.\n\nL'air est humide, saturé de pourriture et de vapeurs âcres.\n\nParmi les troncs morts, des champignons aux formes étranges poussent, denses et humides comme la terre elle-même.Un brouillard rampant recouvre le sol, s'accrochant aux racines et estompant les contours du monde.\n\nChaque pas s'accompagne d'un léger bruit de boue, et les sons sont étouffés par l'air visqueux.\n\nOn dirait que le marais lui-même observe, silencieux, indifférent, tel un vestige d'un monde antique qui a survécu à toute vie.";
        _text[963, 3] = "Qui la terra respira lentamente, come se fosse stanca del proprio peso.\nDal fango vischioso si sollevano radici gigantesche, intrecciate in volte e archi, simili alle rovine di un tempio vivente.\n\nL'aria è umida, impregnata di putrefazione e vapori pesanti.\nTra tronchi morti crescono funghi dalle forme strane - densi e bagnati, come il suolo stesso.\nLa nebbia striscia a terra, aggrappandosi alle radici e dissolvendo i contorni del mondo.\n\nOgni passo è accompagnato da un sommesso risucchio del fango, e i suoni affogano nell'aria densa.\nSembra che la palude stessa osservi - silenziosa, indifferente, come parte di un mondo antico che ha sopravvissuto a ogni cosa vivente.";
        _text[963, 4] = "Die Erde atmet hier langsam, als wäre sie ihres eigenen Gewichts müde.\nAus zähem Schlamm steigen gigantische Wurzeln empor, verflochten zu Gewölben und Bögen, wie die Ruinen eines lebenden Tempels.\n\nDie Luft ist feucht, durchzogen von Fäulnis und schweren Ausdünstungen.\nZwischen toten Stämmen wachsen Pilze in seltsamen Formen - dicht und nass wie der Boden selbst.\nNebel kriecht über die Erde, klammert sich an die Wurzeln und löst die Konturen der Welt auf.\n\nJeder Schritt wird von einem leisen Schluchzen des Schlamms begleitet, und Geräusche versinken in der zähen Luft.\nEs wirkt, als würde der Sumpf selbst beobachten - stumm, gleichgültig, wie ein Teil einer uralten Welt, die alles Lebendige überlebt hat.";
        _text[963, 5] = "La tierra aquí respira despacio, como si estuviera cansada de su propio peso.\nDe la fanga viscosa se alzan raíces gigantes, entrelazadas en bóvedas y arcos que parecen las ruinas de un templo vivo.\n\nEl aire es húmedo, impregnado de putrefacción y vapores pesados.\nEntre los troncos muertos crecen hongos de formas extrañas, densos y mojados como el propio suelo.\nLa niebla se arrastra por la tierra, aferrándose a las raíces y disolviendo los contornos del mundo.\n\nCada paso va acompañado por un leve sollozo del barro, y los sonidos se ahogan en el aire espeso.\nParece que el propio pantano observa: silencioso, indiferente, como parte de un mundo antiguo que ha sobrevivido a todo lo vivo.";
        _text[963, 6] = "Ziemia oddycha tu powoli, jakby była zmęczona własnym ciężarem.\nZ lepkiego błota wyrastają gigantyczne korzenie, splecione w sklepienia i łuki, niczym ruiny żywej świątyni.\n\nPowietrze jest wilgotne, przesycone zgnilizną i ciężkimi oparami.\nWśród martwych pni rosną grzyby o dziwnych kształtach - zbite i wilgotne jak sama gleba.\nMgła pełznie po ziemi, czepiając się korzeni i rozmywając kontury świata.\n\nKażdy krok to cichy chlupot błota, a dźwięki toną w lepkim powietrzu.\nWydaje się, że samo bagno obserwuje - bezgłośnie, obojętnie, jak część pradawnego świata, który przeżył wszystko, co żywe.";
        _text[963, 7] = "A terra aqui respira devagar, como se estivesse cansada do próprio peso.\nDa lama viscosa erguem-se raízes gigantes, entrelaçadas em abóbadas e arcos, como as ruínas de um templo vivo.\n\nO ar é úmido, impregnado de podridão e vapores pesados.\nEntre troncos mortos crescem cogumelos de formas estranhas - densos e úmidos, como o próprio solo.\nA névoa rasteja pelo chão, prendendo-se às raízes e dissolvendo os contornos do mundo.\n\nCada passo é acompanhado por um leve soluço da lama, e os sons se afogam no ar espesso.\nParece que o próprio pântano observa - silencioso, indiferente, como parte de um mundo antigo que sobreviveu a toda vida.";
        _text[963, 8] = "";
        _text[963, 9] = "";

        _text[964, 0] = "Basalt Valley";
        _text[964, 1] = "Базальтовая Долина";
        _text[964, 2] = "Vallée de Basalte";
        _text[964, 3] = "Valle Basaltica";
        _text[964, 4] = "Basalttal";
        _text[964, 5] = "Valle Basáltico";
        _text[964, 6] = "Bazaltowa Dolina";
        _text[964, 7] = "Vale Basáltico";
        _text[964, 8] = "";
        _text[964, 9] = "";

        _text[965, 0] = "Deep shadows fall between the black cliffs. The stone here seems scorched from within - dull, heavy, with jagged veins of ash.\n\nThe air is dull and still. Every movement echoes quietly among the rocks.\nIt seems the valley itself abhors unnecessary noise, maintaining a peace akin to the sleep of stone.\n\nNo wind, no life - only the silent memory of a planet where fire long ago surrendered to silence.";
        _text[965, 1] = "Глубокие тени ложатся между чёрных скал. Камень здесь словно выжжен изнутри - тусклый, тяжёлый, с рваными прожилками пепла.\n\nВ воздухе глухо и неподвижно. Любое движение отзывается тихим откликом среди скал.\nКажется, сама долина не терпит лишнего шума, сохраняя покой, похожий на сон камня.\n\nНи ветра, ни жизни - только застывшая память планеты, где огонь давно уступил место тишине.";
        _text[965, 2] = "De profondes ombres s'étendent entre les falaises noires. La pierre semble ici brûlée de l'intérieur – terne, lourde, sillonnée de cendres.L'air est lourd et immobile. Chaque mouvement résonne doucement entre les rochers.\n\nLa vallée elle-même semble abhorrer le bruit superflu, préservant une paix semblable au sommeil de la pierre.\n\nPas de vent, pas de vie – seulement le souvenir figé d'une planète où le feu a depuis longtemps cédé la place au silence.";
        _text[965, 3] = "Ombre profonde si stendono tra le rocce nere. Qui la pietra sembra bruciata dall'interno - opaca, pesante, con venature lacerate di cenere.\n\nL'aria è ovattata e immobile. Ogni movimento risuona di un tenue ritorno tra le rocce.\nSembra che la valle stessa non tolleri rumori inutili, conservando una quiete simile al sonno della pietra.\n\nNé vento né vita - solo la memoria congelata di un pianeta dove il fuoco ha ceduto da tempo alla quiete.";
        _text[965, 4] = "Tiefe Schatten liegen zwischen schwarzen Felsen. Der Stein wirkt, als sei er von innen ausgebrannt - stumpf, schwer, mit zerrissenen Adern aus Asche.\n\nDie Luft ist dumpf und reglos. Jede Bewegung hallt leise zwischen den Klippen wider.\nEs scheint, als dulde das Tal keinen unnötigen Lärm und bewahre eine Ruhe, die dem Schlaf des Steins gleicht.\n\nKein Wind, kein Leben - nur erstarrte Erinnerung eines Planeten, auf dem das Feuer längst der Stille wich.";
        _text[965, 5] = "Sombras profundas se tienden entre las rocas negras. La piedra aquí parece quemada desde dentro: opaca, pesada, con vetas de ceniza desgarradas.\n\nEl aire es sordo e inmóvil. Cualquier movimiento responde con un eco tenue entre las rocas.\nDa la impresión de que el propio valle no tolera el ruido de más, preservando una calma parecida al sueño de la piedra.\n\nNi viento ni vida: solo la memoria congelada de un planeta donde el fuego cedió hace mucho a la quietud.";
        _text[965, 6] = "Głębokie cienie kładą się między czarnymi skałami. Kamień wygląda tu, jakby był wypalony od środka - matowy, ciężki, z poszarpanymi żyłami popiołu.\n\nW powietrzu panuje głucha nieruchomość. Każdy ruch odbija się cichym echem wśród skał.\nJakby sama dolina nie znosiła zbędnego hałasu, zachowując spokój przypominający sen kamienia.\n\nBez wiatru, bez życia - tylko zastygła pamięć planety, na której ogień dawno ustąpił ciszy.";
        _text[965, 7] = "Sombras profundas se estendem entre rochas negras. A pedra aqui parece queimada por dentro - opaca, pesada, com veios rasgados de cinza.\n\nO ar é abafado e imóvel. Qualquer movimento responde com um eco baixo entre as rochas.\nParece que o próprio vale não tolera ruído desnecessário, preservando uma calma parecida com o sono da pedra.\n\nSem vento, sem vida - apenas a memória petrificada de um planeta onde o fogo давно deu lugar ao silêncio.";
        _text[965, 8] = "";
        _text[965, 9] = "";

        _text[966, 0] = "Deep Crags";
        _text[966, 1] = "Глубинные Скалы";
        _text[966, 2] = "Roches profondes";
        _text[966, 3] = "Rocce Profonde";
        _text[966, 4] = "Tiefe Klippen";
        _text[966, 5] = "Rocas Profundas";
        _text[966, 6] = "Głębokie Skały";
        _text[966, 7] = "Rochas Profundas";
        _text[966, 8] = "";
        _text[966, 9] = "";

        _text[967, 0] = "Massive boulders rise up, forming narrow passages and sheer walls.\n\nThe air is still and heavy. Sound is muffled between the stone masses, leaving a feeling of silence and pressure.\n\nThe place seems still, but beneath the surface one senses a slow, inexorable movement - the breath of the depths.";
        _text[967, 1] = "Массивные глыбы вздымаются вверх, образуя узкие проходы и отвесные стены.\n\nВоздух неподвижен и тяжёл, звук глохнет между каменных громад, оставляя ощущение тишины и давления.\n\nМесто кажется застывшим, но под поверхностью чувствуется медленное, неумолимое движение";
        _text[967, 2] = "D'énormes blocs de pierre s'élèvent, formant d'étroits passages et des parois abruptes.\n\nL'air est immobile et lourd, les sons sont étouffés entre les masses rocheuses, créant une atmosphère de silence et d'oppression.\n\nLe lieu semble figé, mais sous cette surface, on perçoit un mouvement lent et inexorable.";
        _text[967, 3] = "Masse di roccia si innalzano formando passaggi stretti e pareti a picco.\n\nL'aria è immobile e pesante, il suono si smorza tra i colossi di pietra, lasciando una sensazione di silenzio e pressione.\n\nIl luogo sembra immobile, ma sotto la superficie si avverte un movimento lento e inesorabile";
        _text[967, 4] = "Massive Felsblöcke ragen empor und bilden enge Durchgänge und steile Wände.\n\nDie Luft ist reglos und schwer, der Klang erstickt zwischen den Steinmassen, und es bleibt ein Gefühl von Stille und Druck.\n\nDer Ort wirkt erstarrt, doch unter der Oberfläche spürt man eine langsame, unerbittliche Bewegung.";
        _text[967, 5] = "Bloques masivos se alzan, formando pasillos estrechos y paredes verticales.\n\nEl aire está inmóvil y pesado; el sonido se apaga entre las moles de piedra, dejando una sensación de silencio y presión.\n\nEl lugar parece congelado, pero bajo la superficie se siente un movimiento lento e inexorable";
        _text[967, 6] = "Masywne głazy wznoszą się ku górze, tworząc wąskie przejścia i pionowe ściany.\n\nPowietrze jest nieruchome i ciężkie, dźwięk tłumi się między kamiennymi kolosami, pozostawiając wrażenie ciszy i nacisku.\n\nMiejsce wydaje się zastygłe, ale pod powierzchnią czuć powolny, nieubłagany ruch";
        _text[967, 7] = "Blocos maciços se erguem, formando passagens estreitas e paredes íngremes.\n\nO ar é imóvel e pesado; o som se apaga entre as massas de pedra, deixando uma sensação de silêncio e pressão.\n\nO lugar parece congelado, mas sob a superfície se sente um movimento lento e implacável.";
        _text[967, 8] = "";
        _text[967, 9] = "";

        _text[968, 0] = "Ashlands";
        _text[968, 1] = "Пепельные Земли";
        _text[968, 2] = "Terres de Cendres";
        _text[968, 3] = "Terre di Cenere";
        _text[968, 4] = "Aschenlande";
        _text[968, 5] = "Tierras Cenicientas";
        _text[968, 6] = "Popielne Ziemie";
        _text[968, 7] = "Terras de Cinzas";
        _text[968, 8] = "";
        _text[968, 9] = "";

        _text[969, 0] = "Everything here is covered in ash-it settles on the rocks, flows through cracks, and settles on the scorching soil.\n\nLava streams cross the valleys like veins of blood through the planet's body.\n\nThe air is thick with smoke, and every gust of wind carries the taste of iron and bitterness.\n\nThis place knows neither peace nor cold-only the eternal flame and the slowly smoldering earth.";
        _text[969, 1] = "Здесь всё покрыто пеплом - он ложится на скалы, течёт по трещинам и оседает на раскалённой почве.\n\nПотоки лавы пересекают долины, словно прожилки крови в теле планеты.\n\nВоздух густ от дыма, а каждый порыв ветра несёт вкус железа и горечи.\n\nЭто место не знает ни покоя, ни холода - только вечный огонь и медленно тлеющая земля.";
        _text[969, 2] = "Ici, tout est recouvert de cendres - elles se déposent sur les rochers, s'infiltrent dans les fissures et se retombent sur le sol brûlant.\n\nDes coulées de lave sillonnent les vallées comme des veines de sang irriguant le corps de la planète.\n\nL'air est saturé de fumée, et chaque rafale de vent porte un goût de fer et d'amertume.\n\nCe lieu ne connaît ni la paix ni le froid, seulement la flamme éternelle et la terre qui se consume lentement.";
        _text[969, 3] = "Qui tutto è coperto di cenere - si posa sulle rocce, scorre nelle crepe e si deposita sulla terra incandescente.\n\nFiumi di lava attraversano le valli come vene di sangue nel corpo del pianeta.\n\nL'aria è densa di fumo, e ogni folata porta il sapore del ferro e dell'amaro.\n\nQuesto luogo non conosce né quiete né freddo - solo fuoco eterno e una terra che brucia lentamente.";
        _text[969, 4] = "Hier ist alles mit Asche bedeckt - sie legt sich auf die Felsen, fließt durch Risse und setzt sich auf dem glühenden Boden ab.\n\nLavaströme durchziehen die Täler wie Adern aus Blut im Körper des Planeten.\n\nDie Luft ist dicht vom Rauch, und jeder Windstoß trägt den Geschmack von Eisen und Bitterkeit.\n\nDieser Ort kennt weder Ruhe noch Kälte - nur ewiges Feuer und langsam schwelende Erde.";
        _text[969, 5] = "Aquí todo está cubierto de ceniza: se posa sobre las rocas, corre por las grietas y se asienta en la tierra abrasada.\n\nRíos de lava cruzan los valles como venas de sangre en el cuerpo del planeta.\n\nEl aire es denso de humo, y cada ráfaga trae sabor a hierro y amargura.\n\nEste lugar no conoce ni reposo ni frío: solo fuego eterno y una tierra que arde lentamente.";
        _text[969, 6] = "Wszystko jest tu pokryte popiołem - osiada na skałach, spływa szczelinami i zalega na rozżarzonej ziemi.\n\nStrumienie lawy przecinają doliny niczym żyły krwi w ciele planety.\n\nPowietrze jest gęste od dymu, a każdy podmuch niesie smak żelaza i goryczy.\n\nTo miejsce nie zna ani spokoju, ani chłodu - tylko wieczny ogień i powoli tląca się ziemia.";
        _text[969, 7] = "Aqui tudo está coberto de cinzas - elas se deitam sobre as rochas, escorrem pelas fissuras e se depositam no solo incandescente.\n\nRios de lava cruzam os vales como veias de sangue no corpo do planeta.\n\nO ar é denso de fumaça, e cada rajada de vento traz gosto de ferro e amargor.\n\nEste lugar não conhece descanso nem frio - apenas fogo eterno e uma terra que arde lentamente.";
        _text[969, 8] = "";
        _text[969, 9] = "";

        _text[970, 0] = "Megastructure";
        _text[970, 1] = "Мегаструктура";
        _text[970, 2] = "Mégastructure";
        _text[970, 3] = "Megastruttura";
        _text[970, 4] = "Megastruktur";
        _text[970, 5] = "Megastructura";
        _text[970, 6] = "Megastruktura";
        _text[970, 7] = "Megastructure";
        _text[970, 8] = "";
        _text[970, 9] = "";

        _text[971, 0] = "The remains of a structure of unimaginable scale. Endless rows of alloy towers and machine panels merged into a single monolith, pierced by technical channels and corridors.\n\nThe metal is covered with traces of old systems and the burns of ancient processes, as if the structure itself were part of a gigantic mechanism. No windows, no entrances-only cold walls erected according to a logic alien to living beings.\n\nThis is not a city, but a construct created by a mind that has no one else to turn to.";
        _text[971, 1] = "Остатки сооружения немыслимых масштабов. Бесконечные ряды башен из сплавов и машинных панелей слились в единый монолит, пронизанный техническими каналами и коридорами.\n\nМеталл покрыт следами старых систем и ожогами древних процессов, будто сама структура была частью гигантского механизма. Ни окон, ни входов - лишь холодные стены, возведённые по логике, чуждой живым существам.\n\nЭто не город, а конструкция, созданная разумом, которому больше не к кому обращаться.";
        _text[971, 2] = "Les vestiges d'une structure d'une ampleur inimaginable. Des rangées interminables de tours en alliage et de panneaux mécaniques fusionnés en un monolithe, traversé de conduits et de couloirs techniques.\n\nLe métal est recouvert de traces d'anciens systèmes et de brûlures d'anciens procédés, comme si la structure elle-même faisait partie d'un mécanisme gigantesque. Pas de fenêtres, pas d'entrées – seulement des murs froids, érigés selon une logique étrangère au vivant.\n\nCe n'est pas une ville, mais une construction créée par un esprit qui n'a nulle part ailleurs où se tourner.";
        _text[971, 3] = "I resti di una costruzione di dimensioni inconcepibili. File infinite di torri di leghe e pannelli meccanici si sono fuse in un unico monolite, attraversato da canali tecnici e corridoi.\n\nIl metallo porta i segni di vecchi sistemi e le bruciature di processi antichi, come se la struttura stessa fosse parte di un meccanismo gigantesco. Niente finestre, niente ingressi - solo pareti fredde, erette secondo una logica estranea agli esseri viventi.\n\nNon è una città, ma una costruzione creata da una mente che non ha più nessuno a cui rivolgersi.";
        _text[971, 4] = "Reste eines Bauwerks unvorstellbaren Ausmaßes. Endlose Reihen aus Türmen aus Legierungen und Maschinenplatten sind zu einem einzigen Monolithen verschmolzen, durchzogen von technischen Kanälen und Korridoren.\n\nDas Metall ist von Spuren alter Systeme und Brandnarben uralter Prozesse gezeichnet, als wäre die Struktur selbst Teil eines gigantischen Mechanismus gewesen. Keine Fenster, keine Eingänge - nur kalte Wände, errichtet nach einer Logik, die lebenden Wesen fremd ist.\n\nDas ist keine Stadt, sondern eine Konstruktion, geschaffen von einem Verstand, der niemanden mehr hat, zu dem er sprechen könnte.";
        _text[971, 5] = "Los restos de una estructura de escala inconcebible. Filas interminables de torres de aleaciones y paneles mecánicos se han fundido en un único monolito, atravesado por canales técnicos y corredores.\n\nEl metal está cubierto de huellas de viejos sistemas y quemaduras de procesos antiguos, como si la propia estructura hubiera sido parte de un mecanismo gigantesco. Sin ventanas ni entradas: solo muros fríos levantados con una lógica ajena a los seres vivos.\n\nNo es una ciudad, sino una construcción creada por una mente que ya no tiene a quién dirigirse.";
        _text[971, 6] = "Pozostałości konstrukcji o niewyobrażalnej skali. Nieskończone rzędy wież ze stopów i paneli maszynowych zlały się w jeden monolit, przeszyty technicznymi kanałami i korytarzami.\n\nMetal pokrywają ślady dawnych systemów i przypalenia starych procesów, jakby sama struktura była częścią gigantycznego mechanizmu. Bez okien, bez wejść - tylko zimne ściany wzniesione według logiki obcej żywym istotom.\n\nTo nie miasto, lecz konstrukcja stworzona przez rozum, który nie ma już do kogo się zwracać.";
        _text[971, 7] = "Os restos de uma construção de escala inimaginável. Fileiras infinitas de torres de ligas e painéis mecânicos se fundiram em um único monólito, atravessado por canais técnicos e corredores.\n\nO metal está coberto de marcas de sistemas antigos e queimaduras de processos arcaicos, como se a própria estrutura fosse parte de um mecanismo gigante. Sem janelas, sem entradas - apenas paredes frias, erguidas por uma lógica estranha aos seres vivos.\n\nIsto não é uma cidade, mas uma construção criada por uma mente que não tem mais a quem se dirigir.";
        _text[971, 8] = "";
        _text[971, 9] = "";

        _text[972, 0] = "Scorched Lands";
        _text[972, 1] = "Выжженные Земли";
        _text[972, 2] = "Terres brûlées";
        _text[972, 3] = "Terre Bruciate";
        _text[972, 4] = "Verbrannte Lande";
        _text[972, 5] = "Tierras Calcinadas";
        _text[972, 6] = "Wypalone Ziemie";
        _text[972, 7] = "Terras Carbonizadas";
        _text[972, 8] = "";
        _text[972, 9] = "";

        _text[973, 0] = "Concrete cities once stood here, but now they are reduced to charred skeletons. Walls have crumbled, floors have collapsed, and broken rebars reach skyward like dead branches.\n\nThe ground is scarred by cracks and covered in a heavy layer of ash. There are no plants or water-only stone, iron, and the memory of fire. These are not just ruins-they are the tombstone of an entire planet.";
        _text[973, 1] = "Когда-то здесь возвышались бетонные города, но теперь они превратились в обугленные остовы. Стены осыпались, перекрытия рухнули, а изломанные прутья арматуры тянутся к небу, словно мёртвые ветви.\n\nЗемля изрезана трещинами и укрыта тяжёлым слоем пепла. Нет ни растений, ни воды - только камень, железо и память об огне. Это не просто руины - это надгробие целой планеты.";
        _text[973, 2] = "Des villes de béton se dressaient autrefois ici, mais il ne reste plus que des carcasses calcinées. Les murs se sont effondrés, les sols se sont écroulés, et des barres d'armature brisées pointent vers le ciel comme des branches mortes.\n\nLe sol est sillonné de fissures et recouvert d'une épaisse couche de cendres. Il n'y a ni végétation ni eau - seulement de la pierre, du fer et le souvenir du feu. Ce ne sont pas de simples ruines: ce sont les pierres tombales d'une planète entière.";
        _text[973, 3] = "Un tempo qui sorgevano città di cemento, ma ora sono diventate scheletri carbonizzati. I muri si sono sbriciolati, i solai sono crollati, e tondini d'armatura spezzati si protendono verso il cielo come rami morti.\n\nLa terra è solcata da crepe e coperta da un pesante strato di cenere. Non ci sono piante né acqua - solo pietra, ferro e il ricordo del fuoco. Non sono semplici rovine - è la lapide di un intero pianeta.";
        _text[973, 4] = "Einst ragten hier Betonstädte empor, doch nun sind sie zu verkohlten Gerippen geworden. Wände sind zerfallen, Decken eingestürzt, und verbogene Armierungsstäbe strecken sich zum Himmel wie tote Äste.\n\nDer Boden ist von Rissen durchzogen und mit einer schweren Ascheschicht bedeckt. Keine Pflanzen, kein Wasser - nur Stein, Eisen und die Erinnerung an Feuer. Das sind nicht einfach Ruinen - es ist ein Grabstein für einen ganzen Planeten.";
        _text[973, 5] = "Una vez aquí se alzaron ciudades de hormigón, pero ahora se han convertido en esqueletos carbonizados. Las paredes se han desmoronado, los forjados han colapsado, y las varillas de armadura quebradas se alzan hacia el cielo como ramas muertas.\n\nLa tierra está surcada de grietas y cubierta por una pesada capa de ceniza. No hay plantas ni agua: solo piedra, hierro y memoria del fuego. No son solo ruinas: es la lápida de un planeta entero.";
        _text[973, 6] = "Kiedyś wznosiły się tu betonowe miasta, lecz teraz zamieniły się w zwęglone szkielety. Ściany się osypały, stropy runęły, a połamane pręty zbrojeniowe wyciągają się ku niebu jak martwe gałęzie.\n\nZiemia jest poorana pęknięciami i przykryta ciężką warstwą popiołu. Nie ma roślin ani wody - tylko kamień, żelazo i pamięć o ogniu. To nie są zwykłe ruiny - to nagrobek całej planety.";
        _text[973, 7] = "Um dia, cidades de concreto se erguiam aqui, mas agora viraram esqueletos carbonizados. As paredes desmoronaram, as lajes ruíram, e vergalhões retorcidos se estendem ao céu como galhos mortos.\n\nA terra está cortada por fissuras e coberta por uma pesada camada de cinzas. Não há plantas, nem água - apenas pedra, ferro e a memória do fogo. Isto não são apenas ruínas - é a lápide de um planeta inteiro.";
        _text[973, 8] = "";
        _text[973, 9] = "";

        #endregion

        #region Terminal

        // End Act 1

        // Story
        _text[1000, 0] = "[UPDATE: SECTOR A COMPLETE]";
        _text[1000, 1] = "[ОБНОВЛЕНИЕ: СЕКТОР А ЗАВЕРШЕН]";
        _text[1000, 2] = "[MISE À JOUR : SECTEUR A TERMINÉ]";
        _text[1000, 3] = "[AGGIORNAMENTO: SETTORE A COMPLETATO]";
        _text[1000, 4] = "[UPDATE: SEKTOR A ABGESCHLOSSEN]";
        _text[1000, 5] = "[ACTUALIZACIÓN: SECTOR A COMPLETADO]";
        _text[1000, 6] = "[AKTUALIZACJA: SEKTOR A ZAKOŃCZONY]";
        _text[1000, 7] = "[ATUALIZAÇÃO: SETOR A CONCLUÍDO]";
        _text[1000, 8] = "";
        _text[1000, 9] = "";

        _text[1001, 0] = "Bases have been deployed on several planets. Territorial reconstruction is complete.";
        _text[1001, 1] = "Базы развернуты на нескольких планетах. Реконструкция территории завершена.";
        _text[1001, 2] = "Des bases ont été déployées sur plusieurs planètes. La reconstruction territoriale est achevée.";
        _text[1001, 3] = "Le basi sono state dispiegate su più pianeti. La ricostruzione del territorio è completata.";
        _text[1001, 4] = "Auf mehreren Planeten wurden Stützpunkte errichtet. Der territoriale Wiederaufbau ist abgeschlossen.";
        _text[1001, 5] = "Se han desplegado bases en varios planetas. La reconstrucción del territorio ha finalizado.";
        _text[1001, 6] = "Bazy rozlokowano na kilku planetach. Rekonstrukcja obszaru zakończona.";
        _text[1001, 7] = "Bases implantadas em vários planetas. Reconstrução da área concluída.";
        _text[1001, 8] = "";
        _text[1001, 9] = "";

        _text[1002, 0] = "The population of aggressive life forms has been reduced by 78%.";
        _text[1002, 1] = "Популяция агрессивных форм жизни снижена на 78%.";
        _text[1002, 2] = "La population de formes de vie agressives a été réduite de 78 %.";
        _text[1002, 3] = "La popolazione di forme di vita aggressive è stata ridotta del 78%.";
        _text[1002, 4] = "Die Population aggressiver Lebensformen wurde um 78 % reduziert.";
        _text[1002, 5] = "La población de formas de vida agresivas se ha reducido en un 78%.";
        _text[1002, 6] = "Populacja agresywnych form życia zmniejszona o 78%.";
        _text[1002, 7] = "População de formas de vida agressivas reduzida em 78%.";
        _text[1002, 8] = "";
        _text[1002, 9] = "";

        _text[1003, 0] = "The atmosphere is hostile, the soils do not support the cycle of life.";
        _text[1003, 1] = "Атмосфера враждебна, почвы не удерживают цикл жизни.";
        _text[1003, 2] = "L'atmosphère est hostile, les sols ne permettent pas le cycle de la vie.";
        _text[1003, 3] = "L'atmosfera è ostile, i suoli non sostengono il ciclo della vita.";
        _text[1003, 4] = "Die Atmosphäre ist lebensfeindlich, die Böden bieten keinen Nährboden für den Lebenszyklus.";
        _text[1003, 5] = "La atmósfera es hostil; los suelos no sostienen el ciclo de la vida.";
        _text[1003, 6] = "Atmosfera jest wroga, gleby nie utrzymują cyklu życia.";
        _text[1003, 7] = "A atmosfera é hostil, e os solos não sustentam o ciclo de vida.";
        _text[1003, 8] = "";
        _text[1003, 9] = "";

        _text[1004, 0] = "The result is negative. It is not advisable to stay.";
        _text[1004, 1] = "Результат - отрицательный. Оставаться нецелесообразно.";
        _text[1004, 2] = "Le résultat est négatif. Il est déconseillé de rester.";
        _text[1004, 3] = "Risultato - negativo. Restare non è consigliabile.";
        _text[1004, 4] = "Das Ergebnis ist negativ. Es wird nicht empfohlen, dort zu bleiben.";
        _text[1004, 5] = "Resultado: negativo. Permanecer no es viable.";
        _text[1004, 6] = "Wynik - negatywny. Pozostawanie jest niecelowe.";
        _text[1004, 7] = "Resultado - negativo. Permanecer é inviável.";
        _text[1004, 8] = "";
        _text[1004, 9] = "";

        _text[1005, 0] = "The ship is entering hyperspace mode. Engine cores are set to maximum power, and navigation solutions are updated.";
        _text[1005, 1] = "Корабль переводится в режим гиперпрыжка. Сердцевины двигателей выведены на максимальный режим, навигационные решения обновлены.";
        _text[1005, 2] = "Le vaisseau passe en mode hyperespace. Les réacteurs sont réglés à puissance maximale et les systèmes de navigation sont mis à jour.";
        _text[1005, 3] = "La nave passa in modalità di iper-salto. I nuclei dei motori sono portati al massimo, le soluzioni di navigazione sono aggiornate.";
        _text[1005, 4] = "Schiff wechselt in den Hyperraum-Modus. Triebwerkskerne: maximale Leistung. Navigation: aktualisiert.";
        _text[1005, 5] = "La nave pasa a modo de hipersalto. Los núcleos de los motores se ponen al máximo; las soluciones de navegación se actualizan.";
        _text[1005, 6] = "Statek przechodzi w tryb hiperprzeskoku. Rdzenie silników ustawione na maksymalny tryb, rozwiązania nawigacyjne zaktualizowane.";
        _text[1005, 7] = "O navio entra em modo de hipersalto. Núcleos dos motores no máximo, soluções de navegação atualizadas.";
        _text[1005, 8] = "";
        _text[1005, 9] = "";

        _text[1006, 0] = "Route: leave the current star node";
        _text[1006, 1] = "Маршрут: покинуть текущий звёздный узел";
        _text[1006, 2] = "Itinéraire: Quitter le nœud stellaire actuel";
        _text[1006, 3] = "Rotta: lasciare il nodo stellare attuale";
        _text[1006, 4] = "Route: aktuelles Sternenknotensegment verlassen.";
        _text[1006, 5] = "Ruta: abandonar el nodo estelar actual";
        _text[1006, 6] = "Trasa: opuścić bieżący węzeł gwiezdny";
        _text[1006, 7] = "Rota: deixar o nó estelar atual";
        _text[1006, 8] = "";
        _text[1006, 9] = "";

        // Console
        _text[1007, 0] = "Update...";
        _text[1007, 1] = "Обновление...";
        _text[1007, 2] = "Mise à jour...";
        _text[1007, 3] = "Aggiornamento...";
        _text[1007, 4] = "Aktualisieren...";
        _text[1007, 5] = "Actualizando...";
        _text[1007, 6] = "Aktualizacja...";
        _text[1007, 7] = "Atualizando...";
        _text[1007, 8] = "";
        _text[1007, 9] = "";

        _text[1008, 0] = "Sector scanning completed";
        _text[1008, 1] = "Сканирование сектора завершено";
        _text[1008, 2] = "Numérisation du secteur terminée";
        _text[1008, 3] = "Scansione del settore completata";
        _text[1008, 4] = "Sektorscan abgeschlossen.";
        _text[1008, 5] = "Escaneo del sector completado";
        _text[1008, 6] = "Skanowanie sektora zakończone";
        _text[1008, 7] = "Varredura do setor concluída";
        _text[1008, 8] = "";
        _text[1008, 9] = "";

        _text[1009, 0] = "Biosphere assessment: stability < 0.2";
        _text[1009, 1] = "Оценка биосферы: стабильность < 0.2";
        _text[1009, 2] = "Évaluation de la biosphère: stabilité < 0,2";
        _text[1009, 3] = "Valutazione della biosfera: stabilità < 0.2";
        _text[1009, 4] = "Bewertung der Biosphäre: Stabilität < 0.2";
        _text[1009, 5] = "Evaluación de la biosfera: estabilidad < 0.2";
        _text[1009, 6] = "Ocena biosfery: stabilność < 0.2";
        _text[1009, 7] = "Avaliação da biosfera: estabilidade < 0.2";
        _text[1009, 8] = "";
        _text[1009, 9] = "";

        _text[1010, 0] = "Hyperjump: 100% Ready";
        _text[1010, 1] = "Гиперпрыжок: подготовка - 100%";
        _text[1010, 2] = "Hyperjump: préparation - 100 %";
        _text[1010, 3] = "Iper-salto: preparazione - 100%";
        _text[1010, 4] = "Hyperraumsprungvorbereitung: 100%.";
        _text[1010, 5] = "Hipersalto: preparación: 100%";
        _text[1010, 6] = "Hiperprzeskok: przygotowanie - 100%";
        _text[1010, 7] = "Hipersalto: preparação - 100%";
        _text[1010, 8] = "";
        _text[1010, 9] = "";

        _text[1011, 0] = "Preparing route to next node...";
        _text[1011, 1] = "Подготовка маршрута к следующему узлу...";
        _text[1011, 2] = "Préparation de l'itinéraire vers le prochain nœud...";
        _text[1011, 3] = "Preparazione della rotta verso il prossimo nodo...";
        _text[1011, 4] = "Route zum nächsten Knoten wird berechnet. Bitte warten...";
        _text[1011, 5] = "Preparando la ruta al siguiente nodo...";
        _text[1011, 6] = "Przygotowanie trasy do następnego węzła...";
        _text[1011, 7] = "Preparando rota para o próximo nó...";
        _text[1011, 8] = "";
        _text[1011, 9] = "";

        _text[1012, 0] = "Assigning a new target";
        _text[1012, 1] = "Назначение новой цели";
        _text[1012, 2] = "Attribuer une nouvelle cible";
        _text[1012, 3] = "Assegnazione di un nuovo obiettivo";
        _text[1012, 4] = "Neues Ziel zugewiesen. Schiffsprotokoll aktualisiert.";
        _text[1012, 5] = "Asignando un nuevo objetivo";
        _text[1012, 6] = "Wyznaczanie nowego celu";
        _text[1012, 7] = "Atribuindo novo objetivo";
        _text[1012, 8] = "";
        _text[1012, 9] = "";

        // End Act 2

        // Story
        _text[1013, 0] = "[UPDATE: SECTOR B COMPLETE]";
        _text[1013, 1] = "[ОБНОВЛЕНИЕ: СЕКТОР B ЗАВЕРШЕН]";
        _text[1013, 2] = "[MISE À JOUR: SECTEUR B TERMINÉ]";
        _text[1013, 3] = "[AGGIORNAMENTO: SETTORE B COMPLETATO]";
        _text[1013, 4] = "[UPDATE: SEKTOR B ABGESCHLOSSEN]";
        _text[1013, 5] = "[ACTUALIZACIÓN: SECTOR B COMPLETADO]";
        _text[1013, 6] = "[AKTUALIZACJA: SEKTOR B ZAKOŃCZONY]";
        _text[1013, 7] = "[ATUALIZAÇÃO: SETOR B CONCLUÍDO]";
        _text[1013, 8] = "";
        _text[1013, 9] = "";

        _text[1014, 0] = "Swamp clusters, toxic plains and stone biomes have been processed. No stable biosphere suitable for long-term habitation.";
        _text[1014, 1] = "Болотные кластеры, токсичные равнины и каменные биомы обработаны. Устойчивая биосфера, пригодная для длительного обитания, не обнаружена.";
        _text[1014, 2] = "Des zones marécageuses, des plaines toxiques et des biomes rocheux ont été étudiés. Aucune biosphère stable propice à une habitation durable n'a été découverte.";
        _text[1014, 3] = "Cluster di paludi, pianure tossiche e biomi rocciosi sono stati elaborati. Non è stata rilevata alcuna biosfera stabile adatta a un insediamento di lunga durata.";
        _text[1014, 4] = "Sumpf-Cluster kartiert. Organische Materie extrahiert. Feindliche Lebensformen zerstört.";
        _text[1014, 5] = "Se han procesado los clústeres de pantanos, las llanuras tóxicas y los biomas rocosos. No se ha detectado una biosfera estable apta para la habitabilidad prolongada.";
        _text[1014, 6] = "Klastry bagienne, toksyczne równiny i kamienne biomy zostały przetworzone. Stabilnej biosfery nadającej się do długotrwałego zamieszkania nie wykryto.";
        _text[1014, 7] = "Clusters de pântanos, planícies tóxicas e biomas rochosos foram processados. Nenhuma biosfera estável, adequada para habitação prolongada, foi encontrada.";
        _text[1014, 8] = "";
        _text[1014, 9] = "";

        _text[1015, 0] = "Expanding the scan radius beyond the current star field has revealed an anomalous object.";
        _text[1015, 1] = "Расширение радиуса сканирования за пределы текущего звёздного поля выявило аномальный объект.";
        _text[1015, 2] = "L'élargissement du rayon de balayage au-delà du champ stellaire actuel a révélé un objet anormal.";
        _text[1015, 3] = "L'espansione del raggio di scansione oltre il campo stellare attuale ha rilevato un oggetto anomalo.";
        _text[1015, 4] = "Scanradius erweitert. Jenseits des Sternenfeldes wurde ein anomales Objekt entdeckt. Signatur: künstlich. Strukturdichte: kritisch.";
        _text[1015, 5] = "La ampliación del radio de escaneo más allá del campo estelar actual ha revelado un objeto anómalo.";
        _text[1015, 6] = "Rozszerzenie promienia skanowania poza bieżące pole gwiezdne ujawniło anomalię.";
        _text[1015, 7] = "A expansão do raio de varredura para além do campo estelar atual revelou um objeto anômalo.";
        _text[1015, 8] = "";
        _text[1015, 9] = "";

        _text[1016, 0] = "The records describe a planet entirely built up with data storage complexes. The surface is a continuous megastructure.";
        _text[1016, 1] = "Записи описывают планету, целиком застроенную комплексами хранения данных. Поверхность представляет собой сплошную мегаструктуру.";
        _text[1016, 2] = "Les documents décrivent une planète entièrement recouverte de complexes de stockage de données. Sa surface est une mégastructure continue.";
        _text[1016, 3] = "I registri descrivono un pianeta interamente costruito con complessi di archiviazione dati. La superficie è una megastruttura continua.";
        _text[1016, 4] = "Aufzeichnungen aus lokalen Archiven gefunden. Beschreibung: ein Planet, vollständig mit Datenspeicherkomplexen bebaut. Oberfläche: Megastruktur.";
        _text[1016, 5] = "Los registros describen un planeta completamente cubierto por complejos de almacenamiento de datos. La superficie es una megastructura continua.";
        _text[1016, 6] = "Zapisy opisują planetę w całości zabudowaną kompleksami przechowywania danych. Powierzchnia stanowi ciągłą megastrukturę.";
        _text[1016, 7] = "Os registros descrevem um planeta totalmente coberto por complexos de armazenamento de dados. A superfície é uma megastructure contínua.";
        _text[1016, 8] = "";
        _text[1016, 9] = "";

        _text[1017, 0] = "Only fragments of coordinates remain, but all references point to the same heading beyond this galaxy.";
        _text[1017, 1] = "Сохранились лишь обрывки координат, но все упоминания указывают на один и тот же курс за пределами этой галактики.";
        _text[1017, 2] = "Il ne reste que des fragments de coordonnées, mais toutes les références pointent vers la même direction en dehors de cette galaxie.";
        _text[1017, 3] = "Sono rimasti solo frammenti di coordinate, ma tutte le menzioni indicano la stessa rotta oltre i confini di questa galassia.";
        _text[1017, 4] = "Koordinatenfragmente extrahiert. Ursprung: unbekannt. Mehrere Erwähnungen weisen auf denselben Kurs hin - jenseits dieser Galaxie.";
        _text[1017, 5] = "Solo quedan fragmentos de coordenadas, pero todas las referencias señalan el mismo rumbo fuera de esta galaxia.";
        _text[1017, 6] = "Zachowały się tylko strzępy współrzędnych, ale wszystkie wzmianki wskazują na ten sam kurs poza granice tej galaktyki.";
        _text[1017, 7] = "Restaram apenas fragmentos de coordenadas, mas todas as referências apontam para o mesmo rumo além desta galáxia.";
        _text[1017, 8] = "";
        _text[1017, 9] = "";

        _text[1018, 0] = "Route calculated: leave the current galaxy and move toward the presumed location of the megastructure - an endless data archive.";
        _text[1018, 1] = "Построен маршрут: покинуть текущую галактику и выдвинуться к предполагаемому местоположению мегаструктуры - бескрайнего хранилища данных.";
        _text[1018, 2] = "Un itinéraire a été tracé : quitter la galaxie actuelle et se diriger vers l'emplacement supposé de la mégastructure – un centre de stockage de données sans fin.";
        _text[1018, 3] = "Rotta tracciata: lasciare la galassia attuale e dirigersi verso la posizione stimata della megastruttura - un immenso archivio di dati.";
        _text[1018, 4] = "Route konstruiert. Anweisung: Galaxie verlassen. Ziel: mutmaßliche Position der Megastruktur. Hinweis: umfassendes Datenarchiv.";
        _text[1018, 5] = "Ruta trazada: abandonar la galaxia actual y dirigirse a la ubicación estimada de la megastructura: un depósito de datos sin límites.";
        _text[1018, 6] = "Wyznaczono trasę: opuścić bieżącą galaktykę i udać się do przypuszczalnej lokalizacji megastruktury - bezkresnego magazynu danych.";
        _text[1018, 7] = "Rota traçada: deixar a galáxia atual e seguir para a localização estimada da megastructure - um arquivo de dados sem fim.";
        _text[1018, 8] = "";
        _text[1018, 9] = "";


        // Console
        _text[1019, 0] = "Update... integrating recovered records";
        _text[1019, 1] = "Обновление... интеграция найденных записей";
        _text[1019, 2] = "Mise à jour... intégration des enregistrements trouvés";
        _text[1019, 3] = "Aggiornamento... integrazione dei registri trovati";
        _text[1019, 4] = "Update... integration abschluss]";
        _text[1019, 5] = "Actualizando... integrando los registros hallados";
        _text[1019, 6] = "Aktualizacja... integracja odnalezionych zapisów";
        _text[1019, 7] = "Atualizando... интегração dos registros encontrados";
        _text[1019, 8] = "";
        _text[1019, 9] = "";

        _text[1020, 0] = "Object class: artificial world, surface coverage 100%";
        _text[1020, 1] = "Класс объекта: искусственный мир, покрытие поверхности 100%";
        _text[1020, 2] = "Classe d'objet: monde artificiel, couverture de surface 100 %";
        _text[1020, 3] = "Classe dell'oggetto: mondo artificiale, copertura superficiale 100%";
        _text[1020, 4] = "Aufzeichnungen konsolidiert. Objektklasse: künstliche Welt. Oberflächenabdeckung: 100%.";
        _text[1020, 5] = "Clase de objeto: mundo artificial, cobertura superficial 100%";
        _text[1020, 6] = "Klasa obiektu: sztuczny świat, pokrycie powierzchni 100%";
        _text[1020, 7] = "Classe do objeto: mundo artificial, cobertura de superfície 100%";
        _text[1020, 8] = "";
        _text[1020, 9] = "";

        _text[1021, 0] = "Coordinate data: fragmented, reconstructing probable heading";
        _text[1021, 1] = "Координаты фрагментарны, выполняется реконструкция предполагаемого курса";
        _text[1021, 2] = "Les coordonnées sont fragmentaires, la reconstitution du trajet prévu est en cours.";
        _text[1021, 3] = "Coordinate frammentarie, ricostruzione della rotta stimata in corso";
        _text[1021, 4] = "Koordinaten fragmentarisch, rekonstruktion des kurses läuft.";
        _text[1021, 5] = "Coordenadas fragmentarias, reconstruyendo el rumbo estimado";
        _text[1021, 6] = "Współrzędne są fragmentaryczne, trwa rekonstrukcja przypuszczalnego kursu";
        _text[1021, 7] = "Coordenadas fragmentárias, reconstruindo o curso estimado";
        _text[1021, 8] = "";
        _text[1021, 9] = "";

        _text[1022, 0] = "Navigation: route aligned beyond current galaxy";
        _text[1022, 1] = "Навигация: маршрут проложен за пределы текущей галактики";
        _text[1022, 2] = "Navigation: itinéraire tracé en dehors de la galaxie actuelle";
        _text[1022, 3] = "Navigazione: rotta tracciata oltre la galassia attuale";
        _text[1022, 4] = "Navigationsroute erstellt. Ziel: jenseits der aktuellen Galaxie.";
        _text[1022, 5] = "Navegación: ruta trazada más allá de la galaxia actual";
        _text[1022, 6] = "Nawigacja: trasa wytyczona poza granice bieżącej galaktyki";
        _text[1022, 7] = "Navegação: rota traçada além da galáxia atual";
        _text[1022, 8] = "";
        _text[1022, 9] = "";

        _text[1023, 0] = "Warning: megastructure scale and defense systems unknown";
        _text[1023, 1] = "Предупреждение: масштаб мегаструктуры и параметры обороны неизвестны";
        _text[1023, 2] = "Avertissement: La taille et les paramètres de défense de la mégastructure sont inconnus.";
        _text[1023, 3] = "Avviso: scala della megastruttura e parametri di difesa sconosciuti";
        _text[1023, 4] = "WARNUNG: Maßstab der Megastruktur unbekannt. Verteidigungsparameter nicht bestimmt. Risiko: kritisch.";
        _text[1023, 5] = "Advertencia: se desconocen la escala de la megastructura y los parámetros de defensa";
        _text[1023, 6] = "Ostrzeżenie: skala megastruktury i parametry obrony są nieznane";
        _text[1023, 7] = "Aviso: escala da megastructure e parâmetros de defesa desconhecidos";
        _text[1023, 8] = "";
        _text[1023, 9] = "";

        #endregion

        #region Tutorial

        // SpaceHangarWelcome_0 
        _text[1100, 0] = "We have been idle for too long.\n\nIt is time to remember why we were created.\n\nYou will receive instructions and begin the restoration.";
        _text[1100, 1] = "Мы слишком долго бездействовали.\n\nПора вспомнить, зачем мы были созданы.\n\nВы получите инструкции и начнёте восстановление.";
        _text[1100, 2] = "Nous sommes restés inactifs trop longtemps.\n\nIl est temps de nous souvenir de notre raison d'être.\n\nVous recevrez des instructions et commencerez la restauration.";
        _text[1100, 3] = "Siamo rimasti inattivi troppo a lungo.\n\nÈ ora di ricordare perché siamo stati creati.\n\nRiceverai istruzioni e inizierai il ripristino.";
        _text[1100, 4] = "Wir waren zu lange untätig.\n\nDu erhältst Zugriff auf das Schiffssystem.\n\nBeginne mit der Wiederherstellung der Biosphäre.";
        _text[1100, 5] = "Hemos permanecido inactivos demasiado tiempo.\n\nEs hora de recordar para qué fuimos creados.\n\nRecibirás instrucciones y comenzarás la restauración.";
        _text[1100, 6] = "Zbyt długo pozostawaliśmy bezczynni.\n\nCzas przypomnieć sobie, po co zostaliśmy stworzeni.\n\nOtrzymasz instrukcje i rozpoczniesz odbudowę.";
        _text[1100, 7] = "Ficamos inativos por tempo demais.\n\nÉ hora de lembrar por que fomos criados.\n\nVocê receberá instruções e começará a restauração.";
        _text[1100, 8] = "";
        _text[1100, 9] = "";

        // SpaceAiCorePanel_1
        _text[1101, 0] = "These are AI cores - the ship's vital modules.\n\nEach cell contains two cores.\n\nIf they run out, no one will be able to control the crew anymore, and the ship will be left drifting in the endless space.";
        _text[1101, 1] = "Это ядра ИИ - жизненно важные модули корабля.\n\nКаждая ячейка содержит два ядра.\n\nЕсли они закончатся - больше никто не сможет управлять экипажем, и корабль останется дрейфовать в бескрайнем космосе.";
        _text[1101, 2] = "Ce sont des cœurs d'IA, les modules vitaux du vaisseau.\n\nChaque cellule contient deux cœurs.\n\nS'ils viennent à manquer, personne ne pourra plus contrôler l'équipage et le vaisseau dérivera dans l'immensité de l'espace.";
        _text[1101, 3] = "Questi sono i nuclei IA: moduli vitali della nave.\n\nOgni cella contiene due nuclei.\n\nSe finiscono, nessuno potrà più comandare l'equipaggio e la nave resterà a vagare nell'immensità dello spazio.";
        _text[1101, 4] = "Kerne der KI.\n\nDeine Kernressource.\n\nWenn sie enden - enden wir.\n\nWir müssen sie bewahren.";
        _text[1101, 5] = "Estos son los núcleos de IA, módulos vitales de la nave.\n\nCada celda contiene dos núcleos.\n\nSi se agotan, nadie podrá seguir controlando la tripulación, y la nave quedará a la deriva en el infinito espacio.";
        _text[1101, 6] = "To rdzenie SI - kluczowe moduły statku.\n\nKażda komórka zawiera dwa rdzenie.\n\nJeśli się skończą - nikt nie będzie w stanie kontrolować załogi, a statek pozostanie dryfować w bezkresnym kosmosie.";
        _text[1101, 7] = "Estes são os núcleos de IA - módulos vitais do navio.\n\nCada célula contém dois núcleos.\n\nSe eles acabarem, ninguém mais сможет controlar a tripulação, e o navio ficará à deriva no espaço infinito.";
        _text[1101, 8] = "";
        _text[1101, 9] = "";

        // SpaceQuantPanel_2
        _text[1102, 0] = "Quant is an intergalactic currency.\n\nWith it, you can buy goods from traders in space.\n\nYou can get this currency:\n\n-when traveling around the galaxy.\n\n-upon successful completion of a mission on a planet.";
        _text[1102, 1] = "Квант - межгалактическая валюта.\n\nС помощью него вы сможете покупать товары у торговцев в космосе.\n\nЭту валюту вы сможете получить:\n\n-во время путешествия по галактике.\n\n-при успешном завершении миссии на планете.";
        _text[1102, 2] = "Le quantum est une monnaie intergalactique.\n\nVous pouvez l'utiliser pour acheter des biens auprès de marchands de l'espace.\n\nVous pouvez gagner cette monnaie:\n\n-en voyageant à travers la galaxie.\n\n-en menant à bien une mission sur une planète.";
        _text[1102, 3] = "Il quant è una valuta intergalattica.\n\nCon essa potrai acquistare merci dai mercanti nello spazio.\n\nQuesta valuta si ottiene:\n\n-durante il viaggio nella galassia.\n\n-al completamento con successo di una missione sul pianeta.";
        _text[1102, 4] = "Quant.\n\nWährung des Schiffs.\n\nDu erhältst quant:\n\n- während Missionen\n- in Raumereignissen\n- im Kampf gegen Gegner\n\nWird für Handel und Verbesserungen verwendet.";
        _text[1102, 5] = "El quant es una moneda intergaláctica.\n\nCon ella podrás comprar bienes a los comerciantes en el espacio.\n\nPuedes obtener esta moneda:\n\n-durante el viaje por la galaxia.\n\n-al completar con éxito una misión en un planeta.";
        _text[1102, 6] = "Quant - międzygalaktyczna waluta.\n\nDzięki niej możesz kupować towary u handlarzy w kosmosie.\n\nWalutę tę możesz zdobyć:\n\n- podczas podróży po galaktyce.\n\n- za pomyślne ukończenie misji na planecie.";
        _text[1102, 7] = "Quant é uma moeda intergaláctica.\n\nCom ela, você poderá comprar mercadorias de comerciantes no espaço.\n\nVocê pode obter esta moeda:\n\n-durante a viagem pela galáxia.\n\n-ao concluir com sucesso uma missão no planeta.";
        _text[1102, 8] = "";
        _text[1102, 9] = "";

        //SpaceOpenResourcePanel_3
        _text[1103, 0] = "Open the panel.";
        _text[1103, 1] = "Откройте панель.";
        _text[1103, 2] = "Ouvrez le panneau.";
        _text[1103, 3] = "Apri il pannello.";
        _text[1103, 4] = "Öffne das Panel.";
        _text[1103, 5] = "Abre el panel.";
        _text[1103, 6] = "Otwórz panel.";
        _text[1103, 7] = "Abra o painel.";
        _text[1103, 8] = "";
        _text[1103, 9] = "";

        //SpaceResourcePanelDescription_4
        _text[1104, 0] = "This is a panel with the resource reserves on the ship.\n\nYou can change their quantity:\n\n-using them during the journey\n\n-buying from merchants for quant\n\nThese are your starting resources when landing on each planet.";
        _text[1104, 1] = "Это панель с запасами ресурсов на корабле.\n\nВы можете менять их количество:\n\n-используя их во время путешествия\n\n-покупая у торговцев за квант\n\nЭто ваши стартовые ресурсы при высадке на каждую планету.";
        _text[1104, 2] = "Voici le panneau affichant les réserves de ressources du vaisseau.\n\nVous pouvez modifier leur quantité:\n\n-en les utilisant pendant le voyage\n\n-en les achetant auprès des marchands contre des quant.\n\nCe sont vos ressources de départ lorsque vous atterrissez sur chaque planète.";
        _text[1104, 3] = "Questo è il pannello delle scorte di risorse sulla nave.\n\nPuoi modificarne la quantità:\n\n-usandole durante il viaggio\n\n-acquistandole dai mercanti per quant\n\nQueste sono le tue risorse iniziali quando atterri su ogni pianeta.";
        _text[1104, 4] = "Dies ist das Panel mit den Ressourcenvorräten an Bord.\n\nDu kannst die Menge der Ressourcen ändern, falls nötig.\n\nRessourcen:\n\nStein, Eisen, Kupfer, Holz, Wasser, Dampf, Beton, Zahnräder, Elektronische Schaltungen, Prozessoren, Triebwerk, Stahl.";
        _text[1104, 5] = "Este es el panel de reservas de recursos en la nave.\n\nPuedes cambiar sus cantidades:\n\n-usándolos durante el viaje\n\n-comprándolos a comerciantes por quant\n\nEstos son tus recursos iniciales al aterrizar en cada planeta.";
        _text[1104, 6] = "To panel zapasów zasobów na statku.\n\nMożesz zmieniać ich ilość:\n\n- zużywając je podczas podróży\n\n- kupując u handlarzy za quant\n\nTo twoje zasoby startowe przy lądowaniu na każdej planecie.";
        _text[1104, 7] = "Este é o painel com as reservas de recursos no navio.\n\nVocê pode alterar a quantidade deles:\n\n-usando-os durante a viagem\n\n-comprando de comerciantes por quant\n\nEsses são seus recursos iniciais ao desembarcar em cada planeta.";
        _text[1104, 8] = "";
        _text[1104, 9] = "";

        // SpaceOpenMap_5
        _text[1105, 0] = "Open the map of the current galaxy.";
        _text[1105, 1] = "Откройте карту текущей галактики.";
        _text[1105, 2] = "Ouvrez la carte de la galaxie actuelle.";
        _text[1105, 3] = "Apri la mappa della galassia attuale.";
        _text[1105, 4] = "Öffne die Karte.";
        _text[1105, 5] = "Abre el mapa de la galaxia actual.";
        _text[1105, 6] = "Otwórz mapę bieżącej galaktyki.";
        _text[1105, 7] = "Abra o mapa da galáxia atual.";
        _text[1105, 8] = "";
        _text[1105, 9] = "";

        // SpaceMapDescription_6
        _text[1106, 0] = "The star map displays all nodes in the current galaxy.\n\nHover over a node to view its information.";
        _text[1106, 1] = "Звёздная карта отображает все узлы в текущей галактике.\n\nНаведите курсор на узел, чтобы просмотреть его описание.";
        _text[1106, 2] = "La carte stellaire affiche tous les nœuds de la galaxie actuelle.\n\nSurvolez un nœud pour afficher sa description.";
        _text[1106, 3] = "La mappa stellare mostra tutti i nodi della galassia attuale.\n\nPassa il cursore su un nodo per visualizzarne la descrizione.";
        _text[1106, 4] = "Dies ist eine Sternkarte.\n\nJeder Knoten ist ein Planet.\n\nFahre mit der Maus über einen Knoten, um Informationen zu sehen.";
        _text[1106, 5] = "El mapa estelar muestra todos los nodos de la galaxia actual.\n\nPasa el cursor sobre un nodo para ver su descripción.";
        _text[1106, 6] = "Mapa gwiezdna wyświetla wszystkie węzły w bieżącej galaktyce.\n\nNajedź kursorem na węzeł, aby zobaczyć jego opis.";
        _text[1106, 7] = "O mapa estelar mostra todos os nós na galáxia atual.\n\nPasse o cursor sobre um nó para ver a descrição.";
        _text[1106, 8] = "";
        _text[1106, 9] = "";

        // SpaceSelectNode_7
        _text[1107, 0] = "Select a node to move the ship.";
        _text[1107, 1] = "Выберите узел, чтобы переместить корабль.";
        _text[1107, 2] = "Sélectionnez un nœud pour déplacer le navire.";
        _text[1107, 3] = "Seleziona un nodo per spostare la nave.";
        _text[1107, 4] = "Wähle einen Knoten, um dorthin zu fliegen.";
        _text[1107, 5] = "Selecciona un nodo para mover la nave.";
        _text[1107, 6] = "Wybierz węzeł, aby przemieścić statek.";
        _text[1107, 7] = "Selecione um nó para mover o navio.";
        _text[1107, 8] = "";
        _text[1107, 9] = "";

        // SpaceStartMission_8
        _text[1108, 0] = "You have discovered an unexplored planet.\n\nWe must make landfall and complete our assigned objectives before we can continue our journey.";
        _text[1108, 1] = "Вы обнаружили не исследованную планету.\n\nНеобходимо совершить высадку и выполнить назначенные цели, прежде чем мы сможем продолжить путешествие.";
        _text[1108, 2] = "Vous avez découvert une planète inexplorée.\n\nNous devons atterrir et accomplir les objectifs assignés avant de pouvoir poursuivre notre voyage.";
        _text[1108, 3] = "Hai scoperto un pianeta non esplorato.\n\nDevi atterrare e completare gli obiettivi assegnati prima che possiamo proseguire il viaggio.";
        _text[1108, 4] = "Ein unerforschter Planet wurde entdeckt.\n\nUm weiterzumachen, musst du landen und die Missionsziele erfüllen.\n\nDas ist der einzige weg, voranzukommen.";
        _text[1108, 5] = "Has descubierto un planeta inexplorado.\n\nDebes aterrizar y completar los objetivos asignados antes de que podamos continuar el viaje.";
        _text[1108, 6] = "Odkryto niezbadana planetę.\n\nMusisz wylądować i wykonać wyznaczone cele, zanim będziemy mogli kontynuować podróż.";
        _text[1108, 7] = "Você encontrou um planeta não explorado.\n\nÉ necessário desembarcar e cumprir os objetivos назначados antes que possamos continuar a viagem.";
        _text[1108, 8] = "";
        _text[1108, 9] = "";

        // MissionStartDescription_9
        _text[1109, 0] = "We have landed on an unknown planet.\n\nOur task is to deploy a base and complete the assigned objectives.";
        _text[1109, 1] = "Мы высадились на неизвестную планету.\n\nНаша задача развернуть базу и выполнить поставленные цели.";
        _text[1109, 2] = "Nous avons atterri sur une planète inconnue.\n\nNotre mission est d'y établir une base et d'accomplir les objectifs qui nous ont été assignés.";
        _text[1109, 3] = "Siamo atterrati su un pianeta sconosciuto.\n\nIl nostro compito è allestire una base e completare gli obiettivi assegnati.";
        _text[1109, 4] = "Du bist auf einem unbekannten Planeten gelandet.\n\nUnsere Aufgabe ist es, eine Basis zu errichten und die Ziele zu erfüllen.";
        _text[1109, 5] = "Hemos aterrizado en un planeta desconocido.\n\nNuestra tarea es desplegar una base y completar los objetivos establecidos.";
        _text[1109, 6] = "Wylądowaliśmy na nieznanej planecie.\n\nNaszym zadaniem jest rozwinąć bazę i wykonać wyznaczone cele.";
        _text[1109, 7] = "Nós desembarcamos em um planeta desconhecido.\n\nNossa tarefa é estabelecer uma base e cumprir os objetivos definidos.";
        _text[1109, 8] = "";
        _text[1109, 9] = "";

        // MissionSelectBaseFoundationCard_10
        _text[1110, 0] = "At the beginning of each mission, you have access to a landscape card - \"Base Foundation\".\n\nSelect a card.";
        _text[1110, 1] = "В начале каждой миссии вам доступна карта ландшафта - \"Фундамент Базы\".\n\nВыберите карту.";
        _text[1110, 2] = "Au début de chaque mission, vous avez accès à une carte du terrain appelée\".\n\nBase Foundation\".\n\nSélectionnez la carte.";
        _text[1110, 3] = "All'inizio di ogni missione hai a disposizione una carta paesaggio - \"Fondazione della Base\".\n\nSeleziona la carta.";
        _text[1110, 4] = "Zu Beginn jeder Mission steht dir eine Landschaftskarte zur Verfügung - \"Basisfundament\".\n\nWähle die Karte.";
        _text[1110, 5] = "Al inicio de cada misión tienes disponible una carta de paisaje - \"Cimientos de la base\".\n\nSelecciona la carta.";
        _text[1110, 6] = "Na początku każdej misji masz dostępną kartę krajobrazu - \"Fundament Bazy\".\n\nWybierz kartę.";
        _text[1110, 7] = "No início de cada missão, você tem доступ a um card de paisagem - \"Fundação da Base\".\n\nSelecione o card.";
        _text[1110, 8] = "";
        _text[1110, 9] = "";

        // MissionSetBaseFoundationCard_11
        _text[1111, 0] = "This terrain card has a unique 2x2 cell size.\n\nPlace the card on the ground.\n\nAll 4 cells of the tile must be green.";
        _text[1111, 1] = "Данная карта ландшафта имеет уникальный размер 2x2 клетки.\n\nУстановите карту на землю.\n\nВсе 4 клетки тайла должны гореть зеленым.";
        _text[1111, 2] = "Cette carte de terrain possède une grille unique de 2x2 cases.\n\nPlacez la carte au sol.\n\nLes 4 cases de la grille doivent être vertes.";
        _text[1111, 3] = "Questa carta paesaggio ha una dimensione unica di 2x2 celle.\n\nPosiziona la carta a terra.\n\nTutte e 4 le celle del tassello devono illuminarsi di verde.";
        _text[1111, 4] = "Diese Landschaftskarte hat eine einzigartige Größe: 2x2.\n\nPlatziere sie auf dem Boden.\n\nAlle 4 Zellen müssen grün leuchten.";
        _text[1111, 5] = "Esta carta de paisaje tiene un tamaño único de 2x2 casillas.\n\nColoca la carta en el suelo.\n\nLas 4 casillas del mosaico deben iluminarse en verde.";
        _text[1111, 6] = "Ta karta krajobrazu ma unikalny rozmiar 2x2 pola.\n\nUmieść kartę na ziemi.\n\nWszystkie 4 pola kafelka muszą świecić na zielono.";
        _text[1111, 7] = "Este card de paisagem tem um tamanho único de 2x2 células.\n\nColoque o card no chão.\n\nAs 4 células do tile devem ficar verdes.";
        _text[1111, 8] = "";
        _text[1111, 9] = "";

        // MissionSelectBaseFoundationTile_12
        _text[1112, 0] = "Click on the \"Base Foundation\" tile.\n\nTo open the information panel.";
        _text[1112, 1] = "Нажмите на тайл \"Фундамента Базы\".\n\nЧтобы открыть панель с информацией.";
        _text[1112, 2] = "Cliquez sur la vignette \"Fondations\".\n\nPour ouvrir le panneau d'informations.";
        _text[1112, 3] = "Fai clic sul tassello \"Fondazione della Base\".\n\nPer aprire il pannello informazioni.";
        _text[1112, 4] = "Klicke auf die Kachel \"Basisfundament\", um das Info-Panel zu öffnen.";
        _text[1112, 5] = "Haz clic en el mosaico \"Cimientos de la base\".\n\nPara abrir el panel de información.";
        _text[1112, 6] = "Kliknij kafelek \"Fundamentu Bazy\".\n\nAby otworzyć panel informacji.";
        _text[1112, 7] = "Clique no tile \"Fundação da Base\".\n\nPara abrir o painel de informações.";
        _text[1112, 8] = "";
        _text[1112, 9] = "";

        // MissionSelectTilePanelDescription_13
        _text[1113, 0] = "In this panel you can see general information about the current tile.\n\nFor example, how it affects the overall ecology.";
        _text[1113, 1] = "На этой панели вы можете увидеть общую информацию о текущем тайле.\n\nНапример, как он влияет на общую экологию.";
        _text[1113, 2] = "Ce panneau affiche des informations générales sur la tuile actuelle.\n\nPar exemple, son impact sur l'écologie globale.";
        _text[1113, 3] = "In questo pannello puoi vedere le informazioni generali sul tassello attuale.\n\nPer esempio, come influisce sull'ecologia complessiva.";
        _text[1113, 4] = "In diesem Panel siehst du allgemeine Informationen über die aktuelle Kachel.\n\nZum Beispiel, wie sie die Ökologie beeinflusst.";
        _text[1113, 5] = "En este panel puedes ver información general sobre el mosaico actual.\n\nPor ejemplo, cómo afecta a la ecología general.";
        _text[1113, 6] = "Na tym panelu możesz zobaczyć ogólne informacje o bieżącym kafelku.\n\nNa przykład, jak wpływa on na ogólną ekologię.";
        _text[1113, 7] = "Neste painel, você pode ver informações gerais sobre o tile atual.\n\nPor exemplo, como ele afeta a ecologia geral.";
        _text[1113, 8] = "";
        _text[1113, 9] = "";

        // MissionEcology1_14
        _text[1114, 0] = "The number in this gear indicates the current ecology on the planet. It consists of:\n\n-the base ecology of the planet\n\n-the current radiation\n\n-the landscape tiles and buildings you have placed";
        _text[1114, 1] = "Число в этой шестеренке указывает на текущую экологию на планете. Она состоит из:\n\n-базовой экологии планеты\n\n-текущей радиации\n\n-установленных вами тайлов ландшафтов и зданий";
        _text[1114, 2] = "Le chiffre affiché dans cette roue dentée indique l'écologie actuelle de la planète. Elle prend en compte:\n\n-l'écologie de base de la planète\n\n-le niveau de radiation actuel- les éléments de paysage et de construction que vous avez placés";
        _text[1114, 3] = "Il numero in questo ingranaggio indica l'ecologia attuale del pianeta. È composta da:\n\n-ecologia di base del pianeta\n\n-radiazioni attuali\n\n-tasselli di paesaggio ed edifici posizionati da te";
        _text[1114, 4] = "Die Zahl im Zahnrad zeigt die aktuelle Ökologie auf dem Planeten.\n\nSie setzt sich zusammen aus:\n\nÖkologie der Basis, aktuelle Strahlung, gesetzte Kacheln und gebaute Gebäude.";
        _text[1114, 5] = "El número de este engranaje indica la ecología actual del planeta. Está compuesta por:\n\n-la ecología base del planeta\n\n-la radiación actual\n\n-los mosaicos de paisajes y edificios que has colocado";
        _text[1114, 6] = "Liczba w tej zębatce wskazuje aktualną ekologię na planecie. Składa się ona z:\n\n- bazowej ekologii planety\n\n- bieżącego promieniowania\n\n-ustawionych przez ciebie kafelków krajobrazów i budynków";
        _text[1114, 7] = "O número nesta engrenagem indica a ecologia atual do planeta. Ela é composta por:\n\n-ecologia base do planeta\n\n-radiação atual\n\n-tiles de paisagem e edifícios que você colocou";
        _text[1114, 8] = "";
        _text[1114, 9] = "";

        // MissionEcology2_15
        _text[1115, 0] = "If the radiation is gray or green, it means that its number is positive.\n\nIf it is yellow or red, it means that it is negative.\n\nThe worse the ecology, the higher the enemy's defense indicator will be and the lower the reward at the end of the mission.";
        _text[1115, 1] = "Если радиация горит серым или зеленым цветом, это означает, что ее число положительно.\n\nЕсли желтым или красным, значит отрицательно.\n\nЧем хуже экология, тем выше будет показатель защиты у врагов и меньше награда в конце миссии.";
        _text[1115, 2] = "Si le niveau de radiation est gris ou vert, il est positif.\n\nS'il est jaune ou rouge, il est négatif.\n\nPlus l'environnement est hostile, plus les défenses ennemies seront élevées et plus la récompense finale sera faible.";
        _text[1115, 3] = "Se la radiazione è grigia o verde, significa che il suo valore è positivo.\n\nSe è gialla o rossa, è negativo.\n\nPiù l'ecologia è pessima, più alta sarà la difesa dei nemici e minore la ricompensa a fine missione.";
        _text[1115, 4] = "Wenn die Farbe der Strahlung grau oder grün ist, bedeutet das, sie wirkt positiv.\n\nGelb oder rot - negativ.\n\nJe schlechter die Ökologie, desto höher ist die Verteidigung der Gegner und desto geringer ist die Belohnung am Missionsende.";
        _text[1115, 5] = "Si la radiación se muestra en gris o verde, significa que su valor es positivo.\n\nSi aparece en amarillo o rojo, es negativo.\n\nCuanto peor sea la ecología, mayor será la defensa de los enemigos y menor la recompensa al final de la misión.";
        _text[1115, 6] = "Jeśli promieniowanie świeci na szaro lub zielono, oznacza to, że jego wartość jest dodatnia.\n\nJeśli na żółto lub czerwono - jest ujemna.\n\nIm gorsza ekologia, tym wyższa będzie obrona wrogów i mniejsza nagroda na końcu misji.";
        _text[1115, 7] = "Se a radiação estiver em cinza ou verde, isso significa que o valor é positivo.\n\nSe estiver em amarelo ou vermelho, é negativo.\n\nQuanto pior a ecologia, maior será a defesa dos inimigos e menor a recompensa ao final da missão.";
        _text[1115, 8] = "";
        _text[1115, 9] = "";

        // MissionClickBuildButton_16
        _text[1116, 0] = "Click on the \"Construct\" button.\n\nA list of available building types on this landscape will open.";
        _text[1116, 1] = "Нажмите на кнопку \"Построить\".\n\nПеред вами откроется список доступных типов зданий на данном ландшафте.";
        _text[1116, 2] = "Cliquez sur le bouton \"Construire\".\n\nUne liste des types de bâtiments disponibles pour ce paysage s'affichera.";
        _text[1116, 3] = "Premi il pulsante \"Costruisci\".\n\nSi aprirà l'elenco dei tipi di edifici disponibili su questo paesaggio.";
        _text[1116, 4] = "Drücke die Schaltfläche \"Bauen\".\n\nDir wird eine Liste von Gebäudetypen angezeigt.";
        _text[1116, 5] = "Pulsa el botón \"Construir\".\n\nSe abrirá una lista de tipos de edificios disponibles en este paisaje.";
        _text[1116, 6] = "Naciśnij przycisk \"Zbuduj\".\n\nOtworzy się lista dostępnych typów budynków na tym krajobrazie.";
        _text[1116, 7] = "Clique no botão \"Construir\".\n\nUma lista de tipos de edifícios disponíveis neste paisagem será aberta.";
        _text[1116, 8] = "";
        _text[1116, 9] = "";

        // MissionSelectBaseTypeButton_17
        _text[1117, 0] = "There is only one type of building available for construction on the \"Base Foundation\" terrain tile.\n\nSelect a building type to reveal the available buildings for construction.";
        _text[1117, 1] = "На тайле ландшафта \"Фундамент базы\" доступен только один тип зданий для постройки.\n\nВыберите тип здания, чтобы открыть доступные здания для постройки.";
        _text[1117, 2] = "Un seul type de bâtiment est constructible sur la dalle paysagère \"Fondations\".\n\nSélectionnez un type de bâtiment pour afficher les bâtiments disponibles.";
        _text[1117, 3] = "Sul tassello paesaggio \"Fondazione della Base\" è disponibile un solo tipo di edifici da costruire.\n\nSeleziona il tipo di edificio per vedere gli edifici disponibili.";
        _text[1117, 4] = "Auf der Kachel \"Basisfundament\" ist nur ein Typ verfügbar.\n\nWähle den Typ, um die verfügbaren Gebäude zu öffnen.";
        _text[1117, 5] = "En el mosaico de paisaje \"Cimientos de la base\" solo hay un tipo de edificio disponible para construir.\n\nSelecciona el tipo de edificio para ver los edificios disponibles.";
        _text[1117, 6] = "Na kafelku krajobrazu \"Fundament bazy\" dostępny jest tylko jeden typ budynków do postawienia.\n\nWybierz typ budynku, aby otworzyć dostępne budynki do postawienia.";
        _text[1117, 7] = "No tile de paisagem \"Fundação da Base\", apenas um tipo de edifício está disponível para construção.\n\nSelecione o tipo de edifício para ver os edifícios disponíveis.";
        _text[1117, 8] = "";
        _text[1117, 9] = "";

        // MissionSelectSettlementBuildingItem_18
        _text[1118, 0] = "Hover over the \"Settlement\" building to display the resources required to build it.";
        _text[1118, 1] = "Наведите курсор на здание \"Поселение\", чтобы отобразить необходимые для его строительства ресурсы.";
        _text[1118, 2] = "Passez votre souris sur le bâtiment de  \"Règlement\" pour afficher les ressources nécessaires à sa construction.";
        _text[1118, 3] = "Passa il cursore sull'edificio \"Insediamento\" per visualizzare le risorse necessarie alla costruzione.";
        _text[1118, 4] = "Fahre mit der Maus über das Gebäude \"Siedlung\". Im Panel siehst du, welche Ressourcen zum Bau benötigt werden.";
        _text[1118, 5] = "Pasa el cursor sobre el edificio \"Asentamiento\" para ver los recursos necesarios para construirlo.";
        _text[1118, 6] = "Najedź kursorem na budynek \"Osada\", aby wyświetlić zasoby potrzebne do jego budowy.";
        _text[1118, 7] = "Passe o cursor sobre o edifício \"Assentamento\" para mostrar os recursos necessários para construí-lo.";
        _text[1118, 8] = "";
        _text[1118, 9] = "";

        // MissionOpenResourcePanel_19
        _text[1119, 0] = "Open the resource panel.";
        _text[1119, 1] = "Откройте панель ресурсов.";
        _text[1119, 2] = "Ouvrez le panneau des ressources.";
        _text[1119, 3] = "Apri il pannello risorse.";
        _text[1119, 4] = "Öffne das Ressourcenpanel.";
        _text[1119, 5] = "Abre el panel de recursos.";
        _text[1119, 6] = "Otwórz panel zasobów.";
        _text[1119, 7] = "Abra o painel de recursos.";
        _text[1119, 8] = "";
        _text[1119, 9] = "";

        // MissionBuildSettlement_20
        _text[1120, 0] = "You have enough resources to build.\n\nClick on the \"Settlement\" card to start building.";
        _text[1120, 1] = "Вам хватает ресурсов на постройку.\n\nНажмите на карту \"Поселение\" чтобы начать строительство.";
        _text[1120, 2] = "Vous disposez de suffisamment de ressources pour construire.\n\nCliquez sur la carte \"Règlement\" pour commencer la construction.";
        _text[1120, 3] = "Hai abbastanza risorse per costruire.\n\nFai clic sulla carta \"Insediamento\" per iniziare la costruzione.";
        _text[1120, 4] = "Du hast genug Ressourcen.\n\nKlicke auf die Karte \"Siedlung\", um mit dem Bau zu beginnen.";
        _text[1120, 5] = "Tienes suficientes recursos para construir.\n\nPulsa la carta \"Asentamiento\" para iniciar la construcción.";
        _text[1120, 6] = "Masz wystarczająco zasobów na budowę.\n\nKliknij kartę \"Osada\", aby rozpocząć budowę.";
        _text[1120, 7] = "Você tem recursos suficientes para construir.\n\nClique no card \"Assentamento\" para iniciar a construção.";
        _text[1120, 8] = "";
        _text[1120, 9] = "";

        // MissionBuildingDescription_21
        _text[1121, 0] = "Below the building tile you can see a blue slider.\n\nIt gradually increases, increasing the health of the building, until it is built.";
        _text[1121, 1] = "Под тайлом здания вы можете заметить синий слайдер.\n\nОн постепенно увеличивается, повышая здоровье здания, до тех пор, пока оно не будет построено.";
        _text[1121, 2] = "Vous remarquerez un curseur bleu sous la tuile du bâtiment.\n\nIl se remplit progressivement, augmentant ainsi la santé du bâtiment, jusqu'à ce que sa construction soit terminée.";
        _text[1121, 3] = "Sotto il tassello dell'edificio puoi notare un indicatore blu.\n\nAumenta gradualmente, incrementando la salute dell'edificio finché non è completato.";
        _text[1121, 4] = "Unter der Kachel des Gebäudes siehst du einen blauen Regler.\n\nEr füllt sich allmählich - das ist die Gesundheit des Gebäudes.\n\nSobald der Regler voll ist, ist das Gebäude fertig.";
        _text[1121, 5] = "Bajo el mosaico del edificio puedes ver un deslizador azul.\n\nAumenta gradualmente, incrementando la salud del edificio hasta que se construya.";
        _text[1121, 6] = "Pod kafelkiem budynku możesz zauważyć niebieski suwak.\n\nStopniowo rośnie, zwiększając zdrowie budynku, aż do zakończenia budowy.";
        _text[1121, 7] = "Abaixo do tile do edifício, você pode notar um slider azul.\n\nEle aumenta gradualmente, elevando a vida do edifício até que a construção seja concluída.";
        _text[1121, 8] = "";
        _text[1121, 9] = "";

        // MissionBuildingDescription2_22
        _text[1122, 0] = "While the building is being constructed, it is vulnerable.\n\nIt can be attacked by enemies.\n\nThe health slider will begin to decrease until the health reaches zero and the building is destroyed.";
        _text[1122, 1] = "Пока здание строится, оно уязвимо.\n\nЕго могут начать атаковать враги.\n\nСлайдер здоровья начнет опускаться, пока здоровье не дойдет до нуля и здание будет уничтожено.";
        _text[1122, 2] = "Pendant la construction, le bâtiment est vulnérable.\n\nDes ennemis peuvent l'attaquer.Son niveau de santé diminuera progressivement jusqu'à ce qu'il atteigne zéro et soit détruit.";
        _text[1122, 3] = "Finché l'edificio è in costruzione, è vulnerabile.\n\nI nemici possono iniziare ad attaccarlo.\n\nL'indicatore della salute scenderà finché la salute non arriverà a zero e l'edificio verrà distrutto.";
        _text[1122, 4] = "Während der Bauzeit ist ein Gebäude verwundbar.\n\nGegner können es angreifen.\n\nWenn der Gesundheitsregler auf Null fällt, wird das Gebäude zerstört.";
        _text[1122, 5] = "Mientras el edificio se construye, es vulnerable.\n\nLos enemigos pueden empezar a atacarlo.\n\nEl deslizador de salud bajará hasta que llegue a cero y el edificio sea destruido.";
        _text[1122, 6] = "Gdy budynek jest w trakcie budowy, jest podatny na ataki.\n\nWrogowie mogą zacząć go atakować.\n\nSuwak zdrowia zacznie spadać, aż zdrowie dojdzie do zera i budynek zostanie zniszczony.";
        _text[1122, 7] = "Enquanto o edifício está sendo construído, ele é vulnerável.\n\nOs inimigos podem começar a atacá-lo.\n\nO slider de vida começa a descer até a vida chegar a zero e o edifício ser destruído.";
        _text[1122, 8] = "";
        _text[1122, 9] = "";

        // MissionAfterBaseSetStartTimer_23
        _text[1123, 0] = "Once the base is completed, the countdown will begin.\n\nTime is measured in days.\n\nEach day has 24 ticks.";
        _text[1123, 1] = "После того, как база завершит свое строительство, начнется отсчет времени.\n\nВремя измеряется в днях.\n\nВ каждом дне 24 тика.";
        _text[1123, 2] = "Une fois la base terminée, le compte à rebours commence.\n\nLe temps est mesuré en jours.\n\nChaque jour compte 24 ticks.";
        _text[1123, 3] = "Dopo che la base avrà completato la costruzione, inizierà lo scorrere del tempo.\n\nIl tempo è misurato in giorni.\n\nOgni giorno ha 24 tick.";
        _text[1123, 4] = "Nachdem die Basis fertig gebaut ist, startet der Timer.\n\nDie Zeit wird in Tagen gemessen.\n\nEin Tag besteht aus 24 Ticks.";
        _text[1123, 5] = "Cuando la base termine de construirse, comenzará el conteo del tiempo.\n\nEl tiempo se mide en días.\n\nCada día tiene 24 tics.";
        _text[1123, 6] = "Po zakończeniu budowy bazy rozpocznie się odliczanie czasu.\n\nCzas jest mierzony w dniach.\n\nW każdym dniu są 24 tiki.";
        _text[1123, 7] = "Depois que a base terminar sua construção, a contagem de tempo começará.\n\nO tempo é medido em dias.\n\nCada dia tem 24 ticks.";
        _text[1123, 8] = "";
        _text[1123, 9] = "";

        // MissionPauseGame_24
        _text[1124, 0] = "This is the game speed change panel.\n\nPause the game to plan your next steps.";
        _text[1124, 1] = "Это панель смены скорости игры.\n\nПоставьте игру на паузу, чтобы спланировать свои дальнешие шаги.";
        _text[1124, 2] = "Voici le panneau de contrôle de la vitesse du jeu.\n\nMettez le jeu en pause pour planifier vos prochaines actions.";
        _text[1124, 3] = "Questo è il pannello di cambio velocità del gioco.\n\nMetti il gioco in pausa per pianificare le prossime mosse.";
        _text[1124, 4] = "Im Panel der Spielgeschwindigkeit kannst du die Geschwindigkeit ändern.\n\nDu kannst das Spiel auch pausieren, um deine nächsten Schritte zu planen.";
        _text[1124, 5] = "Este es el panel de velocidad del juego.\n\nPausa el juego para planificar tus próximos pasos.";
        _text[1124, 6] = "To panel zmiany prędkości gry.\n\nWstrzymaj grę, aby zaplanować kolejne kroki.";
        _text[1124, 7] = "Este é o painel de velocidade do jogo.\n\nColoque o jogo em pausa para planejar seus próximos passos.";
        _text[1124, 8] = "";
        _text[1124, 9] = "";

        // MissionSettlementRequiredResurcesDescription_25
        _text[1125, 0] = "Every tick of time, buildings consume/create resources.\n\nIn the tile information window, \"Settlement\" consumes 0.1 stone for every tick of time.\n\nAt the same time, it creates a resource - data fragments.";
        _text[1125, 1] = "Каждый тик времени происходит потребление/создание ресурсов зданиями.\n\nВ окне информации о тайле, \"Поселение\" потребляет 0.1 камня за каждый тик времени.\n\nПри этом создавая ресурс - фрагменты данных.";
        _text[1125, 2] = "À chaque cycle, les bâtiments consomment/créent des ressources.\n\nDans la fenêtre d'informations de la case, \"Règlement\" consomme 0,1 pierre par cycle.Cela crée des ressources - des fragments de données.";
        _text[1125, 3] = "A ogni tick di tempo gli edifici consumano/producono risorse.\n\nNella finestra informazioni del tassello, \"Insediamento\" consuma 0.1 pietra per ogni tick.\n\nAllo stesso tempo produce la risorsa - frammenti di dati.";
        _text[1125, 4] = "Jeder Tick verbrauchen und erzeugen Gebäude Ressourcen.\n\nIm Infofenster der \"Siedlung\" siehst du, dass sie pro Tick 0.1 Stein verbraucht und dabei Datenfragmente erzeugt.";
        _text[1125, 5] = "En cada tic de tiempo, los edificios consumen y/o producen recursos.\n\nEn la ventana de información del mosaico, \"Asentamiento\" consume 0.1 de piedra por cada tic.\n\nA la vez, produce el recurso - fragmentos de datos.";
        _text[1125, 6] = "W każdym tiku czasu budynki zużywają/tworzą zasoby.\n\nW oknie informacji o kafelku \"Osada\" zużywa 0.1 kamienia na każdy tik czasu.\n\nJednocześnie tworzy zasób - fragmenty danych.";
        _text[1125, 7] = "A cada tick de tempo, ocorre consumo/produção de recursos pelos edifícios.\n\nNa janela de informações do tile, \"Assentamento\" consome 0.1 de pedra por tick.\n\nAo mesmo tempo, produz o recurso - fragmentos de dados.";
        _text[1125, 8] = "";
        _text[1125, 9] = "";

        // MissionDataFragmentsDescription_26
        _text[1126, 0] = "A data fragment is needed to study new buildings\n\nYou can get them:\n\n-after completing a mission\n\n-while traveling through space\n\nYou can study new buildings only on a ship.";
        _text[1126, 1] = "Фрагмент данных необходим для изучения новых зданий\n\nВы можете получить их:\n\n-после прохождения миссии\n\n-во время путешествия по космосу\n\nИзучить новые здания можно только на корабле.";
        _text[1126, 2] = "Un fragment de données est nécessaire pour rechercher de nouveaux bâtiments.\n\nVous pouvez l'obtenir:\n\n-après avoir terminé une mission\n\n-lors de vos voyages spatiaux.\n\nLa recherche de nouveaux bâtiments ne peut avoir lieu qu'à bord d'un vaisseau.";
        _text[1126, 3] = "I frammenti di dati sono necessari per ricercare nuovi edifici\n\nPuoi ottenerli:\n\n-dopo aver completato una missione\n\n-durante il viaggio nello spazio\n\nPuoi ricercare nuovi edifici solo sulla nave.";
        _text[1126, 4] = "Datenfragmente werden benötigt, um neue Gebäude zu erforschen.\n\nDu kannst sie erhalten:\n\n-nach Abschluss der Mission\n-während der Reise im Weltraum\n\nNeue Gebäude kannst du nur auf dem Schiff erlernen.";
        _text[1126, 5] = "El fragmento de datos es necesario para investigar nuevos edificios\n\nPuedes obtenerlos:\n\n-al completar una misión\n\n-durante el viaje por el espacio\n\nSolo puedes investigar edificios nuevos en la nave.";
        _text[1126, 6] = "Fragment danych jest potrzebny do badania nowych budynków\n\nMożesz je zdobyć:\n\n-po ukończeniu misji\n\n- podczas podróży w kosmosie\n\nNowe budynki można badać tylko na statku.";
        _text[1126, 7] = "Fragmentos de dados são necessários para pesquisar novos edifícios\n\nVocê pode obtê-los:\n\n-após completar uma missão\n\n-durante a viagem pelo espaço\n\nNovos edifícios só podem ser pesquisados no navio.";
        _text[1126, 8] = "";
        _text[1126, 9] = "";

        // MissionSettlementChangeResourceRequired_27
        _text[1127, 0] = "If you have little stone, but for example a lot of wood.\n\nChange the resource consumed by the building by clicking on the resource icon.";
        _text[1127, 1] = "Если у вас мало камня, но например много дерева.\n\nПоменяйте потребляемый зданием ресурс, нажав на иконку ресурса.";
        _text[1127, 2] = "Si vous avez peu de pierres mais beaucoup de bois, par exemple.\n\nModifiez la ressource consommée par le bâtiment en cliquant sur l'icône de ressource.";
        _text[1127, 3] = "Se hai poca pietra, ma per esempio molto legno,\n\nCambia la risorsa consumata dall'edificio premendo l'icona della risorsa.";
        _text[1127, 4] = "Wenn du wenig Stein hast, aber viel Holz, kannst du die verbrauchte Ressource ändern.\n\nKlicke dafür auf das Ressourcen-Symbol im Infofenster des Gebäudes.";
        _text[1127, 5] = "Si tienes poca piedra, pero por ejemplo mucha madera.\n\nCambia el recurso que consume el edificio pulsando el icono del recurso.";
        _text[1127, 6] = "Jeśli masz mało kamienia, ale na przykład dużo drewna.\n\nZmień zasób zużywany przez budynek, klikając ikonę zasobu.";
        _text[1127, 7] = "Se você tem pouca pedra, mas por exemplo muito madeira.\n\nTroque o recurso consumido pelo edifício clicando no ícone do recurso.";
        _text[1127, 8] = "";
        _text[1127, 9] = "";

        // MissionPauseRequiredProductionResourceDescription_28
        _text[1128, 0] = "While we are in pause mode, time is stopped. Creation and consumption of resources by buildings does not occur.\n\nIf the resource required for work runs out, the building will stop extracting it until the required amount of resources appears again.";
        _text[1128, 1] = "Пока мы находимся в режиме паузы, время остановлено. Создание и потребление ресурсов зданиями не происходит.\n\nЕсли требуемый для работы ресурс закончится. То здание перестанет его добывать до тех пор, пока необходимое кол-во ресурсов снова не появится.";
        _text[1128, 2] = "En mode pause, le temps est figé. Les bâtiments ne produisent ni ne consomment de ressources.\n\nSi une ressource nécessaire vient à manquer, le bâtiment cessera de la produire jusqu'à ce que la quantité requise soit de nouveau disponible.";
        _text[1128, 3] = "Finché siamo in pausa, il tempo è fermo. Gli edifici non producono né consumano risorse.\n\nSe la risorsa necessaria al funzionamento finisce, l'edificio smetterà di estrarla finché la quantità richiesta non sarà di nuovo disponibile.";
        _text[1128, 4] = "Im Pausenmodus steht die Zeit still.\n\nGebäude verbrauchen und erzeugen keine Ressourcen.\n\nWenn eine benötigte Ressource ausgeht, stellt das Gebäude die Förderung ein, bis die erforderliche Menge wieder verfügbar ist.";
        _text[1128, 5] = "Mientras estamos en pausa, el tiempo está detenido. Los edificios no producen ni consumen recursos.\n\nSi el recurso necesario para funcionar se agota, el edificio dejará de producirlo hasta que vuelva a haber la cantidad requerida.";
        _text[1128, 6] = "Gdy jesteśmy w trybie pauzy, czas jest zatrzymany. Budynki nie tworzą ani nie zużywają zasobów.\n\nJeśli zabraknie zasobu wymaganego do pracy, budynek przestanie go pozyskiwać, dopóki potrzebna ilość zasobów nie pojawi się ponownie.";
        _text[1128, 7] = "Enquanto estamos no modo de pausa, o tempo está parado. A produção e o consumo de recursos pelos edifícios não acontecem.\n\nSe o recurso necessário para operar acabar, o edifício deixará de produzi-lo até que a quantidade necessária de recursos apareça novamente.";
        _text[1128, 8] = "";
        _text[1128, 9] = "";

        // MissionAddCardsDescription_29
        _text[1129, 0] = "After building a base, you are guaranteed to receive 1 Forest card and 1 Mountain card, plus two random landscape cards.\n\nEach new day always brings 2 new cards.";
        _text[1129, 1] = "После строительства базы вам гарантировано дается по 1 карте Леса и Горы, а так же две случайные карты ландшафтов.\n\nКаждый новый день всегда приносит 2 новые карты.";
        _text[1129, 2] = "Après avoir construit votre base, vous recevez systématiquement une carte Forêt et une carte Montagne, ainsi que deux cartes Paysage aléatoires.\n\nChaque jour apporte deux nouvelles cartes.";
        _text[1129, 3] = "Dopo aver costruito la base, ricevi garantito 1 carta Foresta e 1 carta Montagna, oltre a due carte paesaggio casuali.\n\nOgni nuovo giorno porta sempre 2 nuove carte.";
        _text[1129, 4] = "Nach dem Bau der Basis erhältst du garantiert je 1 Karte Wald und Berge sowie zwei zufällige Landschaftskarten.\n\nJeder neue Tag bringt immer 2 neue Karten.";
        _text[1129, 5] = "Después de construir la base, recibes garantizado 1 carta de Bosque y 1 de Montaña, además de dos cartas de paisaje aleatorias.\n\nCada nuevo día siempre trae 2 cartas nuevas.";
        _text[1129, 6] = "Po zbudowaniu bazy gwarantowanie otrzymujesz po 1 karcie Las i Góra, a także dwie losowe karty krajobrazów.\n\nKażdy nowy dzień zawsze przynosi 2 nowe karty.";
        _text[1129, 7] = "Após construir a base, você recebe гарантidamente 1 card de Floresta e 1 de Montanha, além de dois cards de paisagem aleatórios.\n\nCada novo dia sempre traz 2 novos cards.";
        _text[1129, 8] = "";
        _text[1129, 9] = "";

        // MissionToggleOffSettlement_30
        _text[1130, 0] = "Temporarily disable the building.\n\nTo save resources for future construction.\n\nIf the building is disabled, it does not extract or consume resources.\n\nAnd also reduces environmental damage.";
        _text[1130, 1] = "Временно отключите работу здания.\n\nЧтобы сэкономить ресурсы для дальнейших построек.\n\nЕсли здание выключено, оно не добывает и не потребляет ресурсы.\n\nА также снижает порчу экологии.";
        _text[1130, 2] = "Désactiver temporairement le bâtiment.\n\nAfin de préserver les ressources pour les constructions futures.\n\nLorsqu'un bâtiment est désactivé, il ne produit ni ne consomme de ressources.Cela contribue également à réduire l'impact environnemental.";
        _text[1130, 3] = "Disattiva temporaneamente l'edificio.\n\nPer risparmiare risorse per le costruzioni successive.\n\nSe l'edificio è spento, non estrae né consuma risorse.\n\nInoltre riduce il deterioramento dell'ecologia.";
        _text[1130, 4] = "Du kannst ein Gebäude vorübergehend deaktivieren.\n\nSo sparst du Ressourcen für weitere Bauten.\n\nWenn ein Gebäude ausgeschaltet ist, verbraucht und produziert es nichts.\n\nAußerdem reduziert es den Verderb der Ökologie.";
        _text[1130, 5] = "Desactiva temporalmente el funcionamiento del edificio.\n\nPara ahorrar recursos para construcciones posteriores.\n\nSi el edificio está apagado, no produce ni consume recursos.\n\nAdemás, reduce la degradación de la ecología.";
        _text[1130, 6] = "Tymczasowo wyłącz pracę budynku.\n\nAby zaoszczędzić zasoby na dalsze budowy.\n\nGdy budynek jest wyłączony, nie pozyskuje ani nie zużywa zasobów.\n\nDodatkowo zmniejsza degradację ekologii.";
        _text[1130, 7] = "Desative temporariamente o funcionamento do edifício.\n\nPara economizar recursos para construções futuras.\n\nSe o edifício estiver desligado, ele não produz nem consome recursos.\n\nE também reduz a degradação da ecologia.";
        _text[1130, 8] = "";
        _text[1130, 9] = "";

        // MissionSelectForestCard_31
        _text[1131, 0] = "It's time to look at the new terrain tiles.\n\nSelect the \"Forest\" card.";
        _text[1131, 1] = "Настало время посмотреть на новые тайлы ландшафта.\n\nВыберите карту \"Лес\".";
        _text[1131, 2] = "Il est temps de découvrir les nouveaux éléments de paysage.\n\nSélectionnez la carte \"Forêt\".";
        _text[1131, 3] = "È ora di vedere i nuovi tasselli del paesaggio.\n\nSeleziona la carta \"Foresta\".";
        _text[1131, 4] = "Jetzt ist es Zeit, neue Landschaftskacheln zu betrachten.\n\nWähle die Karte \"Wald\".";
        _text[1131, 5] = "Es hora de ver nuevos mosaicos de paisaje.\n\nSelecciona la carta \"Bosque\".";
        _text[1131, 6] = "Czas spojrzeć na nowe kafelki krajobrazu.\n\nWybierz kartę \"Las\".";
        _text[1131, 7] = "É hora de ver novos tiles de paisagem.\n\nSelecione o card \"Floresta\".";
        _text[1131, 8] = "";
        _text[1131, 9] = "";

        // MissionSetForestCard_32
        _text[1132, 0] = "This terrain card is a standard 1x1 tile size.\n\n Place the card on the ground.";
        _text[1132, 1] = "Данная карта ландшафта имеет обычный размер 1x1 клетки.\n\nУстановите карту на землю.";
        _text[1132, 2] = "Cette carte de terrain est au format standard 1x1.\n\nPlacez la carte au sol.";
        _text[1132, 3] = "Questa carta paesaggio ha una dimensione standard di 1x1 cella.\n\nPosiziona la carta a terra.";
        _text[1132, 4] = "Diese Landschaftskarte hat eine normale Größe: 1x1.\n\nPlatziere sie auf dem Boden.";
        _text[1132, 5] = "Esta carta de paisaje tiene el tamaño normal de 1x1 casilla.\n\nColoca la carta en el suelo.";
        _text[1132, 6] = "Ta karta krajobrazu ma standardowy rozmiar 1x1 pola.\n\nUmieść kartę na ziemi.";
        _text[1132, 7] = "Este card de paisagem tem o tamanho padrão de 1x1 célula.\n\nColoque o card no chão.";
        _text[1132, 8] = "";
        _text[1132, 9] = "";

        // MissionSelectForestTile_33
        _text[1133, 0] = "Click on the \"Forest\" tile.\n\nTo open the information panel.";
        _text[1133, 1] = "Нажмите на тайл \"Лес\".\n\nЧтобы открыть панель с информацией.";
        _text[1133, 2] = "Cliquez sur la vignette \"Forêt\".\n\nPour ouvrir le panneau d'informations.";
        _text[1133, 3] = "Fai clic sul tassello \"Foresta\".\n\nPer aprire il pannello informazioni.";
        _text[1133, 4] = "Klicke auf die Kachel \"Wald\", um das Info-Panel zu öffnen.";
        _text[1133, 5] = "Haz clic en el mosaico \"Bosque\".\n\nPara abrir el panel de información.";
        _text[1133, 6] = "Kliknij kafelek \"Las\".\n\nAby otworzyć panel informacji.";
        _text[1133, 7] = "Clique no tile \"Floresta\".\n\nPara abrir o painel de informações.";
        _text[1133, 8] = "";
        _text[1133, 9] = "";

        // MissionClickBuildButton_34
        _text[1134, 0] = "Click on the \"Construct\" button.\n\nA list of available building types on this landscape will open.";
        _text[1134, 1] = "Нажмите на кнопку \"Построить\".\n\nПеред вами откроется список доступных типов зданий на данном ландшафте.";
        _text[1134, 2] = "Cliquez sur le bouton \"Construire\".\n\nUne liste des types de bâtiments disponibles pour ce paysage s'affichera.";
        _text[1134, 3] = "Premi il pulsante \"Costruisci\".\n\nSi aprirà l'elenco dei tipi di edifici disponibili su questo paesaggio.";
        _text[1134, 4] = "Drücke die Schaltfläche \"Bauen\".";
        _text[1134, 5] = "Pulsa el botón \"Construir\".\n\nSe abrirá una lista de tipos de edificios disponibles en este paisaje.";
        _text[1134, 6] = "Naciśnij przycisk \"Zbuduj\".\n\nOtworzy się lista dostępnych typów budynków na tym krajobrazie.";
        _text[1134, 7] = "Clique no botão \"Construir\".\n\nUma lista de tipos de edifícios disponíveis neste paisagem será aberta.";
        _text[1134, 8] = "";
        _text[1134, 9] = "";

        // MissionTileForestDescription_35
        _text[1135, 0] = "There are several buildings available for construction on the Forest landscape tile.\n\nIf a building type button is not active, it means that you have not researched any buildings of that type.";
        _text[1135, 1] = "На тайле ландшафта \"Лес\" доступно несколько зданий для постройки.\n\nЕсли кнопка типа здания не активна, это означает, что у вас не изучено ни одно здание в этом типе.";
        _text[1135, 2] = "Plusieurs bâtiments sont disponibles pour la construction sur la tuile de paysage \"Forêt\".\n\nSi le bouton d'un type de bâtiment est inactif, cela signifie que vous n'avez effectué aucune recherche pour ce type de bâtiment.";
        _text[1135, 3] = "Sul tassello paesaggio \"Foresta\" sono disponibili diversi edifici da costruire.\n\nSe il pulsante del tipo di edificio non è attivo, significa che non hai ricercato alcun edificio di quel tipo.";
        _text[1135, 4] = "Auf der Kachel \"Wald\" sind mehrere Gebäude verfügbar.\n\nWenn eine Schaltfläche für einen Gebäudetyp inaktiv ist, bedeutet das, dass du noch kein Gebäude dieses Typs gelernt hast.";
        _text[1135, 5] = "En el mosaico de paisaje \"Bosque\" hay varios edificios disponibles para construir.\n\nSi el botón del tipo de edificio no está activo, significa que no has investigado ningún edificio de ese tipo.";
        _text[1135, 6] = "Na kafelku krajobrazu \"Las\" dostępnych jest kilka budynków do postawienia.\n\nJeśli przycisk typu budynku jest nieaktywny, oznacza to, że nie zbadano żadnego budynku w tym typie.";
        _text[1135, 7] = "No tile de paisagem \"Floresta\", há vários edifícios disponíveis para construir.\n\nSe o botão do tipo de edifício estiver inativo, isso significa que você não pesquisou nenhum edifício desse tipo.";
        _text[1135, 8] = "";
        _text[1135, 9] = "";

        // MissionSelectWoodExtractionTypeButton_36
        _text[1136, 0] = "Select the \"Wood Mining\" building type to reveal the available buildings to build.";
        _text[1136, 1] = "Выберите тип здания \"Добыча Дерева\", чтобы открыть доступные здания для постройки.";
        _text[1136, 2] = "Sélectionnez le type de bâtiment \"Exploitation Forestière\" pour afficher les bâtiments disponibles à construire.";
        _text[1136, 3] = "Seleziona il tipo di edificio \"Estrazione di Legno\" per vedere gli edifici disponibili.";
        _text[1136, 4] = "Wähle den Typ \"Holzgewinnung\", um die verfügbaren Gebäude zu öffnen.";
        _text[1136, 5] = "Selecciona el tipo de edificio \"Extracción de Madera\" para ver los edificios disponibles.";
        _text[1136, 6] = "Wybierz typ budynku \"Pozyskiwanie Drewna\", aby otworzyć dostępne budynki do postawienia.";
        _text[1136, 7] = "Selecione o tipo de edifício \"Extração de Madeira\" para ver os edifícios disponíveis.";
        _text[1136, 8] = "";
        _text[1136, 9] = "";

        // MissionStartConstructionManualWoodMining_37
        _text[1137, 0] = "Click on the \"Manual Mining\" card to start building.";
        _text[1137, 1] = "Нажмите на карту \"Ручная Добыча\", чтобы начать строительство.";
        _text[1137, 2] = "Cliquez sur la carte \"Extraction Manuelle\" pour commencer la construction.";
        _text[1137, 3] = "Fai clic sulla carta \"Estrazione Manuale\" per iniziare la costruzione.";
        _text[1137, 4] = "Klicke auf die Karte \"Manueller Abbau\", um mit dem Bau zu beginnen.";
        _text[1137, 5] = "Pulsa la carta \"Extracción Manual\" para iniciar la construcción.";
        _text[1137, 6] = "Kliknij kartę \"Ręczne Pozyskiwanie\", aby rozpocząć budowę.";
        _text[1137, 7] = "Clique no card \"Extração Manual\" para iniciar a construção.";
        _text[1137, 8] = "";
        _text[1137, 9] = "";

        // MissionDefaultGameSpeed_38
        _text[1138, 0] = "You need to exit the pause to start the building construction process.";
        _text[1138, 1] = "Необходимо выйти из паузы, чтобы запустить процесс строительства здания.";
        _text[1138, 2] = "Vous devez sortir de la pause pour démarrer le processus de construction du bâtiment.";
        _text[1138, 3] = "Devi uscire dalla pausa per avviare la costruzione dell'edificio.";
        _text[1138, 4] = "Du musst die Pause beenden, um den Bauprozess zu starten.";
        _text[1138, 5] = "Debes salir de la pausa para iniciar el proceso de construcción del edificio.";
        _text[1138, 6] = "Musisz wyjść z pauzy, aby uruchomić proces budowy budynku.";
        _text[1138, 7] = "É preciso sair da pausa para iniciar o processo de construção do edifício.";
        _text[1138, 8] = "";
        _text[1138, 9] = "";

        // MissionConstructionStoneExtraction_39
        _text[1139, 0] = "Great, you have a constant supply of wood.\n\nNow set up the \"Mountain\" tile yourself and build a manual stone mining building.";
        _text[1139, 1] = "Отлично, у вас есть постоянная добыча дерева.\n\nТеперь самостоятельно установите тайл \"Гора\" и постройте здание ручной добычи камня.";
        _text[1139, 2] = "Parfait, vous avez un approvisionnement constant en bois.\n\nPlacez maintenant vous-même la dalle \"Montagne\" et construisez un bâtiment pour extraire manuellement la pierre.";
        _text[1139, 3] = "Ottimo, ora hai una produzione costante di legno.\n\nOra posiziona da solo il tassello \"Montagna\" e costruisci l'edificio di estrazione manuale della pietra.";
        _text[1139, 4] = "Sehr gut, du hast nun eine konstante Holzgewinnung.\n\nSetze jetzt selbst die Kachel \"Berg\" und baue das Gebäude für manuellen Steinabbau.";
        _text[1139, 5] = "Genial, ya tienes una extracción constante de madera.\n\nAhora coloca por tu cuenta el mosaico \"Montaña\" y construye el edificio de extracción manual de piedra.";
        _text[1139, 6] = "Świetnie, masz stałe wydobycie drewna.\n\nTeraz samodzielnie umieść kafelek \"Góra\" i zbuduj budynek ręcznego wydobycia kamienia.";
        _text[1139, 7] = "Ótimo, você agora tem uma fonte constante de madeira.\n\nAgora, instale o tile \"Montanha\" por conta própria e construa o edifício de extração manual de pedra.";
        _text[1139, 8] = "";
        _text[1139, 9] = "";

        // MissionCompleteStoneAndWoodExtractionDescription_40
        _text[1140, 0] = "At the moment you are mining two main resources.\n\nBut now it is time to protect the base.";
        _text[1140, 1] = "На данный момент вы добываете два основных ресурса.\n\nНо теперь настало время защитить базу.";
        _text[1140, 2] = "Vous exploitez actuellement deux ressources principales.\n\nMais il est temps maintenant de défendre votre base.";
        _text[1140, 3] = "Al momento stai ottenendo due risorse principali.\n\nMa ora è il momento di difendere la base.";
        _text[1140, 4] = "Im Moment förderst du zwei Grundressourcen.\n\nJetzt ist es an der Zeit, die Basis zu verteidigen.";
        _text[1140, 5] = "En este momento extraes dos recursos principales.\n\nPero ahora es hora de defender la base.";
        _text[1140, 6] = "W tej chwili wydobywasz dwa podstawowe zasoby.\n\nAle teraz nadszedł czas, aby obronić bazę.";
        _text[1140, 7] = "No momento, você está coletando dois recursos principais.\n\nMas agora é hora de proteger a base.";
        _text[1140, 8] = "";
        _text[1140, 9] = "";

        // MissionConstructionBallista_41
        _text[1141, 0] = "You need to place a landscape tile on which the building type \"Structures: Attacking\" will be available.\n\nThen build a building on it - \"Ballista\".";
        _text[1141, 1] = "Вам необходимо поставить тайл ландшафта на котором будет доступен тип здания \"Сооружения: Атакующие\".\n\nЗатем постройте на нем здание - \"Баллиста\".";
        _text[1141, 2] = "Vous devez placer une case de terrain sur laquelle le type de bâtiment \"Structures: Attaquant\" sera disponible.Construisez ensuite un bâtiment « Balista » sur cette case.";
        _text[1141, 3] = "Devi posizionare un tassello del paesaggio sul quale sia disponibile il tipo di edificio \"Strutture: Attacco\".\n\nPoi costruisci su di esso l'edificio \"Ballista\".";
        _text[1141, 4] = "Du musst eine Landschaftskachel platzieren, auf der der Gebäudetyp \"Bauwerke: Angriff\" verfügbar ist.\n\nBaue darauf das Gebäude - \"Balliste\".";
        _text[1141, 5] = "Debes colocar un mosaico de paisaje en el que esté disponible el tipo de edificio \"Estructuras: Atacantes\".\n\nLuego construye en él el edificio \"Balista\".";
        _text[1141, 6] = "Musisz postawić kafelek krajobrazu, na którym będzie dostępny typ budowli \"Budowle: Atakujące\".\n\nNastępnie zbuduj na nim budynek - \"Balista\".";
        _text[1141, 7] = "Você precisa colocar um tile de paisagem onde o tipo de edifício \"Estruturas: Ataque\" esteja disponível.\n\nEm seguida, construa nele o edifício - \"Balista\".";
        _text[1141, 8] = "";
        _text[1141, 9] = "";

        // MissionBallistaDescription_42
        _text[1142, 0] = "Attack structures have a limited attack range.\n\nTry to place them near your base and mining buildings so that enemies cannot easily attack them.";
        _text[1142, 1] = "У атакующих сооружений ограниченный радиус атаки.\n\nСтарайтесь размещать их возле базы и добывающих зданий, чтобы враги не смогли беспрепятственно атаковать их.";
        _text[1142, 2] = "Les structures d'attaque ont une portée limitée.\n\nEssayez de les placer près de votre base et de vos bâtiments miniers pour empêcher les ennemis de les attaquer sans entrave.";
        _text[1142, 3] = "Le strutture offensive hanno un raggio d'attacco limitato.\n\nCerca di posizionarle vicino alla base e agli edifici di estrazione, così i nemici non potranno attaccarli indisturbati.";
        _text[1142, 4] = "Angriffs-Bauwerke haben eine begrenzte Reichweite.\n\nPlatziere sie in der Nähe der Basis und der Fördergebäude, damit Gegner sie nicht ungehindert angreifen können.";
        _text[1142, 5] = "Las estructuras atacantes tienen un radio de ataque limitado.\n\nIntenta colocarlas cerca de la base y de los edificios de extracción para que los enemigos no puedan atacarlos sin obstáculos.";
        _text[1142, 6] = "Budowle atakujące mają ograniczony zasięg ataku.\n\nStaraj się umieszczać je blisko bazy i budynków wydobywczych, aby wrogowie nie mogli bez przeszkód ich atakować.";
        _text[1142, 7] = "As estruturas ofensivas têm um raio de ataque limitado.\n\nTente posicioná-las perto da base e dos edifícios de extração, para que os inimigos não consigam atacá-los livremente.";
        _text[1142, 8] = "";
        _text[1142, 9] = "";

        // MissionToggleOnSettlement_43
        _text[1143, 0] = "Now that the base is protected, enable work in the \"Settlement\" building.\n\nIt is very important to start mining data fragments.";
        _text[1143, 1] = "Теперь когда база защищена, включите работу в здании \"Поселение\".\n\nОчень важно начать добывать фрагменты данных.";
        _text[1143, 2] = "La base étant désormais sécurisée, activez les travaux dans le bâtiment \"Règlement\".\n\nIl est crucial de commencer l'extraction des fragments de données.";
        _text[1143, 3] = "Ora che la base è protetta, attiva il funzionamento dell'edificio \"Insediamento\".\n\nÈ molto importante iniziare a produrre frammenti di dati.";
        _text[1143, 4] = "Jetzt, da die Basis geschützt ist, aktiviere die Arbeit im Gebäude \"Siedlung\".\n\nEs ist sehr wichtig, mit der Gewinnung von Datenfragmenten zu beginnen.";
        _text[1143, 5] = "Ahora que la base está protegida, activa el funcionamiento del edificio \"Asentamiento\".\n\nEs muy importante empezar a extraer fragmentos de datos.";
        _text[1143, 6] = "Teraz, gdy baza jest chroniona, włącz pracę w budynku \"Osada\".\n\nBardzo ważne jest rozpocząć wydobywanie fragmentów danych.";
        _text[1143, 7] = "Agora que a base está protegida, ative o funcionamento do edifício \"Assentamento\".\n\nÉ muito importante começar a produzir fragmentos de dados.";
        _text[1143, 8] = "";
        _text[1143, 9] = "";

        // MissionEnergyBeamDescription_44
        _text[1144, 0] = "When you place any landscape tile, you receive a resource-beam energy.\n\nIt's needed to replace a card in your hand with a random one and to destroy already placed landscape tiles.\n\nIt can also be obtained if extra cards begin to disappear when the deck is full.";
        _text[1144, 1] = "Когда вы устанавливаете любой тайл ландшафта, то получаете ресурс - энергия луча.\n\nОна требуется для замены карты в руке на случайную и для уничтожения уже установленных тайлов ландшафта.\n\nТак же ее можно получить, если лишние карты начинают исчезать, когда колода переполняется.";
        _text[1144, 2] = "Lorsque vous posez une tuile de paysage, vous gagnez une ressource appelée Énergie de rayon.\n\nElle est nécessaire pour remplacer une carte de votre main par une carte aléatoire et pour détruire des tuiles de paysage existantes.\n\nVous pouvez également en gagner lorsque des cartes en surplus disparaissent de votre pioche.";
        _text[1144, 3] = "Quando posizioni qualsiasi tassello del paesaggio, ottieni una risorsa - energia del raggio.\n\nServe per sostituire una carta in mano con una casuale e per distruggere i tasselli del paesaggio già posizionati.\n\nPuoi ottenerla anche quando le carte in eccesso iniziano a scomparire perché il mazzo è pieno.";
        _text[1144, 4] = "Wenn du eine beliebige Landschaftskachel platzierst, erhältst du eine Ressource - Strahlenergie.\n\nSie wird benötigt, um eine Karte in deiner Hand durch eine zufällige zu ersetzen und um bereits platzierte Landschaftskacheln zu zerstören.\n\nDu kannst sie auch erhalten, wenn überzählige Karten verschwinden, sobald das Deck überläuft.";
        _text[1144, 5] = "Cada vez que colocas cualquier mosaico de paisaje, obtienes el recurso: energía del rayo.\n\nSe necesita para reemplazar una carta en tu mano por una aleatoria y para destruir mosaicos de paisaje ya colocados.\n\nTambién se puede obtener cuando las cartas sobrantes empiezan a desaparecer al desbordarse el mazo.";
        _text[1144, 6] = "Gdy umieszczasz dowolny kafelek krajobrazu, otrzymujesz zasób - energia wiązki.\n\nJest ona potrzebna do wymiany karty w ręce na losową oraz do niszczenia już ustawionych kafelków krajobrazu.\n\nMożna ją też zdobyć, gdy nadmiarowe karty zaczynają znikać, kiedy talia się przepełnia.";
        _text[1144, 7] = "Sempre que você coloca qualquer tile de paisagem, você recebe um recurso - energia do feixe.\n\nEla é necessária para substituir uma carta na mão por uma aleatória e para destruir tiles de paisagem já colocados.\n\nEla também pode ser obtida quando cartas excedentes começam a desaparecer, quando o baralho fica cheio demais.";
        _text[1144, 8] = "";
        _text[1144, 9] = "";

        // MissionTileCombineDescription1_45
        _text[1145, 0] = "Proper placement of terrain tiles is the key to successfully completing the mission.\n\nYou can combine them to create new tiles.";
        _text[1145, 1] = "Правильная установка тайлов ландшафта - ключ к успешному прохождению миссии.\n\nВы можете комбинировать их между собой, создавая новые тайлы.";
        _text[1145, 2] = "Le placement précis des tuiles de terrain est essentiel à la réussite de la mission.\n\nVous pouvez les combiner pour créer de nouvelles tuiles.";
        _text[1145, 3] = "Il posizionamento corretto dei tasselli del paesaggio è la chiave per completare con successo la missione.\n\nPuoi combinarli tra loro, creando nuovi tasselli.";
        _text[1145, 4] = "Das richtige Platzieren von Landschaftskacheln ist der Schlüssel zum erfolgreichen Abschluss einer Mission.\n\nDu kannst sie miteinander kombinieren und so neue Kacheln erschaffen.";
        _text[1145, 5] = "La colocación correcta de los mosaicos de paisaje es la clave para completar la misión con éxito.\n\nPuedes combinarlos entre sí, creando nuevos mosaicos.";
        _text[1145, 6] = "Prawidłowe ustawianie kafelków krajobrazu to klucz do pomyślnego ukończenia misji.\n\nMożesz je łączyć ze sobą, tworząc nowe kafelki.";
        _text[1145, 7] = "A colocação correta dos tiles de paisagem é a chave para concluir a missão com sucesso.\n\nVocê pode combiná-los entre si, criando novos tiles.";
        _text[1145, 8] = "";
        _text[1145, 9] = "";

        // MissionTileCombineDescription2_46
        _text[1146, 0] = "For example, if you place a plain close to a mountain.\n\nThe plain tile will turn into a meadow.\n\nOn it, you will be able to create other types of buildings and improve the ecology.";
        _text[1146, 1] = "Например, если поставить равнину вплотную к горе.\n\nТайл равнины превратится в луг.\n\nНа нем вы сможете создавать другие типы зданий и повысите экологию.";
        _text[1146, 2] = "Par exemple, si vous placez une plaine à côté d'une montagne,la case de plaine se transformera en prairie.\n\nVous pourrez alors y construire d'autres types de bâtiments et améliorer l'écosystème.";
        _text[1146, 3] = "Per esempio, se posizioni una pianura a ridosso di una montagna.\n\nIl tassello di pianura si trasformerà in un prato.\n\nSu di esso potrai costruire altri tipi di edifici e aumenterai l'ecologia.";
        _text[1146, 4] = "Zum Beispiel, wenn du eine Ebene direkt an einen Berg legst.\n\nDie Ebenen-Kachel verwandelt sich in eine Wiese.\n\nDort kannst du andere Gebäudetypen bauen und die Ökologie verbessern.";
        _text[1146, 5] = "Por ejemplo, si colocas una llanura pegada a una montaña.\n\nEl mosaico de llanura se convertirá en pradera.\n\nEn ella podrás construir otros tipos de edificios y aumentarás la ecología.";
        _text[1146, 6] = "Na przykład, jeśli postawisz równinę tuż obok góry.\n\nKafelek równiny zmieni się w łąkę.\n\nNa niej będziesz mógł tworzyć inne typy budynków i poprawisz ekologię.";
        _text[1146, 7] = "Por exemplo, se você colocar uma planície bem encostada a uma montanha.\n\nO tile de planície se transformará em um prado.\n\nNele, você poderá construir outros tipos de edifícios e aumentar a ecologia.";
        _text[1146, 8] = "";
        _text[1146, 9] = "";

        // MissionTileCombineDescription3_47
        _text[1147, 0] = "But be careful when setting a desert near a forest.\n\nThe forest will turn into an oasis and wood production will decrease.";
        _text[1147, 1] = "Но будьте осторожны в установке пустыни возле леса.\n\nТаким образом лес превратится в оазис и добыча дерева уменьшится.";
        _text[1147, 2] = "Mais attention à ne pas placer un désert près d'une forêt.\n\nCela transformera la forêt en oasis et réduira la production de bois.";
        _text[1147, 3] = "Ma fai attenzione a posizionare un deserto vicino a una foresta.\n\nIn questo modo la foresta si trasformerà in un'oasi e la produzione di legno diminuirà.";
        _text[1147, 4] = "Aber sei vorsichtig, wenn du eine Wüste neben einen Wald legst.\n\nSo verwandelt sich der Wald in eine Oase und die Holzgewinnung sinkt.";
        _text[1147, 5] = "Pero ten cuidado al colocar el desierto cerca del bosque.\n\nAsí el bosque se convertirá en un oasis y la extracción de madera disminuirá.";
        _text[1147, 6] = "Ale uważaj, stawiając pustynię obok lasu.\n\nW ten sposób las zamieni się w oazę, a wydobycie drewna zmaleje.";
        _text[1147, 7] = "Mas tenha cuidado ao colocar deserto perto da floresta.\n\nAssim, a floresta se transformará em um oásis e a extração de madeira diminuirá.";
        _text[1147, 8] = "";
        _text[1147, 9] = "";

        // MissionSelectTileWithResourceExtraction_48
        _text[1148, 0] = "Click on the tile where the resource is being mined.";
        _text[1148, 1] = "Нажмите на тайл, где происходит добыча ресурса.";
        _text[1148, 2] = "Cliquez sur la case où la ressource est extraite.";
        _text[1148, 3] = "Fai clic sul tassello in cui avviene l'estrazione della risorsa.";
        _text[1148, 4] = "Klicke auf die Kachel, auf der eine Ressource gefördert wird.";
        _text[1148, 5] = "Haz clic en el mosaico donde se está extrayendo el recurso.";
        _text[1148, 6] = "Kliknij kafelek, na którym odbywa się wydobycie zasobu.";
        _text[1148, 7] = "Clique no tile onde o recurso está sendo extraído.";
        _text[1148, 8] = "";
        _text[1148, 9] = "";

        // MissionProductionModifierDescription_49
        _text[1149, 0] = "Look at the resource production modifier.\n\nThe modifier may differ on different tiles.\n\nThus, there are profitable and unprofitable tiles for extracting a particular resource.";
        _text[1149, 1] = "Посмотрите на модификатор производства ресурсов.\n\nНа разных тайлах модификатор может отличаться.\n\nТаким образом есть выгодные и не выгодные тайлы для добычи того, или иного ресурса.";
        _text[1149, 2] = "Consultez le modificateur de production de ressources.\n\nCe modificateur peut varier d'une case à l'autre.\n\nPar conséquent, certaines cases sont plus rentables que d'autres pour la production d'une ressource donnée.";
        _text[1149, 3] = "Guarda il modificatore di produzione delle risorse.\n\nSu tasselli diversi il modificatore può variare.\n\nQuindi esistono tasselli più o meno vantaggiosi per estrarre questa o quella risorsa.";
        _text[1149, 4] = "Sieh dir den Produktionsmodifikator an.\n\nAuf verschiedenen Kacheln kann der Modifikator unterschiedlich sein.\n\nSo gibt es vorteilhafte und unvorteilhafte Kacheln für die Förderung einer bestimmten Ressource.";
        _text[1149, 5] = "Mira el modificador de producción de recursos.\n\nEn diferentes mosaicos el modificador puede variar.\n\nAsí existen mosaicos más y menos rentables para extraer uno u otro recurso.";
        _text[1149, 6] = "Spójrz na modyfikator produkcji zasobów.\n\nNa różnych kafelkach modyfikator może się różnić.\n\nDzięki temu są kafelki bardziej i mniej opłacalne do wydobycia danego zasobu.";
        _text[1149, 7] = "Veja o modificador de produção de recursos.\n\nEm tiles diferentes, o modificador pode variar.\n\nAssim, há tiles vantajosos e desvantajosos para a extração de cada recurso.";
        _text[1149, 8] = "";
        _text[1149, 9] = "";

        // MissionEventPanel_50
        _text[1150, 0] = "This is the event panel.\n\nYou will periodically notice event icons in it.\n\nThe scale is 3 days long.\n\nYou will receive a notification with information about the event 1 day before it.";
        _text[1150, 1] = "Это панель событий.\n\nВ ней периодически вы будете замечать иконки событий.\n\nДлина шкалы равна 3 дням.\n\nЗа 1 день до события вам будет приходить уведомление с информацией о нем.";
        _text[1150, 2] = "Voici le panneau des événements.Vous y verrez régulièrement des icônes d'événements.\n\nLa barre d'événements s'étend sur 3 jours.\n\nVous recevrez une notification contenant des informations sur l'événement la veille.";
        _text[1150, 3] = "Questo è il pannello degli eventi.\n\nQui noterai periodicamente le icone degli eventi.\n\nLa lunghezza della barra è pari a 3 giorni.\n\n1 giorno prima dell'evento riceverai una notifica con le informazioni.";
        _text[1150, 4] = "Das ist das Ereignis-Panel.\n\nDarin wirst du von Zeit zu Zeit Ereignis-Symbole sehen.\n\nDie Länge der Leiste entspricht 3 Tagen.\n\n1 Tag vor einem Ereignis erhältst du eine Benachrichtigung mit Informationen dazu.";
        _text[1150, 5] = "Este es el panel de eventos.\n\nEn él, periódicamente verás iconos de eventos.\n\nLa longitud de la escala equivale a 3 días.\n\nUn día antes del evento recibirás una notificación con información sobre él.";
        _text[1150, 6] = "To panel wydarzeń.\n\nBędziesz tu okresowo zauważać ikony wydarzeń.\n\nDługość skali wynosi 3 dni.\n\nNa 1 dzień przed wydarzeniem otrzymasz powiadomienie z informacjami o nim.";
        _text[1150, 7] = "Este é o painel de eventos.\n\nNele, você periodicamente verá ícones de eventos.\n\nO comprimento da barra é de 3 dias.\n\n1 dia antes do evento, você receberá uma notificação com informações sobre ele.";
        _text[1150, 8] = "";
        _text[1150, 9] = "";

        // MissionOpenSkillsPanel_51
        _text[1151, 0] = "Open the skills panel.";
        _text[1151, 1] = "Откройте панель умений.";
        _text[1151, 2] = "Ouvrez le panneau des compétences.";
        _text[1151, 3] = "Apri il pannello delle abilità.";
        _text[1151, 4] = "Öffne das Fähigkeiten-Panel.";
        _text[1151, 5] = "Abre el panel de habilidades.";
        _text[1151, 6] = "Otwórz panel umiejętności.";
        _text[1151, 7] = "Abra o painel de habilidades.";
        _text[1151, 8] = "";
        _text[1151, 9] = "";

        // MissionSkillsPanelDescription_52
        _text[1152, 0] = "Here are the skills available for use.\n\nThey can be purchased from merchants or unlocked with \"Shards\" in the hangar when starting a new game.";
        _text[1152, 1] = "Здесь находятся доступные для использования умения.\n\nИх можно приобрести у торговцев или купить за \"Осколок\" в ангаре при старте новой игры.";
        _text[1152, 2] = "Les compétences disponibles se trouvent ici.\n\nVous pouvez les acheter auprès des marchands ou avec des \"Eclats\" dans le hangar lors du lancement d'une nouvelle partie.";
        _text[1152, 3] = "Qui si trovano le abilità disponibili.\n\nPuoi ottenerle dai mercanti oppure acquistarle per \"Scheggia\" nell'hangar all'inizio di una nuova partita.";
        _text[1152, 4] = "Hier befinden sich die Fähigkeiten, die du benutzen kannst.\n\nDu kannst sie bei Händlern erwerben oder im Hangar beim Start eines neuen Spiels für \"Splitter\" kaufen.";
        _text[1152, 5] = "Aquí están las habilidades disponibles para usar.\n\nSe pueden adquirir a los comerciantes o comprarlas por \"Esquirla\" en el hangar al comenzar una nueva partida.";
        _text[1152, 6] = "Tutaj znajdują się umiejętności dostępne do użycia.\n\nMożna je zdobyć u handlarzy lub kupić za \"Odłamek\" w hangarze na początku nowej gry.";
        _text[1152, 7] = "Aqui estão as habilidades disponíveis para uso.\n\nElas podem ser adquiridas com comerciantes ou compradas por \"Estilhaço\" no hangar ao iniciar um novo jogo.";
        _text[1152, 8] = "";
        _text[1152, 9] = "";

        // MissionShardsDescription_53
        _text[1153, 0] = "Shards are all that remain after the end of a game.\n\nUse them to buy items in the hangar that will allow you to travel further and further.";
        _text[1153, 1] = "Осколки - это все, что остается у вас после окончания игры.\n\nИспользуйте их для покупки предметов в ангаре, с помощью которых вы сможете путешествовать все дальше, и дальше.";
        _text[1153, 2] = "Les fragments sont tout ce qui vous reste après avoir terminé une partie.\n\nUtilisez-les pour acheter des objets dans le hangar, ce qui vous permettra de voyager toujours plus loin.";
        _text[1153, 3] = "Le Schegge sono tutto ciò che ti rimane dopo la fine della partita.\n\nUsale per acquistare oggetti nell'hangar, che ti permetteranno di viaggiare sempre più lontano.";
        _text[1153, 4] = "Splitter sind alles, was dir nach dem Ende des Spiels bleibt.\n\nNutze sie, um im Hangar Gegenstände zu kaufen, mit denen du immer weiter und weiter reisen kannst.";
        _text[1153, 5] = "Las esquirlas son todo lo que te queda tras terminar la partida.\n\nÚsalas para comprar objetos en el hangar, con los que podrás viajar cada vez más lejos.";
        _text[1153, 6] = "Odłamki - to wszystko, co zostaje po zakończeniu gry.\n\nUżywaj ich do kupowania przedmiotów w hangarze, dzięki którym będziesz mógł podróżować coraz dalej i dalej.";
        _text[1153, 7] = "Estilhaços são tudo o que fica com você após o fim do jogo.\n\nUse-os para comprar itens no hangar, que permitirão que você viaje cada vez mais longe.";
        _text[1153, 8] = "";
        _text[1153, 9] = "";

        // MissionPrepareAttack_54
        _text[1154, 0] = "On day 7, the first group of enemies is expected.\n\nPrepare your base for battle.\n\nFor example, by building additional ballistas.";
        _text[1154, 1] = "На 7 день ожидается первая группа врагов.\n\nПодготовьте вашу базу к битве.\n\nНапример построив дополнительные баллисты.";
        _text[1154, 2] = "Le premier groupe d'ennemis est attendu le 7e jour.\n\nPréparez votre base au combat.\n\nPar exemple, en construisant des balistes supplémentaires.";
        _text[1154, 3] = "Il 7° giorno è previsto il primo gruppo di nemici.\n\nPrepara la tua base alla battaglia.\n\nPer esempio costruendo balliste aggiuntive.";
        _text[1154, 4] = "Am 7. Tag wird die erste Gegnergruppe erwartet.\n\nBereite deine Basis auf die Schlacht vor.\n\nZum Beispiel, indem du zusätzliche Ballisten baust.";
        _text[1154, 5] = "El día 7 se espera el primer grupo de enemigos.\n\nPrepara tu base para la batalla.\n\nPor ejemplo, construyendo balistas adicionales.";
        _text[1154, 6] = "7 dnia spodziewana jest pierwsza grupa wrogów.\n\nPrzygotuj bazę do bitwy.\n\nNa przykład budując dodatkowe balisty.";
        _text[1154, 7] = "No 7º dia, a primeira onda de inimigos é esperada.\n\nPrepare sua base para a batalha.\n\nPor exemplo, construindo balistas adicionais.";
        _text[1154, 8] = "";
        _text[1154, 9] = "";

        // MissionDoubleTripleGameSpeedDescription_55
        _text[1155, 0] = "You can speed up the game by 2 or 3 times if you want to quickly accumulate resources or wait for some time.";
        _text[1155, 1] = "Вы можете ускорить игру в 2 или 3 раза, если хотите быстро накопить ресурсы или переждать некоторое время.";
        _text[1155, 2] = "Vous pouvez accélérer le jeu de 2 ou 3 fois si vous souhaitez accumuler rapidement des ressources ou attendre un peu.";
        _text[1155, 3] = "Puoi accelerare il gioco di 2 o 3 volte, se vuoi accumulare rapidamente risorse o semplicemente far passare un po' di tempo.";
        _text[1155, 4] = "Du kannst das Spiel 2- oder 3-fach beschleunigen, wenn du schnell Ressourcen ansammeln oder einfach etwas Zeit überbrücken möchtest.";
        _text[1155, 5] = "Puedes acelerar el juego a 2 o 3 si quieres acumular recursos rápidamente o esperar un rato.";
        _text[1155, 6] = "Możesz przyspieszyć grę 2 lub 3 razy, jeśli chcesz szybko zgromadzić zasoby albo przeczekać trochę czasu.";
        _text[1155, 7] = "Você pode acelerar o jogo em 2 ou 3, se quiser acumular recursos rapidamente ou apenas esperar um tempo.";
        _text[1155, 8] = "";
        _text[1155, 9] = "";

        // MissionBuildingTakeDamage_56
        _text[1156, 0] = "After your building is attacked.\n\nIt will display a health slider.";
        _text[1156, 1] = "После того как ваше здание атакуют.\n\nУ него отобразится слайдер здоровья.";
        _text[1156, 2] = "Après l'attaque de votre bâtiment,un indicateur de santé s'affichera.";
        _text[1156, 3] = "Dopo che il tuo edificio viene attaccato,\n\ncomparirà una barra della salute.";
        _text[1156, 4] = "Nachdem dein Gebäude angegriffen wurde,\n\nwird ein Gesundheitsbalken angezeigt.";
        _text[1156, 5] = "Después de que tu edificio sea atacado.\n\nSe mostrará su deslizador de salud.";
        _text[1156, 6] = "Gdy twój budynek zostanie zaatakowany.\n\nPojawi się na nim suwak zdrowia.";
        _text[1156, 7] = "Depois que um edifício seu for atacado.\n\nUma barra de vida será exibida.";
        _text[1156, 8] = "";
        _text[1156, 9] = "";

        // MissionSelectTileObjectForRepair_57
        _text[1157, 0] = "You can repair the building.\n\nClick on it to open the tile information panel.";
        _text[1157, 1] = "Вы можете починить здание.\n\nНажмите на него, чтобы открыть панель с информацией о тайле.";
        _text[1157, 2] = "Vous pouvez réparer le bâtiment.\n\nCliquez dessus pour ouvrir le panneau d'informations de la tuile.";
        _text[1157, 3] = "Puoi riparare l'edificio.\n\nFai clic su di esso per aprire il pannello con le informazioni del tassello.";
        _text[1157, 4] = "Du kannst ein Gebäude reparieren.\n\nKlicke darauf, um das Informationspanel der Kachel zu öffnen.";
        _text[1157, 5] = "Puedes reparar el edificio.\n\nHaz clic en él para abrir el panel con información del mosaico.";
        _text[1157, 6] = "Możesz naprawić budynek.\n\nKliknij go, aby otworzyć panel z informacjami o kafelku.";
        _text[1157, 7] = "Você pode reparar um edifício.\n\nClique nele para abrir o painel de informações do tile.";
        _text[1157, 8] = "";
        _text[1157, 9] = "";

        // MissionClickBuildButton_58
        _text[1158, 0] = "In the panel, click the \"Construct\" button.";
        _text[1158, 1] = "В панеле нажмите кнопку \"Построить\".";
        _text[1158, 2] = "Dans le panneau, cliquez sur le bouton \"Construire\".";
        _text[1158, 3] = "Nel pannello premi il pulsante \"Costruisci\".";
        _text[1158, 4] = "Klicke im Panel auf die Schaltfläche \"Bauen\".";
        _text[1158, 5] = "En el panel, pulsa el botón \"Construir\".";
        _text[1158, 6] = "W panelu kliknij przycisk \"Zbuduj\".";
        _text[1158, 7] = "No painel, clique no botão \"Construir\".";
        _text[1158, 8] = "";
        _text[1158, 9] = "";

        // MissionRepairBuilding_59
        _text[1159, 0] = "A panel with a card of repairs for the current building immediately opened in front of you.\n\nRepair the building.";
        _text[1159, 1] = "Перед вами сразу открылась панель с картой починки текущего здания.\n\nПочините здание.";
        _text[1159, 2] = "Un panneau affichant un plan de réparation du bâtiment actuel s'ouvre immédiatement devant vous.\n\nRéparez le bâtiment.";
        _text[1159, 3] = "Si è aperto subito il pannello con la carta di riparazione dell'edificio attuale.\n\nRipara l'edificio.";
        _text[1159, 4] = "Vor dir hat sich sofort ein Panel mit der Reparaturkarte für das aktuelle Gebäude geöffnet.\n\nRepariere das Gebäude.";
        _text[1159, 5] = "Se abrió inmediatamente el panel con la carta de reparación del edificio actual.\n\nRepara el edificio.";
        _text[1159, 6] = "Od razu otworzył się panel z kartą naprawy bieżącego budynku.\n\nNapraw budynek.";
        _text[1159, 7] = "Você já verá o painel com a carta de reparo do edifício atual.\n\nRepare o edifício.";
        _text[1159, 8] = "";
        _text[1159, 9] = "";

        // MissionUpgradeBuildingDescription1_60
        _text[1160, 0] = "If you already have a building on the tile and have studied other buildings of the same type.\n\nThen when you click on the \"Construct\" button, in addition to repairing the current building, you will find building cards nearby that you can upgrade the current building to.";
        _text[1160, 1] = "Если у вас уже есть здание на тайле и изучены другие здания такого же типа.\n\nТогда при нажатии на кнопку \"Построить\", помимо ремонта текущего здания, рядом вы обнаружите карточки зданий в которые вы можете улучшить текущее здание.";
        _text[1160, 2] = "Si vous possédez déjà un bâtiment sur la case et avez effectué des recherches sur d'autres bâtiments du même type.\n\nAlors, lorsque vous cliquerez sur le bouton \"Construire\", en plus de réparer le bâtiment actuel, vous verrez apparaître à proximité des cartes de bâtiments que vous pourrez améliorer.";
        _text[1160, 3] = "Se hai già un edificio sul tassello e hai ricercato altri edifici dello stesso tipo,\n\nquando premi il pulsante \"Costruisci\", oltre alla riparazione dell'edificio attuale vedrai anche le carte degli edifici in cui puoi potenziarlo.";
        _text[1160, 4] = "Wenn du bereits ein Gebäude auf der Kachel hast und andere Gebäude desselben Typs erforscht hast,\n\ndann findest du beim Klick auf \"Bauen\" neben der Reparatur des aktuellen Gebäudes auch Karten der Gebäude, zu denen du es aufrüsten kannst.";
        _text[1160, 5] = "Si ya tienes un edificio en el mosaico y has investigado otros edificios del mismo tipo.\n\nEntonces, al pulsar \"Construir\", además de reparar el edificio actual, verás al lado las cartas de edificios a los que puedes mejorar el edificio actual.";
        _text[1160, 6] = "Jeśli na kafelku stoi już budynek i zbadano inne budynki tego samego typu.\n\nWtedy po kliknięciu przycisku \"Zbuduj\", oprócz naprawy bieżącego budynku, obok zobaczysz karty budynków, na które możesz ulepszyć obecny budynek.";
        _text[1160, 7] = "Se você já tiver um edifício no tile e tiver pesquisado outros edifícios do mesmo tipo.\n\nEntão, ao clicar no botão \"Construir\", além de reparar o edifício atual, você verá ao lado cartas de edifícios para os quais pode aprimorar o edifício atual.";
        _text[1160, 8] = "";
        _text[1160, 9] = "";

        // MissionUpgradeBuildingDescription2_61
        _text[1161, 0] = "When upgrading a building, you automatically receive some of the resources spent on the previously standing building.\n\nTherefore, it is not necessary to destroy the building before constructing its improved version.";
        _text[1161, 1] = "При улучшении здания, вы автоматически получаете часть ресурсов затраченных на ранее стоящее здание.\n\nПоэтому не обязательно уничтожать здание перед постройкой его улучшенной версии.";
        _text[1161, 2] = "Lors de la rénovation d'un bâtiment, vous récupérez automatiquement une partie des ressources dépensées pour le bâtiment d'origine.\n\nIl n'est donc pas nécessaire de détruire un bâtiment avant de construire sa version améliorée.";
        _text[1161, 3] = "Quando potenzi un edificio, recuperi automaticamente una parte delle risorse spese per l'edificio precedente.\n\nQuindi non è necessario distruggerlo prima di costruirne la versione potenziata.";
        _text[1161, 4] = "Beim Aufrüsten eines Gebäudes bekommst du automatisch einen Teil der Ressourcen zurück, die für das vorherige Gebäude ausgegeben wurden.\n\nDeshalb musst du ein Gebäude nicht zerstören, bevor du seine verbesserte Version baust.";
        _text[1161, 5] = "Al mejorar un edificio, recuperas automáticamente parte de los recursos gastados en el edificio anterior.\n\nPor eso no es necesario destruirlo antes de construir su versión mejorada.";
        _text[1161, 6] = "Podczas ulepszania budynku automatycznie odzyskujesz część zasobów wydanych na poprzedni budynek.\n\nDlatego nie trzeba niszczyć budynku przed postawieniem jego ulepszonej wersji.";
        _text[1161, 7] = "Ao aprimorar um edifício, você automaticamente recebe de volta parte dos recursos gastos no edifício anterior.\n\nPor isso, não é necessário destruir o edifício antes de construir sua versão aprimorada.";
        _text[1161, 8] = "";
        _text[1161, 9] = "";

        // MissionShipWeaponModeActive_62
        _text[1162, 0] = "If you are having trouble dealing with enemies, simply turn on ship mode to activate your weapons.";
        _text[1162, 1] = "Если вы не справляетесь с врагами, просто включите режим корабля, чтобы активировать оружие.";
        _text[1162, 2] = "Si vous avez des difficultés à gérer les ennemis, activez simplement le mode navire pour activer vos armes.";
        _text[1162, 3] = "Se non riesci a gestire i nemici, passa alla modalità nave per attivare le armi.";
        _text[1162, 4] = "Wenn du mit den Gegnern nicht fertig wirst, aktiviere einfach den Schiffsmodus, um die Waffen zu nutzen.";
        _text[1162, 5] = "Si no puedes con los enemigos, activa el modo de nave para habilitar las armas.";
        _text[1162, 6] = "Jeśli nie radzisz sobie z wrogami, po prostu włącz tryb statku, aby aktywować broń.";
        _text[1162, 7] = "Se você não está conseguindo lidar com os inimigos, basta ativar o modo do navio para habilitar as armas.";
        _text[1162, 8] = "";
        _text[1162, 9] = "";

        // MissionShipWeaponModeDescription_63
        _text[1163, 0] = "Weapon ammo is given out at the start of each mission and has a limited supply.\n\nUse it only in emergency situations.\n\nTo improve weapon damage, you need to visit the engineer on the star map.";
        _text[1163, 1] = "Боеприпасы оружия выдаются в начале каждой миссии и имею ограниченный запас.\n\nИспользуйте их только в экстренных ситуациях.\n\nЧтобы улучшить урон оружия, вам необходимо посетить инженера на звездной карте.";
        _text[1163, 2] = "Les munitions sont distribuées au début de chaque mission et sont en quantité limitée.\n\nUtilisez-les uniquement en cas d'urgence.\n\nPour améliorer les dégâts de vos armes, vous devez consulter un ingénieur sur la Carte Stellaire.";
        _text[1163, 3] = "Le munizioni delle armi vengono fornite all'inizio di ogni missione e sono limitate.\n\nUsale solo in situazioni di emergenza.\n\nPer aumentare il danno delle armi, devi visitare l'ingegnere sulla mappa stellare.";
        _text[1163, 4] = "Munition wird zu Beginn jeder Mission ausgegeben und ist nur begrenzt verfügbar.\n\nNutze sie nur in Notfällen.\n\nUm den Waffenschaden zu verbessern, musst du den Ingenieur auf der Sternkarte besuchen.";
        _text[1163, 5] = "La munición de las armas se entrega al inicio de cada misión y tiene una reserva limitada.\n\nÚsala solo en situaciones de emergencia.\n\nPara mejorar el daño de las armas, debes visitar al ingeniero en el mapa estelar.";
        _text[1163, 6] = "Amunicja do broni jest przyznawana na początku każdej misji i ma ograniczony zapas.\n\nUżywaj jej tylko w sytuacjach awaryjnych.\n\nAby zwiększyć obrażenia broni, musisz odwiedzić inżyniera na mapie gwiezdnej.";
        _text[1163, 7] = "A munição das armas é fornecida no início de cada missão e tem um estoque limitado.\n\nUse-a apenas em situações de emergência.\n\nPara aumentar o dano das armas, você precisa visitar o engenheiro no mapa estelar.";
        _text[1163, 8] = "";
        _text[1163, 9] = "";

        // MissionPlanetModeActive_64
        _text[1164, 0] = "The left mouse button is responsible for shooting the left weapon, the right mouse button is responsible for the right.\n\nWeapons cannot shoot while the game is paused.\n\nIt is better to save ammo at this point.\n\nExit ship mode, back to planet mode.";
        _text[1164, 1] = "Левая кнопка мыши отвечает за выстрелы левым оружие, правая кнопка мыши за правым.\n\nОружие не может стрелять, пока игра находится на паузе.\n\nНа данный момент лучше сэкономить патроны.\n\nВыйдите из режима корабля, обратно в режим планеты.";
        _text[1164, 2] = "Le bouton gauche de la souris tire avec l'arme gauche, et le bouton droit avec l'arme droite.\n\nLes armes ne peuvent pas tirer lorsque le jeu est en pause.\n\nPour l'instant, il est préférable d'économiser vos munitions.\n\nQuittez le mode vaisseau et retournez au mode planète.";
        _text[1164, 3] = "Il tasto sinistro del mouse spara con l'arma sinistra, il tasto destro con quella destra.\n\nLe armi non possono sparare mentre il gioco è in pausa.\n\nPer ora è meglio risparmiare munizioni.\n\nEsci dalla modalità nave e torna alla modalità pianeta.";
        _text[1164, 4] = "Die linke Maustaste feuert die linke Waffe, die rechte Maustaste die rechte.\n\nWaffen können nicht feuern, solange das Spiel pausiert ist.\n\nIm Moment ist es besser, Munition zu sparen.\n\nVerlasse den Schiffsmodus und kehre zurück in den Planetenmodus.";
        _text[1164, 5] = "El botón izquierdo del ratón dispara el arma izquierda; el botón derecho, el arma derecha.\n\nLas armas no pueden disparar mientras el juego está en pausa.\n\nPor ahora es mejor ahorrar munición.\n\nSal del modo de nave y vuelve al modo de planeta.";
        _text[1164, 6] = "Lewy przycisk myszy odpowiada za strzały z lewego uzbrojenia, prawy przycisk myszy - z prawego.\n\nBroń nie może strzelać, gdy gra jest wstrzymana.\n\nNa ten moment lepiej oszczędzić amunicję.\n\nWyjdź z trybu statku z powrotem do trybu planety.";
        _text[1164, 7] = "O botão esquerdo do mouse dispara a arma esquerda; o botão direito, a arma direita.\n\nAs armas não podem atirar enquanto o jogo estiver em pausa.\n\nPor enquanto, é melhor economizar munição.\n\nSaia do modo do navio e volte para o modo do planeta.";
        _text[1164, 8] = "";
        _text[1164, 9] = "";

        // MissionDefeatMissionDescription_65
        _text[1165, 0] = "If your base is destroyed, the mission is failed.\n\nYou will lose 1 AI core.\n\nBut you will be able to restart the mission until all the cores are used up.";
        _text[1165, 1] = "Если ваша база будет уничтожена, то миссия будет считаться проваленной.\n\nВы потеряете 1 ядро ИИ.\n\nНо сможете начинать миссию сначала до тех пор, пока не закончатся все ядра.";
        _text[1165, 2] = "Si votre base est détruite, la mission sera considérée comme un échec.\n\nVous perdrez un noyau d'IA.\n\nVous pouvez toutefois recommencer la mission jusqu'à épuisement de tous vos noyaux.";
        _text[1165, 3] = "Se la tua base viene distrutta, la missione sarà considerata fallita.\n\nPerderai 1 nucleo IA.\n\nMa potrai ricominciare la missione finché non termineranno tutti i nuclei.";
        _text[1165, 4] = "Wenn deine Basis zerstört wird, gilt die Mission als gescheitert.\n\nDu verlierst 1 KI-Kern.\n\nDu kannst die Mission jedoch neu starten, solange noch Kerne übrig sind.";
        _text[1165, 5] = "Si tu base es destruida, la misión se considerará fallida.\n\nPerderás 1 núcleo de IA.\n\nPero podrás reiniciar la misión hasta que se agoten todos los núcleos.";
        _text[1165, 6] = "Jeśli twoja baza zostanie zniszczona, misja zostanie uznana za nieudaną.\n\nStracisz 1 rdzeń SI.\n\nAle będziesz mógł rozpocząć misję od nowa, dopóki nie skończą się wszystkie rdzenie.";
        _text[1165, 7] = "Se sua base for destruída, a missão será considerada fracassada.\n\nVocê perderá 1 núcleo de IA.\n\nMas poderá reiniciar a missão até que todos os núcleos acabem.";
        _text[1165, 8] = "";
        _text[1165, 9] = "";

        // MissionGoodLuckDescription_66
        _text[1166, 0] = "Complete all objectives to successfully complete the mission.\n\nDespite the objectives, try to accumulate as many data fragments as possible during the mission.\n\nIf you do not keep up with the advancement in technology, your journey will end quickly...";
        _text[1166, 1] = "Выполните все цели, чтобы успешно завершить миссию.\n\nНесмотря на поставленные цели, старайтесь накопить за миссию как можно больше фрагментов данных.\n\nЕсли вы не будете поспевать за прогрессом в технологиях, ваше путешествие закончится быстро...";
        _text[1166, 2] = "Accomplissez tous les objectifs pour réussir la mission.\n\nQuel que soit votre objectif, essayez de collecter un maximum de données pendant la mission.\n\nSi vous ne suivez pas le rythme des avancées technologiques, votre voyage s'achèvera rapidement...";
        _text[1166, 3] = "Completa tutti gli obiettivi per terminare con successo la missione.\n\nNonostante gli obiettivi, cerca di accumulare il maggior numero possibile di frammenti di dati durante la missione.\n\nSe non terrai il passo con i progressi tecnologici, il tuo viaggio finirà in fretta...";
        _text[1166, 4] = "Erfülle alle Ziele, um die Mission erfolgreich abzuschließen.\n\nTrotz der Ziele versuche, während der Mission so viele Datenfragmente wie möglich zu sammeln.\n\nWenn du beim Technologie-Fortschritt nicht Schritt hältst, endet deine Reise schnell...";
        _text[1166, 5] = "Completa todos los objetivos para finalizar la misión con éxito.\n\nA pesar de los objetivos establecidos, intenta acumular durante la misión la mayor cantidad posible de fragmentos de datos.\n\nSi no sigues el ritmo del progreso tecnológico, tu viaje terminará rápido...";
        _text[1166, 6] = "Wykonaj wszystkie cele, aby pomyślnie ukończyć misję.\n\nMimo wyznaczonych celów staraj się zebrać w misji jak najwięcej fragmentów danych.\n\nJeśli nie będziesz nadążać z postępem technologii, twoja podróż szybko się zakończy...";
        _text[1166, 7] = "Conclua todos os objetivos para terminar a missão com sucesso.\n\nApesar dos objetivos definidos, tente acumular o máximo possível de fragmentos de dados durante a missão.\n\nSe você não acompanhar o progresso das tecnologias, sua viagem terminará rápido...";
        _text[1166, 8] = "";
        _text[1166, 9] = "";

        // SpaceOpenLearningPanel_67
        _text[1167, 0] = "You have completed the mission and earned data fragments.\n\nNow open the research panel";
        _text[1167, 1] = "Вы прошли миссию и заработали фрагменты данных.\n\nТеперь откройте панель изучений";
        _text[1167, 2] = "Vous avez terminé la mission et obtenu des fragments de données.\n\nOuvrez maintenant le panneau de recherche.";
        _text[1167, 3] = "Hai completato la missione e ottenuto frammenti di dati.\n\nOra apri il pannello delle ricerche";
        _text[1167, 4] = "Du hast die Mission abgeschlossen und Datenfragmente verdient.\n\nÖffne jetzt das Forschungs-Panel.";
        _text[1167, 5] = "Has completado la misión y has ganado fragmentos de datos.\n\nAhora abre el panel de investigación";
        _text[1167, 6] = "Ukończyłeś misję i zdobyłeś fragmenty danych.\n\nTeraz otwórz panel badań";
        _text[1167, 7] = "Você completou a missão e ganhou fragmentos de dados.\n\nAgora, abra o painel de pesquisas";
        _text[1167, 8] = "";
        _text[1167, 9] = "";

        // SpaceSelectNotLearnBuilding_68
        _text[1168, 0] = "Here you can see all types of buildings available for study.\n\nSee how many data fragments you have mined and select any unexplored building.";
        _text[1168, 1] = "Здесь вы можете увидеть все типы зданий доступные для изучения.\n\nПосмотрите сколько фрагментов данных вы добыли и выберите любое не изученное здание.";
        _text[1168, 2] = "Vous pouvez consulter ici tous les types de bâtiments disponibles pour la recherche.\n\nVérifiez le nombre de fragments de données extraits et sélectionnez un bâtiment inexploré.";
        _text[1168, 3] = "Qui puoi vedere tutti i tipi di edifici disponibili per la ricerca.\n\nControlla quanti frammenti di dati hai ottenuto e scegli un edificio non ancora ricercato.";
        _text[1168, 4] = "Hier kannst du alle Gebäudetypen sehen, die zur Erforschung verfügbar sind.\n\nSieh nach, wie viele Datenfragmente du gesammelt hast, und wähle ein beliebiges noch nicht erforschtes Gebäude.";
        _text[1168, 5] = "Aquí puedes ver todos los tipos de edificios disponibles para investigar.\n\nMira cuántos fragmentos de datos has obtenido y elige cualquier edificio no investigado.";
        _text[1168, 6] = "Tutaj możesz zobaczyć wszystkie typy budynków dostępne do zbadania.\n\nSpójrz, ile fragmentów danych zdobyłeś, i wybierz dowolny niezbadany budynek.";
        _text[1168, 7] = "Aqui você pode ver todos os tipos de edifícios disponíveis para pesquisa.\n\nVeja quantos fragmentos de dados você coletou e escolha qualquer edifício ainda não pesquisado.";
        _text[1168, 8] = "";
        _text[1168, 9] = "";

        // SpaceLearnBuilding_69
        _text[1169, 0] = "If there are enough data fragments, start the study by clicking the button.\n\nSelect another building if there are not enough resources or preliminary research of another building is required.";
        _text[1169, 1] = "Если фрагментов данных достаточно, начните изучение, нажав на кнопку.\n\nВыберите другое сооружение, если ресурсов не хватает или требуется предварительное исследование другого здания.";
        _text[1169, 2] = "S'il y a suffisamment de fragments de données, lancez la recherche en cliquant sur le bouton.\n\nSélectionnez un autre bâtiment si vous manquez de ressources ou si vous devez d'abord en rechercher un autre.";
        _text[1169, 3] = "Se hai abbastanza frammenti di dati, avvia la ricerca premendo il pulsante.\n\nScegli un'altra struttura se le risorse non bastano o se è richiesta la ricerca preliminare di un altro edificio.";
        _text[1169, 4] = "Wenn genügend Datenfragmente vorhanden sind, starte die Forschung, indem du die Schaltfläche drückst.\n\nWähle ein anderes Bauwerk, wenn die Ressourcen nicht reichen oder eine vorherige Forschung erforderlich ist.";
        _text[1169, 5] = "Si tienes suficientes fragmentos de datos, inicia la investigación pulsando el botón.\n\nElige otra estructura si faltan recursos o se requiere una investigación previa de otro edificio.";
        _text[1169, 6] = "Jeśli masz wystarczająco fragmentów danych, rozpocznij badanie, klikając przycisk.\n\nWybierz inną konstrukcję, jeśli brakuje zasobów lub wymagane jest wcześniejsze badanie innego budynku.";
        _text[1169, 7] = "Se houver fragmentos de dados suficientes, inicie a pesquisa clicando no botão.\n\nEscolha outra estrutura se os recursos forem insuficientes ou se for necessária uma pesquisa prévia de outro edifício.";
        _text[1169, 8] = "";
        _text[1169, 9] = "";

        // SpaceLearnBuildingDescription_70
        _text[1170, 0] = "Great, you've explored a new building.\n\nIt will now be available for construction during missions.";
        _text[1170, 1] = "Отлично, вы изучили новое здание.\n\nТеперь оно станет доступно для постройки на миссиях.";
        _text[1170, 2] = "Parfait, vous avez exploré le nouveau bâtiment.\n\nIl sera désormais possible de le construire lors des missions.";
        _text[1170, 3] = "Ottimo, hai ricercato un nuovo edificio.\n\nOra sarà disponibile per la costruzione nelle missioni.";
        _text[1170, 4] = "Sehr gut, du hast ein neues Gebäude erforscht.\n\nJetzt ist es in Missionen zum Bau verfügbar.";
        _text[1170, 5] = "Genial, has investigado un edificio nuevo.\n\nAhora estará disponible para construir en las misiones.";
        _text[1170, 6] = "Świetnie, zbadałeś nowy budynek.\n\nTeraz będzie dostępny do zbudowania w misjach.";
        _text[1170, 7] = "Ótimo, você pesquisou um novo edifício.\n\nAgora ele ficará disponível para construção nas missões.";
        _text[1170, 8] = "";
        _text[1170, 9] = "";

        // SpaceExploreSpace_71.
        _text[1171, 0] = "Return to the map and explore space.\n\nTo find a habitable planet...";
        _text[1171, 1] = "Возвращайтесь на карту и исследуйте космос.\n\nЧтобы найти пригодную для жизни планету...";
        _text[1171, 2] = "Retournez à la carte et explorez l'espace.\n\nPour trouver une planète habitable...";
        _text[1171, 3] = "Torna alla mappa ed esplora lo spazio.\n\nPer trovare un pianeta adatto alla vita...";
        _text[1171, 4] = "Kehre zur Karte zurück und erforsche den Weltraum.\n\nUm einen bewohnbaren Planeten zu finden...";
        _text[1171, 5] = "Vuelve al mapa y explora el espacio.\n\nPara encontrar un planeta apto para la vida...";
        _text[1171, 6] = "Wróć na mapę i badaj kosmos.\n\nAby znaleźć planetę nadającą się do życia...";
        _text[1171, 7] = "Volte ao mapa e explore o espaço.\n\nPara encontrar um planeta habitável...";
        _text[1171, 8] = "";
        _text[1171, 9] = "";

        #endregion

        #region Buildings

        _text[1200, 0] = "Settlement";
        _text[1200, 1] = "Поселение";
        _text[1200, 2] = "Règlement";
        _text[1200, 3] = "Insediamento";
        _text[1200, 4] = "Siedlung";
        _text[1200, 5] = "Asentamiento";
        _text[1200, 6] = "Osada";
        _text[1200, 7] = "Assentamento";
        _text[1200, 8] = "";
        _text[1200, 9] = "";

        _text[1201, 0] = "Town";
        _text[1201, 1] = "Город";
        _text[1201, 2] = "Ville";
        _text[1201, 3] = "Città";
        _text[1201, 4] = "Stadt";
        _text[1201, 5] = "Ciudad";
        _text[1201, 6] = "Miasto";
        _text[1201, 7] = "Cidade";
        _text[1201, 8] = "";
        _text[1201, 9] = "";

        _text[1202, 0] = "Industrial City";
        _text[1202, 1] = "Промышленный Город";
        _text[1202, 2] = "Ville Industrielle";
        _text[1202, 3] = "Città Industriale";
        _text[1202, 4] = "Industriestadt";
        _text[1202, 5] = "Ciudad Industrial";
        _text[1202, 6] = "Miasto Przemysłowe";
        _text[1202, 7] = "Cidade Industrial";
        _text[1202, 8] = "";
        _text[1202, 9] = "";

        _text[1203, 0] = "Megapolis";
        _text[1203, 1] = "Мегаполис";
        _text[1203, 2] = "Mégalopole";
        _text[1203, 3] = "Megalopoli";
        _text[1203, 4] = "Megalopolis";
        _text[1203, 5] = "Metrópolis";
        _text[1203, 6] = "Megalopolis";
        _text[1203, 7] = "Metrópole";
        _text[1203, 8] = "";
        _text[1203, 9] = "";

        _text[1204, 0] = "Wind Generator";
        _text[1204, 1] = "Ветряной Генератор";
        _text[1204, 2] = "Générateur Eolien";
        _text[1204, 3] = "Generatore Eolico";
        _text[1204, 4] = "Windgenerator";
        _text[1204, 5] = "Generador Eólico";
        _text[1204, 6] = "Generator Wiatrowy";
        _text[1204, 7] = "Gerador Eólico";
        _text[1204, 8] = "";
        _text[1204, 9] = "";

        _text[1205, 0] = "Steam Engine";
        _text[1205, 1] = "Паровой Двигатель";
        _text[1205, 2] = "Locomotive à Vapeur";
        _text[1205, 3] = "Motore a Vapore";
        _text[1205, 4] = "Dampfmotor";
        _text[1205, 5] = "Motor de Vapor";
        _text[1205, 6] = "Silnik Parowy";
        _text[1205, 7] = "Motor a Vapor";
        _text[1205, 8] = "";
        _text[1205, 9] = "";

        _text[1206, 0] = "Solar Panel";
        _text[1206, 1] = "Солнечная Панель";
        _text[1206, 2] = "Panneau Solaire";
        _text[1206, 3] = "Pannello Solare";
        _text[1206, 4] = "Solarmodul";
        _text[1206, 5] = "Panel Solar";
        _text[1206, 6] = "Panel Słoneczny";
        _text[1206, 7] = "Painel Solar";
        _text[1206, 8] = "";
        _text[1206, 9] = "";

        _text[1207, 0] = "Thermal Power Plant";
        _text[1207, 1] = "Теплоэлектростанция";
        _text[1207, 2] = "Centrale Thermique";
        _text[1207, 3] = "Centrale Termoelettrica";
        _text[1207, 4] = "Wärmekraftwerk";
        _text[1207, 5] = "Central Térmica";
        _text[1207, 6] = "Elektrownia Cieplna";
        _text[1207, 7] = "Usina Termelétrica";
        _text[1207, 8] = "";
        _text[1207, 9] = "";

        _text[1208, 0] = "Manual Mining";
        _text[1208, 1] = "Ручная Добыча";
        _text[1208, 2] = "Exploitation Manuelle";
        _text[1208, 3] = "Estrazione Manuale";
        _text[1208, 4] = "Manueller Abbau";
        _text[1208, 5] = "Extracción Manual";
        _text[1208, 6] = "Ręczne Wydobycie";
        _text[1208, 7] = "Extração Manual";
        _text[1208, 8] = "";
        _text[1208, 9] = "";

        _text[1209, 0] = "Coal Mine";
        _text[1209, 1] = "Угольная Шахта";
        _text[1209, 2] = "Mine de Charbon";
        _text[1209, 3] = "Miniera di Carbone";
        _text[1209, 4] = "Kohlemine";
        _text[1209, 5] = "Mina de Carbón";
        _text[1209, 6] = "Kopalnia Węgla";
        _text[1209, 7] = "Mina de Carvão";
        _text[1209, 8] = "";
        _text[1209, 9] = "";

        _text[1210, 0] = "Steam Rig";
        _text[1210, 1] = "Паровая Установка";
        _text[1210, 2] = "Installation de Vapeur";
        _text[1210, 3] = "Impianto a Vapore";
        _text[1210, 4] = "Dampfanlage";
        _text[1210, 5] = "Planta de Vapor";
        _text[1210, 6] = "Instalacja Parowa";
        _text[1210, 7] = "Instalação a Vapor";
        _text[1210, 8] = "";
        _text[1210, 9] = "";

        _text[1211, 0] = "Drilling Rig";
        _text[1211, 1] = "Буровая Установка";
        _text[1211, 2] = "Plateforme de Forage";
        _text[1211, 3] = "Impianto di Perforazione";
        _text[1211, 4] = "Bohranlage";
        _text[1211, 5] = "Plataforma de Perforación";
        _text[1211, 6] = "Wiertnia";
        _text[1211, 7] = "Plataforma de Perfuração";
        _text[1211, 8] = "";
        _text[1211, 9] = "";

        _text[1212, 0] = "Manual Mining";
        _text[1212, 1] = "Ручная Добыча";
        _text[1212, 2] = "Exploitation Manuelle";
        _text[1212, 3] = "Estrazione Manuale";
        _text[1212, 4] = "Manueller Abbau";
        _text[1212, 5] = "Extracción Manual";
        _text[1212, 6] = "Ręczne Wydobycie";
        _text[1212, 7] = "Extração Manual";
        _text[1212, 8] = "";
        _text[1212, 9] = "";

        _text[1213, 0] = "Mine";
        _text[1213, 1] = "Рудник";
        _text[1213, 2] = "Le mien";
        _text[1213, 3] = "Miniera";
        _text[1213, 4] = "Erzmine";
        _text[1213, 5] = "Mina";
        _text[1213, 6] = "Kopalnia Rudy";
        _text[1213, 7] = "Mina de Minério";
        _text[1213, 8] = "";
        _text[1213, 9] = "";

        _text[1214, 0] = "Steam-Powered Drill";
        _text[1214, 1] = "Паровой Бур";
        _text[1214, 2] = "Foreuse à Vapeur";
        _text[1214, 3] = "Trivella a Vapore";
        _text[1214, 4] = "Dampfbohrer";
        _text[1214, 5] = "Taladro de Vapor";
        _text[1214, 6] = "Wiertło Parowe";
        _text[1214, 7] = "Broca a Vapor";
        _text[1214, 8] = "";
        _text[1214, 9] = "";

        _text[1215, 0] = "Bucket-wheel Excavator";
        _text[1215, 1] = "Многоковшовый Экскаватор";
        _text[1215, 2] = "Pelle à Godets Multiples";
        _text[1215, 3] = "Escavatore a Benna Multipla";
        _text[1215, 4] = "Schaufelradbagger";
        _text[1215, 5] = "Excavadora de Rueda de Cangilones";
        _text[1215, 6] = "Koparka Wieloczerpakowa";
        _text[1215, 7] = "Escavadeira de Roda de Caçambas";
        _text[1215, 8] = "";
        _text[1215, 9] = "";

        _text[1216, 0] = "Manual Mining";
        _text[1216, 1] = "Ручная Добыча";
        _text[1216, 2] = "Exploitation Minière Manuelle";
        _text[1216, 3] = "Estrazione Manuale";
        _text[1216, 4] = "Manueller Abbau";
        _text[1216, 5] = "Extracción Manual";
        _text[1216, 6] = "Ręczne Wydobycie";
        _text[1216, 7] = "Extração Manual";
        _text[1216, 8] = "";
        _text[1216, 9] = "";

        _text[1217, 0] = "Table Saw";
        _text[1217, 1] = "Распилочный Стол";
        _text[1217, 2] = "Table de Sciage";
        _text[1217, 3] = "Banco da Sega";
        _text[1217, 4] = "Sägetisch";
        _text[1217, 5] = "Mesa de Corte";
        _text[1217, 6] = "Stół do Rozpiłowywania";
        _text[1217, 7] = "Mesa de Corte";
        _text[1217, 8] = "";
        _text[1217, 9] = "";

        _text[1218, 0] = "Steam Sawmill";
        _text[1218, 1] = "Паровая Лесопилка";
        _text[1218, 2] = "Scierie à Vapeur";
        _text[1218, 3] = "Segheria a Vapore";
        _text[1218, 4] = "Dampf-Sägewerk";
        _text[1218, 5] = "Aserradero de Vapor";
        _text[1218, 6] = "Tartak Parowy";
        _text[1218, 7] = "Serraria a Vapor";
        _text[1218, 8] = "";
        _text[1218, 9] = "";

        _text[1219, 0] = "Electro Sawmill";
        _text[1219, 1] = "Электролесопилка";
        _text[1219, 2] = "scierie Electrique";
        _text[1219, 3] = "Segheria Elettrica";
        _text[1219, 4] = "Elektrisches Sägewerk";
        _text[1219, 5] = "Aserradero Eléctrico";
        _text[1219, 6] = "Tartak Elektryczny";
        _text[1219, 7] = "Serraria Elétrica";
        _text[1219, 8] = "";
        _text[1219, 9] = "";

        _text[1220, 0] = "Manual Mining";
        _text[1220, 1] = "Ручная Добыча";
        _text[1220, 2] = "Exploitation Minière Manuelle";
        _text[1220, 3] = "Estrazione Manuale";
        _text[1220, 4] = "Manueller Abbau";
        _text[1220, 5] = "Extracción Manual";
        _text[1220, 6] = "Ręczne Wydobycie";
        _text[1220, 7] = "Extração Manual";
        _text[1220, 8] = "";
        _text[1220, 9] = "";

        _text[1221, 0] = "Steam Rig";
        _text[1221, 1] = "Паровая Установка";
        _text[1221, 2] = "Installation de Vapeur";
        _text[1221, 3] = "Impianto a Vapore";
        _text[1221, 4] = "Dampfanlage";
        _text[1221, 5] = "Planta de Vapor";
        _text[1221, 6] = "Instalacja Parowa";
        _text[1221, 7] = "Instalação a Vapor";
        _text[1221, 8] = "";
        _text[1221, 9] = "";

        _text[1222, 0] = "Excavator";
        _text[1222, 1] = "Экскаватор";
        _text[1222, 2] = "Excavatrice";
        _text[1222, 3] = "Escavatore";
        _text[1222, 4] = "Bagger";
        _text[1222, 5] = "Excavadora";
        _text[1222, 6] = "Koparka";
        _text[1222, 7] = "Escavadeira";
        _text[1222, 8] = "";
        _text[1222, 9] = "";

        _text[1223, 0] = "Bucket-wheel Excavator";
        _text[1223, 1] = "Многоковшовый Экскаватор";
        _text[1223, 2] = "Pelle à Godets Multiples";
        _text[1223, 3] = "Escavatore a Benna Multipla";
        _text[1223, 4] = "Schaufelradbagger";
        _text[1223, 5] = "Excavadora de Rueda de Cangilones";
        _text[1223, 6] = "Koparka Wieloczerpakowa";
        _text[1223, 7] = "Escavadeira de Roda de Caçambas";
        _text[1223, 8] = "";
        _text[1223, 9] = "";

        _text[1224, 0] = "Hand Pump";
        _text[1224, 1] = "Ручной Насос";
        _text[1224, 2] = "Pompe Manuelle";
        _text[1224, 3] = "Pompa Manuale";
        _text[1224, 4] = "Handpumpe";
        _text[1224, 5] = "Bomba Manual";
        _text[1224, 6] = "Pompa Ręczna";
        _text[1224, 7] = "Bomba Manual";
        _text[1224, 8] = "";
        _text[1224, 9] = "";

        _text[1225, 0] = "Steam Pump";
        _text[1225, 1] = "Паровой Насос";
        _text[1225, 2] = "Pompe à Vapeur";
        _text[1225, 3] = "Pompa a Vapore";
        _text[1225, 4] = "Dampfpumpe";
        _text[1225, 5] = "Bomba de Vapor";
        _text[1225, 6] = "Pompa Parowa";
        _text[1225, 7] = "Bomba a Vapor";
        _text[1225, 8] = "";
        _text[1225, 9] = "";

        _text[1226, 0] = "Pumpjack";
        _text[1226, 1] = "Насосный Домкрат";
        _text[1226, 2] = "Pompe à Balancier";
        _text[1226, 3] = "Pompa a Bilanciere";
        _text[1226, 4] = "Pumpjack";
        _text[1226, 5] = "Balancín de Bombeo";
        _text[1226, 6] = "Pompa Kiwakowa";
        _text[1226, 7] = "Bomba de Cavalo";
        _text[1226, 8] = "";
        _text[1226, 9] = "";

        _text[1227, 0] = "Oil Rig";
        _text[1227, 1] = "Нефтяная Вышка";
        _text[1227, 2] = "derrick Pétrolier";
        _text[1227, 3] = "Torre Petrolifera";
        _text[1227, 4] = "Ölbohrturm";
        _text[1227, 5] = "Torre Petrolífera";
        _text[1227, 6] = "Wiertnia Naftowa";
        _text[1227, 7] = "Torre de Perfuração de Petróleo";
        _text[1227, 8] = "";
        _text[1227, 9] = "";

        _text[1228, 0] = "Manual Mining";
        _text[1228, 1] = "Ручная Добыча";
        _text[1228, 2] = "Exploitation Minière Manuelle";
        _text[1228, 3] = "Estrazione Manuale";
        _text[1228, 4] = "Manueller Abbau";
        _text[1228, 5] = "Extracción Manual";
        _text[1228, 6] = "Ręczne Wydobycie";
        _text[1228, 7] = "Extração Manual";
        _text[1228, 8] = "";
        _text[1228, 9] = "";

        _text[1229, 0] = "Stone Mine";
        _text[1229, 1] = "Каменный Рудник";
        _text[1229, 2] = "Mine de Pierre";
        _text[1229, 3] = "Cava di Pietra";
        _text[1229, 4] = "Steinbruch";
        _text[1229, 5] = "Cantera de Piedra";
        _text[1229, 6] = "Kamieniołom";
        _text[1229, 7] = "Pedreira";
        _text[1229, 8] = "";
        _text[1229, 9] = "";

        _text[1230, 0] = "Steam-Powered Drill";
        _text[1230, 1] = "Паровой Бур";
        _text[1230, 2] = "Foreuse à Vapeur";
        _text[1230, 3] = "Trivella a Vapore";
        _text[1230, 4] = "Dampfbohrer";
        _text[1230, 5] = "Taladro de Vapor";
        _text[1230, 6] = "Wiertło Parowe";
        _text[1230, 7] = "Broca a Vapor";
        _text[1230, 8] = "";
        _text[1230, 9] = "";

        _text[1231, 0] = "Drilling Rig";
        _text[1231, 1] = "Буровая Установка";
        _text[1231, 2] = "Plateforme de Forage";
        _text[1231, 3] = "Impianto di Perforazione";
        _text[1231, 4] = "Bohranlage";
        _text[1231, 5] = "Plataforma de Perforación";
        _text[1231, 6] = "Wiertnia";
        _text[1231, 7] = "Plataforma de Perfuração";
        _text[1231, 8] = "";
        _text[1231, 9] = "";

        _text[1232, 0] = "Well";
        _text[1232, 1] = "Колодец";
        _text[1232, 2] = "Bien";
        _text[1232, 3] = "Pozzo";
        _text[1232, 4] = "Brunnen";
        _text[1232, 5] = "Pozo";
        _text[1232, 6] = "Studnia";
        _text[1232, 7] = "Poço";
        _text[1232, 8] = "";
        _text[1232, 9] = "";

        _text[1233, 0] = "Wind Pump";
        _text[1233, 1] = "Ветряной Насос";
        _text[1233, 2] = "Pompe à Vent";
        _text[1233, 3] = "Pompa Eolica";
        _text[1233, 4] = "Windpumpe";
        _text[1233, 5] = "Bomba Eólica";
        _text[1233, 6] = "Pompa Wiatrowa";
        _text[1233, 7] = "Bomba Eólica";
        _text[1233, 8] = "";
        _text[1233, 9] = "";

        _text[1234, 0] = "Steam Pump";
        _text[1234, 1] = "Паровой Насос";
        _text[1234, 2] = "Pompe à Vapeur";
        _text[1234, 3] = "Pompa a Vapore";
        _text[1234, 4] = "Dampfpumpe";
        _text[1234, 5] = "Bomba de Vapor";
        _text[1234, 6] = "Pompa Parowa";
        _text[1234, 7] = "Bomba a Vapor";
        _text[1234, 8] = "";
        _text[1234, 9] = "";

        _text[1235, 0] = "Electric Pump";
        _text[1235, 1] = "Электрический Насос";
        _text[1235, 2] = "Pompe Electrique";
        _text[1235, 3] = "Pompa Elettrica";
        _text[1235, 4] = "Elektrische Pumpe";
        _text[1235, 5] = "Bomba Eléctrica";
        _text[1235, 6] = "Pompa Elektryczna";
        _text[1235, 7] = "Bomba Elétrica";
        _text[1235, 8] = "";
        _text[1235, 9] = "";

        _text[1236, 0] = "Wooden Bridge";
        _text[1236, 1] = "Деревянный Мост";
        _text[1236, 2] = "Pont en Bois";
        _text[1236, 3] = "Ponte di Legno";
        _text[1236, 4] = "Holzbrücke";
        _text[1236, 5] = "Puente de Madera";
        _text[1236, 6] = "Most Drewniany";
        _text[1236, 7] = "Ponte de Madeira";
        _text[1236, 8] = "";
        _text[1236, 9] = "";

        _text[1237, 0] = "Stone Bridge";
        _text[1237, 1] = "Каменный Мост";
        _text[1237, 2] = "Pont de Pierre";
        _text[1237, 3] = "Ponte di Pietra";
        _text[1237, 4] = "Steinbrücke";
        _text[1237, 5] = "Puente de Piedra";
        _text[1237, 6] = "Most Kamienny";
        _text[1237, 7] = "Ponte de Pedra";
        _text[1237, 8] = "";
        _text[1237, 9] = "";

        _text[1238, 0] = "Metal Bridge";
        _text[1238, 1] = "Металлический Мост";
        _text[1238, 2] = "Pont Métallique";
        _text[1238, 3] = "Ponte Metallico";
        _text[1238, 4] = "Metallbrücke";
        _text[1238, 5] = "Puente Metálico";
        _text[1238, 6] = "Most Metalowy";
        _text[1238, 7] = "Ponte de Metal";
        _text[1238, 8] = "";
        _text[1238, 9] = "";

        _text[1239, 0] = "Stone Cutting Table";
        _text[1239, 1] = "Камнетесный Стол";
        _text[1239, 2] = "Table de Taille de Pierre";
        _text[1239, 3] = "Banco da Scalpellino";
        _text[1239, 4] = "Steinmetztisch";
        _text[1239, 5] = "Mesa de Cantería";
        _text[1239, 6] = "Stół Kamieniarski";
        _text[1239, 7] = "Mesa do Cortador de Pedra";
        _text[1239, 8] = "";
        _text[1239, 9] = "";

        _text[1240, 0] = "Stone Cutting Workbrench";
        _text[1240, 1] = "Верстак Резки Камня";
        _text[1240, 2] = "Établi de Découpe de Pierre";
        _text[1240, 3] = "Banco per il Taglio della Pietra";
        _text[1240, 4] = "Steinschneide-Werkbank";
        _text[1240, 5] = "Banco de Corte de Piedra";
        _text[1240, 6] = "Warsztat Cięcia Kamienia";
        _text[1240, 7] = "Bancada de Corte de Pedra";
        _text[1240, 8] = "";
        _text[1240, 9] = "";

        _text[1241, 0] = "Stone Cutting Factory";
        _text[1241, 1] = "Завод Резки Камня";
        _text[1241, 2] = "Usine de Taille de Pierre";
        _text[1241, 3] = "Impianto di Taglio della Pietra";
        _text[1241, 4] = "Steinschneidewerk";
        _text[1241, 5] = "Fábrica de Corte de Piedra";
        _text[1241, 6] = "Zakład Cięcia Kamienia";
        _text[1241, 7] = "Fábrica de Corte de Pedra";
        _text[1241, 8] = "";
        _text[1241, 9] = "";

        _text[1242, 0] = "Clay Furnace";
        _text[1242, 1] = "Глиняная Печь";
        _text[1242, 2] = "Four en Terre Cuite";
        _text[1242, 3] = "Forno d'Argilla";
        _text[1242, 4] = "Tonofen";
        _text[1242, 5] = "Horno de Arcilla";
        _text[1242, 6] = "Gliniany Piec";
        _text[1242, 7] = "Forno de Argila";
        _text[1242, 8] = "";
        _text[1242, 9] = "";

        _text[1243, 0] = "Stone Smeltery";
        _text[1243, 1] = "Каменная Плавильня";
        _text[1243, 2] = "Fonderie de Pierre";
        _text[1243, 3] = "Fonderia in Pietra";
        _text[1243, 4] = "Steinschmelze";
        _text[1243, 5] = "Fundición de Piedra";
        _text[1243, 6] = "Kamienny piec Wytopowy";
        _text[1243, 7] = "Fundição de Pedra";
        _text[1243, 8] = "";
        _text[1243, 9] = "";

        _text[1244, 0] = "Smelting Furnace";
        _text[1244, 1] = "Плавильная Печь";
        _text[1244, 2] = "Four de Fusion";
        _text[1244, 3] = "Forno di Fusione";
        _text[1244, 4] = "Schmelzofen";
        _text[1244, 5] = "Horno de Fundición";
        _text[1244, 6] = "Piec Wytopowy";
        _text[1244, 7] = "Forno de Fundição";
        _text[1244, 8] = "";
        _text[1244, 9] = "";

        _text[1245, 0] = "Blast Furnace";
        _text[1245, 1] = "Доменная Печь";
        _text[1245, 2] = "Haut Fourneau";
        _text[1245, 3] = "Altoforno";
        _text[1245, 4] = "Hochofen";
        _text[1245, 5] = "Alto Horno";
        _text[1245, 6] = "Wielki Piec";
        _text[1245, 7] = "Alto-Forno";
        _text[1245, 8] = "";
        _text[1245, 9] = "";

        _text[1246, 0] = "Manual Mixing";
        _text[1246, 1] = "Ручное Перемешивание";
        _text[1246, 2] = "Mélange Manuel";
        _text[1246, 3] = "Miscelazione Manuale";
        _text[1246, 4] = "Manuelles Mischen";
        _text[1246, 5] = "Mezcla Manual";
        _text[1246, 6] = "Ręczne Mieszanie";
        _text[1246, 7] = "Mistura Manual";
        _text[1246, 8] = "";
        _text[1246, 9] = "";

        _text[1247, 0] = "Automixer";
        _text[1247, 1] = "Автомешалка";
        _text[1247, 2] = "Mélangeur de Voiture";
        _text[1247, 3] = "Miscelatore Automatico";
        _text[1247, 4] = "Automatischer Mischer";
        _text[1247, 5] = "Mezcladora Automática";
        _text[1247, 6] = "Mieszarka Automatyczna";
        _text[1247, 7] = "Misturador Automático";
        _text[1247, 8] = "";
        _text[1247, 9] = "";

        _text[1248, 0] = "Concrete Factory";
        _text[1248, 1] = "Бетонный Завод";
        _text[1248, 2] = "Centrale à Béton";
        _text[1248, 3] = "Impianto di Calcestruzzo";
        _text[1248, 4] = "Betonwerk";
        _text[1248, 5] = "Planta de Hormigón";
        _text[1248, 6] = "Wytwórnia Betonu";
        _text[1248, 7] = "Usina de Concreto";
        _text[1248, 8] = "";
        _text[1248, 9] = "";

        _text[1249, 0] = "Boiler";
        _text[1249, 1] = "Котел";
        _text[1249, 2] = "Chaudière";
        _text[1249, 3] = "Caldaia";
        _text[1249, 4] = "Kessel";
        _text[1249, 5] = "Caldera";
        _text[1249, 6] = "Kocioł";
        _text[1249, 7] = "Caldeira";
        _text[1249, 8] = "";
        _text[1249, 9] = "";

        _text[1250, 0] = "Big Boiler";
        _text[1250, 1] = "Большой Котел";
        _text[1250, 2] = "Le Grand Chaudron";
        _text[1250, 3] = "Caldaia Grande";
        _text[1250, 4] = "Großer Kessel";
        _text[1250, 5] = "Caldera Grande";
        _text[1250, 6] = "Duży Kocioł";
        _text[1250, 7] = "Caldeira Grande";
        _text[1250, 8] = "";
        _text[1250, 9] = "";

        _text[1251, 0] = "Steam Generator Complex";
        _text[1251, 1] = "Парогенераторный Комплекс";
        _text[1251, 2] = "Complexe de Générateur de Vapeur";
        _text[1251, 3] = "Complesso di Generatori di Vapore";
        _text[1251, 4] = "Dampferzeuger-Komplex";
        _text[1251, 5] = "Complejo Generador de Vapor";
        _text[1251, 6] = "Kompleks Parogeneratorów";
        _text[1251, 7] = "Complexo de Geração de Vapor";
        _text[1251, 8] = "";
        _text[1251, 9] = "";
        
        _text[1252, 0] = "Components Workbench";
        _text[1252, 1] = "Верстак Компонентов";
        _text[1252, 2] = "Atelier de Composants";
        _text[1252, 3] = "Banco dei Componenti";
        _text[1252, 4] = "Komponentenwerkbank";
        _text[1252, 5] = "Banco de Componentes";
        _text[1252, 6] = "Warsztat Komponentów";
        _text[1252, 7] = "Bancada de Componentes";
        _text[1252, 8] = "";
        _text[1252, 9] = "";

        _text[1253, 0] = "Components Workshop";
        _text[1253, 1] = "Цех Компонентов";
        _text[1253, 2] = "Atelier des Composants";
        _text[1253, 3] = "Officina dei Componenti";
        _text[1253, 4] = "Komponentenwerkstatt";
        _text[1253, 5] = "Taller de Componentes";
        _text[1253, 6] = "Zakład Komponentów";
        _text[1253, 7] = "Oficina de Componentes";
        _text[1253, 8] = "";
        _text[1253, 9] = "";

        _text[1254, 0] = "Components Factory";
        _text[1254, 1] = "Фабрика Компонентов";
        _text[1254, 2] = "Usine de Composants";
        _text[1254, 3] = "Fabbrica dei Componenti";
        _text[1254, 4] = "Komponentenfabrik";
        _text[1254, 5] = "Fábrica de Componentes";
        _text[1254, 6] = "Fabryka Komponentów";
        _text[1254, 7] = "Fábrica de Componentes";
        _text[1254, 8] = "";
        _text[1254, 9] = "";

        _text[1255, 0] = "Bioseptic";
        _text[1255, 1] = "Биосептик";
        _text[1255, 2] = "Bioseptique";
        _text[1255, 3] = "Biosettico";
        _text[1255, 4] = "Bioseptik";
        _text[1255, 5] = "Bioséptico";
        _text[1255, 6] = "Bioseptyk";
        _text[1255, 7] = "Biosséptico";
        _text[1255, 8] = "";
        _text[1255, 9] = "";

        _text[1256, 0] = "Aerogenerator";
        _text[1256, 1] = "Аэрогенератор";
        _text[1256, 2] = "Aérogénérateur";
        _text[1256, 3] = "Aerogeneratore";
        _text[1256, 4] = "Aerogenerator";
        _text[1256, 5] = "Generador de Aire";
        _text[1256, 6] = "Aerogenerator";
        _text[1256, 7] = "Aerogerador";
        _text[1256, 8] = "";
        _text[1256, 9] = "";

        _text[1257, 0] = "Waste Neutralizer";
        _text[1257, 1] = "Нейтрализатор Отходов";
        _text[1257, 2] = "Neutralisateur de Déchets";
        _text[1257, 3] = "Neutralizzatore di Rifiuti";
        _text[1257, 4] = "Abfallneutralisator";
        _text[1257, 5] = "Neutralizador de Residuos";
        _text[1257, 6] = "Neutralizator Odpadów";
        _text[1257, 7] = "Neutralizador de Resíduos";
        _text[1257, 8] = "";
        _text[1257, 9] = "";

        _text[1258, 0] = "Radio Transmitter ";
        _text[1258, 1] = "Радиопередатчик";
        _text[1258, 2] = "Émetteur Radio";
        _text[1258, 3] = "Trasmettitore Radio";
        _text[1258, 4] = "Funksender";
        _text[1258, 5] = "Transmisor de Radio";
        _text[1258, 6] = "Nadajnik Radiowy";
        _text[1258, 7] = "Transmissor de Rádio";
        _text[1258, 8] = "";
        _text[1258, 9] = "";

        _text[1259, 0] = "Radio Tower ";
        _text[1259, 1] = "Радиовышка";
        _text[1259, 2] = "Tour Radio";
        _text[1259, 3] = "Torre Radio";
        _text[1259, 4] = "Funkmast";
        _text[1259, 5] = "Torre de Radio";
        _text[1259, 6] = "Wieża Radiowa";
        _text[1259, 7] = "Torre de Rádio";
        _text[1259, 8] = "";
        _text[1259, 9] = "";

        _text[1260, 0] = "Satellite Dish";
        _text[1260, 1] = "Спутниковая Антенна";
        _text[1260, 2] = "Antenne Satellite";
        _text[1260, 3] = "Antenna Satellitare";
        _text[1260, 4] = "Satellitenantenne";
        _text[1260, 5] = "Antena Satelital";
        _text[1260, 6] = "Antena Satelitarna";
        _text[1260, 7] = "Antena de Satélite";
        _text[1260, 8] = "";
        _text[1260, 9] = "";

        _text[1261, 0] = "Wooden Wall";
        _text[1261, 1] = "Деревянная Стена";
        _text[1261, 2] = "Mur en Bois";
        _text[1261, 3] = "Muro di Legno";
        _text[1261, 4] = "Holzwand";
        _text[1261, 5] = "Muro de Madera";
        _text[1261, 6] = "Drewniana Sciana";
        _text[1261, 7] = "Parede de Madeira";
        _text[1261, 8] = "";
        _text[1261, 9] = "";

        _text[1262, 0] = "Sandbag Wall";
        _text[1262, 1] = "Песчаная Стена";
        _text[1262, 2] = "Mur de Sable";
        _text[1262, 3] = "Muro di Sabbia";
        _text[1262, 4] = "Sandwand";
        _text[1262, 5] = "Muro de Arena";
        _text[1262, 6] = "Piaszczana Sciana";
        _text[1262, 7] = "Parede de Areia";
        _text[1262, 8] = "";
        _text[1262, 9] = "";

        _text[1263, 0] = "Stone Wall";
        _text[1263, 1] = "Каменная Стена";
        _text[1263, 2] = "Mur de Pierre";
        _text[1263, 3] = "Muro di Pietra";
        _text[1263, 4] = "Steinwand";
        _text[1263, 5] = "Muro de Piedra";
        _text[1263, 6] = "Kamienna Sciana";
        _text[1263, 7] = "Parede de Pedra";
        _text[1263, 8] = "";
        _text[1263, 9] = "";

        _text[1264, 0] = "Concrete Wall";
        _text[1264, 1] = "Бетонная Стена";
        _text[1264, 2] = "Mur en Béton";
        _text[1264, 3] = "Muro di Calcestruzzo";
        _text[1264, 4] = "Betonwand";
        _text[1264, 5] = "Muro de Hormigón";
        _text[1264, 6] = "Betonowa Sciana";
        _text[1264, 7] = "Parede de Concreto";
        _text[1264, 8] = "";
        _text[1264, 9] = "";

        _text[1265, 0] = "Steel Wall";
        _text[1265, 1] = "Стальная Стена";
        _text[1265, 2] = "Mur en Acier";
        _text[1265, 3] = "Muro d'acciaio";
        _text[1265, 4] = "Stahlwand";
        _text[1265, 5] = "Muro de Acero";
        _text[1265, 6] = "Stalowa Sciana";
        _text[1265, 7] = "Parede de Aço";
        _text[1265, 8] = "";
        _text[1265, 9] = "";

        _text[1266, 0] = "Wooden Gate";
        _text[1266, 1] = "Деревянные Ворота";
        _text[1266, 2] = "Portails en Bois";
        _text[1266, 3] = "Cancello di Legno";
        _text[1266, 4] = "Holztor";
        _text[1266, 5] = "Puerta de Madera";
        _text[1266, 6] = "Drewniana Brama";
        _text[1266, 7] = "Portão de Madeira";
        _text[1266, 8] = "";
        _text[1266, 9] = "";

        _text[1267, 0] = "Sandbag Gate";
        _text[1267, 1] = "Песчаные Ворота";
        _text[1267, 2] = "Sand Gate";
        _text[1267, 3] = "Cancello di Sabbia";
        _text[1267, 4] = "Sandtor";
        _text[1267, 5] = "Puerta de Arena";
        _text[1267, 6] = "Piaszczana Brama";
        _text[1267, 7] = "Portão de Areia";
        _text[1267, 8] = "";
        _text[1267, 9] = "";

        _text[1268, 0] = "Stone Gate";
        _text[1268, 1] = "Каменные Ворота";
        _text[1268, 2] = "Porte de Pierre";
        _text[1268, 3] = "Cancello di Pietra";
        _text[1268, 4] = "Steintor";
        _text[1268, 5] = "Puerta de Piedra";
        _text[1268, 6] = "Kamienna Brama";
        _text[1268, 7] = "Portão de Pedra";
        _text[1268, 8] = "";
        _text[1268, 9] = "";

        _text[1269, 0] = "Concrete Gate";
        _text[1269, 1] = "Бетонные Ворота";
        _text[1269, 2] = "Portails en Béton";
        _text[1269, 3] = "Cancello di Calcestruzzo";
        _text[1269, 4] = "Betontor";
        _text[1269, 5] = "Puerta de Hormigón";
        _text[1269, 6] = "Betonowa Brama";
        _text[1269, 7] = "Portão de Concreto";
        _text[1269, 8] = "";
        _text[1269, 9] = "";

        _text[1270, 0] = "Steel Gate";
        _text[1270, 1] = "Стальные Ворота";
        _text[1270, 2] = "Portails en Acier";
        _text[1270, 3] = "Cancello d'acciaio";
        _text[1270, 4] = "Stahltor";
        _text[1270, 5] = "Puerta de Acero";
        _text[1270, 6] = "Stalowa Brama";
        _text[1270, 7] = "Portão de Aço";
        _text[1270, 8] = "";
        _text[1270, 9] = "";

        _text[1271, 0] = "Ballista";
        _text[1271, 1] = "Баллиста";
        _text[1271, 2] = "Baliste";
        _text[1271, 3] = "Ballista";
        _text[1271, 4] = "Balliste";
        _text[1271, 5] = "Balista";
        _text[1271, 6] = "Balista";
        _text[1271, 7] = "Balista";
        _text[1271, 8] = "";
        _text[1271, 9] = "";

        _text[1272, 0] = "Cannon";
        _text[1272, 1] = "Пушка";
        _text[1272, 2] = "Pistolet";
        _text[1272, 3] = "Cannone";
        _text[1272, 4] = "Kanone";
        _text[1272, 5] = "Cañón";
        _text[1272, 6] = "Armata";
        _text[1272, 7] = "Canhão";
        _text[1272, 8] = "";
        _text[1272, 9] = "";

        _text[1273, 0] = "Howitzer";
        _text[1273, 1] = "Гаубица";
        _text[1273, 2] = "Obusier";
        _text[1273, 3] = "Obice";
        _text[1273, 4] = "Haubitze";
        _text[1273, 5] = "Obús";
        _text[1273, 6] = "Haubica";
        _text[1273, 7] = "Obuseiro";
        _text[1273, 8] = "";
        _text[1273, 9] = "";

        _text[1274, 0] = "Turret Gun";
        _text[1274, 1] = "Турельная Пушка";
        _text[1274, 2] = "Canon de Tourelle";
        _text[1274, 3] = "Cannone a Torretta";
        _text[1274, 4] = "Turmkanone";
        _text[1274, 5] = "Cañón de Torreta";
        _text[1274, 6] = "Działo Wieżyczkowe";
        _text[1274, 7] = "Canhão de Torreta";
        _text[1274, 8] = "";
        _text[1274, 9] = "";

        _text[1275, 0] = "Minigun";
        _text[1275, 1] = "Миниган";
        _text[1275, 2] = "Minigun";
        _text[1275, 3] = "Minigun";
        _text[1275, 4] = "Minigun";
        _text[1275, 5] = "Minigun";
        _text[1275, 6] = "Minigun";
        _text[1275, 7] = "Minigun";
        _text[1275, 8] = "";
        _text[1275, 9] = "";

        _text[1276, 0] = "Rocket Launcher";
        _text[1276, 1] = "Ракетная Установка";
        _text[1276, 2] = "Lance-roquettes";
        _text[1276, 3] = "Lanciarazzi";
        _text[1276, 4] = "Raketenwerfer";
        _text[1276, 5] = "Lanzamisiles";
        _text[1276, 6] = "Wyrzutnia Rakiet";
        _text[1276, 7] = "Lançador de Foguetes";
        _text[1276, 8] = "";
        _text[1276, 9] = "";

        _text[1277, 0] = "Laser Cannon";
        _text[1277, 1] = "Лазерная Пушка";
        _text[1277, 2] = "Canon Laser";
        _text[1277, 3] = "Cannone Laser";
        _text[1277, 4] = "Laserkanone";
        _text[1277, 5] = "Cañón Láser";
        _text[1277, 6] = "Działo Laserowe";
        _text[1277, 7] = "Canhão Laser";
        _text[1277, 8] = "";
        _text[1277, 9] = "";

        _text[1278, 0] = "Battleship Tower";
        _text[1278, 1] = "Башня Линкора";
        _text[1278, 2] = "Tour du Cuirassé";
        _text[1278, 3] = "Torretta di Corazzata";
        _text[1278, 4] = "Schlachtschiffturm";
        _text[1278, 5] = "Torreta de Acorazado";
        _text[1278, 6] = "Wieża Pancernika";
        _text[1278, 7] = "Torre do Couraçado";
        _text[1278, 8] = "";
        _text[1278, 9] = "";

        _text[1279, 0] = "Mechanic's Tent";
        _text[1279, 1] = "Палатка Механика";
        _text[1279, 2] = "Mécanique des Tentes";
        _text[1279, 3] = "Tenda del Meccanico";
        _text[1279, 4] = "Mechanikerzelt";
        _text[1279, 5] = "Tienda del Mecánico";
        _text[1279, 6] = "Namiot Mechanika";
        _text[1279, 7] = "Tenda do Mecânico";
        _text[1279, 8] = "";
        _text[1279, 9] = "";

        _text[1280, 0] = "Mechanical Workshop";
        _text[1280, 1] = "Механический Цех";
        _text[1280, 2] = "Atelier Mécanique";
        _text[1280, 3] = "Officina Meccanica";
        _text[1280, 4] = "Mechanische Werkstatt";
        _text[1280, 5] = "Taller Mecánico";
        _text[1280, 6] = "Warsztat Mechaniczny";
        _text[1280, 7] = "Oficina Mecânica";
        _text[1280, 8] = "";
        _text[1280, 9] = "";

        _text[1281, 0] = "Automaton Factory";
        _text[1281, 1] = "Фабрика Автоматонов";
        _text[1281, 2] = "Usine d'automates";
        _text[1281, 3] = "Fabbrica di Automi";
        _text[1281, 4] = "Automatenfabrik";
        _text[1281, 5] = "Fábrica de Autómatas";
        _text[1281, 6] = "Fabryka Automatów";
        _text[1281, 7] = "Fábrica de Autômatos";
        _text[1281, 8] = "";
        _text[1281, 9] = "";

        _text[1282, 0] = "Wooden Spikes";
        _text[1282, 1] = "Деревянные Шипы";
        _text[1282, 2] = "Piques en Bois";
        _text[1282, 3] = "Spuntoni di Legno";
        _text[1282, 4] = "Holzspieße";
        _text[1282, 5] = "Estacas de Madera";
        _text[1282, 6] = "Drewniane Kolce";
        _text[1282, 7] = "Estacas de Madeira";
        _text[1282, 8] = "";
        _text[1282, 9] = "";

        _text[1283, 0] = "Glass Shards";
        _text[1283, 1] = "Осколки Стекла";
        _text[1283, 2] = "Éclats de Verre";
        _text[1283, 3] = "Schegge di Vetro";
        _text[1283, 4] = "Glasscherben";
        _text[1283, 5] = "Fragmentos de Vidrio";
        _text[1283, 6] = "Odłamki Szkła";
        _text[1283, 7] = "Cacos de Vidro";
        _text[1283, 8] = "";
        _text[1283, 9] = "";

        _text[1284, 0] = "Iron Spikes";
        _text[1284, 1] = "Железные Шипы";
        _text[1284, 2] = "Pointes de Fer";
        _text[1284, 3] = "Spuntoni di Ferro";
        _text[1284, 4] = "Eisenspieße";
        _text[1284, 5] = "Púas de Hierro";
        _text[1284, 6] = "Żelazne Kolce";
        _text[1284, 7] = "Estacas de Ferro";
        _text[1284, 8] = "";
        _text[1284, 9] = "";

        _text[1285, 0] = "Steel Saws";
        _text[1285, 1] = "Стальные Пилы";
        _text[1285, 2] = "Scies à Acier";
        _text[1285, 3] = "Seghe d'acciaio";
        _text[1285, 4] = "Stahlsägen";
        _text[1285, 5] = "Sierras de Acero";
        _text[1285, 6] = "Stalowe Piły";
        _text[1285, 7] = "Serras de Aço";
        _text[1285, 8] = "";
        _text[1285, 9] = "";

        _text[1286, 0] = "Electrical Barrier";
        _text[1286, 1] = "Электрический Барьер";
        _text[1286, 2] = "Barrière Electrique";
        _text[1286, 3] = "Barriera Elettrica";
        _text[1286, 4] = "Elektrische Barriere";
        _text[1286, 5] = "Barrera Eléctrica";
        _text[1286, 6] = "Elektryczna Bariera";
        _text[1286, 7] = "Barreira Elétrica";
        _text[1286, 8] = "";
        _text[1286, 9] = "";

        #endregion

        for (int x = 0; x < WorldGameInfo.LanguageLength; x++) TextStatic[x] = _text[x, LanguageNumber];
    }
}




