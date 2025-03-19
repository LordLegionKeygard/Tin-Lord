using UnityEngine;

public class EnemyLevel : MonoBehaviour
{
    [SerializeField] private EnemyInformation _information;
    public EnemyInformation GetInformation() => _information;
    private int _level;
    public int GetLevel() => _level;
    public int GetExperience() => _information.GetExperience(_level);

    public void SetLevel(int spawnerLevel)
    {
        _level = spawnerLevel;
    }
}
