using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyPatrol enemy;
    public int vida = 1;
    public bool die = false; 

    public void TakeDamage(int damage)
    {



        if (die)
        {

            return;
        }

        vida -= damage;

        if(vida <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemy = GetComponent<EnemyPatrol>();


        die = true;
        if (enemy != null && enemy.animator != null)
        {
            Debug.Log("animação de morte enemy");
             enemy.animator.Play("death"); 

        }

        

        Destroy(gameObject,2f);
        Debug.Log("Função Die() foi executada.");
    }
}