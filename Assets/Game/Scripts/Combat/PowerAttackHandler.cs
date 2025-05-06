using UnityEngine;

public class PowerAttackHandler : MonoBehaviour
{
    [SerializeField] private PowerAttackSO powerAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;

    private float lastUsedTime = -Mathf.Infinity;

    public bool CanUse => Time.time >= lastUsedTime + powerAttack.cooldown;

    public void ExecutePower()
    {
        if (!CanUse) return;  

        lastUsedTime = Time.time;

        animator.SetTrigger(powerAttack.animationTrigger);

        if (powerAttack.vfxPrefab != null && firePoint != null)
        {
            GameObject vfx = Instantiate(
                powerAttack.vfxPrefab,
                firePoint.position,
                Quaternion.LookRotation(firePoint.forward)
            );

            var ps = vfx.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
                ps.Play();

            EnergyProjectile proj = vfx.GetComponent<EnergyProjectile>();
            if (proj != null)
                proj.SetDirection(firePoint.forward);

            Destroy(vfx, 2f);
        }

        Collider[] hits = Physics.OverlapSphere(firePoint.position, powerAttack.range, powerAttack.targetMask);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Damageable target))
            {
                target.ApplyHit(true, HitRegion.Body);   
            }
        }
    }

    public float CooldownFraction
    {
        get
        {
            float elapsed = Time.time - lastUsedTime;
            return Mathf.Clamp01(1f - elapsed / powerAttack.cooldown);
        }
    }



}
