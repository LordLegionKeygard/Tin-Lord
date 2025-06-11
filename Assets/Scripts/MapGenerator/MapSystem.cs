using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public MapVisualizer mapVisualizer;
    public ConnectionsDrawer connectionsDrawer;

    private void Start() //при старте новой игры
    {
        GenerateFullMap();
    }

    public void GenerateFullMap()
    {
        // Генерируем структуру
        mapGenerator.GenerateMap();

        // Спавним визуальные ноды
        mapVisualizer.GenerateAndDisplayMap();

        // Рисуем связи
        connectionsDrawer.DrawConnections();
    }
}
