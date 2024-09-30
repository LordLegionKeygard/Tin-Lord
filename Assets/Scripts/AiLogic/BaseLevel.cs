using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] private int _level;
    public int GetLevel() => _level;

    [SerializeField] private AiLevelInformation _aiLevelInfo;
    public AiLevelInformation GetAiLevelInformation() => _aiLevelInfo;

    public void SetLevel(int spawnerLevel)
    {
        _level = spawnerLevel;
    }
}
