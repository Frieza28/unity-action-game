using UnityEngine;

public class HitSound : MonoBehaviour
{
    [SerializeField] private AudioClip hitClip;   // um único clip
    private AudioSource src;

    void Awake() => src = GetComponent<AudioSource>();

    public void PlayHit()
    {
        if (hitClip) src.PlayOneShot(hitClip);
    }
}
