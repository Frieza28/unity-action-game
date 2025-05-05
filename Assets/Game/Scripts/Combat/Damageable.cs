using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health = 10;

    public void ApplyDamage(float amount)
    {
        health -= Mathf.RoundToInt(amount);
        Debug.Log($"{gameObject.name} levou {amount} de dano. Vida: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} morreu!");
        // Aqui podes pôr uma animação ou destruir
        Destroy(gameObject);
    }
}
