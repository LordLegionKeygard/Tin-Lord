using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Статический набор вспомогательных методов для генерации и «раскрытия» карты.
/// Никакого state внутри — все коллекции передаются как параметры,
/// поэтому класс безопасен для многократных вызовов и Unit‑тестов.
/// </summary>
public static class MapHelper
{
    // ════════════════════════════════════════════════════════════════════════
    // 1. Универсальный Fisher‑Yates Shuffle  (расширение для List<T>)
    // ════════════════════════════════════════════════════════════════════════
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    // ════════════════════════════════════════════════════════════════════════
    // 2. Построение единой очереди Event / RewardEvent из нескольких пулов
    // ════════════════════════════════════════════════════════════════════════
    public static List<EventEntry> BuildEventEntries(EventPool[] pools)
    {
        var list = new List<EventEntry>();

        for (int poolIndex = 0; poolIndex < pools.Length; poolIndex++)
        {
            var pool = pools[poolIndex];
            int limit;

            if (pool.RepeatOneEventSameTime)
            {
                limit = pool.MaxOnMap; // один и тот же диалог
            }
            else if (pool.Node is EventNode ev)
            {
                // Уникальные реплики
                limit = Mathf.Clamp(pool.MaxOnMap, 1, ev.Dialogue.Length);
            }
            else
            {
                limit = pool.MaxOnMap; // RewardEvent / любой NodeData
            }

            for (int i = 0; i < limit; i++)
            {
                list.Add(new EventEntry
                {
                    Node = pool.Node,
                    PoolIndex = poolIndex,
                    SequenceIndex = pool.RepeatOneEventSameTime ? 0 : i
                });
            }
        }

        return list;
    }

    // ════════════════════════════════════════════════════════════════════════
    // 3. Вытягиваем Event / RewardEvent из очереди
    // ════════════════════════════════════════════════════════════════════════
    public static bool TryPickEvent(List<EventEntry> queue, out NodeData node, out int poolId, out int seq)
    {
        if (queue.Count == 0)
        {
            node = null; poolId = -1; seq = -1;
            return false;
        }

        var entry = queue[0]; // «голова» после Shuffle определяет рандом
        queue.RemoveAt(0);

        node = entry.Node;
        poolId = entry.PoolIndex;
        seq = entry.SequenceIndex;
        return true;
    }

    // ════════════════════════════════════════════════════════════════════════
    // 4. Вытягиваем Trader (Resource / Skill / WeaponEngineer) из трёх очередей
    // ════════════════════════════════════════════════════════════════════════
    public static bool TryPickTrader(
        List<ResourceTraderNode> resTraders,
        List<SkillTraderNode> skillTraders,
        List<WeaponEngineerNode> weaponEngineers,
        out NodeData node,
        out NodeType type)
    {
        // Собираем доступные варианты
        var options = new List<int>(3);
        if (resTraders.Count > 0) options.Add(0);
        if (skillTraders.Count > 0) options.Add(1);
        if (weaponEngineers.Count > 0) options.Add(2);

        if (options.Count == 0)
        {
            node = null;
            type = NodeType.None;
            return false;
        }

        // Равновероятно выбираем среди непустых
        int pick = options[Mathf.FloorToInt(Random.value * options.Count)];
        switch (pick)
        {
            case 0:
                node = resTraders[0];
                resTraders.RemoveAt(0);
                type = NodeType.ResourceTrader;
                break;

            case 1:
                node = skillTraders[0];
                skillTraders.RemoveAt(0);
                type = NodeType.SkillTrader;
                break;

            default: // 2 — WeaponEngineer
                node = weaponEngineers[0];
                weaponEngineers.RemoveAt(0);
                type = NodeType.WeaponEngineer;
                break;
        }

        return true;
    }


    public static bool TryPickNonMission(
        List<EventEntry> events,
        List<ResourceTraderNode> resourceTraders,
        List<SkillTraderNode> skillTraders,
        List<WeaponEngineerNode> weaponEngineers,
        out NodeData node,
        out NodeType type,
        out int poolId,
        out int seqId)
    {
        bool tryEventFirst = Random.value < 0.5f;

        for (int pass = 0; pass < 2; pass++)
        {
            // Сначала Event
            if (tryEventFirst && events.Count > 0)
            {
                var e = events[0]; events.RemoveAt(0);
                node = e.Node;
                type = (node is RewardEventNode) ? NodeType.RewardEvent : NodeType.Event;
                poolId = e.PoolIndex;
                seqId = e.SequenceIndex;
                return true;
            }

            // Иначе Trader (любой из трёх)
            var options = new List<int>(3);
            if (resourceTraders.Count > 0) options.Add(0);
            if (skillTraders.Count > 0) options.Add(1);
            if (weaponEngineers.Count > 0) options.Add(2);

            if (options.Count > 0)
            {
                int pick = options[Mathf.FloorToInt(Random.value * options.Count)];
                switch (pick)
                {
                    case 0:
                        node = resourceTraders[0];
                        resourceTraders.RemoveAt(0);
                        type = NodeType.ResourceTrader;
                        break;
                    case 1:
                        node = skillTraders[0];
                        skillTraders.RemoveAt(0);
                        type = NodeType.SkillTrader;
                        break;
                    default: // 2 — WeaponEngineer
                        node = weaponEngineers[0];
                        weaponEngineers.RemoveAt(0);
                        type = NodeType.WeaponEngineer;
                        break;
                }

                poolId = -1;
                seqId = -1;
                return true;
            }

            // меняем порядок и пробуем второй раз
            tryEventFirst = !tryEventFirst;
        }

        node = null;
        type = NodeType.None;
        poolId = -1;
        seqId = -1;
        return false;
    }


    public static bool RemoveAtReturn<T>(this List<T> list, int index)
    {
        list.RemoveAt(index);
        return true;
    }

    public static bool IsVisible(NodeData d) =>
       d is RewardEventNode || d is ResourceTraderNode || d is SkillTraderNode;

    public static bool TryPop<T>(this IList<T> list, out T value)
    {
        if (list.Count > 0)
        {
            value = list[0];
            list.RemoveAt(0);
            return true;
        }
        value = default;
        return false;
    }

}
