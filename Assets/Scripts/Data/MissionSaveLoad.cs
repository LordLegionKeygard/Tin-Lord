using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MissionSaveLoad : MonoBehaviour
{
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private MissionSaveGame _missionSaveGame;
    [Inject] private MissionResources _missionResources;
    [Inject] private readonly TutorialSystem _tutorialSystem;

    [Header("Main")]
    [SerializeField] private AllNodesInfo _allMissionsInfo;
    [SerializeField] private TileMapBuilder _tileMapBuilder;

    [Header("UpPanel")]
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;


    [Header("Cards")]
    [SerializeField] private CardHolderSystem _cardHolderSystem;

    [Header("WorldEvents")]
    [SerializeField] private MissionEventSystem _missionEventSystem;

    [Header("Enemies")]
    [SerializeField] private EnemiesSpawnerSystem _enemiesSpawnerSystem;

    [Header("Tiles")]
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private AllTileObjects _allTileObjects;

    [Header("Robot")]
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;

    [Header("LearnedBuildings")]
    [SerializeField] private LearnedBuildingsDataMission _learnedBuildingsDataMission;

    [Header("Objectives")]
    [SerializeField] private ObjectivesPanel _objectivesPanel;

    [Header("ShipCannons")]
    [SerializeField] private MissionModeSystem _missionModeSystem;
    [SerializeField] private MissionShipWeaponSystem _missionShipWeaponSystem;
    [SerializeField] private MissionShipWeaponSystem _missionWeaponSetterSystem;

    [Header("Skill")]
    [SerializeField] private AllSkills _allSkills;

    [Header("Quants")]
    [SerializeField] private QuantPickupPool _quantPool;
    [SerializeField] private MissionQuantSystem _missionQuantSystem;

    [Header("Hangar")]
    [SerializeField] private MissionHangarSystem _missionHangarSystem;

    [Header("Hazards")]
    [SerializeField] private SpawnedHazardSystem _spawnedHazardSystem;

    private void Awake()
    {
        _missionSaveGame.MissionSaveLoad = this;
    }

    private void Start()
    {
        CustomEvents.OnDayEnd += AutoSave;
    }

    public void ResetMissionData(ref MissionSaveData currentSaveData)
    {
        currentSaveData = new MissionSaveData
        {
            IsStartMission = true,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
            ShipCannonsData = new MissionShipWeaponsData(),
            Cards = Array.Empty<int>(),
            DayEventsData = Array.Empty<DayEventData>(),
            EnemyData = Array.Empty<EnemyData>(),
            TilesData = Array.Empty<TileDataWrapper>(),
            RoadTilesId = Array.Empty<int>(),
            ObjectiveAmount = Array.Empty<int>(),
            SkillsCooldown = Array.Empty<int>(),
            SkillsDuration = Array.Empty<int>(),
            QuantPickups = Array.Empty<QuantPickupData>(),
            Hazards = Array.Empty<HazardSaveData>()
        };

        // копирование стартовых ресурсов из ангара:
        for (int i = 0; i < _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.MainResourcesData.Length; i++)
            currentSaveData.ResourcesData[i] = _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.MainResourcesData[i];

        currentSaveData.ResourcesData[(int)ResourceEnum.DataFragment] = 0;
    }

    private T[] NotNull<T>(T[] arr) => arr ?? Array.Empty<T>();

    public void SaveMissionData(ref MissionSaveData data)
    {
        EnsureDefaults(ref data);
        var localData = data;

        int resLen = Enum.GetValues(typeof(ResourceEnum)).Length - 1;

        StepSave("Main", () =>
        {
            localData.IsStartMission = false;
        });

        StepSave("UpPanel", () =>
        {
            localData.Day = _timeTickSystem.GetCurrentDay();
            localData.Tick = _timeTickSystem.GetCurrentTick();
            localData.Radiation = _ecologySystem.GetRadiation();
            localData.EveryDayEcology = NotNull(_ecologySystem.GetEveryDayEcology());
            localData.GameSpeed = (int)GameSpeedEnum.Pause;
        });

        StepSave("Resources", () =>
        {
            var res = _missionResources.GetAllResourcesAmount();
            if (res == null) res = new float[resLen];
            else if (res.Length != resLen) Array.Resize(ref res, resLen);
            localData.ResourcesData = res;
        });

        StepSave("Cards", () =>
        {
            localData.Cards = NotNull(_cardHolderSystem.GetAllCards());
        });

        StepSave("DayEvents", () =>
        {
            localData.DayEventsData = NotNull(_missionEventSystem.GetAllCurrentEvents());
        });

        StepSave("Enemies", () =>
        {
            localData.EnemyData = NotNull(_enemiesSpawnerSystem.GetAllCurrentEnemies());
        });

        StepSave("Tiles", () =>
        {
            localData.BaseLevel = _tilesSystem.GetBaseLevel();
            localData.IsHaveRiver = _tilesSystem.IsHaveRiver();
            localData.IsHaveMachineProduction = _tilesSystem.IsHaveMachineProduction();
            localData.TilesData = NotNull(_allTileObjects.GetAllTileObjects());
            localData.RoadTilesId = NotNull(_tileMapBuilder.GetRoadTilesId());
        });

        StepSave("Robot", () =>
        {
            localData.MachineData = _currentMachineSystem.GetMachineData();
        });

        StepSave("Objectives", () =>
        {
            localData.ObjectiveAmount = NotNull(_objectivesPanel.GetAllObjectivesAmount());
        });

        StepSave("ShipCannons", () =>
        {
            localData.ShipCannonsData ??= new MissionShipWeaponsData();
            localData.ShipCannonsData.IsWeaponMode = !_missionModeSystem.IsPlanetMode();
            localData.ShipCannonsData.LeftWeaponBulletsCount = _missionShipWeaponSystem.GetCurrentLeftShipWeaponBulletsCount();
            localData.ShipCannonsData.RightWeaponBulletsCount = _missionShipWeaponSystem.GetCurrentRightShipWeaponBulletsCount();
        });

        StepSave("Skills", () =>
        {
            localData.SkillsCooldown = NotNull(_allSkills.GetAllSkillsCooldown());
            localData.SkillsDuration = NotNull(_allSkills.GetAllSkillsDuration());
        });

        StepSave("Quants", () =>
        {
            localData.QuantsAmount = _missionQuantSystem.GetQuants();
            localData.QuantPickups = NotNull(_quantPool.GetActiveQuants());
        });

        StepSave("Hazards", () =>
        {
            localData.Hazards = NotNull(_spawnedHazardSystem.GetHazards());
        });
    }

    public void LoadGameData(ref MissionSaveData data)
    {
        EnsureDefaults(ref data);
        var localData = data;

        // --- защита от пустой выбранной миссии ---
        var currentSelectMission = _spaceSaveGame.SpaceSaveData?.CurrentMission;
        if (currentSelectMission == null)
        {
            Debug.LogError("[MissionSaveLoad.Load] CurrentMission is null");
            return;
        }

        int deckIndex = Mathf.Clamp(
            currentSelectMission.MissionDeckIndex,
            0,
            _allMissionsInfo.MissionDeck.Length - 1
        );

        StepLoad("Tutorial", () =>
            _tutorialSystem.LoadTutorial(_hangarSaveGame.HangarSaveData.TutorialProgress,
                                         _spaceSaveGame.SpaceSaveData.PrologueCompleted));

        StepLoad("BuildMission", () =>
        {
            var node = BuildMissionFromSelected(currentSelectMission, deckIndex);
            CurrentMissionInfo.Instance.LoadMission(node, deckIndex);
            _tileMapBuilder.BuildMap(localData.IsStartMission);
        });

        StepLoad("UpPanel", () =>
        {
            _timeTickSystem.LoadTime(localData.Day, localData.Tick);
            _ecologySystem.LoadEcology(localData.Radiation, NotNull(localData.EveryDayEcology), localData.IsStartMission);
            _gameSpeedSystem.ChangeGameSpeed(localData.GameSpeed, false);
        });

        StepLoad("Resources", () => _missionResources.LoadResources(localData.ResourcesData));

        StepLoad("Cards", () => _cardHolderSystem.LoadCards(localData.IsStartMission, NotNull(localData.Cards)));

        StepLoad("DayEvents", () => _missionEventSystem.LoadEvents(NotNull(localData.DayEventsData), localData.IsStartMission));

        StepLoad("Enemies", () => _enemiesSpawnerSystem.LoadEnemies(NotNull(localData.EnemyData), localData.IsStartMission));

        StepLoad("Tiles", () =>
        {
            _tilesSystem.SetBaseLevel(localData.BaseLevel);
            _tilesSystem.SetIsHaveRiver(localData.IsHaveRiver);
            _tilesSystem.SetIsHaveMachineProduction(localData.IsHaveMachineProduction);
            _allTileObjects.LoadTiles(NotNull(localData.TilesData), localData.IsStartMission);
            _tileMapBuilder.LoadRoadTiles(NotNull(localData.RoadTilesId), localData.IsStartMission);
        });

        StepLoad("Machine", () =>
        {
            localData.MachineData ??= new MachineData();
            _machineSpawnerSystem.LoadSpawnMachine(localData);
        });

        StepLoad("LearnedBuildings", () =>
            _learnedBuildingsDataMission.LoadLearnedBuildings(
                _spaceSaveGame.SpaceSaveData.BuildingsLearned));

        StepLoad("Objectives", () =>
            _objectivesPanel.LoadObjectiveItems(NotNull(localData.ObjectiveAmount), localData.IsStartMission));

        StepLoad("ShipCannons", () =>
        {
            localData.ShipCannonsData ??= new MissionShipWeaponsData();
            _missionModeSystem.LoadMode(localData.ShipCannonsData.IsWeaponMode);
            _missionWeaponSetterSystem.LoadWeapons(
                _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.WeaponData,
                localData.ShipCannonsData,
                localData.IsStartMission);
        });

        StepLoad("Skills", () =>
            _allSkills.LoadAllSkills(NotNull(localData.SkillsCooldown), NotNull(localData.SkillsDuration),
                _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.OpenedSkills));

        StepLoad("Quants", () =>
        {
            _missionQuantSystem.SetQuants(localData.QuantsAmount);
            _quantPool.LoadQuantPickup(NotNull(localData.QuantPickups));
        });

        StepLoad("Hangar", () =>
            _missionHangarSystem.LoadHangarData(_spaceSaveGame.SpaceSaveData.HangarCommandCenterData));

        StepLoad("Hazards", () =>
            _spawnedHazardSystem.LoadHazardData(NotNull(localData.Hazards), localData.IsStartMission));

        CustomEvents.FireDataLoad();
    }

    private MissionNode BuildMissionFromSelected(SelectedMissionData sel, int deckIndex)
    {
        if (sel == null)
        {
            Debug.LogError("[BuildMissionFromSelected] SelectedMissionData is null");
            return null;
        }

        // Защита MissionDeck
        var deck = _allMissionsInfo.MissionDeck;
        if (deck == null || deck.Length == 0)
        {
            Debug.LogError("[BuildMissionFromSelected] MissionDeck is null or empty");
            return null;
        }
        deckIndex = Mathf.Clamp(deckIndex, 0, deck.Length - 1);

        // Защита Landscapes
        var landscapes = _allMissionsInfo.Landscapes;
        if (landscapes == null || landscapes.Length == 0)
        {
            Debug.LogError("[BuildMissionFromSelected] Landscapes is null or empty");
            return null;
        }
        int landscapeIndex = Mathf.Clamp(sel.LandscapeId, 0, landscapes.Length - 1);

        var definition = deck[deckIndex];
        var template = _allMissionsInfo.MissionNodeTemplate;
        var landscape = landscapes[landscapeIndex];
        var spawnerSO = definition.Spawner;

        // SavedObjectives защита
        var saved = sel.SavedObjectives ?? Array.Empty<ObjectiveSave>();

        var wrappers = new ObjectiveWrapper[saved.Length];
        for (int i = 0; i < saved.Length; i++)
        {
            wrappers[i] = new ObjectiveWrapper
            {
                ObjectiveEnum = saved[i].Objective,
                ObjectiveAmount = saved[i].Amount
            };
        }
        var objectiveSO = ScriptableObject.CreateInstance<Objective>();
        objectiveSO.Objectives = wrappers;

        var node = ScriptableObject.CreateInstance<MissionNode>();
        node.Landscape = landscape;
        node.Objective = objectiveSO;
        node.EnemiesSpawner = spawnerSO;

        node.Icon = template.Icon;
        node.IconColor = template.IconColor;
        node.IconWidth = template.IconWidth;
        node.IconHeight = template.IconHeight;
        node.DescriptionTextNumber = template.DescriptionTextNumber;
        node.CosmosVariations = landscape.CosmosVariations;

        return node;
    }


    public void PrepareRestartMission()
    {
        StartCoroutine(nameof(PrepareRestartMissionCoroutine));
    }

    public void PrepareSaveMission()
    {
        StartCoroutine(nameof(PrepareSaveMissionCoroutine));
    }


    private IEnumerator PrepareRestartMissionCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        _missionSaveGame.ResetMissionJson();
        CustomEvents.FireLoadScene(SceneEnum.Mission, WorldGameInfo.LoadSceneTime, CurrentMissionInfo.Instance.GetCurrentLandscape().LoadingScreenSprite);
    }

    private IEnumerator PrepareSaveMissionCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        _missionSaveGame.SaveMissionToJson();
        CustomEvents.FireLoadScene(SceneEnum.Space, WorldGameInfo.LoadSceneTime, null);
    }

    public void AutoSave(int day)
    {
        if (_endMissionSystem.IsMissionEnd() || _tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionGoodLuckDescription_66) return;
        _missionSaveGame.SaveMissionToJson();
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= AutoSave;
    }


    private void EnsureDefaults(ref MissionSaveData d)
    {
        if (d == null) d = new MissionSaveData();

        int resLen = Enum.GetValues(typeof(ResourceEnum)).Length - 1;

        d.ResourcesData ??= new float[resLen];
        d.Cards ??= Array.Empty<int>();
        d.DayEventsData ??= Array.Empty<DayEventData>();
        d.EnemyData ??= Array.Empty<EnemyData>();
        d.TilesData ??= Array.Empty<TileDataWrapper>();
        d.RoadTilesId ??= Array.Empty<int>();
        d.ObjectiveAmount ??= Array.Empty<int>();
        d.ShipCannonsData ??= new MissionShipWeaponsData();   // ← ← главный виновник
        d.SkillsCooldown ??= Array.Empty<int>();
        d.SkillsDuration ??= Array.Empty<int>();
        d.QuantPickups ??= Array.Empty<QuantPickupData>();
        d.Hazards ??= Array.Empty<HazardSaveData>();

        // На случай, если количество ресурсов изменилось между версиями:
        if (d.ResourcesData.Length != resLen)
        {
            Array.Resize(ref d.ResourcesData, resLen);
        }
    }

    private static void StepSave(string label, Action body)
    {
        try
        {
            body();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[MissionSave] FAIL at '{label}': {ex.Message}");
            Debug.LogException(ex);
        }
    }

    private static void StepLoad(string label, Action body)
    {
        try { body(); }
        catch (Exception ex)
        {
            Debug.LogError($"[MissionLoad.Load] FAIL at '{label}': {ex.Message}");
            Debug.LogException(ex);
            throw;
        }
    }
}
