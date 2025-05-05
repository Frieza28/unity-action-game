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
        if (!CanUse) return;  // Cooldown ainda ativo

        lastUsedTime = Time.time;

        animator.SetTrigger(powerAttack.animationTrigger);

        // Instancia o VFX
        if (powerAttack.vfxPrefab != null && firePoint != null)
        {
            GameObject vfx = Instantiate(
                powerAttack.vfxPrefab,
                firePoint.position,
                Quaternion.LookRotation(firePoint.forward)
            );

            // Ativar a particle system manualmente
            var ps = vfx.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
                ps.Play();

            // Direcionamento do projétil
            EnergyProjectile proj = vfx.GetComponent<EnergyProjectile>();
            if (proj != null)
                proj.SetDirection(firePoint.forward);

            Destroy(vfx, 2f);
        }

        // Aplica dano
        Collider[] hits = Physics.OverlapSphere(firePoint.position, powerAttack.range, powerAttack.targetMask);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Damageable target))
            {
                target.ApplyDamage(powerAttack.damage);
            }
        }
    }
}
