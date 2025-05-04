using UnityEngine;

public abstract class StrikeMove : AttackSO
{
    public override void Execute(Animator animator)
    {
        if (!string.IsNullOrEmpty(animationTrigger))
        {
            animator.SetTrigger(animationTrigger);
        }
    }
}
