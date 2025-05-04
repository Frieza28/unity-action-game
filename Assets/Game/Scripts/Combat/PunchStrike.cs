using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Punch Strike")]
public class PunchStrike : StrikeMove
{
    public override void Execute(Animator animator)
    {
        animator.SetTrigger(animationTrigger);
    }
}
