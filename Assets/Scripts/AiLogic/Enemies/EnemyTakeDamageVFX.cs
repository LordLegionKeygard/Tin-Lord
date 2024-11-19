using Pathfinding;

public class EnemyTakeDamageVFX : BaseTakeDamageVFX
{
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }

    private void Start()
    {
        Height = _aiPath.height;
    }

    public override void SpawnTakeDamageVFX()
    {
        if (!WorldGameInfo.StaticBlood) return;
        base.SpawnTakeDamageVFX();
    }
}
