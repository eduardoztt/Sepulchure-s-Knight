using UnityEngine;

public class TestKnockback : MonoBehaviour
{
    public float testForce = 50f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(-testForce, testForce / 2f); // Empurra para a esquerda e um pouco para cima
                Debug.Log("Teste de Knockback Ativado!");
            }
            else
            {
                Debug.LogError("Rigidbody2D não encontrado no GameObject!");
            }
        }
    }
}