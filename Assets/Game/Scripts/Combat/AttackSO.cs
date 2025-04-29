using UnityEngine;


[CreateAssetMenu(menuName = "Combat/Attack", fileName = "AttackSO")]
public class AttackSO : ScriptableObject
{
    public string animationTrigger;
    public float cooldown = 0.5f;
    [Header("Hit data (use in Phase 2)")]
    public int damage = 1;
    public float range = 1.5f;
    public AudioClip sfx;
}
