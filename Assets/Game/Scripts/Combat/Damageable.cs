using UnityEngine;     

public class Damageable : MonoBehaviour
{
    public void ApplyHit(bool power, HitRegion region)
    {
        int pts = power ? 5 : (region == HitRegion.Head ? 2 : 1);
        if (GetComponent<ScoreKeeper>() is ScoreKeeper sk)
            sk.AddPoints(pts);         
    }


    private void Die()
    {
        Debug.Log($"{name} perdeu!");
        gameObject.SetActive(false);
    }
}
