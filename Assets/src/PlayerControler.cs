using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int velocidade = 5;
    [SerializeField] private float jumpForce = 600;

    [SerializeField] private Transform charactereFeet;
    [SerializeField] private LayerMask groundLayer;

    private bool inTheGround;
    private bool isAttacking = false;

    public Animator animator;
    private SpriteRenderer spriteRender;

    private int movingHash = Animator.StringToHash("moving");
    private int jumpingHash = Animator.StringToHash("jumping");
    private int attackingHash = Animator.StringToHash("attack");

    [SerializeField] private float attackDuration = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && inTheGround && !isAttacking)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        inTheGround = Physics2D.OverlapCircle(charactereFeet.position, 0.5f, groundLayer);

        animator.SetBool(movingHash, horizontalInput != 0);
        animator.SetBool(jumpingHash, !inTheGround);
        animator.SetBool(attackingHash, isAttacking);

        if (horizontalInput > 0)
        {
            spriteRender.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRender.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        animator.SetBool(attackingHash, true);

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        animator.SetBool(attackingHash, false);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * velocidade, rb.linearVelocity.y);
    }
}
