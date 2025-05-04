using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Kick Strike")]
public class KickStrike : StrikeMove
{
    [SerializeField] private bool mirrored;

    public override void Execute(Animator animator)
    {
        animator.SetTrigger(animationTrigger);
    }
}
