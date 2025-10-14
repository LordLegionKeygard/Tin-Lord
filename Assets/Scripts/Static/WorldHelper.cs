/// <summary>
/// Статический набор вспомогательных методов
/// </summary>
public static class WorldHelper
{
    public static float Normalize360(float angle)
    {
        angle %= 360f;
        if (angle < 0f) angle += 360f;
        return angle;
    }
}
