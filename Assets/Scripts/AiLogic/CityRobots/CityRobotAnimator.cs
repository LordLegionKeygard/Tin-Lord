
public class CityRobotAnimator : BaseAnimator
{
    public override void IsCombat(bool state)
    {
        Animator.SetBool(AnimatorStrings.IsCombat, state);
    }
}
