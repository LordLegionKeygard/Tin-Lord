using UnityEngine;
using Zenject;

public class MapSystem : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _save;

    [SerializeField] private MapGenerator _generator;
    [SerializeField] private MapVisualizer _visualizer;
    [SerializeField] private ConnectionsDrawer _drawer;

    private void OnEnable() => CustomEvents.OnDataLoad += HandleDataLoaded;
    private void OnDisable() => CustomEvents.OnDataLoad -= HandleDataLoaded;

    private void HandleDataLoaded()
    {
        var data = _save.CommandCenterSaveData;

        bool needGenerate = data.Map == null || data.Map.Nodes == null || data.Map.Nodes.Count == 0;

        if (needGenerate)
        {
            // Debug.Log("1  — новая карта");
            _generator.GenerateMap();
            data.Map = _generator.SavedMap;

            _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(data);
        }
        else
        {
            // Debug.Log("2  — загружаем сохранённую карту");
            RestoreNodes(data.Map);
        }

        _drawer.DrawConnections();
        _visualizer.GenerateAndDisplayMap();
    }


    /* ----- восстановление NodeInstance-ов из SavedMapData -------- */
    private void RestoreNodes(SavedMapData map)
    {
        var list = _generator.GetGeneratedNodes();
        list.Clear();

        foreach (var n in map.Nodes)
        {
            list.Add(new NodeInstance
            {
                nodeData = _generator.GetNodeTemplate(n.NodeType),
                layer = n.Layer,
                position = n.Position
            });
        }
        for (int i = 0; i < map.Nodes.Count; i++)
        {
            list[i].connectedNodes = map.Nodes[i].ConnectedNodeIndices.ConvertAll(idx => list[idx]);
        }
    }
}
