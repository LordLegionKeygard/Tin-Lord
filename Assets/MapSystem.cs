using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public MapVisualizer mapVisualizer;
    public ConnectionsDrawer connectionsDrawer;

    private void Start()
    {
        GenerateFullMap();
    }

    public void GenerateFullMap()
    {
        // Генерируем структуру
        mapGenerator.GenerateDesertMap();

        // Спавним визуальные ноды
        mapVisualizer.GenerateAndDisplayMap();

        // Рисуем связи
        connectionsDrawer.DrawConnections();
    }
}
