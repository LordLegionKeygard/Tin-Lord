using System.Collections;
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
        public float TimeLeft;
        public float DamageFactor;
    }

    public void RegisterHazard(int type, GameObject obj, float duration, float damageFactor)
    {
        _activeHazards.Add(new ActiveHazard
        {
            Type = type,
            Instance = obj,
            TimeLeft = duration,
            DamageFactor = damageFactor
        });
    }

    // вызывается, когда хазард досрочно умер
    // public void UnregisterHazard(GameObject obj)
    // {
    //     _activeHazards.RemoveAll(h => h.Instance == obj);
    // }

    public HazardSaveData[] GetHazards()
    {
        var list = new List<HazardSaveData>(_activeHazards.Count);
        foreach (var h in _activeHazards)
        {
            var tr = h.Instance.transform;
            list.Add(new HazardSaveData
            {
                HazardType = h.Type,
                PosX = tr.position.x,
                PosY = tr.position.y,
                PosZ = tr.position.z,
                RotationY = tr.eulerAngles.y,
                TimeLeft = h.TimeLeft,
                DamageFactor = h.DamageFactor
            });
        }
        return list.ToArray();
    }

    public void LoadHazardData(HazardSaveData[] data)
    {
        foreach (var h in data)
        {
            var prefab = _hazardPrefabs[h.HazardType];
            var obj = Instantiate(prefab, new Vector3(h.PosX, h.PosY, h.PosZ), Quaternion.Euler(0, h.RotationY, 0));

            var dmg = obj.GetComponent<OnTriggerStayDealDamage>();
            dmg.SetInfo(Mathf.CeilToInt(h.TimeLeft), h.DamageFactor);

            RegisterHazard(h.HazardType, obj, h.TimeLeft, h.DamageFactor);
        }
    }

    private void Update()
    {
        // идём с конца, чтобы безопасно удалять
        for (int i = _activeHazards.Count - 1; i >= 0; i--)
        {
            var hazard = _activeHazards[i];
            hazard.TimeLeft -= Time.deltaTime;
            if (hazard.TimeLeft <= 0f)
            {
                _activeHazards.RemoveAt(i); // объект сам уничтожится по таймеру
            }
        }
    }
}

[System.Serializable]
public enum HazardEnum
{
    AcidRain = 0,
    IgniteSkill = 1,
}

