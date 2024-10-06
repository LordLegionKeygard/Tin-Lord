using UnityEngine;

public class EnemyLevel : MonoBehaviour
{
    [SerializeField] private AiLevelInformation _aiLevelInfo;
    public AiLevelInformation GetAiLevelInformation() => _aiLevelInfo;
    [SerializeField] private int _level;
    public int GetLevel() => _level;

    public void SetLevel(int spawnerLevel)
    {
        _level = spawnerLevel;
    }
}
