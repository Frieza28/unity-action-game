using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Power Attack")]
public class PowerAttackSO : AttackSO
{
    public GameObject vfxPrefab;
    public LayerMask targetMask;
}
