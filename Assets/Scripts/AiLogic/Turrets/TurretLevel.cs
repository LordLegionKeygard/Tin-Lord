

public class TurretLevel : BaseLevel
{
    private TurretBuilding _turretBuilding;
    public override int GetLevel() => _turretBuilding.Building().Level;

    private void Awake()
    {
        _turretBuilding = GetComponent<TurretBuilding>();
    }


}
