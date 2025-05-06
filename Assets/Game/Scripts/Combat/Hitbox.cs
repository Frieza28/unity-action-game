using UnityEngine;     

public enum HitRegion { Body, Head }

public class Hitbox : MonoBehaviour
{
    public HitRegion region = HitRegion.Body;
    [HideInInspector] public Damageable owner;

    private void Awake()
    {
        owner = GetComponentInParent<Damageable>();
    }
}
