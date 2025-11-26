using System.Collections.Generic;
using UnityEngine;

public class SpawnedHazardSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _hazardPrefabs;
    [SerializeField] private List<ActiveHazard> _activeHazards = new();

    private class ActiveHazard
    {
        public int Type;
        public GameObject Instance;
        public int TicksLeft;
        public float DamageFactor;
    }

    private void OnEnable()
    {
        CustomEvents.OnTimeTick += OnTimeTick;
    }

    private void OnDisable()
    {
        CustomEvents.OnTimeTick -= OnTimeTick;
    }

    public void RegisterHazard(int type, GameObject obj, int ticksLeft, float damageFactor)
    {
        _activeHazards.Add(new ActiveHazard
        {
            Type = type,
            Instance = obj,
            TicksLeft = ticksLeft,
            DamageFactor = damageFactor
        });
    }

    public HazardSaveData[] GetHazards()
    {
        var list = new List<HazardSaveData>(_activeHazards.Count);
        foreach (var h in _activeHazards)
        {
            if (h.Instance == null) continue;

            var tr = h.Instance.transform;
            list.Add(new HazardSaveData
            {
                HazardType = h.Type,
                PosX = tr.position.x,
                PosY = tr.position.y,
                PosZ = tr.position.z,
                RotationY = tr.eulerAngles.y,
                TimeLeft = h.TicksLeft,
                DamageFactor = h.DamageFactor
            });
        }
        return list.ToArray();
    }

    public void LoadHazardData(HazardSaveData[] data, bool isStartMission)
    {
        if (isStartMission || data == null || data.Length == 0) return;

        _activeHazards.Clear();

        foreach (var h in data)
        {
            if (h.HazardType < 0 || h.HazardType >= _hazardPrefabs.Length) continue;

            var ticksLeft = Mathf.Max(1, Mathf.CeilToInt(h.TimeLeft));
            var prefab = _hazardPrefabs[h.HazardType];
            var obj = Instantiate(prefab, new Vector3(h.PosX, h.PosY, h.PosZ), Quaternion.Euler(0, h.RotationY, 0));

            var dmg = obj.GetComponent<OnTriggerStayDealDamage>();
            dmg.SetInfo(ticksLeft, h.DamageFactor);

            RegisterHazard(h.HazardType, obj, ticksLeft, h.DamageFactor);
        }
    }

    private void OnTimeTick()
    {
        for (int i = _activeHazards.Count - 1; i >= 0; i--)
        {
            var hazard = _activeHazards[i];
            hazard.TicksLeft--;

            if (hazard.TicksLeft <= 0 || hazard.Instance == null)
            {
                _activeHazards.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public enum HazardEnum
{
    AcidRain = 0,
    IgniteSkill = 1,
    ToxicGas = 2,
}
