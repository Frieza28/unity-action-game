using System.Collections.Generic;  
using UnityEngine;


public class AttackHandler : MonoBehaviour
{
    [Header("Strike Moves (order matters: 0-punchL,1-punchR,2-kickL,3-kickR,4-extraL,5-extraR�)")]
    [SerializeField] private AttackSO[] strikes;

    [Header("Power Attack")]
    [SerializeField] private AttackSO powerStrike;

    [Header("Refs")]
    [SerializeField] private Transform hitOrigin;   // e.g., chest or weapon socket
    [SerializeField] private LayerMask hittableMask;

    private Animator animator;

    private float[] strikeCooldowns;
    private float powerCooldown;

    private void Awake()
    {
        strikeCooldowns = new float[strikes.Length];
    }

    public void Init(Animator anim) => animator = anim;

    /// <summary>Attempt to perform a strike (0 ? index &lt; strikes.Length)</summary>
    public void ExecuteStrike(int index)
    {
        if (index < 0 || index >= strikes.Length) return;
        if (strikeCooldowns[index] > 0f) return;

        AttackSO atk = strikes[index];

        // TODO: remove
        Debug.Log($"Strike {index} triggered: {atk.name}");

        animator.SetTrigger(atk.animationTrigger);
        strikeCooldowns[index] = atk.cooldown;
    }

    /// <summary>Attempt to perform the power attack</summary>
    public void ExecutePower()
    {
        if (powerCooldown > 0f) return;

        // TODO: remove
        Debug.Log("Power Attack triggered");
        animator.SetTrigger(powerStrike.animationTrigger);
        powerCooldown = powerStrike.cooldown;
    }

    /// <summary>Cooldown ticking � call from Fighter.Update()</summary>
    public void Tick(float dt)
    {
        for (int i = 0; i < strikeCooldowns.Length; i++)
            if (strikeCooldowns[i] > 0) strikeCooldowns[i] -= dt;

        if (powerCooldown > 0) powerCooldown -= dt;
    }

    public void OnHit(int attackSlot)
    {
        bool isPower = attackSlot == -1;
        AttackSO atk = isPower ? powerStrike : strikes[attackSlot];

        if (!TryGetComponent(out ScoreKeeper attackerSK))
            return;

        var victims = new HashSet<Damageable>();  

        Collider[] hits = Physics.OverlapSphere(
            hitOrigin.position,
            atk.range,
            hittableMask);

        foreach (var col in hits)
        {
            if (col.TryGetComponent(out Hitbox hb) && hb.owner != null)
            {
                if (!victims.Add(hb.owner)) continue;

                int pts = isPower ? 5 :
                          (hb.region == HitRegion.Head ? 2 : 1);

                attackerSK.AddPoints(pts);

                if (hb.owner.TryGetComponent(out HitSound sfx))
                    sfx.PlayHit();

            }
        }
    }

    private void TriggerAnimation(AttackSO atk)
    {
        throw new System.NotImplementedException("AttackHandler.TriggerAnimation() not implemented yet.");
    }
}
