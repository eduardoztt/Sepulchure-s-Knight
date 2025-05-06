using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyPatrol enemy;
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
        enemy = GetComponent<EnemyPatrol>();
        if (deathEffect != null)
        {
            
            enemy.animator.SetBool("die", true);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
