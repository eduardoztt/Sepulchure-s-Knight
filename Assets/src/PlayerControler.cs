using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    public Rigidbody2D rb;

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

    //variaveis para Knockback
    public float KBForce;
    public float KBCount;
    public float KBTime;
    public bool isKnock;
    public float KBForceUp = 5f;

    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float attackRange = 1.5f; // Adicionando alcance de ataque
    [SerializeField] private LayerMask enemyLayer; // Layer para os inimigos

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        KnockLogic();
        
    }

    private void FixedUpdate(){
        if (KBCount <= 0){
            rb.linearVelocity = new Vector2(horizontalInput * velocidade, rb.linearVelocity.y);
        }
    }

    private void MoveLogic(){
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

    private IEnumerator AttackCoroutine(){
        isAttacking = true;
        animator.SetBool(attackingHash, true);

        // Detectando inimigos no alcance do ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null && !enemyHealth.die)
            {
                enemyHealth.TakeDamage(20);
            }
        }

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        animator.SetBool(attackingHash, false);
    }

    void KnockLogic()
    {
        if (KBCount > 0)
        {
            float direction = isKnock ? -1 : 1;
            rb.linearVelocity = new Vector2(direction * KBForce, KBForceUp);
            KBCount -= Time.deltaTime;
        }
        else
        {
            MoveLogic();
            isKnock = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Mostra o alcance de ataque no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
