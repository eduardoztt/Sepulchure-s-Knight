using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida = 1;
    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        vida -= damage;

        if (vida <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // Corrigido aqui
    }
}
