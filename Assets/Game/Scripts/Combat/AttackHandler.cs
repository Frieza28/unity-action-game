using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [Header("Strike Moves (order matters: 0-punchL,1-punchR,2-kickL,3-kickR,4-extraL,5-extraR…)")]
    [SerializeField] private AttackSO[] strikes;

    [Header("Special Power")]
    [SerializeField] private AttackSO powerStrike;

    [Header("Refs")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform hitOrigin;   // e.g., chest or weapon socket
    [SerializeField] private LayerMask hittableMask;

    private float[] strikeCd;
    private float powerCd;
    public bool Ready => true;   // placeholder – logic lives in Execute*

    private void Awake()
    {
        strikeCd = new float[strikes.Length];
    }

    /// <summary>Attempt to perform a strike (0 ? index &lt; strikes.Length)</summary>
    public void ExecuteStrike(int index)
    {
        throw new System.NotImplementedException("AttackHandler.ExecuteStrike() not implemented yet.");
    }

    /// <summary>Attempt to perform the power attack</summary>
    public void ExecutePower()
    {
        throw new System.Exception("AttackHandler.ExecutePower() not implemented yet.");
    }

    /// <summary>Cooldown ticking – call from Fighter.Update()</summary>
    public void Tick(float dt)
    {
        for (int i = 0; i < strikeCd.Length; ++i)
            if (strikeCd[i] > 0f) strikeCd[i] -= dt;

        if (powerCd > 0f) powerCd -= dt;
    }

    /* ------------------------------------------------------------------ */
    /*  HOOKS – animation events call OnHit() at the exact contact frame  */
    /* ------------------------------------------------------------------ */
    // Called via Animation Event
    public void OnHit(int attackSlot)        // 0-… index for strikes, -1 for power
    {
        throw new System.NotImplementedException("AttackHandler.OnHit() not implemented yet.");
    }

    /* ------------------------------------------------------------------ */
    /*  PRIVATE HELPERS                                                   */
    /* ------------------------------------------------------------------ */
    private void TriggerAnimation(AttackSO atk)
    {
        throw new System.NotImplementedException("AttackHandler.TriggerAnimation() not implemented yet.");
    }
}
