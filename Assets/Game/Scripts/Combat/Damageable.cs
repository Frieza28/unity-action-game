using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} levou {amount} de dano! Vida restante: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} morreu!");
        // Aqui podes adicionar lógica de morte, animação, desativar AI, etc.
        gameObject.SetActive(false);
    }
}
