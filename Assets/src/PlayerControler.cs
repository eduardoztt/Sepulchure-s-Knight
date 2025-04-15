using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Vector2 movDir;
    private bool pulando;

    public void Start() {
        pulando = false;
        movDir = new Vector2(0f, 0f);
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        if (movDir.x < 0) {
            spr.flipX = true;
        }

        if (movDir.x > 0)
        {
            spr.flipX = false;
        }

        float velV = rb.linearVelocity.y;

        if (movDir.y > 0 && !pulando) {
            velV += 5f;
        }

        rb.linearVelocity = new Vector2(movDir.x * 10, velV);

        anim.SetFloat("velH", Mathf.Abs(movDir.x));
    }

    public void OnMove(InputValue value)
    {
        movDir = value.Get<Vector2>();
    }

    public void OnFire()
    {
        Debug.Log("ATIROU");
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Chao")) {
            pulando = false;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao")) {
            pulando = false;
        }
    }
}