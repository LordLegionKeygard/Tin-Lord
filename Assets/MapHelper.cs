// -----------------------------------------------------------------------------
//  MapHelper.cs  ‑  общий набор утилит, которые раньше дублировались в
//  MapGenerator и MapSystem.  Все методы сделаны static, чтобы не требовать
//  экземпляров: просто вызывайте MapHelper.XXX() или используйте расширения
//  (Shuffle()) там, где это удобно.
// -----------------------------------------------------------------------------
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
                limit = pool.MaxOnMap;                                   // один и тот же диалог
            }
            else if (pool.Node is EventNode ev)
            {
                // Уникальные реплики
                limit = Mathf.Clamp(pool.MaxOnMap, 1, ev.Dialogue.Length);
            }
            else
            {
                limit = pool.MaxOnMap;                                   // RewardEvent / любой NodeData
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

        var entry = queue[0];      // «голова» после Shuffle определяет рандом
        queue.RemoveAt(0);

        node = entry.Node;
        poolId = entry.PoolIndex;
        seq = entry.SequenceIndex;
        return true;
    }

    // ════════════════════════════════════════════════════════════════════════
    // 4. Вытягиваем Trader (Resource или Skill) из двух очередей
    // ════════════════════════════════════════════════════════════════════════
    public static bool TryPickTrader(List<ResourceTraderNode> resTraders, List<SkillTraderNode> skillTraders, out NodeData node, out NodeType type)
    {
        // Случайно решаем, кого пробовать первым, если есть оба типа.
        bool takeRes = (resTraders.Count > 0 && skillTraders.Count > 0)
                        ? Random.value < 0.5f
                        : resTraders.Count > 0;

        if (takeRes && resTraders.Count > 0)
        {
            node = resTraders[0];
            resTraders.RemoveAt(0);
            type = NodeType.ResourceTrader;
            return true;
        }
        else if (skillTraders.Count > 0)
        {
            node = skillTraders[0];
            skillTraders.RemoveAt(0);
            type = NodeType.SkillTrader;
            return true;
        }

        node = null;
        type = NodeType.None;
        return false;
    }

    // добавьте в MapHelper.cs
    public static bool TryPickNonMission(
        List<EventEntry> events,
        List<ResourceTraderNode> resTraders,
        List<SkillTraderNode> skillTraders,
        out NodeData node,
        out NodeType type,
        out int poolId,
        out int seqId)
    {
        // 1) выбираем, что пробовать первым: 0 = Event, 1 = Trader
        bool tryEventFirst = Random.value < 0.5f;

        for (int pass = 0; pass < 2; pass++)          // максимум 2 попытки
        {
            if (tryEventFirst && events.Count > 0)
            {
                var e = events[0]; events.RemoveAt(0);
                node = e.Node;
                type = (node is RewardEventNode) ? NodeType.RewardEvent : NodeType.Event;
                poolId = e.PoolIndex;
                seqId = e.SequenceIndex;
                return true;
            }

            // иначе Trader
            if (resTraders.Count + skillTraders.Count > 0)
            {
                bool takeRes = (resTraders.Count > 0 && skillTraders.Count > 0) ?
                               Random.value < 0.5f : resTraders.Count > 0;

                if (takeRes && resTraders.Count > 0)
                {
                    node = resTraders[0]; resTraders.RemoveAt(0);
                    type = NodeType.ResourceTrader;
                }
                else
                {
                    node = skillTraders[0]; skillTraders.RemoveAt(0);
                    type = NodeType.SkillTrader;
                }
                poolId = seqId = -1;
                return true;
            }

            // меняем порядок и пробуем второй раз
            tryEventFirst = !tryEventFirst;
        }

        node = null; type = NodeType.None; poolId = seqId = -1;
        return false;
    }

    public static bool RemoveAtReturn<T>(this List<T> list, int index)
    {
        list.RemoveAt(index);
        return true;
    }
}
