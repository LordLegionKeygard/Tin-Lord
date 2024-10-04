using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] private AiLevelInformation _aiLevelInfo;
    public AiLevelInformation GetAiLevelInformation() => _aiLevelInfo;
    public virtual int GetLevel() => 0;
}
