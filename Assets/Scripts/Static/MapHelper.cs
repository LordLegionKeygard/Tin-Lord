using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Статический набор вспомогательных методов для генерации и «раскрытия» карты.
/// Никакого state внутри — все коллекции передаются как параметры,
/// поэтому класс безопасен для многократных вызовов и Unit‑тестов.
/// </summary>
public static class MapHelper
{

    // Универсальный Fisher‑Yates Shuffle  (расширение для List<T>)
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    // Построение единой очереди Event / RewardEvent из нескольких пулов
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

    // Вытягиваем Event / RewardEvent из очереди
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

    // Вытягиваем Trader (Resource / Skill / WeaponEngineer) из трёх очередей
    public static bool TryPickTrader(
        List<ResourceTraderNode> resTraders,
        List<SkillTraderNode> skillTraders,
        List<WeaponTraderNode> weaponEngineers,
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
                type = NodeType.WeaponTrader;
                break;
        }

        return true;
    }


    public static bool TryPickNonMission(List<EventEntry> events, List<ResourceTraderNode> resourceTraders, List<SkillTraderNode> skillTraders, List<WeaponTraderNode> weaponEngineers, out NodeData node, out NodeType type, out int poolId, out int seqId)
    {
        bool tryEventFirst = Random.value < 0.5f;

        for (int pass = 0; pass < 2; pass++)
        {
            // Сначала Event
            if (tryEventFirst && events.Count > 0)
            {
                var e = events[0]; events.RemoveAt(0);
                node = e.Node;
                type = (node is RewardEventNode) ? NodeType.RestEvent : NodeType.Event;
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
                        type = NodeType.WeaponTrader;
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

    public static bool IsVisible(NodeData data) => data is RewardEventNode or ResourceTraderNode or SkillTraderNode or WeaponTraderNode;

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

    // Индекс «видимого» в слое, либо -1
    public static int GetVisibleIndexInLayer(Dictionary<int, List<NodeInstance>> layers, int layer)
    {
        if (layers == null || !layers.ContainsKey(layer)) return -1;
        var list = layers[layer];
        for (int i = 0; i < list.Count; i++)
            if (IsVisible(list[i].nodeData)) return i;
        return -1;
    }

    // Выбор слота для «видимого»: сначала разнос ≥2, иначе хотя бы ≠ prev
    public static int PickVisibleSlot(int slotsCount, int prevVisibleIdx)
    {
        var candidates = new List<int>();

        for (int i = 0; i < slotsCount; i++)
        {
            if (prevVisibleIdx >= 0 && Mathf.Abs(i - prevVisibleIdx) <= 1) continue;
            candidates.Add(i);
        }

        if (candidates.Count == 0 && prevVisibleIdx >= 0)
        {
            for (int i = 0; i < slotsCount; i++)
                if (i != prevVisibleIdx) candidates.Add(i);
        }

        if (candidates.Count == 0)
            for (int i = 0; i < slotsCount; i++) candidates.Add(i);

        return candidates[Mathf.FloorToInt(Random.value * candidates.Count)];
    }

    public static bool LayerHasVisible(Dictionary<int, List<NodeInstance>> layers, int layer)
    {
        return layers.ContainsKey(layer) && layers[layer].Exists(n => IsVisible(n.nodeData));
    }

    // шумим только НЕвидимые, видимые — «прибиты» к своим индексам
    public static void ShuffleNonVisible(Dictionary<int, List<NodeInstance>> layers)
    {
        foreach (var kv in layers)
        {
            var list = kv.Value;
            // соберём невидимые
            var pinned = new List<(int idx, NodeInstance n)>();
            var free = new List<NodeInstance>();
            for (int i = 0; i < list.Count; i++)
            {
                if (IsVisible(list[i].nodeData)) pinned.Add((i, list[i]));
                else free.Add(list[i]);
            }
            // перетасуем «free»
            free.Shuffle();
            // вернём обратно, не трогая «pinned»
            int f = 0;
            for (int i = 0; i < list.Count; i++)
                if (!IsVisible(list[i].nodeData))
                    list[i] = free[f++];
        }
    }

    /// Подбор плейсхолдера под «открытый» узел:
    /// 1) слой ещё без открытого; 2) по возможности нет открытого в соседних слоях;
    /// 3) по вертикали «через один» от видимого сверху, иначе хотя бы ≠ индекса сверху.
    public static NodeInstance PickSafeStubForVisible(
        List<NodeInstance> generatedNodes,
        Dictionary<int, List<NodeInstance>> layers,
        SavedMapData savedMap)
    {
        // все плейсхолдеры, где в слое ещё нет «видимого»
        var stubs = new List<NodeInstance>();
        for (int i = 0; i < generatedNodes.Count; i++)
        {
            var n = generatedNodes[i];
            if (savedMap.Nodes[i].NodeType == NodeType.None &&
                n.layer > 1 &&
                !LayerHasVisible(layers, n.layer))
                stubs.Add(n);
        }
        if (stubs.Count == 0) return null;

        int BestScore(NodeInstance stub)
        {
            int layer = stub.layer;
            int idxInLayer = layers[layer].IndexOf(stub);
            int prevIdx = GetVisibleIndexInLayer(layers, layer - 1);

            bool neighborsFree =
                !LayerHasVisible(layers, layer - 1) &&
                !LayerHasVisible(layers, layer + 1);

            bool farFromPrev = (prevIdx >= 0) ? Mathf.Abs(idxInLayer - prevIdx) >= 2 : true;
            bool notEqualPrev = (prevIdx < 0) || idxInLayer != prevIdx;

            // 0 — идеально; ниже — хуже
            if (neighborsFree && farFromPrev) return 0;
            if (neighborsFree && notEqualPrev) return 1;
            if (farFromPrev) return 2;
            if (notEqualPrev) return 3;
            return 4;
        }

        // минимальный «штраф»
        int best = int.MaxValue;
        var bucket = new List<NodeInstance>();
        foreach (var s in stubs)
        {
            int sc = BestScore(s);
            if (sc < best) { best = sc; bucket.Clear(); bucket.Add(s); }
            else if (sc == best) bucket.Add(s);
        }
        return bucket[Random.Range(0, bucket.Count)];
    }

}
